If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Vales_VentasPorClaveMensual' and xType = 'P' ) 
   Drop Proc spp_Rpt_Vales_VentasPorClaveMensual
Go--#SQL 

---- Exec   spp_Rpt_Vales_VentasPorClaveMensual  '21', '0188', '*', '2011-08-09', '2011-11-09', '0'  

Create Proc spp_Rpt_Vales_VentasPorClaveMensual 
( 
	@IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0038',
	@IdClaveSSA varchar(4) = '*',		
	@FechaInicial varchar(10) = '2011-07-09', @FechaFinal varchar(10) = '2011-07-10', @TipoDispensacion smallint = 0,
	@TipoInsumo tinyint = 0, @SubFarmacias varchar(200) = '01'
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
	@EncSecundario varchar(500)

--- Asignar valores iniciales 
	--If @IdClaveSSA = '' 
	  -- Set @IdClaveSSA = '*' 
	
	
	Set @iVenta = 0 
	Set @iConsignacion = 1
	Set @sWhereSubFarmacias = ''
	

	if @SubFarmacias <> '' 
		Begin 
			Set @sWhereSubFarmacias = ' And L.IdSubFarmacia In ( ' + @SubFarmacias + ' ) '
		End 
	
	if @TipoDispensacion = 1 
	   Set @iConsignacion = 0     	
	
	if @TipoDispensacion = 2 
	   Set @iVenta = 1     	


	Select @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Select @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
	
	Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario 
	from dbo.fg_Unidad_EncabezadoReportesClientesSSA()


--- Obtener los Datos Principales 
	Select -- *
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, 
		T.Año, T.Mes, 	
		T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave,
		T.TasaIva,		
		T.PrecioLicitacion, T.EsDeConsignacion,		
		sum(T.CantidadVendida) as CantidadVendida 		 		  
	Into #tmpValesClaves 
	From 
	( 
		Select E.IdEmpresa, E.IdEstado, E.IdFarmacia,
			   datepart(yy, E.FechaRegistro) as Año, 
			   datepart(mm, E.FechaRegistro) as Mes,			   
			   P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, D.TasaIva,			   
			   cast(0 as numeric(14,4)) as PrecioLicitacion,   	
			   (case when L.ClaveLote like '%*%' then 1 else 0 end) as EsDeConsignacion,			   		   
			   sum(L.CantidadRecibida) as CantidadVendida			    
		From ValesEnc E (NoLock)	 
		Inner Join ValesDet D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.Folio ) 
		Inner Join ValesDet_Lotes L (NoLock) 
			On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.Folio = L.Folio 
		 		 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )		 		
		Inner Join vw_Productos_CodigoEAN P (NOLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN 
				and P.IdClaveSSA_sal in 
				( 
				    Select IdClaveSSA From CTE_ClavesAProcesar CTE(NoLock) Where  CTE.IdEstado = @IdEstado And CTE.IdFarmacia = @IdFarmacia  ) 
				) 
		Where  E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia 
		    -- And convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  --- + @sWhereSubFarmacias		
		Group by  E.IdEmpresa, E.IdEstado, E.IdFarmacia,
			   datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro),			    
			   P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, D.TasaIva, L.ClaveLote			   
	) as T 
    Where T.EsDeConsignacion in ( @iVenta, @iConsignacion ) 
	Group by T.IdEmpresa, T.IdEstado, T.IdFarmacia,
		T.Año, T.Mes, 		 
		T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, T.TasaIva,		
		T.PrecioLicitacion, T.EsDeConsignacion

	
	If @TipoInsumo <> 0 
		   Begin 
			  If @TipoInsumo = 1 
				 Delete From #tmpValesClaves Where TasaIva <> 0  --- Medicamentos 

			  If @TipoInsumo = 2
				 Delete From #tmpValesClaves Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
		   End

---- Realizar la Relacion de Claves 


------- Reemplazo de Claves 
	Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionClave = C.Descripcion 
	From #tmpValesClaves B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
------- Reemplazo de Claves 


----------- Asignacion de Precios 
	Update B Set PrecioLicitacion = IsNull(PC.Precio, 0) 
	From #tmpValesClaves B (NoLock) 
	Left Join vw_Claves_Precios_Asignados PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
		On ( B.IdEstado = PC.IdEstado and B.IdClaveSSA = PC.IdClaveSSA  ) 
----------- Asignacion de Precios 

--- 
	Select 	   	
		IdEmpresa, IdEstado, IdFarmacia,
		Año, Mes,  
		IdClaveSSA, ClaveSSA, DescripcionClave,		
		PrecioLicitacion, 
		--EsDeConsignacion, 
		sum(CantidadVendida) as CantidadVendida				
	Into  #tmpValesClaves_Claves	
	From #tmpValesClaves 	   		   	
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, Año, Mes, 
		IdClaveSSA, ClaveSSA, DescripcionClave, PrecioLicitacion, EsDeConsignacion    
		   		   	
---    spp_Rpt_VentasPorClaveMensual


------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		 IdEmpresa, IdEstado, IdFarmacia, 
		 IdClaveSSA, ClaveSSA, DescripcionClave, 
		 -- EsDeConsignacion,		  
		 PrecioLicitacion, Año, 
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total   
	into #tmpValesClaves_Claves_Cruze	 
	From #tmpValesClaves_Claves 

--- Agregar cada Clave Localizada 
    Insert Into #tmpValesClaves_Claves_Cruze 
	Select Distinct 
		 IdEmpresa, IdEstado, IdFarmacia,		 
		 IdClaveSSA, ClaveSSA, DescripcionClave,		 
		 --0 As EsDeConsignacion, 
		 PrecioLicitacion, Año, 
		 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
		 0 as Total   	
	From #tmpValesClaves_Claves 
	Order by  Año 
	
--	select * from #tmpVentasClaves_Claves_Cruze 

----------------- Asignar los totales por Mes 
	Update T Set Enero = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iEnero   ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 

	Update T Set Febrero = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iFebrero  ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iMarzo ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iAbril ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 

	Update T Set Mayo = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iMayo  ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 

	Update T Set Junio = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iJunio ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iJulio ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iAgosto ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iSeptiembre ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 

	Update T Set Octubre = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iOctubre ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iNoviembre ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iDiciembre ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 


	Update T Set Total = IsNull(( select sum(CantidadVendida) 
			From #tmpValesClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año ), 0 )  
	From #tmpValesClaves_Claves_Cruze T 
	
	
	
----------------- Asignar los totales por Mes 

/* 
	Set @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Set @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
*/ 


	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'Rpt_DispensacionVales_VentasPorClaveMensual' and xType = 'U' ) 
	   Drop Table Rpt_DispensacionVales_VentasPorClaveMensual 

	Select 
		 @EncPrincipal As EncabezadoPrincipal, @EncSecundario As EncabezadoSecundario,
		 T.IdEmpresa, Ex.Nombre as Empresa, T.IdEstado, E.Nombre as Estado, T.IdFarmacia, F.NombreFarmacia as Farmacia,		 
		 T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave,		 
		 -- EsDeConsignacion, 
		 T.PrecioLicitacion, T.Año, 
		 T.Enero, T.Febrero, T.Marzo, T.Abril, T.Mayo, T.Junio, T.Julio, T.Agosto, T.Septiembre, T.Octubre, T.Noviembre, T.Diciembre, T.Total     
	Into Rpt_DispensacionVales_VentasPorClaveMensual 	   
	From #tmpValesClaves_Claves_Cruze T	 
	Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
	Order By T.IdClaveSSA, T.Año 	

End	
Go--#SQL 
	