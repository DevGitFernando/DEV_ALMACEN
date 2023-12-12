Set NoCount On
Go--#SQL   

------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_RecetaElectronica' and xType = 'U' ) 
   Drop Table INT_RecetaElectronica 
Go--#SQL   

Create Table INT_RecetaElectronica 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Interface smallint Not Null Default 0, 
	NombreInterface varchar(100) Not Null Default '' UNIQUE, 
	Status varchar(1) Not Null Default 'A'  
)
Go--#SQL   

Alter Table INT_RecetaElectronica 
	Add Constraint PK_INT_RecetaElectronica Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Interface ) 
Go--#SQL   



Go--#SQL 

 