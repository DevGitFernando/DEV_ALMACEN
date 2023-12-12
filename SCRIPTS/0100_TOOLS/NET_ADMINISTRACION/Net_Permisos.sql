If Exists ( Select Name From Sysobjects Where Name = 'Net_Permisos_Operaciones_Supervisadas' and xType = 'U' )
   Drop Table Net_Permisos_Operaciones_Supervisadas
Go 

-------------------------
If Exists ( Select Name From Sysobjects Where Name = 'Net_Operaciones_Supervisadas' and xType = 'U' )
   Drop Table Net_Operaciones_Supervisadas
Go 

Create Table Net_Operaciones_Supervisadas 
( 
	IdOperacion varchar(4) Not Null, 
	Nombre varchar(50) Not Null Default '' Unique Clustered, 
	Descripcion varchar(200) Not Null, 
	Status varchar(1) Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go 

Alter Table Net_Operaciones_Supervisadas Add Constraint PK_Net_Operaciones_Supervisadas Primary Key ( IdOperacion ) 
Go 


-------------
If Exists ( Select Name From Sysobjects Where Name = 'Net_Permisos_Operaciones_Supervisadas' and xType = 'U' )
   Drop Table Net_Permisos_Operaciones_Supervisadas
Go 

Create Table Net_Permisos_Operaciones_Supervisadas 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdPersonal varchar(4) Not Null, 
	IdOperacion varchar(4) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0
)
Go 

Alter Table Net_Permisos_Operaciones_Supervisadas Add Constraint PK_Net_Permisos_Operaciones_Supervisadas 
	Primary Key ( IdEstado, IdFarmacia, IdPersonal, IdOperacion ) 
Go 

Alter Table Net_Permisos_Operaciones_Supervisadas Add Constraint FK_Net_Permisos_Operaciones_Supervisadas_Net_Operaciones_Supervisadas 
	Foreign Key ( IdOperacion ) References Net_Operaciones_Supervisadas ( IdOperacion )  
Go 	

Alter Table Net_Permisos_Operaciones_Supervisadas Add Constraint FK_Net_Permisos_Operaciones_Supervisadas_CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal )  
Go 	
