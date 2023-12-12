------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_FACT_Remisiones_Validar__PolizaBeneficiario' and xType = 'P' )
    Drop Proc spp_FACT_Remisiones_Validar__PolizaBeneficiario 
Go--#SQL 

--	Exec  spp_FACT_Remisiones_Validar__PolizaBeneficiario 1, 11, 1, 1, 0, 1 
  
Create Proc spp_FACT_Remisiones_Validar__PolizaBeneficiario 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', 
	@IdFarmaciaGenera varchar(4) = '0001', @FolioRemision varchar(10) = '0000000001',
	@EsConsulta bit = 1, @Devolver_A_Repositorio bit = 0   
)
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@IdFuenteFinanciamiento varchar(8), 
	@IdFinanciamiento varchar(8), 
	@iLargoMenor int, 
	@iLargoMayor int  	

Declare 
	@SubTotalSinGrabar numeric(18,4), 
	@SubTotalGrabado numeric(18,4), 
	@Iva numeric(18,4), 
	@Total numeric(18,4), 
	@TipoDeRemision int 


	Set @SubTotalSinGrabar = 0  
	Set @SubTotalGrabado = 0  
	Set @Iva = 0 
	Set @Total = 0 

--------------------- Formatear parametros 
	Select @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Select @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	Select @IdFarmaciaGenera = dbo.fg_FormatearCadena(@IdFarmaciaGenera, '0', 4) 
	Select @FolioRemision = dbo.fg_FormatearCadena(@FolioRemision, '0', 10) 
--------------------- Formatear parametros 
		

---		spp_FACT_Remisiones_Validar__PolizaBeneficiario  	

----------------- Datos para validación de polizas 
	Select @IdFuenteFinanciamiento = IdFuenteFinanciamiento, @IdFinanciamiento = IdFinanciamiento, @TipoDeRemision = TipoDeRemision 
	From FACT_Remisiones E(NoLock) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision 

	Select @iLargoMenor = LongitudMinimaPoliza, @iLargoMayor = LongitudMaximaPoliza 
	From FACT_Fuentes_De_Financiamiento_Detalles 
	Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento and IdFinanciamiento = @IdFinanciamiento 
	
	
	-- select top 1 * from FACT_Fuentes_De_Financiamiento_Detalles
	
	-- Select @IdFuenteFinanciamiento, @IdFinanciamiento 
	Set @iLargoMenor = IsNull(@iLargoMenor, 0) 
	Set @iLargoMayor = IsNull(@iLargoMayor, 0) 	
----------------- Datos para validación de polizas 


	---------------------------------------
	-- Se obtienen los datos principales --
	---------------------------------------
	Select	
		D.IdEmpresa, D.IdEstado, E.IdFarmaciaGenera, D.IdFarmacia, Cast('' as varchar(100)) as Farmacia, 
		D.FolioVenta, getdate() as FechaVenta, E.FolioRemision, 
		space(4) as IdCliente, space(4) as IdSubCliente, space(4) as IdPrograma, space(4) as IdSubPrograma, 
		space(10) as IdBeneficiario, space(200) as Beneficiario, 
		space(20) as Referencia, space(20) as ReferenciaAux, 0 as EsValida    
	into #tmpFolios 	
	From FACT_Remisiones E(NoLock)
	Inner Join FACT_Remisiones_Detalles D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmaciaGenera = D.IdFarmaciaGenera And E.FolioRemision = D.FolioRemision )
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision
	
--------- Datos de Farmacias 
	Update C Set Farmacia = F.NombreFarmacia
	From #tmpFolios C (NoLock)
	Inner Join CatFarmacias F (NoLock) On( C.IdEstado = F.IdEstado And C.IdFarmacia = F.IdFarmacia ) 
--------- Datos de Farmacias 

	
--------- Datos de Beneficiarios 
	Update C Set FechaVenta = D.FechaRegistro, 
		IdCliente = D.IdCliente, IdSubCliente = D.IdSubCliente, IdPrograma = D.IdPrograma, IdSubPrograma = D.IdSubPrograma 
	From #tmpFolios C (NoLock)
	Inner Join VentasEnc D (NoLock) 
		On ( C.IdEmpresa = D.IdEmpresa And C.IdEstado = D.IdEstado And C.IdFarmacia = D.IdFarmacia And C.FolioVenta = D.FolioVenta )		

	Update C Set IdBeneficiario = D.IdBeneficiario
	From #tmpFolios C (NoLock)
	Inner Join VentasInformacionAdicional D (NoLock) 
		On ( C.IdEmpresa = D.IdEmpresa And C.IdEstado = D.IdEstado And C.IdFarmacia = D.IdFarmacia And C.FolioVenta = D.FolioVenta )		
	
	Update C Set 
		Beneficiario = ltrim(rtrim((ltrim(rtrim(ApPaterno)) + ' ' + ltrim(rtrim(ApMaterno)) + ' ' + ltrim(rtrim(Nombre)) ))), 
		Referencia = D.FolioReferencia, 
		ReferenciaAux = replace(replace(ltrim(rtrim(D.FolioReferencia)), '-', ''), '.', '')  
	From #tmpFolios C (NoLock)
	Inner Join CatBeneficiarios D (NoLock) 
		On ( C.IdEstado = D.IdEstado And C.IdFarmacia = D.IdFarmacia 
			 And C.IdCliente = D.IdCliente And C.IdSubCliente = D.IdSubCliente And C.IdBeneficiario = D.IdBeneficiario )			
--------- Datos de Beneficiarios 	


--------------------------- REVISAR VALIDEZ DE LA POLIZA 
	Update C Set EsValida = 1 
	From #tmpFolios C 
	Where ( len(Referencia) Between @iLargoMenor and @iLargoMayor ) --and IsNumeric(ReferenciaAux) = 1 

	--	select top 1 * from CatBeneficiarios 
	

---		spp_FACT_Remisiones_Validar__PolizaBeneficiario  	
	
	
------------ Salida Final 
	If @EsConsulta = 1 
	Begin 
		Select distinct 
			'Id Farmacia' = IdFarmacia, Farmacia, 'Folio de venta' = FolioVenta, 
			'Fecha de venta' = convert(varchar(10), FechaVenta, 120),  
			'Clave de beneficiario' = IdBeneficiario, Beneficiario, 'Folio referencia' = Referencia	-- *, len(Referencia), @iLargoMenor, @iLargoMayor 	
		From #tmpFolios 
		Where EsValida = 0 
	End 
	
	
	If @Devolver_A_Repositorio = 1 
	Begin 
		--- Base a modificar 
		Select distinct 
			D.IdEmpresa, D.IdEstado, D.IdFarmaciaGenera, D.IdFarmacia, D.IdSubFarmacia, 
			D.FolioVenta, D.FolioRemision, D.IdFuenteFinanciamiento, D.IdFinanciamiento, 
			D.IdPrograma, D.IdSubPrograma, D.ClaveSSA, D.IdProducto, D.CodigoEAN, D.ClaveLote, 
			D.PrecioLicitado, D.Cantidad, D.TasaIva, D.SubTotalSinGrabar, D.SubTotalGrabado, D.Iva, D.Importe, 
			0 as Renglon  
		Into #tmp_InformacionEnviarRepositorio 	
		From #tmpFolios E 
		Inner Join FACT_Remisiones_Detalles D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado 
				 And E.IdFarmaciaGenera = D.IdFarmaciaGenera and E.IdFarmacia = D.IdFarmacia 
				 And E.FolioRemision = D.FolioRemision and E.FolioVenta = D.FolioVenta )	
		Where EsValida = 0  	
		
--		select * from #tmp_InformacionEnviarRepositorio 
		
		Update E Set Renglon = D.Renglon
		From #tmp_InformacionEnviarRepositorio E 
		Inner Join VentasDet D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia 
				 And E.FolioVenta = D.FolioVenta and E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN )	
		 		 		
---------- Esta parte ya no se hace, ya que la informacion solo se marca o desmarca para facturar ---------
		-------------- Reintegrar información  
----		Insert Into VentasDet_Lotes 
----		(
----			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, 
----			IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, 
----			Cant_Vendida, Cant_Devuelta, CantidadVendida, Status, Actualizado		 
----		) 		 
----		Select 
----			D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.IdSubFarmacia, D.FolioVenta, 
----			D.IdProducto, D.CodigoEAN, D.ClaveLote, D.Renglon, 
----			(case when D.ClaveLote like '%*%' then 1 else 0 end) as EsConsignacion, 
----			0 as Cant_Vendida, 0 as Cant_Devuelta, Cantidad as CantidadVendida, 'A' as Status, 1 as Actualizado  
----		From #tmp_InformacionEnviarRepositorio D 
		-------------- Reintegrar información 
----------------------------------------------------------------------------------------------------------------------------------------------

		--------Select L.* 
		--------From VentasDet_Lotes L (NoLock) 
		--------Inner Join #tmp_InformacionEnviarRepositorio V (NoLock) 
		--------	On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
		--------		And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote ) 
		--------Where L.EnRemision_Insumo = 1 or L.EnRemision_Admon = 1  
				

		----  Se Desmarca la informacion referente a los folios que contienen errores ----- 
		If @TipoDeRemision = 1 
			Begin 
				Update L Set EnRemision_Insumo = 0, RemisionFinalizada = 0 
				From VentasDet_Lotes L (NoLock) 
				Inner Join #tmp_InformacionEnviarRepositorio V (NoLock) 
					On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
						And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote ) 
				Where L.EnRemision_Insumo = 1 
			End 
		Else 
			Begin 
				Update L Set EnRemision_Admon = 0, RemisionFinalizada = 0 
				From VentasDet_Lotes L(NoLock) 
				Inner Join #tmp_InformacionEnviarRepositorio V(NoLock) 
					On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
						And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote ) 
				Where L.EnRemision_Admon = 1 
			End 		
			
		------Select L.* 
		------From VentasDet_Lotes L (NoLock) 
		------Inner Join #tmp_InformacionEnviarRepositorio V (NoLock) 
		------	On ( L.IdEmpresa = V.IdEmpresa And L.IdEstado = V.IdEstado And L.IdFarmacia = V.IdFarmacia And L.IdSubFarmacia = V.IdSubFarmacia 
		------		And L.FolioVenta = V.FolioVenta And L.IdProducto = V.IdProducto And L.CodigoEAN = V.CodigoEAN And L.ClaveLote = V.ClaveLote ) 					
-----------------------------------------------------------------------------------------------------------------------------------------------
		
---		spp_FACT_Remisiones_Validar__PolizaBeneficiario  			
		
		-------------- Borrar información de Detalles   
		Delete FACT_Remisiones_Detalles 
		--Select * 
		From FACT_Remisiones_Detalles E 		  
		Inner Join #tmpFolios D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado 
				 And E.IdFarmaciaGenera = D.IdFarmaciaGenera and E.IdFarmacia = D.IdFarmacia 
				 And E.FolioRemision = D.FolioRemision and E.FolioVenta = D.FolioVenta )	
		Where EsValida = 0 
		-------------- Borrar información de Detalles    
			 
			 
		-------------- Se genera de nuevo el detallado de la Remision  
		Delete From FACT_Remisiones_Concentrado 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 
		
		Delete From FACT_Remisiones_Resumen 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision 		
		
		Select 
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, 
			IdFuenteFinanciamiento, IdFinanciamiento, 
			IdPrograma,	IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado as PrecioClave, 
			Cantidad as CantidadVendida, TasaIva, 
			SubTotalSinGrabar as SubTotal_SinGrabar, SubTotalGrabado as SubTotal_Grabado, Iva, Importe 
		Into #tmpRemisionModificada 	
		From FACT_Remisiones_Detalles E (NoLock) 
		Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision		
		
		
		-- select '' as P, * 		from #tmpRemisionModificada 				
		Select IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, 
			sum(SubTotal_SinGrabar) as SubTotalSinGrabar, sum(SubTotal_Grabado) as SubTotalGrabado, 
			sum(Iva) as Iva, sum(Importe) as Total 
		Into #tmpRemisionEncabezado 	
		From #tmpRemisionModificada  	
		Group by IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision 
		
		Update R Set 
			SubTotalSinGrabar = E.SubTotalSinGrabar, 
			SubTotalGrabado = E.SubTotalGrabado, Iva = E.Iva, Total = E.Total 
		From FACT_Remisiones R (NoLock) 
		Inner Join #tmpRemisionEncabezado E 
			On ( R.IdEmpresa = E.IdEmpresa And R.IdEstado = E.IdEstado 
				 And R.IdFarmaciaGenera = E.IdFarmaciaGenera And R.FolioRemision = E.FolioRemision ) 
	 		 
	 		 
		---		select top 1 * from FACT_Remisiones 
		 
		 
		-- Se inserta el Concentrado de la Factura
		Insert Into FACT_Remisiones_Concentrado ( IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, FolioRemision, 
											IdFuenteFinanciamiento, IdFinanciamiento, 
											IdPrograma,	IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioLicitado, 
											Cantidad, TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe ) 
		Select	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, @FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento,  
				IdPrograma, IdSubPrograma, 
				ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave, Sum(CantidadVendida), Sum(TasaIva), 
				Sum(SubTotal_SinGrabar), Sum(SubTotal_Grabado), Sum(Iva), Sum(Importe)
		From #tmpRemisionModificada(NoLock)		
		Group By 
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, IdSubFarmacia, IdFuenteFinanciamiento, IdFinanciamiento,  
			IdPrograma, IdSubPrograma, ClaveSSA, IdProducto, CodigoEAN, ClaveLote, PrecioClave
 

		-- Se inserta el Resumen de la Factura
		Insert Into FACT_Remisiones_Resumen ( IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, 
											IdFuenteFinanciamiento, IdFinanciamiento,  
											IdPrograma,	IdSubPrograma, ClaveSSA, PrecioLicitado, 
											Cantidad, TasaIva, SubTotalSinGrabar, SubTotalGrabado, Iva, Importe ) 
		Select	IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, 
				IdPrograma, IdSubPrograma, ClaveSSA, PrecioLicitado, Sum(Cantidad), Sum(TasaIva), 
				Sum(SubTotalSinGrabar), Sum(SubTotalGrabado), Sum(Iva), Sum(Importe)
		From FACT_Remisiones_Concentrado(NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioRemision = @FolioRemision
		Group By 
			IdEmpresa, IdEstado, IdFarmaciaGenera, IdFarmacia, FolioRemision, IdFuenteFinanciamiento, IdFinanciamiento, 
			IdPrograma, IdSubPrograma, ClaveSSA, PrecioLicitado 
			
		-------------- Se genera de nuevo el detallado de la Remision  
					 
	End 


/* 	
	select top 1 * from VentasDet_Lotes 
	select top 1 * from FACT_Remisiones_Detalles 
*/

	
--	sp_listacolumnas FACT_Remisiones_Detalles 
	
--	sp_listacolumnas VentasDet_Lotes 	
	
	
End 
Go--#SQL 
