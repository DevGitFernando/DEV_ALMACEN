----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_Existencias' and xType = 'U' ) 
   Drop Table INT_ND_Existencias 
Go--#SQL 

Create Table INT_ND_Existencias 
( 
	Keyx int Identity(1, 1), 
	Keyx_Auxiliar int Not Null, 	
	FechaRegistro datetime Not Null Default getdate(), 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	FolioIntegracion int Not Null, 	
	ClaveSSA_ND varchar(20) Not Null, 
	CodigoEAN_ND varchar(20)  Not Null, 
	ClaveSSA varchar(20) Not Null Default '', 
	IdProducto varchar(8) Not Null Default '', 
	CodigoEAN varchar(20) Not Null Default '', 
	Cantidad int Not Null Default 0, 
	CantidadAsignada int Not Null Default 0 
) 	
Go--#SQL 

Alter Table INT_ND_Existencias Add Constraint PK_INT_ND_Existencias Primary Key ( Keyx_Auxiliar ) 
Go--#SQL 

----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_Existencias_Historico' and xType = 'U' ) 
   Drop Table INT_ND_Existencias_Historico  
Go--#SQL 

Create Table INT_ND_Existencias_Historico 
( 
	Keyx int Identity(1, 1), 
	FechaRegistro datetime Not Null Default getdate(), 
	IdEmpresa varchar(3) Not Null, 	
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	FolioIntegracion int Not Null, 	
	ClaveSSA_ND varchar(20) Not Null, 
	CodigoEAN_ND varchar(20)  Not Null, 
	ClaveSSA varchar(20) Not Null Default '', 
	IdProducto varchar(8) Not Null Default '', 
	CodigoEAN varchar(20) Not Null Default '', 
	Cantidad int Not Null Default 0, 
	CantidadAsignada int Not Null Default 0 		
) 	
Go--#SQL 



