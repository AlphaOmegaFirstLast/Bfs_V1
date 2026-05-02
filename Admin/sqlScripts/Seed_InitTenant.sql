declare @tenantId bigint = 686238956320700
declare @userId bigint = 200200200200
declare @roleUserId bigint = 211211211211
declare @aspNetUserId nvarchar(450) = '2d2ad3ab-1a6a-44a7-8d1a-b2b79cd8e49c'
declare @bfsAdminName nvarchar(max) = 'Suzette'
declare @SystemId bigint = 2 --Auth system
declare @Logo nvarchar(450) = 'BackOffice.jpg'

SELECT *  FROM [athUser]
SELECT *  FROM [athRole]
SELECT *  FROM [athRoleUser]
--delete [athUser]
--delete [athRole]
--delete [athRoleUser]
-- For Each new Tenant
-- Add application to a system
INSERT INTO [dbo].[athApp]
           ([Id]
           ,[TenantId]
           ,[IsDeleted]
           ,[Name]
           ,[Notes]
           ,[Logo]
           ,[BfsSystemId])
     VALUES
           (1
           ,@tenantId
           ,0
           ,'Auth-b.ofc'
           ,'The application defines which menu items accessible to the backend'
           ,@Logo
           ,@SystemId)

-- Add Role of BFS Admin in the tenant DB
INSERT INTO [dbo].[athRole]
           ([Id]
           ,[TenantId]
           ,[IsDeleted]
           ,[Name]
           ,[Notes])
     VALUES
           (1
           ,@tenantId
           ,0
           ,'bfs.admin'
           ,'BestFit Admin')

-- Add Role of Client Admin in the tenant DB
INSERT INTO [dbo].[athRole]
           ([Id]
           ,[TenantId]
           ,[IsDeleted]
           ,[Name]
           ,[Notes])
     VALUES
           (3
           ,@tenantId
           ,0
           ,'client.admin'
           ,'Client Admin')
--------------------------------------------
-- For each new Bfs Admin, Link to an AspNet User (Randa)
INSERT INTO [dbo].[athUser]
           ([Id]
           ,[TenantId]
           ,[IsDeleted]
           ,[AspNetUserId]
           ,[Notes]
           ,[Name])
     VALUES
           (@userId
           ,@tenantId
           ,0
           ,@aspNetUserId
           ,'Added by the InitializationScript'
           ,@bfsAdminName
           )
-- Add BFS Admin Role 1 to Bfs User
INSERT INTO [dbo].[athRoleUser]
           ([Id]
           ,[TenantId]
           ,[IsDeleted]
           ,[RoleId]
           ,[UserId])
     VALUES
           (@roleUserId
           ,@tenantId
           ,0
           ,1
           ,@userId)
GO


