If Exists ( select Name From Sysobjects (NoLock) Where Name = 'Rpt_DispensacionVentasPorClaveMensual' and xType = 'U' ) 
   Drop Table Rpt_DispensacionVentasPorClaveMensual 
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_EdoJuris_VentasPorClaveMensual' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_EdoJuris_VentasPorClaveMensual
Go--#SQL 

----	Exec   spp_Rpt_CteReg_EdoJuris_VentasPorClaveMensual  '21', '006', 0,  '2012-02-01', '2012-02-01', '0'  


Create Proc spp_Rpt_CteReg_EdoJuris_VentasPorClaveMensual 
( 
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '006', 
	-- @IdClaveSSA varchar(4) = '*', 
	@EsConcentrado int = 1, 
	@FechaInicial varchar(10) = '2011-07-01', @FechaFinal varchar(10) = '2011-08-31', 
	@TipoDispensacion smallint = 0, @TipoInsumo tinyint = 0, @SubFarmacias varchar(200) = '01'
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
	@sSql varchar(8000) 

Declare 
	@iExisteInformacion int, 
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


---------- Obtener lista de Farmacias a Procesas  
	Select IdEstado, @IdEstado as Estado, IdJurisdiccion, Descripcion as Jurisdiccion 
	Into #tmpJuris 
	From CatJurisdicciones 
	Where IdEstado = @IdEstado and IdJurisdiccion = @IdJurisdiccion 

	if @IdJurisdiccion = '*' 
		Begin 
			Insert Into #tmpJuris 
			Select IdEstado, @IdEstado as Estado, IdJurisdiccion as IdJuris, Descripcion as Jurisdiccion 
			From CatJurisdicciones 
			Where IdEstado = @IdEstado -- and IdJurisdiccion = @IdJurisdiccion 	   
	    End 

	
	Select F.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia 
	Into #tmpFarmacias 
	From vw_Farmacias F (NoLock) 
	Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion ) 
	
--	select * from #tmpFarmacias 
---------- Obtener lista de Farmacias a Procesas  


--- Obtener los Datos Principales 
	Select @iExisteInformacion = count(*) 
	From VentasEnc E (NoLock)	 
	Inner Join #tmpFarmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )
	Where convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  --- + @sWhereSubFarmacias		


	--- select @iExisteInformacion as Datos 


	Select -- * 
		T.IdEmpresa, T.IdEstado, T.IdJurisdiccion, T.IdFarmacia, 
		T.A�o, T.Mes, 	
		T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave,
		T.TasaIva,		
		T.PrecioLicitacion, T.EsDeConsignacion,
		T.IdTipoDeDispensacion, T.Descripcion as DescTipoDispensacion, 
		sum(T.CantidadVendida) as CantidadVendida 		 		  
	Into #tmpVentasClaves 
	From 
	( 
		Select E.IdEmpresa, E.IdEstado, F.IdJurisdiccion, E.IdFarmacia,
			   datepart(yy, E.FechaRegistro) as A�o, 
			   datepart(mm, E.FechaRegistro) as Mes,			   
			   P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, D.TasaIva,			   
			   cast(0 as numeric(14,4)) as PrecioLicitacion,   	
			   (case when L.ClaveLote like '%*%' then 1 else 0 end) as EsDeConsignacion,
			   VA.IdTipoDeDispensacion,	TD.Descripcion, 		   
			   sum(L.CantidadVendida) as CantidadVendida			    
		From VentasEnc E (NoLock)	 
		Inner Join #tmpFarmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
		Inner Join VentasDet D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
		Inner Join VentasDet_Lotes L (NoLock) 
			On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
		 		 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )
		Inner Join VentasInformacionAdicional VA (Nolock)
			On ( E.IdEmpresa = VA.IdEmpresa and E.IdEstado = VA.IdEstado and E.IdFarmacia = VA.IdFarmacia and E.FolioVenta = VA.FolioVenta )
		Inner Join CatTiposDispensacion TD (Nolock) On (VA.IdTipoDeDispensacion = TD.IdTipoDeDispensacion) 		
		Inner Join vw_Productos_CodigoEAN P (NOLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
----				and P.IdClaveSSA_sal in 
----				( 
----				    Select IdClaveSSA From CTE_ClavesAProcesar CTE(NoLock) Where  CTE.IdEstado = @IdEstado And CTE.IdFarmacia = @IdFarmacia  ) 
----				) 
		Where  -- E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia 
		    -- And 
		    @iExisteInformacion > 0 
		    and 
		    ( 
				convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  --- + @sWhereSubFarmacias		
			) 
		Group by  E.IdEmpresa, E.IdEstado, F.IdJurisdiccion, E.IdFarmacia,
			   datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro),			    
			   P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, D.TasaIva, L.ClaveLote, VA.IdTipoDeDispensacion, TD.Descripcion			   
	) as T 
    Where T.EsDeConsignacion in ( @iVenta, @iConsignacion ) 
	Group by T.IdEmpresa, T.IdEstado, T.IdJurisdiccion, T.IdFarmacia,
		T.A�o, T.Mes, 		 
		T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, T.TasaIva,		
		T.PrecioLicitacion, T.EsDeConsignacion, T.IdTipoDeDispensacion, T.Descripcion

	-- Se Borran los tipos de dispensacion que se dieron por Receta Generada por vales
	Delete From #tmpVentasClaves Where IdTipoDeDispensacion = '07'  --- Receta Generada por vales 
	
	
	If @TipoInsumo <> 0 
		   Begin 
			  If @TipoInsumo = 1 
				 Delete From #tmpVentasClaves Where TasaIva <> 0  --- Medicamentos 

			  If @TipoInsumo = 2
				 Delete From #tmpVentasClaves Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
		   End
--	select count(*) from #tmpVentasClaves 
	
--          spp_Rpt_CteReg_EdoJuris_VentasPorClaveMensual 	
	
	
	--select * from #tmpVentasClaves

----    spp_Rpt_CteReg_EdoJuris_VentasPorClaveMensual 
---- Realizar la Relacion de Claves 

----	-- Se Actualiza la descripcion del Tipo de Dispensacion
----	Update V Set V.DescTipoDispensacion = T.Descripcion
----		From #tmpVentasClaves V (Nolock)
----		Inner Join CatTiposDispensacion T (Nolock) On (V.IdTipoDeDispensacion = T.IdTipoDeDispensacion)

------- Reemplazo de Claves 
	Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionClave = C.Descripcion 
	From #tmpVentasClaves B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
------- Reemplazo de Claves 


----------- Asignacion de Precios 
	Update B Set PrecioLicitacion = IsNull(PC.Precio, 0) 
	From #tmpVentasClaves B (NoLock) 
	Left Join vw_Claves_Precios_Asignados PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
		On ( B.IdEstado = PC.IdEstado and B.IdClaveSSA = PC.IdClaveSSA  ) 
----------- Asignacion de Precios 

--- 
	Select 	   	
		T.IdEmpresa, T.IdEstado, F.Estado, T.IdJurisdiccion, F.Jurisdiccion, T.IdFarmacia, F.Farmacia, 		
		T.A�o, T.Mes,  
		T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave,
		T.IdTipoDeDispensacion, T.DescTipoDispensacion,
		T.PrecioLicitacion, 
		--EsDeConsignacion, 
		sum(T.CantidadVendida) as CantidadVendida				
	Into  #tmpVentasClaves_Claves	
	From #tmpVentasClaves T 	   		   	
	Inner Join vw_Farmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )	
	Group by 
		T.IdEmpresa, T.IdEstado, F.Estado, T.IdJurisdiccion, F.Jurisdiccion, T.IdFarmacia, F.Farmacia, T.A�o, T.Mes, 
		T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, T.IdTipoDeDispensacion, T.DescTipoDispensacion, T.PrecioLicitacion, T.EsDeConsignacion    

--- Unificar 	
	If @EsConcentrado = 1 
	Begin  
	   Update T Set 
		-- IdJurisdiccion = '000', Jurisdiccion = 'Todas', 
		IdFarmacia = '', Farmacia = '' 
	   From #tmpVentasClaves_Claves T 
	End 
	
	
		   		   	
---    spp_Rpt_CteReg_EdoJuris_VentasPorClaveMensual


------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		IdEmpresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
		 IdClaveSSA, ClaveSSA, DescripcionClave, 
		 -- EsDeConsignacion,
		 IdTipoDeDispensacion, DescTipoDispensacion, 
		 PrecioLicitacion, A�o, 
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total   
	into #tmpVentasClaves_Claves_Cruze	 
	From #tmpVentasClaves_Claves 

--- Agregar cada Clave Localizada 
    Insert Into #tmpVentasClaves_Claves_Cruze 
	Select Distinct 
		 IdEmpresa, IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia,	Farmacia, 
		 IdClaveSSA, ClaveSSA, DescripcionClave,
		 IdTipoDeDispensacion, DescTipoDispensacion,
		 --0 As EsDeConsignacion, 
		 PrecioLicitacion, A�o, 
		 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
		 0 as Total   	
	From #tmpVentasClaves_Claves 
	Order by  A�o 

--	select * from #tmpVentasClaves_Claves_Cruze 

----------------- Asignar los totales por Mes 
	Update T Set Enero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iEnero and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Febrero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iFebrero and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iMarzo and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iAbril and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Mayo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iMayo and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Junio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iJunio and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iJulio and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iAgosto and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iSeptiembre and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Octubre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iOctubre and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iNoviembre and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.Mes = @iDiciembre and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Total = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdJurisdiccion = T.IdJurisdiccion and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.A�o = T.A�o and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	
----	Update T Set Venta = IsNull(( select sum(CantidadVendida_Venta) 
----			From #tmpVentasClaves_Claves X 
----			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
----			      and X.A�o = T.A�o  ), 0 )  
----	From #tmpVentasClaves_Claves_Cruze T 	 
----	-- Where EsDeConsignacion = 0 
----	
----	Update T Set Consignacion = IsNull(( select sum(CantidadVendida_Consignacion) 
----			From #tmpVentasClaves_Claves X 
----			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
----			      and X.A�o = T.A�o  ), 0 )  
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


	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'Rpt_DispensacionVentasPorClaveMensual' and xType = 'U' ) 
	   Drop Table Rpt_DispensacionVentasPorClaveMensual 

	Select 
		 @EncPrincipal As EncabezadoPrincipal, @EncSecundario As EncabezadoSecundario,
		 T.IdEmpresa, Ex.Nombre as Empresa, 
		 T.IdEstado, T.Estado, 
		 T.IdJurisdiccion, T.Jurisdiccion, 
		 T.IdFarmacia, T.Farmacia,		 
		 T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave,
		 T.IdTipoDeDispensacion, T.DescTipoDispensacion,
		 -- EsDeConsignacion, 
		 T.PrecioLicitacion, T.A�o, 
		 T.Enero, T.Febrero, T.Marzo, T.Abril, T.Mayo, T.Junio, T.Julio, T.Agosto, T.Septiembre, T.Octubre, T.Noviembre, T.Diciembre, T.Total     
	Into Rpt_DispensacionVentasPorClaveMensual 	   
	From #tmpVentasClaves_Claves_Cruze T	 
	Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
	-- Inner Join vw_Farmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )
	-- Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	-- Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
	Order By T.IdClaveSSA, T.A�o 


	Set @sSql = '' 
	If @EsConcentrado = 1 
		Begin 
		   Select 
				'N�m. Juris' = IdJurisdiccion, Jurisdiccion, -- 'Unidad' = IdFarmacia, Farmacia, 
				ClaveSSA, 'Descripci�n Clave' = DescripcionClave, 'Tipo de dispensaci�n' = DescTipoDispensacion, 
				'Precio licitaci�n' = PrecioLicitacion, 
				A�o, Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre, Total 
		   From Rpt_DispensacionVentasPorClaveMensual (NoLock) 
		   Order By DescripcionClave, IdTipoDeDispensacion 
		End    
	Else 
		Begin 
		   Select 
				'N�m. Juris' = IdJurisdiccion, Jurisdiccion, 'Unidad' = IdFarmacia, Farmacia, 
				ClaveSSA, 'Descripci�n Clave' = DescripcionClave, 'Tipo de dispensaci�n' = DescTipoDispensacion, 
				'Precio licitaci�n' = PrecioLicitacion, 
				A�o, Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre, Total 
		   From Rpt_DispensacionVentasPorClaveMensual (NoLock) 
		   Order By DescripcionClave, IdTipoDeDispensacion 	
		End 	


----	   Select 
----			'N�m. Juris' = IdJurisdiccion, Jurisdiccion, 'Unidad' = IdFarmacia, Farmacia, 
----			ClaveSSA, 'Descripci�n Clave' = DescripcionClave, 'Tipo de dispensaci�n' = DescTipoDispensacion, 
----			'Precio licitaci�n' = PrecioLicitacion, 
----			A�o, Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre, Total 
----	   From Rpt_DispensacionVentasPorClaveMensual (NoLock) 
----	   Order By DescripcionClave, IdTipoDeDispensacion 	

	   
	-- Exec(@sSql)


	-- Select * From #tmpVentasClaves_Claves_Cruze
	
----	Select 
----		 IdEmpresa, IdEstado, IdFarmacia,		
----		 IdClaveSSA, ClaveSSA, DescripcionClave, 
----		 -- EsDeConsignacion, 
----		 PrecioLicitacion, A�o, 
----		 Enero, Febrero, Marzo, Abril, Mayo, Junio, Julio, Agosto, Septiembre, Octubre, Noviembre, Diciembre, Total		 
----	From Rpt_DispensacionVentasPorClaveMensual 		
	
--	spp_Rpt_CteReg_EdoJuris_VentasPorClaveMensual 


---	select count(*) as Registros   	from Rpt_DispensacionVentasPorClaveMensual 


End	
Go--#SQL 
	