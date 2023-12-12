---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name From sysobjects So (NoLock) Where So.Name = 'CatProductos_CFDI' ) 
Begin 	
	Create Table CatProductos_CFDI 
	(
		IdProducto varchar(8) Not Null, 
		SAT_ClaveProducto_Servicio varchar(20)  Not Null Default '01010101', 
		SAT_UnidadDeMedida varchar(5)  Not Null Default 'H87' 
	) 

	Alter Table CatProductos_CFDI Add Constraint PK_CatProductos_CFDI Primary Key ( IdProducto ) 

	Alter Table CatProductos_CFDI Add Constraint FK_CatProductos_CFDI___CatProductos 
		Foreign Key ( IdProducto ) References CatProductos ( IdProducto )

End 
Go--#SQL  	

---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select So.Name From sysobjects So (NoLock) Where So.Name = 'CatProductos_CFDI_Load' ) 
   Drop Table CatProductos_CFDI_Load 
Go--#SQL 

Create Table CatProductos_CFDI_Load 
(
	IdProducto varchar(20) Not Null, 
	SAT_ClaveProducto_Servicio varchar(20)  Not Null Default '01010101', 
	SAT_UnidadDeMedida varchar(5)  Not Null Default 'H87' 
) 
Go--#SQL  	


----	Insert Into CatProductos_CFDI_Load 
----	select * 
----	from CargaCatalogo_CFDI 
----	where IdProducto is not  null 


----sp_formateartabla CargaCatalogo_CFDI


/* 
	Insert Into CatProductos_CFDI ( IdProducto ) 
	select IdProducto 
	from CatProductos 


	select * 
	from CatProductos_CFDI 
*/ 

