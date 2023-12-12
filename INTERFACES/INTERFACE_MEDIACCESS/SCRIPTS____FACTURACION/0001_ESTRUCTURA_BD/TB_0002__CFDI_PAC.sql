------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_PACs' and xType = 'U' ) 
	Drop Table CFDI_PACs
Go--#SQL  

Create Table CFDI_PACs 
( 
	IdPAC varchar(3) Not Null, 
	NombrePAC varchar(100) Not Null Default '' Unique,  
	UrlProduccion varchar(500) Not Null,  
	UrlPruebas varchar(500) Not Null 	
) 
Go--#SQL  

Alter Table CFDI_PACs Add Constraint PK_CFDI_PACs Primary Key ( IdPAC ) 
Go--#SQL 

Insert Into CFDI_PACs Select '002', 'PAX Facturación', 
	'https://www.paxfacturacion.com.mx:453', 
	'https://test.paxfacturacion.com.mx:453' 
	
Insert Into CFDI_PACs Select '003', 'Folios Digitales', 
	'https://foliosdigitalespac.com', 
	'https://foliosdigitalespac.com' 	 
Go--#SQL 
