If Exists ( Select Name From SysObjects(NoLock) Where Name = 'Spp_Rpt_ListadoDeOrdenesDeCompraAlmacenes' And xType = 'P' )
	Drop Proc Spp_Rpt_ListadoDeOrdenesDeCompraAlmacenes 
Go--#SQL

Create Procedure Spp_Rpt_ListadoDeOrdenesDeCompraAlmacenes
( 
	@IdEstado Varchar(2) = '21', @IdFarmacia Varchar(4) = '2182', @IdProveedor Varchar(4) = '',
	@FechaInicial varchar(10) = '2016-07-04', @FechaFinal Varchar(10) = '2016-08-12', @ReferenciaDocto Varchar(200) = ''
) 
With Encryption
As
Begin
	Declare
		@sSql Varchar(Max),
		@sWhereReferenciaDocto Varchar(300),
		@sWhereProveedor Varchar(300)
		
	Set @sWhereReferenciaDocto = ''
	Set @sWhereProveedor = ''
	
	if (@ReferenciaDocto <> '')
	Begin
		Set @sWhereReferenciaDocto = ' And E.ReferenciaDocto like ' + Char(39) + '%' + @ReferenciaDocto +'%' + Char(39)
	End
	
	if (@IdProveedor <> '')
	Begin
		Set @sWhereProveedor = ' And E.IdProveedor like ' + Char(39) + @IdProveedor + Char(39)
	End
	
	Set @sSql = '	Select
		' + Char(39) + '00' + Char(39) + ' As IdEstadoGenera, ' + Char(39) + '0000' + Char(39) + ' As IdFarmaciaGenera,
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdProveedor, Cast(' + Char(39) + Char(39) + ' As Varchar(300)) As Proveedor, 
		Cast(' + Char(39) + Char(39) + ' As Varchar(10)) As  FechaGeneracionOC,
		FolioOrdenCompraReferencia, ' + Char(39) + '0000' + Char(39) + ' As IdPersonalCompras, 
		Cast(' + Char(39) + Char(39) + ' As Varchar(300)) As PersonalCompras, E.FolioOrdenCompra As Folio,
		convert(varchar(10), FechaDocto, 120) As FechaDocto,  convert(varchar(10), E.FechaRegistro, 120) As FechaRegistro,
		E.ReferenciaDocto, E.Total, Cast(' + Char(39) + Char(39) + ' As Varchar(300)) As Estado, Cast(' + Char(39) + Char(39) + ' As Varchar(300)) As Farmacia
	Into #TempOrdenesDeCompraEnc
	From OrdenesDeComprasEnc E (Nolock)
	Where E.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And E.IdFarmacia = '  + Char(39) + @IdFarmacia  + Char(39) + 
		' And Convert( varchar(10), E.FechaRegistro, 120) between '  + Char(39) + @FechaInicial  + Char(39) + ' and '  + Char(39) + @FechaFinal + Char(39)
		+ @sWhereProveedor + @sWhereReferenciaDocto + Char(13) + Char(13) +
		
		
	'Update E Set Estado = P.Nombre
	From #TempOrdenesDeCompraEnc E (NoLock)
	Inner Join CatEstados P (NoLock) On (E.IdEstado = P.IdEstado)
	
	Update E Set Farmacia = P.NombreFarmacia
	From #TempOrdenesDeCompraEnc E (NoLock)
	Inner Join CatFarmacias P (NoLock) On (E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia)	
		
	Update E Set Proveedor = P.Nombre
	From #TempOrdenesDeCompraEnc E (NoLock)
	Inner Join CatProveedores P (NoLock) On (E.IdProveedor = P.IdProveedor)
	
	Update M
	Set IdEstadoGenera = OC.IdEstado, IdFarmaciaGenera = OC.IdFarmacia, FechaGeneracionOC = Convert(Varchar(10), OC.FechaRegistro, 120), IdPersonalCompras = OC.Idpersonal
	From #TempOrdenesDeCompraEnc M (NoLock)
	Inner Join COM_OCEN_OrdenesCompra_Claves_Enc OC (NoLock) 
		On ( M.IdEmpresa = OC.FacturarA and M.IdEstado = OC.EstadoEntrega and M.IdFarmacia = OC.EntregarEn 
		     and M.FolioOrdenCompraReferencia = OC.FolioOrden )
	
	Update M Set PersonalCompras = (ApPaterno + ' + Char(39) + ' ' + Char(39) + ' + ApMaterno + ' + Char(39) + ' ' + Char(39) + ' + Nombre)
	From #TempOrdenesDeCompraEnc M (NoLock)
	Inner Join CatPersonal vP (NoLock) On ( M.IdEstadoGenera = vP.IdEstado and M.IdFarmaciaGenera = vP.IdFarmacia and M.IdPersonalCompras = vP.IdPersonal )	
	
	Select
		E.IdEstadoGenera, E.IdFarmaciaGenera, E.IdEstado, E.IdFarmacia, 
		E.IdEmpresa, E.IdProveedor, E.Proveedor, convert(varchar(10), FechaGeneracionOC, 120) As FechaGeneracionOC,
		FolioOrdenCompraReferencia, IdPersonalCompras, PersonalCompras, E.Folio,
		convert(varchar(10), FechaDocto, 120) As FechaDocto,  convert(varchar(10), E.FechaRegistro, 120) As FechaRegistro,
		E.ReferenciaDocto, E.Total, E.Estado, E.Farmacia, P.ClaveSSA_Base, P.ClaveSSA, P.DescripcionSal, P.TipoDeClave,
		P.Laboratorio, L.CodigoEAN, L.IdSubFarmacia, D.IdProducto, P.Presentacion, P.ContenidoPaquete, L.ClaveLote, D.CostoUnitario As Costo,
		L.CantidadRecibida As CantidadLote, P.TasaIva, GETDATE() As FechaCad, Cast(0 As int) as MesesParaCaducar
	Into #TempOrdenesDeCompraDet
	From #TempOrdenesDeCompraEnc E
	Inner Join OrdenesDeComprasDet D (Nolock)
		On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.FolioOrdenCompra)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN)
	Inner Join OrdenesDeComprasDet_Lotes L (Nolock)
		On (E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia
			And E.Folio = L.FolioOrdenCompra And D.CodigoEAN = L.CodigoEAN)



	Update E Set E.FechaCad = F.FechaCaducidad, MesesParaCaducar = datediff(mm, getdate(), IsNull(F.FechaCaducidad, cast('  + CHAR(39) + '2000-01-01' + CHAR(39) + ' as datetime)))
	From #TempOrdenesDeCompraDet E
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
		On ( E.IdEmpresa = F.IdEmpresa and E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia
			 and E.IdSubFarmacia = F.IdSubFarmacia and  E.IdProducto = F.IdProducto and E.CodigoEAN = F.CodigoEAN and E.ClaveLote = F.ClaveLote )

	Select
		E.IdEmpresa, E.IdProveedor, E.Proveedor, E.FechaGeneracionOC, FolioOrdenCompraReferencia, E.IdPersonalCompras, E.PersonalCompras, E.Folio,
		E.FechaDocto,  E.FechaRegistro, E.ReferenciaDocto, E.Total, E.Estado, E.Farmacia, E.ClaveSSA_Base, E.ClaveSSA, E.DescripcionSal, E.TipoDeClave,
		E.Laboratorio, E.CodigoEAN, E.Presentacion, E.ContenidoPaquete, E.ClaveLote, E.Costo, E.CantidadLote, E.TasaIva, E.FechaCad, E.MesesParaCaducar
	From #TempOrdenesDeCompraDet E
	Order By E.Folio, E.FechaRegistro'
	
	Print(@sSql)
	Exec(@sSql)

End 
Go--#SQL