--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'INT_MA_Ventas_Importes' and xType = 'U' ) 
   Drop Table INT_MA_Ventas_Importes  
Go--#SQL   

Create Table INT_MA_Ventas_Importes
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null, 
	
	SubTotal_SinGravar Numeric(14,4) Not Null Default 0, 
	SubTotal_Gravado  Numeric(14,4) Not Null Default 0, 
	DescuentoCopago Numeric(14,4) Not Null Default 0, 
	
	Importe_SinGravar Numeric(14,4) Not Null Default 0, 	
	Importe_Gravado  Numeric(14,4) Not Null Default 0, 	
	Iva Numeric(14,4) Not Null Default 0, 		
	Importe_Neto Numeric(14,4) Not Null Default 0, 		
	
	Porcentajes_Procesados bit Not Null Default 'false', 	
	Porcentaje_01 Numeric(14,4) Not Null Default 0, 
	Porcentaje_02 Numeric(14,4) Not Null Default 0, 		
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 


Alter Table INT_MA_Ventas_Importes Add Constraint PK_INT_MA_Ventas_Importes Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL

Alter Table INT_MA_Ventas_Importes Add Constraint FK_INT_MA_Ventas_Importes__VentasEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL
 

