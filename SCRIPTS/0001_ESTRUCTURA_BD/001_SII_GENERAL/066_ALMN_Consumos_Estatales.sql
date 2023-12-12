-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMN_Consumos_Estatales' and xType = 'U' ) 
Begin 
	Create Table ALMN_Consumos_Estatales
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdClaveSSA varchar(4) Not Null,
		
		--Año int Not Null,
		--Mes int Not Null,
		
		ContenidoPaquete int Not Null,
		
		Piezas_Mes numeric(14, 4) Not Null Default 0,
		Piezas_Semana Numeric(14, 4) Not Null Default 0, 
		Piezas_Dia int Not Null Default 0,
		
		Cajas_Mes numeric(14, 4) Not Null Default 0,
		Cajas_Semana Numeric(14, 4) Not Null Default 0,
		Cajas_Dia int Not Null Default 0,
		
		Piezas_StockSegurida Numeric(14, 4) Not Null Default 0,
		Cajas_StockSegurida Numeric(14, 4) Not Null Default 0,
		
		Confirmado bit Not Null Default 0,
		
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table ALMN_Consumos_Estatales Add Constraint PK_ALMN_Consumos_Estatales 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA ) 

End 
Go--#SQL 

