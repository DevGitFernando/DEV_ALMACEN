If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_Claves_Status_Historial' and xType = 'U' ) 
   Drop Table COM_OCEN_OrdenesCompra_Claves_Status_Historial  
Go--#SQL

If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_Claves_Status' and xType = 'U' ) 
   Drop Table COM_OCEN_OrdenesCompra_Claves_Status  
Go--#SQL
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'Cat_StatusDeOrdenesDeCompras' and xType = 'U' ) 
   Drop Table Cat_StatusDeOrdenesDeCompras  
Go--#SQL 

Create Table Cat_StatusDeOrdenesDeCompras
( 
	IdStatus Varchar(2) Not Null,
	Nombre varchar(100) Not Null,
	--Descripcion Varchar(200) Not Null,
	PermiteDescarga Bit Not Null Default 0,
	ModificaCompras Bit Not Null Default 1,
	FechaRegistro datetime Not Null Default getdate(),
	Status varchar(2) Not Null Default 'A',
	Actualizado tinyint Not Null Default 0
) 
Go--#SQL

Alter Table Cat_StatusDeOrdenesDeCompras Add Constraint PK_Cat_StatusDeOrdenesDeCompras
	Primary Key ( IdStatus ) 
Go--#SQL

Set DateFormat YMD
Go--#SQL
If Not Exists ( Select * From Cat_StatusDeOrdenesDeCompras Where IdStatus = '01' )  Insert Into Cat_StatusDeOrdenesDeCompras (  IdStatus, Nombre, PermiteDescarga, ModificaCompras, FechaRegistro, Status, Actualizado )  Values ( '01', 'ORDEN COLOCADA', 0, 1, '2015-05-15 07:41:29', 'A', 0 )    Else Update Cat_StatusDeOrdenesDeCompras Set Nombre = 'ORDEN COLOCADA', PermiteDescarga = 0, ModificaCompras = 1, FechaRegistro = '2015-05-15 07:41:29', Status = 'A', Actualizado = 0 Where IdStatus = '01'  
If Not Exists ( Select * From Cat_StatusDeOrdenesDeCompras Where IdStatus = '02' )  Insert Into Cat_StatusDeOrdenesDeCompras (  IdStatus, Nombre, PermiteDescarga, ModificaCompras, FechaRegistro, Status, Actualizado )  Values ( '02', 'LIBERADA', 0, 1, '2015-05-15 07:42:19', 'A', 0 )    Else Update Cat_StatusDeOrdenesDeCompras Set Nombre = 'LIBERADA', PermiteDescarga = 0, ModificaCompras = 1, FechaRegistro = '2015-05-15 07:42:19', Status = 'A', Actualizado = 0 Where IdStatus = '02'  
If Not Exists ( Select * From Cat_StatusDeOrdenesDeCompras Where IdStatus = '03' )  Insert Into Cat_StatusDeOrdenesDeCompras (  IdStatus, Nombre, PermiteDescarga, ModificaCompras, FechaRegistro, Status, Actualizado )  Values ( '03', 'TRANSITO', 1, 1, '2015-05-15 07:43:37', 'A', 0 )    Else Update Cat_StatusDeOrdenesDeCompras Set Nombre = 'TRANSITO', PermiteDescarga = 1, ModificaCompras = 1, FechaRegistro = '2015-05-15 07:43:37', Status = 'A', Actualizado = 0 Where IdStatus = '03'  
If Not Exists ( Select * From Cat_StatusDeOrdenesDeCompras Where IdStatus = '04' )  Insert Into Cat_StatusDeOrdenesDeCompras (  IdStatus, Nombre, PermiteDescarga, ModificaCompras, FechaRegistro, Status, Actualizado )  Values ( '04', 'DESCARGADA', 0, 0, '2015-05-15 07:44:44', 'A', 0 )    Else Update Cat_StatusDeOrdenesDeCompras Set Nombre = 'DESCARGADA', PermiteDescarga = 0, ModificaCompras = 0, FechaRegistro = '2015-05-15 07:44:44', Status = 'A', Actualizado = 0 Where IdStatus = '04'  

------------------------------------------------------------------------------------------------------------------------

If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_Claves_Status' and xType = 'U' ) 
   Drop Table COM_OCEN_OrdenesCompra_Claves_Status  
Go--#SQL 

Create Table COM_OCEN_OrdenesCompra_Claves_Status
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	FolioOrden varchar(8) Not Null,
	IdPersonal varchar(4) Not Null,
	FechaRegistro datetime Not Null Default getdate(),
	--FechaRequeridaEntrega datetime Not Null Default getdate(),
	Observaciones varchar(200) Not Null Default '',
	IdStatus varchar(2) Not Null Default '01',
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table COM_OCEN_OrdenesCompra_Claves_Status Add Constraint PK_COM_OCEN_OrdenesCompra_Claves_Status 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden ) 
Go--#SQL 

Alter Table COM_OCEN_OrdenesCompra_Claves_Status Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL
  
Alter Table COM_OCEN_OrdenesCompra_Claves_Status Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL  

Alter Table COM_OCEN_OrdenesCompra_Claves_Status Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table COM_OCEN_OrdenesCompra_Claves_Status Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL

Alter Table COM_OCEN_OrdenesCompra_Claves_Status Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_Cat_StatusDeOrdenesDeCompras
	Foreign Key ( IdStatus ) References Cat_StatusDeOrdenesDeCompras ( IdStatus ) 
Go--#SQL
---------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'COM_OCEN_OrdenesCompra_Claves_Status_Historial' and xType = 'U' ) 
   Drop Table COM_OCEN_OrdenesCompra_Claves_Status_Historial  
Go--#SQL 

Create Table COM_OCEN_OrdenesCompra_Claves_Status_Historial
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	FolioOrden varchar(8) Not Null,
	IdPersonal varchar(4) Not Null,
	FechaRegistro datetime Not Null Default getdate(),
	--FechaRequeridaEntrega datetime Not Null Default getdate(),
	Observaciones varchar(200) Not Null Default '',
	IdStatus varchar(2) Not Null Default '01',
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table COM_OCEN_OrdenesCompra_Claves_Status_Historial Add Constraint PK_COM_OCEN_OrdenesCompra_Claves_Status_Historial 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdStatus, FechaRegistro ) 
Go--#SQL

Alter Table COM_OCEN_OrdenesCompra_Claves_Status_Historial Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_COM_OCEN_OrdenesCompra_Claves_Status
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden ) References COM_OCEN_OrdenesCompra_Claves_Status ( IdEmpresa, IdEstado, IdFarmacia, FolioOrden ) 
Go--#SQL

Alter Table COM_OCEN_OrdenesCompra_Claves_Status_Historial Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_Historial_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL
  
Alter Table COM_OCEN_OrdenesCompra_Claves_Status_Historial Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_Historial_CatEstados 
	Foreign Key ( IdEstado ) References CatEstados ( IdEstado ) 
Go--#SQL  

Alter Table COM_OCEN_OrdenesCompra_Claves_Status_Historial Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_Historial_CatFarmacias
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table COM_OCEN_OrdenesCompra_Claves_Status_Historial Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_Historial_CatPersonal 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL

Alter Table COM_OCEN_OrdenesCompra_Claves_Status_Historial Add Constraint FK_COM_OCEN_OrdenesCompra_Claves_Status_Historial_Cat_StatusDeOrdenesDeCompras
	Foreign Key ( IdStatus ) References Cat_StatusDeOrdenesDeCompras ( IdStatus ) 
Go--#SQL