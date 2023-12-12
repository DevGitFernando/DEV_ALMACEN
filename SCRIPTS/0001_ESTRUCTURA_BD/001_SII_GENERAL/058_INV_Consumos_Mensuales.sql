
------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) where Name = 'INV_Consumos_Mensuales' and xType = 'U' ) 
	Drop Table INV_Consumos_Mensuales 
Go--#SQL 

Create Table INV_Consumos_Mensuales 
( 
	Año int Not Null Default 0,
	Mes int Not Null Default 0,
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 	
	IdJurisdiccion varchar(3) Not Null, 
		
	ClaveSSA varchar(20) Not Null, 
	Consumo int Not Null Default 0, 
	ConsumoMensual int Not Null Default 0,     
	StockSugerido int Not Null Default 0,  
	StockAsignado int Not Null Default 0 	
) 
Go--#SQL 




