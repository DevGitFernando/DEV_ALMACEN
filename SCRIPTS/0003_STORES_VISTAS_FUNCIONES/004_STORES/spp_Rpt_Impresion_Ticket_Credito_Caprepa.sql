--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Ticket_Credito_Caprepa' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Ticket_Credito_Caprepa 
Go--#SQL  

Create Proc	 spp_Rpt_Impresion_Ticket_Credito_Caprepa 
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0016', @Folio varchar(8) = '00102569' 
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
		Cast(DescripcionCortaClave as varchar(max)) as DescripcionCortaClave, 
		Renglon, 
		IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, UnidadDeSalida, TasaIva, 
		CEILING(Cantidad) as Cantidad, CEILING(CantidadCajas) as CantidadCajas, 
		Costo, Importe, PrecioLicitacion, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio,
		getdate() as FechaImpresion  
	Into #tmpDetalleFolioVenta
	From vw_Impresion_Ventas_Credito (Nolock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio
	
	
	-------------- Asignar datos de la configuracion por Cliente   
	------Update T Set T.ClaveSSA_Aux = IsNull(M.Mascara, T.ClaveSSA_Aux), T.DescripcionCortaClave = IsNull(M.DescripcionCorta, T.DescripcionCortaClave)
	------From #tmpDetalleFolioVenta T (Nolock)
	------Left Join CFG_ClaveSSA_Mascara M (Nolock) 
	------	On ( T.IdEstado = M.IdEstado and M.IdCliente = T.IdCliente and M.IdSubCliente = T.IdSubCliente and T.IdClaveSSA_Sal = M.IdClaveSSA )
		
	Update T Set T.ClaveSSA_Aux = IsNull(M.Mascara, T.ClaveSSA_Aux), 
		T.DescripcionCortaClave = IsNull(M.DescripcionCorta, T.DescripcionCortaClave)
	From #tmpDetalleFolioVenta T (Nolock) 
	Inner Join CFG_ClaveSSA_Mascara M (Nolock) 
		On ( T.IdEstado = M.IdEstado and M.IdCliente = T.IdCliente and M.IdSubCliente = T.IdSubCliente 
			and T.IdClaveSSA_Sal = M.IdClaveSSA and M.Status = 'A' )		
		
		
	------Update T  
	------Set T.ClaveSSA_Aux = IsNull(M.ClaveSSA_Aux, T.ClaveSSA_Aux), T.ClaveSSA = IsNull(M.ClaveSSA, T.ClaveSSA),
	------	T.DescripcionCortaClave = IsNull(M.Descripcion, T.DescripcionCortaClave)
	------From #tmpDetalleFolioVenta T (Nolock)
	------Left Join vw_Relacion_ClavesSSA_Claves M (Nolock) 
	------	On ( T.IdEstado = M.IdEstado and T.IdClaveSSA_Sal = M.IdClaveSSA_Relacionada  )
		
	
	Select * 
	From #tmpDetalleFolioVenta (Nolock)
	
 
End 
Go--#SQL  