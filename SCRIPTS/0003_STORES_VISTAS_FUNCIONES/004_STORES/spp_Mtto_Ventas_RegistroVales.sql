------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Mtto_Ventas_RegistroVales' and xType = 'P' )
    Drop Proc spp_Mtto_Ventas_RegistroVales 
Go--#SQL
  
Create Proc spp_Mtto_Ventas_RegistroVales 
(	
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0101', 
    @Folio_Vale_Registro varchar(30) = '00000001', -- @FolioVale_Emitido varchar(30) = '00000001', 
    @FolioVenta varchar(30) = '00000001', @IdPersonal varchar(4) = '0001', 
    @TieneFolioDeVenta bit = 1 
 )
With Encryption 
As
Begin 
Set NoCount On 

Declare @FolioVentaNuevo varchar(30), 
        @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@Descuento numeric(14,4), 
		@IdCaja varchar(2), 
		@TipoDispensacion varchar(2),  
		@iValor numeric(14,4), 
		@FolioMovtoInv varchar(22), 	
		@TipoMovto varchar(5), 
		@TipoES varchar(2), 
		@IdCliente varchar(4), 
		@IdSubCliente varchar(4) 	 
		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	
	Set @FolioMovtoInv = '0'
	Set @Descuento = 0
	Set @IdCaja = '01' 
	Set @TipoDispensacion = '06' 
	Set @iValor = 0 
	Set @IdCliente = '0002' 
	Set @IdSubCliente = '0002' 	
	
	Set @TipoMovto = 'SV' 
	Set @TipoES = 'S' 
	
	-- Set @IdPaciente = '01' 
-------------------------------------------------- Obtener Tipo de Dispensacion por Vales 
	Select @TipoDispensacion = dbo.fg_GetParametro_TipoDispensacion_Vale( @IdEstado, @IdFarmacia ) 
-------------------------------------------------- Obtener Tipo de Dispensacion por Vales 

-------------------------------------------------- Generar Folios 	
--- Armar folio de venta 
    Select @FolioVentaNuevo = cast( (max(FolioVenta) + 1) as varchar)  From VentasEnc (NoLock) 
    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and @TieneFolioDeVenta = 1  

	-- Asegurar que FolioVenta sea valido y formatear la cadena 
	Set @FolioVentaNuevo = IsNull(@FolioVentaNuevo, '1')
	Set @FolioVentaNuevo = right(replicate('0', 8) + @FolioVentaNuevo, 8) 	
--- Armar folio de venta 

--- Armar folio de inventario 
    Select @FolioMovtoInv = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 From MovtosInv_Enc (NoLock) 
    Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @TipoMovto and @TieneFolioDeVenta = 1  

    ---- Actualizar el registro de folios 
    --Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = cast(IsNull(@FolioMovtoInv, 1) as int), Actualizado = 0 
    --Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdTipoMovto_Inv 
	   
	Set @FolioMovtoInv = IsNull(@FolioMovtoInv, '1') 
	Set @FolioMovtoInv = @TipoMovto + right(replicate('0', 8) + @FolioMovtoInv, 8) 
--- Armar folio de inventario 
-------------------------------------------------- Generar Folios 	


-------------------------------------------------- Generar Ventas 
   	Select IdEmpresa, IdEstado, IdFarmacia, @FolioVentaNuevo as FolioVenta, '' as FolioMovtoInv, 
    	       dbo.fg_GetParametro_FechaSistema(IdEstado, IdFarmacia) as FechaSistema, 
    	       getdate() as FechaRegistro, 0 as FolioCierre, 0 as Corte, 
    	       @IdCaja as IdCaja, @IdPersonal as IdPersonal, 
    	       IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
    	       0 as SubTotal, 0 as Descuento, 0 as Iva, 0 as Total, TipoDeVenta, 'A' as Status, 0 as Actualizado 
    Into #tmpVentas  
    From VentasEnc (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioVenta = @FolioVenta 
	      and @TieneFolioDeVenta = 1  
	

    Select IdEmpresa, IdEstado, IdFarmacia, @FolioVentaNuevo as FolioVenta, 
           IdBeneficiario, @TipoDispensacion as IdTipoDeDispensacion, NumReceta, FechaReceta, 
           IdMedico, IdBeneficio, IdDiagnostico, IdUMedica, IdServicio, IdArea, 
           left(RefObservaciones, 100) as RefObservaciones, 
           Status, Actualizado 
    Into #tmpVentas_Inf  
	From VentasInformacionAdicional (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioVenta = @FolioVenta 
	      and @TieneFolioDeVenta = 1 
	
	
------------ Vales sin venta relacionada  		
--- Jesus Diaz 2K111217.1056 
	If @TieneFolioDeVenta = 0 
	Begin 
        Insert Into #tmpVentas 
       	Select IdEmpresa, IdEstado, IdFarmacia, @FolioVentaNuevo as FolioVenta, '' as FolioMovtoInv, 
	       dbo.fg_GetParametro_FechaSistema(IdEstado, IdFarmacia) as FechaSistema, 
	       getdate() as FechaRegistro, 0 as FolioCierre, 0 as Corte, 
	       @IdCaja as IdCaja, @IdPersonal as IdPersonal, 
	       IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
	       0 as SubTotal, 0 as Descuento, 0 as Iva, 0 as Total, 2 as TipoDeVenta, 'A' as Status, 0 as Actualizado 
	    From Vales_EmisionEnc E (NoLock) 
	    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and FolioVale in
			( 
				Select FolioVale 
				From ValesEnc V 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				and Folio = @Folio_Vale_Registro  	     			
			) 


        Insert Into #tmpVentas_Inf   
        Select IdEmpresa, IdEstado, IdFarmacia, @FolioVentaNuevo as FolioVenta, 
               IdBeneficiario, @TipoDispensacion as IdTipoDeDispensacion, NumReceta, FechaReceta, 
               IdMedico, IdBeneficio, IdDiagnostico, IdUMedica, IdServicio, IdArea, 
               left(RefObservaciones, 100) as RefObservaciones, 
               Status, Actualizado 
	    From Vales_Emision_InformacionAdicional (NoLock) 
	    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and FolioVale in
			( 
				Select FolioVale
				From ValesEnc V 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				and Folio = @Folio_Vale_Registro  	     			
			) 
	End 
------------ Vales sin venta relacionada  		
	
	
---	select * 	from #tmpVentas_Inf 
	
	
	Select V.IdEmpresa, V.IdEstado, V.IdFarmacia, @FolioVentaNuevo as FolioVenta, 
	    V.IdProducto, V.CodigoEAN, V.Renglon, V.UnidadDeEntrada as UnidadDeSalida, 
	    V.Cant_Recibida as Cant_Entregada, V.Cant_Devuelta, V.Cant_Recibida as CantidadVendida, 
	    dbo.fg_ObtenerCostoPromedio(V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.IdProducto) as CostoUnitario, 
	    dbo.fg_CalcularPrecioVenta(V.IdEstado, @IdCliente, @IdSubCliente, V.IdProducto) as PrecioLicitacion, 
	    dbo.fg_CalcularPrecioVenta(1, @IdCliente, @IdSubCliente, V.IdProducto) as PrecioUnitario, 
	    P.TasaIva as TasaIva, @iValor as ImpteIva, @iValor as PorcDescto, @iValor as ImpteDescto, 'A' as Status, 0 as Actualizado 	
    Into #tmpVentas_Detalles   
	From ValesDet V (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( V.IdProducto = P.IdProducto and V.CodigoEAN = P.CodigoEAN )
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and V.Folio = @Folio_Vale_Registro 	
		and @TieneFolioDeVenta = 1 
	

	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioVentaNuevo as FolioVenta, 
	    IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, 
	    Cant_Recibida as Cant_Vendida, Cant_Devuelta, CantidadRecibida as CantidadVendida, 
	    'A' as Status, 0 as Actualizado 	
    Into #tmpVentas_Detalles_Lotes    
	From ValesDet_Lotes V (NoLock) 
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and V.Folio = @Folio_Vale_Registro 	
		and @TieneFolioDeVenta = 1  
-------------------------------------------------- Generar Ventas 


-------------------------------------------------- Generar Inventarios 
--  IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
    Select 
        @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
        @FolioMovtoInv as FolioMovtoInv, @TipoMovto as IdTipoMovto_Inv, @TipoES as TipoES, 
        getdate() as FechaSistema, getdate() as FechaRegistro, '' as Referencia, 'N' as MovtoAplicado, 
        @IdPersonal as IdPersonalRegistra, '' as Observaciones, 
        @iValor as SubTotal, @iValor as Iva, @iValor as Total, 'A' as Status, 0 as Actualizado 
    Into #tmpMovtos     
    
    
    Select 
        IdEmpresa, IdEstado, IdFarmacia, @FolioMovtoInv as FolioMovtoInv, 
        IdProducto, CodigoEAN, getdate() as FechaSistema, UnidadDeSalida, 
        TasaIva, CantidadVendida as Cantidad, 
        CostoUnitario as Costo, (CostoUnitario * CantidadVendida)as Importe, 
        0 as Existencia, 'A' as Status, 0 as Actualizado 
    Into #tmpMovtos_Detalles 
    From #tmpVentas_Detalles 
    Where @TieneFolioDeVenta = 1 
    
    Select 
        IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioMovtoInv as FolioMovtoInv, 
        IdProducto, CodigoEAN, ClaveLote, EsConsignacion, CantidadVendida as Cantidad,  
        @iValor as Costo, @iValor as Importe, 0 as Existencia, 'A' as Status, 0 as Actualizado 
    Into #tmpMovtos_Detalles_Lotes
    From #tmpVentas_Detalles_Lotes    
	Where @TieneFolioDeVenta = 1  
-------------------------------------------------- Generar Inventarios 


----------------------------------- Inserciones  
----------------------- Ventas   
    Insert Into VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioMovtoInv, FechaSistema, FechaRegistro, FolioCierre, Corte, IdCaja, IdPersonal, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, SubTotal, Descuento, Iva, Total, TipoDeVenta, Status, Actualizado  )
    Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioMovtoInv, FechaSistema, FechaRegistro, FolioCierre, Corte, IdCaja, IdPersonal, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, SubTotal, Descuento, Iva, Total, TipoDeVenta, Status, Actualizado 
    From #tmpVentas     
	Where @TieneFolioDeVenta = 1  
    
    Insert Into VentasInformacionAdicional ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdBeneficiario, IdTipoDeDispensacion, NumReceta, FechaReceta, IdMedico, IdBeneficio, IdDiagnostico, IdUMedica, IdServicio, IdArea, RefObservaciones, Status, Actualizado ) 
    Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdBeneficiario, IdTipoDeDispensacion, NumReceta, FechaReceta, IdMedico, IdBeneficio, IdDiagnostico, IdUMedica, IdServicio, IdArea, RefObservaciones, Status, Actualizado 
    From #tmpVentas_Inf 
	Where @TieneFolioDeVenta = 1 
    
    Insert Into VentasDet ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon, UnidadDeSalida, Cant_Entregada, Cant_Devuelta, CantidadVendida, CostoUnitario, PrecioLicitacion, PrecioUnitario, TasaIva, ImpteIva, PorcDescto, ImpteDescto, Status, Actualizado ) 
    Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon, UnidadDeSalida, Cant_Entregada, Cant_Devuelta, CantidadVendida, CostoUnitario, PrecioLicitacion, PrecioUnitario, TasaIva, ImpteIva, PorcDescto, ImpteDescto, Status, Actualizado 
    From #tmpVentas_Detalles 
	Where @TieneFolioDeVenta = 1 
    
    Insert Into VentasDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, Cant_Vendida, Cant_Devuelta, CantidadVendida, Status, Actualizado ) 
    Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, Cant_Vendida, Cant_Devuelta, CantidadVendida, Status, Actualizado 
    From #tmpVentas_Detalles_Lotes   
	Where @TieneFolioDeVenta = 1 
----------------------- Ventas   


----------------------- Movtos 
    Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, Referencia, MovtoAplicado, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, Actualizado ) 
    Select IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, Referencia, MovtoAplicado, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, Actualizado 
    From #tmpMovtos 
	Where @TieneFolioDeVenta = 1 
    
    Insert Into MovtosInv_Det_CodigosEAN ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, FechaSistema, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 
    Select IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, FechaSistema, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, Existencia, Status, Actualizado
    From #tmpMovtos_Detalles 
	Where @TieneFolioDeVenta = 1 
    
    Insert Into MovtosInv_Det_CodigosEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Cantidad, Costo, Importe, Existencia, Status, Actualizado ) 
    Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Cantidad, Costo, Importe, Existencia, Status, Actualizado
    From #tmpMovtos_Detalles_Lotes    
	Where @TieneFolioDeVenta = 1 
----------------------- Movtos 
----------------------------------- Inserciones  

-------------------- Aplicar Existencia   
	If @TieneFolioDeVenta = 1  
		Begin 
			Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovtoInv, 1, 0  
		End 
-------------------- Aplicar Existencia   


--      spp_Mtto_Ventas_RegistroVales  

--- 	
	Select @FolioVentaNuevo as FolioNuevo, @FolioMovtoInv as FolioInventario Where @TieneFolioDeVenta = 1   


 	
--	Select * From  #tmpVentas
--	Select * From  #tmpVentas_Detalles
--	Select * From  #tmpVentas_Detalles_Lotes 

 	
	
--	Select * From  #tmpMovtos
--	Select * From  #tmpMovtos_Detalles
--	Select * From  #tmpMovtos_Detalles_Lotes 		


		
/* 	
     select top 1 * from VentasEnc  
     select top 1 * from ValesDet_Lotes  	
     
     
     select top 10 * from MovtosInv_Enc  
     select top 1 * from MovtosInv_Det_CodigosEAN  	
     select top 1 * from MovtosInv_Det_CodigosEAN_Lotes   	          
*/ 	
	
End 
Go--#SQL 

--    sp_listacolumnas MovtosInv_Det_CodigosEAN_Lotes        

