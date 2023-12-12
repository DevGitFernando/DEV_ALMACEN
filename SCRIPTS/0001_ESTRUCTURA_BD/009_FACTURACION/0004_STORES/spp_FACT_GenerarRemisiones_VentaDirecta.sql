
	--Drop table #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	--Drop Table #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia 
	--Drop Table #CFG_Clientes_Claves 
	--Drop Table #CFG_Clientes_SubClientes_Claves 
	--Drop Table #CFG_ClavesSSA_Precios  
	--Drop Table #FACT_Remisiones_Detalles 
	--Drop Table #FACT_Remisiones_InformacionAdicional_Almacenes 
	--Drop Table #FACT_Remisiones_Concentrado  
	--Drop Table #FACT_Remisiones_Resumen 

/* 

Begin tran 

	Exec spp_FACT_GenerarRemisiones_VentaDirecta 
		 @IdEmpresa = '004', @IdEstado = '11', @IdFarmaciaGenera = '0001', @IdFarmacia = '4005', 
		 @FolioVenta = '00002121', 
		 @IdFuenteFinanciamiento = '0002', @IdFinanciamiento = '0001', 
		 @IdPersonalRemision = '0001', @FechaVenta = '2022-06-22', @GUID = 'r23r23rq', @Porcentaje = 2.00, 
		 @MostrarResultado = 1  

	 rollback tran 

*/ 


---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_GenerarRemisiones_VentaDirecta' and xType = 'P' ) 
    Drop Proc spp_FACT_GenerarRemisiones_VentaDirecta  
Go--#SQL 
 
Create Proc spp_FACT_GenerarRemisiones_VentaDirecta 
( 
	 @IdEmpresa Varchar(3)= '001', @IdEstado Varchar(2)= '28', @IdFarmaciaGenera Varchar(4) = '0001', @IdFarmacia Varchar(4) = '0103', 
	 @FolioVenta Varchar(8) = '00000064', 
	 @IdFuenteFinanciamiento Varchar(4) = '0002', @IdFinanciamiento Varchar(4) = '0001', 
	 @IdPersonalRemision Varchar(4) = '0001', @FechaVenta Varchar(10) = '2019-04-24', @GUID Varchar(50) = 'r23r23rq', @Porcentaje Numeric(14,4)= 12.00, 
	 @MostrarResultado int = 0 
) 
With Encryption		
As 
Begin 
Set NoCount On 

	Declare 
		@IdCliente  Varchar(4),
		@IdSubCliente Varchar(4),
		@IdPrograma Varchar(4),
		@IdSubPrograma Varchar(4),
		@FolioRemision Varchar(10), 
		@iFolioRemision int 


	Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000000' + @IdEstado, 2) 
	Set @IdFarmaciaGenera = right('000000000000' + @IdFarmaciaGenera, 4) 
	Set @IdFarmacia = right('000000000000' + @IdFarmacia, 4) 
	Set @FolioVenta = right('000000000000' + @FolioVenta, 8)

		
	Select @FolioRemision = cast( (max(FolioRemision) + 1) as varchar)  
	From FACT_Remisiones (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera

	Set @FolioRemision = IsNull(@FolioRemision, '1')
	Set @FolioRemision = right(replicate('0', 10) + @FolioRemision, 10)
	


	------------------------------- Generar los folios de remision 
	Set @iFolioRemision = 0 
	Select @iFolioRemision = cast( (max(FolioRemision) + 1) as varchar) 
	From FACT_Remisiones (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado 
	Set @iFolioRemision = IsNull(@iFolioRemision, 1) 



	--Select * From FACT_Remisiones (NoLock)


	Select  @IdCliente = IdCliente, @IdSubCliente = IdSubCliente, @IdPrograma = IdPrograma, @IdSubPrograma = IdSubPrograma
	From VentasEnc
	--Where IdCliente = '0012' And IdSubCliente = '0001' 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta

		--------FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA
	Select 
		@IdFuenteFinanciamiento As IdFuenteFinanciamiento, @IdFinanciamiento As IdFinanciamiento,
		ClaveSSA, 100.0000 As PorcParticipacion, 999999 As CantidadPresupuestadaPiezas, 999999 As CantidadPresupuestada,
		0 As CantidadAsignada, 0 As CantidadRestante, 'A' As Status, 0 As Actualizado,'' As SAT_ClaveProducto_Servicio,'' As  SAT_UnidadDeMedida,
		'' As Referencia_04, 0.0000 As CostoBase, 0.0000 As Porcentaje_01, 0.0000 As Porcentaje_02
	Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA 
	From VentasDet D (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN )
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 
	Group by ClaveSSA 


	--Select *--distinct IdFuenteFinanciamiento, IdFinanciamiento
	Update F Set Status = 'A'
	From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA F
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA T
		On ( F.IdFuenteFinanciamiento = T.IdFuenteFinanciamiento And F.IdFinanciamiento = T.IdFinanciamiento And F.ClaveSSA = T.ClaveSSA )


	Insert Into FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA
	(	
		IdFuenteFinanciamiento, IdFinanciamiento, ClaveSSA, PorcParticipacion, CantidadPresupuestadaPiezas, CantidadPresupuestada, CantidadAsignada, CantidadRestante,
		Status, Actualizado, SAT_ClaveProducto_Servicio, SAT_UnidadDeMedida, Referencia_04, CostoBase, Porcentaje_01, Porcentaje_02 )
	Select 
		distinct 
		T.IdFuenteFinanciamiento, T.IdFinanciamiento, T.ClaveSSA, T.PorcParticipacion, T.CantidadPresupuestadaPiezas, T.CantidadPresupuestada, T.CantidadAsignada, T.CantidadRestante,
		T.Status, T.Actualizado, T.SAT_ClaveProducto_Servicio, T.SAT_UnidadDeMedida, T.Referencia_04, T.CostoBase, T.Porcentaje_01, T.Porcentaje_02
	From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA T
	Left Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA F
		On ( F.IdFuenteFinanciamiento = T.IdFuenteFinanciamiento And F.IdFinanciamiento = T.IdFinanciamiento And F.ClaveSSA = T.ClaveSSA )
	Where F.ClaveSSA Is Null 
	-----------------FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA


	--------FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia  
	Select
		@IdFuenteFinanciamiento As IdFuenteFinanciamiento, @IdFinanciamiento As IdFinanciamiento, IdEstado, IdFarmacia,
		ClaveSSA, 100.0000 As PorcParticipacion, 999999 As CantidadPresupuestadaPiezas, 999999 As CantidadPresupuestada,
		0 As CantidadAsignada, 0 As CantidadRestante, 'A' As Status, 0 As Actualizado,'' As SAT_ClaveProducto_Servicio,'' As  SAT_UnidadDeMedida,
		'' As Referencia_04, 0.0000 As CostoBase, 0.0000 As Porcentaje_01, 0.0000 As Porcentaje_02,
		'' As Referencia_01, '' As Referencia_02, '' As Referencia_03, '' As Referencia_05
	Into #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia 
	From VentasDet D (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN )
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 
		And CantidadVendida > 0 
	Group by 
		IdEstado, IdFarmacia, ClaveSSA 


	--Select *--distinct IdFuenteFinanciamiento, IdFinanciamiento
	Update F Set Status = 'A'
	From FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia F
	Inner Join #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia T
		On ( F.IdFuenteFinanciamiento = T.IdFuenteFinanciamiento And F.IdFinanciamiento = T.IdFinanciamiento And F.ClaveSSA = T.ClaveSSA And F.IdEstado = T.IdEstado And F.IdFarmacia = T.IdFarmacia )


	Insert Into FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia
	(
		IdFuenteFinanciamiento, IdFinanciamiento, IdEstado, IdFarmacia, ClaveSSA, PorcParticipacion,
		 CantidadPresupuestadaPiezas, CantidadPresupuestada, CantidadAsignada, CantidadRestante,
		 Status, Actualizado, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05)
	Select 
		T.IdFuenteFinanciamiento, T.IdFinanciamiento, T.IdEstado, T.IdFarmacia, T.ClaveSSA, T.PorcParticipacion,
		T.CantidadPresupuestadaPiezas, T.CantidadPresupuestada, T.CantidadAsignada, T.CantidadRestante,
		T.Status, T.Actualizado, T.Referencia_01, T.Referencia_02, T.Referencia_03, T.Referencia_04, T.Referencia_05
	From #FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia T
	Left Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia F
		On ( F.IdFuenteFinanciamiento = T.IdFuenteFinanciamiento And F.IdFinanciamiento = T.IdFinanciamiento And F.ClaveSSA = T.ClaveSSA And F.IdEstado = T.IdEstado And F.IdFarmacia = T.IdFarmacia )
	Where F.ClaveSSA Is Null

	---------FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia	

	----CFG_Clientes_Claves
	Select Distinct @IdCliente As IdCliente, IdClaveSSA_Sal, 'A' As Status, 0 As Actualizado
	Into #CFG_Clientes_Claves
	From VentasDet D (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
		And CantidadVendida > 0 


		--Select *--distinct IdFuenteFinanciamiento, IdFinanciamiento
	Update F Set Status = 'A' 
	From CFG_Clientes_Claves F
	Inner Join #CFG_Clientes_Claves T On ( F.IdCliente = T.IdCliente And F.IdClaveSSA_Sal = T.IdClaveSSA_Sal )

	Insert Into CFG_Clientes_Claves ( IdCliente, IdClaveSSA_Sal, Status, Actualizado )
	Select T.IdCliente, T.IdClaveSSA_Sal, T.Status, T.Actualizado
	From #CFG_Clientes_Claves T
	Left Join CFG_Clientes_Claves F On ( F.IdCliente = T.IdCliente And F.IdClaveSSA_Sal = T.IdClaveSSA_Sal )
	Where F.IdClaveSSA_Sal Is Null 
		and Not Exists 
		( 
			Select * 
			From CFG_Clientes_Claves C (NoLock) 
			Where T.IdCliente = C.IdCliente and T.IdClaveSSA_Sal = C.IdClaveSSA_Sal 
		) 
	----------CFG_Clientes_Claves


	----CFG_Clientes_SubClientes_Claves
	Select Distinct @IdCliente As IdCliente, @IdSubCliente As IdSubCliente, IdClaveSSA_Sal, 'A' As Status, 0 As Actualizado
	Into #CFG_Clientes_SubClientes_Claves
	From VentasDet D (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.CodigoEAN = P.CodigoEAN)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
		And CantidadVendida > 0 


		--Select *--distinct IdFuenteFinanciamiento, IdFinanciamiento
	Update F Set Status = 'A'
	From CFG_Clientes_SubClientes_Claves F 
	Inner Join #CFG_Clientes_SubClientes_Claves T On ( F.IdCliente = T.IdCliente And F.IdSubCliente = T.IdSubCliente And F.IdClaveSSA_Sal = T.IdClaveSSA_Sal )

	Insert Into CFG_Clientes_SubClientes_Claves ( IdCliente, IdSubCliente, IdClaveSSA_Sal, Status, Actualizado )
	Select T.IdCliente, T.IdSubCliente, T.IdClaveSSA_Sal, T.Status, T.Actualizado 
	From #CFG_Clientes_SubClientes_Claves T 
	Left Join CFG_Clientes_SubClientes_Claves F 
		On ( F.IdCliente = T.IdCliente And F.IdSubCliente = T.IdSubCliente And F.IdClaveSSA_Sal = T.IdClaveSSA_Sal )
	Where F.IdClaveSSA_Sal Is Null  
		and Not Exists 
		( 
			Select * 
			From CFG_Clientes_SubClientes_Claves C (NoLock) 
			Where T.IdCliente = C.IdCliente and T.IdSubCliente  = C.IdSubCliente and T.IdClaveSSA_Sal = C.IdClaveSSA_Sal 
		) 
	----------CFG_Clientes_SubClientes_Claves



	----CFG_ClavesSSA_Precios
	Select 
		distinct 
		IdEstado, @IdCliente As IdCliente, @IdSubCliente As IdSubCliente, IdClaveSSA_Sal, 0.0000 As Precio, 'A' As Status, 0 As Actualizado,
		1.0000 As Factor, P.ContenidoPaquete as ContenidoPaquete_Licitado, P.IdPresentacion As IdPresentacion_Licitado, 0 As Dispensacion_CajasCompletas,
		'' As SAT_ClaveDeProducto_Servicio, '' As SAT_UnidadDeMedida
	Into #CFG_ClavesSSA_Precios
	From VentasDet D (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN ) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
		And CantidadVendida > 0 

	
	Update F Set Status = 'A' 
	From CFG_ClavesSSA_Precios F
	Inner Join #CFG_ClavesSSA_Precios T On ( F.IdCliente = T.IdCliente And F.IdSubCliente = T.IdSubCliente And F.IdClaveSSA_Sal = T.IdClaveSSA_Sal )

	Insert Into CFG_ClavesSSA_Precios
	( 
		IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio, Status, Actualizado,
		Factor, ContenidoPaquete_Licitado, IdPresentacion_Licitado, Dispensacion_CajasCompletas, SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida 
	) 
	Select
		T.IdEstado, T.IdCliente, T.IdSubCliente, T.IdClaveSSA_Sal, T.Precio, T.Status, T.Actualizado,
		T.Factor, T.ContenidoPaquete_Licitado, T.IdPresentacion_Licitado, T.Dispensacion_CajasCompletas, T.SAT_ClaveDeProducto_Servicio, T.SAT_UnidadDeMedida
	From #CFG_ClavesSSA_Precios T
	Left Join CFG_ClavesSSA_Precios F On ( F.IdCliente = T.IdCliente And F.IdSubCliente = T.IdSubCliente And F.IdClaveSSA_Sal = T.IdClaveSSA_Sal )
	Where F.IdClaveSSA_Sal Is Null
	----------CFG_ClavesSSA_Precios


	------------------------------------------------------- Preparar la estructura para la remisiones 
	Select --D.CostoUnitario,
		D.IdEmpresa, D.IdEstado, @IdFarmaciaGenera As IdFarmaciaGenera, D.IdFarmacia, D.IdSubFarmacia, D.FolioVenta, 
		@FolioRemision As FolioRemision, @IdFuenteFinanciamiento As IdFuenteFinanciamiento, @IdFinanciamiento As IdFinanciamiento,
		@IdPrograma As IdPrograma, @IdSubPrograma As IdSubPrograma, P.ClaveSSA, P.IdProducto, P.CodigoEAN, ClaveLote, SKU, 
		D.CostoUnitario As PrecioLicitado, D.CostoUnitario As  PrecioLicitadoUnitario,
		cast(P.ContenidoPaquete * 1.0 as numeric(14,4)) as ContenidoPaquete,  
		cast((D.CantidadVendida / ( P.ContenidoPaquete * 1.0 ) ) as numeric(14,4)) As Cantidad_Agrupada, D.CantidadVendida As Cantidad, P.TasaIva,
		cast(0 As numeric(20, 10)) As SubTotalSinGrabar, cast(0 As numeric(20, 10)) As SubTotalGrabado, cast(0 As numeric(20, 10)) As Iva, cast(0 As numeric(20, 10)) As Importe,
		'' As Referencia_01, 1 As Partida, 
		TipoDeClave, TipoDeClaveDescripcion, 
		EsControlado, EsAntibiotico, EsRefrigerado 
	Into #FACT_Remisiones_Detalles
	From VentasDet_Lotes D (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN )
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
		And CantidadVendida > 0 


	
	------------------------ Generar folios de remisiones 
	--Select @Remisiones_x_Farmacia as Remisiones_x_Farmacia  
	Select 
		IDENTITY(int, 0, 1) as Consecutivo, 
		@iFolioRemision as FolioInicial, 
		cast('' as varchar(20)) as FolioRemision, 
		IdFuenteFinanciamiento, IdFinanciamiento, 
		TipoDeClave, TipoDeClaveDescripcion, 
		EsControlado, EsAntibiotico, EsRefrigerado          
	Into #tmp_Remisiones 
	From #FACT_Remisiones_Detalles 
	Group By 
		IdFuenteFinanciamiento, IdFinanciamiento, 
		TipoDeClave, TipoDeClaveDescripcion 
		, EsControlado
		, EsAntibiotico 
		, EsRefrigerado   
	Order By 
		--IdFarmacia, 
		IdFuenteFinanciamiento, IdFinanciamiento  
		, EsControlado desc 
		, EsAntibiotico desc 
		, EsRefrigerado desc         


	--------------- Remisiones generadas  
	Update R Set FolioRemision = right('0000000000000000' + cast(FolioInicial + Consecutivo as varchar(10)), 10) 
	From #tmp_Remisiones R 


	Update L Set FolioRemision = R.FolioRemision 
	From #FACT_Remisiones_Detalles L (NoLock) 
	Inner Join #tmp_Remisiones R (NoLock) 
		On 
			( 
				L.IdFuenteFinanciamiento = R.IdFuenteFinanciamiento and L.IdFinanciamiento = R.IdFinanciamiento  
				--and L.IdFarmacia = R.IdFarmacia 
				--and L.GrupoProceso = R.GrupoProceso and L.IdGrupoDeRemisiones = R.IdGrupoDeRemisiones and L.GrupoDispensacion = R.GrupoDispensacion
				--and L.PartidaGeneral = R.PartidaGeneral and L.Referencia_05 = R.Referencia_05 
				and L.TipoDeClave = R.TipoDeClave and L.TipoDeClaveDescripcion = R.TipoDeClaveDescripcion  
				and L.EsControlado = R.EsControlado and L.EsAntibiotico = R.EsAntibiotico and L.EsRefrigerado = R.EsRefrigerado 
			) 
	--------------- Remisiones generadas  




	--select * from #FACT_Remisiones_Detalles 	order by claveSSA  


	Update D Set PrecioLicitadoUnitario = ( Select avg(C.PrecioLicitado) From #FACT_Remisiones_Detalles C Where C.CodigoEAN = D.CodigoEAN ) 
	From #FACT_Remisiones_Detalles D (NoLock) 
	
	Update D Set PrecioLicitadoUnitario = ( Select avg(C.PrecioLicitado) From #FACT_Remisiones_Detalles C Where C.ClaveSSA = D.ClaveSSA ) 
	From #FACT_Remisiones_Detalles D (NoLock) 
	Where 
		ClaveSSA in 
		( 
			Select ClaveSSA 
			From #FACT_Remisiones_Detalles R (NoLock) 
			Group by ClaveSSA 
			Having count(*) > 1 
		)  


	Update D Set PrecioLicitado = PrecioLicitadoUnitario * ContenidoPaquete 
	From #FACT_Remisiones_Detalles D (NoLock) 

	Update D Set 	
		PrecioLicitado = dbo.fg_PRCS_Redondear((PrecioLicitado * (1 + ( @Porcentaje / 100.00 ))), 2, 0),
		PrecioLicitadoUnitario = dbo.fg_PRCS_Redondear((PrecioLicitadoUnitario * (1 + ( @Porcentaje / 100.00 ))), 2, 0) 
	From #FACT_Remisiones_Detalles D (NoLock) 

	Update D Set 	
		SubTotalSinGrabar = dbo.fg_PRCS_Redondear(PrecioLicitado * Cantidad_Agrupada, 2, 0) 
	From #FACT_Remisiones_Detalles D (NoLock) 
	where TasaIVA = 0 

	Update D Set 	
		SubTotalGrabado = dbo.fg_PRCS_Redondear(PrecioLicitado * Cantidad_Agrupada, 2, 0)
	From #FACT_Remisiones_Detalles D (NoLock) 
	where TasaIVA > 0

	Update D Set 
			Iva = dbo.fg_PRCS_Redondear((SubTotalGrabado * ( TasaIva / 100.00 )), 2, 0)				  
	From #FACT_Remisiones_Detalles D (NoLock) 
	where TasaIVA > 0 

	Update D Set Iva = round(Iva, 3, 1) 
	From #FACT_Remisiones_Detalles D (NoLock) 
	where TasaIVA > 0 

	Update D Set Iva = round(Iva, 2, 1) 
	From #FACT_Remisiones_Detalles D (NoLock) 
	where TasaIVA > 0

		------- SubTotal, Importe 
	Update D Set
		--SubTotal = (D.SubTotalSinGrabar + D.SubTotalGrabado),
		Importe = (D.SubTotalSinGrabar + D.SubTotalGrabado + D.IVA) 
	From #FACT_Remisiones_Detalles D (NoLock) 


	--  Select * From #FACT_Remisiones_Detalles
	

	--------------------------------------------------- GENERAR CONCENTRADOS 

	Select --D.CostoUnitario,
		D.IdEmpresa, D.IdEstado, @IdFarmaciaGenera As IdFarmaciaGenera, F.FolioRemision, P.TipoDeBeneficiario, D.IdBeneficiario As Beneficiario, P.Nombre As NombreBeneficiario
	Into #FACT_Remisiones_InformacionAdicional_Almacenes
	From VentasInformacionAdicional D (NoLock) 
	Inner Join #FACT_Remisiones_Detalles F (NoLock) 
		On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia and D.FolioVenta = F.FolioVenta )  
	Inner Join vw_Beneficiarios  P (NoLock) 
		On ( D.IdEstado = P.IdEstado And D.IdFarmacia = P.IdFarmacia And D.IdBeneficiario = P.IdBeneficiario And P.IdCliente = @IdCliente And P.IdSubCliente = @IdSubCliente ) 
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado And D.IdFarmacia = @IdFarmacia And D.FolioVenta = @FolioVenta
	Group by 
		D.IdEmpresa, D.IdEstado, IdFarmaciaGenera, F.FolioRemision, P.TipoDeBeneficiario, D.IdBeneficiario, P.Nombre	


	--Select * From #FACT_Remisiones_InformacionAdicional_Almacenes

	Select
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma,
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, PrecioLicitadoUnitario,
		Sum(Cantidad_Agrupada) As Cantidad_Agrupada, Sum(Cantidad) As Cantidad, TasaIva,
		Sum(SubTotalSinGrabar) As SubTotalSinGrabar, Sum(SubTotalGrabado) As SubTotalGrabado, Sum(Iva) As Iva, Sum(Importe) As Importe 
	Into #FACT_Remisiones_Concentrado 
	From #FACT_Remisiones_Detalles
	Group By 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, 
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, PrecioLicitadoUnitario, TasaIva 

	--Select * From #FACT_Remisiones_Concentrado


	Select
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, ClaveSSA,
		PrecioLicitado, PrecioLicitadoUnitario,
		Sum(Cantidad) As Cantidad, Sum(Cantidad_Agrupada) As Cantidad_Agrupada, TasaIva,
		Sum(SubTotalSinGrabar) As SubTotalSinGrabar, Sum(SubTotalGrabado) As SubTotalGrabado, Sum(Iva) As Iva, Sum(Importe) As Importe 
	Into #FACT_Remisiones_Resumen 
	From #FACT_Remisiones_Concentrado 
	Group By IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, ClaveSSA,
		PrecioLicitado, PrecioLicitadoUnitario, TasaIva

	--Select * From #FACT_Remisiones_Resumen


	Select
		IdEmpresa, IdEstado, IdFarmaciaGenera, GetDate() As FechaRemision, GetDate() As FechaValidacion, FolioRemision,
		4 As TipoDeRemision, 
		--0 As TipoInsumo, 
		TipoDeClave as TipoInsumo, 
		0 As OrigenInsumo, 0 As EsFacturada, 1 As EsFacturable, 0 As EsExcedente, @IdPersonalRemision As IdPersonalRemision,
		@IdPersonalRemision As IdPersonalValida, IdFuenteFinanciamiento, IdFinanciamiento,
		Sum(SubTotalSinGrabar) As SubTotalSinGrabar, Sum(SubTotalGrabado) As SubTotalGrabado, Sum(Iva) As Iva, Sum(Importe) As Total, '' As Observaciones,
		'A' As Status, 0 As Actualizado, @FechaVenta As FechaInicial, @FechaVenta As FechaFinal, @GUID As GUID,
		0 As EsRelacionFacturaPrevia, '' As Serie, '' As Folio, 0 As EsRelacionMontos, 0 As TipoDeDispensacion, 0 As EsDeVales, 0 As PartidaGeneral
	Into #FACT_Remisiones 
	From #FACT_Remisiones_Detalles 
	Group By IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, TipoDeClave 

	--------------------------------------------------- GENERAR CONCENTRADOS 
	

	--SELECT * FROM #FACT_Remisiones_Resumen 
	 

	
	--Select * From VentasDet_Lotes Where IdFarmacia = '0003' And CostoUnitario = 0 FolioVenta = '00006044' Order by FolioVenta Desc

	--------------------------------------------------- INSERTAR LA INFORMACION EN LAS TABLAS DE REMISIONES 
	Insert Into FACT_Remisiones  
	(	
		IdEmpresa, IdEstado, IdFarmaciaGenera, FechaRemision, FechaValidacion, FolioRemision, TipoDeRemision, TipoInsumo, OrigenInsumo, EsFacturada, EsFacturable, EsExcedente,
		IdPersonalRemision, IdPersonalValida, IdFuenteFinanciamiento, IdFinanciamiento, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, Status, Actualizado,
		FechaInicial, FechaFinal, GUID, EsRelacionFacturaPrevia, Serie, Folio, EsRelacionMontos, TipoDeDispensacion, EsDeVales, PartidaGeneral 
	)  
	Select 
		IdEmpresa, IdEstado, IdFarmaciaGenera, FechaRemision, FechaValidacion, FolioRemision, TipoDeRemision, TipoInsumo, OrigenInsumo, EsFacturada, EsFacturable, EsExcedente,
		IdPersonalRemision, IdPersonalValida, IdFuenteFinanciamiento, IdFinanciamiento, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, Status, Actualizado,
		FechaInicial, FechaFinal, GUID, EsRelacionFacturaPrevia, Serie, Folio, EsRelacionMontos, TipoDeDispensacion, EsDeVales, PartidaGeneral
	From #FACT_Remisiones (NoLock)



	Insert Into FACT_Remisiones_Detalles
	(	
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma,
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, PrecioLicitadoUnitario, Cantidad_Agrupada, Cantidad, TasaIva,
		SubTotalSinGrabar, SubTotalGrabado, Iva, Importe, Referencia_01, Partida, SKU
	) 
	Select
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioVenta, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma,
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, PrecioLicitadoUnitario, Cantidad_Agrupada, Cantidad, TasaIva,
		SubTotalSinGrabar, SubTotalGrabado, Iva, Importe, Referencia_01, Partida, SKU 
	From #FACT_Remisiones_Detalles (NoLock)



	Insert Into FACT_Remisiones_InformacionAdicional
	( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, Observaciones, Referencia_01, Referencia_02, Referencia_03, Referencia_04, Referencia_05 )
	Select
		Distinct IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, '' As Observaciones, '' As Referencia_01, '' As Referencia_02, '' As Referencia_03, '' As Referencia_04, '' As Referencia_05
	From #FACT_Remisiones_Resumen(NoLock)


	Insert Into FACT_Remisiones_InformacionAdicional_Almacenes
	( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, TipoDeBeneficiario, Beneficiario, NombreBeneficiario )
	Select 
		IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, TipoDeBeneficiario, Beneficiario, NombreBeneficiario
	From #FACT_Remisiones_InformacionAdicional_Almacenes (NoLock)
	


	Insert Into FACT_Remisiones_Concentrado
	( 
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma,
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, PrecioLicitadoUnitario, Cantidad_Agrupada, Cantidad, TasaIva,
		SubTotalSinGrabar, SubTotalGrabado, Iva, Importe 
	) 
	Select
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma,
		ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, PrecioLicitadoUnitario, Cantidad_Agrupada, Cantidad, TasaIva,
		SubTotalSinGrabar, SubTotalGrabado, Iva, Importe 
	From #FACT_Remisiones_Concentrado (NoLock) 



	Insert Into FACT_Remisiones_Resumen 
	(
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, ClaveSSA,
		PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe 
	) 
	Select
		IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, IdPrograma, IdSubPrograma, ClaveSSA,
		PrecioLicitado, PrecioLicitadoUnitario, Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe
	From #FACT_Remisiones_Resumen (NoLock) 


	Insert Into FACT_Remisiones___GUID (IdEmpresa, IdEstado, IdFarmaciaGenera, GUID, HostName, FechaRegistro)
	Select @IdEmpresa As IdEmpresa, @IdEstado As IdEstado, @IdFarmaciaGenera As IdFarmaciaGenera, @GUID As GUID, Host_Name() HostName, getDate() FechaRegistro



	Update D Set CantidadRemision_Insumo = CantidadVendida
	From VentasDet_Lotes D (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 
	
	--------------------------------------------------- INSERTAR LA INFORMACION EN LAS TABLAS DE REMISIONES 

	--Exec sp_ListaColumnas FACT_Remisiones___GUID  

	If @MostrarResultado = 1 
	Begin 
		Select * From #FACT_Remisiones_Detalles
	End 

End 
Go--#SQL 

