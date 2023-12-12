

	

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Conciliacion_OC' and xType = 'V' ) 
	Drop View vw_FACT_Conciliacion_OC 
Go--#SQL 
 	
Create View vw_FACT_Conciliacion_OC 
With Encryption 
As 
	Select E.IdEmpresa, CE.Nombre As Empresa, E.IdEstado, EDO.Nombre As Estado, E.IdFarmacia, FAR.NombreFarmacia As Farmacia, E.FolioOrden As Folio, 
	E.TipoOrden, Convert( varchar(10), E.FechaRegistro, 120) As FechaRegistro,
	E.IdProveedor, P.Nombre As Proveedor, E.EstadoEntrega, EE.Nombre As NombreEstadoEntrega, E.EntregarEn, FE.NombreFarmacia As FarmaciaEntregarEn,
	Case When D.TasaIva = 0 Then Sum(D.Precio * D.Cantidad) Else 0 End As SubTotalSinGrabar,  
	Case When D.TasaIva = 16 Then Sum(D.Precio * D.Cantidad)  Else 0 End As SubTotalGrabado,	
	Case When D.TasaIva = 16 Then Sum(D.Iva * D.Cantidad) Else 0 End As Iva,
	Sum(D.Importe) As TotalOrdenCompra,
	IsNull(F.SubTotal_SinGrabar_Recibido, 0) As SubTotalSinGrabarRecibido, 
	IsNull(F.SubTotal_Grabado_Recibido, 0) As SubTotalGrabadoRecibido, IsNull(F.Iva_Recibido, 0) As IvaRecibido,  
	IsNull(F.Total_Recibido, 0) As TotalRecibido, (Sum(D.Importe) - IsNull(F.Total_Recibido, 0)) As Diferencia,
	E.EsContado, Convert(int, E.EsContado) As EsDeContado, Case When E.EsContado = 0 Then 'Credito' Else 'Contado' End As FormaPago, E.Status,
	Case When (Sum(D.Importe) - IsNull(F.Total_Recibido, 0)) < 0 Then 1 Else 0 End As Cargo,
	Case When (Sum(D.Importe) - IsNull(F.Total_Recibido, 0)) < 0 Then 'A FAVOR' 
		When (Sum(D.Importe) - IsNull(F.Total_Recibido, 0)) = 0 Then 'CONCILIADO' Else 'EN CONTRA' End As StatusDiferencia 
	From COM_OCEN_OrdenesCompra_Claves_Enc E (Nolock)  
	Inner Join COM_OCEN_OrdenesCompra_CodigosEAN_Det D (Nolock)  
		On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioOrden = D.FolioOrden )  
	Left Join FACT_Conciliacion_OrdenesDeCompra F (Nolock)  
		On ( E.IdEmpresa = F.IdEmpresa And E.IdEstado = F.IdEstado And E.EntregarEn = F.IdFarmacia And E.FolioOrden = F.FolioOrden )
	Inner Join CatEmpresas CE (Nolock) On ( E.IdEmpresa = CE.IdEmpresa )	
	Inner Join CatEstados EDO (Nolock) On ( E.IdEstado = EDO.IdEstado )
	Inner Join CatFarmacias FAR (Nolock) On ( E.IdEstado = FAR.IdEstado And E.IdFarmacia = FAR.IdFarmacia )
	Inner Join CatProveedores P (Nolock) On ( E.IdProveedor = P.IdProveedor )
	Inner Join CatEstados EE (Nolock) On ( E.EstadoEntrega = EE.IdEstado )
	Inner Join CatFarmacias FE (Nolock) On ( E.EstadoEntrega = FE.IdEstado And E.EntregarEn = FE.IdFarmacia )
	Where E.Status = 'OC' And E.TipoOrden = 2
	Group By E.IdEmpresa, CE.Nombre, E.IdEstado, EDO.Nombre, E.IdFarmacia, FAR.NombreFarmacia, E.FolioOrden, E.TipoOrden, E.FechaRegistro, 
	E.IdProveedor, P.Nombre, E.EstadoEntrega, EE.Nombre, E.EntregarEn, FE.NombreFarmacia,
	F.Total_Recibido, D.TasaIva, F.SubTotal_SinGrabar_Recibido, F.SubTotal_Grabado_Recibido, F.Iva_Recibido, E.EsContado, E.Status  
	----Having (Sum(D.Importe) - IsNull(F.Total_Recibido, 0)) <> 0  
		
Go--#SQL	
 

