------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_PrepararTablas' and xType = 'P') 
    Drop Proc spp_Mtto_INT_ND_PrepararTablas
Go--#SQL 
  
--  Exec spp_Mtto_INT_ND_PrepararTablas '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Mtto_INT_ND_PrepararTablas 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD  
Declare 
	@sSql varchar(2000), 
	@sTabla varchar(200),  
	@bEjecutar bit 

	Set @sSql = '' 
	Set @sTabla = '' 	 

	Set @bEjecutar = 0        
	--  0	====> Generar solo script 
	--  1	====> Ejecutar script 

	If Exists ( Select Name From tempdb..sysobjects Where Name like '#tmpLista_Tablas%' and xType = 'U' ) 
	   Drop Table tempdb..#tmpLista_Tablas 

	Select NombreTabla, identity(int, 1, 1) as Orden 
	Into #tmpLista_Tablas 
	From INT_ND_CFG_EnvioInformacion 
	--where 1 = 0 
	Order by IdOrden Desc 
	


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
			Set @sSql = @sSql + ' Delete From ' + @sTabla + ' '  
			
			Exec(@sSql) 
			
			FETCH NEXT FROM #cLimpiar Into @sTabla  
        END 
    Close #cLimpiar 
    Deallocate #cLimpiar  	
	
End  
Go--#SQL 

