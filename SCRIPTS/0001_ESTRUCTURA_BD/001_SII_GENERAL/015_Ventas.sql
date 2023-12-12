--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasInformacionAdicional' and xType = 'U' ) 
	Drop Table VentasInformacionAdicional 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstDispensacion' and xType = 'U' ) 
	Drop Table VentasEstDispensacion 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes' and xType = 'U' ) 
	Drop Table VentasDet_Lotes  
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet' and xType = 'U' ) 
	Drop Table VentasDet 
Go--#SQL 

------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatTiposDispensacion' and xType = 'U') 
   Drop Table CatTiposDispensacion 
Go--#SQL 

Create Table CatTiposDispensacion 
( 
	IdTipoDeDispensacion varchar(2) Not Null, 
	Descripcion varchar(50) Not Null, 
	EsDeFarmacia bit Not Null Default 'false', 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL 


Alter Table CatTiposDispensacion Add Constraint PK_CatTiposDispensacion Primary Key ( IdTipoDeDispensacion ) 
Go--#SQL 

If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '00' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '00', 'Origen No Especificado', 'A', 0, 1 )    Else Update CatTiposDispensacion Set Descripcion = 'Origen No Especificado', Status = 'A', Actualizado = 0, EsDeFarmacia = 1 Where IdTipoDeDispensacion = '00'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '01' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '01', 'Receta Consulta Externa', 'A', 0, 1 )    Else Update CatTiposDispensacion Set Descripcion = 'Receta Consulta Externa', Status = 'A', Actualizado = 0, EsDeFarmacia = 1 Where IdTipoDeDispensacion = '01'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '02' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '02', 'Receta Hospitalaria', 'A', 0, 1 )    Else Update CatTiposDispensacion Set Descripcion = 'Receta Hospitalaria', Status = 'A', Actualizado = 0, EsDeFarmacia = 1 Where IdTipoDeDispensacion = '02'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '03' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '03', 'Colectivo Hospital', 'A', 0, 1 )    Else Update CatTiposDispensacion Set Descripcion = 'Colectivo Hospital', Status = 'A', Actualizado = 0, EsDeFarmacia = 1 Where IdTipoDeDispensacion = '03'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '04' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '04', 'Colectivo Almacen', 'A', 0, 1 )    Else Update CatTiposDispensacion Set Descripcion = 'Colectivo Almacen', Status = 'A', Actualizado = 0, EsDeFarmacia = 1 Where IdTipoDeDispensacion = '04'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '05' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '05', 'Oficio Requerimiento', 'A', 0, 1 )    Else Update CatTiposDispensacion Set Descripcion = 'Oficio Requerimiento', Status = 'A', Actualizado = 0, EsDeFarmacia = 1 Where IdTipoDeDispensacion = '05'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '06' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '06', 'Receta Foranea', 'A', 0, 1 )    Else Update CatTiposDispensacion Set Descripcion = 'Receta Foranea', Status = 'A', Actualizado = 0, EsDeFarmacia = 1 Where IdTipoDeDispensacion = '06'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '07' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '07', 'Receta generada por Vales', 'A', 0, 1 )    Else Update CatTiposDispensacion Set Descripcion = 'Receta generada por Vales', Status = 'A', Actualizado = 0, EsDeFarmacia = 1 Where IdTipoDeDispensacion = '07'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '08' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '08', 'CS menos de 800', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'CS menos de 800', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '08'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '09' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '09', 'Semana Nacional de Salud', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Semana Nacional de Salud', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '09'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '10' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '10', 'Casas de Salud', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Casas de Salud', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '10'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '11' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '11', 'Unidades Móviles', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Unidades Móviles', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '11'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '12' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '12', 'Caravanas', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Caravanas', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '12'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '13' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '13', 'Extramuros', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Extramuros', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '13'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '14' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '14', 'Planificación familiar', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Planificación familiar', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '14'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '15' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '15', 'Feria de la salud', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Feria de la salud', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '15'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '16' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '16', 'Equipo de Salud Itinerante', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Equipo de Salud Itinerante', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '16'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '17' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '17', 'Zoonosis', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Zoonosis', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '17'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '18' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '18', 'Brigadas de Oportunidades', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Brigadas de Oportunidades', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '18'
If Not Exists ( Select * From CatTiposDispensacion Where IdTipoDeDispensacion = '19' )  Insert Into CatTiposDispensacion (  IdTipoDeDispensacion, Descripcion, Status, Actualizado, EsDeFarmacia )  Values ( '19', 'Capasits', 'A', 0, 0 )    Else Update CatTiposDispensacion Set Descripcion = 'Capasits', Status = 'A', Actualizado = 0, EsDeFarmacia = 0 Where IdTipoDeDispensacion = '19' 
Go--#SQL  


---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEnc' and xType = 'U' ) 
	Drop Table VentasEnc 
Go--#SQL  

Create Table VentasEnc 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	FolioMovtoInv varchar(30) Not Null, 
	FechaSistema datetime Default GetDate(),     --- Fecha de Sistema en que se realizo el movimiento  		
	-- FechaVenta datetime Default getdate(), 
	FechaRegistro datetime Default getdate(), 
	
	FolioCierre int Not Null Default 0, 
	Corte tinyint Not Null Default 0, 	
	
	IdCaja varchar(2) Not Null Default '', 
	IdPersonal varchar(4) Not Null, 
	IdCliente varchar(4) Not Null, 
	IdSubCliente varchar(4) Not Null, 
	IdPrograma varchar(4) Not Null, 
	IdSubPrograma varchar(4) Not Null, 
--	IdPaciente varchar(20) Null Default '', 
--	FolioDerechoHabiencia varchar(30) Null Default '', 
--	FolioReceta varchar(20) Null Default '', 
	SubTotal Numeric(14,4) Not Null Default 0, 
	Descuento Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0, 
	TipoDeVenta smallint Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table VentasEnc Add Constraint PK_VentasEnc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL  

Alter Table VentasEnc Add Constraint FK_VentasEnc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table VentasEnc Add Constraint FK_VentasEnc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

--Alter Table VentasEnc Add Constraint FK_VentasEnc_CatCajas  
--	Foreign Key ( IdEstado, IdFarmacia, IdCaja ) References CatCajas ( IdEstado, IdFarmacia, IdCaja ) 
--Go 

Alter Table VentasEnc Add Constraint FK_VentasEnc_CatPersonal  
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

Alter Table VentasEnc -- With NoCheck 
    Add Constraint FK_VentasEnc_CatSubClientes -- With NoCheck 
    Foreign Key ( IdCliente, IdSubCliente  ) References CatSubClientes ( IdCliente, IdSubCliente )  
Go--#SQL 

Alter Table VentasEnc -- With NoCheck 
    Add Constraint FK_VentasEnc_CatSubProgramas  
    Foreign Key ( IdPrograma, IdSubPrograma ) References CatSubProgramas ( IdPrograma, IdSubPrograma )  
Go--#SQL


---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet' and xType = 'U' ) 
	Drop Table VentasDet 
Go--#SQL  

Create Table VentasDet 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	Renglon int Not Null,  
	-- EsConsignacion tinyint Not Null Default 0,  	
	UnidadDeSalida smallint Not Null Default 1, 
	Cant_Entregada Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadVendida Numeric(14,4) Not Null Default 0, --- == (Cant_Entregada - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	CostoUnitario Numeric(14,4) Not Null Default 0,  
	PrecioLicitacion Numeric(14,4) Not Null Default 0, 		
	PrecioUnitario Numeric(14,4) Not Null Default 0, 
	TasaIva Numeric(14,4) Not Null Default 0, 
	ImpteIva Numeric(14,4) Not Null Default 0, 
	PorcDescto Numeric(14,4) Not Null Default 0, 
	ImpteDescto Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table VentasDet Add Constraint PK_VentasDet Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  

Alter Table VentasDet Add Constraint FK_VentasDet_VentasEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL  

Alter Table VentasDet Add Constraint FK_VentasDet_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) 
	References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
Go--#SQL 


---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes' and xType = 'U' ) 
	Drop Table VentasDet_Lotes  
Go--#SQL  

Create Table VentasDet_Lotes 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	FolioVenta varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,  
	EsConsignacion Bit Not Null Default 'false',  	
	CostoUnitario numeric(14,4) Not Null Default 0, 
	Cant_Vendida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadVendida Numeric(14,4) Not Null Default 0, --- == (Cant_Entregada - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table VentasDet_Lotes Add Constraint PK_VentasDet_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL  

Alter Table VentasDet_Lotes Add Constraint FK_VentasDet_Lotes_VentasDet 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon ) 
	References VentasDet ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon ) 
Go--#SQL  

Alter Table VentasDet_Lotes Add Constraint FK_VentasDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  


---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'VentasDet_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table VentasDet_Lotes_Ubicaciones 
Go--#SQL 

Create Table VentasDet_Lotes_Ubicaciones 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdSubFarmacia varchar(2) Not Null, 	
	FolioVenta varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	Renglon int not null,

	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null,
  
	EsConsignacion Bit Not Null Default 'false',  	
	Cant_Vendida Numeric(14,4) Not Null Default 0, 
	Cant_Devuelta Numeric(14,4) Not Null Default 0, 
	CantidadVendida Numeric(14,4) Not Null Default 0, --- == (Cant_Entregada - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL 

Alter Table VentasDet_Lotes_Ubicaciones Add Constraint PK_VentasDet_Lotes_Ubicaciones 
    Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, Renglon, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL 

Alter Table VentasDet_Lotes_Ubicaciones Add Constraint FK_VentasDet_Lotes_Ubicaciones_VentasDet_Lotes 
    Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
    References VentasDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, Renglon ) 
Go--#SQL 

---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstDispensacion' and xType = 'U' ) 
	Drop Table VentasEstDispensacion 
Go--#SQL  

Create Table VentasEstDispensacion  
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	TotalProductos int Not Null Default 0, 	
	ProductosADispensar int Not Null Default 0, 	
	ProductosDispensados int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table VentasEstDispensacion Add Constraint PK_VentasEstDispensacion Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL  

Alter Table VentasEstDispensacion Add Constraint FK_VentasEstDispensacion_VentasEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL  


---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasInformacionAdicional' and xType = 'U' ) 
	Drop Table VentasInformacionAdicional 
Go--#SQL  

Create Table VentasInformacionAdicional  
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	IdBeneficiario varchar(8) Not Null, 
	IdEstadoResidencia varchar(2) Not Null Default '00', 
	IdTipoDerechoHabiencia varchar(3) Not Null Default '001', 

	IdTipoDeDispensacion varchar(2) Not Null Default '00', 
	NumReceta varchar(50) Not Null, 
	FechaReceta datetime Not Null default getdate(), 
	IdMedico varchar(6) Not Null, 
	IdBeneficio varchar(4) Not Null Default '0000', 
	IdDiagnostico varchar(6) Not Null, 
    IdUMedica varchar(6) Not Null Default '000000', 	
	IdServicio varchar(3) Not Null, 
	IdArea varchar(3) Not Null,  
	NumeroDeHabitacion varchar(20) Not Null Default '', 
	NumeroDeCama varchar(20) Not Null Default '', 
	RefObservaciones varchar(100) Not Null Default '', 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
) 
Go--#SQL  

Alter Table VentasInformacionAdicional Add Constraint PK_VentasInformacionAdicional Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL  

Alter Table VentasInformacionAdicional Add Constraint FK_VentasInformacionAdicional_VentasEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL  

Alter Table VentasInformacionAdicional Add Constraint FK_VentasInformacionAdicional_CatMedicos 
	Foreign Key ( IdEstado, IdFarmacia, IdMedico ) References CatMedicos ( IdEstado, IdFarmacia, IdMedico ) 
Go--#SQL  

Alter Table VentasInformacionAdicional Add Constraint FK_VentasInformacionAdicional_CatServicios_Areas  
	Foreign Key ( IdServicio, IdArea ) References CatServicios_Areas ( IdServicio, IdArea ) 
Go--#SQL  

Alter Table VentasInformacionAdicional Add Constraint FK_VentasInformacionAdicional_CatBeneficios  
	Foreign Key ( IdBeneficio ) References CatBeneficios ( IdBeneficio ) 
Go--#SQL  

Alter Table VentasInformacionAdicional Add Constraint FK_VentasInformacionAdicional_CatTiposDispensacion  
	Foreign Key ( IdTipoDeDispensacion ) References CatTiposDispensacion ( IdTipoDeDispensacion ) 
Go--#SQL  

Alter Table VentasInformacionAdicional Add Constraint FK_VentasInformacionAdicional_CatUnidadesMedicas 
    Foreign Key ( IdUMedica ) References CatUnidadesMedicas ( IdUMedica )  
Go--#SQL  

Alter Table VentasInformacionAdicional Add Constraint FK_VentasInformacionAdicional___CatTiposDeDerechohabiencia 
	Foreign Key ( IdTipoDerechoHabiencia ) References CatTiposDeDerechohabiencia ( IdTipoDerechoHabiencia )   
Go--#SQL  


---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasEstadisticaClavesDispensadas' and xType = 'U' ) 	
   Drop Table VentasEstadisticaClavesDispensadas
Go--#SQL    

Create Table VentasEstadisticaClavesDispensadas 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null, 
	IdClaveSSA varchar(4) Not Null, 
	
	Observaciones varchar(100) Not Null Default '', 	
	EsCapturada bit Not Null Default 'false', 
	
	CantidadRequerida numeric(14,4) Not Null Default 0, 
	CantidadEntregada numeric(14,4) Not Null Default 0, 
	
	ExistenciaSistema numeric(14,4) Not Null Default 0, 
	TieneCartaFaltante bit Not Null Default 'false', 
		
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0  
)
Go--#SQL 

Alter Table VentasEstadisticaClavesDispensadas Add Constraint PK_VentasEstadisticaClavesDispensadas 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdClaveSSA ) 
Go--#SQL 

Alter Table VentasEstadisticaClavesDispensadas Add Constraint FK_VentasEstadisticaClavesDispensadas_VentasEnc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
Go--#SQL 	

Alter Table VentasEstadisticaClavesDispensadas Add Constraint FK_VentasEstadisticaClavesDispensadas_CatClavesSSA_Sales 
	Foreign Key ( IdClaveSSA ) References CatClavesSSA_Sales ( IdClaveSSA_Sal ) 
Go--#SQL 	
	

	