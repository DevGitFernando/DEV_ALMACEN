If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Impresion_Ventas_SociosComerciales' And xType = 'P' )
	Drop Proc spp_Impresion_Ventas_SociosComerciales 
Go--#SQL

--Select * From vw_VentasDet_SociosComerciales
--Select * From vw_VentasEnc_SociosComerciales

Create Procedure spp_Impresion_Ventas_SociosComerciales
(
	@IdEmpresa varchar(3) = '001', @IdEstado Varchar(2) = '21', @IdFarmacia Varchar(4)= '2188',
	@Folio Varchar(8)= '00000001'
)
With Encryption 	
As
Begin

	--Drop table #TempSocios
	Select
		E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, 
		E.IdPersonal, E.NombreCompleto,
		CAST('' As Varchar(8)) As IdSocioComercial, CAST('' As Varchar(200)) As SocioComercial,
		CAST('' As Varchar(8)) As IdSocioComercial_Sucursal, CAST('' As Varchar(200)) As SocioComercial_Sucursal,
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		E.FolioVenta, E.ClaveSSA, E.DescripcionSal,
		E.IdProducto, E.CodigoEAN, E.Descripcion, D.ClaveLote, GETDATE() as FechaReg, GETDATE() as FechaCad, 
		'Cancelado' as Status, 
		cast(0 as Int) as Existencia, --, D.Keyx as KeyxDetalleLote
		cast( (D.Cant_Vendida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Vendida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadVendida as int) as CantidadVendida,
		E.Precio, E.TasaIva, CAST('' As Varchar(200)) As Observaciones
	Into #TempSocios	
	From vw_VentasDet_SociosComerciales E (NoLock) 
	Inner Join VentasDet_SociosComerciales_Lotes D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN)
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.FolioVenta = @Folio
			 
			 
	UpDate E
	Set Existencia = L.Existencia, Status = (Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End),
		FechaReg = L.FechaRegistro, FechaCad = L.FechaCaducidad
	From #TempSocios E
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and 
			 E.IdProducto = L.IdProducto and E.CodigoEAN = L.CodigoEAN and
			 E.IdSubFarmacia = L.IdSubFarmacia And E.ClaveLote = L.ClaveLote )
			 
	Update E
	Set
		E.IdSocioComercial = S.IdSocioComercial, E.SocioComercial = O.Nombre,
		E.IdSocioComercial_Sucursal = S.IdSucursal, E.SocioComercial_Sucursal = U.NombreSucursal,
		E.Observaciones = S.Observaciones
	From #TempSocios E
	Inner Join VentasEnc_SociosComerciales S (NoLock)
		On ( E.IdEmpresa = S.IdEmpresa and E.IdEstado = S.IdEstado and E.IdFarmacia = S.IdFarmacia and E.FolioVenta = S.FolioVenta)
	Inner Join CatSociosComerciales O (NoLock) On (S.IdSocioComercial = O.IdSocioComercial)
	Inner Join CatSociosComerciales_Sucursales U (NoLock) On (S.IdSocioComercial = U.IdSocioComercial And S.IdSucursal = U.IdSucursal)
	
			 
	Select *, GETDATE() As FechaSistema From #TempSocios
	
End 
Go--#SQL