----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva' and xType = 'U' ) 
   Drop Table INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva 
Go--#SQL 

Create Table INT_ND_CFG_CB_CuadrosBasicos_CargaMasiva 
( 
	Keyx int Identity(1, 1), 	
	IdEstado varchar(2) Not Null, 
	Contrato varchar(100) Not Null Default '', 
	Prioridad int Not Null Default 0, 
	NombrePrograma varchar(200) Not Null Default '', 
	IdAnexo varchar(50) Not Null Default '', 
	NombreAnexo varchar(500) Not Null Default '', 
	ClaveSSA varchar(50) Not Null Default '',		---- Clave SSA del SII 
	ClaveSSA_ND varchar(20) Not Null Default '', 
	ClaveSSA_Mascara varchar(20) Not Null Default '', 
	ManejaIva smallint Not Null Default 0, 
	PrecioVenta numeric(14,4) Not Null Default 0,
	PrecioServicio numeric(14,4) Not Null Default 0, 
	Descripcion_Mascara varchar(max) Not null Default '',  
	Lote varchar(20) Not null Default '', 
	ContenidoPaquete int Not Null Default 0, 
	UnidadDeMedida varchar(500) Not Null Default '',  
	Vigencia datetime Not Null Default getdate(), 
	
	EsClaveSSA_Valida int Not Null Default 0 	
) 	
Go--#SQL 




