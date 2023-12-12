--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Transferencias_Custom_GetSegmentos' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Transferencias_Custom_GetSegmentos 
Go--#SQL  

Create Proc	 spp_Rpt_Impresion_Transferencias_Custom_GetSegmentos
(
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '4', @Folio varchar(20) = 'TS00000015',  
	@TipoProceso_Desglozado int = 1
)    
With Encryption 
As 
Begin 
-- Set NoCount On  
Set DateFormat YMD 
Declare @sNA varchar(10),
		@sNombreArchivo Varchar(200),
		@IdFarmaciaRecibe Varchar(4),
		@FarmaciaRecibe varchar(500),
		@IdJurisdiccionRecibe Varchar(4)


	Set @IdEmpresa = right('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000000000' + @IdFarmacia, 4) 
	Set @sNombreArchivo = '';

	--Select @sNombreArchivo = left(Folio + '__' + IdFarmaciaRecibe + '__' + replace(FarmaciaRecibe, ' ', '_') + '__JS' + IdJurisdiccionRecibe, 195)
	Select @IdFarmaciaRecibe = IdFarmaciaRecibe, @FarmaciaRecibe = FarmaciaRecibe, @IdJurisdiccionRecibe = IdJurisdiccionRecibe
	From vw_TransferenciasEnc E (NoLock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 


	Select 
		cast(@sNombreArchivo as varchar(200)) as NombreArchivo, 
		1 as Segmentos, 
		1 as NumeroDeSegmento, 
		cast(P.IdTipoProducto as int) as IdTipoProducto, 
		P.EsControlado, P.EsAntibiotico, P.EsRefrigerado 
		, (case when @TipoProceso_Desglozado = 0 then 0 else (case when ClaveLote like '%*%' then 2 else 1 end) end ) as OrigenInsumo 
		, T.*  
	Into #tmpDetalleTransferencia 
	From TransferenciasDet_Lotes T (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( T.IdProducto = P.IdProducto and T.CodigoEAN = P.CodigoEAN )  
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioTransferencia = @Folio 


	If @TipoProceso_Desglozado = 1 
	Begin
		Update S Set Segmentos = 0, NumeroDeSegmento = 0 
		from #tmpDetalleTransferencia S 

		----------------------------- VENTA 
		Update S Set NumeroDeSegmento = 1   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsControlado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 2   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsAntibiotico = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 3   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 4   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 5   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and IdTipoProducto = 1 and NumeroDeSegmento = 0 
		----------------------------- VENTA 


		----------------------------- CONSIGNA  
		Update S Set NumeroDeSegmento = 6   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsControlado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 7   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsAntibiotico = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 8   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 9   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 10    
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and IdTipoProducto = 1 and NumeroDeSegmento = 0 
		----------------------------- CONSIGNA  


		---------------------------------------------------------------- GENERAL   
		---------------- Limpiar asignacion, solo se solicito separar por VENTA y CONSIGNA 
		Update S Set Segmentos = 0, NumeroDeSegmento = 0 
		from #tmpDetalleTransferencia S 


		Update S Set NumeroDeSegmento = 101     
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 102     
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and NumeroDeSegmento = 0 
		---------------------------------------------------------------- GENERAL   


		Update S Set Segmentos = ( Select count(distinct NumeroDeSegmento) From #tmpDetalleTransferencia ) 
		from #tmpDetalleTransferencia S 


	End  

	---			spp_Rpt_Impresion_Transferencias_Custom_GetSegmentos

	----------------------------- GENERAR EL NOMBRE DEL ARCHIVO DE SALIDA 
	Update T Set NombreArchivo = NombreArchivo + '_' + Cast(Segmentos As Varchar(10)) + '_' + Cast(NumeroDeSegmento As Varchar(10)) --+ '.pdf'  
	From #tmpDetalleTransferencia T (Nolock) 

	Set @FarmaciaRecibe = replace(replace(@FarmaciaRecibe, char(34), ''), char(39), '')  
	----------------------------- GENERAR EL NOMBRE DEL ARCHIVO DE SALIDA 

	---------- SALIDA FINAL 
	Select
		ROW_NUMBER() OVER(ORDER BY NumeroDeSegmento ASC) AS Iteracion,
		NombreArchivo,
		Segmentos, 
		NumeroDeSegmento
	Into #tmpConcentradoTransferencia
	From #tmpDetalleTransferencia  
	Group by
		NombreArchivo,
		Segmentos, 
		NumeroDeSegmento 


	Select
		
		left(
		@Folio + '_' +
		RIGHT('000000' + cast(Iteracion as varchar(10)), 2) + '__' + @IdFarmaciaRecibe + '__' + replace(@FarmaciaRecibe, ' ', '_') + '__JS' + @IdJurisdiccionRecibe
		, 195)
		As NombreArchivo,
		Segmentos, 
		NumeroDeSegmento
	From #tmpConcentradoTransferencia
	Order By Iteracion

End 
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Impresion_Transferencias_Custom' and xType = 'P' ) 
   Drop Proc spp_Rpt_Impresion_Transferencias_Custom 
Go--#SQL  

Create Proc	 spp_Rpt_Impresion_Transferencias_Custom
(
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '22', @IdFarmacia varchar(4) = '3', @Folio varchar(20) = 'TS00000082', 
	@IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '13', 
	@TipoProceso_Desglozado int = 0, @Iteracion int = 1, @Segmentos int = 1, @NumeroDeSegmento int = 1 
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
	--Set @Folio = right('0000000000' + @Folio, 8) 



	Select 
		right('000000' + @IdCliente, 4) as IdCliente, right('000000' + @IdSubCliente, 4) as IdSubCliente, 
		IdEmpresa, Empresa, IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, 
		IdSubFarmaciaEnvia, SubFarmaciaEnvia, IdJurisdiccion, Jurisdiccion, Municipio, Colonia, Domicilio, CodigoPostal, 
		
		cast(Folio as varchar(20)) as Folio, TipoTransferencia, TransferenciaAplicada, FechaSistema, FechaReg, IdPersonal, NombrePersonal, Observaciones, 
		IdEstadoRecibe, EstadoRecibe, ClaveRenapoRecibe, 
		IdFarmaciaRecibe, FarmaciaRecibe, IdSubFarmaciaRecibe, SubFarmaciaRecibe, IdJurisdiccionRecibe, JurisdiccionRecibe, MunicipioRecibe, ColoniaRecibe, DomicilioRecibe, CodigoPostalRecibe, SubTotal, Iva, Total, Status, 
				
		IdProducto, DescripcionProducto, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, CodigoEAN, IdSegmento, Segmento, 
		
		1 as Segmentos, 
		1 as NumeroDeSegmento, 

		0 as OrigenInsumo, 
		0 as IdTipoProducto,
		0 as EsControlado, 	 
		0 as EsAntibiotico, 
		0 as EsRefrigerado, 
		--cast('' as varchar(200)) as NombreArchivo, 

		SegmentoTipoMed, ContenidoPaquete, 
		ContenidoPaquete as ContenidoPaquete_Licitado, 
		ClaveLote, CantidadLote, CantidadCajaLote, 
		CantidadCajaLote as CantidadCajaLote_Licitacion, 
		
		FechaCaducidad, FechaRegistro 
	Into #tmpDetalleTransferencia  
	From vw_Impresion_Transferencias (Nolock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 

	
--	
	Update T Set 
		IdTipoProducto = cast(P.IdTipoProducto as int), 
		EsControlado = P.EsControlado, EsAntibiotico = P.EsAntibiotico, EsRefrigerado = P.EsRefrigerado,  
		OrigenInsumo = (case when @TipoProceso_Desglozado = 0 then 0 else (case when ClaveLote like '%*%' then 2 else 1 end) end ) 
	From #tmpDetalleTransferencia T (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( T.IdProducto = P.IdProducto and T.CodigoEAN = P.CodigoEAN )  


	------------------------------ APLICAR CONTENIDO PAQUETE EN BASE A LICITACION  
	If @IdCliente <> '' and @IdSubCliente <> '' 
	Begin 
		
		Update T Set ContenidoPaquete_Licitado = C.ContenidoPaquete_Licitado 
		From #tmpDetalleTransferencia T (NoLock) 
		Inner Join vw_Claves_Precios_Asignados C (NoLock) On ( T.IdEstadoRecibe = C.IdEstado and T.IdCliente = C.IdCliente and T.IdSubCliente = C.IdSubCliente and T.ClaveSSA = C.ClaveSSA ) 
		--Where OrigenInsumo = 1 


		Update T Set CantidadCajaLote_Licitacion = (CantidadLote / ContenidoPaquete_Licitado)
		From #tmpDetalleTransferencia T (NoLock) 
		--Where OrigenInsumo = 1 

	End   
	------------------------------ APLICAR CONTENIDO PAQUETE EN BASE A LICITACION  



	If @TipoProceso_Desglozado = 1 
	Begin 

		Update S Set Segmentos = 0, NumeroDeSegmento = 0, Folio = S.Folio + '-' + RIGHT('000000' + cast(@Iteracion as varchar(10)), 2) 
		from #tmpDetalleTransferencia S 

		---	  spp_Rpt_Impresion_Transferencias_Custom  

		----------------------------- VENTA 
		Update S Set NumeroDeSegmento = 1   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsControlado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 2   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsAntibiotico = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 3   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and EsRefrigerado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 4   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and IdTipoProducto = 2 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 5   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and IdTipoProducto = 1 and NumeroDeSegmento = 0 
		----------------------------- VENTA 


		----------------------------- CONSIGNA  
		Update S Set NumeroDeSegmento = 6   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsControlado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 7   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and EsAntibiotico = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 8   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and EsRefrigerado = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 9   
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and IdTipoProducto = 2 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 10    
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and IdTipoProducto = 1 and NumeroDeSegmento = 0 
		----------------------------- CONSIGNA  


		---------------------------------------------------------------- GENERAL   
		---------------- Limpiar asignacion, solo se solicito separar por VENTA y CONSIGNA 
		Update S Set Segmentos = 0, NumeroDeSegmento = 0 
		from #tmpDetalleTransferencia S 


		Update S Set NumeroDeSegmento = 101     
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 1 and NumeroDeSegmento = 0 

		Update S Set NumeroDeSegmento = 102     
		From #tmpDetalleTransferencia S 
		Where OrigenInsumo = 2 and NumeroDeSegmento = 0 
		---------------------------------------------------------------- GENERAL   


		----Update S Set Segmentos = ( Select count(distinct NumeroDeSegmento) From #tmpDetalleTransferencia ) 
		----from #tmpDetalleTransferencia S 


		Delete From #tmpDetalleTransferencia Where NumeroDeSegmento <> @NumeroDeSegmento 

	End  




		---	  spp_Rpt_Impresion_Transferencias_Custom  @NumeroDeSegmento = 9 

	-------------------------------- SALIDA FINAL 
	Select * 
	From #tmpDetalleTransferencia (Nolock) 
	Where CantidadLote > 0 
		-- and ContenidoPaquete <> ContenidoPaquete_Licitado
	
 
End 
Go--#SQL  


