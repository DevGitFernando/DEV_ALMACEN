If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Terminales_Versiones' and xType = 'U' ) 
Begin  
	CREATE TABLE dbo.CFG_Terminales_Versiones (  
	[Servidor] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  
	[Dll] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  
	[Version] [varchar](50) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  
	[FechaSistema] [smalldatetime] NOT NULL DEFAULT (getdate()),  
	[HostName] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (host_name()),  
	[MAC_Address] [varchar](100) COLLATE Latin1_General_CI_AI NOT NULL DEFAULT (''),  
	[Keyx] int identity(1,1)  
	) ON [PRIMARY] 
End 
Go--#SQL   
 
                   