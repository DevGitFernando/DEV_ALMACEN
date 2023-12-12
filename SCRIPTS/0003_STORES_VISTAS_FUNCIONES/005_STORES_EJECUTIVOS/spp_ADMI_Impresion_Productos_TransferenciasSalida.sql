If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_ADMI_Impresion_Productos_TransferenciasSalida' And xType = 'P' )
	Drop Proc spp_ADMI_Impresion_Productos_TransferenciasSalida
Go--#SQL 

Create Procedure spp_ADMI_Impresion_Productos_TransferenciasSalida
( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4) )
With Encryption
As
Begin

	-------------------------------------------------------------------
	-- Se inserta en la tabla temporal los productos de consignacion --
	-------------------------------------------------------------------
	
	Create Table #tmpClaves
	(
		 IdClaveSSA_Sal varchar(4) Not Null Default '',
		 ClaveSSA varchar(50) Not Null Default '',
		 DescripcionSal varchar(7500) Not Null Default '',
		 IdProducto varchar(8) Not Null Default '',
		 CodigoEAN varchar(30) Not Null Default '',
		 ClaveLote varchar(30) Not Null Default '',
		 FechaCaducidad datetime Not Null Default '1900-01-01',
		 MesesPorCaducar int Null Default '',
		 FechaRegistro datetime Not Null Default '',
		 EsConsignacion int Not Null Default 0,
		 DescripcionProducto varchar(200) Not Null Default '',
		 IdPresentacion varchar(3) Not Null Default '',
		 Presentacion varchar(100) Not Null Default '',
		 ContenidoPaquete int Not Null Default '',
		 IdEmpresa varchar(3) Not Null Default '',
		 Empresa varchar(100) Not Null Default '',
		 IdEstado varchar(2) Not Null Default '',
		 Estado varchar(50) Not Null Default '',
		 IdFarmacia varchar(4) Not Null Default '',
		 Farmacia varchar(50) Not Null Default '',
		 Existencia numeric(38, 4) Null Default 0,
		 Folio varchar(30) Not Null Default '', 
		 FolioTransferenciaRef varchar(30) Not Null Default '',
		 IdFarmaciaEnvia varchar(4) Not Null Default '',
		 FarmaciaEnvia varchar(50) Not Null Default '',
		 FechaTransferencia datetime Not Null Default '1900-01-01',
		 FechaRegistroTransferencia datetime Not Null Default '1900-01-01',
		 Costo numeric(14, 4) Not Null Default 0,
		 Cantidad numeric(14, 4) Not Null Default 0,
		 Importe numeric(14, 4) Not Null Default 0,
		 FechaImpresion datetime Not Null  Default GetDate()
	)

	---------------------------------------------------
	-- Se inserta en la tabla temporal los productos --
	---------------------------------------------------
	Insert Into #tmpClaves ( IdProducto, CodigoEAN, ClaveLote, Cantidad, Costo, Importe, IdEmpresa, IdEstado, IdFarmacia, Folio, FolioTransferenciaRef, IdFarmaciaEnvia )
	Select	L.IdProducto, L.CodigoEAN, L.ClaveLote, IsNull( L.CantidadEnviada, 0 ) as CantidadTE, D.CostoUnitario, D.Importe, L.IdEmpresa, 
			L.IdEstado, L.IdFarmacia, E.FolioTransferencia, E.FolioTransferenciaRef, IdFarmaciaRecibe as IdFarmaciaEnvia
	From TransferenciasEnc E(NoLock) 
	Inner Join TransferenciasDet D(NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioTransferencia = D.FolioTransferencia ) 
	Inner Join TransferenciasDet_Lotes L(NoLock) On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia 
			and D.FolioTransferencia = L.FolioTransferencia and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.TipoTransferencia = 'TS' -- And L.ClaveLote Like '%*%'

	-----------------------------------
	-- Se obtienen las descripciones --
	-----------------------------------
	-- Se actualiza el Nombre de la empresa.
	Update C
	Set Empresa = I.NombreEmpresa, Estado = I.Estado, Farmacia = I.Farmacia
	From #tmpClaves C 
	Inner Join vw_EmpresasFarmacias I(NoLock) On ( C.IdEmpresa = I.IdEmpresa and C.IdEstado = I.IdEstado And C.IdFarmacia = I.IdFarmacia )

	-- Se actualizan las descripciones del producto.
	Update C
	Set IdClaveSSA_Sal = I.IdClaveSSA_Sal, ClaveSSA = I.ClaveSSA, DescripcionSal = I.DescripcionSal, DescripcionProducto = I.Descripcion, 
		IdPresentacion = I.IdPresentacion, Presentacion = I.Presentacion, ContenidoPaquete = I.ContenidoPaquete
	From #tmpClaves C 
	Inner Join vw_Productos_CodigoEAN I(NoLock) On ( C.IdProducto = I.IdProducto and C.CodigoEAN = I.CodigoEAN )

	------------------------------------
	-- Se obtienen los datos del Lote --
	------------------------------------

	-- Se actualiza el Campo EsConsignacion
	Update #tmpClaves Set EsConsignacion = 1 Where ClaveLote Like '%*%'

	-- Se actualiza la existencia y los meses por caducar
	Update C
	Set MesesPorCaducar = DateDiff( Month, GetDate(), I.FechaCaducidad ), Existencia = I.Existencia, FechaCaducidad = I.FechaCaducidad, 
		FechaRegistro = I.FechaRegistro
	From #tmpClaves C 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes I(NoLock) On ( C.IdEmpresa = I.IdEmpresa and C.IdEstado = I.IdEstado And C.IdFarmacia = I.IdFarmacia 
		And C.IdProducto = I.IdProducto And C.CodigoEAN = I.CodigoEAN And C.ClaveLote = I.ClaveLote )

	-------------------------------------------------------
	-- Se inserta el resultado en una tabla del servidor --
	-------------------------------------------------------	
	If Exists ( Select Name From SysObjects(NoLock) Where Name = 'tmpADMI_Productos_TransferenciaSalida' And xType = 'U' )
	  Begin
		Drop Table tmpADMI_Productos_TransferenciaSalida
	  End	
	
	Select *
	Into tmpADMI_Productos_TransferenciaSalida
	From #tmpClaves(NoLock)
	Order By IdFarmacia, DescripcionSal

	----------------------------------------
	-- Se devuelven los datos del reporte --
	----------------------------------------

	Select * From tmpADMI_Productos_TransferenciaSalida(NoLock) Order By IdFarmacia, DescripcionSal
End
Go--#SQL 
