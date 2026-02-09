WITH UpdatedData AS (
    SELECT 
        id, 
        uiFormControlOrder,
        ROW_NUMBER() OVER (
            PARTITION BY componentId 
            ORDER BY uiFormControlOrder ASC, id ASC
        ) AS new_order
    FROM 
        TableField
)

UPDATE TableField
SET uiFormControlOrder = UpdatedData.new_order
FROM UpdatedData
WHERE TableField.id = UpdatedData.id;