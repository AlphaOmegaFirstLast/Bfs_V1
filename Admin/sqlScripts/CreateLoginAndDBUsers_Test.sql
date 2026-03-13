-- sql server authentication for all databases
ALTER LOGIN sa WITH PASSWORD = '12Remember!';
ALTER LOGIN sa ENABLE;

-- create login for a specific tenant
CREATE LOGIN Tenant10User WITH PASSWORD = '12Remember!';

-- create user in the database Tenant10, to link to that login. Instead of Tenant10, Use the company name like AhmedSami
USE Tenant10;
CREATE USER Tenant10User FOR LOGIN Tenant10User;
ALTER ROLE db_datareader ADD MEMBER Tenant10User;
ALTER ROLE db_datawriter ADD MEMBER Tenant10User;
ALTER ROLE db_ddladmin ADD MEMBER Tenant10User;