--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Ticket_Credito_Lotes_Custom_GetSegmentos' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Ticket_Credito_Lotes_Custom_GetSegmentos 
Go--#SQL  

Create Proc	 spp_Rpt_Impresion_Ticket_Credito_Lotes_Custom_GetSegmentos
(
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '4', @Folio varchar(20) = '00000002',  
	@TipoProceso_Desglosado int = 1
	 --Exec spp_Rpt_Impresion_Ticket_Credito_Lotes_Custom_GetSegmentos @IdEmpresa = '001', @IdEstado = '13', @IdFarmacia = '0004', @Folio = '00000002', @TipoProceso_Desglosado = 1
)    
With Encryption 
As 
Begin 
-- Set NoCount On  
Set DateFormat YMD 
Declare @sNA varchar(10),
		@sNombreArchivo Varchar(200)


	Set @IdEmpresa = right('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000000000' + @IdFarmacia, 4) 
	Set @sNombreArchivo = '';

	----Select @sNombreArchivo = left(Folio + '__' + IdFarmaciaRecibe + '__' + replace(FarmaciaRecibe, ' ', '_') + '__JS' + IdJurisdiccionRecibe, 195)
	--Select @IdFarmaciaRecibe = IdFarmaciaRecibe, @FarmaciaRecibe = FarmaciaRecibe, @IdJurisdiccionRecibe = IdJurisdiccionRecibe
	--From VentasEnc E (NoLock)
	--Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioVenta = @Folio 


	Select 
		cast(@sNombreArchivo as varchar(200)) as NombreArchivo, 
		1 as Segmentos, 
		1 as NumeroDeSegmento, 
		cast(P.IdTipoProducto as int) as IdTipoProducto, 
		P.EsControlado, P.EsAntibiotico, P.EsRefrigerado 
		, (case when @TipoProceso_Desglosado = 0 then 0 else (case when ClaveLote like '%*%' then 2 else 1 end) end ) as OrigenInsumo 
		, T.*  
	Into #tmpDetalleVenta
	From VentasDet_Lotes T (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( T.IdProducto = P.IdProducto and T.CodigoEAN = P.CodigoEAN )  
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioVenta = @Folio 


	If @TipoProceso_Desglosado = 1 
	Begin
		Update S Set Segmentos = 0, NumeroDeSegmento = 0 
		from #tmpDetalleVenta S 

		----------------------------- VENTA 
		Update S Set NumeroDeSegmento = 1   
		From #tmpDetalleVenta S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsControlado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 2   
		From #tmpDetalleVenta S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsAntibiotico = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 3   
		From #tmpDetalleVenta S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 4   
		From #tmpDetalleVenta S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 5   
		From #tmpDetalleVenta S 
		Where OrigenInsumo = 1 and IdTipoProducto = 1 and NumeroDeSegmento = 0 
		----------------------------- VENTA 


		----------------------------- CONSIGNA  
		Update S Set NumeroDeSegmento = 6   
		From #tmpDetalleVenta S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsControlado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 7   
		From #tmpDetalleVenta S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsAntibiotico = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 8   
		From #tmpDetalleVenta S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 9   
		From #tmpDetalleVenta S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 10    
		From #tmpDetalleVenta S 
		Where OrigenInsumo = 2 and IdTipoProducto = 1 and NumeroDeSegmento = 0 
		----------------------------- CONSIGNA  


		---------------------------------------------------------------- GENERAL   
		---------------- Limpiar asignacion, solo se solicito separar por VENTA y CONSIGNA 
		--Update S Set Segmentos = 0, NumeroDeSegmento = 0 
		--from #tmpDetalleVenta S 


		--Update S Set NumeroDeSegmento = 101     
		--From #tmpDetalleVenta S 
		--Where OrigenInsumo = 1 and NumeroDeSegmento = 0 

		--Update S Set NumeroDeSegmento = 102     
		--From #tmpDetalleVenta S 
		--Where OrigenInsumo = 2 and NumeroDeSegmento = 0 
		---------------------------------------------------------------- GENERAL   


		Update S Set Segmentos = ( Select count(distinct NumeroDeSegmento) From #tmpDetalleVenta ) 
		from #tmpDetalleVenta S 


	End  

	---			spp_Rpt_Impresion_Transferencias_Custom_GetSegmentos

		----------------------------- GENERAR EL NOMBRE DEL ARCHIVO DE SALIDA 
	Update T Set NombreArchivo = NombreArchivo + '_' + Cast(Segmentos As Varchar(10)) + '_' + Cast(NumeroDeSegmento As Varchar(10)) --+ '.pdf'  
	From #tmpDetalleVenta T (Nolock) 

	---------- SALIDA FINAL 
	Select
		ROW_NUMBER() OVER(ORDER BY NumeroDeSegmento ASC) AS Iteracion,
		NombreArchivo,
		Segmentos, 
		NumeroDeSegmento
	Into #tmpConcentradoVenta
	From #tmpDetalleVenta  
	Group by
		NombreArchivo,
		Segmentos, 
		NumeroDeSegmento 


	Select
		
		left(@Folio + '_' + RIGHT('000000' + cast(Iteracion as varchar(10)), 2), 195)
		As NombreArchivo,
		Segmentos, 
		NumeroDeSegmento
	From #tmpConcentradoVenta
	Order By Iteracion

End 
Go--#SQL 
--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Ticket_Credito_Lotes_Custom' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Ticket_Credito_Lotes_Custom 
Go--#SQL  
			      
Create Proc	 spp_Rpt_Impresion_Ticket_Credito_Lotes_Custom
(
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '4', @Folio varchar(8) = '2',
	@TipoProceso_Desglozado int = 1, @Iteracion int = 1, @Segmentos int = 2, @NumeroDeSegmento int = 5
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
		(DescripcionClave + Cast('' as varchar(7500))) as DescripcionClave, 
		(DescripcionCortaClave + Cast('' as varchar(7500))) as DescripcionCortaClave, 
		Renglon, 
		IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, IdSubFarmacia, SubFarmacia, 
		ClaveLote, EsConsignacion, CantidadLote, CantidadCajasLote, FechaCad, 
		UnidadDeSalida, TasaIva, Costo, Importe, PrecioLicitacion, IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, 
		IdSegmento, Segmento, EsAntibiotico, EsControlado, SegmentoTipoMed, RefObservaciones, 
		getdate() as FechaImpresion,

		1 as Segmentos, 
		1 as NumeroDeSegmento, 
		0 as OrigenInsumo, 
		0 as IdTipoProducto, 
		0 as EsRefrigerado

	Into #tmpDetalleFolioVenta 
	From vw_Impresion_Ventas_Credito_Lotes (Nolock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio


	Update T Set 
		IdTipoProducto = cast(P.IdTipoProducto as int), 
		EsControlado = P.EsControlado, EsAntibiotico = P.EsAntibiotico, EsRefrigerado = P.EsRefrigerado,  
		OrigenInsumo = (case when @TipoProceso_Desglozado = 0 then 0 else (case when ClaveLote like '%*%' then 2 else 1 end) end ) 
	From #tmpDetalleFolioVenta T (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( T.IdProducto = P.IdProducto and T.CodigoEAN = P.CodigoEAN )  

	


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
		Where M.STatus = 'A' 
	End 	
	

	If @IdEstado = '28' 
	Begin 
		Update T Set T.ClaveSSA_Aux = IsNull(M.Mascara, T.ClaveSSA), 
			T.DescripcionSal = IsNull(M.Descripcion, T.DescripcionClave), 
			T.DescripcionClave = IsNull(M.Descripcion, T.DescripcionClave), 
			T.DescripcionCortaClave = IsNull(M.DescripcionCorta, T.DescripcionCortaClave), 
			Presentacion = IsNull(M.Presentacion, '')  
		From #tmpDetalleFolioVenta T (Nolock)
		Left Join CFG_ClaveSSA_Mascara M (Nolock) 
			On ( T.IdEstado = M.IdEstado and M.IdCliente = T.IdCliente and M.IdSubCliente = T.IdSubCliente and T.IdClaveSSA_Sal = M.IdClaveSSA )
		Where M.STatus = 'A' 
	End


	If @TipoProceso_Desglozado = 1 
	Begin 

		Update S Set Segmentos = 0, NumeroDeSegmento = 0, Folio = S.Folio + '-' + RIGHT('000000' + cast(@Iteracion as varchar(10)), 2) 
		from #tmpDetalleFolioVenta S 

		---	  spp_Rpt_Impresion_Transferencias_Custom  

		----------------------------- VENTA 
		Update S Set NumeroDeSegmento = 1   
		From #tmpDetalleFolioVenta S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsControlado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 2   
		From #tmpDetalleFolioVenta S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsAntibiotico = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 3   
		From #tmpDetalleFolioVenta S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 4   
		From #tmpDetalleFolioVenta S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 5   
		From #tmpDetalleFolioVenta S 
		Where OrigenInsumo = 1 and IdTipoProducto = 1 and NumeroDeSegmento = 0 
		----------------------------- VENTA 


		----------------------------- CONSIGNA  
		Update S Set NumeroDeSegmento = 6   
		From #tmpDetalleFolioVenta S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsControlado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 7   
		From #tmpDetalleFolioVenta S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsAntibiotico = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 8   
		From #tmpDetalleFolioVenta S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 9   
		From #tmpDetalleFolioVenta S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 10    
		From #tmpDetalleFolioVenta S 
		Where OrigenInsumo = 2 and IdTipoProducto = 1 and NumeroDeSegmento = 0 
		----------------------------- CONSIGNA  


		---------------------------------------------------------------- GENERAL   
		---------------- Limpiar asignacion, solo se solicito separar por VENTA y CONSIGNA 
		--Update S Set Segmentos = 0, NumeroDeSegmento = 0 
		--from #tmpDetalleTransferencia S 


		--Update S Set NumeroDeSegmento = 101     
		--From #tmpDetalleTransferencia S 
		--Where OrigenInsumo = 1 and NumeroDeSegmento = 0 

		--Update S Set NumeroDeSegmento = 102     
		--From #tmpDetalleTransferencia S 
		--Where OrigenInsumo = 2 and NumeroDeSegmento = 0 
		---------------------------------------------------------------- GENERAL   


		----Update S Set Segmentos = ( Select count(distinct NumeroDeSegmento) From #tmpDetalleTransferencia ) 
		----from #tmpDetalleTransferencia S 


		Delete From #tmpDetalleFolioVenta Where NumeroDeSegmento <> @NumeroDeSegmento 

	End  


	-------------------------------- SALIDA FINAL 
	Select * 
	From #tmpDetalleFolioVenta (Nolock) 
	Where CantidadLote > 0 
	
 
End 
Go--#SQL  