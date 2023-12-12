----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_Claves_CargaMasiva' and xType = 'U' ) 
   Drop Table INT_ND_Claves_CargaMasiva 
Go--#SQL 

Create Table INT_ND_Claves_CargaMasiva 
( 
	Keyx int Identity(1, 1) , 
	ClaveSSA_ND varchar(20) Not Null Default '', 
	Descripcion varchar(max) Not Null Default '', 
	TipoInsumo int Not null Default 0, 
	TipoInsumoDescripcion varchar(20) Not Null Default '', 	
	ClaveSSA_ND_Auxiliar varchar(20) Not Null Default '', 
	ClaveSSA_ND_Mascara varchar(20) Not Null Default '', 			
	EsClaveSSA_Valida int Not Null Default 0 
) 	
Go--#SQL 


