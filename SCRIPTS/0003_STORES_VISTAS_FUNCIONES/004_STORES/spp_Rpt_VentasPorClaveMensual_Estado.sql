If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_VentasPorClaveMensual_Estado' and xType = 'P' ) 
   Drop Proc spp_Rpt_VentasPorClaveMensual_Estado
Go--#SQL 

---- Exec   spp_Rpt_VentasPorClaveMensual_Estado  '20', '0038', '', '2011-07-09', '2011-07-10', '0'  

---	Exec  spp_Rpt_VentasPorClaveMensual_Estado '21', '*', '*', '*', '2012-10-01', '2012-10-01', '0', '0'  


---		Exec  spp_Rpt_VentasPorClaveMensual_Estado '09', '*', '*', '*', '2014-08-01', '2014-08-01', 0, 0, '', 0, 0, 0, '*', '*', 1, 0      

Create Proc spp_Rpt_VentasPorClaveMensual_Estado 
( 
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '*', @IdFarmacia varchar(4) = '1101', 
	@IdClaveSSA varchar(4) = '*',		
	@FechaInicial varchar(10) = '2012-09-01', @FechaFinal varchar(10) = '2012-12-31', 
	@TipoDispensacion smallint = 0,	@TipoInsumo tinyint = 0, @SubFarmacias varchar(200) = '',
	@AgrupaDispensacion smallint  = 0, @Filtro tinyint = 0, @TipoMedicamento tinyint = 0, @IdMunicipio varchar(4) = '*',
	@IdTipoUnidad varchar(3) = '*', @Concentrado int = 0, @ProcesoPorDia tinyint = 0  

	--	@Filtro = 0 son todas las claves   @Filtro = 1 son las claves por tipo medicamento. ya sea antibiotico o controlado.
	--	@TipoMedicamento = 0  son los medicamentos antibioticos  1 = los medicamentos controlados.
	--	@IdMunicipio = filtrado por municipio.
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
	@DescTipoDispensacion varchar(50),
	@iTipoMedicamento tinyint 

Declare 
	@sFechaAñoMesInicial varchar(10), 
	@sFechaAñoMesFinal varchar(10), 
	@iMesesAnalisis int 

	Set @iMesesAnalisis = 0  
	Set @sFechaAñoMesInicial = cast(datepart(yy, @FechaInicial) as varchar) + right('00' + cast(datepart(mm, @FechaInicial) as varchar), 2) 
	Set @sFechaAñoMesFinal = cast(datepart(yy, @FechaFinal) as varchar) + right('00' + cast(datepart(mm, @FechaFinal) as varchar), 2) 
	Set @iMesesAnalisis = datediff(month, @FechaInicial, @FechaFinal) + 1 

	If @ProcesoPorDia = 1 
	Begin 
		Set @sFechaAñoMesInicial = cast(datepart(yy, @FechaInicial) as varchar) + right('00' + cast(datepart(mm, @FechaInicial) as varchar), 2) + right('00' + cast(datepart(dd, @FechaInicial) as varchar), 2) 
		Set @sFechaAñoMesFinal = cast(datepart(yy, @FechaFinal) as varchar) + right('00' + cast(datepart(mm, @FechaFinal) as varchar), 2) + right('00' + cast(datepart(dd, @FechaFinal) as varchar), 2)
	End 


--- Asignar valores iniciales 
	--If @IdClaveSSA = '' 
	  -- Set @IdClaveSSA = '*' 
	Set @iTipoMedicamento = 1
	
	
	Set @iVenta = 0 
	Set @iConsignacion = 1
	Set @sWhereSubFarmacias = ''
	Set @DescTipoDispensacion = ''  

	Select * 
	into #vw_Productos_CodigoEAN
	From vw_Productos_CodigoEAN (NoLock) 

	-------------------------------------------------
	-- Se Obtiene la lista de Farmacias a Procesar --
	-------------------------------------------------

	Select IdTipoUnidad
	Into #tmpTipoUnidades
	From CatTiposUnidades (Nolock)

	If @IdTipoUnidad <> '*'
		Begin
			Delete From #tmpTipoUnidades Where IdTipoUnidad <> @IdTipoUnidad
		End
	

	Select IdEstado, @IdEstado as Estado, IdJurisdiccion, Descripcion as Jurisdiccion 
	Into #tmpJuris 
	From CatJurisdicciones 
	Where IdEstado = @IdEstado and IdJurisdiccion = @IdJurisdiccion 

	if @IdJurisdiccion = '*' 
		Begin 
			Insert Into #tmpJuris 
			Select IdEstado, @IdEstado as Estado, IdJurisdiccion as IdJuris, Descripcion as Jurisdiccion 
			From CatJurisdicciones 
			Where IdEstado = @IdEstado 
		End 

	--	Se obtiene la lista de los municipios

	Select Distinct F.IdEstado, F.IdMunicipio 
	Into #tmpMunicipios 
	From CatFarmacias F (Nolock)
	Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion ) 
	Where F.IdEstado = @IdEstado
	Order By IdMunicipio

	If @IdMunicipio <> '*'
		Begin
			Delete From #tmpMunicipios Where IdEstado = @IdEstado And IdMunicipio <> @IdMunicipio
		End

	-- Se obtiene la Lista de Farmacias de las Jurisdicciones.	
	Select F.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia 
	Into #tmpFarmacias 
	From vw_Farmacias F (NoLock) 
	Inner Join #tmpMunicipios M (NoLock) On ( F.IdEstado = M.IdEstado and F.IdMunicipio = M.IdMunicipio)
	Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion )	 
	Where F.Status = 'A' and F.IdTipoUnidad Not In ('000', '005', '006') -- Excluir almacenes
	and F.IdTipoUnidad In ( Select IdTipoUnidad From #tmpTipoUnidades  )

	If @IdFarmacia <> '*' 
	  Begin
		Delete From #tmpFarmacias Where IdEstado = @IdEstado And IdFarmacia <> @IdFarmacia
	  End

------------------ Se obtiene la Lista de Claves 
	Select Top 0 IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, Status, Actualizado  
	Into #CTE_ClavesAProcesar 
	From CTE_ClavesAProcesar 

	Insert Into #CTE_ClavesAProcesar ( IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, Status, Actualizado ) 
	Select E.IdEstado, E.IdFarmacia, E.IdClaveSSA_Sal, E.ClaveSSA, 'A', 0 
	From SVR_INV_Generar_Existencia_Concentrado E (NoLock) 
	Inner Join #tmpFarmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
------------------ Se obtiene la Lista de Claves 


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

	Select ClaveSSA
	Into #tmpClaves_A_Procesar
	From vw_ClavesSSA_Sales (Nolock) Where 1 = 0

	If @Filtro = 0
		Begin
			Insert Into #tmpClaves_A_Procesar
			Select Distinct ClaveSSA			 
			From vw_ClavesSSA_Sales (Nolock)
		End
	Else
		Begin
			If @TipoMedicamento = 0
				Begin
					Insert Into #tmpClaves_A_Procesar
					Select Distinct ClaveSSA			 
					From vw_ClavesSSA_Sales (Nolock) Where EsAntibiotico = @iTipoMedicamento
				End
			Else
				Begin  
					Insert Into #tmpClaves_A_Procesar
					Select Distinct ClaveSSA			 
					From vw_ClavesSSA_Sales (Nolock) Where EsControlado = @iTipoMedicamento
				End
		End

--------------------------------------- Obtener los Datos Principales 
	Select 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes, 
		IdClaveSSA, ClaveSSA, DescripcionClave, TasaIva, 0 as PrecioLicitacion, EsDeConsignacion, 
		IdTipoDeDispensacion, DescTipoDispensacion, sum(CantidadVendida) as CantidadVendida 
	Into #tmpVentasClaves 
	From Rpt_Concentrado__DispensacionVentasPorClaveMensual E (NoLock) 
	Where 1 = 0 
	Group By 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes, 
		IdClaveSSA, ClaveSSA, DescripcionClave, TasaIva, PrecioLicitacion, EsDeConsignacion, 
		IdTipoDeDispensacion, DescTipoDispensacion 

	If @ProcesoPorDia = 0 
		Begin 
			Insert Into #tmpVentasClaves 
			Select 
				IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes, 
				IdClaveSSA, ClaveSSA, DescripcionClave, TasaIva, 0 as PrecioLicitacion, EsDeConsignacion, 
				IdTipoDeDispensacion, DescTipoDispensacion, sum(CantidadVendida) as CantidadVendida  
			From Rpt_Concentrado__DispensacionVentasPorClaveMensual E (NoLock) 
			Where  
				Exists ( Select * From #tmpFarmacias LF Where E.IdEstado = LF.IdEstado And E.IdFarmacia = LF.IdFarmacia )
				And cast(Año as varchar) + right('00' + cast(Mes as varchar), 2) Between @sFechaAñoMesInicial and @sFechaAñoMesFinal 
				And EsDeConsignacion in ( @iConsignacion, @iVenta )
				And Exists ( Select * From #tmpClaves_A_Procesar C (Nolock) Where E.ClaveSSA = C.ClaveSSA ) 
			Group By 
				IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes, 
				IdClaveSSA, ClaveSSA, DescripcionClave, TasaIva, PrecioLicitacion, EsDeConsignacion, 
				IdTipoDeDispensacion, DescTipoDispensacion 
		End 
	Else 
		Begin 
			Insert Into #tmpVentasClaves 
			Select 
				IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes, 
				IdClaveSSA, ClaveSSA, DescripcionClave, TasaIva, 0 as PrecioLicitacion, EsDeConsignacion, 
				IdTipoDeDispensacion, DescTipoDispensacion, sum(CantidadVendida) as CantidadVendida  
			From Rpt_Concentrado__DispensacionVentasPorClaveMensual E (NoLock) 
			Where  
				Exists ( Select * From #tmpFarmacias LF Where E.IdEstado = LF.IdEstado And E.IdFarmacia = LF.IdFarmacia )
				And cast(Año as varchar) + right('00' + cast(Mes as varchar), 2) + right('00' + cast(Dia as varchar), 2) 
				Between @sFechaAñoMesInicial and @sFechaAñoMesFinal 
				And EsDeConsignacion in ( @iConsignacion, @iVenta )
				And Exists ( Select * From #tmpClaves_A_Procesar C (Nolock) Where E.ClaveSSA = C.ClaveSSA ) 
			Group By 
				IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes, 
				IdClaveSSA, ClaveSSA, DescripcionClave, TasaIva, PrecioLicitacion, EsDeConsignacion, 
				IdTipoDeDispensacion, DescTipoDispensacion 
		End 	
--------------------------------------- Obtener los Datos Principales 


	-- Se Borran los tipos de dispensacion que se dieron por Receta Generada por vales
	Delete From #tmpVentasClaves Where IdTipoDeDispensacion = '07'  --- Receta Generada por vales 
	
	If @AgrupaDispensacion = 1
		Begin
			Update #tmpVentasClaves Set IdTipoDeDispensacion = '00', DescTipoDispensacion = 'Concentrado'
		End
	
	If @TipoInsumo <> 0 
		   Begin 
			  If @TipoInsumo = 1 
				 Delete From #tmpVentasClaves Where TasaIva <> 0  --- Medicamentos 

			  If @TipoInsumo = 2
				 Delete From #tmpVentasClaves Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
		   End

	
--          spp_Rpt_VentasPorClaveMensual_Estado 	


--- 
	Select 	   	
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, 
		--'' as IdFarmacia, '' as Farmacia, 
		IdFarmacia, Farmacia, 
		Año, Mes,  
		IdClaveSSA, ClaveSSA, DescripcionClave,
		IdTipoDeDispensacion, DescTipoDispensacion, 
		TasaIva, PrecioLicitacion, 
		--EsDeConsignacion, 
		sum(CantidadVendida) as CantidadVendida				
	Into  #tmpVentasClaves_Claves	
	From #tmpVentasClaves 	   		   	
	Group by 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes, 
		IdClaveSSA, ClaveSSA, DescripcionClave, IdTipoDeDispensacion, DescTipoDispensacion, TasaIva, PrecioLicitacion, EsDeConsignacion    
		   		   	
---    spp_Rpt_VentasPorClaveMensual_Estado


------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, 
		--'' as IdFarmacia, '' as Farmacia, 
		IdFarmacia, Farmacia, 
		IdClaveSSA, ClaveSSA, DescripcionClave, 
		-- EsDeConsignacion,
		IdTipoDeDispensacion, DescTipoDispensacion, 
		TasaIva, PrecioLicitacion, Año, 
		0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total   
	into #tmpVentasClaves_Claves_Cruze	 
	From #tmpVentasClaves_Claves 

--- Agregar cada Clave Localizada 
	Insert Into #tmpVentasClaves_Claves_Cruze 
	Select Distinct 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, 
		--'' as IdFarmacia, '' as Farmacia, 
		IdFarmacia, Farmacia, 
		IdClaveSSA, ClaveSSA, DescripcionClave,
		IdTipoDeDispensacion, DescTipoDispensacion,
		--0 As EsDeConsignacion, 
		TasaIva, PrecioLicitacion, Año, 
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 
		0 as Total   	
	From #tmpVentasClaves_Claves 
	Order by  Año 
		
---------------------------- Asignar Jurisdiccion 
	Update C Set IdJurisdiccion = F.IdJurisdiccion, Jurisdiccion = F.Jurisdiccion 
	From #tmpVentasClaves_Claves_Cruze C 
	Inner Join #tmpFarmacias F On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
---------------------------- Asignar Jurisdiccion 






--	select * from #tmpVentasClaves_Claves_Cruze 

----------------- Asignar los totales por Mes 
	Update T Set Enero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iEnero and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Febrero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iFebrero and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iMarzo and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iAbril and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Mayo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iMayo and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Junio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iJunio and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iJulio and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iAgosto and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iSeptiembre and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Octubre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iOctubre and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iNoviembre and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.Mes = @iDiciembre and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Total = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.IdClaveSSA = T.IdClaveSSA
			      and X.Año = T.Año and X.IdTipoDeDispensacion = T.IdTipoDeDispensacion ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	
----	Update T Set Venta = IsNull(( select sum(CantidadVendida_Venta) 
----			From #tmpVentasClaves_Claves X 
----			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
----			      and X.Año = T.Año  ), 0 )  
----	From #tmpVentasClaves_Claves_Cruze T 	 
----	-- Where EsDeConsignacion = 0 
----	
----	Update T Set Consignacion = IsNull(( select sum(CantidadVendida_Consignacion) 
----			From #tmpVentasClaves_Claves X 
----			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
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


/*
	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'Rpt_DispensacionVentasPorClaveMensual' and xType = 'U' ) 
	   Drop Table Rpt_DispensacionVentasPorClaveMensual 
*/ 

	Select 
		 T.IdEstado, E.Nombre as Estado, 
		 -- T.IdJurisdiccion, T.Jurisdiccion, 
		 'Clave SSA' = T.ClaveSSA, 'Descripción Clave' = T.DescripcionClave,
		 --'Tipo de dispensación' = T.DescTipoDispensacion, 
		 'Tipo de insumo' = (Case When T.TasaIva = 0 Then 'MEDICAMENTO' Else 'MATERIAL DE CURACIÓN' End), 
		 T.Año, 
		sum(T.Enero) as Enero, sum(T.Febrero) as Febrero, sum(T.Marzo) as Marzo, sum(T.Abril) as Abril, 
		sum(T.Mayo) as Mayo, sum(T.Junio) as Junio, sum(T.Julio) as Julio, sum(T.Agosto) as Agosto, 
		sum(T.Septiembre) as Septiembre, sum(T.Octubre) as Octubre, sum(T.Noviembre) as Noviembre, sum(T.Diciembre) as Diciembre, sum(T.Total) as Total 
	-- Into Rpt_DispensacionVentasPorClaveMensual 	   
	From #tmpVentasClaves_Claves_Cruze T	 
	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	Group by  
		T.IdEstado, E.Nombre, 
		--T.IdJurisdiccion, T.Jurisdiccion, 
		T.Año, T.ClaveSSA, T.DescripcionClave, 
		(Case When T.TasaIva = 0 Then 'MEDICAMENTO' Else 'MATERIAL DE CURACIÓN' End) 
			

	----If @Concentrado = 1 
	----	Begin 
	----		Select 
	----			 T.IdJurisdiccion, T.Jurisdiccion, 
	----			 T.IdFarmacia, F.NombreFarmacia as Farmacia,		 
	----			 'Clave SSA' = T.ClaveSSA, 'Descripción Clave' = T.DescripcionClave, 
	----			 'Tipo de insumo' = (Case When T.TasaIva = 0 Then 'MEDICAMENTO' Else 'MATERIAL DE CURACIÓN' End), 
	----			 'Meses analizados' = @iMesesAnalisis, 
	----			 cast(sum(T.Total) as int) as Total      
	----		-- Into Rpt_DispensacionVentasPorClaveMensual 	   
	----		From #tmpVentasClaves_Claves_Cruze T	 
	----		Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
	----		Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	----		Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
	----		Group by T.IdJurisdiccion, T.Jurisdiccion, T.IdFarmacia, F.NombreFarmacia, T.ClaveSSA, T.DescripcionClave, T.TasaIva 
	----		Order By T.IdJurisdiccion, T.IdFarmacia, T.ClaveSSA  
	----	End 
	----Else 
	----	Begin 
	----		Select 
	----			 -- @EncPrincipal As EncabezadoPrincipal, @EncSecundario As EncabezadoSecundario,
	----			 -- T.IdEmpresa, Ex.Nombre as Empresa, T.IdEstado, E.Nombre as Estado, 
	----			 T.IdJurisdiccion, T.Jurisdiccion, 
	----			 T.IdFarmacia, F.NombreFarmacia as Farmacia,		 
	----			 -- T.IdClaveSSA, 
	----			 'Clave SSA' = T.ClaveSSA, 'Descripción Clave' = T.DescripcionClave,
	----			 -- T.IdTipoDeDispensacion, 
	----			 'Tipo de dispensación' = T.DescTipoDispensacion, 
	----			 -- EsDeConsignacion, 
	----			 'Tipo de insumo' = (Case When T.TasaIva = 0 Then 'MEDICAMENTO' Else 'MATERIAL DE CURACIÓN' End), 
	----			 T.PrecioLicitacion, T.Año, 
	----			 T.Enero, T.Febrero, T.Marzo, T.Abril, T.Mayo, T.Junio, T.Julio, T.Agosto, T.Septiembre, T.Octubre, T.Noviembre, T.Diciembre, T.Total     
	----		-- Into Rpt_DispensacionVentasPorClaveMensual 	   
	----		From #tmpVentasClaves_Claves_Cruze T	 
	----		Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
	----		Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	----		Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
	----		Order By T.IdClaveSSA, T.Año 
	----	End 

	-- Select * From #tmpVentasClaves_Claves_Cruze

	
--	spp_Rpt_VentasPorClaveMensual_Estado 

End	
Go--#SQL 
	