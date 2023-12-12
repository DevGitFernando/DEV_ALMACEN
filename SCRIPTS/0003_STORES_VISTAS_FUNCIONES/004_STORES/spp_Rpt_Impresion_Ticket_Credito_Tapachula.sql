--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Ticket_Credito_Tapachula' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Ticket_Credito_Tapachula 
Go--#SQL  

Create Proc	 spp_Rpt_Impresion_Ticket_Credito_Tapachula
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '07', @IdFarmacia varchar(4) = '0011', @Folio varchar(8) = '00000010' 
)    
With Encryption 
As 
Begin 
-- Set NoCount On 
Set DateFormat YMD 
Declare @sNA varchar(10)  

	Set @sNA = '' 
	
	
	Select 
		IdEmpresa, Empresa, EmpDomicilio, EmpColonia, EmpCodigoPostal, EmpEdoCiudad, IdEstado, Estado, ClaveRenapo, 
		IdFarmacia, Farmacia, EsAlmacen, Folio, FechaSistema, FechaRegistro, IdCaja, IdPersonal, NombrePersonal, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, DomicilioEntrega, FolioReferencia, NumReceta, FechaReceta, IdMedico, Medico, 
		Cedula, IdServicio, Servicio, IdArea, Area_Servicio, EsCorte, StatusVenta, SubTotal, Descuento, Iva, Total, 
		TipoDeVenta, NombreTipoDeVenta, IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionSal, 
		DescripcionClave, 
		DescripcionCortaClave + Cast('' as varchar(max)) as DescripcionCortaClave, 
		Renglon, 
		IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, UnidadDeSalida, TasaIva, 
		CEILING(Cantidad) as Cantidad, CEILING(CantidadCajas) as CantidadCajas, 
		Costo, Importe, PrecioLicitacion, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio,
		getdate() as FechaImpresion  
	Into #tmpDetalleFolioVenta
	From vw_Impresion_Ventas_Credito (Nolock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio
	
	
	-------------- Asignar datos de la configuracion por Cliente   
	Update T Set T.ClaveSSA_Aux = IsNull(M.Mascara, T.ClaveSSA_Aux), T.DescripcionCortaClave = IsNull(M.DescripcionCorta, T.DescripcionCortaClave)
	From #tmpDetalleFolioVenta T (Nolock)
	Left Join CFG_ClaveSSA_Mascara M (Nolock) 
		On ( T.IdEstado = M.IdEstado and M.IdCliente = T.IdCliente and M.IdSubCliente = T.IdSubCliente and T.IdClaveSSA_Sal = M.IdClaveSSA )
		
	
	Select * 
	From #tmpDetalleFolioVenta (Nolock)
	
 
End 
Go--#SQL  