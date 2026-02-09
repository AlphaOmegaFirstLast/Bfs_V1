UPDATE T
SET id = T.row_num
FROM (
    SELECT
        id,
        ROW_NUMBER() OVER (ORDER BY componentId ASC) AS row_num
    FROM
        TableField
) AS T
WHERE id = T.id; -- You may need a join condition if not using CTE (depends on the SQL version/flavor).