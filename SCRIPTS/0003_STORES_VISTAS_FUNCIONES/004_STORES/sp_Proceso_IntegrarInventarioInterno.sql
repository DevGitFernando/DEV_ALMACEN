If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioInterno' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioInterno 
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioInterno  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', 
	@IdPersonal varchar(6) = '0009', @Tipo int = 1, @TipoInv tinyint = 0   
) 
With Encryption 
As 
Begin 
Set NoCount On 
-- Set Ansi_Warnings Off  --- Especial, peligroso 

		--	@TipoInv = 0 : Inventario Completo
		--	@TipoInv = 1 : Inventario Parcial
Declare 
	@sPoliza_Salida varchar(8), 
	@sPoliza_Generada varchar(8), 	
	@Observaciones varchar(500), 
	@SubTotal numeric(14,4), @Iva numeric(14,4), @Total numeric(14,4), 
	@PolizaAplicada varchar(1), @iOpcion smallint, 
	@iMostrarResultado int, 
	@ManejaUbicaciones tinyint  


	Set @sPoliza_Salida = '*'  
	Set @sPoliza_Generada = '*'  	
	Select @Observaciones = '' 
	Select @SubTotal = 0, @Iva = 0 , @Total = 0  
	Select @PolizaAplicada = 'N', @iOpcion = 1
	Set @iMostrarResultado = 0  
	Set @ManejaUbicaciones = 0 
	
	If @Tipo = 1 
	Begin 
		Select @ManejaUbicaciones = (case when upper(Valor) = 'TRUE' then 1 else 0 end)
		From Net_CFGC_Parametros (NoLock) 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ArbolModulo = 'ALMN' and NombreParametro = 'ManejaUbicaciones' 
		
		Set @ManejaUbicaciones = IsNull(@ManejaUbicaciones, 0) 
	End 
	 


---------------------------------------- Cargar todos los Productos registrados 
--	Select top 1 * 	from CatProductos_Estado 	
---------------------------------------- 


			
--- Se igualan las cantidades de los productos. 
	Exec sp_Proceso_IntegrarInventarioInterno_001 @IdEmpresa, @IdEstado, @IdFarmacia, @ManejaUbicaciones    --- Cuadrar inventario 


--- Se registran : Productos y Ubicaciones 
	Exec sp_Proceso_IntegrarInventarioInterno_002  @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @Tipo, @TipoInv  

--- Se llenan las tablas de Ajuste de Inventario 
	Exec sp_Proceso_IntegrarInventarioInterno_003  @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @sPoliza_Generada output, @Tipo  --- Generar Entrada General 



------------------------------------ Folios generados   
	Select @sPoliza_Generada as Poliza_Generada 
 

End 
Go--#SQL

