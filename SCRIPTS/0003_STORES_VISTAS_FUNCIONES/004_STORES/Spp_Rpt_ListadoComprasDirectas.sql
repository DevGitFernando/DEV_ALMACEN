If Exists ( Select Name From SysObjects(NoLock) Where Name = 'Spp_Rpt_ListadoComprasDirectas' And xType = 'P' )
	Drop Proc Spp_Rpt_ListadoComprasDirectas
Go--#SQL

Create Procedure Spp_Rpt_ListadoComprasDirectas
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
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdProveedor, Cast(' + Char(39) + Char(39) + ' As Varchar(300)) As Proveedor,
		IdPersonal, Cast(' + Char(39) + Char(39) + ' As Varchar(300)) As Personal, E.FolioCompra As Folio,
		convert(varchar(10), FechaDocto, 120) As FechaDocto,  convert(varchar(10), E.FechaRegistro, 120) As FechaRegistro,
		E.ReferenciaDocto, E.Total, Cast(' + Char(39) + Char(39) + ' As Varchar(300)) As Estado, Cast(' + Char(39) + Char(39) + ' As Varchar(300)) As Farmacia
	Into #TempCompraEnc
	From ComprasEnc E (Nolock)
	Where E.IdEstado = ' + Char(39) + @IdEstado + Char(39) + ' And E.IdFarmacia = '  + Char(39) + @IdFarmacia  + Char(39) + 
		' And Convert( varchar(10), E.FechaRegistro, 120) between '  + Char(39) + @FechaInicial  + Char(39) + ' and '  + Char(39) + @FechaFinal + Char(39)
		+ @sWhereProveedor + @sWhereReferenciaDocto + Char(13) + Char(13) +
		
		
	'Update E Set Estado = P.Nombre
	From #TempCompraEnc E (NoLock)
	Inner Join CatEstados P (NoLock) On (E.IdEstado = P.IdEstado)
	
	Update E Set Farmacia = P.NombreFarmacia
	From #TempCompraEnc E (NoLock)
	Inner Join CatFarmacias P (NoLock) On (E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia)	
		
	Update E Set Proveedor = P.Nombre
	From #TempCompraEnc E (NoLock)
	Inner Join CatProveedores P (NoLock) On (E.IdProveedor = P.IdProveedor)

	
	Update M Set Personal = (ApPaterno + ' + Char(39) + ' ' + Char(39) + ' + ApMaterno + ' + Char(39) + ' ' + Char(39) + ' + Nombre)
	From #TempCompraEnc M (NoLock)
	Inner Join CatPersonal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )	
	
	Select
		E.IdEstado, E.IdFarmacia, 
		E.IdEmpresa, E.IdProveedor, E.Proveedor, IdPersonal, Personal, E.Folio,
		convert(varchar(10), FechaDocto, 120) As FechaDocto,  convert(varchar(10), E.FechaRegistro, 120) As FechaRegistro,
		E.ReferenciaDocto, E.Total, E.Estado, E.Farmacia, P.ClaveSSA_Base, P.ClaveSSA, P.DescripcionSal, P.TipoDeClave,
		P.Laboratorio, L.CodigoEAN, L.IdSubFarmacia, D.IdProducto, P.Presentacion, P.ContenidoPaquete, L.ClaveLote, D.CostoUnitario As Costo,
		L.CantidadRecibida As CantidadLote, P.TasaIva, GETDATE() As FechaCad, Cast(0 As int) as MesesParaCaducar
	Into #TempCompraDet
	From #TempCompraEnc E
	Inner Join ComprasDet D (Nolock)
		On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.FolioCompra)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN)
	Inner Join ComprasDet_Lotes L (Nolock)
		On (E.IdEmpresa = L.IdEmpresa And E.IdEstado = L.IdEstado And E.IdFarmacia = L.IdFarmacia
			And E.Folio = L.FolioCompra And D.CodigoEAN = L.CodigoEAN)



	Update E Set E.FechaCad = F.FechaCaducidad, MesesParaCaducar = datediff(mm, getdate(), IsNull(F.FechaCaducidad, cast('  + CHAR(39) + '2000-01-01' + CHAR(39) + ' as datetime)))
	From #TempCompraDet E
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
		On ( E.IdEmpresa = F.IdEmpresa and E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia
			 and E.IdSubFarmacia = F.IdSubFarmacia and  E.IdProducto = F.IdProducto and E.CodigoEAN = F.CodigoEAN and E.ClaveLote = F.ClaveLote )

	Select 
		E.Estado, E.Farmacia, E.Folio,
		E.IdProveedor As '  + CHAR(39) + 'Clave Proveedor'  + CHAR(39) + ', E.Proveedor, E.IdPersonal As '  + CHAR(39) + 'Clave Personal'  + CHAR(39) + ', E.Personal,
		E.FechaDocto As '  + CHAR(39) + 'Fecha Documento'  + CHAR(39) + ',  E.FechaRegistro As '  + CHAR(39) + 'Fecha Registro'  + CHAR(39) + ',
		E.ReferenciaDocto As Referencia, E.ClaveSSA_Base As '  + CHAR(39) + 'Clave SSA Base'  + CHAR(39) + ', E.ClaveSSA As '  + CHAR(39) + 'Clave SSA'  + CHAR(39) + ',
		E.DescripcionSal As Descripción, E.TipoDeClave As '  + CHAR(39) + 'Tipo De Clave'  + CHAR(39) + ',
		E.Laboratorio, E.CodigoEAN As '  + CHAR(39) + 'Código EAN'  + CHAR(39) + ', E.Presentacion As Presentación,
		E.ContenidoPaquete As '  + CHAR(39) + 'Contenido Paquete'  + CHAR(39) + ', E.ClaveLote As '  + CHAR(39) + 'Clave Lote'  + CHAR(39) + ',
		E.FechaCad As '  + CHAR(39) + 'Fecha Caducidad'  + CHAR(39) + ', E.MesesParaCaducar As '  + CHAR(39) + 'Meses Para Caducar'  + CHAR(39) + ',
		E.CantidadLote As Cantidad, E.Costo, E.TasaIva As '  + CHAR(39) + 'Tasa Iva'  + CHAR(39) + ', ( E.CantidadLote * E.Costo ) * (1 + (E.TasaIva/100.0000)) as Total
	From #TempCompraDet E
	Order By E.Folio, E.FechaRegistro'
	
	Print(@sSql)
	Exec(@sSql)

End 
Go--#SQL