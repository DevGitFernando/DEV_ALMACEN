
-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_TiposDeComprobantes' and xType = 'U' ) 
	Drop Table CFDI_TiposDeComprobantes    
Go--#SQL  

Create Table CFDI_TiposDeComprobantes 
(	
	Clave varchar(4) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_TiposDeComprobantes Add Constraint PK_CFDI_TiposDeComprobantes Primary	Key ( Clave ) 
Go--#SQL 

If Not Exists ( Select * From CFDI_TiposDeComprobantes Where Clave = 'E' )  Insert Into CFDI_TiposDeComprobantes (  Clave, Descripcion, Status )  Values ( 'E', 'Egreso', 'A' ) 
 Else Update CFDI_TiposDeComprobantes Set Descripcion = 'Egreso', Status = 'A' Where Clave = 'E'  
If Not Exists ( Select * From CFDI_TiposDeComprobantes Where Clave = 'I' )  Insert Into CFDI_TiposDeComprobantes (  Clave, Descripcion, Status )  Values ( 'I', 'Ingreso', 'A' ) 
 Else Update CFDI_TiposDeComprobantes Set Descripcion = 'Ingreso', Status = 'A' Where Clave = 'I'  
If Not Exists ( Select * From CFDI_TiposDeComprobantes Where Clave = 'N' )  Insert Into CFDI_TiposDeComprobantes (  Clave, Descripcion, Status )  Values ( 'N', 'Nómina', 'A' ) 
 Else Update CFDI_TiposDeComprobantes Set Descripcion = 'Nómina', Status = 'A' Where Clave = 'N'  
If Not Exists ( Select * From CFDI_TiposDeComprobantes Where Clave = 'P' )  Insert Into CFDI_TiposDeComprobantes (  Clave, Descripcion, Status )  Values ( 'P', 'Pago', 'A' ) 
 Else Update CFDI_TiposDeComprobantes Set Descripcion = 'Pago', Status = 'A' Where Clave = 'P'  
If Not Exists ( Select * From CFDI_TiposDeComprobantes Where Clave = 'T' )  Insert Into CFDI_TiposDeComprobantes (  Clave, Descripcion, Status )  Values ( 'T', 'Traslado', 'A' ) 
 Else Update CFDI_TiposDeComprobantes Set Descripcion = 'Traslado', Status = 'A' Where Clave = 'T'  
Go--#SQL 

