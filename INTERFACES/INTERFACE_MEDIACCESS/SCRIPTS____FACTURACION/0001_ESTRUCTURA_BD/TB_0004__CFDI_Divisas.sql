------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Divisas' and xType = 'U' ) 
	Drop Table CFDI_Divisas    
Go--#SQL 

Create Table CFDI_Divisas
( 
	IdDivisa varchar(3) Not Null Default '',  	
	Descripcion varchar(100) Not Null Default '', 
	Codigo varchar(10) Not Null Default '' Unique, 
	TipoCambio numeric(14,4) Not Null Default 1 
) 
Go--#SQL  

Alter Table CFDI_Divisas Add Constraint PK_CFDI_Divisas Primary Key ( IdDivisa )  
Go--#SQL 

Insert Into CFDI_Divisas 
Select '001', 'Pesos mexicanos', 'MXN', 1 
Go--#SQL 

 