

---- Comentariar todo el Script 
Declare 
    @sSql varchar(max), 
    @sSql_Aux varchar(max), 
    @sSql_Exec varchar(max),             
    @sTabla varchar(200), 
    @sTriggerPrefijo varchar(100), 
    @sTriggerPosfijo varchar(100),     
    @sTriggerName varchar(1000), 
    @sTriggerTipo varchar(100), 
    @sMensajeTexto varchar(500), 
    @sCamposRelacion varchar(max),  
    @iCrearTR bit 

    Set @sSql = '' 
    Set @sSql_Aux = '' 
    Set @sSql_Exec = '' 
    Set @sTabla = '' 
    Set @sCamposRelacion = 'campos' 
    Set @sTriggerPrefijo = 'TR_OnUpdate__' 
    Set @sTriggerPosfijo = '__FechaControl' 
    Set @sTriggerTipo = 'For Update ' 
    Set @sTriggerName = '' 
    
    Set @sMensajeTexto = 'Deshacer la actualización de datos ' 
    Set @iCrearTR = 1 

--    Select NombreTabla 
--    From CFGC_EnvioDetalles (NoLock) 
--    Where NombreTabla <> 'Net_Usuarios' 
--    Order By IdOrden -    


    Declare Tabla_TR Cursor For 
        Select top 10000 NombreTabla, ( @sTriggerPrefijo + NombreTabla + @sTriggerPosfijo ) as TriggerName 
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
		    Set @sSql = @sSql + 'Go--#SQL ' 
		    
		    If @iCrearTR = 1 
		    Begin 
				Set @sCamposRelacion = ''  
				-- Set @sTabla = 'CtlErrores'
				Exec spp_GetCampos @sTabla, 'U', 'I', @sCamposRelacion output 
		    
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
		        
		        Set @sSql_Aux = @sSql_Aux + space(10) + ' ' + char(13) 
		        Set @sSql_Aux = @sSql_Aux + space(10) + 'If Update(FechaControl) ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(13) + 'Begin ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(13) + '   --- ' + @sMensajeTexto + ' ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(13) + '   Rollback ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(13) + ' ' + char(13) 
		        Set @sSql_Aux = @sSql_Aux + space(13) + '   --- Enviar el mensaje de error ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(13) + '   Raiserror (' + char(39) + char(39) + 'Esta acción no esta permitida para FechaControl' + char(39) +  + char(39) +  ', 1, 1) ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(13) + 'End ' + char(10) 		        
		        Set @sSql_Aux = @sSql_Aux + space(10) + 'Else ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(13) + 'Begin ' + char(10) 		        
		        Set @sSql_Aux = @sSql_Aux + space(13) + '   Update U Set FechaControl = dbo.fg_FormatearFechaControl() ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(13) + '   From ' + @sTabla + ' U ' + char(10) 
		        Set @sSql_Aux = @sSql_Aux + space(13) + '   Inner Join Inserted I ' + char(10)	
		        Set @sSql_Aux = @sSql_Aux + space(13) + '		On ( ' + @sCamposRelacion + ' ) ' + char(10)			        
		        Set @sSql_Aux = @sSql_Aux + space(13) + 'End ' + char(10) 
		        
		        
		        Set @sSql_Aux = @sSql_Aux + space(5) + 'End ' + char(10) 
		        --Set @sSql = @sSql + space(5) + 'Go---#S#QL  ' 
		        
		        Set @sSql_Exec = @sSql_Exec + @sSql + char(13) 	        
				Set @sSql_Exec = @sSql_Exec + 'Declare @sSql varchar(max) '  + char(10) 
				Set @sSql_Exec = @sSql_Exec + 'Set @sSql = ' + char(39) + char(39) + char(13)   		        
		        
				Set @sSql_Exec = @sSql_Exec + 'Set @sSql = ' + char(10) + space(5) + char(39) + @sSql_Aux + space(5) + char(39) + char(10) 
				Set @sSql_Exec = @sSql_Exec + 'Exec ( @sSql ) ' + char(10) 
		        Set @sSql_Exec = @sSql_Exec + 'End '+ char(10) 
		        Set @sSql_Exec = @sSql_Exec + 'Go--#SQL ' 					
					
		        
		        Set @sSql = @sSql + @sSql_Aux + 'End '+ char(10) 
		        Set @sSql = @sSql + 'Go--#SQL ' 
		        Set @sSql = replace(@sSql, char(39) + '' + char(39), char(39))
		    End 
		    
		    -- Print @sSql 	
		    Print @sSql_Exec 
		    Print '' 
		    
		    Fetch next From Tabla_TR into @sTabla, @sTriggerName  
		end 
    close Tabla_TR  
	deallocate Tabla_TR 




