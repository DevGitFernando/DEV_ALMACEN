
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_ADMI_Impresion_Productos_Compras_Estado' And xType = 'P' )
	Drop Proc spp_ADMI_Impresion_Productos_Compras_Estado
Go--#SQL

Create Procedure spp_ADMI_Impresion_Productos_Compras_Estado ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', 
	@IdFarmacia varchar(4) = '', @FechaInicial varchar(10) =  '2010-01-01', @FechaFinal varchar(10) = '2011-01-31' )
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
		 IdProveedor varchar(5) Not Null Default '',
		 Proveedor varchar(100) Not Null Default '',
		 Folio varchar(30) Not Null Default '',
		 ReferenciaDocto varchar(20) Not Null Default '',
		 FechaCompra datetime Not Null Default '1900-01-01', 
		 SubTotalFactura numeric(14, 4) Not Null Default 0,
		 ImporteIvaFactura numeric(14, 4) Not Null Default 0,
		 ImporteFactura numeric(14, 4) Not Null Default 0,
		 Cantidad numeric(14, 4) Not Null Default 0,
		 Costo numeric(14, 4) Not Null Default 0,
		 TasaIva numeric(14, 4) Not Null Default 0,
		 SubTotalLote numeric(14, 4) Not Null Default 0,
		 ImporteIvaLote numeric(14, 4) Not Null Default 0,
		 ImporteLote numeric(14, 4) Not Null Default 0,
		 SubTotalProducto numeric(14, 4) Not Null Default 0,
		 ImporteIvaProducto numeric(14, 4) Not Null Default 0,
		 ImporteProducto numeric(14, 4) Not Null Default 0,
		 FechaImpresion datetime Not Null  Default GetDate(),
		 FechaInicial varchar(10) Not Null Default '', 
		 FechaFinal varchar(10) Not Null Default '',
		 Año int Null Default 0,
		 Mes int Null Default 0
	)

	Select IdEstado, IdFarmacia Into #tmpFarmaciasProcesar From CatFarmacias(NoLock) Where 1=0 
	
	If @IdFarmacia = ''
	  Begin
		Insert Into #tmpFarmaciasProcesar Select IdEstado, IdFarmacia From CatFarmacias(NoLock) Where IdEstado = @IdEstado
	  End
	Else
	  Begin
		Insert Into #tmpFarmaciasProcesar Select @IdEstado, @IdFarmacia
	  End

	---------------------------------------------------
	-- Se inserta en la tabla temporal los productos --
	---------------------------------------------------
	Insert Into #tmpClaves ( IdProducto, CodigoEAN, ClaveLote, Cantidad, Costo, TasaIva, SubTotalLote, ImporteIvaLote, ImporteLote, 
			SubTotalProducto, ImporteIvaProducto, ImporteProducto, 
			IdEmpresa, IdEstado, IdFarmacia, Folio, ReferenciaDocto, IdProveedor, FechaCompra, SubTotalFactura, ImporteIvaFactura, ImporteFactura, 
			FechaInicial, FechaFinal, Año, Mes )
	Select	L.IdProducto, L.CodigoEAN, L.ClaveLote, IsNull( L.CantidadRecibida, 0 ) as CantidadComprada, D.CostoUnitario, D.TasaIva, 
			Cast(0.000 as Numeric(14,4)) as SubTotalLote, Cast(0.000 as Numeric(14,4)) as ImporteIvaLote, Cast(0.000 as Numeric(14,4)) as ImporteLote, 
			D.SubTotal as SubTotalProducto, D.ImpteIva as ImporteIvaProducto, D.Importe as ImporteProducto, 
			E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioCompra, E.ReferenciaDocto, E.IdProveedor, E.FechaRegistro, 
			E.SubTotal as SubTotalFactura, E.Iva as ImporteIvaFactura, E.Total as ImporteFactura, 
			@FechaInicial, @FechaFinal, DatePart(year, E.FechaRegistro) as Año, DatePart(month, E.FechaRegistro) as Mes 
	From ComprasEnc E(NoLock)
	Inner Join ComprasDet D(NoLock) On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioCompra = D.FolioCompra )
	Inner Join ComprasDet_Lotes L(NoLock) On ( D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioCompra = L.FolioCompra And D.IdProducto = L.IdProducto And D.CodigoEAN = L.CodigoEAN )
	Inner Join #tmpFarmaciasProcesar F(NoLock) On ( E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia ) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado 
		And Convert( varchar(10), E.FechaRegistro, 120 ) Between @FechaInicial And @FechaFinal --And ClaveLote Like '%*%'

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

	-- Se actualiza el SubTotal, Iva y Total de los Lotes
	Update C Set SubTotalLote = IsNull( ( Cantidad * Costo ), 0 )
	From #tmpClaves C(NoLock)

	Update C Set 
			ImporteIvaLote = IsNull( ( ( (1 + ( Cast(TasaIva as Numeric(14,4)) / 100 ) )* SubTotalLote ) - SubTotalLote ), 0 )
	From #tmpClaves C(NoLock)	

	Update F Set ImporteLote = IsNull( ( SubTotalLote + ImporteIvaLote ), 0 )
	From #tmpClaves F(NoLock)

	----------------------------------------
	-- Se obtienen los datos de la Compra --
	----------------------------------------
		
	-- Se obtiene el nombre del proveedor
	Update C
	Set Proveedor = I.Nombre
	From #tmpClaves C 
	Inner Join CatProveedores I(NoLock) On ( C.IdProveedor = I.IdProveedor )
	
	-------------------------------------------------------
	-- Se inserta el resultado en una tabla del servidor --
	-------------------------------------------------------	
	If Exists ( Select Name From SysObjects(NoLock) Where Name = 'tmpADMI_Productos_Compras_Estado' And xType = 'U' )
	  Begin
		Drop Table tmpADMI_Productos_Compras_Estado
	  End	
	
	Select *
	Into tmpADMI_Productos_Compras_Estado
	From #tmpClaves(NoLock)
	Order By IdFarmacia, DescripcionSal

	----------------------------------------
	-- Se devuelven los datos del reporte --
	----------------------------------------

	--Select * From tmpADMI_Productos_Compras_Estado(NoLock) Order By IdFarmacia, DescripcionSal

End
Go--#SQL


