-------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_RPT__GetInformacion_Remision_ToPDF_09_SSH_1N' and xType = 'P' )
    Drop Proc spp_FACT_RPT__GetInformacion_Remision_ToPDF_09_SSH_1N
Go--#SQL

Create Proc spp_FACT_RPT__GetInformacion_Remision_ToPDF_09_SSH_1N 
( 
	@IdEmpresa varchar(3) = '2', @IdEstado varchar(2) = '09', 
	@IdFarmaciaGenera varchar(4) = '1',  @FolioRemision varchar(10) = '30000' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YDM 

Declare 
	@sNombreEmpresa varchar(500), 
	@sNombreEstado varchar(500)  

	--------------------- FORMATEAR LOS PARAMETROS 
	Set @IdEmpresa = right('0000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('0000000000000000' + @IdEstado, 2)  
	Set @IdFarmaciaGenera = right('0000000000000000' + @IdFarmaciaGenera, 4)  
	Set @FolioRemision = right('0000000000000000' + @FolioRemision, 10)  
	Set @sNombreEmpresa = '' 
	Set @sNombreEstado = '' 


	Select @sNombreEmpresa = Nombre From CatEmpresas (NoLock) Where IdEmpresa = @IdEmpresa
	Select top 1 @sNombreEstado = Nombre From CatEstados (NoLock) Where IdEstado = @IdEstado 

--------------------------------------------------------------------------------- 
	Select 
		cast('' as varchar(500)) as Empresa, 
		-- cast('' as varchar(500)) as Estado, 
		 *  
		 , cast('' as varchar(max)) as PartidaPresupuestariaDescripcion  
		 , cast('' as varchar(max)) as TipoDeAccion   
		 , cast(NumReceta + ' - ' + Alias as varchar(max)) as Observaciones_02  
		 , cast('' as varchar(max)) as ClaveSSA_Mascara  
		 , cast('' as varchar(max)) as Descripcion_Mascara  
		 , cast('' as varchar(max)) as Presentacion_Mascara  
		 , cast(Referencia_01 as varchar(max)) as AIE   
		 , cast('' as varchar(max)) as Causes_NoCauses  
		 , cast('' as varchar(max)) as Marca_Distribuida
		 , cast('' as varchar(max)) as Marca_Licitada
		 , cast('' as varchar(max)) as RegistroSSA 
		 , cast('' as varchar(max)) as Caducidad   		   
		 , getdate() as FechaImpresion  
		 --getdate() as FechaFinal 
		--IdEmpresa, IdEstado, IdFarmacia, FolioRemision, FolioFacturaElectronica, IdFarmaciaDispensacion, FarmaciaDispensacion, Referencia_Beneficiario, Referencia_NombreBeneficiario, 
		--FolioVenta, FechaRemision, NumeroDeContrato, IdFuenteFinanciamiento, IdFinanciamiento, Financiamiento, IdDocumento, NombreDocumento, 
		--IdPrograma, Programa, IdSubPrograma, SubPrograma, EsFacturable, TipoDeRemision, TipoDeRemisionDesc, OrigenInsumo, OrigenInsumoDesc, TipoInsumo, TipoDeInsumoDesc, 
		--IdPersonalRemision, SubTotalSinGrabar, SubTotalGrabado, Iva, Total, Observaciones, ObservacionesRemision, Status, 
		--IdProducto, CodigoEAN, Descripcion, ClaveLote, IdClaveSSA, ClaveSSA, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion, PrecioLicitado, PrecioLicitadoUnitario, 
		--Cantidad, Cantidad_Agrupada, TasaIva, SubTotalSinGrabar_Clave, SubTotalGrabado_Clave, Iva_Clave, Importe_Clave 
	Into #vw_FACT_Remisiones_Detalles
	From vw_FACT_Remisiones_Detalles E (NoLock) 
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision 
		-- and ClaveSSA = '010.000.1242.00' 

	Update E Set Empresa = @sNombreEmpresa -- , Estado = @sNombreEstado  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 


	Update E Set Empresa = @sNombreEmpresa -- , Estado = @sNombreEstado  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 


	Update E Set PartidaPresupuestariaDescripcion = I.Observaciones, TipoDeAccion = Referencia_05  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_Remisiones_InformacionAdicional I On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmaciaGenera and E.FolioRemision = I.FolioRemision ) 

	Update E Set ClaveSSA_Mascara = M.Mascara, Descripcion_Mascara = M.DescripcionMascara, Presentacion_Mascara = M.Presentacion 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join vw_ClaveSSA_Mascara M (NoLock) 
		On ( E.IdEstado = M.IdEstado and E.IdCliente = M.IdCliente and E.IdSubCliente = M.IdSubCliente  and E.ClaveSSA = M.ClaveSSA ) 

	Update E Set Caducidad = convert(varchar(7), L.FechaCaducidad, 120)  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmaciaDispensacion = L.IdFarmacia and E.IdSubFarmacia = L.IdSubFarmacia
			and E.IdProducto = L.IdProducto and E.CodigoEAN = L.CodigoEAN and E.ClaveLote = L.ClaveLote  ) 

--		spp_FACT_RPT__GetInformacion_Remision_ToPDF_09_SSH_1N  

	Update E Set Marca_Distribuida = M.DescripcionComercial, Marca_Licitada = M.DescripcionComercial, RegistroSSA = M.RegistroSSA   
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_ClavesSSA_InformacionMarcaComercial M (NoLock) 
		On ( E.IdEstado = M.IdEstado and E.IdCliente = M.IdCliente and E.IdSubCliente = M.IdSubCliente and E.ClaveSSA = M.ClaveSSA ) 

	Update E Set Marca_Distribuida = M.DescripcionComercial 
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FACT_ClavesSSA_InformacionMarcaComercial_Excepcion M (NoLock) 
		On ( E.IdEstado = M.IdEstado and E.IdCliente = M.IdCliente and E.IdSubCliente = M.IdSubCliente and E.ClaveSSA = M.ClaveSSA
			and M.FechaVigencia >= convert(varchar(10), E.FechaFinal, 120) ) 


	--Update E Set Observaciones_02 = 
	--From #vw_FACT_Remisiones_Detalles E (NoLock) 
	--Inner Join VentasInformacion



--	select * from FACT_ClavesSSA_InformacionMarcaComercial 

/* 

	Update C Set IdCliente = '0002', IdSubCliente = '0012'  
	From CFG_ClaveSSA_Mascara C 

	Update C Set IdCliente = '0002', IdSubCliente = '0012'  
	From FACT_ClavesSSA_InformacionMarcaComercial C 


	Update C Set IdCliente = '0002', IdSubCliente = '0012'  
	From FACT_ClavesSSA_InformacionMarcaComercial_Excepcion C 

*/ 


	----Update E Set AIE = F.Referencia_01  
	----From #vw_FACT_Remisiones_Detalles E (NoLock) 
	----Inner Join FACT_Fuentes_De_Financiamiento_Detalles_ClavesSSA__Farmacia F (NoLock) 
	----	On ( E.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento and E.IdFinanciamiento = F.IdFinanciamiento 
	----		and E.IdEstado = F.IdEstado and E.IdFarmaciaDispensacion = F.IdFarmacia and E.ClaveSSA = F.ClaveSSA ) 





--------------------------------------------------------------------------------- 

------------------------------------- SALIDA FINAL 
	Select * 
	From #vw_FACT_Remisiones_Detalles 
	where Cantidad > 0 and Cantidad_Agrupada > 0 

End
Go--#SQL
