---------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Administrativos_VentasPorClaveMensualProgramas' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_VentasPorClaveMensualProgramas
Go--#SQL 

Create Proc spp_Rpt_Administrativos_VentasPorClaveMensualProgramas 
----( 
----	@IdEstado varchar(2) = '10', 
----	@IdClaveSSA varchar(4) = '*', 
----	-- @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0003', 	
----	-- @IdPrograma varchar(4) = '0002', @IdSubPrograma varchar(4) = '0001', 		
----	@FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-06-30'   
----) 	
With Encryption 
As 
Begin 
Set NoCount On   
Set Dateformat YMD 
Declare 
	@iEnero int, @iFebrero int, @iMarzo int, @iAbril int, @iMayo int, @iJunio int, 
	@iJulio int, @iAgosto int, @iSeptiembre int, @iOctubre int, @iNoviembre int, @iDiciembre int 											


----- Asignar valores iniciales 
--	If @IdClaveSSA = '' 
--	   Set @IdClaveSSA = '*' 
	   
	Select @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Select @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
	
	

------- Obtener los Datos Principales 
----	Select -- *
----		T.IdEmpresa, T.IdEstado, 
----		T.Año, T.Mes,  -- , E.Dia, 
----		-- T.IdPrograma, T.Programa, T.IdSubPrograma, T.SubPrograma, 
----		T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, 
----		-- T.IdProducto, T.CodigoEAN, 
----		T.PrecioLicitacion, -- T.EsDeConsignacion, 
----		sum(T.CantidadVendida) as CantidadVendida  
----		-- sum(T.CantidadVendida_Consignacion) as CantidadVendida_Consignacion, 
----		-- sum(T.CantidadVendida_Venta) as CantidadVendida_Venta  		  
----	Into #tmpVentasClaves 
----	From 
----	( 
----		Select E.IdEmpresa, E.IdEstado, 
----			   datepart(yy, E.FechaRegistro) as Año, 
----			   datepart(mm, E.FechaRegistro) as Mes,  
----			   -- E.Año, E.Mes,  -- , E.Dia, 
----			   -- E.IdPrograma, E.Programa, E.IdSubPrograma, E.SubPrograma, 
----			   P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, 
----			   -- D.IdProducto, D.CodigoEAN, 
----			   cast(0 as numeric(14,4)) as PrecioLicitacion,   	
----			   -- (case when L.ClaveLote like '%*%' then 1 else 0 end) 
----			   0 as EsDeConsignacion, 
----			   
----			   sum(D.CantidadVendida) as CantidadVendida    
----			   -- sum((case when L.ClaveLote like '%*%' then L.CantidadVendida else 0 end)) as CantidadVendida_Consignacion,  
----			   -- sum((case when L.ClaveLote like '%*%' then 0 else L.CantidadVendida end)) as CantidadVendida_Venta  
----		From VentasEnc E (NoLock) 	   
----	--	Into #tmpVentasClaves 		   
--------		From 
--------		( 	   	
--------			Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioVenta, 
--------				-- C.IdPrograma, P.Programa, C.IdSubPrograma, P.SubPrograma,  
--------				-- '' as IdPrograma, '' as Programa, '' as IdSubPrograma, '' as SubPrograma, 
--------				datepart(yy, C.FechaRegistro) as Año, 
--------				datepart(mm, C.FechaRegistro) as Mes 
--------			From VentasEnc C (NoLock) 
--------			-- Inner Join vw_Programas_SubProgramas P (NoLock) 
--------			--	On ( C.IdPrograma = P.IdPrograma and C.IdSubPrograma = P.IdSubPrograma ) 
--------			Where 
--------				  -- C.IdPrograma = @IdPrograma and C.IdSubPrograma = @IdSubPrograma and 
--------				  convert(varchar(10), C.FechaRegistro, 120) Between @FechaInicial and @FechaFinal 
--------			Group by C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioVenta, 
--------				-- C.IdPrograma, P.Programa, C.IdSubPrograma, P.SubPrograma, 
--------				datepart(yy, C.FechaRegistro), datepart(mm, C.FechaRegistro) 
--------		) as E 
----		Inner Join VentasDet D (NoLock) 
----			On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
----		-- Inner Join VentasDet_Lotes L (NoLock) 
----		--	On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
----		-- 		 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 		
----		Inner Join vw_Productos_CodigoEAN P (NOLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN 
----				and P.IdClaveSSA_sal in 
----				 ( 70, 71, 73, 74, 162, 163, 333, 336,  424, 426, 1054, 1071, 
----					1222, 1227, 1230, 1234, 1239, 1250, 1256, 1672, 1729, 1730, 1731, 1768, 2590  ) 
----				) 
----		Where convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal 		
----		Group by  E.IdEmpresa, E.IdEstado, 
----			   datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro), 
----			   -- E.IdPrograma, E.Programa, E.IdSubPrograma, E.SubPrograma, 
----			   P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave  
----			   -- D.IdProducto, D.CodigoEAN -- , L.ClaveLote  
----	) as T 
----	Group by T.IdEmpresa, T.IdEstado, 
----		T.Año, T.Mes,  -- , E.Dia, 
----		-- T.IdPrograma, T.Programa, T.IdSubPrograma, T.SubPrograma, 
----		T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, 
----		-- T.IdProducto, T.CodigoEAN, 
----		T.PrecioLicitacion -- , T.EsDeConsignacion
----	
----	
----
--------    spp_Rpt_Administrativos_VentasPorClaveMensual 
-------- Realizar la Relacion de Claves 
----
----------- Reemplazo de Claves 
----	Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionClave = C.Descripcion 
----	From #tmpVentasClaves B (NoLock) 
----	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
----------- Reemplazo de Claves 
----
----
--------------- Asignacion de Precios 
--------	Update B Set PrecioLicitacion = IsNull(PC.Precio, 0) 
--------	From #tmpVentasClaves B (NoLock) 
--------	Left Join vw_Claves_Precios_Asignados PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
--------		On ( B.IdEstado = PC.IdEstado and B.IdClaveSSA = PC.IdClaveSSA 
--------			 and PC.IdCliente = @IdCliente and PC.IdSubCliente = @IdSubCliente ) 
--------------- Asignacion de Precios 

--- 
	Select 	   	
		IdEmpresa, IdEstado, 
		datepart(yy, FechaReceta) as Año, 
		datepart(mm, FechaReceta) as Mes,  
----		datepart(yy, FechaRegistro) as Año, 
----		datepart(mm, FechaRegistro) as Mes,  		
		IdPrograma, Programa, IdSubPrograma, SubPrograma,		
		IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, 
		DescripcionSal as DescripcionClave, 
		PrecioLicitacion, 
		-- EsDeConsignacion, 
		sum(Cantidad) as CantidadVendida   
--		sum(CantidadVendida_Consignacion) as CantidadVendida_Consignacion, 
--		sum(CantidadVendida_Venta) as CantidadVendida_Venta  		  
	Into  #tmpVentasClaves_Claves	
	From RptAdmonDispensacion  	   		   	
	Group by 
		IdEmpresa, IdEstado, 
		datepart(yy, FechaReceta), 
		datepart(mm, FechaReceta),  		
----		datepart(yy, FechaRegistro), 
----		datepart(mm, FechaRegistro),  		
		-- Año, Mes, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, PrecioLicitacion -- , EsDeConsignacion    

		   		   	
---    spp_Rpt_Administrativos_VentasPorClaveMensual


------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		 IdEmpresa, IdEstado, -- IdFarmacia, 
		 space(100) as IdPrograma, space(100) as Programa, space(100) as IdSubPrograma, space(100) as SubPrograma,	
		 IdClaveSSA, ClaveSSA, DescripcionClave, 
		 PrecioLicitacion, Año, 
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total, 
		 0 as Venta, 0 as Consignacion, 0 as InventarioRecibido, 0 as EsDeConsignacion   
	into #tmpVentasClaves_Claves_Cruze	 
	From #tmpVentasClaves_Claves 

--- Agregar cada Clave Localizada 
    Insert Into #tmpVentasClaves_Claves_Cruze 
	Select Distinct 
		 IdEmpresa, IdEstado, 
		 IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		 IdClaveSSA, ClaveSSA, DescripcionClave, PrecioLicitacion, Año, 
		 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
		 0 as Total, 
		 0 as Venta, 0 as Consignacion, 0 as InventarioRecibido, 0 as EsDeConsignacion    	
	From #tmpVentasClaves_Claves 
	Order by  Año 
	
--	select * from #tmpVentasClaves_Claves_Cruze 

----------------- Asignar los totales por Mes 
	Update T Set Enero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma 
			      and X.Año = T.Año and X.Mes = @iEnero  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Febrero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iFebrero  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iMarzo  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iAbril  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Mayo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iMayo  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Junio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iJunio  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iJulio  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iAgosto  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iSeptiembre  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Octubre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iOctubre  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iNoviembre  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año and X.Mes = @iDiciembre  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Total = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
			      and X.Año = T.Año  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	
----	Update T Set Venta = IsNull(( select sum(CantidadVendida_Venta) 
----			From #tmpVentasClaves_Claves X 
----			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
----			      and X.Año = T.Año  ), 0 )  
----	From #tmpVentasClaves_Claves_Cruze T 	 
----	-- Where EsDeConsignacion = 0 
----	
----	Update T Set Consignacion = IsNull(( select sum(CantidadVendida_Consignacion) 
----			From #tmpVentasClaves_Claves X 
----			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma
----			      and X.Año = T.Año  ), 0 )  
----	From #tmpVentasClaves_Claves_Cruze T 	 
----	-- Where EsDeConsignacion = 1 	

	-- CantidadVendida_Consignacion, 
	-- sum(T.CantidadVendida_Venta) as CantidadVendida_Venta	
	-- 	 0 as Venta, 0 as Consignacion, 0 as InventarioRecibido   	
		 	
	
----------------- Asignar los totales por Mes 

/* 
	Set @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Set @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
*/ 


	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'Rpt_DispensacionPorClaveMensual_Programas' and xType = 'U' ) 
	   Drop Table Rpt_DispensacionPorClaveMensual_Programas 

	Select 
		 IdEmpresa, IdEstado, 
		 IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		 IdClaveSSA, ClaveSSA, DescripcionClave, PrecioLicitacion, Año, 
		 Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre, Total, 
		 Venta, Consignacion, InventarioRecibido, ( InventarioRecibido - Consignacion ) as InventarioRestante, 0 as EsSeguroPopular     
	Into Rpt_DispensacionPorClaveMensual_Programas 	   
	From #tmpVentasClaves_Claves_Cruze 
	Order by IdPrograma, IdSubPrograma, DescripcionClave 

------- Revisar cambio a Configurable 
----	update x Set EsSeguroPopular = 1  
----	From Rpt_DispensacionPorClaveMensual_Programas x 
----	Where IdPrograma = 2 and IdSubPrograma = 1 


----	Select 
----		 IdEmpresa, IdEstado, 
----		 IdPrograma, Programa, IdSubPrograma, SubPrograma, 
----		 IdClaveSSA, ClaveSSA, DescripcionClave, PrecioLicitacion, Año, 
----		 sum(Enero) as Enero, sum(Febrero) as Febrero, sum(Marzo) as Marzo, sum(Abril) as Abril, 
----		 sum(Mayo) as Mayo, sum(Junio) as Junio, sum(Julio) as Julio, sum(Agosto) as Agosto, 
----		 sum(Septiembre) as Septiembre, sum(Octubre) as Octubre, sum(Noviembre) as Noviembre, sum(Diciembre) as Diciembre, 
----		 sum(Total) as Total, 
----		 sum(Venta) as Venta, sum(Consignacion) as Consignacion, 
----		 sum(InventarioRecibido) as InventarioRecibido, sum(InventarioRestante) as InventarioRestante 
----	From Rpt_DispensacionPorClaveMensual_Programas 
----	Group by 
----		 IdEmpresa, IdEstado, 
----		 IdPrograma, Programa, IdSubPrograma, SubPrograma, 
----		 IdClaveSSA, ClaveSSA, DescripcionClave, PrecioLicitacion, Año 
	
----	Select 
----		 -- IdEmpresa, IdEstado, 
----		 -- IdPrograma, Programa, IdSubPrograma, SubPrograma, 
----		 IdClaveSSA, ClaveSSA, DescripcionClave, -- PrecioLicitacion, 
----		 Año, 
----		 Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre, Total  
----		 -- Venta, Consignacion, InventarioRecibido, InventarioRestante, EsSeguroPopular   
----	From Rpt_DispensacionPorClaveMensual_Programas 	
----	
	
--	sp_listacolumnas tmpMeses 
		   	
	
----    spp_Rpt_Administrativos_VentasPorClaveMensualProgramas  
	
End	
Go--#SQL 
	