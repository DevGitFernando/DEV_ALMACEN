

---- Comentariar todo el Script 
Declare 
    @sSql varchar(max), 
    @sSql_Aux varchar(max), 
    @sSql_Exec varchar(max),             
    @sTabla varchar(200), 
    @sTriggerPrefijo varchar(100), 
    @sTriggerName varchar(1000), 
    @sTriggerTipo varchar(100), 
    @sMensajeTexto varchar(500),  
    @sCampoControl varchar(100), 
    @iCrearTR bit 

    Set @sSql = '' 
    Set @sSql_Aux = '' 
    Set @sSql_Exec = '' 
    Set @sTabla = '' 
	Set @sCampoControl = 'FechaControl'

    
    Set @sMensajeTexto = 'Deshacer la eliminacion de datos ' 
    Set @iCrearTR = 1 

--    Select NombreTabla 
--    From CFGC_EnvioDetalles (NoLock) 
--    Where NombreTabla <> 'Net_Usuarios' 
--    Order By IdOrden -    


    Declare Tabla_TR Cursor For 
        Select NombreTabla 
        From CFGC_EnvioDetalles E (NoLock)  --- CFGC_EnvioDetalles | CFGC_EnvioDetallesTrans 
        Inner Join Sysobjects S (NoLock) On ( E.NombreTabla = S.Name ) 
        Where NombreTabla <> 'Net_Usuarios' 
        Order By IdOrden 
	open Tabla_TR 
	Fetch From Tabla_TR into @sTabla 
	while @@Fetch_status = 0 
		begin 
			Set @sSql_Exec = '' 
			Set @sSql_Aux = '' 
		    Set @sSql = '' -- '----------------------------------------------------------------------------------------------------' + char(13) 
		    Set @sSql = @sSql + 'If Not Exists ( Select So.Name, Sc.Name From Sysobjects So (NoLock) ' + char(10) 
		    Set @sSql = @sSql + space(16) + 'Inner Join Syscolumns sc (NoLock) On ( So.Id = Sc.Id ) '  + char(10) 
		    Set @sSql = @sSql + space(16) + 'Where So.Name = ' + char(39) + @sTabla + char(39) + ' and Sc.Name = ' + char(39) + @sCampoControl + char(39) + ' ) ' + char(10) 
            Set @sSql = replicate('-', len(@sSql) + 5 ) + char(10) + @sSql     
		    Set @sSql = @sSql + '   Alter Table ' + @sTabla + ' Add ' + @sCampoControl + ' DateTime Not Null Default getdate() ' + char(10) 
		    Set @sSql = @sSql + 'Go--#SQL ' 
		    
		   
		    -- Print @sSql 	
		    Print @sSql 
		    Print '' 
		    
		    Fetch next From Tabla_TR into @sTabla 
		end 
    close Tabla_TR  
	deallocate Tabla_TR 




