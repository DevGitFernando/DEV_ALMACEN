If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Rpt_ALMN_DIST_REMISIONES_NivelesAtencion_ClavesSSA' and Type = 'P' ) 
   Drop Proc spp_Rpt_ALMN_DIST_REMISIONES_NivelesAtencion_ClavesSSA
Go--#SQL 
---		spp_Rpt_ALMN_DIST_REMISIONES_NivelesAtencion_ClavesSSA  1, 21, 1182, 4721, 0
   
--   use SII_21_1182_CEDIS_PUEBLA 
   
Create Proc spp_Rpt_ALMN_DIST_REMISIONES_NivelesAtencion_ClavesSSA 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0004', @IdDistribuidor varchar(4) = '0001',
	@EsConsignacion smallint = 0, @FechaIni varchar(10) = '2013-02-01', @FechaFin varchar(10) = '2013-02-28', @PrecioAdmon numeric(14, 4) = 0.0000,
	@IdPerfilDeAtencion int = 1  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFiltro_Atencion varchar(1000),   
	@sFiltro varchar(1000), 
	@sTipoReporte varchar(10)   

---------------	Formatear valores 
	Set @IdEmpresa = right('0000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000000000000' + @IdFarmacia, 4) 
----	Set @Folio = right('0000000000000' + @Folio, 8) 	
	Set @sFiltro = '' 
	Set @sFiltro_Atencion = '' 
	Set @sTipoReporte = '01'	

	If @EsConsignacion = 1 
	   Set @sTipoReporte = '02'	 
			
---------------	Preparar tabla 	
	Select * 
	Into #tmpRemision 
	From vw_Impresion_RemisionesDistribuidor 
	Where 1 = 0 
	
	-- Alter Table #tmpRemision Alter Column Folio varchar(30) 
	
	Select * 
	Into #tmpRemisionFiltro 	
	From #tmpRemision 
	

	Set @sFiltro = 'Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) 
	Set @sFiltro = @sFiltro + ' and IdFarmacia = ' + char(39) + @IdFarmacia + char(39)
	Set @sFiltro = @sFiltro + ' and IdDistribuidor = ' + char(39) + @IdDistribuidor + char(39)
	Set @sFiltro = @sFiltro + ' and Convert(varchar(10), FechaDocumento, 120) Between ' + char(39) + @FechaIni + char(39) + ' and ' + char(39) + @FechaFin + char(39)
	Set @sFiltro = @sFiltro + ' and EsConsignacion = ' + Cast(@EsConsignacion as varchar)
	Set @sFiltro = @sFiltro + ' and Status =  ' + char(39) + 'T' + char(39) + ' and EsExcepcion = 0 ' 
	Set @sFiltro_Atencion = ' C.IdEmpresa = V.IdEmpresa and C.IdEstado = V.IdEstado and C.IdFarmacia = V.IdFarmacia ' 
	
	
--	spp_Rpt_ALMN_DIST_REMISIONES_NivelesAtencion_ClavesSSA	
	
--- Crear la tabla base 	
	Set @sSql = 'Insert Into #tmpRemision Select * From vw_Impresion_RemisionesDistribuidor V (NoLock) ' + @sFiltro 
	Exec (@sSql) 

	--Print @sSql 
	
----	Update V Set Folio = Folio + '-' + cast(@IdPerfilDeAtencion as varchar) + '-' + @sTipoReporte
----	From #tmpVenta V 
	
	
	If @IdPerfilDeAtencion = 0
	   Begin 
			Set @sFiltro_Atencion = char(13) + 
			'Where Not Exists ' + char(13) + 
			'	(	' + 
			'		Select * From vw_CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA C (NoLock) ' + char(13) + 
			'		Where ' + @sFiltro_Atencion + char(13) +			 			
			'		And C.ClaveSSA = V.ClaveSSA and C.StatusClaveSSA = ' + char(39) + 'A' + char(39) + char(13) + 			
			'	) ' + char(13) 
			Set @sSql = 'Insert Into #tmpRemisionFiltro Select * From #tmpRemision V (NoLock) ' + @sFiltro_Atencion  
	   End 
	Else 
	   Begin 
			Set @sFiltro_Atencion = char(13) + 
			'Where Exists ' + char(13) + 
			'	(	' + 
			'		Select * From vw_CFGC_ALMN_DIST_CB_NivelesAtencion_ClavesSSA C (NoLock) ' + char(13) + 
			'		Where ' + @sFiltro_Atencion + ' And IdPerfilAtencion = ' + cast(@IdPerfilDeAtencion as varchar) +  char(13) + 
			'			And C.ClaveSSA = V.ClaveSSA and C.StatusClaveSSA = ' + char(39) + 'A' + char(39) + char(13) + 
			'	) ' + char(13) 
			Set @sSql = 'Insert Into #tmpRemisionFiltro Select * From #tmpRemision V (NoLock) ' + @sFiltro_Atencion  
	   End 
	
--- Generar Impresion 
	  Exec (@sSql) 
	--	Print @sSql 

---------------	Salida final 
--	Select * From #tmpVenta   

	Delete From #tmpRemisionFiltro Where CantidadRecibida = 0

	Update T Set T.FarmaciaRelacionada = 'SIN MODULO'
	From #tmpRemisionFiltro T 

	Select * From #tmpRemisionFiltro  

End 
Go--#SQL 
