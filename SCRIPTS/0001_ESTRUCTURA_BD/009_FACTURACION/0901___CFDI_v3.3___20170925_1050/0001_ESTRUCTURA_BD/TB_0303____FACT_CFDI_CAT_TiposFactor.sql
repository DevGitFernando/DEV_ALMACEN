
-------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TiposFactor' and xType = 'U' ) 
	Drop Table FACT_CFDI_TiposFactor  
Go--#SQL  

Create Table FACT_CFDI_TiposFactor 
(	
	Clave varchar(20) Not Null Default '', 
	Descripcion varchar(100) Not Null Default '' Unique, 
	Status varchar(1) Not Null Default 'A' 
) 
Go--#SQL 

Alter Table FACT_CFDI_TiposFactor Add Constraint PK_FACT_CFDI_TiposFactor Primary	Key ( Clave ) 
Go--#SQL 


If Not Exists ( Select * From FACT_CFDI_TiposFactor Where Clave = 'Cuota' )  Insert Into FACT_CFDI_TiposFactor (  Clave, Descripcion, Status )  Values ( 'Cuota', 'Cuota', 'A' ) 
 Else Update FACT_CFDI_TiposFactor Set Descripcion = 'Cuota', Status = 'A' Where Clave = 'Cuota'  
If Not Exists ( Select * From FACT_CFDI_TiposFactor Where Clave = 'Exento' )  Insert Into FACT_CFDI_TiposFactor (  Clave, Descripcion, Status )  Values ( 'Exento', 'Exento', 'A' ) 
 Else Update FACT_CFDI_TiposFactor Set Descripcion = 'Exento', Status = 'A' Where Clave = 'Exento'  
If Not Exists ( Select * From FACT_CFDI_TiposFactor Where Clave = 'Tasa' )  Insert Into FACT_CFDI_TiposFactor (  Clave, Descripcion, Status )  Values ( 'Tasa', 'Tasa', 'A' ) 
 Else Update FACT_CFDI_TiposFactor Set Descripcion = 'Tasa', Status = 'A' Where Clave = 'Tasa'  
Go--#SQL 

