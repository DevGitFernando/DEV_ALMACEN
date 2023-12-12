---------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatFarmacias_ProveedoresVales' and xType = 'U' ) 
Begin 
	Create Table CatFarmacias_ProveedoresVales 
	(
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,
		IdProveedor varchar(4) Not Null,
		EsProv_Reembolso bit Not Null Default 0,
		Status varchar(2) Not Null Default 'A',	
		Actualizado tinyint Not Null Default 0
	)

	Alter Table CatFarmacias_ProveedoresVales Add Constraint PK_CatFarmacias_ProveedoresVales 
		Primary Key ( IdEstado, IdFarmacia, IdProveedor ) 

	Alter Table CatFarmacias_ProveedoresVales Add Constraint FK_CatFarmacias_ProveedoresVales_CatFarmacias
		Foreign Key ( IdEstado, IdFarmacia ) 
		References CatFarmacias ( IdEstado, IdFarmacia )

	Alter Table CatFarmacias_ProveedoresVales Add Constraint FK_CatFarmacias_ProveedoresVales_CatProveedores
		Foreign Key ( IdProveedor ) 
		References CatProveedores ( IdProveedor )

End 
Go--#SQL 