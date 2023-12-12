------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_TiposDeImpuestos' and xType = 'U' ) 
	Drop Table CFDI_TiposDeImpuestos    
Go--#SQL  

Create Table CFDI_TiposDeImpuestos 
(	
	Clave varchar(4) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '', 
	EsTraslado bit Not Null Default 'false', 
	EsRetencion bit Not Null Default 'false', 
	TipoImpuesto varchar(20) Not Null Default '', 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_TiposDeImpuestos Add Constraint PK_CFDI_TiposDeImpuestos Primary	Key ( Clave  ) 
Go--#SQL 


If Not Exists ( Select * From CFDI_TiposDeImpuestos Where Clave = '001' )  Insert Into CFDI_TiposDeImpuestos (  Clave, Descripcion, EsTraslado, EsRetencion, TipoImpuesto, Status )  Values ( '001', 'ISR', 1, 0, 'Federal', 'A' ) 
 Else Update CFDI_TiposDeImpuestos Set Descripcion = 'ISR', EsTraslado = 1, EsRetencion = 0, TipoImpuesto = 'Federal', Status = 'A' Where Clave = '001'  
If Not Exists ( Select * From CFDI_TiposDeImpuestos Where Clave = '002' )  Insert Into CFDI_TiposDeImpuestos (  Clave, Descripcion, EsTraslado, EsRetencion, TipoImpuesto, Status )  Values ( '002', 'IVA', 1, 1, 'Federal', 'A' ) 
 Else Update CFDI_TiposDeImpuestos Set Descripcion = 'IVA', EsTraslado = 1, EsRetencion = 1, TipoImpuesto = 'Federal', Status = 'A' Where Clave = '002'  
If Not Exists ( Select * From CFDI_TiposDeImpuestos Where Clave = '003' )  Insert Into CFDI_TiposDeImpuestos (  Clave, Descripcion, EsTraslado, EsRetencion, TipoImpuesto, Status )  Values ( '003', 'IEPS', 1, 1, 'Federal', 'A' ) 
 Else Update CFDI_TiposDeImpuestos Set Descripcion = 'IEPS', EsTraslado = 1, EsRetencion = 1, TipoImpuesto = 'Federal', Status = 'A' Where Clave = '003'  
Go--#SQL 

