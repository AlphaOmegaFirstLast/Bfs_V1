-- Tenant Database
SELECT * FROM [Tenant_HaniAyad].[dbo].[athUserRequest]

SELECT * FROM [Tenant_HaniAyad].[dbo].[athUser]

-- Master Database
SELECT [Id],[UserName],[NormalizedUserName] FROM   [BestFit_V5].[dbo].[AspNetUsers]

SELECT [Id],[UserId],[ClaimType],[ClaimValue]FROM   [BestFit_V5].[dbo].[AspNetUserClaims]
