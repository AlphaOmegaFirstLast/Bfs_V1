-- Set the target table here (schema optional; default = dbo)
DECLARE @Input SYSNAME = 'BfsManualCode'; -- or 'schema.BfsManualCode'
DECLARE @Schema SYSNAME = PARSENAME(@Input, 2);
DECLARE @Table  SYSNAME = PARSENAME(@Input, 1);
IF @Schema IS NULL SET @Schema = 'dbo';

DECLARE @FullName SYSNAME = QUOTENAME(@Schema) + '.' + QUOTENAME(@Table);
DECLARE @ObjectId INT = OBJECT_ID(@Schema + '.' + @Table);

IF @ObjectId IS NULL
BEGIN
    PRINT 'Table not found: ' + @Schema + '.' + @Table;
    RETURN;
END

SET NOCOUNT ON;

-- Collect and execute drops in a controlled TRY/CATCH block
BEGIN TRY
    DECLARE @sql NVARCHAR(MAX) = N'';

    -- 1) Drop foreign keys in other tables that reference this table
    ;WITH FKs AS (
        SELECT fk.name AS fk_name,
               SCHEMA_NAME(p.schema_id) AS parent_schema,
               p.name AS parent_table
        FROM sys.foreign_keys fk
        JOIN sys.objects p ON fk.parent_object_id = p.object_id
        WHERE fk.referenced_object_id = @ObjectId
    )
    SELECT @sql = @sql + 'ALTER TABLE ' + QUOTENAME(parent_schema) + '.' + QUOTENAME(parent_table)
                       + ' DROP CONSTRAINT ' + QUOTENAME(fk_name) + ';' + CHAR(13)
    FROM FKs;

    -- 2) Drop foreign keys defined on this table (outgoing FKs)
    ;WITH OutFKs AS (
        SELECT fk.name AS fk_name
        FROM sys.foreign_keys fk
        WHERE fk.parent_object_id = @ObjectId
    )
    SELECT @sql = @sql + 'ALTER TABLE ' + @FullName + ' DROP CONSTRAINT ' + QUOTENAME(fk_name) + ';' + CHAR(13)
    FROM OutFKs;

    -- 3) Drop triggers defined on this table
    ;WITH Trigs AS (
        SELECT t.name AS trig_name
        FROM sys.triggers t
        WHERE t.parent_id = @ObjectId AND t.is_ms_shipped = 0
    )
    SELECT @sql = @sql + 'DROP TRIGGER ' + QUOTENAME(@Schema) + '.' + QUOTENAME(trig_name) + ' ON ' + @FullName + ';' + CHAR(13)
    FROM Trigs;

    -- 4) Drop dependent schema-bound objects tracked by the dependency catalog
    ;WITH Deps AS (
        SELECT DISTINCT 
               o.object_id, o.name, SCHEMA_NAME(o.schema_id) AS obj_schema, o.type
        FROM sys.sql_expression_dependencies sed
        JOIN sys.objects o ON sed.referencing_id = o.object_id
        WHERE sed.referenced_id = @ObjectId
          AND o.is_ms_shipped = 0
    )
    SELECT @sql = @sql +
        CASE d.type
            WHEN 'V'  THEN 'DROP VIEW ' + QUOTENAME(d.obj_schema) + '.' + QUOTENAME(d.name) + ';' + CHAR(13)
            WHEN 'P'  THEN 'DROP PROCEDURE ' + QUOTENAME(d.obj_schema) + '.' + QUOTENAME(d.name) + ';' + CHAR(13)
            WHEN 'FN' THEN 'DROP FUNCTION ' + QUOTENAME(d.obj_schema) + '.' + QUOTENAME(d.name) + ';' + CHAR(13)
            WHEN 'IF' THEN 'DROP FUNCTION ' + QUOTENAME(d.obj_schema) + '.' + QUOTENAME(d.name) + ';' + CHAR(13)
            WHEN 'TF' THEN 'DROP FUNCTION ' + QUOTENAME(d.obj_schema) + '.' + QUOTENAME(d.name) + ';' + CHAR(13)
            WHEN 'SN' THEN 'DROP SYNONYM ' + QUOTENAME(d.obj_schema) + '.' + QUOTENAME(d.name) + ';' + CHAR(13)
            ELSE '-- Skipping unsupported dependent object: ' + QUOTENAME(d.obj_schema) + '.' + QUOTENAME(d.name) + ' (type=' + d.type + ')' + CHAR(13)
        END
    FROM Deps d;

    -- 5) Finally drop the table
    SELECT @sql = @sql + 'DROP TABLE ' + @FullName + ';' + CHAR(13);

    -- Output the actions for review, then execute if desired
    PRINT 'The following DROP statements were generated. REVIEW CAREFULLY before running:';
    PRINT '---- Generated statements ----';
    PRINT @sql;

    -- To actually execute the statements, uncomment the EXEC line below after review:
    -- EXEC sp_executesql @sql;

END TRY
BEGIN CATCH
    PRINT 'Error during script generation:';
    PRINT ERROR_MESSAGE();
END CATCH
GO