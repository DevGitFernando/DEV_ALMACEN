


----------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFG_ClavesSSA_OperacionMaquila_Chiapas' and xType = 'U' ) 
   Drop Table CFG_ClavesSSA_OperacionMaquila_Chiapas 
Go--#SQL 

Create Table CFG_ClavesSSA_OperacionMaquila_Chiapas 
( 
	IdEmpresa varchar(3) Not Null,	
	IdEstado varchar(2) Not Null, 
	ClaveSSA varchar(50) Not Null Default '',		---- Clave SSA del SII 
	ClaveSSA_ND varchar(20) Not Null Default '', 
	ClaveSSA_Mascara varchar(20) Not Null Default '', 
	ManejaIva smallint Not Null Default 0, 
	PrecioVenta numeric(14,4) Not Null Default 0,
	PrecioServicio numeric(14,4) Not Null Default 0, 
	Descripcion_Mascara varchar(max) Not null Default '',
	UnidadDeMedida varchar(200) Not Null Default '',
	ContenidoPaquete int Not Null Default 0,
	Status varchar(2) Not Null Default 'A',
	Actualizado  tinyint Not Null Default 0
) 	
Go--#SQL 