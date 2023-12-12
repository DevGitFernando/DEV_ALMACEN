If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Versiones' and xType = 'U' ) 
Begin  
    CREATE TABLE dbo.Net_Versiones
    (  
        [IdVersion] int identity(1,1),  
        [Modulo] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  
        [NombreVersion] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),          
        [Version] [varchar](20) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  
        [Tipo] int Not Null Default 1, 
        [FechaRegistro] [datetime] NOT NULL DEFAULT (getdate()),  
        [HostName] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (host_name())  
    ) ON [PRIMARY] 
End 
Go--#SQL 


If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Versiones_Regional' and xType = 'U' ) 
Begin  
    CREATE TABLE dbo.Net_Versiones_Regional
    (  
        [IdVersion] int identity(1,1),  
        [Modulo] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  
        [NombreVersion] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),          
        [Version] [varchar](20) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  
        [Tipo] int Not Null Default 1, 
        [FechaRegistro] [datetime] NOT NULL DEFAULT (getdate()),  
        [HostName] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (host_name())  
    ) ON [PRIMARY] 
End 
Go--#SQL 
