USE [BestFit_V5]
GO
declare @tenantId bigint = 686238956320700
declare @userId bigint = 200200200200
declare @roleUserId bigint = 211211211211
declare @aspNetUserId nvarchar(450) = '2d2ad3ab-1a6a-44a7-8d1a-b2b79cd8e49c'
declare @bfsAdminName nvarchar(max) = 'Suzette'

INSERT INTO [AspNetUserClaims]
           ([UserId]
           ,[ClaimType]
           ,[ClaimValue])
     VALUES
           (@aspNetUserId
           ,'Tenant'
           ,@tenantId)
GO


