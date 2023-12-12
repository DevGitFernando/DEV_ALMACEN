If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CTE_Tipos_Sancion' and xType = 'U' ) 
Begin 
	---		Drop Table Rpt_CTE_Tipos_Sancion  
	Create Table Rpt_CTE_Tipos_Sancion
	(
		Tipo int Not Null Default 1, 
		Sancion varchar(50) Not Null Default '' Unique 
	) 
	
	Alter Table Rpt_CTE_Tipos_Sancion Add Constraint PK_Rpt_CTE_Tipos_Sancion Primary Key ( Tipo ) 
End 
Go--#SQL 

If Not Exists ( Select * From Rpt_CTE_Tipos_Sancion Where Tipo = 1 )  Insert Into Rpt_CTE_Tipos_Sancion (  Tipo, Sancion )  Values ( 1, 'Piezas' )    Else Update Rpt_CTE_Tipos_Sancion Set Sancion = 'Piezas' Where Tipo = 1 
If Not Exists ( Select * From Rpt_CTE_Tipos_Sancion Where Tipo = 3 )  Insert Into Rpt_CTE_Tipos_Sancion (  Tipo, Sancion )  Values ( 3, 'Porcentaje sanción' )    Else Update Rpt_CTE_Tipos_Sancion Set Sancion = 'Porcentaje sanción' Where Tipo = 3 
If Not Exists ( Select * From Rpt_CTE_Tipos_Sancion Where Tipo = 2 )  Insert Into Rpt_CTE_Tipos_Sancion (  Tipo, Sancion )  Values ( 2, 'Sanción' )    Else Update Rpt_CTE_Tipos_Sancion Set Sancion = 'Sanción' Where Tipo = 2 
Go--#SQL 

---------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CTE_Sansiones' and xType = 'U' ) 
Begin 
	---		Drop Table Rpt_CTE_Sansiones  
	Create Table Rpt_CTE_Sansiones
	(
		IdEstado varchar(2) Not Null Default '', 
		IdJurisdiccion varchar(3) Not Null Default '', 
		IdFarmacia varchar(4) Not Null Default '', 
		Año int Not Null Default 0, 
		Mes int Not Null Default 0 
	) 
End 
Go--#SQL 

---------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CTE_VentasEstadisticaClavesDispensadas' and xType = 'U' ) 	
Begin 
	Create Table Rpt_CTE_VentasEstadisticaClavesDispensadas 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		FolioVenta varchar(30) Not Null, 
		IdClaveSSA varchar(4) Not Null, 
		
		Observaciones varchar(100) Not Null Default '', 	
		EsCapturada bit Not Null Default 'false', 
		
		CantidadRequerida numeric(14,4) Not Null Default 0, 
		CantidadEntregada numeric(14,4) Not Null Default 0, 
		
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0  
	)

	Alter Table Rpt_CTE_VentasEstadisticaClavesDispensadas Add Constraint PK_Rpt_CTE_VentasEstadisticaClavesDispensadas 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdClaveSSA ) 

	Alter Table Rpt_CTE_VentasEstadisticaClavesDispensadas Add Constraint FK_Rpt_CTE_VentasEstadisticaClavesDispensadas_VentasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 

	Alter Table Rpt_CTE_VentasEstadisticaClavesDispensadas Add Constraint FK_Rpt_CTE_VentasEstadisticaClavesDispensadas_CatClavesSSA_Sales 
		Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 

End 
Go--#SQL 	



---------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CTE_Sanciones_Detalles' and xType = 'U' ) 
Begin
---		Drop Table Rpt_CTE_Sanciones_Detalles 
	CREATE TABLE Rpt_CTE_Sanciones_Detalles
	(
		IdEstado varchar(2) Not Null Default '',
		Estado varchar(200) Not Null Default '',
		IdJurisdiccion varchar(4) Not Null Default '',
		Jurisdiccion varchar(200) Not Null Default '',
		IdFarmacia varchar(4) Not Null Default '',
		Farmacia varchar(200) Not Null Default '',
		ClaveSSA varchar(30) Not Null Default '',
		DescripcionClave varchar(7800) Not Null Default '',
		Presentacion varchar(50) Not Null Default '',
		PrecioLicitacion numeric(14, 4) Not Null Default 0,
		Tipo int Not Null Default 0,
		DescTipo varchar(50) Not Null Default '',
		Año int Not Null Default 0,
		Mes int Not Null Default 0,
		Total numeric(14, 4) Not Null Default 0,
		Dia_01 numeric(14, 4) Not Null Default 0,
		Dia_02 numeric(14, 4) Not Null Default 0,
		Dia_03 numeric(14, 4) Not Null Default 0,
		Dia_04 numeric(14, 4) Not Null Default 0,
		Dia_05 numeric(14, 4) Not Null Default 0,
		Dia_06 numeric(14, 4) Not Null Default 0,
		Dia_07 numeric(14, 4) Not Null Default 0,
		Dia_08 numeric(14, 4) Not Null Default 0,
		Dia_09 numeric(14, 4) Not Null Default 0,
		Dia_10 numeric(14, 4) Not Null Default 0,
		Dia_11 numeric(14, 4) Not Null Default 0,
		Dia_12 numeric(14, 4) Not Null Default 0,
		Dia_13 numeric(14, 4) Not Null Default 0,
		Dia_14 numeric(14, 4) Not Null Default 0,
		Dia_15 numeric(14, 4) Not Null Default 0,
		Dia_16 numeric(14, 4) Not Null Default 0,
		Dia_17 numeric(14, 4) Not Null Default 0,
		Dia_18 numeric(14, 4) Not Null Default 0,
		Dia_19 numeric(14, 4) Not Null Default 0,
		Dia_20 numeric(14, 4) Not Null Default 0,
		Dia_21 numeric(14, 4) Not Null Default 0,
		Dia_22 numeric(14, 4) Not Null Default 0,
		Dia_23 numeric(14, 4) Not Null Default 0,
		Dia_24 numeric(14, 4) Not Null Default 0,
		Dia_25 numeric(14, 4) Not Null Default 0,
		Dia_26 numeric(14, 4) Not Null Default 0,
		Dia_27 numeric(14, 4) Not Null Default 0,
		Dia_28 numeric(14, 4) Not Null Default 0,
		Dia_29 numeric(14, 4) Not Null Default 0,
		Dia_30 numeric(14, 4) Not Null Default 0,
		Dia_31 numeric(14, 4) Not Null Default 0
	) 
End 
Go--#SQL 


	