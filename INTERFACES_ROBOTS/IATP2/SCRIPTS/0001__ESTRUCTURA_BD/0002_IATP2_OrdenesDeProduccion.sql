-------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'IATP2_OrdenesDeProduccion_Productos' and xType = 'U' ) 
   Drop Table IATP2_OrdenesDeProduccion_Productos 
Go--#SQL   

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IATP2_OrdenesDeProduccion_InformacionAdicional' and xType = 'U' ) 
	Drop Table IATP2_OrdenesDeProduccion_InformacionAdicional
Go--#SQL  


-------------------------------------------------------------------------------------------------------------------------------------  
-------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'IATP2_OrdenesDeProduccion' and xType = 'U' ) 
   Drop Table IATP2_OrdenesDeProduccion 
Go--#SQL   

Create Table IATP2_OrdenesDeProduccion 
( 
	FolioSolicitud varchar(10) Not Null, 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 

	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null, 

	FechaRegistro datetime Not Null Default getdate(), 

	NumeroDeDocumento varchar(50) Not Null Default '', 
	Observaciones varchar(500) Not Null Default '', 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL   

Alter Table IATP2_OrdenesDeProduccion Add Constraint PK_IATP2_OrdenesDeProduccion Primary Key ( FolioSolicitud )
Go--#SQL   

Alter Table IATP2_OrdenesDeProduccion Add Constraint FK_IATP2_OrdenesDeProduccion___CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL   

Alter Table IATP2_OrdenesDeProduccion Add Constraint FK_IATP2_OrdenesDeProduccion___CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL   

Alter Table IATP2_OrdenesDeProduccion Add Constraint FK_IATP2_OrdenesDeProduccion___CatSubClientes 
	Foreign Key ( IdCliente, IdSubCliente ) References CatSubClientes ( IdCliente, IdSubCliente ) 
Go--#SQL   

Alter Table IATP2_OrdenesDeProduccion Add Constraint FK_IATP2_OrdenesDeProduccion___CatSubProgramas 
	Foreign Key ( IdPrograma, IdSubPrograma ) References CatSubProgramas ( IdPrograma, IdSubPrograma ) 
Go--#SQL   


-------------------------------------------------------------------------------------------------------------------------------------  
-------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'IATP2_OrdenesDeProduccion_InformacionAdicional' and xType = 'U' ) 
	Drop Table IATP2_OrdenesDeProduccion_InformacionAdicional
Go--#SQL  

Create Table IATP2_OrdenesDeProduccion_InformacionAdicional  
(
	FolioSolicitud varchar(10) Not Null, 
	Consecutivo int Not Null, --- identity(1,1), 

	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null, 

	IdBeneficiario varchar(8) Not Null, 
	IdTipoDeDispensacion varchar(2) Not Null Default '00', 
	NumeroDeHabitacion varchar(20) Not Null Default '', 
	NumeroDeCama varchar(20) Not Null Default '', 
	NumReceta varchar(20) Not Null Default '', 
	FechaReceta datetime Not Null default getdate(), 
	IdMedico varchar(6) Not Null, 
	IdBeneficio varchar(4) Not Null Default '0000', 
	IdDiagnostico varchar(6) Not Null Default '000000', 
    IdUMedica varchar(6) Not Null Default '000000', 	
	IdServicio varchar(3) Not Null, 
	IdArea varchar(3) Not Null,  
	RefObservaciones varchar(100) Not Null Default '', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table IATP2_OrdenesDeProduccion_InformacionAdicional Add Constraint 
	PK_IATP2_OrdenesDeProduccion_InformacionAdicional Primary Key ( FolioSolicitud, Consecutivo ) 
Go--#SQL  

Alter Table IATP2_OrdenesDeProduccion_InformacionAdicional Add Constraint FK_IATP2_OrdenesDeProduccion_InformacionAdicional___IATP2_OrdenesDeProduccion
	Foreign Key ( FolioSolicitud ) References IATP2_OrdenesDeProduccion ( FolioSolicitud ) 
Go--#SQL  

Alter Table IATP2_OrdenesDeProduccion_InformacionAdicional Add Constraint FK_IATP2_OrdenesDeProduccion_InformacionAdicional_CatMedicos 
	Foreign Key ( IdEstado, IdFarmacia, IdMedico ) References CatMedicos ( IdEstado, IdFarmacia, IdMedico ) 
Go--#SQL  

Alter Table IATP2_OrdenesDeProduccion_InformacionAdicional Add Constraint FK_IATP2_OrdenesDeProduccion_InformacionAdicional_CatServicios_Areas  
	Foreign Key ( IdServicio, IdArea ) References CatServicios_Areas ( IdServicio, IdArea ) 
Go--#SQL  

Alter Table IATP2_OrdenesDeProduccion_InformacionAdicional Add Constraint FK_IATP2_OrdenesDeProduccion_InformacionAdicional_CatBeneficios  
	Foreign Key ( IdBeneficio ) References CatBeneficios ( IdBeneficio ) 
Go--#SQL  

Alter Table IATP2_OrdenesDeProduccion_InformacionAdicional Add Constraint FK_IATP2_OrdenesDeProduccion_InformacionAdicional_CatTiposDispensacion  
	Foreign Key ( IdTipoDeDispensacion ) References CatTiposDispensacion ( IdTipoDeDispensacion ) 
Go--#SQL  

Alter Table IATP2_OrdenesDeProduccion_InformacionAdicional Add Constraint FK_IATP2_OrdenesDeProduccion_InformacionAdicional_CatUnidadesMedicas 
    Foreign Key ( IdUMedica ) References CatUnidadesMedicas ( IdUMedica )  
Go--#SQL  



-------------------------------------------------------------------------------------------------------------------------------------  
-------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'IATP2_OrdenesDeProduccion_Productos' and xType = 'U' ) 
   Drop Table IATP2_OrdenesDeProduccion_Productos 
Go--#SQL   

Create Table IATP2_OrdenesDeProduccion_Productos 
(
--	IdCliente varchar(4) Not Null, 		
	FolioSolicitud varchar(10) Not Null, 
	Consecutivo int Not Null, --- identity(1,1), 
	
	Partida int Not Null, --- identity(1,1), 	
		
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	CantidadSolicitada int Not Null Default 0, 
	CantidadSurtida int Not Null Default 0, 
	
	FechaHora_De_Administracion datetime Not Null Default getdate(), 

	FechaRegistro datetime Not Null Default getdate(), 
	FechaSurtido datetime Not Null Default getdate(), 	
	
	Status_ATP2 smallint Not Null Default 0,  ---> -2 Registrado, -1 Solicitado 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
) 
Go--#SQL   

Alter Table IATP2_OrdenesDeProduccion_Productos Add Constraint PK_IATP2_OrdenesDeProduccion_Productos Primary Key ( FolioSolicitud, Consecutivo, IdProducto, CodigoEAN ) 
Go--#SQL  

Alter Table IATP2_OrdenesDeProduccion_Productos Add Constraint PK_IATP2_OrdenesDeProduccion_Productos__IATP2_OrdenesDeProduccion_InformacionAdicional 
	Foreign Key ( FolioSolicitud, Consecutivo ) References IATP2_OrdenesDeProduccion_InformacionAdicional ( FolioSolicitud, Consecutivo ) 
Go--#SQL  

----Alter Table IATP2_OrdenesDeProduccion_Productos Add Constraint FK_IATP2_OrdenesDeProduccion_Productos_IMach_CFGC_Clientes_Productos 
----	Foreign Key ( IdCliente, IdProducto, CodigoEAN ) References IMach_CFGC_Clientes_Productos ( IdCliente, IdProducto, CodigoEAN ) 
----Go--#SQL   


