	
Declare 
	@sSql varchar(2000), 
	@sTipoProceso varchar(20),   
	@iTipo int 
	
	Set @sSql = '' 	
	Set @iTipo = 1 
	Set @sTipoProceso = case when @iTipo = 1 then 'ENABLE TRIGGER ' else 'DISABLE TRIGGER ' end 
	
	Declare #cursorTriggers  
	Cursor For 
	select @sTipoProceso +  name + ' ON ' + ( select name from sysobjects P Where T.Parent_obj = P.Id ) + char(10) + char(13) + '' 
	from sysobjects T 
	where xtype = 'TR' 
	Order by crDate 
	Open #cursorTriggers 
	FETCH NEXT FROM #cursorTriggers Into @sSql 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			Exec (@sSql) 
			FETCH NEXT FROM #cursorTriggers Into @sSql   
		END	 
	Close #cursorTriggers 
	Deallocate #cursorTriggers 	

