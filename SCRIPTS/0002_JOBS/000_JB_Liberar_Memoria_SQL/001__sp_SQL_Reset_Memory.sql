
Use Master 
Go--#SQL 

If Exists ( Select * From Sysobjects where name = 'sp_SQL_Reset_Memory' and xType = 'P' ) 
   Drop Proc sp_SQL_Reset_Memory 
Go--#SQL    
  
Create Proc sp_SQL_Reset_Memory ( @MemoriaMaxima int = 4, @Minutos int = 0, @Segundos int = 10 )   
As 
Begin   
Declare   
	@iMinimo int,    
	@iMaximo int,   
	@iMinutos int,    
	@iSegundos int,     
	@sMinimo varchar(20),    
	@sMaximo varchar(20),    
	@sTiempo varchar(20),    
	@iGB int, 
	@iError int    
   
    Set @iError = 0 
	Set @iGB = (1024 * 1)   
	Set @iMinimo = @iGB * 0.25   
	Set @iMaximo = @iGB * @MemoriaMaxima      
	Set @sMinimo = cast(@iMinimo as varchar)   
	Set @sMaximo = cast(@iMaximo as varchar)    
   
	if @Minutos <= 0 Set @Minutos = 1   
	if @Minutos > 5 Set @Minutos = 1    

	if @Segundos <= 0 Set @Segundos = 0   
	if @Segundos > 59 Set @Segundos = 0     
    
   
	Set @sTiempo = '00:' + right('0000' + cast(@Minutos as varchar), 2) + ':'+ right('0000' + cast(@Segundos as varchar), 2)   

  
	Begin Try    
		Exec sys.sp_configure 'show advanced options', '1'     
		Reconfigure With Override       
		Exec sys.sp_configure 'max server memory (MB)', @sMinimo    -- N'12000' --- Maximo (para hacerlo bajar del Task Manager)   Reconfigure With Override   
		Exec sys.sp_configure 'show advanced options', '0'     
		Reconfigure With Override    
		WAITFOR DELAY @sTiempo -- '00:01:50'      
	End Try   
	Begin Catch   
		Exec sys.sp_configure 'show advanced options', '1'    
		Reconfigure With Override      
		Exec sys.sp_configure 'max server memory (MB)', @sMaximo   ---- N'44000'   
		Reconfigure With Override      
		Exec sys.sp_configure 'show advanced options', '0'    
		Reconfigure With Override     	
		
		Set @iError = 1 
	End Catch     -- Volvemos a establecer los valores normales seteados  

	if @iError = 0 
	Begin 
		Exec sys.sp_configure 'show advanced options', '1'    
		Reconfigure With Override      
		Exec sys.sp_configure 'max server memory (MB)', @sMaximo   ---- N'44000'   
		Reconfigure With Override      
		Exec sys.sp_configure 'show advanced options', '0'    
		Reconfigure With Override     
	End 

End 	
	
Go--#SQL 

 
 