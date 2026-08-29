-- sql server authentication for all databases
ALTER LOGIN sa WITH PASSWORD = '12Remember!';
ALTER LOGIN sa ENABLE;
-- Even if you changed the password, SQL Server will not allow SQL logins unless Mixed Mode is enabled.
-- Verify it:
--In SSMS → right‑click the server → Properties → Security  
--Check:
--      ✔ SQL Server and Windows Authentication mode
--If it still shows Windows Authentication mode, SQL logins will always fail.
--After changing it:
--   You must restart SQL Server:
--      SSMS → right‑click server → Restart
--   Or restart the service: SQL Server (MSSQLSERVER)

-- create login for a specific tenant
CREATE LOGIN Tenant10User WITH PASSWORD = '12Remember!';

-- create user in the database Tenant10, to link to that login. Instead of Tenant10, Use the company name like AhmedSami
USE Tenant10;
CREATE USER Tenant10User FOR LOGIN Tenant10User;
ALTER ROLE db_datareader ADD MEMBER Tenant10User;
ALTER ROLE db_datawriter ADD MEMBER Tenant10User;
ALTER ROLE db_ddladmin ADD MEMBER Tenant10User;