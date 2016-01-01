USE [master];
GO
--Create a server-level login named liakat with password l0cky0urstay007
CREATE LOGIN [kaustav] WITH PASSWORD = 'l0cky0urstay007';
--Set context to msdb database
USE [LYSAdmin];
GO
--Create a database user named liakat and link it to server-level login liakat
CREATE USER [kaustav] FOR LOGIN [kaustav];
GO
--Added database user liakat in msdb to db_owner in msdb
EXEC sp_addrolemember [db_owner], [kaustav];