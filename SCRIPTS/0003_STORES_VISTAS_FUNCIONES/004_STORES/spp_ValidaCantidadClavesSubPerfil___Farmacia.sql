----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( select Name From Sysobjects (NoLock) Where Name = 'tmpProductos' and xType = 'U' ) 
   Drop Table tmpProductos  
Go--#SQL 	   

----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select * From SysObjects(NoLock) Where Name = 'spp_ValidaCantidadClavesSubPerfil____Farmacia' and xType = 'P')
    Drop Proc spp_ValidaCantidadClavesSubPerfil____Farmacia
Go--#SQL 

--		Exec spp_ValidaCantidadClavesSubPerfil____Farmacia '003', '16', '0011', '0002', '0002', '0001', 'tmptest'
  
Create Proc spp_ValidaCantidadClavesSubPerfil____Farmacia 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0093', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0006', @TipoDeRecetaAtendida varchar(4) = '06',  
	@Tabla varchar(100) = 'tmpCantClavesSubPerfil_28F10E2C032C_20170513_131000'  
)
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@iResultado tinyint, 
	@sSql varchar(7500), 
	@sTablaRegistro varchar(100),  
	@dFechaValidacion datetime 


	Set @dFechaValidacion = dateadd(mm, 0, getdate()) 
	Set @sSql = ''	
	Set @iResultado = 0	
	

	------------------------------------------ Obtener la lista de Tipos de Receta que se deben excluir de la validación 
	Select *
	Into #tmpParametros  
	From Net_CFGC_Parametros (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		and NombreParametro in  ( 'ClaveDispensacionRecetasForaneas', 'TipoDispensacionVale', 'TipoDispensacionRecetaValeForaneo' )  
		-- and 1 = 0 


	If Exists ( Select * From #tmpParametros Where Valor = @TipoDeRecetaAtendida ) 
	Begin 	
		Return ---- Terminar el proceso de validación 
	End 




	--------- SE ACTUALIZA EL IDCLAVESSA DE LOS PRODUCTOS  ---------------------------------------------------------------------
	Set @sSql = ' Update T Set T.IdClaveSSA = P.IdClaveSSA_Sal, T.ClaveSSA = P.ClaveSSA, T.Descripcion = P.DescripcionCortaClave ' + 
	'From ' + @Tabla + ' T (Nolock) ' + 
	'Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto and T.CodigoEAN = P.CodigoEAN ) ' 
	Exec(@sSql)	
	


	
	
	---------  SE CREA UNA TEMPORAL DE LOS PRODUCTOS  -------------------------------------------------------------------------
	-- If Exists ( select Name From Sysobjects (NoLock) Where Name = 'tmpProductos' and xType = 'U' ) 
	--   Drop Table tmpProductos 
	Select top 0 
		cast('' As varchar(30)) as IdEmpresa, cast('' As varchar(30)) as IdEstado, cast('' As varchar(30)) as IdFarmacia, 
		cast('' As varchar(30)) as IdClaveSSA, cast('' As varchar(30)) as ClaveSSA,  
		cast('' As varchar(30)) as IdClaveSSA_Aux, cast('' As varchar(30)) as ClaveSSA_Aux, 
		cast('' As varchar(max)) as Descripcion, cast('' As varchar(30)) as IdProducto, cast('' As varchar(30)) as CodigoEAN, 0 as Cantidad
	Into #tmpProductos 
	

	Set @sSql = 
		'Insert Into #tmpProductos  ' + 
		'Select IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, ' + 
		'IdClaveSSA as IdClaveSSA_Aux, Cast(ClaveSSA As Varchar(30)) as ClaveSSA_Aux, cast(Descripcion as varchar(max)) as Descripcion, IdProducto, CodigoEAN, sum(Cantidad) as Cantidad ' + 
		' ' + 
		'From ' + @Tabla + ' ' + 
		'Group by IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, IdClaveSSA, ClaveSSA, Descripcion, IdProducto, CodigoEAN '
	Exec(@sSql)		
	
	

	------------ Reemplazo de Claves  
	--select * from tmpProductos 
	
	Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, Descripcion = C.Descripcion 
	From #tmpProductos B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) 
		On ( B.IdEstado = C.IdEstado and B.IdClaveSSA_Aux = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
	
	--select * from #tmpProductos 
	------------ Reemplazo de Claves  	
	
	
	Select IdEmpresa, IdEstado, IdFarmacia, 
		IdClaveSSA, ClaveSSA, Descripcion, 
		IdClaveSSA as IdClaveSSA_Aux, ClaveSSA as ClaveSSA_Aux, 
		0 as Stock, 
		cast(Sum(Cantidad) as int) as Cant_A_Surtir, 0 as Cant_Surtida, 0 as CantidadTotal, 0 as CantExcedida, 
		0 as EsExcedida -- Por default todas las Claves estan excedidas 
	Into #tmpClaves 
	From #tmpProductos (NOLOCK) 
	Group By IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, Descripcion	



	Select 1 as Origen, IdClaveSSA 
	Into #tmpClaves_Generales 
	From #tmpProductos (NOLOCK) 
	Group By IdClaveSSA 
	

	Insert Into #tmpClaves_Generales 
	Select 2 as Origen, IdClaveSSA_Relacionada 
	From vw_Relacion_ClavesSSA_Claves C (NoLock) 
	Where IdEstado = @IdEstado and C.Status = 'A'  
		and Exists ( Select * From #tmpProductos B (NoLock) Where  B.IdClaveSSA_Aux = C.IdClaveSSA_Relacionada ) 
	Group By IdClaveSSA_Relacionada   
	

	---------  SE CREA UNA TEMPORAL DE LOS PRODUCTOS  -------------------------------------------------------------------------			
	
	
	
	
----		Exec spp_ValidaCantidadClavesSubPerfil____Farmacia 	
	
	
	---------------------- Se obtienen las cantidades del mes 
	Select 
		IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, 
		Cantidad, 
		Cantidad as CantidadBase, 0 as CantidadExcepciones, 0 as CantidadTotal 
	Into #tmpProgramacion 
	From CFG_CB_CuadroBasico_Claves_Programacion E (NoLock) 
	Where E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia 
		  and E.IdCliente = @IdCliente and E.IdSubCliente = @IdSubCliente  
		  and Año = year(@dFechaValidacion) and Mes = month(@dFechaValidacion) 
	
	Update E Set CantidadExcepciones = 
	IsNull(
		(
			Select sum (Cantidad) 
			From CFG_CB_CuadroBasico_Claves_Programacion_Excepciones P 
			Where E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia 
				  and E.IdCliente = P.IdCliente and E.IdSubCliente = P.IdSubCliente 
				  and E.Año = P.Año and E.Mes = P.Mes and E.IdClaveSSA = P.IdClaveSSA 
		), 0) 
	From #tmpProgramacion E 	
	
	Update P Set CantidadTotal = (CantidadBase + CantidadExcepciones)
	From #tmpProgramacion P 	
	---------------------- Se obtienen las cantidades del mes 
		
	
	------ select * from #tmpProgramacion 

---		spp_ValidaCantidadClavesSubPerfil____Farmacia 
	
	--Select * from #tmpClaves 
	--Select * from #tmpClaves_Generales  
	
	-------------------  VALIDAMOS QUE EXISTA EL SUB PERFIL  --------------------------------------------------------------------------------------------		 
	----------------------------------------- SE OBTIENE LO SURTIDO DEL MES EN CURSO  -------------------------------------------------	
	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, I.IdTipoDeDispensacion, P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, Sum(CantidadVendida) as Cantidad, 0 as Stock  
	Into #tmpClavesSurtidasMes_TipoDispensacion  
	From VentasEnc E (Nolock) 
	Inner Join VentasDet D (Nolock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta )
	Inner Join VentasInformacionAdicional I (Nolock)
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	-- Inner Join #tmpClaves_Generales C (Nolock) On ( P.IdClaveSSA_Sal = C.IdClaveSSA ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia 
		and E.IdCliente = @IdCliente and E.IdSubCliente = @IdSubCliente 
		and year(E.FechaRegistro) = year(@dFechaValidacion) and month(E.FechaRegistro) = month(@dFechaValidacion) 
		and P.IdClaveSSA_Sal In ( Select IdClaveSSA From #tmpClaves_Generales (Nolock) ) 
	Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia, I.IdTipoDeDispensacion, P.IdClaveSSA_Sal, P.ClaveSSA 
	 

	-- Select * from #tmpProgramacion 
	--Select * from #tmpClavesSurtidasMes_TipoDispensacion 

	
	----- Excluir los Tipos de Dispensacion Foraneos y Emision de Vales 
	Delete #tmpClavesSurtidasMes_TipoDispensacion 
	From #tmpClavesSurtidasMes_TipoDispensacion V 
	Inner Join #tmpParametros D (NoLock) On ( V.IdTipoDeDispensacion = D.Valor ) 


	Select IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, Sum(Cantidad) as Cantidad, 0 as Stock  
	Into #tmpClavesSurtidasMes 
	From #tmpClavesSurtidasMes_TipoDispensacion  
	Group by IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA 


	-- select * from #tmpClavesSurtidasMes 

---		spp_ValidaCantidadClavesSubPerfil____Farmacia 

		 
	-------------------  SE ACTUALIZA LA CANTIDAD SURTIDA Y LA CANTIDAD TOTAL  ---------------------------------- 
	Update T Set T.Cant_Surtida = IsNull(S.Cantidad, 0), T.CantidadTotal = (T.Cant_A_Surtir + IsNull(S.Cantidad, 0))
	From #tmpClaves T (Nolock) 
	Left Join #tmpClavesSurtidasMes S (Nolock) On ( T.IdClaveSSA = S.IdClaveSSA )	
	
	-- select * from #tmpClaves 
	
	Update T Set Stock = C.CantidadTotal 
	From #tmpClaves T (Nolock) 
	Inner Join #tmpProgramacion C (Nolock) 
		On ( T.IdEstado = C.IdEstado and T.IdFarmacia = C.IdFarmacia and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
			 and T.IdClaveSSA = C.IdClaveSSA ) 	


	-- Select * from #tmpClaves 

	Update T Set EsExcedida = 1 
	From #tmpClaves T (Nolock) 
	Where ( Cant_Surtida + CantidadTotal ) > Stock -- and 1 = 0 


	-- select * from #tmpClaves 
	---------------------------------------------------------------------------------------------------------------
	
	
	-------------- SE OBTIENEN LAS CLAVES DEL SUBPERFIL EN CASO DE QUE EXISTAN  ---------------------------------------------------
	----Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdClaveSSA, C.CantidadTotal as Stock, 1 as EsExcedida 
	----Into #tmpClavesSubPerfil 
	----From #tmpClaves T (Nolock) 
	----Inner Join #tmpProgramacion C (Nolock) 
	----	On ( T.IdEstado = C.IdEstado and T.IdFarmacia = C.IdFarmacia and C.IdCliente = @IdCliente and C.IdSubCliente = @IdSubCliente 
	----		 and T.IdClaveSSA = C.IdClaveSSA ) 
	----Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdClaveSSA, C.CantidadTotal, T.CantidadTotal 
	------ Having T.CantidadTotal > C.CantidadTotal 
	--------------------------------------------------------------------------------------------------------------------------------
	
	----select * from tmpProductos  
	--select * from #tmpClavesSurtidasMes  	
	--select * from #tmpClaves   	
	--select * from #tmpProgramacion    		
	----select * from #tmpClavesSubPerfil 	
	
---		spp_ValidaCantidadClavesSubPerfil____Farmacia 
	
	-------------------  SE ACTUALIZA EL STOCK, CANTIDAD EXCEDIDA ---------------------------------------------------------
	--Update T Set T.Stock = T.Stock, T.CantExcedida = T.Cant_Surtida, T.EsExcedida = 2 
	--From #tmpClaves T (Nolock) 	
	
	--Update T Set T.Stock = S.Stock, T.CantExcedida = ( T.CantidadTotal - S.Stock ), T.EsExcedida = S.EsExcedida
	--From #tmpClaves T (Nolock) 
	--Inner Join #tmpClavesSubPerfil S (Nolock) On ( T.IdClaveSSA = S.IdClaveSSA ) 
	--Where S.EsExcedida = 1 
	------------------------------------------------------------------------------------------------------------------------
	
--	select * from #tmpClaves  
	

	--------------------  SE REGRESA LA CONSULTA EN CASO DE QUE HAYA CLAVES EXCEDIDAS QUE PERTENECEN AL SUBPERFIL   ---------------
	Select 'Clave SSA' = ClaveSSA, 'Descripción Clave' = Descripcion, Stock, 
		'Surtido previo' = Cant_Surtida, 
		'Surtido actual' = Cant_A_Surtir, 
		'Cantidad Excedida' = abs(Stock-CantidadTotal) 
	From #tmpClaves (Nolock)  
	Where -- EsExcedida in ( 1, 2 )  
		CantidadTotal > Stock 
	
	-- Select * From #tmpClaves (Nolock)  
		
	-----------------------------------------------------------------------------------------------------------------
	
	
	
End
Go--#SQL	


