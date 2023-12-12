If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioInterno_003___Farmacia' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioInterno_003___Farmacia 
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioInterno_003___Farmacia  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182',  
	@IdPersonal varchar(6) = '0001', @Poliza_Salida varchar(10) = '' output  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare @Actualizado smallint 
Declare 
	@sPoliza_Generada varchar(8), 
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
	Set @Observaciones = 'Ajuste de inventario interno' 	
	

	----------------------------------------------------------------
	-- Se obtienen los Productos, CodigosEAN, Lotes y Ubicaciones --
	----------------------------------------------------------------


------------------------- Productos a Procesar  	
	Select * 
		-- IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdPasillo, IdEstante, IdEntrepaño, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Caducidad, Cantidad  
	Into #tmpBase 
	From INV__InventarioInterno_CargaMasiva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	--		and CodigoEAN = '7501125195150' 
	
---	sp_listacolumnas INV__InventarioInterno_CargaMasiva 

--	Select * 	From #tmpBase 




	Select Distinct P.* 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN P (NoLock) 
	Inner Join #tmpBase CM (NoLock) On ( P.IdProducto = CM.IdProducto and P.CodigoEAN = CM.CodigoEAN ) 	
	
	Select Distinct P.* 
	Into #vw_Productos 
	From vw_Productos P (NoLock) 
	Inner Join #tmpBase LP (NoLock) On ( P.IdProducto = LP.IdProducto )			
------------------------- Productos a Procesar  	


	------------------------------------------------------------------------------------------
	-- Se actualiza el Status de los Productos para que no se puede hacer ningun movimiento --
	------------------------------------------------------------------------------------------
	Update F Set Status = 'I'
	From FarmaciaProductos F 
	Inner Join #vw_Productos P (NoLock) On ( F.IdProducto = P.IdProducto )
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  




--------------------------------------------- 	Preparar Productos 
	Select distinct	F.*, 
		0 as ExistenciaFisica, 	
		Cast(0.0000 as Numeric(14,4)) as Costo,  P.TasaIva 
	Into #tmpProductos 
	From FarmaciaProductos F (NoLock) 
	Inner Join #vw_Productos P(NoLock) On ( F.IdProducto = P.IdProducto ) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia ---- And Existencia > 0

	Select distinct	F.*, 
		0 as ExistenciaFisica, 	
		Cast(0.0000 as Numeric(14,4)) as Costo, P.TasaIva  
	Into #tmpCodigosEAN
	From FarmaciaProductos_CodigoEAN F (NoLock) 
	Inner Join #vw_Productos_CodigoEAN P (NoLock) On ( F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN ) 
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia ---- And Existencia > 0
	
	Select distinct	F.*, 
		0 as ExistenciaFisica, 	
		Cast(0.0000 as Numeric(14,4)) as Costo, 0 as TasaIva 
	Into #tmpLotes 
	From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
	Inner Join #tmpBase P (NoLock) 
		On ( F.IdSubFarmacia = P.IdSubFarmacia and F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN and F.ClaveLote = P.ClaveLote ) 
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia ---- And Existencia > 0



	Select distinct	F.*, 
		0 as ExistenciaFisica, 
		Cast(0.0000 as Numeric(14,4)) as Costo, L.TasaIva 
	Into #tmpUbicaciones 
	From #tmpLotes L (NoLock)
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock) 
		On ( L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
			And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote ) 
	Inner Join #tmpBase B (NoLock) 
		On ( B.IdEmpresa = F.IdEmpresa And B.IdEstado = F.IdEstado And B.IdFarmacia = F.IdFarmacia And B.IdSubFarmacia = F.IdSubFarmacia 
			And B.IdProducto = F.IdProducto And B.CodigoEAN = F.CodigoEAN And B.ClaveLote = F.ClaveLote 
			And F.IdPasillo = B.IdPasillo and F.IdEstante = B.IdEstante and F.IdEntrepaño = B.IdEntrepaño ) 			
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia ---- And F.Existencia > 0		



---------------------- Se actualizan los costos	de acuerdo al inventario cargado 
	Update C Set Costo = P.Costo, UltimoCosto = P.Costo   
	From #tmpProductos C (NoLock) 
	Inner Join INV__InventarioInterno_CargaMasiva P (NoLock) On ( C.IdProducto = P.IdProducto )
---------------------- Se actualizan los costos	de acuerdo al inventario cargado 

---------------------- Se actualizan los costos	
	Update C Set Costo = P.UltimoCosto 
	From #tmpCodigosEAN C(NoLock)
	Inner Join #tmpProductos P (NoLock) On ( C.IdProducto = P.IdProducto )
	
	Update C Set Costo = P.UltimoCosto, CostoPromedio = P.UltimoCosto, UltimoCosto = P.UltimoCosto 
	From #tmpLotes C(NoLock) 
	Inner Join #tmpProductos P(NoLock) On ( C.IdProducto = P.IdProducto )

	Update C Set Costo = P.UltimoCosto
	From #tmpUbicaciones C(NoLock)
	Inner Join #tmpProductos P(NoLock) On ( C.IdProducto = P.IdProducto ) 
	
	
----	select * from #tmpProductos where IdProducto =  7799 
----	select * from #tmpCodigosEAN where IdProducto =  7799 
----	select * from #tmpLotes where IdProducto =  7799 
----	select * from #tmpUbicaciones where IdProducto =  7799 				
---------------------- Se actualizan los costos 


--------------------- Existencia capturada 
	Update F Set ExistenciaFisica = ( Select sum(Cantidad) From #tmpBase B Where B.IdProducto = F.IdProducto ) 
	From #tmpProductos F 
	
	Update F Set ExistenciaFisica = ( Select sum(Cantidad) From #tmpBase B Where B.IdProducto = F.IdProducto and B.CodigoEAN = F.CodigoEAN ) 
	From #tmpCodigosEAN F 

	Update F Set ExistenciaFisica = 
	IsNull((
		Select sum(Cantidad) From #tmpBase B 
		Where B.IdSubFarmacia = F.IdSubFarmacia and B.IdProducto = F.IdProducto and B.CodigoEAN = F.CodigoEAN and B.ClaveLote = F.ClaveLote 
	), 0) 
	From #tmpLotes F  	
--------------------- Existencia capturada 	

--------------------------------------------- 	Preparar Productos 

---		sp_Proceso_IntegrarInventarioInterno_003  

 

--	select * from #tmpProductos 
--	select * from #tmpCodigosEAN 
--	select * from #tmpLotes 
--	select * from #tmpUbicaciones 
    
 

 	
---------------------------------------- GENERAR POLIZAS DE SALIDA  
 
	Set @PolizaAplicada = 'S' 
	Set @sPoliza_Generada = '*'  	
	Exec spp_Mtto_AjustesInv_Enc 
			@IdEmpresa, @IdEstado, @IdFarmacia, @sPoliza_Generada output, 
			@IdPersonal, @Observaciones, @SubTotal, @Iva, @Total, @PolizaAplicada, @iOpcion, @iMostrarResultado  
 
	Insert Into AjustesInv_Det 
	(	IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, 
		UnidadDeSalida, TasaIva, ExistenciaSistema, Costo, Importe, ExistenciaFisica, Diferencia, Status, Actualizado ) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, @sPoliza_Generada as Poliza, IdProducto, CodigoEAN, 1 as UnidadDeSalida, TasaIva, 
		sum(Existencia) as ExistenciaSistema, max(Costo), (sum(Existencia) * max(Costo)) as Importe, sum(ExistenciaFisica) as ExistenciaFisica, 
		0 as Diferencia, @Status, @Actualizado   
	From #tmpCodigosEAN F (NoLock)  
	Group by  
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, TasaIva  

	Insert Into AjustesInv_Det_Lotes 
	(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote, 
		EsConsignacion, ExistenciaSistema, Costo, Importe, ExistenciaFisica, Diferencia, Referencia, Status, Actualizado  ) 
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @sPoliza_Generada as Poliza, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		sum(Existencia) as ExistenciaSistema, max(Costo) as Costo, (sum(Existencia) * max(Costo)) as Importe, sum(ExistenciaFisica) as ExistenciaFisica, 
		0 as Diferencia, @Status, '' as Referencia, @Actualizado   
	From #tmpLotes F (NoLock) 
	Group by  
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion 
 


/* 	
	Insert Into AjustesInv_Det_Lotes_Ubicaciones 
	(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, ExistenciaSistema, Costo, Importe, ExistenciaFisica, Diferencia, Referencia, Status, Actualizado ) 
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @sPoliza_Generada as Poliza, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, sum(Existencia) as ExistenciaSistema, max(Costo) as Costo, 
		(sum(Existencia) * max(Costo)) as Importe, sum(ExistenciaFisica) as ExistenciaFisica, 
		0 as Diferencia, '' as Referencia, @Status, @Actualizado   
	From #tmpUbicaciones  
	Group by  
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño	  
*/  	
	
	
---------------- APLICAR LA SALIDA 
------ 
--------	Exec spp_Mtto_AjustesDeInventario @IdEmpresa, @IdEstado, @IdFarmacia, @sPoliza_Generada, @IdPersonal 
------ 
--------Exec   '{0}', '{1}', '{2}', '{3}', '{4}' ",sEmpresa, sEstado, sFarmacia, txtFolio.Text, DtGeneral.IdPersonal 
------                
------                
------	Set @sSqlTexto = 'Exec spp_Mtto_AjustesDeInventario ' 
------					+ char(39) + @IdEmpresa + char(39) + ', ' 
------					+ char(39) + @IdEstado + char(39) + ', ' + 
------					+ char(39) + @IdFarmacia + char(39) + ', ' 
------					+ char(39) + @sPoliza_Generada + char(39) + ', ' 
------					+ char(39) + @IdPersonal + char(39) + ''  					
------	-- Exec (@sSqlTexto) 
------	Print @sSqlTexto  
------	
---------------- APLICAR LA SALIDA 
	
	
	------ Regresar el folio de salida generado  
	Select @Poliza_Salida =  @sPoliza_Generada 
			
---------------------------------------- GENERAR POLIZAS DE SALIDA 
			

 			
End 
Go--#SQL 

