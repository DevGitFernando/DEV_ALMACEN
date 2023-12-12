------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_Informacion_Proceso_Facturacion'  and xType = 'U' ) 
   Drop Table FACT_Informacion_Proceso_Facturacion  
Go--#SQL    

Create Table FACT_Informacion_Proceso_Facturacion
( 
	IdEmpresa varchar(3) Not Null Default '',		
	IdEstado varchar(2) Not Null Default '',		
	IdFarmacia varchar(4) Not Null Default '', 
	FolioCierre int Not Null Default 0,  
	HostName varchar(100) Not Null Default Host_name(), 
	Identificador varchar(100) Not Null Default '', --Host_name() + convert(varchar(10), getdate(), 120 ), 
	FechaRegistro datetime Not Null Default getdate(), 
	Keyx int identity(1,1) 
	
) 
Go--#SQL 

/* 
	Insert Into FACT_Informacion_Proceso_Facturacion ( IdEmpresa, IdEstado, IdFarmacia, FolioCierre ) 
	Select  '001', '21', '0034', 12 
	
	select * 
	from FACT_Informacion_Proceso_Facturacion 
*/ 