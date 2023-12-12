If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Marcar_Replicacion_BD_Completa' and xType = 'P' ) 
   Drop Proc spp_Mtto_Marcar_Replicacion_BD_Completa 
Go--#SQL 

--		Exec spp_Mtto_Marcar_Replicacion_BD_Completa 1    

Create Proc spp_Mtto_Marcar_Replicacion_BD_Completa ( @bEjecutar bit = 1 )   
With Encryption 
As 
Begin 
Set NoCount On 

Declare  
	@sSql varchar(2000), 
	@sTabla varchar(200),  
	@bEjecutar_x bit 
	
	Set @sSql = '' 
	Set @sTabla = '' 	 

	---- Set @bEjecutar = 0     
	--  0	====> Generar solo script 
	--  1	====> Ejecutar script 	
	
----	If Exists ( Select Name From tempdb..sysobjects Where Name like '#tmpLista_Tablas%' and xType = 'U' ) 
----	   Drop Table tempdb..#tmpLista_Tablas 

	Select NombreTabla, identity(int, 1, 1) as Orden 
	Into #tmpLista_Tablas 
	From CFGC_EnvioDetallesTrans 
	Order by IdOrden Desc 

	Insert Into #tmpLista_Tablas ( NombreTabla ) 
	Select NombreTabla 
	From CFGC_EnvioDetalles 
	Order by IdOrden Desc 

	Delete From #tmpLista_Tablas Where NombreTabla = 'Net_Usuarios' 
	
	
    Declare #cLimpiar 
    Cursor For 
		Select NombreTabla 
		From #tmpLista_Tablas   
		Order by Orden   
    Open #cLimpiar 
    FETCH NEXT FROM #cLimpiar Into @sTabla 
        WHILE @@FETCH_STATUS = 0 
        BEGIN 
			-- Print @sTabla 
			Set @sSql = 'If Exists ( Select Name From Sysobjects (NoLock) Where Name = ' + char(39) + @sTabla + char(39) + ' and xType = ' + char(39) + 'U' + char(39) + ' ) '  			
			Set @sSql = @sSql + 'Update ' + @sTabla +  ' Set Actualizado = 0    '
			
			
			If @bEjecutar = 1  
			   Exec(@sSql)   
			Else  
			   Print @sSql    
			
			
			FETCH NEXT FROM #cLimpiar Into @sTabla  
        END 
    Close #cLimpiar 
    Deallocate #cLimpiar  

End 
Go--#SQL    

    