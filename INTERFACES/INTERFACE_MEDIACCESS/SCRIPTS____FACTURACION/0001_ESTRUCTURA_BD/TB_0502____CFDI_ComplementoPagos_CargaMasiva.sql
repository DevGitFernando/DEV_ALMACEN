-----------------------------------------------------------------------------------------------------------------------------------------   
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'CFDI_ComplementoDePagos__CargaMasiva' and xType = 'U' ) 
	Drop Table CFDI_ComplementoDePagos__CargaMasiva   
Go--#SQL 

Create Table CFDI_ComplementoDePagos__CargaMasiva   
( 
	IdEmpresa varchar(3) Not Null Default '', 
	IdEstado varchar(2) Not Null Default '', 
	IdFarmacia varchar(4) Not Null Default '', 
	
	RFC varchar(20) Not Null Default '', 
	RFC_Factura varchar(200) Not Null Default '', 

	Serie varchar(10) Not Null Default '', 
	Folio int Not Null Default 0, 	
	UUID varchar(50) Not Null Default '', 
	Importe_Pagado numeric(14, 4) Not Null Default 0,  
	
	ExisteFactura bit Not Null Default 'false', 
	EsFacturaDeRFC bit Not Null Default 'false', 


	NumParcialidad int Not Null Default 0, 
	ValorNominal numeric(14, 4) Not Null Default 0, 
	Importe_Abonos numeric(14, 4) Not Null Default 0, 

	Importe_SaldoAnterior numeric(14, 4) Not Null Default 0, 
	Importe_SaldoInsoluto numeric(14, 4) Not Null Default 0 

) 
Go--#SQL 

