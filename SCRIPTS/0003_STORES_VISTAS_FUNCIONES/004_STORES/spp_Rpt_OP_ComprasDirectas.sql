If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_OP_ComprasDirectas' and xType = 'P')
    Drop Proc spp_Rpt_OP_ComprasDirectas
Go--#SQL

--Exec spp_Rpt_OP_Transferencias 'TS', '2015-05-05', '2015-06-21', '*', '*', '0'

Create Proc spp_Rpt_OP_ComprasDirectas
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(4) = '28',  @IdFarmacia Varchar(4) = '0003',
	@FechaIncial varchar(10) = '2014-09-01', @FechaFinal varchar(10) = '2018-09-05', @IdProveedor Varchar(4) = '0806', @EsDevolucion Bit = 0
)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

	
	Select * 
	Into #vw_Farmacias 
	From CatFarmacias (NoLock)
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

	Select IdProveedor, Nombre As Proveedor, Status
	Into #CatProveedores
	From CatProveedores (NoLock)
	Where 1 = 0


	If (@IdProveedor <> '*' And @IdProveedor <> '')
		Begin
			Insert Into #CatProveedores (IdProveedor, Proveedor, Status)
			Select IdProveedor, Nombre As Proveedor, Status
			From CatProveedores (NoLock)
			Where IdProveedor = @IdProveedor
		End
	Else
		Begin
			Insert Into #CatProveedores (IdProveedor, Proveedor, Status)
			Select IdProveedor, Nombre As Proveedor, Status
			From CatProveedores (NoLock)
		End
	
	Select *
	Into #ComprasEnc
	From ComprasEnc (NoLock)
	Where
		IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
		and Convert(varchar(10), FechaRegistro, 120) Between @FechaIncial  and @FechaFinal


	Select P.IdEstado, P.IdFarmacia, P.IdPersonal, P.Nombre, P.ApPaterno, P.ApMaterno,
			( P.Nombre + ' ' + IsNull(P.ApPaterno, '') + ' ' + IsNull(P.ApMaterno, '') ) as NombrePersonal
	Into #Personal
	From CatPersonal P
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia



	Select 

	E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioCompra As Folio, E.FechaRegistro,
	E.IdPersonal, R.NombrePersonal, E.IdProveedor, P.Proveedor,
	E.ReferenciaDocto, D.IdProducto, D.CodigoEAN, D.CostoUnitario,

	Cast( (CostoUnitario * D.CantidadRecibida) As Numeric(14,4)) As SubTotalLote, 
	Cast( ( ( CostoUnitario * D.CantidadRecibida) * (TasaIva / 100)) As Numeric(14, 4)) As ImpteIvaLote,
	Cast( ( ( CostoUnitario * D.CantidadRecibida) * ( 1 +(TasaIva / 100) ) ) As Numeric(14, 4)) As ImporteLote,
	Cast( (CostoUnitario * D.Cant_Devuelta) As Numeric(14,4)) As SubTotalLoteDevuelto, 
	Cast( ( ( CostoUnitario * D.Cant_Devuelta) * (TasaIva / 100)) As Numeric(14, 4)) As ImpteIvaLoteDevuelto,
	Cast( ( ( CostoUnitario * D.Cant_Devuelta) * ( 1 +(TasaIva / 100) ) ) As Numeric(14, 4)) As ImporteLoteDevuelto
	
	Into #TempDet
	From #ComprasEnc E 
	Inner Join #CatProveedores P (NoLock) On (E.IdProveedor = P.IdProveedor)
	Inner Join #Personal R (NoLock) On (E.IdEstado = R.IdEstado And E.IdFarmacia = R.IdFarmacia And E.IdPersonal = R.IdPersonal)
	Inner Join ComprasDet D (NoLock) On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioCompra = D.FolioCompra)


	Select 
		D.FechaRegistro, D.Folio, D.ReferenciaDocto, D.IdProveedor, D.Proveedor, D.IdPersonal, NombrePersonal,
		ClaveSSA, DescripcionSal, D.IdProducto, P.Descripcion As DescProducto, D.CodigoEAN, L.ClaveLote,
		Convert(varchar(10), F.FechaCaducidad, 120) As FechaCad, D.CostoUnitario,
		CantidadRecibida, TasaIva, SubTotalLote, ImpteIvaLote, ImporteLote,
		Cant_Devuelta, SubTotalLoteDevuelto, ImpteIvaLoteDevuelto, ImporteLoteDevuelto
	Into #TempFinal
	From #TempDet D
	Inner Join ComprasDet_Lotes L (NoLock)
		On (D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.Folio = L.FolioCompra And
			D.IdProducto = L.IdProducto And D.CodigoEAN = L.CodigoEAN)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (L.CodigoEAN = P.CodigoEAN)
		Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
		On (L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And
			L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote)


	If(@EsDevolucion <> 1)
		Begin
			Select
				FechaRegistro As 'Fecha Entrada', Folio As 'Folio Compra Directa', ReferenciaDocto As 'Referencia Compra Directa',
				Proveedor, NombrePersonal As Personal, ClaveSSA As 'Clave SSA', DescripcionSal As 'Descripción Clave SSA',
				DescProducto As 'Descripción Producto', CodigoEAN As 'Código EAN', ClaveLote As 'Clave Lote', FechaCad As 'Fecha Caducidad', CostoUnitario As 'Costo Unitario',
				CantidadRecibida As 'Cantidad Lote', TasaIva As 'Tasa Iva', SubTotalLote As 'Sub Total Lote',
				ImpteIvaLote As 'Importe Iva Lote', ImporteLote As 'Importe Lote '
			From #TempFinal
			Order By Folio, ClaveSSA
		End
	Else
		Begin			
			Delete #TempFinal Where Cant_Devuelta = 0 

			Select
				FechaRegistro As 'Fecha', Folio As 'Folio Compra Directa', ReferenciaDocto As 'Referencia Compra Directa',
				Proveedor, NombrePersonal As Personal, ClaveSSA As 'Clave SSA', DescripcionSal As 'Descripción Clave SSA',
				DescProducto As 'Descripción Producto', CodigoEAN As 'Código EAN', ClaveLote As 'Clave Lote', FechaCad As 'Fecha Caducidad', CostoUnitario As 'Costo Unitario',
				Cant_Devuelta As 'Cantidad Lote', TasaIva As 'Tasa Iva', SubTotalLoteDevuelto As 'Sub Total Lote',
				ImpteIvaLoteDevuelto As 'Importe Iva Lote', ImporteLoteDevuelto As 'Importe Lote'
			From #TempFinal
			Order By Folio, ClaveSSA
		End

End
Go--#SQL