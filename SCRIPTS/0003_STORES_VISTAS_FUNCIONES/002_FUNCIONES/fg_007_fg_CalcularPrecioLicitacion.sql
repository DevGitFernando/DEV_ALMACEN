-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_CalcularPrecioLicitacion' and xType = 'FN' ) 
   Drop Function fg_CalcularPrecioLicitacion 
Go--#SQL

Create Function dbo.fg_CalcularPrecioLicitacion 
(
	@IdEstado varchar(2) = '25', @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0001', @IdProducto varchar(8) = '00000265' 
)  
Returns numeric(14,4) 
With Encryption 
As 
Begin 
Declare @dPrecioVenta numeric(14,4) 
Declare @sIdClaveSSA varchar(50) 
Declare @sClaveSSA varchar(50) 

	Set @dPrecioVenta = 0 
	Set @sIdClaveSSA = '' 
	Select @sIdClaveSSA = IdClaveSSA_Sal, @sClaveSSA = ClaveSSA  
	From vw_Productos (NoLock) Where IdProducto = @IdProducto 
	
	
	If Exists ( Select * From vw_Relacion_ClavesSSA_Claves C (NoLock) 
				Where C.IdEstado = @IdEstado and C.IdClaveSSA_Relacionada = @sIdClaveSSA and C.Status = 'A'  ) 
	Begin 
		Select @sClaveSSA = C.ClaveSSA 
		From vw_Relacion_ClavesSSA_Claves C (NoLock) 
				Where C.IdEstado = @IdEstado and C.IdClaveSSA_Relacionada = @sIdClaveSSA and C.Status = 'A' 
	End 
	
	
	Select @dPrecioVenta = PC.Precio  
	From vw_Claves_Precios_Asignados PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
	---- 2K111121.1310 Jesus Diaz 
	-- Se modifica función para que tome el precio por ClaveSSA ( Todas las Claves Relacionadas )  
	----
	-- Where PC.IdEstado = @IdEstado and PC.IdCliente = @IdCliente and PC.IdSubCliente = @IdSubCliente and PC.IdClaveSSA = @sIdClaveSSA  
	Where PC.IdEstado = @IdEstado and PC.IdCliente = @IdCliente and PC.IdSubCliente = @IdSubCliente and PC.ClaveSSA = @sClaveSSA  and PC.Status = 'A' 
	Set @dPrecioVenta = IsNull(@dPrecioVenta, -1) 
	
	return round(@dPrecioVenta, 4)  
--	return cast(@IdSubCliente as numeric(14,4)) 
End  

Go--#SQL 
 

-------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_ContenidoPaquete_ClaveLicitada' and xType = 'FN' ) 
   Drop Function fg_ContenidoPaquete_ClaveLicitada 
Go--#SQL 

Create Function dbo.fg_ContenidoPaquete_ClaveLicitada
(
	@IdEstado varchar(2) = '25', @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0001',  @IdProducto varchar(8) = '00000265', @ClaveLote Varchar(30) = '' 
)  
Returns numeric(14,4)   
With Encryption 
As 
Begin 
Declare 
	@dContenidoPaquete numeric(14,4), 
	@dContenidoPaquete_Aux numeric(14,4), 
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
	

	Set @dContenidoPaquete = 0 
	Set @dContenidoPaquete_Aux = 0 
	Set @sIdClaveSSA = ''
	Set @sClaveSSA = '' 
	
	Select @sIdClaveSSA = IdClaveSSA_Sal, @sClaveSSA = ClaveSSA, @dContenidoPaquete_Aux = ContenidoPaquete_ClaveSSA 
	From vw_Productos (NoLock) Where IdProducto = @IdProducto 
	Set @dContenidoPaquete = @dContenidoPaquete_Aux  	
	

	Select @AfectaVenta = IsNull(AfectaVenta, 0), @AfectaConsigna = IsNull(AfectaConsigna, 0)
	From vw_Relacion_ClavesSSA_Claves C (NoLock) 
	Where C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente and C.IdClaveSSA_Relacionada = @sIdClaveSSA and C.Status = 'A'

	If (@bEsConsigna = 1)
		Set @Afectar = @AfectaConsigna
	ELse
		Set @Afectar = @AfectaVenta
	
	
	If (@Afectar = 1)
	Begin 
		Select @sClaveSSA = C.ClaveSSA 
		From vw_Relacion_ClavesSSA_Claves C (NoLock) 
				Where C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
				and C.IdClaveSSA_Relacionada = @sIdClaveSSA and C.Status = 'A' 
	End 
	
	Select 
		--@dContenidoPaquete = C.ContenidoPaquete   ---- Contenido Paquete de la Clave SSA 
		@dContenidoPaquete = C.ContenidoPaquete_Licitado 
	From vw_Claves_Precios_Asignados C (NoLock)  
	Where C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
		and C.ClaveSSA  = @sClaveSSA and C.Status = 'A' 
	Set @dContenidoPaquete = IsNull(@dContenidoPaquete, @dContenidoPaquete_Aux) 

	return round(@dContenidoPaquete, 4) 

End  
Go--#SQL 


--------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_ClaveSSABase_ClaveLicitada' and xType = 'FN' ) 
   Drop Function fg_ClaveSSABase_ClaveLicitada 
Go--#SQL

Create Function dbo.fg_ClaveSSABase_ClaveLicitada 
(
	@IdEstado varchar(2) = '25', @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0001', @IdProducto varchar(8) = '00000265', @ClaveLote Varchar(30) = ''
)  
Returns varchar(50)  
With Encryption 
As 
Begin 
Declare
	@ClaveSSA_Base varchar(50),
	@ClaveSSA_Base_Aux varchar(50),  
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

	Set @ClaveSSA_Base = 0  
	Set @ClaveSSA_Base_Aux = 0  	
	Set @sIdClaveSSA = '' 
	Select @sIdClaveSSA = IdClaveSSA_Sal, @ClaveSSA_Base = ClaveSSA, @ClaveSSA_Base_Aux = ClaveSSA 
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
		Select @ClaveSSA_Base = C.ClaveSSA 
		From vw_Relacion_ClavesSSA_Claves C (NoLock) 
				Where C.IdEstado = @IdEstado and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente and C.IdClaveSSA_Relacionada = @sIdClaveSSA and C.Status = 'A' 
	End 
	
	return @ClaveSSA_Base 
--	return cast(@IdSubCliente as numeric(14,4)) 

End  
Go--#SQL 


