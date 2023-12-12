-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatProveedores_DiasDePlazo' and xType = 'U' ) 
Begin 
	Create Table CatProveedores_DiasDePlazo
	(
		IdProveedor varchar(4) Not Null, 
		Dias int Not Null,		
		Status bit Not Null Default 0,
		Predeterminado bit Not Null Default 0,
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CatProveedores_DiasDePlazo Add Constraint PK_CatProveedores_DiasDePlazo Primary Key ( IdProveedor, Dias ) 

	Alter Table CatProveedores_DiasDePlazo Add Constraint FK_CatProveedores_DiasDePlazo_CatProveedores
		Foreign Key ( IdProveedor )  References CatProveedores ( IdProveedor )
		
		
	Insert Into CatProveedores_DiasDePlazo (IdProveedor, Dias, status, Predeterminado)
	Select IdProveedor, 0, 1, 1
	From CatProveedores
	
End 	
Go--#SQL 