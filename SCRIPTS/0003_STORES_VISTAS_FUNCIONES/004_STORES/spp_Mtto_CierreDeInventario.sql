If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CierreDeInventario' and xType = 'P' ) 
   Drop Proc spp_Mtto_CierreDeInventario 
Go--#SQL 

Create Proc spp_Mtto_CierreDeInventario  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0188',
	@IdEmpresaNueva Varchar(3) = '002', @IdFarmaciaNueva varchar(4) = '0200', 
	@IdPersonal varchar(6) = '0001', @FechaSistema varchar(10) = '2011-11-25', @ManejaUbicaciones tinyint = 1
) 
With Encryption 
As 
Begin 
Set NoCount On 
-- Set Ansi_Warnings Off  --- Especial, peligroso 

Declare 
	@sBD varchar(100) 

Declare @FolioSalidaVenta varchar(30), 
		@FolioSalidaConsignacion varchar(30),
		@FolioEntradaVenta varchar(30), 
		@FolioEntradaConsignacion varchar(30) 

	Set @FolioSalidaVenta = 0
	Set @FolioSalidaConsignacion = 0
	Set @sBD = db_name() 

------------------------------ Crear las tablas para almacenar los Folios Generados    
	If Exists ( Select Name From tempdb..Sysobjects (NoLock) Where Name like '#tmpFoliosCierre%' and xType = 'U' ) 
		Drop Table tempdb..#tmpFoliosCierre 	
		
	If Exists ( Select Name From tempdb..Sysobjects (NoLock) Where Name like 'tmpFoliosCierre_InventarioInicial%' and xType = 'U' ) 
		Drop Table tempdb..#tmpFoliosCierre_InventarioInicial 			


	Select top 0 space(20) as FolioSalidaVenta,  space(20) as FolioSalidaConsignacion
	Into #tmpFoliosCierre 
	
	Select top 0 space(20) as FolioEntradaVenta, space(20) as FolioEntradaConsignacion
	Into #tmpFoliosCierre_InventarioInicial 
------------------------------ Crear las tablas para almacenar los Folios Generados    


	Truncate Table tmpAvance_CierreInventario 

			
--- Se igualan las cantidades de los productos. 
	Insert Into tmpAvance_CierreInventario ( Descripcion, Porcentaje ) Select 'Generando cierre de inventario', 25   
	Exec spp_Mtto_CierreDeInventario_001 @IdEmpresa, @IdEstado, @IdFarmacia, @ManejaUbicaciones


--- Se cierra el inventario de la Farmacia.
	Insert Into tmpAvance_CierreInventario ( Descripcion, Porcentaje ) Select 'Generando cierre de inventario', 45   
	Exec spp_Mtto_CierreDeInventario_002  @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @FechaSistema


 
--- Se obtienen los Folios de Salida del Cierre de Inventario.
	Select @FolioSalidaVenta = FolioSalidaVenta, @FolioSalidaConsignacion = FolioSalidaConsignacion
	From #tmpFoliosCierre (NoLock)
	Set @FolioSalidaVenta = IsNull(@FolioSalidaVenta, '') 
	Set @FolioSalidaConsignacion = IsNull(@FolioSalidaConsignacion, '') 

	
--- Se generan los movimientos de Inventario Inicial de la Farmacia Nueva. 
--	select @IdEmpresa, @IdEstado, @IdFarmacia, @IdFarmaciaNueva, 		@FolioSalidaVenta, @FolioSalidaConsignacion, @IdPersonal, @FechaSistema 

	Insert Into tmpAvance_CierreInventario ( Descripcion, Porcentaje ) Select 'Generando inventarios iniciales', 65   
	Exec spp_Mtto_CierreDeInventario_003  @IdEmpresa, @IdEstado, @IdFarmacia, @IdEmpresaNueva, @IdFarmaciaNueva, 
		@FolioSalidaVenta, @FolioSalidaConsignacion, @IdPersonal, @FechaSistema

	
--- Se obtienen los Folios de Salida del Cierre de Inventario.
	Select @FolioEntradaVenta = FolioEntradaVenta, @FolioEntradaConsignacion = FolioEntradaConsignacion 
	From #tmpFoliosCierre_InventarioInicial (NoLock) 
	Set @FolioEntradaVenta = IsNull(@FolioEntradaVenta, '') 
	Set @FolioEntradaConsignacion = IsNull(@FolioEntradaConsignacion, '') 


----- Se elimina la informacion de la empresa que se esta cerrando.
--	Insert Into tmpAvance_CierreInventario ( Descripcion, Porcentaje ) Select 'Depurando información', 75   
--	Exec spp_Mtto_CierreDeInventario_004 @IdEmpresa, @IdEstado, @IdFarmacia, @IdFarmaciaNueva, 75  
 

	-- Se devuelven los Folios.
--	Insert Into tmpAvance_CierreInventario ( Descripcion, Porcentaje ) Select 'Proceso terminado', 100   	
	Select	@FolioSalidaVenta as FolioSalidaVenta, @FolioSalidaConsignacion as FolioSalidaConsignacion, 
			@FolioEntradaVenta as FolioEntradaVenta, @FolioEntradaConsignacion as FolioEntradaConsignacion


End 
Go--#SQL

