

USE master;
GO
ALTER DATABASE tempdb
MODIFY FILE (NAME = tempdev, FILENAME = '{nueva ruta}\tempdb.mdf');
GO
ALTER DATABASE tempdb
MODIFY FILE (NAME = templog, FILENAME = '{nueva ruta}\templog.ldf');
GO


SELECT name, physical_name
FROM sys.master_files
WHERE database_id = DB_ID('tempdb');

