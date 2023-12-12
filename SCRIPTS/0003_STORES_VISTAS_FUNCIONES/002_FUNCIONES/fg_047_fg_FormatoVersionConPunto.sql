If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_FormatoVersionConPunto' and xType = 'FN' ) 
   Drop Function dbo.fg_FormatoVersionConPunto
Go--#SQL 
--  	select dbo.fg_FormatoVersion( '1.0.20.11' ) 

Create Function dbo.fg_FormatoVersionConPunto ( @sVersion varchar(20) = '1.0.20.11' )
returns varchar(20) 
As 
Begin 
Declare 
	--@sVersion varchar(20) = '0005000000100000',  
	@sParte_01 varchar(4), 
	@sParte_02 varchar(4), 
	@sParte_03 varchar(4), 
	@sParte_04 varchar(4),
	@sVersionFinal varchar(20),
	@iPosicion int
	
	Set @iPosicion = 1

	Set @sParte_01 = SUBSTRING(@sVersion, 1, 4)
	Set @sParte_02 = SUBSTRING(@sVersion, 5, 8)
	Set @sParte_03 = SUBSTRING(@sVersion, 9, 12) 
	Set @sParte_04 = SUBSTRING(@sVersion, 13, 16)
	
	
	Set @sParte_01 = Cast(Cast(@sParte_01 As int) As Varchar(10))
	Set @sParte_02 = Cast(Cast(@sParte_02 As int) As Varchar(10))
	Set @sParte_03 = Cast(Cast(@sParte_03 As int) As Varchar(10))
	Set @sParte_04 = Cast(Cast(@sParte_04 As int) As Varchar(10))
	
	
	--Select @sParte_01, @sParte_02, @sParte_03, @sParte_04 
	
	


	Set @sVersionFinal = @sParte_01 + '.' + @sParte_02 + '.' + @sParte_03 + '.' + @sParte_04 
	
 	

	return @sVersionFinal 
End
Go--#SQL 

