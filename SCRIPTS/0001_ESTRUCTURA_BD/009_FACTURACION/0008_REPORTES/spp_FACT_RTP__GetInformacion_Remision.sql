-------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_RTP__GetInformacion_Remision_ToPDF' and xType = 'P' )
    Drop Proc spp_FACT_RTP__GetInformacion_Remision_ToPDF
Go--#SQL
  
Create Proc spp_FACT_RTP__GetInformacion_Remision_ToPDF 
( 
	@IdEmpresa varchar(3) = '4', @IdEstado varchar(2) = '22', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '31' 
) 
With Encryption 
As 
Begin 
Set NoCount On 

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
		 *, 
		 cast('' as varchar(10)) as FechaCaducidad 
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
	

	Update E Set Empresa = @sNombreEmpresa -- , Estado = @sNombreEstado  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 


	Update E Set Empresa = @sNombreEmpresa -- , Estado = @sNombreEstado  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 

	Update E Set FechaCaducidad = convert(varchar(7), L.FechaCaducidad, 120)  
	From #vw_FACT_Remisiones_Detalles E (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmaciaDispensacion = L.IdFarmacia and E.IdSubFarmacia = L.IdSubFarmacia
			and E.IdProducto = L.IdProducto and E.CodigoEAN = L.CodigoEAN and E.ClaveLote = L.ClaveLote  ) 

	--Update E Set ClaveSSA_Mascara = M.Mascara, Descripcion_Mascara = M.DescripcionMascara, Presentacion_Mascara = M.Presentacion 
	--From #vw_FACT_Remisiones_Detalles E (NoLock) 
	--Inner Join vw_ClaveSSA_Mascara M (NoLock) On ( E.IdEstado = M.IdEstado and E.IdCliente = M.IdCliente and E.IdSubCliente = M.IdSubCliente  and E.ClaveSSA = M.ClaveSSA ) 
--------------------------------------------------------------------------------- 

------------------------------------- SALIDA FINAL 
	Select * 
	From #vw_FACT_Remisiones_Detalles 


End
Go--#SQL



-------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_RTP__GetInformacion_Remision_ToExcel' and xType = 'P' )
    Drop Proc spp_FACT_RTP__GetInformacion_Remision_ToExcel 
Go--#SQL
  
Create Proc spp_FACT_RTP__GetInformacion_Remision_ToExcel 
( 
	@IdEmpresa varchar(3) = '2', @IdEstado varchar(2) = '9', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '1' 
)
With Encryption 
As
Begin 
Set NoCount On 


	--------------------- FORMATEAR LOS PARAMETROS 
	Set @IdEmpresa = right('0000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('0000000000000000' + @IdEstado, 2)  
	Set @IdFarmaciaGenera = right('0000000000000000' + @IdFarmaciaGenera, 4)  
	Set @FolioRemision = right('0000000000000000' + @FolioRemision, 10)  
	

	If @IdEstado = '09' 
	Begin 
		Exec spp_FACT_RTP__GetInformacion_Remision_ToExcel__09_SSH_1N @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @FolioRemision = @FolioRemision 
	End 

	If @IdEstado = '11' 
	Begin 
		Exec spp_FACT_RTP__GetInformacion_Remision_ToExcel__11_SSG @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @FolioRemision = @FolioRemision 
	End 

	If @IdEstado = '14' 
	Begin 
		Exec spp_FACT_RTP__GetInformacion_Remision_ToExcel__09_SSH_1N @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @FolioRemision = @FolioRemision 
	End 


	If @IdEstado = '13' 
	Begin 
		Exec spp_FACT_RTP__GetInformacion_Remision_ToExcel__13_SSH @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @FolioRemision = @FolioRemision 
	End 

	If @IdEstado = '28' 
	Begin 
		Exec spp_FACT_RTP__GetInformacion_Remision_ToExcel__28_SST @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmaciaGenera = @IdFarmaciaGenera, @FolioRemision = @FolioRemision 
	End 

	
End
Go--#SQL

