------------------------------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( select Name From Sysobjects (NoLock) Where Name = 'INT_ND_RPT_SurtidosUnidades_Mensual_Detallado' and xType = 'U' )  
	   Drop Table INT_ND_RPT_SurtidosUnidades_Mensual_Detallado  
Go--#SQL 	   

If Exists ( select Name From Sysobjects (NoLock) Where Name = 'INT_ND_RPT_SurtidosUnidades_Mensual' and xType = 'U' )  
	   Drop Table INT_ND_RPT_SurtidosUnidades_Mensual
Go--#SQL 

If Exists ( select Name From Sysobjects (NoLock) Where Name = 'INT_ND_RPT_SurtidosUnidades' and xType = 'U' )  
	   Drop Table INT_ND_RPT_SurtidosUnidades
Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_INT_ND_RPT_SurtidoUnidades_Mensual' and xType = 'P' ) 
   Drop Proc spp_INT_ND_RPT_SurtidoUnidades_Mensual 
Go--#SQL 

----	Select * From INT_ND_RPT_SurtidosUnidades_Mensual_Detallado (Nolock) 
		
Create Proc spp_INT_ND_RPT_SurtidoUnidades_Mensual  
( 
	@IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', 
	@FechaInicial varchar(10) = '2014-08-16', @FechaFinal varchar(10) = '2015-04-30' 
) 	
With Encryption 
As 
Begin 
Set NoCount On  
Set Dateformat YMD 
Declare 
	@iEnero int, @iFebrero int, @iMarzo int, @iAbril int, @iMayo int, @iJunio int, 
	@iJulio int, @iAgosto int, @iSeptiembre int, @iOctubre int, @iNoviembre int, @iDiciembre int,
	@sWhereSubFarmacias varchar(200) 											

Declare 
	@iVenta int, @iConsignacion int, 
	@EncPrincipal varchar(500), 
	@EncSecundario varchar(500),
	@DescTipoDispensacion varchar(50)

--- Asignar valores iniciales 
	--If @IdClaveSSA = '' 
	  -- Set @IdClaveSSA = '*' 
	
	
	Set @iVenta = 0 
	Set @iConsignacion = 1
	Set @sWhereSubFarmacias = ''
	Set @DescTipoDispensacion = '' 


	Select @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Select @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
	
	Select * into #tmp__vw_ClavesSSA_Sales From vw_ClavesSSA_Sales (Nolock)



--- Obtener los Datos Principales 
	Select -- *
		T.IdEmpresa, T.IdEstado, T.CodigoCliente, T.IdFarmacia, T.Farmacia, 
		T.Año, T.Mes, T.ClaveSSA_ND, T.Descripcion_Mascara, 
		sum(T.CantidadVendida) as CantidadVendida 		
	Into #tmpVentasClaves 
	From 
	( 
		Select 
			E.IdEmpresa, E.IdEstado, E.CodigoCliente, 
			E.IdFarmacia, E.Farmacia, 
			datepart(yy, E.FechaRegistro) as Año, 
			datepart(mm, E.FechaRegistro) as Mes,
			E.ClaveSSA_ND, E.Descripcion_Mascara, 
			cast(sum(Cantidad) as int) as CantidadVendida 
		From INT_ND_RptAdmonDispensacion_Detallado__General E (NoLock) 
		Where E.EsEnResguardo = 0 and Incluir = 1 and 
			E.IdEmpresa = @IdEmpresa AND E.IdEstado = @IdEstado		
		    And convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal 
		    ---- And E.IdFarmacia = 30 
		Group by  
			E.IdEmpresa, E.IdEstado, E.CodigoCliente, E.IdFarmacia, E.Farmacia, 
			datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro), 
			E.ClaveSSA_ND, E.Descripcion_Mascara 
	) as T 
	Group by 
		T.IdEmpresa, T.IdEstado, T.CodigoCliente, T.IdFarmacia, T.Farmacia, 
		T.Año, T.Mes, T.ClaveSSA_ND, T.Descripcion_Mascara	



--		Select top 1 * From INT_ND_RptAdmonDispensacion_Detallado__General 

 --		spp_INT_ND_RPT_SurtidoUnidades_Mensual 	
 	
 	----select top 10 count(*)  
 	----from #tmpVentasClaves 
 	
 	----select top 10 * 
 	----from #tmpVentasClaves 
 	 	
 	
------------ Preparar la tabla base de los calculos 
	Select 	   	
		IdEmpresa, IdEstado, CodigoCliente, IdFarmacia, Farmacia,  
		ClaveSSA_ND, Descripcion_Mascara,
		Año, Mes,  
		sum(CantidadVendida) as CantidadVendida				
	Into  #tmpVentasClaves_Claves	
	From #tmpVentasClaves 	   		   	
	Group by 
		IdEmpresa, IdEstado, CodigoCliente, IdFarmacia, Farmacia,  
		Año, Mes,  
		ClaveSSA_ND, Descripcion_Mascara
		   		   	

------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		 IdEmpresa, IdEstado, CodigoCliente, IdFarmacia, Farmacia,  
		 ClaveSSA_ND, Descripcion_Mascara, 
		 Año, 
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total   
	into #tmpVentasClaves_Claves_Cruze	 
	From #tmpVentasClaves_Claves 


--- Agregar cada Clave Localizada 
    Insert Into #tmpVentasClaves_Claves_Cruze 
	Select Distinct 
		 IdEmpresa, IdEstado, CodigoCliente, IdFarmacia, Farmacia,  
		 ClaveSSA_ND, Descripcion_Mascara,  
		 Año,  
		 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
		 0 as Total   	
	From #tmpVentasClaves_Claves 
	Order by  Año, IdFarmacia, Descripcion_Mascara  
	
	
--		select * from #tmpVentasClaves_Claves_Cruze 


----------------- Asignar los totales por Mes 
	Update T Set Enero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iEnero  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Febrero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND  
			and X.Año = T.Año and X.Mes = @iFebrero  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iMarzo  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iAbril  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Mayo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iMayo  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Junio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iJunio  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iJulio  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iAgosto  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iSeptiembre  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Octubre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iOctubre  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iNoviembre  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año and X.Mes = @iDiciembre  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Total = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia  
			and X.ClaveSSA_ND = T.ClaveSSA_ND 
			and X.Año = T.Año  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T	
----------------- Asignar los totales por Mes 

		 	
		
------------------------------------------------------------------------------------------------------------------------------------------ 
	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'INT_ND_RPT_SurtidosUnidades_Mensual_Detallado' and xType = 'U' ) 
	   Drop Table INT_ND_RPT_SurtidosUnidades_Mensual_Detallado 

	Select 
		 -- Identity(int, 1, 1) as Consecutivo, 
		 -- T.IdEstado, 
		 T.CodigoCliente, 
		 T.IdFarmacia, F.NombreFarmacia as Farmacia, F.IdTipoUnidad, space(100) as TipoUnidad, 	 
		 T.ClaveSSA_ND, T.Descripcion_Mascara, 
		 T.Año, 
		 T.Enero, T.Febrero, T.Marzo, T.Abril, T.Mayo, T.Junio, T.Julio, T.Agosto, T.Septiembre, T.Octubre, T.Noviembre, T.Diciembre, T.Total 
	Into INT_ND_RPT_SurtidosUnidades_Mensual_Detallado 	   
	From #tmpVentasClaves_Claves_Cruze T	
	Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )
	Order By T.IdFarmacia, T.Descripcion_Mascara, T.Año  
	
	
	Update T Set T.TipoUnidad = C.Descripcion 
	From INT_ND_RPT_SurtidosUnidades_Mensual_Detallado T (Nolock)
	Inner Join CatTiposUnidades C On ( C.IdTipoUnidad = T.IdTipoUnidad ) 	
	
	
	
	
------------------------------------------------------------------------------------------------------------------------------------------ 
	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'INT_ND_RPT_SurtidosUnidades_Mensual' and xType = 'U' ) 
	   Drop Table INT_ND_RPT_SurtidosUnidades_Mensual 

	Select 
		 T.ClaveSSA_ND, T.Descripcion_Mascara, T.Año, 
		 sum(T.Enero) as Enero, sum(T.Febrero) as Febrero, sum(T.Marzo) as Marzo, 
		 sum(T.Abril) as Abril, sum(T.Mayo) as Mayo, sum(T.Junio) as Junio, 
		 sum(T.Julio) as Julio, sum(T.Agosto) as Agosto, sum(T.Septiembre) as Septiembre, 
		 sum(T.Octubre) as Octubre, sum(T.Noviembre) as Noviembre, sum(T.Diciembre) as Diciembre, 
		 sum(T.Total) as Total  
	Into INT_ND_RPT_SurtidosUnidades_Mensual
	From INT_ND_RPT_SurtidosUnidades_Mensual_Detallado T		
	Group by  
		T.ClaveSSA_ND, T.Descripcion_Mascara, T.Año	
	Order By T.Descripcion_Mascara, T.Año  
	
	
	
------------------------------------------------------------------------------------------------------------------------------------------ 
	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'INT_ND_RPT_SurtidosUnidades' and xType = 'U' ) 
	   Drop Table INT_ND_RPT_SurtidosUnidades

	Select 
		 T.ClaveSSA_ND, T.Descripcion_Mascara, 
		 sum(T.Total) as Total  
	Into INT_ND_RPT_SurtidosUnidades
	From INT_ND_RPT_SurtidosUnidades_Mensual T		
	Group by  
		T.ClaveSSA_ND, T.Descripcion_Mascara 
	Order By T.Descripcion_Mascara 
	
----		spp_INT_ND_RPT_SurtidoUnidades_Mensual	
	
End	
Go--#SQL 
	
	
	 
	
		