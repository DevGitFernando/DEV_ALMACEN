------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionDocumentos__CargaMasiva'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionDocumentos__CargaMasiva   
Go--#SQL    

Create Table FACT_Remisiones__RelacionDocumentos__CargaMasiva 
( 
	GUID varchar(100) Not Null Default '', 
	ClaveSSA varchar(20) Not Null, 
	ContenidoPaquete int Not Null Default 1, 
	Cantidad_A_Comprobar numeric(14,4) Not Null Default 0, 	
	Cantidad_Distribuida numeric(14,4) Not Null Default 0,  	
	CantidadAgrupada_A_Comprobar numeric(14,4) Not Null Default 0, 	
	CantidadAgrupada_Distribuida numeric(14,4) Not Null Default 0, 
	
	Keyx int identity(1, 1)  	
	
) 
Go--#SQL 

	----sp_listacolumnas FACT_Remisiones__RelacionDocumentos__CargaMasiva 

	----Insert Into FACT_Remisiones__RelacionDocumentos__CargaMasiva ( GUID, ClaveSSA, ContenidoPaquete, Cantidad_A_Comprobar, Cantidad_Distribuida, CantidadAgrupada_A_Comprobar, CantidadAgrupada_Distribuida ) 
	----Select GUID, ClaveSSA, ContenidoPaquete, Cantidad_A_Comprobar, Cantidad_Distribuida, CantidadAgrupada_A_Comprobar, CantidadAgrupada_Distribuida 
	----from FACT_Remisiones__RelacionFacturas__CargaMasiva

