If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Claves' and xType = 'U' ) 
   Drop Table ALMJ_Concentrado_PedidosRC_Claves  
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'U' ) 
   Drop Table ALMJ_Concentrado_PedidosRC_Pedidos  
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC_Det' and xType = 'U' ) 
   Drop Table ALMJ_Pedidos_RC_Det 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC' and xType = 'U' ) 
   Drop Table ALMJ_Pedidos_RC 
Go--#SQL   

------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatCentrosDeSalud' and xType = 'U' ) 
   Drop Table CatCentrosDeSalud 
Go--#SQL  

Create Table CatCentrosDeSalud 
(
	IdEstado varchar(2) Not Null, 
	IdCentro varchar(4) Not Null, 
	IdMunicipio varchar(4) Not Null, 
	IdJurisdiccion varchar(3) Not Null, 	
	Descripcion varchar(100) Not Null, 
	Status varchar(1) Not Null Default 'A',	
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table CatCentrosDeSalud Add Constraint PK_CatCentrosDeSalud Primary Key ( IdEstado, IdCentro ) 
Go--#SQL  

Alter Table CatCentrosDeSalud Add Constraint FK_CatCentrosDeSalud_CatJurisdicciones 
	Foreign Key ( IdEstado, IdJurisdiccion ) References CatJurisdicciones ( IdEstado, IdJurisdiccion ) 
Go--#SQL  	

Alter Table CatCentrosDeSalud Add Constraint FK_CatCentrosDeSalud_CatMunicipios 
	Foreign Key ( IdEstado, IdMunicipio ) References CatMunicipios ( IdEstado, IdMunicipio ) 
Go--#SQL  	


-------------------------------------------------   
-- ALMJ ==> Almacen Jurisdiccional 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC' and xType = 'U' ) 
   Drop Table ALMJ_Pedidos_RC 
Go--#SQL   

Create Table ALMJ_Pedidos_RC 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdJurisdiccion varchar(3) Not Null, 		
	IdFarmacia varchar(4) Not Null, 
	FolioPedidoRC varchar(6) Not Null,  
	IdCentro varchar(4) Not Null, 
	Entrego varchar(100) Not Null Default '', --- Texto libre 
	FechaSistema datetime Not Null Default getdate(), 
	FechaCaptura datetime Not Null Default getdate(), 
	IdPersonal varchar(4) Not Null, 
	
	StatusPedido tinyint Not Null Default 0, 
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 1  ---- El default 1 es conciente, este campo se actualiza en un proceso posterior a la captura de la informacion 
)
Go--#SQL  

Alter Table ALMJ_Pedidos_RC Add Constraint PK_ALMJ_Pedidos_RC Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedidoRC ) 
Go--#SQL  

Alter Table ALMJ_Pedidos_RC Add Constraint FK_ALMJ_Pedidos_RC_CatEmpresas  
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa )  
Go--#SQL  	

Alter Table ALMJ_Pedidos_RC Add Constraint FK_ALMJ_Pedidos_RC_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table ALMJ_Pedidos_RC Add Constraint FK_ALMJ_Pedidos_RC_CatJurisdicciones 
	Foreign Key ( IdEstado, IdJurisdiccion ) References CatJurisdicciones ( IdEstado, IdJurisdiccion ) 
Go--#SQL  

Alter Table ALMJ_Pedidos_RC Add Constraint FK_ALMJ_Pedidos_RC_CatCentrosDeSalud 
	Foreign Key ( IdEstado, IdCentro ) References CatCentrosDeSalud ( IdEstado, IdCentro ) 
Go--#SQL  

Alter Table ALMJ_Pedidos_RC Add Constraint FK_ALMJ_Pedidos_RC_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

-------------------------------------------------  
-- ALMJ ==> Almacen Jurisdiccional 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Pedidos_RC_Det' and xType = 'U' ) 
   Drop Table ALMJ_Pedidos_RC_Det 
Go--#SQL   

Create Table ALMJ_Pedidos_RC_Det 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdJurisdiccion varchar(3) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioPedidoRC varchar(6) Not Null,  
	IdClaveSSA varchar(4) Not Null, 
	Cantidad int Not Null Default 0, 
	Existencia numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 1  ---- El default 1 es conciente, este campo se actualiza en un proceso posterior a la captura de la informacion  
) 
Go--#SQL  

Alter Table ALMJ_Pedidos_RC_Det Add Constraint PK_ALMJ_Pedidos_RC_Det Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedidoRC, IdClaveSSA ) 
Go--#SQL  

Alter Table ALMJ_Pedidos_RC_Det Add Constraint FK_ALMJ_Pedidos_RC_Det_ALMJ_Pedidos_RC 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioPedidoRC ) References ALMJ_Pedidos_RC ( IdEmpresa, IdEstado, IdFarmacia, FolioPedidoRC ) 
Go--#SQL  

Alter Table ALMJ_Pedidos_RC_Det Add Constraint FK_ALMJ_Pedidos_RC_Det_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL  
-- select top 10 * from CatClavesSSA_Sales 


--------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFGC_AlmacenesJurisdiccionales' and xType = 'U' ) 
   Drop Table CFGC_AlmacenesJurisdiccionales  
Go--#SQL   

Create Table CFGC_AlmacenesJurisdiccionales 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdEmpresaCSGN varchar(3) Not Null, 
	IdEstadoCSGN varchar(2) Not Null, 
	IdFarmaciaCSGN varchar(4) Not Null	
)
Go--#SQL  

Alter Table CFGC_AlmacenesJurisdiccionales Add Constraint PK_CFGC_AlmacenesJurisdiccionales Primary Key 
	( IdEmpresa, IdEstado, IdFarmacia, IdEmpresaCSGN, IdEstadoCSGN, IdFarmaciaCSGN ) 
Go--#SQL  

Alter Table CFGC_AlmacenesJurisdiccionales Add Constraint FK_CFGC_AlmacenesJurisdiccionales_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 	

Alter Table CFGC_AlmacenesJurisdiccionales Add Constraint FK_CFGC_AlmacenesJurisdiccionalesCSGN_CatEmpresas 
	Foreign Key ( IdEmpresaCSGN ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 	

Alter Table CFGC_AlmacenesJurisdiccionales Add Constraint FK_CFGC_AlmacenesJurisdiccionales_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

Alter Table CFGC_AlmacenesJurisdiccionales Add Constraint FK_CFGC_AlmacenesJurisdiccionalesCSGN_CatFarmacias 
	Foreign Key ( IdEstadoCSGN, IdFarmaciaCSGN ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

-- Insert Into CFGC_AlmacenesJurisdiccionales values ( '001', '25', '0001', '001', '25', '0001' )
Go--#SQL  


--------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC' and xType = 'U' ) 
   Drop Table ALMJ_Concentrado_PedidosRC 
Go--#SQL   

Create Table ALMJ_Concentrado_PedidosRC 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioConcentrado varchar(6) Not Null,  
	
	FechaSistema datetime Not Null Default getdate(), 
	IdPersonal varchar(4) Not Null, 
		
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL  

Alter Table ALMJ_Concentrado_PedidosRC Add Constraint PK_ALMJ_Concentrado_PedidosRC 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado ) 
Go--#SQL  


------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Pedidos' and xType = 'U' ) 
   Drop Table ALMJ_Concentrado_PedidosRC_Pedidos  
Go--#SQL  

Create Table ALMJ_Concentrado_PedidosRC_Pedidos 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioConcentrado varchar(6) Not Null,  
	
	IdEmpresaPed varchar(3) Not Null, 
	IdEstadoPed varchar(2) Not Null, 
	IdFarmaciaPed varchar(4) Not Null, 	
	FolioPedidoRCPed varchar(6) Not Null, 
		
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL  

Alter Table ALMJ_Concentrado_PedidosRC_Pedidos Add Constraint 
	PK_ALMJ_Concentrado_PedidosRC_Pedidos 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado, IdEmpresaPed, IdEstadoPed, IdFarmaciaPed, FolioPedidoRCPed ) 
Go--#SQL  

Alter Table ALMJ_Concentrado_PedidosRC_Pedidos Add Constraint 
	FK_ALMJ_Concentrado_PedidosRC_Pedidos_ALMJ_Concentrado_PedidosRC 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado ) 
	References ALMJ_Concentrado_PedidosRC ( IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado ) 
Go--#SQL 	

------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'ALMJ_Concentrado_PedidosRC_Claves' and xType = 'U' ) 
   Drop Table ALMJ_Concentrado_PedidosRC_Claves  
Go--#SQL  

Create Table ALMJ_Concentrado_PedidosRC_Claves 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioConcentrado varchar(6) Not Null,  
	
	IdClaveSSA varchar(4) Not Null, 
	CantidadPedidoRC int Not Null Default 0, 	
---	Existencia numeric(14,4) Not Null Default 0, 	
	ExistenciaCSGN numeric(14,4) Not Null Default 0, 
	ExistenciaVTA numeric(14,4) Not Null Default 0, 
	CantidadSurtir int Not Null Default 0, 	
	CantidadPedido int Not Null Default 0, 		
				
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL  

Alter Table ALMJ_Concentrado_PedidosRC_Claves Add Constraint 
	PK_ALMJ_Concentrado_PedidosRC_Claves 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado, IdClaveSSA ) 
Go--#SQL  

Alter Table ALMJ_Concentrado_PedidosRC_Claves Add Constraint 
	FK_ALMJ_Concentrado_PedidosRC_Claves_ALMJ_Concentrado_PedidosRC 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado ) 
	References ALMJ_Concentrado_PedidosRC ( IdEmpresa, IdEstado, IdFarmacia, FolioConcentrado ) 
Go--#SQL 
