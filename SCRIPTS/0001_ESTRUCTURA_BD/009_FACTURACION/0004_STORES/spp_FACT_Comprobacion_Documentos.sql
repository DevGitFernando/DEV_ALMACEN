-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Comprobacion_Documentos' and xType = 'P' )
    Drop Proc spp_FACT_Comprobacion_Documentos 
Go--#SQL

Create Proc spp_FACT_Comprobacion_Documentos
(
	@FolioRelacion varchar(6) = '',  
	@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '22', @IdFarmacia varchar(4) = '1',  
	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', 
	@ReferenciaDocumento varchar(100) = '', @NombreDocumento varchar(500) = '', @MD5_Documento varchar(100) = '', @Documento varchar(max) = '', 
	@Procesa_Venta int = 0, @Procesa_Consigna int = 0, 
	@Procesa_Producto int = 0, @Procesa_Servicio int = 0, 
	@DocumentoEnCajas int = 1, @TipoDeUnidades int = 1  
)
With Encryption 
As
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEmpresa = right('000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000000' + @IdFarmacia, 4) 

	Select @FolioRelacion = max(cast(FolioRelacion as int)) + 1 From FACT_Remisiones__RelacionDocumentos_Enc (NoLock) 
	Set @FolioRelacion = IsNull(@FolioRelacion, '1') 
	Set @FolioRelacion = right('000000000' + @FolioRelacion, 6) 	


	Insert Into FACT_Remisiones__RelacionDocumentos_Enc
	( 
		FolioRelacion, IdEmpresa, IdEstado, IdFarmacia, IdFuenteFinanciamiento, IdFinanciamiento, 
		ReferenciaDocumento, NombreDocumento, MD5_Documento, Documento, Procesa_Venta, Procesa_Consigna, Procesa_Producto, Procesa_Servicio, FechaRegistro, 
		EsDocumentoEnCajas, EsComprobada, TipoDeUnidades 
	) 
	select 
		@FolioRelacion, @IdEmpresa, @IdEstado, @IdFarmacia, @IdFuenteFinanciamiento, @IdFinanciamiento, 
		@ReferenciaDocumento, @NombreDocumento, @MD5_Documento, @Documento, @Procesa_Venta, @Procesa_Consigna, @Procesa_Producto, @Procesa_Servicio, 		
		getdate() As FechaRegistro, @DocumentoEnCajas, 0 as EsComprobada, @TipoDeUnidades as TipoDeUnidades 


	----------------- Salida final 
	Select @FolioRelacion as Folio, ('Información guardada satisfactoriamente con el folio ' + @FolioRelacion) as Mensaje  

End 
Go--#SQL  

-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Comprobacion_Documentos_Detalles' and xType = 'P' )
    Drop Proc spp_FACT_Comprobacion_Documentos_Detalles 
Go--#SQL

Create Proc spp_FACT_Comprobacion_Documentos_Detalles
(
	@FolioRelacion varchar(6) = '',  
	--@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '22', @IdFarmacia varchar(4) = '1',  
	@ClaveSSA varchar(20) = '', @ContenidoPaquete int = 0, 
	@Cantidad_A_Comprobar int = 0, @CantidadAgrupada_A_Comprobar int = 0
)
With Encryption 
As
Begin 
Set NoCount On 
Set DateFormat YMD 

	----Set @IdEmpresa = right('000000000' + @IdEmpresa, 3) 
	----Set @IdEstado = right('000000000' + @IdEstado, 2) 
	----Set @IdFarmacia = right('000000000' + @IdFarmacia, 4) 
	Set @FolioRelacion = right('000000000' + @FolioRelacion, 6) 	


	If Not Exists ( Select * From FACT_Remisiones__RelacionDocumentos (NoLock) Where FolioRelacion = @FolioRelacion and ClaveSSA = @ClaveSSA )  
	Begin 
		Insert Into FACT_Remisiones__RelacionDocumentos 
		( 
			FolioRelacion, ClaveSSA, ContenidoPaquete, Cantidad_A_Comprobar, Cantidad_Distribuida, CantidadAgrupada_A_Comprobar, CantidadAgrupada_Distribuida, EsClaveComprobada 		
		) 
		Select 
			@FolioRelacion, @ClaveSSA, @ContenidoPaquete, @Cantidad_A_Comprobar, 0 as Cantidad_Distribuida, @CantidadAgrupada_A_Comprobar, 0 as CantidadAgrupada_Distribuida, 0 as EsClaveComprobada
	End 


End 
Go--#SQL  



-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Comprobacion_Documentos__Validar' and xType = 'P' )
    Drop Proc spp_FACT_Comprobacion_Documentos__Validar 
Go--#SQL
  
Create Proc spp_FACT_Comprobacion_Documentos__Validar 
(
	@FolioRelacion varchar(6) = '',
	@IdEstado varchar(2) = '22', @IdFarmacia varchar(4) = '1',  
	@GUID varchar(100) = '72592001-beed-443c-b47a-2a75196bc0da', @DocumentoEnCajas int = 1, 
	@TipoProceso int = 0 

)
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEstado = right('000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000000' + @IdFarmacia, 4) 
	Set @FolioRelacion = right('000000000' + @FolioRelacion, 6) 	

	-------------------------------- Obtener los datos   
	Select D.ClaveSSA as ClaveSSA, C.Descripcion as DescripcionClave, 
		max(C.ContenidoPaquete) as ContenidoPaquete, 
		--sum(Cantidad) as Cantidad,
		cast((case when @DocumentoEnCajas = 1 Then sum(Cantidad_A_Comprobar * C.ContenidoPaquete) else sum(Cantidad_A_Comprobar) end) as numeric(14,4)) as Cantidad_A_Comprobar, 
		cast(sum(Cantidad_A_Comprobar) as numeric(14,4)) as Cantidad_A_Comprobar_Cajas, 
		cast(0 as numeric(14,4)) as Cantidad_Comprobada, 
		cast(0 as numeric(14,4)) as Cantidad_Comprobada_Cajas, 
		cast(0 as numeric(14,4)) as Cantidad_Por_Comprobar,
		0 as EsRegistrada, 
		cast('' as varchar(10)) as FolioRelacion 
	Into #tmp_ListadoClaves 
	From FACT_Remisiones__RelacionDocumentos__CargaMasiva D (NoLock) 
	Inner Join CatClavesSSA_Sales C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
	Where D.GUID = @GUID and @TipoProceso = 0 
	Group by 
		D.ClaveSSA, C.Descripcion  


	Insert Into #tmp_ListadoClaves
	Select D.ClaveSSA as ClaveSSA, C.Descripcion as DescripcionClave, 
		max(C.ContenidoPaquete) as ContenidoPaquete, 
		--sum(Cantidad) as Cantidad,
		cast((case when @DocumentoEnCajas = 1 Then sum(Cantidad_A_Comprobar * C.ContenidoPaquete) else sum(Cantidad_A_Comprobar) end) as numeric(14,4)) as Cantidad_A_Comprobar, 
		cast(sum(Cantidad_A_Comprobar) as numeric(14,4)) as Cantidad_A_Comprobar_Cajas, 
		cast(0 as numeric(14,4)) as Cantidad_Comprobada, 
		cast(0 as numeric(14,4)) as Cantidad_Comprobada_Cajas, 
		cast(0 as numeric(14,4)) as Cantidad_Por_Comprobar,
		0 as EsRegistrada, 
		cast('' as varchar(10)) as FolioRelacion  
	From FACT_Remisiones__RelacionDocumentos D (NoLock) 
	Inner Join CatClavesSSA_Sales C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
	Where D.FolioRelacion = @FolioRelacion and @TipoProceso = 1 
	Group by 
		D.ClaveSSA, C.Descripcion  
	-------------------------------- Obtener los datos   


	-------------------------------- Salida final  
	Select * 
	From #tmp_ListadoClaves 
	-------------------------------- Salida final  


End 
Go--#SQL


------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_INFO_Comprobacion_Documentos' and xType = 'P' ) 
   Drop Proc spp_FACT_INFO_Comprobacion_Documentos
Go--#SQL 

Create Proc spp_FACT_INFO_Comprobacion_Documentos
(
	--@FolioRelacion varchar(6) = '',    
	@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '1', @TipoDeUnidades int = 1  
)
With Encryption 
As  
Begin 
Set NoCount On 
Set DateFormat YMD 


	------------------------------------ Parámetros 
	Set @IdEmpresa = right('000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000000' + @IdFarmacia, 4) 
	------------------------------------ Parámetros 


	------------------------------------ Obtener información base  
	Select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.TipoDeUnidades, 
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, E.FolioRelacion, E.ReferenciaDocumento, 
		E.Procesa_Venta, E.Procesa_Consigna, E.Procesa_Producto, E.Procesa_Servicio, -- E.EsComprobada, 
		-- E.Serie, E.Folio, 
		sum(D.Cantidad_A_Comprobar) as Cantidad_A_Comprobar, sum(D.Cantidad_Distribuida) as Cantidad_Distribuida, 
		sum(D.Cantidad_A_Comprobar - D.Cantidad_Distribuida) as Cantidad_x_Comprobar 
	Into #tmp__ListaDeDocumentos 
	From FACT_Remisiones__RelacionDocumentos_Enc E (NoLock) 
	Inner Join FACT_Remisiones__RelacionDocumentos D (NoLock) On ( E.FolioRelacion = D.FolioRelacion  ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia -- and E.Serie = @Serie and E.Folio = @Folio 
		and D.Cantidad_A_Comprobar > D.Cantidad_Distribuida 
		and E.TipoDeUnidades = @TipoDeUnidades 
	Group by 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.TipoDeUnidades, 
		E.FechaRegistro, E.FolioRelacion, E.ReferenciaDocumento, 	
		E.Procesa_Venta, E.Procesa_Consigna, E.Procesa_Producto, E.Procesa_Servicio  -- E.EsComprobada,  
		-- E.Serie, E.Folio  
	------------------------------------ Obtener información base  



	-------------------------------------- Salida final   
	Select 
		E.FechaRegistro, E.FolioRelacion, E.ReferenciaDocumento, 
		
		E.TipoDeUnidades, 
		(case when E.TipoDeUnidades = 1 then 'ORDINARIAS' else 'DOSIS UNITARIA' end) as TipoDeUnidades_Desc, 

		E.Procesa_Venta, 
		(case when E.Procesa_Venta = 1 then 'SI' else 'NO' end) as Procesa_Venta_Desc, 
		E.Procesa_Consigna, -- , E.EsComprobada  
		(case when E.Procesa_Consigna = 1 then 'SI' else 'NO' end) as Procesa_Consigna_Desc,  

		E.Procesa_Producto, 
		(case when E.Procesa_Producto = 1 then 'SI' else 'NO' end) as Procesa_Producto_Desc, 
		E.Procesa_Servicio, -- , E.EsComprobada  
		(case when E.Procesa_Servicio = 1 then 'SI' else 'NO' end) as Procesa_Servicio_Desc 
	From #tmp__ListaDeDocumentos E (NoLock) 
	Group by 
		TipoDeUnidades, 
		E.FechaRegistro, E.FolioRelacion, E.ReferenciaDocumento, E.Procesa_Venta, E.Procesa_Consigna, E.Procesa_Producto, E.Procesa_Servicio
	Order by 
		E.FechaRegistro  


End 
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_INFO_Comprobacion_Documentos_Detalles' and xType = 'P' ) 
   Drop Proc spp_FACT_INFO_Comprobacion_Documentos_Detalles
Go--#SQL 

Create Proc spp_FACT_INFO_Comprobacion_Documentos_Detalles 
(
	--@FolioRelacion varchar(6) = '',    
	@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '1', 
	@Status_Distribucion int = 0  -- 0 ==> Todos | 1 ==> Pendientes | 2 ==> Completas 
)
With Encryption 
As  
Begin 
Set NoCount On 
Set DateFormat YMD 


	------------------------------------ Parámetros 
	Set @IdEmpresa = right('000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000000' + @IdFarmacia, 4) 
	------------------------------------ Parámetros 


	------------------------------------ Obtener información base  
	Select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, --E.Serie, E.Folio, 
		E.FolioRelacion as IdDocumento, 
		E.ReferenciaDocumento, E.NombreDocumento, 
		E.TipoDeUnidades, 
		(case when E.TipoDeUnidades = 1 then 'ORDINARIAS' else 'DOSIS UNITARIA' end) as TipoDeUnidades_Desc, 
		D.ClaveSSA, C.DescripcionClave, D.ContenidoPaquete, 
		(D.Cantidad_A_Comprobar) as Cantidad_A_Comprobar, 
		(D.Cantidad_Distribuida) as Cantidad_Distribuida, 
		(D.Cantidad_A_Comprobar - D.Cantidad_Distribuida) as Cantidad_x_Comprobar, 
		(case when @Status_Distribucion = 0 then 1 else 0 end) as Procesar
	Into #tmp__ListaDeDocumentos 
	From FACT_Remisiones__RelacionDocumentos_Enc E (NoLock) 
	Inner Join FACT_Remisiones__RelacionDocumentos D (NoLock) On ( E.FolioRelacion = D.FolioRelacion  ) 
	Inner Join vw_ClavesSSA_Sales C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
	Where 
		E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia -- and E.Serie = @Serie and E.Folio = @Folio 
		--and 
		--( case when @Status_Distribucion = 1 then D.Cantidad_Facturada > D.Cantidad_Distribuida else 0 end ) 
		--( case when @Status_Distribucion = 1 then (case when Cantidad_Facturada <> Cantidad_Distribuida then 1 else 0 end) else 0 end) 

--		select * from FACT_Remisiones__RelacionDocumentos_Enc 

	Update D Set Procesar = 1 
	From #tmp__ListaDeDocumentos D 
	Where @Status_Distribucion = 1 and Cantidad_x_Comprobar > 0 


	Update D Set Procesar = 1 
	From #tmp__ListaDeDocumentos D 
	Where @Status_Distribucion = 2 and Cantidad_x_Comprobar = 0 
	------------------------------------ Obtener información base  



	------------------------------------ Salida final   
	Select 
		--F.Serie, F.Folio, (F.Serie + '-' + cast(F.Folio as varchar(10))) as Serie_Folio, 
		--F.FechaRegistro, F.NombreReceptor, 
		--F.FuenteFinanciamiento, F.Financiamiento, 
		--F.TipoDocumentoDescripcion, F.TipoInsumoDescripcion, 

		'Tipo de Unidades' = L.TipoDeUnidades_Desc, 
		L.IdDocumento, 
		L.ReferenciaDocumento, L.NombreDocumento, 

		'Clave SSA' = L.ClaveSSA, 
		'Descripción' = L.DescripcionClave, 
		'Contenido paquete' = L.ContenidoPaquete, 
		'Cantidad a comprobar' = L.Cantidad_A_Comprobar, 
		'Cantidad comprobada' = L.Cantidad_Distribuida, 
		'Cantidad por comprobar' = L.Cantidad_x_Comprobar, 

		'Cantidad a comprobar cajas' = cast((L.Cantidad_A_Comprobar / L.ContenidoPaquete) as numeric(14,4)), 
		'Cantidad comprobada cajas' = cast((L.Cantidad_Distribuida / L.ContenidoPaquete) as numeric(14,4)), 
		'Cantidad por comprobar cajas' = cast((L.Cantidad_x_Comprobar  / L.ContenidoPaquete) as numeric(14,4)) 

	From #tmp__ListaDeDocumentos L (NoLock) 
	----Inner Join vw_FACT_CFD_DocumentosElectronicos F (NoLock) 
	----	On ( L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.Serie = F.Serie and L.Folio = F.Folio ) 
	Where Procesar = 1 
	--Order by 
	--	F.FechaRegistro  


	--		spp_FACT_INFO_Comprobacion_Documentos_Detalles

	Select 
		'Clave SSA' = L.ClaveSSA, 
		'Descripción' = L.DescripcionClave, 
		'Contenido paquete' = L.ContenidoPaquete, 
		'Cantidad a comprobar' = sum(L.Cantidad_A_Comprobar), 
		'Cantidad comprobada' = sum(L.Cantidad_Distribuida), 
		'Cantidad por comprobar' = sum(L.Cantidad_x_Comprobar), 

		'Cantidad a comprobar cajas' = cast(sum((L.Cantidad_A_Comprobar / L.ContenidoPaquete)) as numeric(14,4)), 
		'Cantidad comprobada cajas' = cast(sum((L.Cantidad_Distribuida / L.ContenidoPaquete)) as numeric(14,4)), 
		'Cantidad por comprobar cajas' = cast(sum((L.Cantidad_x_Comprobar  / L.ContenidoPaquete)) as numeric(14,4)) 

	From #tmp__ListaDeDocumentos L (NoLock) 
	----Inner Join vw_FACT_CFD_DocumentosElectronicos F (NoLock) 
	----	On ( L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.Serie = F.Serie and L.Folio = F.Folio ) 
	Where Procesar = 1 
	Group by 
		L.ClaveSSA, L.DescripcionClave, L.ContenidoPaquete 
	--Order by 
	--	F.FechaRegistro  


/* 

	select 
		P.ClaveSSA, P.DescripcionClave, P.ContenidoPaquete, 
		L.IdProducto, L.CodigoEAN, 
		sum(L.CantidadVendida) as Cantidad_Piezas,   
		cast(sum(L.CantidadVendida / ( P.ContenidoPaquete * 1.0 )) as numeric(14,4)) as Cantidad_Cajas  
	Into #tmp_Informacion 
	from VentasEnc E (nolock) 
	inner join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join VentasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta and D.IdProducto = L.IdProducto and L.CodigoEAN = L.CodigoEAN ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) 
	Where E.FechaSistema between '2023-02-01' and '2023-02-28' 
		and L.ClaveLote like '%*%'
	Group by 
		P.ClaveSSA, P.DescripcionClave, P.ContenidoPaquete, 
		L.IdProducto, L.CodigoEAN



	select 
		ClaveSSA, DescripcionClave, sum(Cantidad_Piezas) as Cantidad_Piezas, sum(Cantidad_Cajas) as Cantidad_Cajas 
	from #tmp_Informacion 
	Group by 
		ClaveSSA, DescripcionClave 


*/ 



End 
Go--#SQL 

