-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'sp_ShrinkDB' and xType = 'P' ) 
   Drop Proc sp_ShrinkDB 
Go--#SQL 

Create Proc sp_ShrinkDB ( @NombreBD varchar(100) = '', @Ejecutar int = 1 ) 
With Encryption 
As    
Begin 
Set NoCount On 

Declare 
	@sBdName varchar(100), 
	@sNombreLogico varchar(max) 

Declare 
	@sSql varchar(1000), 
	@iTamFile int, -- numeric(14,4), 
	@dbsize bigint   

	Set @dbsize = 0 
	set @sNombreLogico = '' 

    If @NombreBD = '' 
       Set @sBdName = db_name()
    Else 
       Set @sBdName = @NombreBD  


  
--- Datos del Registro 
    Select 0 as FileId, cast('' as varchar(max)) as NombreFile, cast(0 as bigint) as DbSize, cast(0 as bigint) as LogSize   
    Into #tmpSysFiles From Sysfiles Where 1 = 0 
    
    Set @sSql = 'Insert Into #tmpSysFiles 
		Select	
			FileId, Name, 
			size as DbSize,  
			size as LogSize 
		From ' + @sBdName + '..Sysfiles Where FileId = 1 '
    Execute(@sSql)
	--Print @sSql  
	--Print '' 

	Select 
		cast(0 as bigint) as PaginasReservadas, 
		cast(0 as bigint) as PaginasAsignadas, 
		cast(0 as bigint) as Paginas 
	Into #tmpPaginas Where 1 = 0  
	Set @sSql = 
	'Insert Into #tmpPaginas 
	Select 
		sum(a.total_pages),  
		sum(a.used_pages),  
		sum(  
	CASE  
		When it.internal_type IN (202,204,211,212,213,214,215,216) Then 0  
		When a.type <> 1 Then a.used_pages  
		When p.index_id < 2 Then a.data_pages  
		Else 0  
	END )  
	From ' + @sBdName + '.sys.partitions p 
	Inner Join ' + @sBdName + '.sys.allocation_units a on ( p.partition_id = a.container_id  ) 
	Left join ' + @sBdName + '.sys.internal_tables it on ( p.object_id = it.object_id  ) ' 
    Execute(@sSql)
	-- Print @sSql  
	--Print '' 

	------select * from #tmpSysFiles 
	------select * from #tmpPaginas  	
	
	Select @sNombreLogico = NombreFile, @dbsize = DbSize From #tmpSysFiles where FileId = 1 
	Select @iTamFile = 
	cast(
		ceiling(
		( 
			Select 
			ltrim(str((case when @dbsize >= PaginasReservadas then  
				   ((PaginasReservadas * 8) / 1024) else 0 end),15,2)) as EspacioLibre 
			From #tmpPaginas 
		)) 
    as int) + 5  
    

-------------------------------------------- Ejecucion Final 
	Set @sSql = 'USE [' + @sBdName + '] ' + char(13) 
	Set @sSql = @sSql + 'DBCC SHRINKFILE (N' + char(39) + @sNombreLogico + char(39) + ', ' + cast(@iTamFile as varchar) + ' ) '    
	
	If @Ejecutar = 1 
		Begin 
		   Print @sSql 
		   Exec(@sSql) 
		End 
	Else    
		Begin 
		   Print @sSql 
		End    
	   

	
--	DBCC SHRINKFILE (N'SII_Regional_Michoacan___2K150720_1103__Revision_Datos_data' , 3480)


End 
Go--#SQL 
        