
INSERT INTO [BestFit_V8].[dbo].BfsSystem
select *
from [BestFit_V7].[dbo].BfsSystem

INSERT INTO [BestFit_V8].[dbo].BfsComponent
select *
from [BestFit_V7].[dbo].BfsComponent

INSERT INTO [BestFit_V8].[dbo].BfsField
select *
from [BestFit_V7].[dbo].BfsField

-- ------------------------------------------------------

INSERT INTO [BestFit_V8].[dbo].SystemAction
select *
from [BestFit_V7].[dbo].SystemAction

INSERT INTO [BestFit_V8].[dbo].BusinessAction
select *
from [BestFit_V7].[dbo].BusinessAction
-- ------------------------------------------------------

INSERT INTO [BestFit_V8].[dbo].BfsComponentSystemAction
select *
from [BestFit_V7].[dbo].BfsComponentSystemAction

INSERT INTO [BestFit_V8].[dbo].BfsComponentBusinessAction
select *
from [BestFit_V7].[dbo].BfsComponentBusinessAction
-- ------------------------------------------------------

INSERT INTO [BestFit_V8].[dbo].DeploymentAzure
select *
from [BestFit_V7].[dbo].DeploymentAzure

INSERT INTO [BestFit_V8].[dbo].DeploymentLocal
select *
from [BestFit_V7].[dbo].DeploymentLocal

-- ------------------------------------------------------

INSERT INTO [BestFit_V8].[dbo].CustomFieldDefinition
select *
from [BestFit_V7].[dbo].CustomFieldDefinition

INSERT INTO [BestFit_V8].[dbo].CustomReports
select *
from [BestFit_V7].[dbo].CustomReports
-- ------------------------------------------------------

INSERT INTO [BestFit_V8].[dbo].BfsTenant
select *
from [BestFit_V7].[dbo].BfsTenant

INSERT INTO [BestFit_V8].[dbo].BfsTenantSystem
select *
from [BestFit_V7].[dbo].BfsTenantSystem
-- ------------------------------------------------------


