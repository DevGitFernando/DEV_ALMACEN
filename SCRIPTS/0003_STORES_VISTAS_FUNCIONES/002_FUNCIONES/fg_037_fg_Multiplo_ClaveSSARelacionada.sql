-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_Multiplo_ClaveSSARelacionada' and xType = 'FN' ) 
   Drop Function fg_Multiplo_ClaveSSARelacionada 
Go--#SQL 

Create Function dbo.fg_Multiplo_ClaveSSARelacionada
(
	@IdEstado varchar(2) = '25', @IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '',  @IdProducto varchar(8) = '00000265', @ClaveLote Varchar(30) = '' 
)  
Returns Int  
With Encryption 
As 
Begin 
Declare @imultiplo Int,
		@sIdClaveSSA varchar(50), 
		@sClaveSSA varchar(50),
		@bEsConsigna Bit = 0,
		@AfectaVenta Bit = 0,
		@AfectaConsigna Bit = 0,
		@Afectar Bit = 0
	
	if (@ClaveLote Like '%*%')
	Begin 
		set @bEsConsigna = 1
	End

	Set @sIdClaveSSA = ''
	Set @imultiplo = 1
	
	Select @sIdClaveSSA = IdClaveSSA_Sal
	From vw_Productos (NoLock) Where IdProducto = @IdProducto 

	Select @AfectaVenta = IsNull(AfectaVenta, 0), @AfectaConsigna = IsNull(AfectaConsigna, 0)
	From vw_Relacion_ClavesSSA_Claves C (NoLock) 
	Where C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente and C.IdClaveSSA_Relacionada = @sIdClaveSSA and C.Status = 'A'

	If (@bEsConsigna = 1)
		Set @Afectar = @AfectaConsigna
	ELse
		Set @Afectar = @AfectaVenta
	
	
	If (@Afectar = 1)
	Begin 
		Select @imultiplo = C.Multiplo 
		From vw_Relacion_ClavesSSA_Claves C (NoLock) 
				Where C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
				and C.IdClaveSSA_Relacionada = @sIdClaveSSA and C.Status = 'A' 
	End 
	
	return @imultiplo

End  
Go--#SQL 

