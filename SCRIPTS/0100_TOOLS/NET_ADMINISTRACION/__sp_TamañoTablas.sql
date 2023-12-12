---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects Where Name = 'sp_TamañoTablas' and xType = 'P' )
   Drop Proc sp_TamañoTablas 
Go--#SQL  
-- sp_helptext sp_TamañoTablas  

Create Proc sp_TamañoTablas ( @iOrden smallint = 3, @sFiltro varchar(100) = '' )
With Encryption
As 
Begin
Declare 
	@sSql varchar(1000), 
	@iPagina bigint 

	SELECT @iPagina = 8 -- cast(low as bigint) / 1024.0000 FROM master.dbo.spt_values (NOLOCK) WHERE number = 1 AND type = 'E' 


    If @sFiltro <> '' 
       Set @sFiltro =  ' and ' + ' So.Name = ' + char(39) + @sFiltro + char(39) 

	if @iOrden <= 0 
	   Set @iOrden = 1 
	   
	if @iOrden > 20 
	   Set @iOrden = 3     

	Select top 
		0 space(500) as Tabla, 
		getdate() as FechaCreacion, 
		cast(0 as bigint) as Renglones, 		
		0 as Datos, 
		0 as SinUsar, 		
		cast(0 as numeric(14,4)) as SizeKB, 
		cast(0 as numeric(14,4)) as SizeMB, 
		cast(0 as numeric(14,4)) as SizeGB, 
		cast(0 as numeric(14,4)) as SizeKB_SinUsar, 
		cast(0 as numeric(14,4)) as SizeMB_SinUsar, 
		cast(0 as numeric(14,4)) as SizeGB_SinUsar  		 
	Into #tmpTablas 


------    Set @sSql = 'Insert Into #tmpTablas ' + 
------		'Select So.Name, (SELECT rows FROM sysindexes s WHERE s.indid < 2 AND s.id = So.Id) as Rows,
------        CONVERT(numeric(15,4), 
------        ( (( sum(Si.reserved) * 
------			( SELECT cast(low as bigint) as low FROM master.dbo.spt_values (NOLOCK) WHERE number = 1 AND type = ''E'' ) / 1024.0000) ))
------		) as ''Total de espacio MB '', 
------        CONVERT(numeric(15,4), 
------			( 
------				(
------					(sum(Si.reserved) * (SELECT cast(low as bigint) as low FROM master.dbo.spt_values (NOLOCK) WHERE number = 1 AND type = ''E'')) 
------					/ 1024.0000
------				) 
------			/ 1024.0000 
------			) 
------		) as ''Total de espacio GB ''		
------        From Sysobjects So (NoLock)
------        Inner Join Sysindexes Si (NoLock) On (So.Id = Si.Id) 
------        Where So.xType = ' + char(39) + 'U' + char(39) + @sFiltro + ' 
------        group by So.Name, So.Id 
------        order by ' + cast(@iOrden as varchar) + ' desc'
--------    Exec (@sSql) 
--------    Print @sSql 
    
    
    Set @sSql = 'Insert Into #tmpTablas ' + 
		'Select So.Name, so.crDate. Si.Rows, 
		((( (si.used ) * ' + cast(@iPagina as varchar) + ' ) / 1.00) / 1.00) as ''Total de espacio KB '', 		
		((( si.used * ' + cast(@iPagina as varchar) + ' ) / 1.00) / 1.00) as ''Total de espacio MB '', 
		(((( si.used * ' + cast(@iPagina as varchar) + ' ) / 1.00) / 1.00) / 1.00)as ''Total de espacio GB '' 		
        From Sysobjects So (NoLock)
        Inner Join Sysindexes Si (NoLock) On ( So.Id = Si.Id ) 
        Where Si.IndId = 1 and So.xType = ' + char(39) + 'U' + char(39) + @sFiltro + '  
        order by ' + cast(@iOrden as varchar) + ' desc '  
--    Exec (@sSql) 
--    Print @sSql 
    
    
    Set @sSql = 'Insert Into #tmpTablas ' + 
		'Select So.Name, so.crDate, sum(Si.Rows), 
		sum(Si.Reserved) as Datos,
		sum((Si.Reserved - Si.Used)) SinUsar, 
		0, 0, 0, 0, 0, 0 
        From Sysobjects So (NoLock)
        Inner Join Sysindexes Si (NoLock) On ( So.Id = Si.Id ) 
        Where So.xType = ' + char(39) + 'U' + char(39) + @sFiltro + '  
        Group by So.Name, so.crDate  
        order by ' + cast(@iOrden as varchar) + ' desc'  
    Exec (@sSql) 
    --Print @sSql 
        
       
    
---------------------------------------------------------------     
    Update T Set 
		SizeKB = (8 * Datos), 
		SizeMB = ((8 * Datos) / 1024.00), 
		SizeGB = (((8 * Datos) / 1024.00) / 1024.00), 
		
		SizeKB_SinUsar = (8 * SinUsar), 
		SizeMB_SinUsar = ((8 * SinUsar) / 1024.00), 
		SizeGB_SinUsar = (((8 * SinUsar) / 1024.00) / 1024.00) 		 
    From #tmpTablas T  




	Select 
		sum(Renglones) as Renglones, 
		sum(Datos) as Datos, 
		sum(SinUsar) as SinUsar, 		
		'Total de espacio KB ' = sum(SizeKB), 
		'Total de espacio MB ' = sum(SizeMB), 
		'Total de espacio GB ' = sum(SizeGB),   
		'Total de espacio Sin Usar KB ' = sum(SizeKB_SinUsar), 
		'Total de espacio Sin Usar MB ' = sum(SizeMB_SinUsar), 
		'Total de espacio Sin Usar GB ' = sum(SizeGB_SinUsar)  		
    From #tmpTablas 
    

	Set @sSql = ' 
		Select 
			Tabla, FechaCreacion, Renglones, 
			Datos, SinUsar, ' + 
			char(39) + 'Espacio KB ' + char(39) + ' = SizeKB, ' +
			char(39) + 'Espacio MB ' + char(39) + ' = SizeMB, ' +
			char(39) + 'Espacio GB ' + char(39) + ' = SizeGB   ' +
			----'Espacio Sin Usar KB ' = SizeKB_SinUsar, 
			----'Espacio Sin Usar MB ' = SizeMB_SinUsar, 
			----'Espacio Sin Usar GB ' = SizeGB_SinUsar  				
		' From #tmpTablas 
        order by ' + cast(@iOrden as varchar) + ' desc'   
    Exec (@sSql)  
    Print @sSql 

    

End 
Go--#SQL  

-- DBCC UPDATEUSAGE ('ropa', 'Descripciones_De_Codigo') 
-- UPDATE STATISTICS Descripciones_De_Codigo With FullScan
