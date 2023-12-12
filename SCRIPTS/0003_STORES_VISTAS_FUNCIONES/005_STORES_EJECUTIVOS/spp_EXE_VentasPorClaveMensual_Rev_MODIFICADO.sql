
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_EXE_VentasPorClaveMensual_Rev' and xType = 'P' ) 
   Drop Proc spp_EXE_VentasPorClaveMensual_Rev
Go 

Create Proc spp_EXE_VentasPorClaveMensual_Rev 
( 
	@IdEstado varchar(2) = '21', 
	@IdClaveSSA varchar(4) = '*', 
	@IdCliente varchar(4) = '*', @IdSubCliente varchar(4) = '*', 	
	@IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*', 		
	@FechaInicial varchar(10) = '2011-07-01', @FechaFinal varchar(10) = '2012-07-15'   
	-- @FechaInicial varchar(10) = '2009-01-01', @FechaFinal varchar(10) = '2009-11-01'   	
) 	
With Encryption 
As 
Begin 
Set NoCount Off  
Set Dateformat YMD 
Declare 
	@iEnero int, @iFebrero int, @iMarzo int, @iAbril int, @iMayo int, @iJunio int, 
	@iJulio int, @iAgosto int, @iSeptiembre int, @iOctubre int, @iNoviembre int, @iDiciembre int 											


--- Asignar valores iniciales 
	If @IdClaveSSA = '' 
	   Set @IdClaveSSA = '*' 
	   
	Select @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Select @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
	
	
--- Procesar solo las Claves Solicitadas 
----	Select * 
----	Into #tmpProductos_Claves 
----	From vw_Productos_CodigoEAN  
----	
----	
----	If @IdClaveSSA <> '*' 
----	Begin
----	   Select 0 as x  
----	   Delete From #tmpProductos_Claves Where IdClaveSSA_Sal <> right('0000' + @IdClaveSSA, 4)  
----	End    	
	

--- Obtener los Datos Principales 
	Select E.IdEmpresa, E.IdEstado, 
		   E.Año, E.Mes,  -- , E.Dia, 
		   E.IdPrograma, E.Programa, E.IdSubPrograma, E.SubPrograma, 
		   P.IdClaveSSA_Sal as IdClaveSSA,P.ClaveSSA, P.DescripcionClave, 
		   -- D.IdProducto, D.CodigoEAN, 
		   cast(0 as numeric(14,4)) as PrecioLicitacion,   		   
		   sum(D.CantidadVendida) as CantidadVendida  
	Into #tmpVentasClaves 		   
	From 
	( 	   	
		Select C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioVenta, 
			--'' as IdPrograma, '' as Programa, '' as IdSubPrograma, '' as SubPrograma,
            C.IdPrograma, P.Programa, C.IdSubPrograma, P.SubPrograma,  
			datepart(yy, C.FechaRegistro) as Año, 
			datepart(mm, C.FechaRegistro) as Mes 
		From VentasEnc C (NoLock) 
		Inner Join vw_Programas_SubProgramas P (NoLock) 
			On ( C.IdPrograma = P.IdPrograma and C.IdSubPrograma = P.IdSubPrograma ) 
		Where 
		--5	  C.IdPrograma = @IdPrograma and C.IdSubPrograma = @IdSubPrograma  
                                                              --in (93,94,95,128,129,130)--= @IdSubPrograma  
			  -- and 
			  convert(varchar(10), C.FechaRegistro, 120) Between @FechaInicial and @FechaFinal 
		Group by C.IdEmpresa, C.IdEstado, C.IdFarmacia, C.FolioVenta, 
		    C.IdPrograma, P.Programa, C.IdSubPrograma, P.SubPrograma, 
			datepart(yy, C.FechaRegistro), datepart(mm, C.FechaRegistro) 
	) as E 
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN P (NOLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN 
		 -- and P.ClaveSSA = '3' 
		    --and P.IdTipoProducto = '02' 
		) 	
	Group by  E.IdEmpresa, E.IdEstado, 
		   E.Año, E.Mes, 
		   E.IdPrograma, E.Programa, E.IdSubPrograma, E.SubPrograma, 
		   P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave 
		   -- D.IdProducto, D.CodigoEAN 


--	select * 	from #tmpVentasClaves 


----    spp_EXE_VentasPorClaveMensual_Rev 
---- Realizar la Relacion de Claves 

------- Reemplazo de Claves 
	Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionClave = C.Descripcion 
	From #tmpVentasClaves B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
------- Reemplazo de Claves 


------- Asignacion de Precios 
	Update B Set PrecioLicitacion = IsNull(PC.Precio, 0) 
	From #tmpVentasClaves B (NoLock) 
	Left Join vw_Claves_Precios_Asignados PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
		On ( B.IdEstado = PC.IdEstado and B.ClaveSSA = PC.ClaveSSA 
			 and PC.IdCliente = @IdCliente and PC.IdSubCliente = @IdSubCliente ) 
------- Asignacion de Precios 

--- 
	Select 	   	
		IdEmpresa, IdEstado, 
		Año, Mes,  
		IdPrograma, Programa, IdSubPrograma, SubPrograma,		
		IdClaveSSA, ClaveSSA, DescripcionClave, 
		PrecioLicitacion, 
		sum(CantidadVendida) as CantidadVendida 
	Into  #tmpVentasClaves_Claves	
	From #tmpVentasClaves 	   		   	
	Group by 
		IdEmpresa, IdEstado, Año, Mes, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdClaveSSA, ClaveSSA, DescripcionClave, PrecioLicitacion   
		   		   	
---    spp_EXE_VentasPorClaveMensual_Rev


------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		 IdEmpresa, IdEstado, -- IdFarmacia, 
		 space(100) as IdPrograma, space(100) as Programa, space(100) as IdSubPrograma, space(100) as SubPrograma,	
		 IdClaveSSA, ClaveSSA, DescripcionClave, 
		 PrecioLicitacion, Año, 
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total, 
		 0 as InventarioRecibido, 0 as InventarioRestante, 
		 0 as InventarioA_Cobro, cast(0 as numeric(14,4)) as InventarioA_Cobro_Valorizado    
	into #tmpVentasClaves_Claves_Cruze	 
	From #tmpVentasClaves_Claves 

--- Agregar cada Clave Localizada 
    Insert Into #tmpVentasClaves_Claves_Cruze 
	Select Distinct 
		 IdEmpresa, IdEstado, 
		 IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		 IdClaveSSA, ClaveSSA, DescripcionClave, PrecioLicitacion, Año, 
		 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 as Total, 0 as InventarioRecibido, 0 as InventarioRestante, 
		 0 as InventarioA_Cobro, cast(0 as numeric(14,4)) as InventarioA_Cobro_Valorizado      	
	From #tmpVentasClaves_Claves 
	Order by  Año 

--	select * from #tmpVentasClaves_Claves_Cruze 
	

----------------- Asignar los totales por Mes 
	Update T Set Enero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iEnero and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Febrero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iFebrero and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iMarzo and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iAbril and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Mayo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iMayo and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Junio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iJunio and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iJulio and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iAgosto and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iSeptiembre and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Octubre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iOctubre and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iNoviembre and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iDiciembre and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Total = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and T.IdPrograma = X.IdPrograma and T.IdSubPrograma = X.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
----------------- Asignar los totales por Mes 

/* 
	Set @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Set @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
*/ 

--------------- Obtener los Inventarios Iniciales 
Set NoCount Off 
	Select T.IdEmpresa, T.IdFarmacia, 
		 T.IdProducto, T.CodigoEAN, T.Descripcion, 
		 T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, 
		 sum(CantidadRecibida) as CantidadRecibida  
	Into #tmpInventariosIniciales 	 
	From 
	( 
		select E.IdEmpresa, E.IdFarmacia, 
			 D.IdProducto, D.CodigoEAN, 
			 P.Descripcion, P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, 
			 P.DescripcionClave, 
			 -- L.ClaveLote, 
			 (case when L.ClaveLote like '%*%' then 1 else 0 end) as EsDeConsignacion,  
			 sum(L.Cantidad) as CantidadRecibida  
		From MovtosInv_Enc E (NoLock) 
		Inner Join MovtosInv_Det_CodigosEAN D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )  
		Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock) 
			On ( D.IdEmpresa = L.IdEmpresa and D.IdFarmacia = L.IdFarmacia and E.FolioMovtoInv = L.FolioMovtoInv 
				 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
		Inner Join vw_Productos_CodigoEAN P (NoLock) On ( P.IdProducto = L.IdProducto and P.CodigoEAN = L.CodigoEAN )  		 
		Where IdTipoMovto_Inv = 'IIC' 	
		Group by E.IdEmpresa, E.IdFarmacia, 
			 D.IdProducto, D.CodigoEAN, L.ClaveLote,  
			 P.Descripcion, P.IdClaveSSA_Sal, P.ClaveSSA, 
			 P.DescripcionClave  		 
	) as T 
	Where 		EsDeConsignacion = 1 
	Group by T.IdEmpresa, T.IdFarmacia, 
		 T.IdProducto, T.CodigoEAN, T.Descripcion, 
		 T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave 
	Order by T.DescripcionClave 
	
------- Reemplazo de Claves 
	Update B Set IdClaveSSA = C.IdClaveSSA -- , ClaveSSA = C.ClaveSSA, DescripcionClave = C.Descripcion 
	From #tmpInventariosIniciales B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( C.IdEstado = @IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
------- Reemplazo de Claves 

--- Salida Final del Proceso 
	Update P Set InventarioRecibido = I.CantidadRecibida, InventarioRestante = ( I.CantidadRecibida - P.Total )  
	From #tmpVentasClaves_Claves_Cruze P 
	Inner Join #tmpInventariosIniciales I (NoLock) On ( P.IdClaveSSA = I.IdClaveSSA ) 
	
-- 0 as InventarioA_Cobro, cast(0 as numeric(14,4)) as InventarioA_Cobro_Valorizado 	
	Update P Set InventarioA_Cobro =  abs(InventarioRestante), 
		InventarioA_Cobro_Valorizado = (abs(InventarioRestante) * PrecioLicitacion)
	From #tmpVentasClaves_Claves_Cruze P 	
	Where InventarioRestante <= 0 	

	Update P Set InventarioA_Cobro =  Total, 
		InventarioA_Cobro_Valorizado = (Total * PrecioLicitacion)
	From #tmpVentasClaves_Claves_Cruze P 	
	Where InventarioRecibido = 0 	


	Update P Set InventarioRestante = 0  
----		InventarioA_Cobro =  abs(InventarioRestante), 
----		InventarioA_Cobro_Valorizado = (abs(InventarioRestante) * PrecioLicitacion)
	From #tmpVentasClaves_Claves_Cruze P 	
	Where InventarioRestante <= 0 
	
------------------- Obtener los Inventarios Iniciales 



--------- Salida Final del Proceso 
	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'Rpt_DispensacionVentasPorClaveMensual' and xType = 'U' ) 
	   Drop Table Rpt_DispensacionVentasPorClaveMensual 

	Select 
		 T.IdEmpresa, Ex.Nombre as Empresa, T.IdEstado, E.Nombre as Estado, T.IdFarmacia, F.NombreFarmacia as Farmacia,		 
		 T.IdPrograma, T.Programa, T.IdSubPrograma, T.SubPrograma, 
		 T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, T.PrecioLicitacion, T.Año, 
		 T.Enero, T.Febrero, T.Marzo, T.Abril, T.Mayo, T.Junio, T.Julio, T.Agosto, T.Septiembre, T.Octubre, T.Noviembre, T.Diciembre, T.Total, 
		 T.InventarioRecibido, T.InventarioRestante, T.InventarioA_Cobro, T.InventarioA_Cobro_Valorizado    
	Into Rpt_DispensacionVentasPorClaveMensual 	  	 
	From #tmpVentasClaves_Claves_Cruze T 
	Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
	Order By T.IdClaveSSA, T.Año 


----	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'Rpt_DispensacionVentasPorClaveMensual' and xType = 'U' ) 
----	   Drop Table Rpt_DispensacionVentasPorClaveMensual 
----
----	Select 
----		 @EncPrincipal As EncabezadoPrincipal, @EncSecundario As EncabezadoSecundario,
----		 T.IdEmpresa, Ex.Nombre as Empresa, T.IdEstado, E.Nombre as Estado, T.IdFarmacia, F.NombreFarmacia as Farmacia,		 
----		 T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, 
----		 -- EsDeConsignacion, 
----		 T.PrecioLicitacion, T.Año, 
----		 T.Enero, T.Febrero, T.Marzo, T.Abril, T.Mayo, T.Junio, T.Julio, T.Agosto, T.Septiembre, T.Octubre, T.Noviembre, T.Diciembre, T.Total, 
----        InventarioRecibido, InventarioRestante, InventarioA_Cobro, InventarioA_Cobro_Valorizado		      
----	Into Rpt_DispensacionVentasPorClaveMensual 	   
----	From #tmpVentasClaves_Claves_Cruze T	 
----	Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
----	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
----	Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
----	Order By T.IdClaveSSA, T.Año 

--	sp_listacolumnas tmpMeses 
		   	
	
----    spp_EXE_VentasPorClaveMensual_Rev 
	
End	
Go 
	
	