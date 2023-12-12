If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioExterno_002' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioExterno_002 
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioExterno_002  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182',  
	@IdPersonal varchar(6) = '0001', @Poliza_Salida varchar(10) = '' output  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare @Actualizado smallint 
Declare 
	@sPoliza_Salida varchar(8), 
	@sPoliza_Entrada varchar(8),
	@PolizaAplicada varchar(1), @iOpcion smallint, 	 	
	@Observaciones varchar(500), 
	@iMostrarResultado int  
		
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
	Set @Observaciones = 'Salida general por toma de inventario de almacen'
	
	------------------------------------------------------------------------------------------
	-- Se actualiza el Status de los Productos para que no se puede hacer ningun movimiento --
	------------------------------------------------------------------------------------------
	Update FarmaciaProductos Set Status = 'I'
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	----------------------------------------------------------------
	-- Se obtienen los Productos, CodigosEAN, Lotes y Ubicaciones --
	----------------------------------------------------------------

------------------------------ 	Preparar Productos 
	Select	F.*, P.TasaIva, Cast(0.0000 as Numeric(14,4)) as SalidaVenta, Cast(0.000 as Numeric(14,4)) as SalidaConsignacion,
			Cast(0.0000 as Numeric(14,4)) as SubTotalVenta, Cast(0.0000 as Numeric(14,4)) as IvaVenta, Cast(0.0000 as Numeric(14,4)) as TotalVenta,
			Cast(0.0000 as Numeric(14,4)) as SubTotalConsignacion, Cast(0.0000 as Numeric(14,4)) as IvaConsignacion, Cast(0.0000 as Numeric(14,4)) as TotalConsignacion
	Into #tmpProductos
	From FarmaciaProductos F(NoLock) 
	Inner Join vw_Productos P(NoLock) On ( F.IdProducto = P.IdProducto ) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And Existencia > 0

	Select	F.*, Cast(0.0000 as Numeric(14,4)) as Costo, P.TasaIva, 
			Cast(0.000 as Numeric(14,4)) as SalidaVenta,  Cast(0.000 as Numeric(14,4)) as SalidaConsignacion,
			Cast(0.0000 as Numeric(14,4)) as SubTotalVenta, Cast(0.0000 as Numeric(14,4)) as IvaVenta, Cast(0.0000 as Numeric(14,4)) as TotalVenta,
			Cast(0.0000 as Numeric(14,4)) as SubTotalConsignacion, Cast(0.0000 as Numeric(14,4)) as IvaConsignacion, Cast(0.0000 as Numeric(14,4)) as TotalConsignacion
	Into #tmpCodigosEAN
	From FarmaciaProductos_CodigoEAN F(NoLock) 
	Inner Join vw_Productos_CodigoEAN P(NoLock) On ( F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN ) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And Existencia > 0

	Select	F.*, Cast(0.0000 as Numeric(14,4)) as Costo, P.TasaIva 
	Into #tmpLotes 
	From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
	Inner Join vw_Productos_CodigoEAN P(NoLock) On ( F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN ) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And Existencia > 0

	Select	F.*, Cast(0.0000 as Numeric(14,4)) as Costo, L.TasaIva 
	Into #tmpUbicaciones 
	From #tmpLotes L(NoLock)
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F(NoLock) 
		On ( L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
			And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote )
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia And F.Existencia > 0		

	-- Se actualizan los costos
	Update C Set Costo = P.UltimoCosto
	From #tmpCodigosEAN C(NoLock)
	Inner Join #tmpProductos P(NoLock) On ( C.IdProducto = P.IdProducto )
	
	Update C Set Costo = P.UltimoCosto
	From #tmpLotes C(NoLock)
	Inner Join #tmpProductos P(NoLock) On ( C.IdProducto = P.IdProducto )

	Update C Set Costo = P.UltimoCosto
	From #tmpUbicaciones C(NoLock)
	Inner Join #tmpProductos P(NoLock) On ( C.IdProducto = P.IdProducto ) 
------------------------------ 	Preparar Productos 

	
---------------------------------------- GENERAR POLIZAS DE SALIDA 

	Set @PolizaAplicada = 'S' 
	Set @sPoliza_Salida = '*'  	
	Exec spp_Mtto_AjustesInv_Enc 
			@IdEmpresa, @IdEstado, @IdFarmacia, @sPoliza_Salida output, 
			@IdPersonal, @Observaciones, @SubTotal, @Iva, @Total, @PolizaAplicada, @iOpcion, @iMostrarResultado  

----	
----	Select * 
----	From AjustesInv_Enc F 
----	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and Poliza =  @sPoliza_Salida 
 
 
	Insert Into AjustesInv_Det 
	(	IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, EsConsignacion, 
		UnidadDeSalida, TasaIva, ExistenciaSistema, Costo, Importe, ExistenciaFisica, Diferencia, Status, Actualizado ) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, @sPoliza_Salida as Poliza, IdProducto, CodigoEAN, 0 as EsConsignacion, 1 as UnidadDeSalida, 
		TasaIva, Existencia as ExistenciaSistema, Costo, (Existencia * Costo) as Importe, 0 as ExistenciaFisica, 
		0 as Diferencia, @Status, @Actualizado   
	From #tmpCodigosEAN F (NoLock)  
	

 	
	Insert Into AjustesInv_Det_Lotes 
	(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote, 
		EsConsignacion, ExistenciaSistema, Costo, Importe, ExistenciaFisica, Diferencia, Referencia, Status, Actualizado  ) 
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @sPoliza_Salida as Poliza, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		Existencia as ExistenciaSistema, Costo, (Existencia * Costo) as Importe, 0 as ExistenciaFisica, 
		0 as Diferencia, @Status, '' as Referencia, @Actualizado   
	From #tmpLotes F (NoLock) 
	
	
	Insert Into AjustesInv_Det_Lotes_Ubicaciones 
	(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, ExistenciaSistema, Costo, Importe, ExistenciaFisica, Diferencia, Referencia, Status, Actualizado ) 
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @sPoliza_Salida as Poliza, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, sum(Existencia) as ExistenciaSistema, max(Costo) as Costo, 
		(sum(Existencia) * max(Costo)) as Importe, 0 as ExistenciaFisica, 
		0 as Diferencia, '' as Referencia, @Status, @Actualizado   
	From #tmpUbicaciones  
	Group by  
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño	
 
	
---------------- APLICAR LA SALIDA 
------ 
--------	Exec spp_Mtto_AjustesDeInventario @IdEmpresa, @IdEstado, @IdFarmacia, @sPoliza_Salida, @IdPersonal 
------ 
--------Exec   '{0}', '{1}', '{2}', '{3}', '{4}' ",sEmpresa, sEstado, sFarmacia, txtFolio.Text, DtGeneral.IdPersonal 
------                
------                
------	Set @sSqlTexto = 'Exec spp_Mtto_AjustesDeInventario ' 
------					+ char(39) + @IdEmpresa + char(39) + ', ' 
------					+ char(39) + @IdEstado + char(39) + ', ' + 
------					+ char(39) + @IdFarmacia + char(39) + ', ' 
------					+ char(39) + @sPoliza_Salida + char(39) + ', ' 
------					+ char(39) + @IdPersonal + char(39) + ''  					
------	-- Exec (@sSqlTexto) 
------	Print @sSqlTexto  
------	
---------------- APLICAR LA SALIDA 
	
	
	------ Regresar el folio de salida generado  
	Select @Poliza_Salida =  @sPoliza_Salida 
			
---------------------------------------- GENERAR POLIZAS DE SALIDA 
			

 			
End 
Go--#SQL 

