---------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_ClaveSSA_Mascara' and xType = 'U' ) 
Begin 
	Create Table CFG_ClaveSSA_Mascara
	( 
		IdEstado varchar(2) Not Null,
		IdCliente varchar(4) Not Null,
		IdSubCliente varchar(4) Not Null, 
		IdClaveSSA varchar(4) Not Null,
		Mascara varchar(50) Not Null,
		Descripcion  varchar(5000) Default '', 
		DescripcionCorta  varchar(200) Default '', 
		Presentacion varchar(100) Not Null Default '',  	
		Status varchar(1) Not Null Default 'A', 
		Actualizado smallint Not Null Default 0 
	) 

	Alter Table CFG_clavessa_Mascara Add Constraint PK_CFG_clavessa_Mascara 
		Primary Key ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA ) 

	Alter Table CFG_clavessa_Mascara Add Constraint FK_CFG_clavessa_Mascara_CatEstados 
		Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 

	Alter Table CFG_clavessa_Mascara Add Constraint FK_CFG_clavessa_Mascara_CatSubClientes 
		Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente ) 

	Alter Table CFG_clavessa_Mascara Add Constraint FK_CFG_clavessa_Mascara_CatClavesSSA_Sales  
		Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 

End 	
Go--#SQL  

