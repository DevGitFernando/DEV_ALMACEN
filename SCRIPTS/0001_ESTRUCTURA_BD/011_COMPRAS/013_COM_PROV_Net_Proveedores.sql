If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_Proveedores' and xType = 'U' ) 
   Drop Table Net_Proveedores 
Go--#SQL 

Create Table Net_Proveedores 
(
	IdProveedor varchar(4) Not Null, 
	LoginProv varchar(20) Not Null, 
	Password varchar(500) Not Null Default '', 
	FechaRegistro Datetime Not Null Default getdate(),  
	FechaActualizacion Datetime Not Null Default getdate(),  		
	Keyx int Identity(1,1), 
	Status varchar(1) Not Null Default 'A', 	
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table Net_Proveedores Add Constraint PK_Net_Proveedores Primary Key ( IdProveedor )
Go--#SQL 

------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Proveedores_Mtto' and xType = 'P' )
   Drop Proc spp_Net_Proveedores_Mtto
Go--#SQL 

Create Proc spp_Net_Proveedores_Mtto ( @IdProveedor varchar(4), @LoginProv varchar(20), @Password varchar(500), @Status varchar(1) ) 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From Net_Proveedores (NoLock) Where IdProveedor = @IdProveedor ) 
	   Insert Into Net_Proveedores ( IdProveedor, LoginProv, Password, Status, Actualizado ) Select @IdProveedor, @LoginProv, @Password, @Status, 0 
	else 
	   Update Net_Proveedores Set Password = @Password, Status = @Status, Actualizado = 0, FechaActualizacion = getdate()  
	   Where IdProveedor = @IdProveedor 

End 
Go--#SQL 