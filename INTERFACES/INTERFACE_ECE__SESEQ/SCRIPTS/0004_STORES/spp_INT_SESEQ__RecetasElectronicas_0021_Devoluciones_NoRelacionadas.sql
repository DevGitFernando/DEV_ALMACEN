-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0021_Devoluciones_NoRelacionadas' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0021_Devoluciones_NoRelacionadas
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0021_Devoluciones_NoRelacionadas 
( 
	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '113' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set Dateformat YMD 

Declare 
	@sFolio_Receta varchar(20), 
	@sFolio varchar(20), 
	@dFechaSurtido datetime, 
	@sMensaje varchar(200), 
	@iSecuencial int 


	Set @IdEmpresa = right('000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000' + @IdFarmacia, 4) 


	---			spp_INT_SESEQ__RecetasElectronicas_0021_Devoluciones_NoRelacionadas 


	------------------------------------------------ Obtener información 
	Select 
		D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioDevolucion, D.FechaRegistro as FechaDeDevolucion, 
		D.Referencia as FolioVenta, cast('' as varchar(100)) as NumeroReceta, 
		IsNull(T.FolioSurtido, '') as FolioSurtido_SESEQ, 
		--T.TipoDeProceso, 
		IsNull(T.FolioReceta, '') as FolioReceta_SESEQ, IsNull(T.FechaReceta, '') as FechaReceta_SESEQ, 
		(case when IsNull(T.FolioReceta, '') = '' then 0 else 1 end) as EnInterface 
	Into #tmpDevoluciones 
	From DevolucionesEnc D (noLock) 
	Left Join 
	( 
		------------------- Folios de ventas - normales   	
		Select R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.Folio, R.FolioSurtido, 0 as EsResurtible, R.TipoDeProceso, R.FolioReceta, R.FechaReceta  
		from INT_SESEQ__RecetasElectronicas_0001_General R (NoLock) 
		Where ( R.EsSurtido = 1 ) 
			and R.EsResurtible = 0  
			and R.FolioSurtido <> '' 
		Group by 
			R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.Folio, R.FolioSurtido, 
			R.TipoDeProceso, R.FolioReceta, R.FechaReceta 
		Union 

		------------------- Folios de ventas - resurtibles  
		Select R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.Folio, P.FolioSurtido, 1 as EsResurtible, R.TipoDeProceso, R.FolioReceta, R.FechaReceta  
		from INT_SESEQ__RecetasElectronicas_0001_General R (NoLock) 
		Inner Join INT_SESEQ__RecetasElectronicas_0006_PartidasRecetas P (NoLock) 
			On ( R.IdEmpresa = P.IdEmpresa and R.IdEstado = P.IdEstado and R.IdFarmacia = P.IdFarmacia and R.Folio = P.Folio and R.FolioReceta = P.FolioReceta ) 
		Where ( R.EsSurtido = 1 or P.EsSurtido = 1 ) 
			and R.EsResurtible = 1 
			and P.FolioSurtido <> '' 
		Group by 
			R.IdEmpresa, R.IdEstado, R.IdFarmacia, R.Folio, P.FolioSurtido, 
			R.TipoDeProceso, R.FolioReceta, R.FechaReceta 
	) as T On ( T.IdFarmacia = D.IdFarmacia and T.FolioSurtido = D.Referencia ) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia 
	Order by EsResurtible, FolioSurtido 
	------------------------------------------------ Obtener información 

	------------------------------------------------ Obtener información de la venta 
	Update D Set NumeroReceta = V.NumReceta 
	From #tmpDevoluciones D 
	Inner Join VentasInformacionAdicional V (NoLock) On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdFarmacia = V.IdFarmacia and D.FolioVenta = V.FolioVenta )  
	------------------------------------------------ Obtener información de la venta 


	---			spp_INT_SESEQ__RecetasElectronicas_0021_Devoluciones_NoRelacionadas 


	----------------------- SALIDA FINAL 
	Select * 
	From #tmpDevoluciones 
	where EnInterface = 0 


End 
Go--#SQL 
	
	--	sp_listacolumnas__stores		spp_INT_SESEQ__RecetasElectronicas_0021_Devoluciones_NoRelacionadas , 1	
