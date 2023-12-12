If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_IntegrarInventarioInterno_002___Farmacia' and xType = 'P' ) 
   Drop Proc sp_Proceso_IntegrarInventarioInterno_002___Farmacia 
Go--#SQL 

Create Proc sp_Proceso_IntegrarInventarioInterno_002___Farmacia  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182',  @IdPersonal varchar(6) = '0001',
	@TipoInv tinyint = 0  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	--	@TipoInv = 0 : Inventario Completo
	--	@TipoInv = 1 : Inventario Parcial

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
	Set @Observaciones = 'Entrada general por toma de inventario de almacén'



----------------------------------	 Integrar los productos-lotes que no se contemplaron en el inventario 
	----Select 1 as X, 
	----	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
	----	-1 as IdPasillo, -1 as IdEstante, -1 as IdEntrepaño, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, space(10), 0 as Cantidad, 0 as EnInventario
	----From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
	----Where -- Existencia > 0 and  -- CodigoEAN = '7501446024047'  and 
	----Not Exists 
	----(
	----	Select 	* 
	----	From INV__InventarioInterno_CargaMasiva L (NoLock) 
	----	Where 
	----		L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
	----		And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote 
	----) 

	If @TipoInv = 0
	Begin
		Insert Into INV__InventarioInterno_CargaMasiva 
			( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
			  IdPasillo, IdEstante, IdEntrepaño, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Caducidad, Cantidad, EnInventario ) 
		Select 
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
			-1 as IdPasillo, -1 as IdEstante, -1 as IdEntrepaño, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, space(10), 
			-- cast(F.Existencia as int) as Cantidad, 
			0 as Cantidad, 
			0 as EnInventario 
		From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		Where Existencia > 0 and  -- CodigoEAN = '7501446024047'  and 
		Not Exists 
		(
			Select 	* 
			From INV__InventarioInterno_CargaMasiva L (NoLock)  
			Where 
				L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
				And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote 
		) 
		
		Update L Set Caducidad = convert(varchar(10), F.FechaCaducidad, 120) 
		From INV__InventarioInterno_CargaMasiva L 
		Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
			On ( L.IdEmpresa = F.IdEmpresa And L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And L.IdSubFarmacia = F.IdSubFarmacia 
				 And L.IdProducto = F.IdProducto And L.CodigoEAN = F.CodigoEAN And L.ClaveLote = F.ClaveLote 	) 
		Where EnInventario = 0  		
	----------------------------------	 Integrar los productos-lotes que no se contemplaron en el inventario 	
	End


------------------------------------------------------ ACTUALIZACION DE COSTOS 
	If exists ( Select * from tempdb..sysobjects (nolock) Where Name like '#tmp_Costos%' ) Drop Table #tmp_Costos

	Select CodigoEAN, avg(Costo) as Costo  
	Into #tmp_Costos 
	From INV__InventarioInterno_CargaMasiva (NoLock)  
	Group by CodigoEAN  


	Update I Set Costo = C.Costo 
	From INV__InventarioInterno_CargaMasiva I (NoLock) 
	Inner Join #tmp_Costos C (NoLock) On (I.CodigoEAN = C.CodigoEAN ) -- and  I.IdSubFarmacia = C.IdSubFarmacia and I.ClaveLote = C.ClaveLote )  
	where I.Costo <> C.Costo 


	Update I Set Costo = dbo.fg_ObtenerCostoPromedio(IdEmpresa, idestado, IdFarmacia, IdProducto)  
	From INV__InventarioInterno_CargaMasiva I (NoLock) 
	Where I.Costo = 0 
------------------------------------------------------ ACTUALIZACION DE COSTOS 



------------------------------ 	Preparar Productos 
	Insert Into CatProductos_Estado	
	Select IdEstado, IdProducto, Status, Actualizado 
	From 
	( 
		Select @IdEstado as IdEstado, IdProducto, 'A' as Status, 0 as Actualizado From CatProductos ) as P 
		Where Not Exists (  Select * From CatProductos_Estado E Where P.IdEstado = E.IdEstado and P.IdProducto = E.IdProducto 
	) 

	Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, cast(0 as float) as TasaIva, 
		EsConsignacion, 0 as ExistenciaSistema, cast(sum(Cantidad) as int) as ExistenciaFisica, 2 as Tipo, Cast(0.0000 as Numeric(14,4)) as Costo     
	Into #tmpCodigosEAN 
	From INV__InventarioInterno_CargaMasiva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
	Group by IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, EsConsignacion   


	Select IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, 
		Cast(0.0000 as Numeric(14,4)) as Costo, Cast(0.0000 as Numeric(14,4)) as UltimoCosto  
	Into #tmpProductos 
	From 
	(
		Select D.IdEmpresa, D.IdEstado, D.IdFarmacia, P.IdProducto, D.CodigoEAN, 0.0 as CostoPromedio, 0.0 as UltimoCosto 
		From #tmpCodigosEAN D 
		Inner Join vw_Productos_CodigoEAN P On ( D.CodigoEAN = P.CodigoEAN ) 
		Group by D.IdEmpresa, D.IdEstado, D.IdFarmacia, P.IdProducto, D.CodigoEAN  
	) as T 
	
	
	
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, cast(0 as float) as TasaIva, 
		EsConsignacion, convert(varchar(10), Caducidad, 120) as FechaCaducidad, 0 as ExistenciaSistema, 
		cast(sum(Cantidad) as int) as ExistenciaFisica, 2 as Tipo, Cast(0.0000 as Numeric(14,4)) as Costo   	  
	Into #tmpLotes	
	From INV__InventarioInterno_CargaMasiva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Caducidad  
	
	
	Select IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, 
		EsConsignacion, 
		cast(IdPasillo as int) as IdPasillo, cast(IdEstante as int) as IdEstante, cast(IdEntrepaño as int) as IdEntrepaño, 
		cast(0 as float) as TasaIva, 
		0 as ExistenciaSistema, cast(sum(Cantidad) as int) as ExistenciaFisica, 2 as Tipo, Cast(0.0000 as Numeric(14,4)) as Costo   	  		
	Into #tmpUbicaciones 
	From INV__InventarioInterno_CargaMasiva 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
	Group by IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño  	 


--------------- Se actualizan los costos segun el inventario a integrar 
	Update C Set Costo = P.Costo, UltimoCosto = P.Costo 
	From #tmpProductos C (NoLock) 
	Inner Join INV__InventarioInterno_CargaMasiva P  On ( C.CodigoEAN = P.CodigoEAN ) 
--------------- Se actualizan los costos segun el inventario a integrar 


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
--------------- Registrar Productos faltantes   
 	 			
End 
Go--#SQL 

