---------------------------------------------------------------------------------------------------------------------------- 
------If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Servicio_A_DomicilioDet_Lotes' and xType = 'U' ) 
------	Drop Table Vales_Servicio_A_DomicilioDet_Lotes  
------Go--#SzzzQL  

---------------------------------------------------------------------------------------------------------------------------- 
------If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Servicio_A_DomicilioDet' and xType = 'U' ) 
------	Drop Table Vales_Servicio_A_DomicilioDet 
------Go--#SzzzQL

-------------------------------------------
---------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Servicio_A_Domicilio' and xType = 'U' ) 
Begin 
Create Table Vales_Servicio_A_Domicilio 
(
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioServicioDomicilio varchar(30) Not Null, 
	FolioVale varchar(30) Not Null,
	 
	FolioVentaGenerado varchar(30) Not Null Default '', 
	
	FechaRegistro datetime Default getdate(), 
	IdPersonal varchar(4) Not Null, 
	
	HoraVisita_Desde datetime Default getdate(), 
	HoraVisita_Hasta datetime Default getdate(), 

	ServicioConfirmado bit Not Null Default 'false', 
	FechaConfirmacion datetime Default getdate(), 
	IdPersonalConfirma varchar(4) Not Null, 

	TipoSurtimiento int Not Null Default 0, 
	ReferenciaSurtimiento varchar(30) Not Null Default '', 

	OrigenDeServicio smallint Not Null Default 1, 
	PedidoEnviado bit Not Null Default 'false', 
	FechaEnvioPedido datetime Not Null Default getdate(), 

	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	FechaControl datetime Not Null Default getdate()  
) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'PK_Vales_Servicio_A_Domicilio' and xType = 'PK' ) 
Begin 
	Alter Table Vales_Servicio_A_Domicilio Add Constraint PK_Vales_Servicio_A_Domicilio 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_Vales_EmisionDet_Vales_EmisionEnc' and xType = 'F' ) 
Begin 
Alter Table Vales_Servicio_A_Domicilio Add Constraint FK_Vales_Servicio_A_Domicilio___Vales_EmisionEnc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) 
	References Vales_EmisionEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVale ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_Vales_Servicio_A_Domicilio___CatPersonal_Registra' and xType = 'F' ) 
Begin 
Alter Table Vales_Servicio_A_Domicilio Add Constraint FK_Vales_Servicio_A_Domicilio___CatPersonal_Registra
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
End 
Go--#SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'FK_Vales_Servicio_A_Domicilio___CatPersonal_Confirma' and xType = 'F' ) 
Begin 
Alter Table Vales_Servicio_A_Domicilio Add Constraint FK_Vales_Servicio_A_Domicilio___CatPersonal_Confirma 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonalConfirma ) 
	References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
End 
Go--#SQL  


---------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Servicio_A_DomicilioDet' and xType = 'U' )  
Begin 
	Create Table Vales_Servicio_A_DomicilioDet 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,  
		FolioServicioDomicilio varchar(30) Not Null, 
		IdProducto varchar(8) Not Null, 	
		CodigoEAN varchar(30) Not Null, 
		Renglon int Not Null,   	
		UnidadDeEntrada smallint Not Null Default 1, 
		Cant_Recibida Numeric(14,4) Not Null Default 0, 
		Cant_Devuelta Numeric(14,4) Not Null Default 0, 
		CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion
		CostoUnitario Numeric(14,4) Not Null Default 0, 	
		TasaIva Numeric(14,4) Not Null Default 0, 
		SubTotal Numeric(14,4) Not Null Default 0, 
		ImpteIva Numeric(14,4) Not Null Default 0, 
		Importe Numeric(14,4) Not Null Default 0, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table Vales_Servicio_A_DomicilioDet Add Constraint PK_Vales_Servicio_A_DomicilioDet 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio, IdProducto, CodigoEAN, Renglon ) 

	Alter Table Vales_Servicio_A_DomicilioDet Add Constraint FK_Vales_Servicio_A_DomicilioDet_Vales_Servicio_A_Domicilio 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio ) 
		References Vales_Servicio_A_Domicilio ( IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio ) 

	Alter Table Vales_Servicio_A_DomicilioDet Add Constraint FK_Vales_Servicio_A_DomicilioDet_CatProductos_CodigosRelacionados 
		Foreign Key ( IdProducto, CodigoEAN ) 
		References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN ) 
End 	
Go--#SQL  


---------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Vales_Servicio_A_DomicilioDet_Lotes' and xType = 'U' )  
Begin 
	Create Table Vales_Servicio_A_DomicilioDet_Lotes 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdSubFarmacia varchar(2) Not Null, 	
		FolioServicioDomicilio varchar(30) Not Null, 
		IdProducto varchar(8) Not Null, 
		CodigoEAN varchar(30) Not Null, 
		ClaveLote varchar(30) Not Null, 
		Renglon int not null,  
		EsConsignacion Bit Not Null Default 'false',  	
		Cant_Recibida Numeric(14,4) Not Null Default 0, 
		Cant_Devuelta Numeric(14,4) Not Null Default 0, 
		CantidadRecibida Numeric(14,4) Not Null Default 0, --- == (Cant_Recibida - Cant_Devuelta) para tomar en cuenta alguna posible cancelacion	
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table Vales_Servicio_A_DomicilioDet_Lotes Add Constraint PK_Vales_Servicio_A_DomicilioDet_Lotes
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioServicioDomicilio, IdProducto, CodigoEAN, ClaveLote, Renglon ) 

	Alter Table Vales_Servicio_A_DomicilioDet_Lotes Add Constraint FK_Vales_Servicio_A_DomicilioDet_Lotes_Vales_Servicio_A_DomicilioDet 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio, IdProducto, CodigoEAN, Renglon ) 
		References Vales_Servicio_A_DomicilioDet ( IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio, IdProducto, CodigoEAN, Renglon ) 

	Alter Table Vales_Servicio_A_DomicilioDet_Lotes Add Constraint FK_Vales_Servicio_A_DomicilioDet_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
		References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	
End	
Go--#SQL  
