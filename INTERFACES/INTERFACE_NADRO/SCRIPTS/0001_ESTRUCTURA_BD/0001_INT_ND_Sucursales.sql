----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_Sucursales' and xType = 'U' ) 
   Drop Table INT_ND_Sucursales 
Go--#SQL 

Create Table INT_ND_Sucursales 
(		
	IdEstado varchar(2) Not Null,
	IdSucursal varchar(20) Not Null,	
	SucursalNombre varchar(20) Not Null, 
	Status varchar(1)  Not Null Default 'A' 
) 	
Go--#SQL 

Alter Table INT_ND_Sucursales Add Constraint PK_INT_ND_Sucursales Primary Key ( IdEstado, IdSucursal ) 
Go--#SQL   

If Not Exists ( Select * From INT_ND_Sucursales Where IdEstado = '07' and IdSucursal = '42' )  Insert Into INT_ND_Sucursales (  IdEstado, IdSucursal, SucursalNombre, Status )  Values ( '07', '42', 'Sucursal Chiapas', 'A' )    Else Update INT_ND_Sucursales Set SucursalNombre = 'Sucursal Chiapas', Status = 'A' Where IdEstado = '07' and IdSucursal = '42'  
If Not Exists ( Select * From INT_ND_Sucursales Where IdEstado = '16' and IdSucursal = '43' )  Insert Into INT_ND_Sucursales (  IdEstado, IdSucursal, SucursalNombre, Status )  Values ( '16', '43', 'Sucursal Michoacan', 'A' )    Else Update INT_ND_Sucursales Set SucursalNombre = 'Sucursal Michoacan', Status = 'A' Where IdEstado = '16' and IdSucursal = '43'  
Go--#SQL 


