use Tenant_AhmedSami
go

INSERT INTO [dbo].[athRoleComponentSystemAction]
           ([Id]
           ,[TenantId]
           ,[IsDeleted]
           ,[BfsComponentId]
           ,[SystemActionId]
           ,[RoleId])
     select 
           1,0,0
           , id
           ,1
           ,2
     from bestfit_v5.dbo.BfsComponent
     where name = 'UserRequest'


