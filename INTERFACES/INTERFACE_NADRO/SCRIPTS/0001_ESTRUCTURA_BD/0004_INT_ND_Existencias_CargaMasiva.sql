----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_Existencias_CargaMasiva' and xType = 'U' ) 
   Drop Table INT_ND_Existencias_CargaMasiva 
Go--#SQL 

Create Table INT_ND_Existencias_CargaMasiva 
( 
	Keyx int Identity(1, 1) , 
	ClaveSSA_ND varchar(20) Not Null, 
	CodigoEAN_ND varchar(20)  Not Null,
	Cantidad int Not Null Default 0, 
	ClaveSSA varchar(20) Not Null Default '', 
	IdProducto varchar(8) Not Null Default '', 
	CodigoEAN varchar(20) Not Null Default '', 
	EsClaveSSA_Valida int Not Null Default 0,  
	CodigoEAN_Existe int Not Null Default 0   
) 	
Go--#SQL 



