If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_Terminales' and xType = 'U' ) 
   Drop Table CFGC_Terminales 
Go--#SQL  

Create Table CFGC_Terminales  
( 
	IdTerminal varchar(4) Not Null, 
	Nombre varchar(50) Not Null, 
	MAC_Address varchar(20) Not Null, 
	EsServidor bit Not Null Default 'False', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL  

Alter Table CFGC_Terminales Add Constraint PK_CFGC_Terminales Primary Key ( IdTerminal ) 
Go--#SQL  

-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Terminales_Versiones' and xType = 'U' ) 
   Drop Table CFG_Terminales_Versiones 
Go--#SQL  

Create Table CFG_Terminales_Versiones
(
	Servidor varchar(100) Not Null Default (''),
	Dll varchar(100) Not Null Default (''),
	Version varchar(50) Not Null Default (''),
	FechaSistema smalldatetime Not Null Default (getdate()),
	HostName varchar(100) Not Null Default (host_name()),
	MAC_Address varchar(100) Not Null Default (''),
	Keyx int Identity(1,1) Not Null
) 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_Terminales_Versiones_TV' and xType = 'U' ) 
   Drop Table CFG_Terminales_Versiones_TV 
Go--#SQL  

Create Table CFG_Terminales_Versiones_TV
(
	HostName varchar(100) Not Null Default (host_name()),
	MAC_Address varchar(20) Not Null Default (''),
	Version_TV varchar(20) Not Null Default (''), 	
	ID_TV varchar(20) Not Null Default ('') 		
) 
Go--#SQL 

                   