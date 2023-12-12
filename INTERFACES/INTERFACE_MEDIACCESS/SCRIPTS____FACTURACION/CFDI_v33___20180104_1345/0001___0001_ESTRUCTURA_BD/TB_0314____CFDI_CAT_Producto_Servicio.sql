------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Productos_Servicios' and xType = 'U' ) 
	Drop Table CFDI_Productos_Servicios    
Go--#SQL  

Create Table CFDI_Productos_Servicios 
(	
	Clave varchar(20) Not Null Default '', 
	Descripcion varchar(200) Not Null Default '', 
	Incluir_IVA_Traslado bit Not Null Default 'false', 
	Incluir_IEPS_Traslado bit Not Null Default 'false',  
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_Productos_Servicios Add Constraint PK_CFDI_Productos_Servicios Primary	Key ( Clave  ) 
Go--#SQL 



Go--#SQL 

