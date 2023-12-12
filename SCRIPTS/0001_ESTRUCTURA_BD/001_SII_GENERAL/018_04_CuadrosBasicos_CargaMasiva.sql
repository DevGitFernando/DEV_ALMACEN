If Exists ( Select * From sysobjects (NoLock) Where Name = 'CFG_CB_CuadroBasico_Claves__CargaMasiva' and xType = 'U' ) 
   Drop Table CFG_CB_CuadroBasico_Claves__CargaMasiva
Go--#SQL 

Create Table CFG_CB_CuadroBasico_Claves__CargaMasiva  
( 
	IdEstado varchar(2) Not Null Default '', 
	IdCliente varchar(4) Not Null Default '',
	IdNivel int Not Null Default 0, 

	IdClaveSSA_Sal varchar(4) Not Null Default '', 
	ClaveSSA varchar(50) Not Null Default '', 
	ClaveSSA_Proceso varchar(20) Not Null Default '', 
	ClaveValida bit Not Null Default 'False', 	
	DescripcionClaveSSA varchar(max) Not Null Default '', 		

	EmiteVale int Not Null Default 0, 
	Procesado int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 


