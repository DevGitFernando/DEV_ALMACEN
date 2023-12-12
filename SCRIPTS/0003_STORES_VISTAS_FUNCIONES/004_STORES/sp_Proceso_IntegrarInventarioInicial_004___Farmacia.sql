If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioInicial_004___Farmacia' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioInicial_004___Farmacia 
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioInicial_004___Farmacia  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182',  
	@IdPersonal varchar(6) = '0001', @Poliza_Salida varchar(20) = '' output  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare @Actualizado smallint 
Declare 
	@sPoliza_Generada varchar(20), 
	@sPoliza_Entrada varchar(20), 
	@PolizaAplicada varchar(1), @iOpcion smallint, 	 	
	@Observaciones varchar(500), 
	@iMostrarResultado int, 
	@MovtoEntrada varchar(10)   
		
Declare  
	@Status varchar(1), 
	@SubTotal Numeric(14,4),
	@Iva Numeric(14,4),
	@Total Numeric(14,4),
	@sSqlTexto varchar(8000),  
	@iLargoFolios int 

	Set @sSqlTexto = '' 
	Set @iMostrarResultado = 0  
	Set @SubTotal = 0.0000
	Set	@Iva = 0.0000
	Set	@Total = 0.0000
	Set @Actualizado = 3  --- Solo se marca para replicacion cuando se termina el Proceso  ( 0 - 3 ) 
	Set @iLargoFolios = 6 
	Set @Status = 'A'  
	Set @iOpcion = 1 
	Set @PolizaAplicada = 'N' 
	Set @Observaciones = '' 	
	Set @MovtoEntrada = '' -- TipoDeInventario


	----------------------------------------------------------------
	-- Se obtienen los Productos, CodigosEAN, Lotes y Ubicaciones --
	----------------------------------------------------------------


------------------------- Productos a Procesar  	
	Select top 1 @MovtoEntrada = (case when TipoDeInventario = 1 then 'II' else 'IIC' end) From INV__InventarioInterno_CargaMasiva (NoLock) 


	Select * 
		-- IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdPasillo, IdEstante, IdEntrepaño, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Caducidad, Cantidad  
	Into #tmpBase 
	From INV__InventarioInterno_CargaMasiva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	--		and CodigoEAN = '7501125195150' 

	
---	sp_listacolumnas INV__InventarioInterno_CargaMasiva 

	--Select * 	From #tmpBase 




	Select Distinct P.* 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN P (NoLock) 
	Inner Join #tmpBase CM (NoLock) On ( P.IdProducto = CM.IdProducto and P.CodigoEAN = CM.CodigoEAN ) 	
	
	Select Distinct P.* 
	Into #vw_Productos 
	From vw_Productos P (NoLock) 
	Inner Join #tmpBase LP (NoLock) On ( P.IdProducto = LP.IdProducto )			
------------------------- Productos a Procesar  	



--------------------------------------------- 	Preparar Productos 
		Select 
			F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdProducto, F.Costo, F.Costo as UltimoCosto, F.Costo as CostoPromedio, F.TasaIVA, 
			cast(0 as numeric(14,4)) as SubTotal, cast(0 as numeric(14,4)) as IVA, cast(0 as numeric(14,4)) as Importe  
		Into #tmpProductos 
		From #tmpBase F (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia ---- And Existencia > 0 
		Group by F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdProducto, F.Costo, F.Costo, F.TasaIVA  


		Select 
			F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdProducto, F.CodigoEAN, sum(F.Cantidad) as Cantidad, 
			max(F.Costo) as Costo, max(F.Costo) as UltimoCosto, max(F.Costo) as CostoPromedio, F.TasaIVA 
		Into #tmpCodigosEAN
		From #tmpBase F (NoLock) 
		Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia ---- And Existencia > 0
		Group by  
			F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdProducto, F.CodigoEAN, F.TasaIVA 
	
		Select 
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, TasaIVA, ClaveLote, max(Costo) as Costo, sum(Cantidad) as Cantidad  
		Into #tmpLotes
		From #tmpBase F (NoLock) 
		Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia ---- And Existencia > 0 
		Group by 
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, TasaIVA, ClaveLote 

--------------------------------------------- 	Preparar Productos 


 	

---------------------------------- GENERAR EL MOVIMIENTO DE INVENTARIO 		

		Select @sPoliza_Generada = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 From MovtosInv_Enc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @MovtoEntrada 

		Set @sPoliza_Generada = IsNull(@sPoliza_Generada, '1') 
		Set @sPoliza_Generada = @MovtoEntrada + right(replicate('0', 8) + @sPoliza_Generada, 8) 
		
		--------------- Se inserta el Encabezado 	
		Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 
			Referencia, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total ) 
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @sPoliza_Generada, @MovtoEntrada, 'E' as IdTipoMovto_Inv, 
			@sPoliza_Generada, @IdPersonal, 'Carga de inventario inicial', sum(SubTotal) as SubTotal, sum(IVA) as Iva, sum(Importe) as Total  
		From #tmpProductos 


		--------------- Se inserta el Detalles
		Insert Into MovtosInv_Det_CodigosEAN( 
			IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, UnidadDeSalida, 
			TasaIva, Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 
		Select 
			IdEmpresa, IdEstado, IdFarmacia, @sPoliza_Generada as FolioMovtoInv, IdProducto, CodigoEAN, 1 as UnidadDeSalida, 
			TasaIva, sum(Cantidad) as Cantidad, max(Costo), (sum(Cantidad) * max(Costo)) as Importe, 0 as Existencia, @Status, @Actualizado   
		From #tmpCodigosEAN F (NoLock)  
		Group by  
			IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, TasaIva  


		----------------- Se inserta el Lote
		Insert Into MovtosInv_Det_CodigosEAN_Lotes( 
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
			Cantidad, Costo, Importe, Existencia, Status, Actualizado)
		Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @sPoliza_Generada as FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
			sum(Cantidad) as Cantidad, max(Costo) as Costo, (sum(Cantidad) * max(Costo)) as Importe, 0 as Existencia, @Status, @Actualizado   
		From #tmpLotes F (NoLock) 
		Group by  
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote 
---------------------------------- GENERAR EL MOVIMIENTO DE INVENTARIO 		


	
	------ Regresar el folio de salida generado  
	Select @Poliza_Salida = @sPoliza_Generada -- as FolioInventario 
	Print @Poliza_Salida 
			

 			
End 
Go--#SQL 

