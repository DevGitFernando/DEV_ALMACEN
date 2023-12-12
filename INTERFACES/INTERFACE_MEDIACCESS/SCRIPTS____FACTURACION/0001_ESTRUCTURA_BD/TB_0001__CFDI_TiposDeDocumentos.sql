------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_TiposDeDocumentos' and xType = 'U' ) 
	Drop Table CFDI_TiposDeDocumentos  
Go--#SQL 

Create Table CFDI_TiposDeDocumentos 
(
	IdTipoDocumento varchar(3) Not Null, 
	NombreDocumento varchar(100) Not Null Default '' Unique, 
    TipoDeComprobante smallint Not Null Default 0,	
	Alias varchar(10) Not Null Default '', 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_TiposDeDocumentos Add Constraint PK_CFDI_TiposDeDocumentos Primary Key ( IdTipoDocumento ) 
Go--#SQL 

Insert Into CFDI_TiposDeDocumentos values ( '001', 'Factura', 1, 'FAC', 'A' ) 
Insert Into CFDI_TiposDeDocumentos values ( '002', 'Nota de crédito', 2, 'NCR', 'A' ) 
Insert Into CFDI_TiposDeDocumentos values ( '003', 'Nota de débito', 2, 'NDD', 'A' ) 
Insert Into CFDI_TiposDeDocumentos values ( '004', 'Comprabante de ingresos', 1, 'CDI', 'A' ) 
Insert Into CFDI_TiposDeDocumentos values ( '005', 'Carta porte', 3, 'CPO', 'A' ) 
Insert Into CFDI_TiposDeDocumentos values ( '006', 'Nota de cargo', 1, 'NCC', 'A' ) 
Go--#SQL 


------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_RegimenFiscal' and xType = 'U' ) 
	Drop Table CFDI_RegimenFiscal    
Go--#SQL  

Create Table CFDI_RegimenFiscal 
(	
	IdRegimen varchar(2) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table CFDI_RegimenFiscal Add Constraint PK_CFDI_RegimenFiscal Primary	Key ( IdRegimen ) 
Go--#SQL 

Insert Into CFDI_RegimenFiscal Select '01', 'REGIMEN GENERAL DE LEY', 'A' 
Go--#SQL 

