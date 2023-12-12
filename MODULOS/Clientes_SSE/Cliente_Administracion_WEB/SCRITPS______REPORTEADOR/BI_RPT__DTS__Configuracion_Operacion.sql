---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'BI_RPT__DTS__Configuracion_Operacion' and xType = 'U' ) 
   Drop Table BI_RPT__DTS__Configuracion_Operacion 
Go--#SQL 

Create Table BI_RPT__DTS__Configuracion_Operacion 
(
	Keyx int identity(1,1), 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(4) Not Null, 
	IdFarmacia_Almacen varchar(4) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null  
	  
)
Go--#SQL    

--Insert Into BI_RPT__DTS__Configuracion_Operacion (  IdEmpresa, IdEstado, IdFarmacia_Almacen, IdCliente, IdSubCliente )  Values ( '001', '28', '0002', '0002', '0011' )   
--Insert Into BI_RPT__DTS__Configuracion_Operacion (  IdEmpresa, IdEstado, IdFarmacia_Almacen, IdCliente, IdSubCliente )  Values ( '001', '11', '0005', '0002', '0006' )   
--Insert Into BI_RPT__DTS__Configuracion_Operacion (  IdEmpresa, IdEstado, IdFarmacia_Almacen, IdCliente, IdSubCliente )  Values ( '001', '13', '0003', '0002', '0010' )   


Insert Into BI_RPT__DTS__Configuracion_Operacion (  IdEmpresa, IdEstado, IdFarmacia_Almacen, IdCliente, IdSubCliente )  Values ( '004', '22', '0104', '0042', '0003' )   


Go--#SQL    



---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'BI_RPT__DTS__FuentesFinanciamiento' and xType = 'U' ) 
   Drop Table BI_RPT__DTS__FuentesFinanciamiento 
Go--#SQL 

Create Table BI_RPT__DTS__FuentesFinanciamiento 
(
	Keyx int identity(1,1), 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(4) Not Null, 
	NombreFuenteFinanciamiento varchar(200) Not Null Default '',   
	Status varchar(1) Not Null Default ''  
)
Go--#SQL   


---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'BI_RPT__DTS__ProcedenciaInventario' and xType = 'U' ) 
   Drop Table BI_RPT__DTS__ProcedenciaInventario 
Go--#SQL 

Create Table BI_RPT__DTS__ProcedenciaInventario 
(
	Keyx int identity(1,1), 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(4) Not Null, 
	NombreProcedencia varchar(200) Not Null Default '',   
	Status varchar(1) Not Null Default ''  
)
Go--#SQL   


--	sp_generainserts BI_RPT__DTS__ProcedenciaInventario , 1 


 Insert Into BI_RPT__DTS__FuentesFinanciamiento (  IdEmpresa, IdEstado, NombreFuenteFinanciamiento, Status )  Values ( '004', '11', 'INSABI', 'A' )   
 Insert Into BI_RPT__DTS__FuentesFinanciamiento (  IdEmpresa, IdEstado, NombreFuenteFinanciamiento, Status )  Values ( '004', '11', 'OTRAS FUENTES', 'A' )   

 Insert Into BI_RPT__DTS__ProcedenciaInventario (  IdEmpresa, IdEstado, NombreProcedencia, Status )  Values ( '004', '11', 'PHARMAJAL', 'A' )   
 Insert Into BI_RPT__DTS__ProcedenciaInventario (  IdEmpresa, IdEstado, NombreProcedencia, Status )  Values ( '004', '11', 'CONSIGNACIÓN', 'A' )   

 Go--#SQL   


