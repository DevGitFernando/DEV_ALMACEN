If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioExterno_003' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioExterno_003 
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioExterno_003  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182',  
	@IdPersonal varchar(6) = '0001', @Poliza_Entrada varchar(10) = '' output  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare @Actualizado smallint 
Declare 
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
	Set @Observaciones = 'Entrafa general por toma de inventario de almacen'
	
	------------------------------------------------------------------------------------------
	-- Se actualiza el Status de los Productos para que no se puede hacer ningun movimiento --
	------------------------------------------------------------------------------------------
	Update FarmaciaProductos Set Status = 'I'
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	----------------------------------------------------------------
	-- Se obtienen los Productos, CodigosEAN, Lotes y Ubicaciones --
	----------------------------------------------------------------

------------------------------------------------------ UBICACIONES 
	select IdEmpresa, IdEstado, IdFarmacia, IdPasillo, ('Pasillo #' + IdPasillo) as DescripcionPasillo, 'A' as Status, 0 as Actualizado   
	into #tmpPasillos 
	from INV__Inventario_CargaMasiva 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdPasillo 
	order by IdEmpresa, IdEstado, IdFarmacia, IdPasillo 
	
	select IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, ('Estante #' + IdEstante) as DescripcionEstante, 'A' as Status, 0 as Actualizado    
	into #tmpEstantes  
	from INV__Inventario_CargaMasiva 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante 
	order by IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante 	
	
	
	select IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño, 
		('Entrepaño #' + IdEntrepaño) as DescripcionEntrepaño, 'A' as Status, 0 as Actualizado, 0 as EsExclusiva     
	into #tmpEntrepaño  
	from INV__Inventario_CargaMasiva 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño 
	order by IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño 	
	
	
	Insert Into CatPasillos 
	Select IdEmpresa, IdEstado, IdFarmacia, IdPasillo, DescripcionPasillo, Status, Actualizado  
	From #tmpPasillos T 
	Where Not Exists ( Select * From CatPasillos P 
					   Where T.IdEmpresa = P.IdEmpresa and T.IdEstado = P.IdEstado and T.IdFarmacia = P.IdFarmacia and T.IdPasillo = P.IdPasillo ) 
	
	
	Insert Into CatPasillos_Estantes 
	Select IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, DescripcionEstante, Status, Actualizado  
	From #tmpEstantes T 
	Where Not Exists ( Select * From CatPasillos_Estantes P 
					   Where T.IdEmpresa = P.IdEmpresa and T.IdEstado = P.IdEstado and T.IdFarmacia = P.IdFarmacia 
					   and T.IdPasillo = P.IdPasillo and T.IdEstante = P.IdEstante ) 	
	
	Insert Into CatPasillos_Estantes_Entrepaños 
	( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño, DescripcionEntrepaño, Status, Actualizado, EsExclusiva ) 
	Select IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño, DescripcionEntrepaño, Status, Actualizado, EsExclusiva   
	From #tmpEntrepaño T 
	Where Not Exists ( Select * From CatPasillos_Estantes_Entrepaños P 
					   Where T.IdEmpresa = P.IdEmpresa and T.IdEstado = P.IdEstado and T.IdFarmacia = P.IdFarmacia 
					   and T.IdPasillo = P.IdPasillo and T.IdEstante = P.IdEstante and T.IdEntrepaño = P.IdEntrepaño  ) 		
	
------------------------------------------------------ UBICACIONES 


------------------------------ 	Preparar Productos 
	Insert Into CatProductos_Estado	
	Select IdEstado, IdProducto, Status, Actualizado 
	From 
	( Select @IdEstado as IdEstado, IdProducto, 'A' as Status, 0 as Actualizado From CatProductos ) as P 
	Where Not Exists (  Select * From CatProductos_Estado E Where P.IdEstado = E.IdEstado and P.IdProducto = E.IdProducto ) 
		
	Update I Set ClaveLote = replace(ClaveLote, '*', '') 
	From INV__Inventario_CargaMasiva I 

	-- Formatear los EAN 
	Update C Set CodigoEAN = P.CodigoEAN 
	From INV__Inventario_CargaMasiva C 
	Inner Join vw_Productos_CodigoEAN P On ( right('00000000000000000' + C.CodigoEAN, 13) = right('00000000000000000' + P.CodigoEAN, 13) ) 	
	
	-- Formatear los EAN 
	Update C Set CodigoEAN = P.CodigoEAN 
	From INV__Inventario_CargaMasiva C 
	Inner Join vw_Productos_CodigoEAN P On ( right('00000000000000000' + C.CodigoEAN, 13) = right('00000000000000000' + P.CodigoEAN_Interno, 13) ) 		
	

	Select IdEmpresa, IdEstado, IdFarmacia, space(20) as IdProducto, CodigoEAN, cast(0 as float) as TasaIva, 
		0 as EsConsignacion, 0 as ExistenciaSistema, cast(sum(Cantidad) as int) as ExistenciaFisica, 2 as Tipo, Cast(0.0000 as Numeric(14,4)) as Costo     
	Into #tmpCodigosEAN 
	From INV__Inventario_CargaMasiva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
	Group by IdEmpresa, IdEstado, IdFarmacia, CodigoEAN  


	Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, Cast(0.0000 as Numeric(14,4)) as Costo, 0.0 as UltimoCosto  
	Into #tmpProductos 
	From 
	(
		Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, P.IdProducto, D.CodigoEAN, 0.0 as CostoPromedio, 0.0 as UltimoCosto 
		From #tmpCodigosEAN D 
		Inner Join vw_Productos_CodigoEAN P On ( D.CodigoEAN = P.CodigoEAN ) 
		Group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, P.IdProducto, D.CodigoEAN  
	) as T 
	
---	AQUI 
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, space(20) as IdProducto, CodigoEAN, ClaveLote, cast(0 as float) as TasaIva, 
		0 as EsConsignacion, Caducidad as FechaCaducidad, 0 as ExistenciaSistema, 
		cast(sum(Cantidad) as int) as ExistenciaFisica, 2 as Tipo, Cast(0.0000 as Numeric(14,4)) as Costo   	  
	Into #tmpLotes	
	From INV__Inventario_CargaMasiva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, CodigoEAN, ClaveLote, Caducidad  
	
	
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, space(20) as IdProducto, CodigoEAN, ClaveLote, 
		0 as EsConsignacion, 
		cast(IdPasillo as int) as IdPasillo, cast(IdEstante as int) as IdEstante, cast(IdEntrepaño as int) as IdEntrepaño, 
		cast(0 as float) as TasaIva, 
		0 as ExistenciaSistema, cast(sum(Cantidad) as int) as ExistenciaFisica, 2 as Tipo, Cast(0.0000 as Numeric(14,4)) as Costo   	  		
	Into #tmpUbicaciones 
	From INV__Inventario_CargaMasiva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
	Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño    	 


	-- Se actualizan los costos 
	Update C Set Costo = P.UltimoCosto 
	From #tmpProductos C (NoLock)
	Inner Join 	FarmaciaProductos P 
		On ( C.IdEmpresa = P.IdEmpresa and C.IdEstado = P.IdEstado and C.IdFarmacia = P.IdFarmacia and C.IdProducto = P.IdProducto ) 
	Where C.IdEmpresa = @IdEmpresa and C.IdEstado = @IdEstado and C.IdFarmacia = @IdFarmacia 	
	
	Update C Set Costo = P.UltimoCosto, IdProducto = P.IdProducto 
	From #tmpCodigosEAN C(NoLock)
	Inner Join #tmpProductos P(NoLock) On ( C.CodigoEAN = P.CodigoEAN )
	
	Update C Set Costo = P.UltimoCosto, IdProducto = P.IdProducto 
	From #tmpLotes C(NoLock)
	Inner Join #tmpProductos P(NoLock) On ( C.CodigoEAN = P.CodigoEAN )

	Update C Set Costo = P.UltimoCosto, IdProducto = P.IdProducto 
	From #tmpUbicaciones C(NoLock)
	Inner Join #tmpProductos P(NoLock) On ( C.CodigoEAN = P.CodigoEAN ) 
	
------------------------------ 	Preparar Productos 


--------------- Registrar Productos faltantes  	
	Insert Into FarmaciaProductos ( 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, 
		CostoPromedio, UltimoCosto, Existencia, StockMinimo, StockMaximo, Status, Actualizado ) 
	Select Distinct	
		@IdEmpresa, @IdEstado, @IdFarmacia, IdProducto, 
		0 as CostoPromedio, 0 as UltimoCosto, 0 as Existencia, 0 as StockMinimo, 0 as StockMaximo, 'A', 0  
	From #tmpProductos B  
	Where 
	Not Exists 
	(
		Select * From  FarmaciaProductos E (NoLock) 
		Where B.IdEmpresa = E.IdEmpresa and B.IdEstado = E.IdEstado and B.IdFarmacia = E.IdFarmacia and B.IdProducto = E.IdProducto 
	)
	
	
	Insert Into FarmaciaProductos_CodigoEAN ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, Existencia, Status, Actualizado ) 
	Select Distinct	@IdEmpresa, @IdEstado, @IdFarmacia, IdProducto, CodigoEAN, 0 as Existencia, 'A', 0  
	From #tmpCodigosEAN B  
	Where   
	Not Exists 
	(
		Select * From  FarmaciaProductos_CodigoEAN E (NoLock) 
		Where B.IdEmpresa = E.IdEmpresa and B.IdEstado = E.IdEstado and B.IdFarmacia = E.IdFarmacia and B.IdProducto = E.IdProducto 
			  and B.CodigoEAN = E.CodigoEAN 
	)	
	
	
	Insert Into FarmaciaProductos_CodigoEAN_Lotes 
		( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, ClaveLote, Existencia, 
		FechaCaducidad, FechaRegistro, IdPersonal, Status, Actualizado, IdSubFarmacia, EsConsignacion ) 
	Select Distinct	
		@IdEmpresa, @IdEstado, @IdFarmacia, IdProducto, CodigoEAN, ClaveLote, 0 as Existencia, 
		FechaCaducidad, getdate(), @IdPersonal, 'A', 0, IdSubFarmacia, EsConsignacion
	From #tmpLotes B  
	Where 
	Not Exists 
	(
		Select * From  FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
		Where B.IdEmpresa = E.IdEmpresa and B.IdEstado = E.IdEstado and B.IdFarmacia = E.IdFarmacia and B.IdSubFarmacia = E.IdSubFarmacia 
			and B.IdProducto = E.IdProducto and B.CodigoEAN = E.CodigoEAN and B.ClaveLote = E.ClaveLote 
	)	
	
	
	Insert Into FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
		( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, 
		EsConsignacion, IdPasillo, IdEstante, IdEntrepaño,  Existencia, Status, Actualizado  ) 
	Select Distinct	
		@IdEmpresa, @IdEstado, @IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, 
		EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, 0 as Existencia, 'A', 0 		
	From #tmpUbicaciones B  
	Where 
	Not Exists 
	(
		Select * From  FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones E (NoLock) 
		Where B.IdEmpresa = E.IdEmpresa and B.IdEstado = E.IdEstado and B.IdFarmacia = E.IdFarmacia and B.IdSubFarmacia = E.IdSubFarmacia 
			and B.IdPasillo = E.IdPasillo and B.IdEstante = E.IdEstante and B.IdEntrepaño = E.IdEntrepaño 
			and B.IdProducto = E.IdProducto and B.CodigoEAN = E.CodigoEAN and B.ClaveLote = E.ClaveLote 
	)	
	 
	
--------------- Registrar Productos faltantes   
 	
 	
---------------------------------------- GENERAR POLIZAS DE SALIDA 
 
	Set @PolizaAplicada = 'S' 
	Set @sPoliza_Entrada = '*'  	
	Exec spp_Mtto_AjustesInv_Enc 
			@IdEmpresa, @IdEstado, @IdFarmacia, @sPoliza_Entrada output, 
			@IdPersonal, @Observaciones, @SubTotal, @Iva, @Total, @PolizaAplicada, @iOpcion, @iMostrarResultado  

 
	Insert Into AjustesInv_Det 
	(	IdEmpresa, IdEstado, IdFarmacia, Poliza, IdProducto, CodigoEAN, EsConsignacion, 
		UnidadDeSalida, TasaIva, ExistenciaSistema, Costo, Importe, ExistenciaFisica, Diferencia, Status, Actualizado ) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, @sPoliza_Entrada as Poliza, IdProducto, CodigoEAN, 0 as EsConsignacion, 1 as UnidadDeSalida, 
		TasaIva, ExistenciaSistema, Costo, (ExistenciaFisica * Costo) as Importe, ExistenciaFisica, 
		0 as Diferencia, @Status, @Actualizado   
	From #tmpCodigosEAN F (NoLock)  
	
	
	Insert Into AjustesInv_Det_Lotes 
	(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote, 
		EsConsignacion, ExistenciaSistema, Costo, Importe, ExistenciaFisica, Diferencia, Referencia, Status, Actualizado  ) 
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @sPoliza_Entrada as Poliza, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		ExistenciaSistema, Costo, (ExistenciaFisica * Costo) as Importe, ExistenciaFisica, 
		0 as Diferencia, @Status, '' as Referencia, @Actualizado   
	From #tmpLotes F (NoLock) 
	
	
	Insert Into AjustesInv_Det_Lotes_Ubicaciones 
	(	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, Poliza, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, ExistenciaSistema, Costo, Importe, ExistenciaFisica, Diferencia, Referencia, Status, Actualizado ) 
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @sPoliza_Entrada as Poliza, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, sum(ExistenciaSistema) as ExistenciaSistema, max(Costo) as Costo, 
		(sum(ExistenciaFisica) * max(Costo)) as Importe, sum(ExistenciaFisica) as ExistenciaFisica, 
		0 as Diferencia, '' as Referencia, @Status, @Actualizado   
	From #tmpUbicaciones  
	Group by  
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño	
 
	
---------------- APLICAR LA SALIDA 
------ 
--------	Exec spp_Mtto_AjustesDeInventario @IdEmpresa, @IdEstado, @IdFarmacia, @sPoliza_Entrada, @IdPersonal 
------ 
--------Exec   '{0}', '{1}', '{2}', '{3}', '{4}' ",sEmpresa, sEstado, sFarmacia, txtFolio.Text, DtGeneral.IdPersonal 
------                
------                
------	Set @sSqlTexto = 'Exec spp_Mtto_AjustesDeInventario ' 
------					+ char(39) + @IdEmpresa + char(39) + ', ' 
------					+ char(39) + @IdEstado + char(39) + ', ' + 
------					+ char(39) + @IdFarmacia + char(39) + ', ' 
------					+ char(39) + @sPoliza_Entrada + char(39) + ', ' 
------					+ char(39) + @IdPersonal + char(39) + ''  					
------	-- Exec (@sSqlTexto) 
------	Print @sSqlTexto  
------	
---------------- APLICAR LA SALIDA 
	
	
	------ Regresar el folio de salida generado  
	Select @Poliza_Entrada =  @sPoliza_Entrada 
			
---------------------------------------- GENERAR POLIZAS DE SALIDA 
  			

 			
End 
Go--#SQL 

