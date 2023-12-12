------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Remisiones__RelacionFacturas__CargaMasiva'  and xType = 'U' ) 
   Drop Table FACT_Remisiones__RelacionFacturas__CargaMasiva   
Go--#SQL    

Create Table FACT_Remisiones__RelacionFacturas__CargaMasiva
( 
	IdEmpresa varchar(3) Not Null,		
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 	
	
	IdFuenteFinanciamiento varchar(4) Not Null Default '', 
	IdFinanciamiento varchar(4) Not Null Default '', 

	FechaRegistro datetime Not Null Default getdate(), 

	ClaveSSA varchar(20) Not Null, 
	ContenidoPaquete int Not Null Default 1, 
	Cantidad_Facturada numeric(14,4) Not Null Default 0, 	
	Cantidad_Distribuida numeric(14,4) Not Null Default 0,  	
	CantidadAgrupada_Facturada numeric(14,4) Not Null Default 0, 	
	CantidadAgrupada_Distribuida numeric(14,4) Not Null Default 0, 
	
	Keyx int identity(1, 1)  	
	
) 
Go--#SQL 


	delete from FACT_Remisiones__RelacionFacturas 

	Insert Into FACT_Remisiones__RelacionFacturas 
	( 
		IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, IdFuenteFinanciamiento, IdFinanciamiento, FechaRegistro, ClaveSSA, ContenidoPaquete, 
		Cantidad_Facturada, Cantidad_Distribuida, CantidadAgrupada_Facturada, CantidadAgrupada_Distribuida 
	) 
	select 
		IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, IdFuenteFinanciamiento, IdFinanciamiento, FechaRegistro, ClaveSSA, ContenidoPaquete, 
		sum(Cantidad_Facturada) as Cantidad_Facturada, sum(Cantidad_Distribuida) as Cantidad_Distribuida, sum(CantidadAgrupada_Facturada) as CantidadAgrupada_Facturada, sum(CantidadAgrupada_Distribuida) as CantidadAgrupada_Distribuida 	 
	from FACT_Remisiones__RelacionFacturas__CargaMasiva 
	group by 
		IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, IdFuenteFinanciamiento, IdFinanciamiento, FechaRegistro, ClaveSSA, ContenidoPaquete


