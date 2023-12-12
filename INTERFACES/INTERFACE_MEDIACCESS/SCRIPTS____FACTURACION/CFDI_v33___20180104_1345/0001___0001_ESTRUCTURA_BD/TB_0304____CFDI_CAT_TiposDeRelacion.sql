
-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_TiposDeRelacion' and xType = 'U' ) 
	Drop Table CFDI_TiposDeRelacion  
Go--#SQL  

Create Table CFDI_TiposDeRelacion 
(	
	Clave varchar(4) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_TiposDeRelacion Add Constraint PK_CFDI_TiposDeRelacion Primary	Key ( Clave ) 
Go--#SQL 

If Not Exists ( Select * From CFDI_TiposDeRelacion Where Clave = '01' )  Insert Into CFDI_TiposDeRelacion (  Clave, Descripcion, Status )  Values ( '01', 'Nota de cr�dito de los documentos relacionados', 'A' ) 
 Else Update CFDI_TiposDeRelacion Set Descripcion = 'Nota de cr�dito de los documentos relacionados', Status = 'A' Where Clave = '01'  
If Not Exists ( Select * From CFDI_TiposDeRelacion Where Clave = '02' )  Insert Into CFDI_TiposDeRelacion (  Clave, Descripcion, Status )  Values ( '02', 'Nota de d�bito de los documentos relacionados', 'A' ) 
 Else Update CFDI_TiposDeRelacion Set Descripcion = 'Nota de d�bito de los documentos relacionados', Status = 'A' Where Clave = '02'  
If Not Exists ( Select * From CFDI_TiposDeRelacion Where Clave = '03' )  Insert Into CFDI_TiposDeRelacion (  Clave, Descripcion, Status )  Values ( '03', 'Devoluci�n de mercanc�a sobre facturas o traslados previos', 'A' ) 
 Else Update CFDI_TiposDeRelacion Set Descripcion = 'Devoluci�n de mercanc�a sobre facturas o traslados previos', Status = 'A' Where Clave = '03'  
If Not Exists ( Select * From CFDI_TiposDeRelacion Where Clave = '04' )  Insert Into CFDI_TiposDeRelacion (  Clave, Descripcion, Status )  Values ( '04', 'Sustituci�n de los CFDI previos', 'A' ) 
 Else Update CFDI_TiposDeRelacion Set Descripcion = 'Sustituci�n de los CFDI previos', Status = 'A' Where Clave = '04'  
If Not Exists ( Select * From CFDI_TiposDeRelacion Where Clave = '05' )  Insert Into CFDI_TiposDeRelacion (  Clave, Descripcion, Status )  Values ( '05', 'Traslados de mercancias facturados previamente', 'A' ) 
 Else Update CFDI_TiposDeRelacion Set Descripcion = 'Traslados de mercancias facturados previamente', Status = 'A' Where Clave = '05'  
If Not Exists ( Select * From CFDI_TiposDeRelacion Where Clave = '06' )  Insert Into CFDI_TiposDeRelacion (  Clave, Descripcion, Status )  Values ( '06', 'Factura generada por los traslados previos', 'A' ) 
 Else Update CFDI_TiposDeRelacion Set Descripcion = 'Factura generada por los traslados previos', Status = 'A' Where Clave = '06'  
If Not Exists ( Select * From CFDI_TiposDeRelacion Where Clave = '07' )  Insert Into CFDI_TiposDeRelacion (  Clave, Descripcion, Status )  Values ( '07', 'CFDI por aplicaci�n de anticipo', 'A' ) 
 Else Update CFDI_TiposDeRelacion Set Descripcion = 'CFDI por aplicaci�n de anticipo', Status = 'A' Where Clave = '07'  
Go--#SQL 

