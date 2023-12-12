If Exists (Select Name From Sysobjects Where Name = 'sp_Net_RegistrosCatalogos' and xType = 'P')
   Drop Proc sp_Net_RegistrosCatalogos
Go

--	sp_TablasCatalogos 

--  sp_truncatelog 

--	sp_TablasDetalles  


Create Proc sp_Net_RegistrosCatalogos ( @iOrden smallint = 2 )
With Encryption
As
Begin 
Declare @sSql varchar(1000)

--    If @sFiltro <> ''
--       Set @sFiltro =  ' and ' + @sFiltro 

	if @iOrden <= 0 
	   Set @iOrden = 1 
	   
	if @iOrden > 3 
	   Set @iOrden = 3     

----------- 
	Select top 0 space(500) as Nombre, cast(0 as numeric(15,4)) as Rows, cast(0 as numeric(15,4)) as TotalEspacio 
	Into #tmpTamTablas 


	Set @sSql = ' 
		Insert Into #tmpTamTablas ( Nombre, Rows, TotalEspacio ) 
		Select Nombre, Rows, TotalDeEspacioMB 
		From 
			(
				Select So.Name as Nombre, (SELECT rows FROM sysindexes s WHERE s.indid < 2 AND s.id = So.Id) as Rows,
					CONVERT(numeric(15,4), 
					( (( sum(Si.reserved) * 
						( SELECT cast(low as bigint) as low FROM master.dbo.spt_values (NOLOCK) WHERE number = 1 AND type = ''E'' ) / 1024.0000) / 1024.0000 ))
					) as ''TotalDeEspacioMB''
				From Sysobjects So (NoLock)
				Inner Join Sysindexes Si (NoLock) On ( So.Id = Si.Id ) 
				Inner Join CFGS_EnvioCatalogos T On ( So.Name = T.NombreTabla )
				Group by So.Name, So.Id 
			) as T  ' + 
		' order by ' + cast(@iOrden as varchar) + ' desc'	
	Exec(@sSql) 


----
	Select Nombre, Rows, TotalEspacio as 'Total De Espacio MB'  
	From #tmpTamTablas 

	
End
Go 

-- DBCC UPDATEUSAGE ('ropa', 'Descripciones_De_Codigo') 
-- UPDATE STATISTICS Descripciones_De_Codigo With FullScan


If Exists (Select Name From Sysobjects Where Name = 'sp_Net_RegistrosDetalles' and xType = 'P')
   Drop Proc sp_Net_RegistrosDetalles
Go
-- sp_helptext sp_TablasDetalles 

Create Proc sp_Net_RegistrosDetalles ( @iOrden smallint = 2 )
With Encryption
As
Begin 
Declare @sSql varchar(1000)

--    If @sFiltro <> ''
--       Set @sFiltro =  ' and ' + @sFiltro 

	if @iOrden <= 0 
	   Set @iOrden = 1 
	   
	if @iOrden > 3 
	   Set @iOrden = 3     

----------- 
	Select top 0 space(500) as Nombre, cast(0 as numeric(15,4)) as Rows, cast(0 as numeric(15,4)) as TotalEspacio 
	Into #tmpTamTablas 


	Set @sSql = ' 
		Insert Into #tmpTamTablas ( Nombre, Rows, TotalEspacio ) 
		Select Nombre, Rows, TotalDeEspacioMB 
		From 
			(
				Select So.Name as Nombre, (SELECT rows FROM sysindexes s WHERE s.indid < 2 AND s.id = So.Id) as Rows,
					CONVERT(numeric(15,4), 
					( (( sum(Si.reserved) * 
						( SELECT cast(low as bigint) as low FROM master.dbo.spt_values (NOLOCK) WHERE number = 1 AND type = ''E'' ) / 1024.0000) / 1024.0000 ))
					) as ''TotalDeEspacioMB''
				From Sysobjects So (NoLock)
				Inner Join Sysindexes Si (NoLock) On ( So.Id = Si.Id ) 
				Inner Join CFGC_EnvioDetalles T On ( So.Name = T.NombreTabla )
				Group by So.Name, So.Id 
			) as T  ' + 
		' order by ' + cast(@iOrden as varchar) + ' desc'	
	Exec(@sSql) 


----
	Select Nombre, Rows, TotalEspacio as 'Total De Espacio MB'  
	From #tmpTamTablas 

	
End
Go 


