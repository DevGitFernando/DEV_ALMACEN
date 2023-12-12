----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_Claves' and xType = 'U' ) 
   Drop Table INT_ND_Claves 
Go--#SQL 

Create Table INT_ND_Claves 
( 
	Keyx int Identity(1, 1), 
	Keyx_Auxiliar int Not Null, 		
	ClaveSSA_ND varchar(20) Not Null Default '', 
	Descripcion varchar(max) Not Null Default '', 
	TipoInsumo int Not null Default 0, 
	TipoInsumoDescripcion varchar(20) Not Null Default '', 	
	ClaveSSA_ND_Auxiliar varchar(20) Not Null Default '', 
	ClaveSSA_ND_Mascara varchar(20) Not Null Default '', 			
	EsClaveSSA_Valida int Not Null Default 0 
) 	
Go--#SQL 


----Alter Table INT_ND_Claves Add Constraint PK_INT_ND_Claves Primary Key ( Keyx ) 
----Go--#SQL 

--		alter table INT_ND_Claves drop Constraint PK_INT_ND_Claves
