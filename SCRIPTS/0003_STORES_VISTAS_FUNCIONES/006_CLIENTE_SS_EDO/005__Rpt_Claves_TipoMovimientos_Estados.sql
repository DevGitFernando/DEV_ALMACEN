----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Claves_TipoMovimientos_Estados' and xType = 'U' ) 
----	Drop Table Rpt_Claves_TipoMovimientos_Estados 
----Go--#S--QL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_Claves_TipoMovimientos_Estados' and xType = 'U' ) 
Begin 
	Create Table Rpt_Claves_TipoMovimientos_Estados 
	(
		IdEstado varchar(2) Not Null Default '',
		Estado varchar(50) Not Null Default '',
		IdSubFarmacia varchar(2) Not Null Default '',
		SubFarmacia varchar(50) Not Null Default '',  
		IdTipoMovto varchar(6) Not Null Default '', 
		DescripcionTipoMovto varchar(50) Not Null Default '',
		Efecto_Movto varchar(1) Not Null Default '', 
		Año int Not Null Default 0, 
		Mes int Not Null Default 0,	
		NombreMes varchar(50) Not Null Default '', 
		Piezas numeric(14, 4) Not Null Default 0, 
		TotalClaves int Not Null Default 0	
	) 
End 
Go--#SQL 

