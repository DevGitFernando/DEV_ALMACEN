----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_Clientes' and xType = 'U' ) 
   Drop Table INT_ND_Clientes 
Go--#SQL 

Create Table INT_ND_Clientes 
(		
	IdCliente varchar(4)  Not Null,
	CodigoCliente varchar(20)  Not Null, -- Unique,
	Nombre varchar(200)  Not Null,
	IdEstado varchar(2) Not Null,
	IdFarmacia varchar(4) Not Null,	
	EsDeSurtimiento bit not null Default 0,
	Status varchar(1)  Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 	
Go--#SQL 

Alter Table INT_ND_Clientes Add Constraint PK_INT_ND_Clientes Primary Key ( IdCliente ) 
Go--#SQL   

