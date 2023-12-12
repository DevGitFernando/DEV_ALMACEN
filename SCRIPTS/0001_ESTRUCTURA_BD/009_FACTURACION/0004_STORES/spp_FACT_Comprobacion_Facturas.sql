-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Comprobacion_Facturas' and xType = 'P' )
    Drop Proc spp_FACT_Comprobacion_Facturas 
Go--#SQL

Create Proc spp_FACT_Comprobacion_Facturas
(
	@FolioRelacion varchar(6) = '',  
	@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '22', @IdFarmacia varchar(4) = '1',  
	@Serie varchar(10) = 'PHJQRO' , @Folio int = 5, 
	@Serie_Relacionada varchar(10) = 'PHJQRO' , @Folio_Relacionado int = 5, 	
	@FacturaEnCajas int = 1, @TipoDeUnidades int = 0  
)
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEmpresa = right('000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000000' + @IdFarmacia, 4) 

	Select @FolioRelacion = max(cast(FolioRelacion as int)) + 1 From FACT_Remisiones__RelacionFacturas_Enc (NoLock) 
	Set @FolioRelacion = IsNull(@FolioRelacion, '1') 
	Set @FolioRelacion = right('000000000' + @FolioRelacion, 6) 	


	Insert Into FACT_Remisiones__RelacionFacturas_Enc
	( 
		FolioRelacion, IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Serie_Relacionada, Folio_Relacionado, IdFuenteFinanciamiento, IdFinanciamiento, FechaRegistro, EsFacturaEnCajas, EsComprobada, TipoDeUnidades 
	) 
	select 
		@FolioRelacion, @IdEmpresa, @IdEstado, @IdFarmacia, 
		@Serie, @Folio, 
		@Serie_Relacionada, @Folio_Relacionado, 
		'' as IdFuenteFinanciamiento, '' as IdFinanciamiento, getdate() As FechaRegistro, @FacturaEnCajas, 0 as EsComprobada, @TipoDeUnidades 


	Exec spp_FACT_Comprobacion_Facturas__Validar 
		@IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia,  
		@Serie = @Serie, @Folio = @Folio, 
		@Serie_Relacionada = @Serie_Relacionada, @Folio_Relacionado = @Folio_Relacionado, 
		@FacturaEnCajas = @FacturaEnCajas, 
		@FolioRelacion = @FolioRelacion, @Integrar = 1 


	----------------- Salida final 
	Select @FolioRelacion as Folio, ('Información guardada satisfactoriamente con el folio ' + @FolioRelacion) as Mensaje  

End 
Go--#SQL  


-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Comprobacion_Facturas__Validar' and xType = 'P' )
    Drop Proc spp_FACT_Comprobacion_Facturas__Validar 
Go--#SQL
  
Create Proc spp_FACT_Comprobacion_Facturas__Validar 
(
	@IdEstado varchar(2) = '22', @IdFarmacia varchar(4) = '1',  
	@Serie varchar(10) = 'PHJQRO' , @Folio int = 4, 
	@Serie_Relacionada varchar(10) = 'PHJQRO' , @Folio_Relacionado int = 4, 
	@FacturaEnCajas int = 1, 
	@FolioRelacion varchar(6) = '', @Integrar int = 0  
)
With Encryption 
As
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEstado = right('000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000000' + @IdFarmacia, 4) 

	-------------------------------- Obtener los datos   
	Select D.Clave as ClaveSSA, C.Descripcion as DescripcionClave, 
		max(C.ContenidoPaquete) as ContenidoPaquete, 
		--sum(Cantidad) as Cantidad,
		cast((case when @FacturaEnCajas = 1 Then sum(Cantidad * C.ContenidoPaquete) else sum(Cantidad) end) as numeric(14,4)) as Cantidad_Facturada, 
		cast(sum(Cantidad) as numeric(14,4)) as Cantidad_Facturada_Cajas, 
		cast(0 as numeric(14,4)) as Cantidad_Comprobada, 
		cast(0 as numeric(14,4)) as Cantidad_Comprobada_Cajas, 
		cast(0 as numeric(14,4)) as Cantidad_Por_Comprobar,
		0 as EsRegistrada, 
		cast('' as varchar(10)) as FolioRelacion 
	Into #tmp_ListadoClaves 
	From FACT_CFD_Documentos_Generados E (NoLock) 
	Inner Join FACT_CFD_Documentos_Generados_Detalles D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Serie = D.Serie and E.Folio = D.Folio ) 
	Inner Join CatClavesSSA_Sales C (NoLock) On ( D.Clave = C.ClaveSSA ) 
	Where E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia 
		and E.Serie = @Serie_Relacionada and E.Folio = @Folio_Relacionado and E.EsRelacionConRemisiones = 0 
	Group by 
		D.Clave, C.Descripcion  
	-------------------------------- Obtener los datos   

	--@Serie_Relacionada varchar(10) = 'PHJQRO' , @Folio_Relacionado int = 4, 


	If ( @Integrar = 1 and ( @FolioRelacion <> '' and @FolioRelacion <>'*' ) ) 
	Begin 
		Insert Into FACT_Remisiones__RelacionFacturas 
		( 
			FolioRelacion, ClaveSSA, ContenidoPaquete, Cantidad_Facturada, Cantidad_Distribuida, CantidadAgrupada_Facturada, CantidadAgrupada_Distribuida, EsClaveComprobada 
		) 
		Select @FolioRelacion as FolioRelacion, ClaveSSA, ContenidoPaquete, Cantidad_Facturada, 0 as Cantidad_Distribuida, Cantidad_Facturada_Cajas, 0 as CantidadAgrupada_Distribuida, 0 as EsClaveComprobada 
		From #tmp_ListadoClaves 
	End 

	Select @FolioRelacion = FolioRelacion 
	From FACT_Remisiones__RelacionFacturas_Enc (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Serie = @Serie and Folio = @Folio 


	Update R Set EsRegistrada = 1, FolioRelacion = F.FolioRelacion, 
		Cantidad_Por_Comprobar = R.Cantidad_Facturada - F.Cantidad_Distribuida, 
		Cantidad_Comprobada = F.Cantidad_Distribuida, Cantidad_Comprobada_Cajas = F.CantidadAgrupada_Distribuida 
	From #tmp_ListadoClaves R 
	Inner Join FACT_Remisiones__RelacionFacturas F (NoLock) On ( F.FolioRelacion = right('000000000' + @FolioRelacion, 6) and F.ClaveSSA = R.ClaveSSA ) 



/* 
	ClaveSSA varchar(20) Not Null, 
	ContenidoPaquete int Not Null Default 1, 
	Cantidad_Facturada numeric(14,4) Not Null Default 0, 	
	Cantidad_Distribuida numeric(14,4) Not Null Default 0,  	
	CantidadAgrupada_Facturada numeric(14,4) Not Null Default 0, 	
	CantidadAgrupada_Distribuida numeric(14,4) Not Null Default 0, 
	EsClaveComprobada bit not null Default 'false'
	
*/ 


	-------------------------------- Salida final  
	If @Integrar = 0 
	Begin 
		Select * 
		From #tmp_ListadoClaves 
	End 
	-------------------------------- Salida final  


End 
Go--#SQL


------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_INFO_Comprobacion_Facturas' and xType = 'P' ) 
   Drop Proc spp_FACT_INFO_Comprobacion_Facturas
Go--#SQL 

Create Proc spp_FACT_INFO_Comprobacion_Facturas
(
	--@FolioRelacion varchar(6) = '',    
	@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '22', @IdFarmacia varchar(4) = '1', @TipoDeUnidades int = 1   
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
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.Serie, E.Folio, 
		sum(D.Cantidad_Facturada) as Cantidad_Facturada, sum(D.Cantidad_Distribuida) as Cantidad_Distribuida, 
		sum(D.Cantidad_Facturada - D.Cantidad_Distribuida) as Cantidad_x_Comprobar 
	Into #tmp__ListaDeDocumentos 
	From FACT_Remisiones__RelacionFacturas_Enc E (NoLock) 
	Inner Join FACT_Remisiones__RelacionFacturas D (NoLock) On ( E.FolioRelacion = D.FolioRelacion  ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia -- and E.Serie = @Serie and E.Folio = @Folio 
		and D.Cantidad_Facturada > D.Cantidad_Distribuida 
		and E.TipoDeUnidades = @TipoDeUnidades 
	Group by 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, 
		E.Serie, E.Folio  
	------------------------------------ Obtener información base  



	------------------------------------ Salida final   
	Select 
		F.Serie, F.Folio, (F.Serie + '-' + cast(F.Folio as varchar(10))) as Serie_Folio, 
		F.FechaRegistro, F.NombreReceptor, 
		F.FuenteFinanciamiento, F.Financiamiento, 
		F.TipoDocumentoDescripcion, F.TipoInsumoDescripcion, 

		cast((case when F.TipoDocumento = 1 then 1 else 0 end) as bit) as Procesa_Producto, 
		cast((case when F.TipoDocumento = 2 then 1 else 0 end) as bit) as Procesa_Servicio, 
		cast((case when F.TipoInsumo = 2 then 1 else 0 end) as bit) as Procesa_Medicamento, 
		cast((case when F.TipoInsumo = 1 then 1 else 0 end) as bit) as Procesa_MaterialDeCuracion,  

		L.Cantidad_x_Comprobar 
	From #tmp__ListaDeDocumentos L (NoLock) 
	Inner Join vw_FACT_CFD_DocumentosElectronicos F (NoLock) 
		On ( L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.Serie = F.Serie and L.Folio = F.Folio ) 
	Order by 
		F.FechaRegistro  

End 
Go--#SQL 



------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_INFO_Comprobacion_Facturas_Detalles' and xType = 'P' ) 
   Drop Proc spp_FACT_INFO_Comprobacion_Facturas_Detalles
Go--#SQL 

Create Proc spp_FACT_INFO_Comprobacion_Facturas_Detalles 
(
	--@FolioRelacion varchar(6) = '',    
	@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '22', @IdFarmacia varchar(4) = '1', 
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
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.Serie, E.Folio, 
		D.ClaveSSA, C.DescripcionClave, D.ContenidoPaquete, 
		(D.Cantidad_Facturada) as Cantidad_Facturada, 
		(D.Cantidad_Distribuida) as Cantidad_Distribuida, 
		(D.Cantidad_Facturada - D.Cantidad_Distribuida) as Cantidad_x_Comprobar, 
		(case when @Status_Distribucion = 0 then 1 else 0 end) as Procesar
	Into #tmp__ListaDeDocumentos 
	From FACT_Remisiones__RelacionFacturas_Enc E (NoLock) 
	Inner Join FACT_Remisiones__RelacionFacturas D (NoLock) On ( E.FolioRelacion = D.FolioRelacion  ) 
	Inner Join vw_ClavesSSA_Sales C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
	Where 
		E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia -- and E.Serie = @Serie and E.Folio = @Folio 
		--and 
		--( case when @Status_Distribucion = 1 then D.Cantidad_Facturada > D.Cantidad_Distribuida else 0 end ) 
		--( case when @Status_Distribucion = 1 then (case when Cantidad_Facturada <> Cantidad_Distribuida then 1 else 0 end) else 0 end) 


	Update D Set Procesar = 1 
	From #tmp__ListaDeDocumentos D 
	Where @Status_Distribucion = 1 and Cantidad_x_Comprobar > 0 


	Update D Set Procesar = 1 
	From #tmp__ListaDeDocumentos D 
	Where @Status_Distribucion = 2 and Cantidad_x_Comprobar = 0 
	------------------------------------ Obtener información base  



	------------------------------------ Salida final   
	Select 
		F.Serie, F.Folio, (F.Serie + '-' + cast(F.Folio as varchar(10))) as Serie_Folio, 
		F.FechaRegistro, F.NombreReceptor, 
		F.FuenteFinanciamiento, F.Financiamiento, 
		F.TipoDocumentoDescripcion, F.TipoInsumoDescripcion, 
		L.ClaveSSA, L.DescripcionClave, 
		L.ContenidoPaquete, 
		L.Cantidad_Facturada, L.Cantidad_Distribuida, 
		L.Cantidad_x_Comprobar 
	From #tmp__ListaDeDocumentos L (NoLock) 
	Inner Join vw_FACT_CFD_DocumentosElectronicos F (NoLock) 
		On ( L.IdEmpresa = F.IdEmpresa and L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.Serie = F.Serie and L.Folio = F.Folio ) 
	Where Procesar = 1 
	Order by 
		F.FechaRegistro  

End 
Go--#SQL 

