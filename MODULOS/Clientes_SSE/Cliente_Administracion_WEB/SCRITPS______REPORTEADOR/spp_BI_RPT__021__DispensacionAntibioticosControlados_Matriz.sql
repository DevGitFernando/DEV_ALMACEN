------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__021__DispensacionAntibioticosControlados_Matriz' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__021__DispensacionAntibioticosControlados_Matriz 
Go--#SQL 

---- Exec   spp_BI_RPT__021__DispensacionAntibioticosControlados_Matriz  '20', '0038', '', '2011-07-09', '2011-07-10', '0'  

---	Exec  spp_BI_RPT__021__DispensacionAntibioticosControlados_Matriz '21', '*', '*', '*', '2012-10-01', '2012-10-01', '0', '0'  


---		Exec  spp_BI_RPT__021__DispensacionAntibioticosControlados_Matriz '09', '*', '*', '*', '2014-08-01', '2014-08-01', 0, 0, '', 0, 0, 0, '*', '*', 1, 0      

/* 

Exec spp_BI_RPT__021__DispensacionAntibioticosControlados_Matriz 
	@IdEmpresa = '001', 
	@IdEstado = '21', @IdJurisdiccion = '*', @IdMunicipio = '*', 
	@IdFarmacia = '3188', 
	@IdClaveSSA = '*', @FechaInicial = '2018-01-01', @FechaFinal = '2018-02-15', 
	@TipoMedicamento = 0, 
	@TipoDispensacion = 0,	@TipoInsumo = 1, @SubFarmacias = '', 
	@AgrupaDispensacion = 1, @Filtro = 1,  
	@IdTipoUnidad = '*', @ProcesoPorDia = 0  

*/ 


Create Proc spp_BI_RPT__021__DispensacionAntibioticosControlados_Matriz  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '*', @IdMunicipio varchar(4) = '*', 
	@IdFarmacia varchar(4) = '3188', 
	@IdClaveSSA varchar(6) = '*', @FechaInicial varchar(10) = '2018-01-01', @FechaFinal varchar(10) = '2018-01-31', 
	@TipoMedicamento tinyint = 0, 
	@TipoDispensacion smallint = 0,	@TipoInsumo tinyint = 0, @SubFarmacias varchar(200) = '', 
	@AgrupaDispensacion smallint = 1, @Filtro tinyint = 1,  
	@IdTipoUnidad varchar(3) = '*', @ProcesoPorDia tinyint = 0  

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
	@TipoDeDispensacion varchar(50),
	@iTipoMedicamento tinyint 

Declare 
	@sFechaAñoMesInicial varchar(10), 
	@sFechaAñoMesFinal varchar(10), 
	@dateFechaAñoMesInicial datetime, 
	@dateFechaAñoMesFinal datetime, 
	@iMesesAnalisis int 

	Set @iMesesAnalisis = 0  
	----Set @sFechaAñoMesInicial = cast(datepart(yy, @FechaInicial) as varchar) + right('00' + cast(datepart(mm, @FechaInicial) as varchar), 2) 
	----Set @sFechaAñoMesFinal = cast(datepart(yy, @FechaFinal) as varchar) + right('00' + cast(datepart(mm, @FechaFinal) as varchar), 2) 
	----Set @iMesesAnalisis = datediff(month, @FechaInicial, @FechaFinal) + 1 


	------------ TOMAR LAS FECHAS PURAS 
	Set @sFechaAñoMesInicial = @FechaInicial   
	Set @sFechaAñoMesFinal = @FechaFinal  
	Set @dateFechaAñoMesInicial = cast(@sFechaAñoMesInicial + ' 00:00:00:000' as datetime) 
	Set @dateFechaAñoMesFinal = cast(@sFechaAñoMesFinal + ' 00:00:00:000' as datetime) 

	--------If @ProcesoPorDia = 1 
	----Begin 
	----	Set @sFechaAñoMesInicial = cast(datepart(yy, @FechaInicial) as varchar) + right('00' + cast(datepart(mm, @FechaInicial) as varchar), 2) + right('00' + cast(datepart(dd, @FechaInicial) as varchar), 2) 
	----	Set @sFechaAñoMesFinal = cast(datepart(yy, @FechaFinal) as varchar) + right('00' + cast(datepart(mm, @FechaFinal) as varchar), 2) + right('00' + cast(datepart(dd, @FechaFinal) as varchar), 2)
	----End 



--- Asignar valores iniciales 
	--If @IdClaveSSA = '' 
	  -- Set @IdClaveSSA = '*' 
	Set @iTipoMedicamento = 1
	
	
	Set @iVenta = 0 
	Set @iConsignacion = 1
	Set @sWhereSubFarmacias = ''
	Set @TipoDeDispensacion = ''  

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
	From vw_Farmacias__PRCS F (NoLock) 
	Inner Join #tmpMunicipios M (NoLock) On ( F.IdEstado = M.IdEstado and F.IdMunicipio = M.IdMunicipio)
	Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion )	 
	Where F.Status = 'A' and F.IdTipoUnidad Not In ('000', '005', '006') -- Excluir almacenes
	and F.IdTipoUnidad In ( Select IdTipoUnidad From #tmpTipoUnidades  )

	If @IdFarmacia <> '*' 
	  Begin
		Delete From #tmpFarmacias Where IdEstado = @IdEstado And IdFarmacia <> @IdFarmacia
	  End

---------------------- Se obtiene la Lista de Claves 
----	Select Top 0 IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, Status, Actualizado  
----	Into #CTE_ClavesAProcesar 
----	From CTE_ClavesAProcesar 

----	Insert Into #CTE_ClavesAProcesar ( IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, Status, Actualizado ) 
----	Select E.IdEstado, E.IdFarmacia, E.IdClaveSSA_Sal, E.ClaveSSA, 'A', 0 
----	From SVR_INV_Generar_Existencia_Concentrado E (NoLock) 
----	Inner Join #tmpFarmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
---------------------- Se obtiene la Lista de Claves 


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
		IdEmpresa, IdEstado, cast('' as varchar(10)) as IdJurisdiccion, cast('' as varchar(200))  as Jurisdiccion, 
		IdFarmacia, Farmacia, 0 as Año, 0 as Mes, 
		IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, 
		cast(DescripcionSal as varchar(5000)) as DescripcionClave, 
		TasaIva, 0 as PrecioLicitacion, EsConsignacion, 
		IdTipoDeDispensacion, '' as TipoDeDispensacion, 
		sum(Cantidad) as CantidadVendida 
	Into #tmpVentasClaves 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	Where 1 = 0 
	Group By 
		IdEmpresa, IdEstado, --IdJurisdiccion, Jurisdiccion, 
		IdFarmacia, Farmacia, -- Año, Mes, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, TasaIva, PrecioLicitacion, EsConsignacion, 
		IdTipoDeDispensacion --, TipoDeDispensacion 

	----If @ProcesoPorDia = 0 
	----	Begin 
	----		Insert Into #tmpVentasClaves 
	----		Select 
	----			IdEmpresa, IdEstado, '' as IdJurisdiccion, '' as Jurisdiccion, IdFarmacia, Farmacia, 
	----			year(FechaRegistro) as Año, month(FechaRegistro) as Mes, 
	----			IdClaveSSA_Sal, ClaveSSA, DescripcionSal as DescripcionClave, TasaIva, 0 as PrecioLicitacion, EsConsignacion, 
	----			IdTipoDeDispensacion, TipoDeDispensacion, sum(Cantidad) as CantidadVendida  
	----		From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	----		Where  
	----			Exists ( Select * From #tmpFarmacias LF Where E.IdEstado = LF.IdEstado And E.IdFarmacia = LF.IdFarmacia )
	----			And cast(year(FechaRegistro) as varchar) + right('00' + cast(month(FechaRegistro) as varchar), 2) 
	----				Between @sFechaAñoMesInicial and @sFechaAñoMesFinal 
	----			And EsConsignacion in ( @iConsignacion, @iVenta )
	----			And Exists ( Select * From #tmpClaves_A_Procesar C (Nolock) Where E.ClaveSSA = C.ClaveSSA ) 
	----		Group By 
	----			IdEmpresa, IdEstado, -- IdJurisdiccion, Jurisdiccion, 
	----			IdFarmacia, Farmacia, 
	----			--Año, Mes, 
	----			year(FechaRegistro), month(FechaRegistro), 
	----			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, TasaIva, PrecioLicitacion, EsConsignacion, 
	----			IdTipoDeDispensacion, TipoDeDispensacion 
	----	End 
	----Else 
		Begin 
			Insert Into #tmpVentasClaves 
			Select 
				IdEmpresa, IdEstado, '' as IdJurisdiccion, '' as Jurisdiccion, IdFarmacia, Farmacia, 
				year(FechaRegistro) as Año, month(FechaRegistro) as Mes, 
				IdClaveSSA_Sal, ClaveSSA, DescripcionSal as DescripcionClave, TasaIva, 0 as PrecioLicitacion, EsConsignacion, 
				IdTipoDeDispensacion, TipoDeDispensacion, sum(Cantidad) as CantidadVendida  
			From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
			Where  
				Exists ( Select * From #tmpFarmacias LF Where E.IdEstado = LF.IdEstado And E.IdFarmacia = LF.IdFarmacia )
				-- And cast(year(FechaRegistro) as varchar) + right('00' + cast(month(FechaRegistro) as varchar), 2) -- + right('00' + cast(Dia as varchar), 2) 
				-- and Convert(varchar(10), E.FechaRegistro, 120) Between @sFechaAñoMesInicial and @sFechaAñoMesFinal 
				and E.FechaRegistro Between @dateFechaAñoMesInicial and @dateFechaAñoMesFinal   
				And EsConsignacion in ( @iConsignacion, @iVenta )
				And Exists ( Select * From #tmpClaves_A_Procesar C (Nolock) Where E.ClaveSSA = C.ClaveSSA ) 
			Group By 
				IdEmpresa, IdEstado, -- IdJurisdiccion, Jurisdiccion, 
				IdFarmacia, Farmacia, 
				--Año, Mes, 
				year(FechaRegistro), month(FechaRegistro), 
				IdClaveSSA_Sal, ClaveSSA, DescripcionSal, TasaIva, PrecioLicitacion, EsConsignacion, 
				IdTipoDeDispensacion, TipoDeDispensacion 
		End 	

	----Set @dateFechaAñoMesInicial = cast(@sFechaAñoMesInicial as datetime) 
	----Set @dateFechaAñoMesFinal = cast(@sFechaAñoMesFinal as datetime) 
--------------------------------------- Obtener los Datos Principales 


	-- Se Borran los tipos de dispensacion que se dieron por Receta Generada por vales
	Delete From #tmpVentasClaves Where IdTipoDeDispensacion = '07'  --- Receta Generada por vales 
	
	If @AgrupaDispensacion = 1
		Begin
			Update #tmpVentasClaves Set IdTipoDeDispensacion = '00', TipoDeDispensacion = 'Concentrado'
		End
	
	If @TipoInsumo <> 0 
		   Begin 
			  If @TipoInsumo = 1 
				 Delete From #tmpVentasClaves Where TasaIva <> 0  --- Medicamentos 

			  If @TipoInsumo = 2
				 Delete From #tmpVentasClaves Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
		   End

	
--          spp_BI_RPT__021__DispensacionAntibioticosControlados_Matriz 	


--- 
	Select 	   	
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
		Año, Mes,  
		IdClaveSSA, ClaveSSA, DescripcionClave,
		IdTipoDeDispensacion, TipoDeDispensacion, 
		TasaIva, PrecioLicitacion, 
		--EsConsignacion, 
		sum(CantidadVendida) as CantidadVendida				
	Into  #tmpVentasClaves_Claves	
	From #tmpVentasClaves 	   		   	
	Group by 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes, 
		IdClaveSSA, ClaveSSA, DescripcionClave, IdTipoDeDispensacion, TipoDeDispensacion, TasaIva, PrecioLicitacion, EsConsignacion    
		   		   	
---    spp_BI_RPT__021__DispensacionAntibioticosControlados_Matriz


------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		 IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
		 IdClaveSSA, ClaveSSA, DescripcionClave, 
		 -- EsConsignacion,
		 IdTipoDeDispensacion, TipoDeDispensacion, 
		 TasaIva, PrecioLicitacion, Año, 
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total   
	into #tmpVentasClaves_Claves_Cruze	 
	From #tmpVentasClaves_Claves 

--- Agregar cada Clave Localizada 
    Insert Into #tmpVentasClaves_Claves_Cruze 
	Select Distinct 
		 IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 		 
		 IdClaveSSA, ClaveSSA, DescripcionClave,
		 IdTipoDeDispensacion, TipoDeDispensacion,
		 --0 As EsConsignacion, 
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
----	-- Where EsConsignacion = 0 
----	
----	Update T Set Consignacion = IsNull(( select sum(CantidadVendida_Consignacion) 
----			From #tmpVentasClaves_Claves X 
----			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdClaveSSA = T.IdClaveSSA
----			      and X.Año = T.Año  ), 0 )  
----	From #tmpVentasClaves_Claves_Cruze T 	 
----	-- Where EsConsignacion = 1 	

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
		Select 
				-- @EncPrincipal As EncabezadoPrincipal, @EncSecundario As EncabezadoSecundario,
				-- T.IdEmpresa, Ex.Nombre as Empresa, T.IdEstado, E.Nombre as Estado, 
				-- T.IdJurisdiccion, T.Jurisdiccion, 
				T.IdFarmacia, F.NombreFarmacia as Farmacia,		 
				-- T.IdClaveSSA, 
				'Clave SSA' = T.ClaveSSA, 'Descripción Clave' = T.DescripcionClave,
				-- T.IdTipoDeDispensacion, 
				'Tipo de dispensación' = T.TipoDeDispensacion, 
				-- EsConsignacion, 
				'Tipo de insumo' = (Case When T.TasaIva = 0 Then 'MEDICAMENTO' Else 'MATERIAL DE CURACIÓN' End), 
				T.PrecioLicitacion, T.Año, 
				T.Enero, T.Febrero, T.Marzo, T.Abril, T.Mayo, T.Junio, T.Julio, T.Agosto, T.Septiembre, T.Octubre, T.Noviembre, T.Diciembre, T.Total     
		-- Into Rpt_DispensacionVentasPorClaveMensual 	   
		From #tmpVentasClaves_Claves_Cruze T	 
		Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
		Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
		Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
		Order By T.IdClaveSSA, T.Año 
		----End 


	------ DUMMY PARA EL BIRT 
	/* 
	Select  
		cast('' as varchar(20)) as IdFarmacia, cast('' as varchar(200)) as Farmacia,		 
		'Clave SSA' = cast('' as varchar(20)), 'Descripción Clave' = cast('' as varchar(2000)),
		'Tipo de dispensación' = cast('' as varchar(100)), 
		'Tipo de insumo' = cast('' as varchar(200)),  --- (Case When T.TasaIva = 0 Then 'MEDICAMENTO' Else 'MATERIAL DE CURACIÓN' End), 
		cast(0.0 as numeric(14,4)) as PrecioLicitacion, 0 as Año, 
		0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 
		0 as Mayo, 0 as Junio, 0 as Julio, 0 as Agosto, 
		0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 
		0 as Total     
	*/ 


	-- Select * From #tmpVentasClaves_Claves_Cruze

	
--	spp_BI_RPT__021__DispensacionAntibioticosControlados_Matriz 

End	
Go--#SQL 
	
