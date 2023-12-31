

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
    @iCrearTR bit 

    Set @sSql = '' 
    Set @sSql_Aux = '' 
    Set @sSql_Exec = '' 
    Set @sTabla = '' 
    Set @sTriggerPrefijo = 'TR_OnDelete_' 
    Set @sTriggerTipo = 'For Delete ' 
    Set @sTriggerName = '' 
    
    Set @sMensajeTexto = 'Deshacer la eliminacion de datos ' 
    Set @iCrearTR = 1 

--    Select NombreTabla 
--    From CFGC_EnvioDetalles (NoLock) 
--    Where NombreTabla <> 'Net_Usuarios' 
--    Order By IdOrden -    


    Declare Tabla_TR Cursor For 
        Select NombreTabla, ( @sTriggerPrefijo + NombreTabla) as TriggerName 
        From CFGC_EnvioDetalles E (NoLock)  --- CFGC_EnvioDetalles | CFGC_EnvioDetallesTrans 
        Inner Join Sysobjects S (NoLock) On ( E.NombreTabla = S.Name ) 
        Where NombreTabla <> 'Net_Usuarios' 
        Order By IdOrden 
	open Tabla_TR 
	Fetch From Tabla_TR into @sTabla, @sTriggerName  
	while @@Fetch_status = 0 
		begin 
			Set @sSql_Exec = '' 
			Set @sSql_Aux = '' 
		    Set @sSql = '' -- '----------------------------------------------------------------------------------------------------' + char(13) 
		    Set @sSql = @sSql + 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + char(39) + @sTriggerName + char(39) + ' and xType = ' + char(39) + 'TR' + char(39) + ' ) ' + char(10) 
            Set @sSql = replicate('-', len(@sSql) + 5 ) + char(10) + @sSql     
		    Set @sSql = @sSql + '   Drop Trigger ' + @sTriggerName + ' ' + char(10) 
		    Set @sSql = @sSql + 'Go--#S#QL ' 
		    
		    If @iCrearTR = 1 
		    Begin 
		        Set @sSql = @sSql + char(10) + char(10) 		    
		        Set @sSql = @sSql + 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + char(39) + @sTabla + char(39) + ' and xType = ' + char(39) + 'U' + char(39) + ' ) ' + char(10) 
		        Set @sSql = @sSql + 'Begin ' 
				
		        Set @sSql_Aux = @sSql_Aux + char(10) -- + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + 'Create Trigger ' + @sTriggerName + ' ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + 'On ' + @sTabla + ' ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + 'With Encryption ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + @sTriggerTipo + ' ' + char(10) -- 'For Delete ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + 'As ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + 'Begin ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + '   --- ' + @sMensajeTexto + ' ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + '   Rollback ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + ' ' + char(13) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + '   --- Enviar el mensaje de error ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + '   Raiserror (' + char(39) + char(39) + 'Esta acci�n no esta permitida para el objeto [ ' + @sTabla + ' ]' + char(39) +  + char(39) +  ', 1, 1) ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(5) + 'End ' + char(10) 
		        --Set @sSql = @sSql + space(5) + 'Go---#S#QL  ' 
		        
		        Set @sSql_Exec = @sSql_Exec + @sSql + char(13) 	        
				Set @sSql_Exec = @sSql_Exec + 'Declare @sSql varchar(max) '  + char(10) 
				Set @sSql_Exec = @sSql_Exec + 'Set @sSql = ' + char(39) + char(39) + char(13)   		        
		        
				Set @sSql_Exec = @sSql_Exec + 'Set @sSql = ' + char(10) + space(5) + char(39) + @sSql_Aux + space(5) + char(39) + char(10) 
				Set @sSql_Exec = @sSql_Exec + 'Exec ( @sSql ) ' + char(10) 
		        Set @sSql_Exec = @sSql_Exec + 'End '+ char(10) 
		        Set @sSql_Exec = @sSql_Exec + 'Go--#S#QL ' 					
					
		        
		        Set @sSql = @sSql + @sSql_Aux + 'End '+ char(10) 
		        Set @sSql = @sSql + 'Go--#S#QL ' 
		        Set @sSql = replace(@sSql, char(39) + '' + char(39), char(39))
		    End 
		    
		    -- Print @sSql 	
		    Print @sSql_Exec 
		    Print '' 
		    
		    Fetch next From Tabla_TR into @sTabla, @sTriggerName  
		end 
    close Tabla_TR  
	deallocate Tabla_TR 




