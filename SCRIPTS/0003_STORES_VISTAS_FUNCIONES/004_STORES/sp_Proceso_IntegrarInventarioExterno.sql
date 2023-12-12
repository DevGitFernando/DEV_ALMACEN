If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioExterno' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioExterno 
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioExterno  
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0044', @IdPersonal varchar(6) = '0001' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
-- Set Ansi_Warnings Off  --- Especial, peligroso 
Declare 
	@sPoliza_Salida varchar(8), 
	@sPoliza_Entrada varchar(8), 	
	@Observaciones varchar(500), 
	@SubTotal numeric(14,4), @Iva numeric(14,4), @Total numeric(14,4), 
	@PolizaAplicada varchar(1), @iOpcion smallint, 
	@iMostrarResultado int, 
	@ManejaUbicaciones tinyint  


	Set @sPoliza_Salida = '*'  
	Set @sPoliza_Entrada = '*'  	
	Select @Observaciones = '' 
	Select @SubTotal = 0, @Iva = 0 , @Total = 0  
	Select @PolizaAplicada = 'N', @iOpcion = 1
	Set @iMostrarResultado = 0  
	Set @ManejaUbicaciones = 1 


---------------------------------------- Cargar todos los Productos registrados 
--	Select top 1 * 	from CatProductos_Estado 	
---------------------------------------- 


			
--- Se igualan las cantidades de los productos. 
	Exec sp_Proceso_IntegrarInventarioExterno_001 @IdEmpresa, @IdEstado, @IdFarmacia, @ManejaUbicaciones    --- Cuadrar inventario 


--- Se genera la salida general. 
	Exec sp_Proceso_IntegrarInventarioExterno_002  @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @sPoliza_Salida output  --- Generar Salida General 
	Exec spp_Mtto_AjustesDeInventario @IdEmpresa, @IdEstado, @IdFarmacia, @sPoliza_Salida, @IdPersonal, @iMostrarResultado  ---- SALIDA  


--- Se genera la salida general. 
	Exec sp_Proceso_IntegrarInventarioExterno_003  @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @sPoliza_Entrada output  --- Generar Entrada General 
	Exec spp_Mtto_AjustesDeInventario @IdEmpresa, @IdEstado, @IdFarmacia, @sPoliza_Entrada, @IdPersonal, @iMostrarResultado  ---- SALIDA  


------------------------------------ Folios generados   
	Select @sPoliza_Salida as Folio_Salida, @sPoliza_Entrada as Folio_Entrada  

	
----	Insert Into tmpAvance_CierreInventario ( Descripcion, Porcentaje ) Select 'Generando inventarios iniciales', 65   
----	Exec sp_Proceso_IntegrarInventarioExterno_003  @IdEmpresa, @IdEstado, @IdFarmacia, @IdFarmaciaNueva, 
----		@FolioSalidaVenta, @FolioSalidaConsignacion, @IdPersonal, @FechaSistema

 

End 
Go--#SQL

