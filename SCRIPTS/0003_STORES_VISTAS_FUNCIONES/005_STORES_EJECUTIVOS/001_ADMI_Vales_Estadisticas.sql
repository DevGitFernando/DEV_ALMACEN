If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ADMI_Vales_Estadisticas' and xType = 'U' ) 
Begin 
	Create Table ADMI_Vales_Estadisticas 
	( 
		Año int Not Null Default 0,
		Mes int Not Null Default 0,
		IdEmpresa varchar(3) Not Null Default '',
		IdEstado varchar(2) Not Null Default '',
		IdFarmacia varchar(4) Not Null Default '', 
		ClaveSSA varchar(50) Not Null Default '', 
		IdProveedor varchar(4) Not Null Default '', 
		ValesEmitidos_Mes int Not Null Default 0, 
		ValesRegistrados_Mes int Not Null Default 0, 
		PiezasEmitidas_Mes int Not Null Default 0, 
		PiezasRegistradas_Mes int Not Null Default 0,
		ImporteRegistrado_Mes Numeric(14,4) Not Null Default 0,
		ValesEmitidos_Farmacia int Not Null Default 0, 
		ValesRegistrados_Farmacia int Not Null Default 0, 
		PiezasEmitidas_Farmacia int Not Null Default 0, 
		PiezasRegistradas_Farmacia int Not Null Default 0, 
		ImporteRegistrado_Farmacia Numeric(14,4) Not Null Default 0,
		ValesEmitidos_Clave int Not Null Default 0, 
		ValesRegistrados_Clave int Not Null Default 0, 
		PiezasEmitidas_Clave int Not Null Default 0, 
		PiezasRegistradas_Clave int Not Null Default 0,
		ImporteRegistrado_Clave Numeric(14,4) Not Null Default 0,
		PrecioLicitacion_Clave Numeric(14,4) Not Null Default 0,
		CostoMinimo_Clave Numeric(14,4) Not Null Default 0,
		CostoMaximo_Clave Numeric(14,4) Not Null Default 0,
		ValesRegistrados_Proveedor int Not Null Default 0, 
		PiezasRegistradas_Proveedor int Not Null Default 0,
		ImporteRegistrado_Proveedor Numeric(14,4) Not Null Default 0,
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table ADMI_Vales_Estadisticas Add Constraint PK_ADMI_Vales_Estadisticas Primary Key ( Año, Mes, IdEmpresa, IdEstado, IdFarmacia, ClaveSSA, IdProveedor )

	Alter Table ADMI_Vales_Estadisticas Add Constraint FK_ADMI_Vales_Estadisticas_CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )

End 
Go--#SQL 

