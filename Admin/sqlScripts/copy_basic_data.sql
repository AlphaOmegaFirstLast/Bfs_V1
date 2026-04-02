
INSERT INTO [BestFit_V5].[dbo].BfsComponent
select *
from [BestFit_V4].[dbo].BfsComponent

INSERT INTO [BestFit_V5].[dbo].BfsField
select *
from [BestFit_V4].[dbo].BfsField

INSERT INTO [BestFit_V5].[dbo].BfsSystem
select *
from [BestFit_V4].[dbo].BfsSystem

INSERT INTO [BestFit_V5].[dbo].BfsTenant
select *
from [BestFit_V4].[dbo].BfsTenant

INSERT INTO [BestFit_V5].[dbo].BfsTenantSystem
select *
from [BestFit_V4].[dbo].BfsTenantSystem
