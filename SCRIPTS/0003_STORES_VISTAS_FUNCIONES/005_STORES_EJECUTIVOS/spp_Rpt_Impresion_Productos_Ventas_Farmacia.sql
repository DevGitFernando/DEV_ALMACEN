
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_Impresion_Productos_Ventas_Farmacia' And xType = 'P' )
	Drop Proc spp_Rpt_Impresion_Productos_Ventas_Farmacia
Go--#SQL

Create Procedure spp_Rpt_Impresion_Productos_Ventas_Farmacia ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', 
	@IdFarmacia varchar(4) = '0008',
	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-01-31' )
With Encryption
As
Begin

	-------------------------------------------------------------------
	-- Se inserta en la tabla temporal los productos de consignacion --
	-------------------------------------------------------------------
	
	Create Table #tmpClaves_Farmacia
	(
		 IdClaveSSA_Sal varchar(4) Not Null Default '',
		 ClaveSSA varchar(50) Not Null Default '',
		 DescripcionSal varchar(7500) Not Null Default '',
		 IdProducto varchar(8) Not Null Default '',
		 CodigoEAN varchar(30) Not Null Default '',
		 ClaveLote varchar(30) Not Null Default '',
		 FechaCaducidad datetime Not Null Default '1900-01-01',
		 MesesPorCaducar int Null Default 0,
		 FechaRegistro datetime Not Null Default '1900-01-01',		 
		 EsConsignacion int Not Null Default 0,
		 DescripcionProducto varchar(200) Not Null Default '',
		 IdPresentacion varchar(3) Not Null Default '',
		 Presentacion varchar(100) Not Null Default '',
		 ContenidoPaquete int Not Null Default 0,
		 IdEmpresa varchar(3) Not Null Default '',
		 Empresa varchar(100) Not Null Default '',
		 IdEstado varchar(2) Not Null Default '',
		 Estado varchar(50) Not Null Default '',
		 IdFarmacia varchar(4) Not Null Default '',
		 Farmacia varchar(50) Not Null Default '',
		 Existencia numeric(38, 4) Null Default 0,
		 Folio varchar(30) Not Null Default '',
		 FechaVenta datetime Not Null Default '1900-01-01', 
		 Cantidad numeric(14, 4) Not Null Default 0,
		 Costo numeric(14, 4) Not Null Default 0,
		 Importe numeric(14, 4) Not Null Default 0,
		 FechaImpresion datetime Not Null  Default GetDate()
	)

	---------------------------------------------------
	-- Se inserta en la tabla temporal los productos --
	---------------------------------------------------
	Insert Into #tmpClaves_Farmacia ( IdProducto, CodigoEAN, ClaveLote, Cantidad, Costo, Importe, IdEmpresa, IdEstado, IdFarmacia, Folio, FechaVenta )
	Select	L.IdProducto, L.CodigoEAN, L.ClaveLote, IsNull( L.CantidadVendida, 0 ) as CantidadComprada, D.PrecioUnitario, 
			( ( IsNull( L.CantidadVendida, 0 ) ) * D.PrecioUnitario ) as Importe, 
			E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioVenta, E.FechaRegistro
	From VentasEnc E(NoLock)
	Inner Join VentasDet D(NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVenta = D.FolioVenta )
	Inner Join VentasDet_Lotes L(NoLock) On ( D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioVenta = L.FolioVenta And D.IdProducto = L.IdProducto And D.CodigoEAN = L.CodigoEAN )
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia 
		And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal --And ClaveLote Like '%*%'

	-----------------------------------
	-- Se obtienen las descripciones --
	-----------------------------------
	-- Se actualiza el Nombre de la empresa.
	Update C
	Set Empresa = I.NombreEmpresa, Estado = I.Estado, Farmacia = I.Farmacia
	From #tmpClaves_Farmacia C 
	Inner Join vw_EmpresasFarmacias I(NoLock) On ( C.IdEmpresa = I.IdEmpresa and C.IdEstado = I.IdEstado And C.IdFarmacia = I.IdFarmacia )

	-- Se actualizan las descripciones del producto.
	Update C
	Set IdClaveSSA_Sal = I.IdClaveSSA_Sal, ClaveSSA = I.ClaveSSA, DescripcionSal = I.DescripcionSal, DescripcionProducto = I.Descripcion, 
		IdPresentacion = I.IdPresentacion, Presentacion = I.Presentacion, ContenidoPaquete = I.ContenidoPaquete
	From #tmpClaves_Farmacia C 
	Inner Join vw_Productos_CodigoEAN I(NoLock) On ( C.IdProducto = I.IdProducto and C.CodigoEAN = I.CodigoEAN )
	--Where I.ClaveSSA In( '101' )

	--Delete From #tmpClaves_Farmacia Where IdClaveSSA_SAl = ''
	------------------------------------
	-- Se obtienen los datos del Lote --
	------------------------------------

	-- Se actualiza el Campo EsConsignacion
	Update #tmpClaves_Farmacia Set EsConsignacion = 1 Where ClaveLote Like '%*%'

	-- Se actualiza la existencia y los meses por caducar
	Update C
	Set MesesPorCaducar = DateDiff( Month, GetDate(), I.FechaCaducidad ), Existencia = I.Existencia, FechaCaducidad = I.FechaCaducidad, 
		FechaRegistro = I.FechaRegistro
	From #tmpClaves_Farmacia C 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes I(NoLock) On ( C.IdEmpresa = I.IdEmpresa and C.IdEstado = I.IdEstado And C.IdFarmacia = I.IdFarmacia 
		And C.IdProducto = I.IdProducto And C.CodigoEAN = I.CodigoEAN And C.ClaveLote = I.ClaveLote )
	
	-------------------------------------------------------
	-- Se inserta el resultado en una tabla del servidor --
	-------------------------------------------------------	
	If Exists ( Select Name From SysObjects(NoLock) Where Name = 'tmpRpt_Productos_Ventas_Farmacia' And xType = 'U' )
	  Begin
		Drop Table tmpRpt_Productos_Ventas_Farmacia
	  End	
	
	Select *
	Into tmpRpt_Productos_Ventas_Farmacia
	From #tmpClaves_Farmacia(NoLock)
	Order By IdFarmacia, DescripcionSal

	----------------------------------------
	-- Se devuelven los datos del reporte --
	----------------------------------------

	--Select * From tmpRpt_Productos_Ventas_Farmacia(NoLock) Order By IdFarmacia, DescripcionSal

	Exec spp_Rpt_Impresion_Productos_Porcentajes_Ventas_Farmacia @IdEmpresa, @IdEstado, @IdFarmacia, @FechaInicial, @FechaFinal

End
Go--#SQL

