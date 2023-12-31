If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_FormatoVersion' and xType = 'FN' ) 
   Drop Function dbo.fg_FormatoVersion
Go--#SQL 
--  	select dbo.fg_FormatoVersion( '1.0.20.11' ) 

Create Function dbo.fg_FormatoVersion ( @sVersion varchar(20) = '1.0.20.11' )
returns varchar(20) 
As 
Begin 
Declare 
--	@sVersion varchar(20),  
	@sParte_01 varchar(4), 
	@sParte_02 varchar(4), 
	@sParte_03 varchar(4), 
	@sParte_04 varchar(4),
	@sVersionFinal varchar(20),	
	@sChar varchar(10),		
	@iLargo int, @iPosicion int,   
	@iParte int 

	Set @iLargo = 0 
	Set @iParte = 0 
	Set @iPosicion = 1 
	Set @sParte_01 = '0000' 
	Set @sParte_02 = '0000' 
	Set @sParte_03 = '0000' 
	Set @sParte_04 = '0000' 
	
	
--	Set @sVersion = '1.0.20.11' 	
	Set @iLargo = len(@sVersion) 
	Set @sChar = '' 
	
	While @iPosicion <= @iLargo 
	   Begin 
		  Set @sChar = @sChar + SUBSTRING(@sVersion, @iPosicion, 1) 
	      If SUBSTRING(@sVersion, @iPosicion, 1)  = '.'  
	         Begin 
	            Set @iParte = @iParte + 1 

				Set @sChar = replace(@sChar, '.', '') 
	            If @iParte = 1 
	               -- Set @sParte_01 = left(@sChar + '0000', 4) 
	               Set @sParte_01 = right('0000' + @sChar , 4) 	               
	               
	            If @iParte = 2 
	               -- Set @sParte_02 = left(@sChar + '0000', 4) 
	               Set @sParte_02 = right('0000' + @sChar , 4) 	               
	               
	            If @iParte = 3 
	               Begin 
	                  -- Set @sParte_01 = left(@sChar + '0000', 4) 	               
	                  Set @sParte_03 = right('0000' + @sChar , 4) 
	                  
	                  Set @sParte_04 = right('0000' + SUBSTRING(@sVersion, @iPosicion + 1, @iLargo - @iPosicion), 4) 
	                  -- Set @sParte_04 = left(SUBSTRING(@sVersion, @iPosicion + 1, @iLargo - @iPosicion) + '0000', 4) 
	                  	                  
	               End 
--	            If @iParte = 4 
--	               Set @sParte_04 = left(@sChar + '0000', 4) 
	               
	            Set @sChar = ''    
	         End       
	      
	      Set @iPosicion = @iPosicion + 1 	      
	   End 
	
--	Set @sParte_04 = left(@sChar + '0000', 4) 	
--	Set @sVersionFinal = @sParte_01 + '.' + @sParte_02 + '.' + @sParte_03 + '.' + @sParte_04 
	Set @sVersionFinal = @sParte_01 + @sParte_02 + @sParte_03 + @sParte_04 
	
----	Select @sVersion as Version, @iLargo as Largo, 
----		@sParte_01 as Parte_01, 
----		@sParte_02 as Parte_02, 		 
----		@sParte_03 as Parte_03, 	
----		@sParte_04 as Parte_04, 
----		@sVersionFinal  as versionFinal  	

	return @sVersionFinal 
End
Go--#SQL 

