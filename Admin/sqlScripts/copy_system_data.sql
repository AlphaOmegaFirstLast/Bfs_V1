
INSERT INTO [BestFit_V6].[dbo].BfsSystem
select *
from [BestFit_V5].[dbo].BfsSystem

INSERT INTO [BestFit_V6].[dbo].BfsComponent
select *
from [BestFit_V5].[dbo].BfsComponent

INSERT INTO [BestFit_V6].[dbo].BfsField
select *
from [BestFit_V5].[dbo].BfsField

-- ------------------------------------------------------

INSERT INTO [BestFit_V6].[dbo].SystemAction
select *
from [BestFit_V5].[dbo].SystemAction

--INSERT INTO [BestFit_V6].[dbo].BusinessAction
--select *
--from [BestFit_V5].[dbo].BusinessAction
-- ------------------------------------------------------

INSERT INTO [BestFit_V6].[dbo].BfsComponentSystemAction
select *
from [BestFit_V5].[dbo].BfsComponentSystemAction

--INSERT INTO [BestFit_V6].[dbo].BfsComponentBusinessAction
--select *
--from [BestFit_V5].[dbo].BfsComponentBusinessAction
-- ------------------------------------------------------

INSERT INTO [BestFit_V6].[dbo].DeploymentAzure
select *
from [BestFit_V5].[dbo].DeploymentAzure

INSERT INTO [BestFit_V6].[dbo].DeploymentLocal
select *
from [BestFit_V5].[dbo].DeploymentLocal

-- ------------------------------------------------------

INSERT INTO [BestFit_V6].[dbo].CustomFieldDefinition
select *
from [BestFit_V5].[dbo].CustomFieldDefinition

INSERT INTO [BestFit_V6].[dbo].CustomReports
select *
from [BestFit_V5].[dbo].CustomReports
-- ------------------------------------------------------

INSERT INTO [BestFit_V6].[dbo].BfsTenant
select *
from [BestFit_V5].[dbo].BfsTenant

INSERT INTO [BestFit_V6].[dbo].BfsTenantSystem
select *
from [BestFit_V5].[dbo].BfsTenantSystem
-- ------------------------------------------------------


