--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Ticket___AMPM' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Ticket___AMPM 
Go--#SQL  

Create Proc spp_Rpt_Impresion_Ticket___AMPM 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '9', @IdFarmacia varchar(4) = '11', @Folio varchar(8) = '10138' 
)    
With Encryption 
As 
Begin 
-- Set NoCount On 
Set DateFormat YMD 
Declare @sNA varchar(10)  


	Set @IdEmpresa = right('00000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('00000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('00000000' + @IdFarmacia, 4) 
	Set @Folio = right('00000000' + @Folio, 8) 
	Set @sNA = '' 
	
	
	Select 
		V.IdEmpresa, V.Empresa, Cast('' As Varchar(15)) As RFC, V.EmpDomicilio, V.EmpColonia, V.EmpCodigoPostal, V.EmpEdoCiudad, V.IdEstado, V.Estado, V.ClaveRenapo, 
		V.IdFarmacia, V.Farmacia, V.EsAlmacen, V.Folio, V.FechaSistema, V.FechaRegistro, V.IdCaja, V.IdPersonal, V.NombrePersonal, 
		V.IdCliente, V.NombreCliente, V.IdSubCliente, V.NombreSubCliente, V.IdPrograma, V.Programa, V.IdSubPrograma, V.SubPrograma, 
		V.IdBeneficiario, V.Beneficiario, V.DomicilioEntrega, V.FolioReferencia, V.NumReceta, V.FechaReceta, V.IdMedico, V.Medico, 
		V.Cedula, V.IdServicio, V.Servicio, V.IdArea, V.Area_Servicio, V.EsCorte, V.StatusVenta, V.SubTotal, V.Descuento, V.Iva, V.Total, 
		V.TipoDeVenta, V.NombreTipoDeVenta, V.IdClaveSSA_Sal, V.ClaveSSA_Base, V.ClaveSSA, V.ClaveSSA_Aux, V.DescripcionSal, 
		V.DescripcionClave, 
		V.DescripcionCortaClave + Cast('' as varchar(max)) as DescripcionCortaClave, 
		V.Renglon, 
		V.IdProducto, V.CodigoEAN, V.DescProducto, V.DescripcionCorta, V.Presentacion, V.ContenidoPaquete, V.UnidadDeSalida, V.TasaIva, 
		CEILING(V.Cantidad) as Cantidad, CEILING(V.CantidadCajas) as CantidadCajas, 
		V.Costo, V.Importe, 
		V.Importe as PrecioUnitario, 
		cast(0 as numeric(14,4)) as SubTotal_Producto, cast(0 as numeric(14,4)) as Iva_Producto, cast(0 as numeric(14,4)) as Importe_Producto, 
		V.PrecioLicitacion, V.IdMunicipio, V.Municipio, V.IdColonia, V.Colonia, V.Domicilio, cast('' as varchar(50)) as CodigoPostal, 
		
		IsNull(D.SubTotal_SinGravar, 0) as SubTotal_SinGravar_D, 
		IsNull(D.SubTotal_Gravado, 0) as SubTotal_Gravado_D, 
		IsNull(D.DescuentoCopago, 0) as DescuentoCopago_D, 
		IsNull(D.Importe_SinGravar, 0) as Importe_SinGravar_D, 
		IsNull(D.Importe_Gravado - D.Iva, 0) as Importe_Gravado_D, 
		IsNull(D.Iva, 0) as Iva_D, 
		---IsNull(D.Importe_SinGravar + D.Importe_Gravado, 0) as Importe_Neto_D , 
		IsNull(D.Importe_Neto, 0) as Importe_Neto_D , 	
		getdate() as FechaImpresion 
	Into #tmpDetalleFolioVenta 
	From vw_Impresion_Ventas_Credito V (Nolock) 
	Left Join INT_MA_Ventas_Importes D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.Folio = D.FolioVenta ) 
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and V.Folio = @Folio
	
		
	UPdate T 
	Set T.RFC = E.RFC
	From #tmpDetalleFolioVenta T (NoLock) 
	Inner Join CatEmpresas E (NoLock) On (T.IdEmpresa = E.IdEmpresa)
	
	Update T Set CodigoPostal = F.CodigoPostal 
	From #tmpDetalleFolioVenta T (NoLock)
	Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia ) 



	Update T Set 
		Empresa = 'AM/PM ASISTENCIA MEDICA', RFC = 'AAM070420SG4', 
		EmpDomicilio = 'RIO DEL SOCORRO NO. 8', EmpColonia = 'EL SOCORRO', EmpCodigoPostal = '54714', EmpEdoCiudad = 'CUAUTITLAN IZCALLI', Estado = 'MEXICO'	 
	From #tmpDetalleFolioVenta T (NoLock) 

--	sp_listacolumnas INT_MA_Ventas_Importes 


---------------------------- SALIDA FINAL 
	Select * 
	From #tmpDetalleFolioVenta (Nolock)
	
 
End 
Go--#SQL  