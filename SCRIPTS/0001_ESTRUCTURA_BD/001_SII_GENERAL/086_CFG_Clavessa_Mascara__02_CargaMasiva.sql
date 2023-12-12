------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFG_ClavesSSA_Mascaras__CargaMasiva' and xType = 'U' ) 
   Drop Table CFG_ClavesSSA_Mascaras__CargaMasiva
Go--#SQL 

Create Table CFG_ClavesSSA_Mascaras__CargaMasiva  
( 
	IdEstado varchar(2) Not Null Default '', 
	IdCliente varchar(4) Not Null Default '',
	IdSubCliente varchar(4) Not Null Default '',		
	IdClaveSSA_Sal varchar(4) Not Null Default '',

	ClaveSSA varchar(50) Not Null Default '', 
	ClaveSSA_Proceso varchar(20) Not Null Default '', 
	ClaveValida bit Not Null Default 'False', 	
	DescripcionClaveSSA varchar(max) Not Null Default '', 		

	Mascara varchar(50) Not Null Default '',  
	Descripcion varchar(max) Not Null Default '',  
	DescripcionCorta varchar(max) Not Null Default '',  
	Presentacion varchar(max) Not Null Default '',  

	Status varchar(1) Not Null Default 'A'
) 
Go--#SQL 


