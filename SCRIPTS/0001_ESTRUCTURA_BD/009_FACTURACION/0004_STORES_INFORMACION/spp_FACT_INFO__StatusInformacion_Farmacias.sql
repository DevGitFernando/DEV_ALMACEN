------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_INFO__StatusInformacion_Farmacias' and xType = 'P' ) 
   Drop Proc spp_FACT_INFO__StatusInformacion_Farmacias
Go--#SQL 

Create Proc spp_FACT_INFO__StatusInformacion_Farmacias 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '22', @IdFarmacia varchar(4) = '0001'
) 	
With Encryption 
As 
Begin 
Set NoCount On  
Set Dateformat YMD 

Declare 
	@sSql varchar(max), 
	@sBaseDeDatosOperacion varchar(500) 

	Set @sSql = '' 
	Set @sBaseDeDatosOperacion = '' --'SII_Regional__Pharmajal_Queretaro' 


	Select @sBaseDeDatosOperacion = Valor 
	From Net_CFG_Parametros_Facturacion (noLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and NombreParametro = 'BaseDeDatosOperacion' 

	--Set @sBaseDeDatosOperacion = 'SII_Regional__Pharmajal_Queretaro' 
	Set @sBaseDeDatosOperacion = IsNull(@sBaseDeDatosOperacion, '') 


----------------------------------------------------- DATOS FILTRO 
	Select 
		IdEstado, Estado, IdFarmacia, Farmacia, EsAlmacen, EsUnidosis, getdate() as FechaUltima_Actualizacion, 0 as InformacionCuadrada  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, Estado, IdFarmacia, Farmacia, EsAlmacen, EsUnidosis, FechaUltima_Actualizacion, InformacionCuadrada ) ' + char(13) + 
				'Select F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, F.EsAlmacen, F.EsUnidosis, R.FechaUpdate, R.Cuadrada ' + char(13) + 
				'From ' + @sBaseDeDatosOperacion +'..vw_Farmacias F (NoLock) ' + char(13) + 	
				'Inner Join ' + @sBaseDeDatosOperacion +'..Ctl_Replicaciones R (NoLock) On ( F.IdEstado = R.IdEstado and F.IdFarmacia = R.IdFarmacia ) ' + char(13) + 
				'Inner Join FACT_CFG_Farmacias C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) ' + char(13) 	
	Set @sSql = @sSql + 'Where F.IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) 
	Exec(@sSql)  
	Print @sSql 


--	select top 10 * from Ctl_Replicaciones_Historico 

--		spp_FACT_INFO__StatusInformacion_Farmacias 

	----------------------------------- Salida final 
	Select 
		'Id Farmacia' = IdFarmacia, Farmacia, 
		'Es almacén' = (case when EsAlmacen = 1 then 'SI' else 'NO' end), 
		'Es Unidosis' = (case when EsUnidosis = 1 then 'SI' else 'NO' end), 
		'Información cuadrada' = (case when InformacionCuadrada = 1 then 'SI' else 'NO' end), 
		'Fecha de ultima actualización' = FechaUltima_Actualizacion  
	From #vw_Farmacias  
	Order by IdEstado, IdFarmacia 
	-- Where DiasDiferencia_Actualizacion >= 2 


End	
Go--#SQL 
	
