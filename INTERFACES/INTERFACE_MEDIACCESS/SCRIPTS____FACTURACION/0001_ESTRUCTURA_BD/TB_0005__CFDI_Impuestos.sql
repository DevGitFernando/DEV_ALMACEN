------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFDI_Impuestos' and xType = 'U' ) 
	Drop Table CFDI_Impuestos     
Go--#SQL 

Create Table CFDI_Impuestos 
( 
	Keyx int Identity(1, 1), 
	Fecha datetime Not Null Default getdate(), 
	IVA numeric(14, 2) Not Null Default 16, 
	RetencionIVA numeric(14, 2) Not Null Default 10.67, 
	RetencionISR numeric(14, 2) Not Null Default 10.0, 		
	IEPS numeric(14, 2) Not Null Default 0, 
	ISH numeric(14, 2) Not Null Default 2  
) 
Go--#SQL  
 
-- Insert into CFDI_Impuestos ( Fecha ) Select getdate() 
Go--#xSQL  


--	sp_listacolumnas CFDI_Impuestos


--	select * from CFDI_Impuestos 

