If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_ProductosLotes_Excluir_Dispensacion' and xType = 'U' ) 
Begin 
	Create Table CFG_ProductosLotes_Excluir_Dispensacion 
	(
		IdEmpresa varchar(3) Not Null Default '', 	
		IdEstado varchar(2) Not Null Default '', 
		IdFarmacia varchar(4) Not Null Default '', 	
		IdSubFarmacia varchar(2) Not Null Default '', 
		IdProducto varchar(8) Not Null Default '', 
		CodigoEAN varchar(30) Not Null Default '', 	
		ClaveLote varchar(30) Not Null Default '', 		
		IdPasillo int Not Null Default 0, 
		IdEstante int Not Null Default 0, 
		IdEntrepaño int Not Null Default 0, 
		
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0  
	) 

	Alter Table CFG_ProductosLotes_Excluir_Dispensacion Add Constraint PK_CFG_ProductosLotes_Excluir_Dispensacion 
		Primary Key 
		( 
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote  -- , IdPasillo, IdEstante, IdEntrepaño 
		) 

End 
Go--#SQL  
