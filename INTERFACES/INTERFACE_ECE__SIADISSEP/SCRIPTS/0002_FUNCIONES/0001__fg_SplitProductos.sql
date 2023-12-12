If Exists ( select * From Sysobjects (NoLock) Where Name = 'fg_SplitProductos' and xType = 'TF' ) 
   Drop Function fg_SplitProductos 
Go--#SQL 

/* 
	select * from dbo.fg_SplitProductos('0001*2') 

	select * from dbo.fg_SplitProductos('0001*2|0002*4|0003*6') 
*/ 

Create Function fg_SplitProductos 
(    
	@CadenaDeEntrada varchar(max) 
) 
Returns @Output Table  
(
	IdProducto varchar(20), 
	Cantidad int 
) 
As 
Begin 
Declare 
	@StartIndex int, 
	@EndIndex int,  
	@EndIndex_02 int,  	
	@IdProducto varchar(200), 
	@Cantidad varchar(200), 			
	@Segmento varchar(200), 
	@Character_01 char(1), 
	@Character_02 char(1) 
	
	Set @Character_01 = '|' 
	Set @Character_02 = '*' 
	
	Set @IdProducto = '' 
	Set @Cantidad = '' 
	Set @Segmento = '' 

	Set @StartIndex = 1 

	
	If Substring(@CadenaDeEntrada, LEN(@CadenaDeEntrada) - 1, LEN(@CadenaDeEntrada)) <> @Character_01 
	Begin
		SET @CadenaDeEntrada = @CadenaDeEntrada + @Character_01
	End
 
	
	While charindex(@Character_01, @CadenaDeEntrada) > 0
	Begin
		Set @EndIndex = charindex(@Character_01, @CadenaDeEntrada) 
		Set @Segmento = Substring(@CadenaDeEntrada, @StartIndex, @EndIndex - 1) 
		
		Set @EndIndex_02 = charindex(@Character_02, @Segmento) 		
		
		Set @IdProducto = Substring(@Segmento, @StartIndex, @EndIndex_02 - 1)
		Set @Cantidad = replace(@Segmento, @IdProducto + @Character_02, '')  -- Substring(@Segmento, @EndIndex_02 + 1, len(@Segmento) - (@EndIndex_02 + 1))		

		----Print @Segmento 
		----Print @IdProducto  
		----Print @Cantidad 
	
		Insert Into @Output(IdProducto, Cantidad)
		Select @IdProducto, @Cantidad 

		Set @CadenaDeEntrada = Substring(@CadenaDeEntrada, @EndIndex + 1, len(@CadenaDeEntrada))  
		
	End 
 
	Return 
	
End
Go--#SQL 

