Set Identity_Insert [BestFit_V6].[dbo].AspNetRoleClaims On;
INSERT INTO [BestFit_V6].[dbo].AspNetRoleClaims
(id, RoleId, ClaimType,ClaimValue)
select *
from [BestFit_V5].[dbo].AspNetRoleClaims
Set Identity_Insert [BestFit_V6].[dbo].AspNetRoleClaims Off;

INSERT INTO [BestFit_V6].[dbo].AspNetRoles
select *
from [BestFit_V5].[dbo].AspNetRoles
------------------------------------------------------
INSERT INTO [BestFit_V6].[dbo].AspNetUsers
select *
from [BestFit_V5].[dbo].AspNetUsers

-- ------------------------------------------------------

Set Identity_Insert [BestFit_V6].[dbo].AspNetUserClaims On;
INSERT INTO [BestFit_V6].[dbo].AspNetUserClaims
(id, UserId, ClaimType,ClaimValue)
select *
from [BestFit_V5].[dbo].AspNetUserClaims
Set Identity_Insert [BestFit_V6].[dbo].AspNetUserClaims Off;

INSERT INTO [BestFit_V6].[dbo].AspNetUserLogins
select *
from [BestFit_V5].[dbo].AspNetUserLogins
------------------------------------------------------
INSERT INTO [BestFit_V6].[dbo].AspNetUserRoles
select *
from [BestFit_V5].[dbo].AspNetUserRoles

INSERT INTO [BestFit_V6].[dbo].AspNetUserTokens
select *
from [BestFit_V5].[dbo].AspNetUserTokens




