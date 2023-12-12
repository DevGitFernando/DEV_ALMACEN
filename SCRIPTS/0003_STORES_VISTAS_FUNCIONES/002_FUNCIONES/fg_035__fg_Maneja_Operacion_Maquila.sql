----------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (nolock) Where Name = 'fg_Maneja_Operacion_Maquila' and xType = 'FN' ) 
   Drop Function fg_Maneja_Operacion_Maquila 
Go--#SQL 

--		select dbo.fg_Maneja_Operacion_Maquila( '010.000.0101.00', 1, 1 ), dbo.fg_Maneja_Operacion_Maquila( '010.000.0101.00', 1, 2 )

Create Function dbo.fg_Maneja_Operacion_Maquila 
(
	@Dato_A_Validar varchar(30), @Opcion tinyint, @Tipo tinyint
)  
Returns varchar(200)  
With Encryption 
As 
Begin 
Declare @iValor int,  
	    @sModulo varchar(5),   
	    @sParametro varchar(50),
	    @Presentacion varchar(200),
	    @ContenidoPaquete int, 
	    @ValorRetorno varchar(200) 

Declare	
	@sDefault_Presentacion varchar(1000), 
	@iDefault_ContenidoPaquete int  


/* 
	Valor = 0 ==>> Operacion Interna 
	Valor = 1 ==>> Operacion Externa	
*/ 

	Set @iValor = 0 
	Set @sModulo = 'PFAR' 
	Set @sParametro = 'INT_OPM_ManejaOperacionMaquila'
	Set @Presentacion = ''	 
	Set @ContenidoPaquete = -1 
	Set @ValorRetorno = '' 
	
	Set @sDefault_Presentacion = '--------------- ERROR ---------------' 
	-- Set @sDefault_Presentacion = '' 	
	Set @iDefault_ContenidoPaquete = -1   
	Set @Presentacion = @sDefault_Presentacion  
	Set @ContenidoPaquete = @iDefault_ContenidoPaquete  	
	
	

	-- Select * From Net_CFGC_Parametros (Nolock) Where ArbolModulo =  'PFAR'  and NombreParametro = 'ManejaOperacionMaquila' 
	
	
	Select @iValor = 
		( 
			---- validar si alguna de las unidades de la operacion tiene activo este parametro 
			Select Case when Valor = 'TRUE' Then 1 Else 0 End As Valor 
			From Net_CFGC_Parametros (Nolock) 
			Where ArbolModulo = @sModulo and NombreParametro = @sParametro and Valor = 'TRUE'
		)
	Set @iValor = IsNull(@iValor, 0) -------- Asegurar funcionalidad para operaciones que no manejan maquila 
	-- Set @iValor = 1 


	------------------------------------------------ Implementa maquila 		
	If @iValor = 1 
		Begin 
			If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND_CFG__ManejoDeClaves_OPM' and xType = 'U' ) 
				Begin 
					Set @iValor = 0 
				End 
			Else 
				Begin 
					If @Opcion = 1  
						Begin ---- Por Clave SSA  
							Select top 1 @Presentacion = T.UnidadDeDispensacion, @ContenidoPaquete = T.ContenidoPaquete 
							From INT_ND_CFG__ManejoDeClaves_OPM T (Nolock) 
							Where 
								-- T.ClaveSSA = @Dato_A_Validar or T.ClaveSSA_Mascara = @Dato_A_Validar 
								( T.ClaveSSA = @Dato_A_Validar or T.ClaveSSA_Mascara = @Dato_A_Validar  ) 
						End 
					Else  
						Begin -----  Por Producto 
							Select top 1 @Presentacion = T.UnidadDeDispensacion, @ContenidoPaquete = T.ContenidoPaquete  
							From INT_ND_CFG__ManejoDeClaves_OPM T (Nolock) 
							Inner Join CatProductos P (nolock) On ( P.IdClaveSSA_Sal = T.IdClaveSSA ) 
							Where P.IdProducto = @Dato_A_Validar 
						End 
				
					Set @Presentacion = IsNull(@Presentacion, @sDefault_Presentacion) 
					Set @ContenidoPaquete = IsNull(@ContenidoPaquete, @iDefault_ContenidoPaquete) 
									
				End 
			
		End 

		
	------------------------------------------------ No implementa maquila 	
	If @iValor = 0
		Begin 
			If @Opcion = 1  
				Begin ---- Por Clave SSA 
					Select @Presentacion = Pp.Descripcion, @ContenidoPaquete = ContenidoPaquete 
					From CatClavesSSA_Sales S (NoLock)
					Inner Join CatPresentaciones Pp (NoLock) On ( S.IdPresentacion = Pp.IdPresentacion )   
					Where S.ClaveSSA = @Dato_A_Validar 
				End 
			Else 
				Begin -----  Por Producto  
					Select @Presentacion = Pp.Descripcion, @ContenidoPaquete = (Case When S.Descomponer = 1 Then S.ContenidoPaquete Else 1 End)  
					From CatProductos S (NoLock)
					Inner Join CatPresentaciones Pp (NoLock) On ( S.IdPresentacion = Pp.IdPresentacion )   
					Where S.IdProducto = @Dato_A_Validar 				
				End 

			Set @Presentacion = IsNull(@Presentacion, @sDefault_Presentacion) 
			Set @ContenidoPaquete = IsNull(@ContenidoPaquete, @iDefault_ContenidoPaquete) 			
		End 
	
	
	------------------------- Revision final 
	If @Tipo = 1
		Begin 
			Set @ValorRetorno = @Presentacion 
		End 
	Else
		Begin 
			Set @ValorRetorno = cast(@ContenidoPaquete as varchar(200)) 
		End 
		

---------------- SALIDA FINAL 	
	-- Set @ValorRetorno = @ValorRetorno + cast(@iValor as varchar(5))
	Set @ValorRetorno = @ValorRetorno 
	
	Return @ValorRetorno 
	
	
End 
Go--#SQL 
