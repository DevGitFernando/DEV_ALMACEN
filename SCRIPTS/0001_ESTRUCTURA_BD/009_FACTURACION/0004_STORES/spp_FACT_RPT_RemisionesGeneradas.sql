---------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_RPT_RemisionesGeneradas' and xType = 'P' )
    Drop Proc spp_FACT_RPT_RemisionesGeneradas  
Go--#SQL 
  
/* 
	sp_listacolumnas__Stores spp_FACT_RPT_RemisionesGeneradas , 1 

	Exec spp_FACT_RPT_RemisionesGeneradas	
		@IdEmpresa = '001', @IdEstado = '28', @IdFarmaciaGenera = '0001', 
		@FolioRemision = '5953', @Detallado = '1', @Aplicar_Mascara = '0', @Identificador_UUID = '', @MostrarResumen = '0' 


*/ 

--@Identificador_UUID 
Create Proc spp_FACT_RPT_RemisionesGeneradas 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '13', 
	@IdFarmaciaGenera varchar(4) = '0001',  @Identificador_UUID varchar(max) = ''  
) 
With Encryption		
As 
Begin 
Set NoCount On 
Declare 
	@Anexar__Lotes_y_Caducidades bit, 
	@EsInsumos bit, 	
	@EsMedicamento bit, 
	@sUnidadDeMedida varchar(100), 
	@bEsConcentrado_Servicio bit, 
	@bRemision_Medicamento bit, 
	@bRemision_MaterialDeCuracion bit
	
Declare 
	@sSql varchar(max), 
	@Keyx int 


	Select IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, GUID 
	Into #tmp_ListaRemisiones 
	From FACT_Remisiones R (NoLock) 
	Where 1 = 0 

	Set @sSql = '' + 
	'Insert Into #tmp_ListaRemisiones ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, GUID  ) ' + char(10) + 
	'Select IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, GUID ' + char(10) + 
	'From FACT_Remisiones D (NoLock) ' + char(10) + 
	'Where D.IdEstado = ' + char(39) + @IdEstado + char(39) + ' And D.IdFarmaciaGenera = ' + char(39) + @IdFarmaciaGenera + char(39) + ' And D.GUID in ( ' + @Identificador_UUID + ' ) '  
	Exec( @sSql ) 
	Print @sSql 


	------------------------------------- Salida final 
	Select 
		identity(int, 1, 1) as Renglon, 
		R.GUID, 
		'Folio de remisión' = R.FolioRemision, 
		'Fecha de remisión' = R.FechaRemision, 
		R.IdFuenteFinanciamiento, R.IdFinanciamiento, R.Financiamiento, 
		'Tipo de remisión' = R.TipoDeRemisionDesc, 
		'Tipo de insumo' = R.TipoDeInsumoDesc, 
		'Origen de insumo' = R.OrigenInsumoDesc, 
		(R.SubTotalGrabado + R.SubTotalSinGrabar) as SubTotal, Iva, Total as Importe, 
		'Fecha inicial de dispensación' = convert(varchar(10), R.FechaInicial, 120), 
		'Fecha final de dispensación' = convert(varchar(10), R.FechaFinal, 120), 
		
		--R.EsRelacionDocumento, 
		(case when R.EsRelacionDocumento = 1 then 'SI' else 'NO' end) as EsRelacionDocumento_Desc, 
		R.FolioRelacionDocumento, 
		
		--R.EsRelacionFacturaPrevia, 
		(case when R.EsRelacionFacturaPrevia = 1 then 'SI' else 'NO' end) as EsRelacionFacturaPrevia_Desc, 
		'Serie CFDI' = R.Serie, 'Folio CFDI' = R.Folio  
	Into #tmp_Resultado 
	From vw_FACT_Remisiones R (NoLock) 
	Inner Join #tmp_ListaRemisiones L (NoLock) 
		On ( R.IdEmpresa = L.IdEmpresa and R.IdEstado = L.IdEstado and R.IdFarmacia = L.IdFarmaciaGenera and R.FolioRemision = L.FolioRemision) 
	Order by L.FolioRemision desc, L.GUID  


	select * 
	from #tmp_Resultado 
	Order by 
		Renglon 

End
Go--#SQL 

