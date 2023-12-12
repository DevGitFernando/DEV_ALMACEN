Set NoCount On
Go--#SQL   


------------------------------------------------------------------------------------------------------------ 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_RecetaElectronica_Interfaces' and xType = 'U' ) 
Begin 
	Create Table INT_RecetaElectronica_Interfaces 
	(
		Interface smallint Not Null Default 0, 
		NombreInterface varchar(100) Not Null Default '' UNIQUE, 
		Status varchar(1) Not Null Default 'A'  
	)

	Alter Table INT_RecetaElectronica_Interfaces 
		Add Constraint PK_INT_RecetaElectronica_Interfaces Primary Key ( Interface ) 
End 
Go--#SQL 


If Not Exists ( Select * From INT_RecetaElectronica_Interfaces Where Interface = 1 )  Insert Into INT_RecetaElectronica_Interfaces (  Interface, NombreInterface, Status )  Values ( 1, 'EXPEDIENTE ELECTRONICO PUEBLA', 'A' ) 
 Else Update INT_RecetaElectronica_Interfaces Set NombreInterface = 'EXPEDIENTE ELECTRONICO PUEBLA', Status = 'A' Where Interface = 1  
If Not Exists ( Select * From INT_RecetaElectronica_Interfaces Where Interface = 900 )  Insert Into INT_RecetaElectronica_Interfaces (  Interface, NombreInterface, Status )  Values ( 900, 'INTERMED', 'A' ) 
 Else Update INT_RecetaElectronica_Interfaces Set NombreInterface = 'INTERMED', Status = 'A' Where Interface = 900
If Not Exists ( Select * From INT_RecetaElectronica_Interfaces Where Interface = 2 )  Insert Into INT_RecetaElectronica_Interfaces (  Interface, NombreInterface, Status )  Values ( 2, 'SIGHO', 'A' ) 
 Else Update INT_RecetaElectronica_Interfaces Set NombreInterface = 'SIGHO', Status = 'A' Where Interface = 002
 If Not Exists ( Select * From INT_RecetaElectronica_Interfaces Where Interface = 3 )  Insert Into INT_RecetaElectronica_Interfaces (  Interface, NombreInterface, Status )  Values ( 3, 'AMPM', 'A' ) 
 Else Update INT_RecetaElectronica_Interfaces Set NombreInterface = 'AMPM', Status = 'A' Where Interface = 003
Go--#SQL 


------------------------------------------------------------------------------------------------------------ 
If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_RecetaElectronica' and xType = 'U' ) 
Begin 
	Create Table INT_RecetaElectronica 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		Interface smallint Not Null Default 0, 
		NombreInterface varchar(100) Not Null Default '' UNIQUE, 
		Status varchar(1) Not Null Default 'A'  
	)

	Alter Table INT_RecetaElectronica 
		Add Constraint PK_INT_RecetaElectronica Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Interface ) 
End 
Go--#SQL 

 