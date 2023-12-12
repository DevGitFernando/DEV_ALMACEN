------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__023__StatusInformacion_Farmacias' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__023__StatusInformacion_Farmacias
Go--#SQL 

Create Proc spp_BI_RPT__023__StatusInformacion_Farmacias 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21',  
	@IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '', 
	@IdFarmacia varchar(4) = ''
) 	
With Encryption 
As 
Begin 
Set NoCount On  
Set Dateformat YMD 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 

----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  



	Select 
		F.IdEstado, F.Estado, 
		F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia, F.IdTipoUnidad, F.TipoDeUnidad, 
		getdate() as FechaUltima_Venta, 
		0 as DiasDiferencia_Actualizacion_Venta, 0 as HorasDiferencia_Actualizacion_Venta,	  
		(getdate() - 60)  as FechaUltimaActualizacion, 
		0 as DiasDiferencia_Actualizacion, 0 as HorasDiferencia_Actualizacion  
	into #tmpInformacion  
	From vw_Farmacias__PRCS F (NoLock) 
	Inner Join #vw_Farmacias L (NoLock) On ( F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia ) 
	Where F.IdEstado = @IdEstado and Status = 'A' 
	Order by F.IdJurisdiccion, F.IdTipoUnidad, F.IdFarmacia 

	
	Update I Set FechaUltimaActualizacion = 
		IsNull
		(
			(
				Select max(FechaUpdate) 
				From Ctl_Replicaciones_Historico R (NoLock) 
				Where R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmacia = I.IdFarmacia 
					and R.Cuadrada = 1
			) 
		, '2017-01-01') 
	From #tmpInformacion I (NoLock) 
		 


	Update I Set FechaUltima_Venta = 
		IsNull
		( 
			(
				Select max(FechaRegistro) 
				From VentasEnc R (NoLock) 
				Where R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmacia = I.IdFarmacia 
			)  
		, '2017-01-01')  
	From #tmpInformacion I (NoLock) 


--		spp_BI_RPT__023__StatusInformacion_Farmacias 

	Update I Set 
		DiasDiferencia_Actualizacion = datediff(day, FechaUltimaActualizacion, getdate()), 
		HorasDiferencia_Actualizacion =  datediff(hour, FechaUltimaActualizacion, getdate()), 

		DiasDiferencia_Actualizacion_Venta = datediff(day, FechaUltima_Venta, getdate()), 
		HorasDiferencia_Actualizacion_Venta =  datediff(hour, FechaUltima_Venta, getdate()) 
	From #tmpInformacion I (NoLock) 



--	select top 10 * from Ctl_Replicaciones_Historico 


	Select * 
	From #tmpInformacion 
	-- Where DiasDiferencia_Actualizacion >= 2 


End	
Go--#SQL 
	
