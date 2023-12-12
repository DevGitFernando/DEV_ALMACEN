------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_FormaDePago' and xType = 'U' ) 
	Drop Table CFDI_FormaDePago     
Go--#SQL  

Create Table CFDI_FormaDePago 
( 
	IdFormaDePago varchar(2) Not Null, 
	Descripcion varchar(100) Not Null Unique,  
	Status varchar(1) Not Null Default 'A' 	
) 
Go--#SQL  

Alter Table CFDI_FormaDePago Add Constraint PK_CFDI_FormaDePago Primary Key ( IdFormaDePago ) 
Go--#SQL  

Insert Into CFDI_FormaDePago Select '00', 'Sin especificar', 'A'  
Insert Into CFDI_FormaDePago Select '01', 'Crédito', 'A'  
Insert Into CFDI_FormaDePago Select '02', 'Contado', 'A'  
Go--#SQL  


------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_MetodoDePago' and xType = 'U' ) 
	Drop Table CFDI_MetodoDePago     
Go--#SQL  

Create Table CFDI_MetodoDePago 
( 
	IdMetodoDePago varchar(2) Not Null, 
	Descripcion varchar(100) Not Null Unique, 
	EsDocumento smallint Not Null Default 0,  
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0  
) 
Go--#SQL  

Alter Table CFDI_MetodoDePago Add Constraint PK_CFDI_MetodoDePago Primary Key ( IdMetodoDePago ) 
Go--#SQL  

If Not Exists ( Select * From CFDI_MetodoDePago Where IdMetodoDePago = '00' )  Insert Into CFDI_MetodoDePago (  IdMetodoDePago, Descripcion, Status, Actualizado, EsDocumento )  Values ( '00', 'No identificado', 'A', 0, 0 )    Else Update CFDI_MetodoDePago Set Descripcion = 'No identificado', Status = 'A', Actualizado = 0, EsDocumento = 0 Where IdMetodoDePago = '00'  
If Not Exists ( Select * From CFDI_MetodoDePago Where IdMetodoDePago = '01' )  Insert Into CFDI_MetodoDePago (  IdMetodoDePago, Descripcion, Status, Actualizado, EsDocumento )  Values ( '01', 'Efectivo', 'A', 0, 0 )    Else Update CFDI_MetodoDePago Set Descripcion = 'Efectivo', Status = 'A', Actualizado = 0, EsDocumento = 0 Where IdMetodoDePago = '01'  
If Not Exists ( Select * From CFDI_MetodoDePago Where IdMetodoDePago = '02' )  Insert Into CFDI_MetodoDePago (  IdMetodoDePago, Descripcion, Status, Actualizado, EsDocumento )  Values ( '02', 'Cheque', 'A', 0, 0 )    Else Update CFDI_MetodoDePago Set Descripcion = 'Cheque', Status = 'A', Actualizado = 0, EsDocumento = 0 Where IdMetodoDePago = '02'  
If Not Exists ( Select * From CFDI_MetodoDePago Where IdMetodoDePago = '03' )  Insert Into CFDI_MetodoDePago (  IdMetodoDePago, Descripcion, Status, Actualizado, EsDocumento )  Values ( '03', 'Transferencia electrónica', 'A', 0, 0 )    Else Update CFDI_MetodoDePago Set Descripcion = 'Transferencia electrónica', Status = 'A', Actualizado = 0, EsDocumento = 0 Where IdMetodoDePago = '03'  
If Not Exists ( Select * From CFDI_MetodoDePago Where IdMetodoDePago = '04' )  Insert Into CFDI_MetodoDePago (  IdMetodoDePago, Descripcion, Status, Actualizado, EsDocumento )  Values ( '04', 'Tarjeta de crédito', 'A', 0, 0 )    Else Update CFDI_MetodoDePago Set Descripcion = 'Tarjeta de crédito', Status = 'A', Actualizado = 0, EsDocumento = 0 Where IdMetodoDePago = '04'  
If Not Exists ( Select * From CFDI_MetodoDePago Where IdMetodoDePago = '05' )  Insert Into CFDI_MetodoDePago (  IdMetodoDePago, Descripcion, Status, Actualizado, EsDocumento )  Values ( '05', 'Tarjeta de debito', 'A', 0, 0 )    Else Update CFDI_MetodoDePago Set Descripcion = 'Tarjeta de debito', Status = 'A', Actualizado = 0, EsDocumento = 0 Where IdMetodoDePago = '05'  
If Not Exists ( Select * From CFDI_MetodoDePago Where IdMetodoDePago = '06' )  Insert Into CFDI_MetodoDePago (  IdMetodoDePago, Descripcion, Status, Actualizado, EsDocumento )  Values ( '06', 'Deposito', 'A', 0, 0 )    Else Update CFDI_MetodoDePago Set Descripcion = 'Deposito', Status = 'A', Actualizado = 0, EsDocumento = 0 Where IdMetodoDePago = '06'  
If Not Exists ( Select * From CFDI_MetodoDePago Where IdMetodoDePago = '07' )  Insert Into CFDI_MetodoDePago (  IdMetodoDePago, Descripcion, Status, Actualizado, EsDocumento )  Values ( '07', 'Documento institucional', 'A', 0, 1 )    Else Update CFDI_MetodoDePago Set Descripcion = 'Documento institucional', Status = 'A', Actualizado = 0, EsDocumento = 1 Where IdMetodoDePago = '07'  
Go--#SQL  
