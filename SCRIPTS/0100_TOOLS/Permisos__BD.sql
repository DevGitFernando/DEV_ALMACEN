

Declare 
	@sSql varchar(2000), 
	@sTabla varchar(200), 
	@sUsuario varchar(100), 
	@sTipoProceso varchar(20),   
	@iTipo int,  
	@iExec int 	
	
	Set @sTabla = '' 
	Set @sSql = '' 	
	Set @iTipo = 0 
	Set @iExec = 1 
	
	Set @sTipoProceso = case when @iTipo = 1 then 'GRANT ' else 'DENY ' end 
	Set @sUsuario = 'Camarillo'
	
	Declare #cursorTriggers  
	Cursor For 
	select Name   
	from sysobjects T 
	where xtype = 'U ' 
	Order by crDate 
	Open #cursorTriggers 
	FETCH NEXT FROM #cursorTriggers Into  @sTabla
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			Set @sSql = @sTipoProceso + ' UPDATE ON [dbo].[' + @sTabla + '] TO [' + @sUsuario + '] '  
			if @iExec = 1 
				Exec(@sSql) 
			else 	
				Print @sSql 
		
		
			Set @sSql = @sTipoProceso + ' DELETE ON [dbo].[' + @sTabla + '] TO [' + @sUsuario + '] '  
			if @iExec = 1 
				Exec(@sSql) 
			else 	
				Print @sSql 
			
						
			Set @sSql = @sTipoProceso + ' INSERT ON [dbo].[' + @sTabla + '] TO [' + @sUsuario + '] '  
			if @iExec = 1 
				Exec(@sSql) 
			else 	
				Print @sSql 
			
						
			Set @sSql = @sTipoProceso + ' ALTER ON [dbo].[' + @sTabla + '] TO [' + @sUsuario + '] '  
			if @iExec = 1 
				Exec(@sSql) 
			else 	
				Print @sSql 
						
			
			FETCH NEXT FROM #cursorTriggers Into @sTabla
		END	 
	Close #cursorTriggers 
	Deallocate #cursorTriggers 	



/* 
DENY UPDATE ON [dbo].[VentasEnc] TO [Camarillo]
DENY DELETE ON [dbo].[VentasEnc] TO [Camarillo]
DENY INSERT ON [dbo].[VentasEnc] TO [Camarillo]
DENY ALTER ON [dbo].[VentasEnc] TO [Camarillo] 
*/ 


