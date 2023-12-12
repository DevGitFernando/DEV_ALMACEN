--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Ticket_Credito_Custom' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Ticket_Credito_Custom 
Go--#SQL  

Create Proc	 spp_Rpt_Impresion_Ticket_Credito_Custom
(
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '14', @Folio varchar(8) = '23697' 
)    
With Encryption 
As 
Begin 
-- Set NoCount On 
Set DateFormat YMD 
Declare @sNA varchar(10)  

	Set @sNA = '' 
	
	Set @IdEmpresa = right('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000000000' + @IdFarmacia, 4) 
	Set @Folio = right('0000000000' + @Folio, 8) 


	Select 
		IdEmpresa, Empresa, EmpDomicilio, EmpColonia, EmpCodigoPostal, EmpEdoCiudad, IdEstado, Estado, ClaveRenapo, 
		IdFarmacia, Farmacia, EsAlmacen, EsUnidosis, Folio, FechaSistema, FechaRegistro, IdCaja, IdPersonal, NombrePersonal, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, CURP, 	
		IdTipoDerechoHabiencia, DerechoHabiencia, IdEstadoResidencia, EstadoDeResidencia, ClaveRENAPO__EstadoDeResidencia, 			
		DomicilioEntrega, FolioReferencia, NumReceta, FechaReceta, IdMedico, Medico, 
		Cedula, IdServicio, Servicio, IdArea, Area_Servicio, EsCorte, StatusVenta, SubTotal, Descuento, Iva, Total, 
		TipoDeVenta, NombreTipoDeVenta, IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionSal, 
		DescripcionClave, 
		(DescripcionCortaClave + Cast('' as varchar(max))) as DescripcionCortaClave, 
		Renglon, 
		IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, UnidadDeSalida, TasaIva, 
		CEILING(Cantidad) as Cantidad, CEILING(CantidadCajas) as CantidadCajas, 
		Costo, Importe, PrecioLicitacion, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio,
		getdate() as FechaImpresion  
	Into #tmpDetalleFolioVenta 
	From vw_Impresion_Ventas_Credito (Nolock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio

	

	-------------- Asignar datos de la configuracion por Cliente   
	If @IdEstado = '13' 
	Begin 
		Update T Set T.ClaveSSA_Aux = IsNull(M.Mascara, T.ClaveSSA), 
			T.DescripcionSal = IsNull(M.Descripcion, T.DescripcionClave), 
			T.DescripcionClave = IsNull(M.Descripcion, T.DescripcionClave), 
			T.DescripcionCortaClave = IsNull(M.DescripcionCorta, T.DescripcionCortaClave), 
			Presentacion = IsNull(M.Presentacion, '')  
		From #tmpDetalleFolioVenta T (Nolock)
		Left Join CFG_ClaveSSA_Mascara M (Nolock) 
			On ( T.IdEstado = M.IdEstado and M.IdCliente = T.IdCliente and M.IdSubCliente = T.IdSubCliente and T.IdClaveSSA_Sal = M.IdClaveSSA )  
	End 	
	


	-------------------------------- SALIDA FINAL 
	Select * 
	From #tmpDetalleFolioVenta (Nolock)
	Where Cantidad > 0 
	
 
End 
Go--#SQL  