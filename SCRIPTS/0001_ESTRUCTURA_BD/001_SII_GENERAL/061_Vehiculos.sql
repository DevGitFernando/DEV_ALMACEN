----------------------------------------------------------------------------------------------------
If Not Exists ( Select * From sysobjects (NoLock) Where Name = 'CatVehiculos' and xType = 'U' ) 
Begin 
	Create Table CatVehiculos
	(
		IdEstado varchar(2) default '' Not Null,
		IdFarmacia varchar(4) default '' Not Null,
		IdVehiculo varchar(8) default '' Not Null,
		Marca varchar(200) default '',
		Modelo datetime Not Null Default GetDate(),
		Descripcion varchar(200) default '',
		NumSerie varchar(50) default '',
		Placas varchar(20) default '',
		FechaRegistro datetime Not Null Default GetDate(),
		Status varchar(1) Not Null Default 'A',
		Actualizado tinyint Not Null Default 0
	)

	Alter Table CatVehiculos Add Constraint PK_CatVehiculos
	Primary Key ( IdEstado,  IdFarmacia, IdVehiculo )

	Alter Table CatVehiculos Add Constraint FK_CatVehiculos_CatFarmacias 
	Foreign Key ( IdEstado,  IdFarmacia) References CatFarmacias 
End 	
Go--#SQL 
