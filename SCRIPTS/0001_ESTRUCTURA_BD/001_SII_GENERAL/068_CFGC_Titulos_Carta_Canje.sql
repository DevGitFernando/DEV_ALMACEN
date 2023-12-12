-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_Titulos_CartaCanje' and xType = 'U' ) 
Begin 
	Create Table CFGC_Titulos_CartaCanje  
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
				
		ExpedidoEn varchar(100) Not Null Default '', 		
		AQuienCorresponda varchar(100) Not Null Default '', 
		
		MesesCaducar int Not Null Default 0,
		
		Titulo_01 varchar(500) Not Null Default '', 
		Titulo_02 varchar(500) Not Null Default '', 
		Titulo_03 varchar(500) Not Null Default '', 
		Firma varchar(500) Not Null Default '', 
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table CFGC_Titulos_CartaCanje Add Constraint PK_CFGC_Titulos_CartaCanje Primary Key ( IdEmpresa, IdEstado, IdFarmacia ) 

End 
Go--#SQL



