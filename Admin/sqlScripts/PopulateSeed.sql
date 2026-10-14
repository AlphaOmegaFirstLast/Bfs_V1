SELECT

'INSERT INTO [Tenant_AhmedSami].[dbo].stkx'+ [Name] +' select * from Tenant_Amgad.[dbo].stkx' + [Name]

 FROM [BestFit_V8].[dbo].[BfsComponent]
 where  [BfsSystemId]=4 and DataTypeId= 3
