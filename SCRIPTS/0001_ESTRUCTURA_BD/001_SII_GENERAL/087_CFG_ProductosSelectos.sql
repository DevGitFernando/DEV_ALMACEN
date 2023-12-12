---------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (nolock) Where Name = 'CFG_ProductosSelectos' and xType = 'U' ) 
Begin 
	Create Table CFG_ProductosSelectos
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,
		IdClaveSSA_Sal varchar(4) Not Null,
		Status varchar(1) Not Null Default 'A',
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table CFG_ProductosSelectos Add Constraint PK_CFG_ProductosSelectos 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA_Sal ) 

	Alter Table CFG_ProductosSelectos Add Constraint FK_CFG_ProductosSelectos_CatEmpresas 
		Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
	  
	Alter Table CFG_ProductosSelectos Add Constraint FK_CFG_ProductosSelectos_CatEstados 
		Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 

	Alter Table CFG_ProductosSelectos Add Constraint FK_CFG_ProductosSelectos_CatFarmacias
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 

	Alter Table CFG_ProductosSelectos Add Constraint FK_CFG_ProductosSelectos_CatClavesSSA_Sales
		Foreign Key ( IdClaveSSA_Sal ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 

End 
Go--#SQL 
