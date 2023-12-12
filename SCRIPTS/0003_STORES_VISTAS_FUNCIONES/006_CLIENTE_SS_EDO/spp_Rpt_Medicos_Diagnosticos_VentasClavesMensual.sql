If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Medicos_Diagnosticos_VentasClavesMensual' and xType = 'P' ) 
   Drop Proc spp_Rpt_Medicos_Diagnosticos_VentasClavesMensual
Go--#SQL 

---- Exec   spp_Rpt_Medicos_Diagnosticos_VentasClavesMensual  '20', '0038', '', '2011-07-09', '2011-07-10', '0'  

---	Exec  spp_Rpt_Medicos_Diagnosticos_VentasClavesMensual '21', '*', '*', '*', '2012-10-01', '2012-10-01', '0', '0'  

---			Exec  spp_Rpt_Medicos_Diagnosticos_VentasClavesMensual '09', '*', '*', '*', '*', '2014-08-01', '2014-08-01', 1, 0, 0, 0  

Create Proc spp_Rpt_Medicos_Diagnosticos_VentasClavesMensual 
( 
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '006', @IdFarmacia varchar(4) = '*',
	@IdMunicipio varchar(4) = '*', @IdTipoUnidad varchar(3) = '*',		
	@FechaInicial varchar(10) = '2013-03-01', @FechaFinal varchar(10) = '2013-05-01', 
	@Claves tinyint = 1, @Diagnosticos tinyint = 1, @Medicos tinyint = 1, 
	@ProcesoPorDia tinyint = 0 	
) 	
With Encryption 
As 
Begin 
Set NoCount On  
Set Dateformat YMD 
Declare 
	@iEnero int, @iFebrero int, @iMarzo int, @iAbril int, @iMayo int, @iJunio int, 
	@iJulio int, @iAgosto int, @iSeptiembre int, @iOctubre int, @iNoviembre int, @iDiciembre int 											

Declare	
	@EncPrincipal varchar(500), 
	@EncSecundario varchar(500)
	

Declare 
	@sFechaAñoMesInicial varchar(10), 
	@sFechaAñoMesFinal varchar(10)

----------- Preparar el filtro de informacion	
	If @ProcesoPorDia = 0 
		Begin 
			Set @sFechaAñoMesInicial = cast(datepart(yy, @FechaInicial) as varchar) + right('00' + cast(datepart(mm, @FechaInicial) as varchar), 2) 
			Set @sFechaAñoMesFinal = cast(datepart(yy, @FechaFinal) as varchar) + right('00' + cast(datepart(mm, @FechaFinal) as varchar), 2) 
		End 
	Else 
		Begin 
			Set @sFechaAñoMesInicial = cast(datepart(yy, @FechaInicial) as varchar) + right('00' + cast(datepart(mm, @FechaInicial) as varchar), 2) + right('00' + cast(datepart(dd, @FechaInicial) as varchar), 2) 
			Set @sFechaAñoMesFinal = cast(datepart(yy, @FechaFinal) as varchar) + right('00' + cast(datepart(mm, @FechaFinal) as varchar), 2) + right('00' + cast(datepart(dd, @FechaFinal) as varchar), 2) 
		End 	

----------------------------------	
	-- Select @sFechaAñoMesInicial, @sFechaAñoMesFinal  

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
	
-----------------------------------------------------------------------------------------------------------------------------------------

	Select @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Select @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
	
	Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario 
	from dbo.fg_Unidad_EncabezadoReportesClientesSSA()	


-------------------------- Obtener los Datos Principales 
	Select 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		IdMedico, Medico, IdDiagnostico, Diagnostico,
		Año, Mes, Dia, ClaveSSA, DescripcionClave, 0 as PrecioLicitacion, sum(CantidadVendida) as CantidadVendida 
	Into #tmpVentasClaves 
	From Rpt_Concentrado__Dispensacion_Mensual_Estadisticas E (NoLock) 
	Where 1 = 0 And 
		Exists ( Select * From #tmpFarmacias LF Where E.IdEstado = LF.IdEstado And E.IdFarmacia = LF.IdFarmacia )
		And cast(Año as varchar) + right('00' + cast(Mes as varchar), 2) 
		Between @sFechaAñoMesInicial and @sFechaAñoMesFinal			 
	Group By 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
		IdMedico, Medico, IdDiagnostico, Diagnostico, Año, Mes, Dia, 
		ClaveSSA, DescripcionClave, PrecioLicitacion
		
	If @ProcesoPorDia = 0 
		Begin 
			Insert Into #tmpVentasClaves 
			Select 
				IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
				IdMedico, Medico, IdDiagnostico, Diagnostico,
				Año, Mes, 0 as Dia, ClaveSSA, DescripcionClave, 0 as PrecioLicitacion, sum(CantidadVendida) as CantidadVendida 			 
			From Rpt_Concentrado__Dispensacion_Mensual_Estadisticas E (NoLock) 
			Where 
				Exists ( Select * From #tmpFarmacias LF Where E.IdEstado = LF.IdEstado And E.IdFarmacia = LF.IdFarmacia )
				And cast(Año as varchar) + right('00' + cast(Mes as varchar), 2) 
				Between @sFechaAñoMesInicial and @sFechaAñoMesFinal			 
			Group By 
				IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
				IdMedico, Medico, IdDiagnostico, Diagnostico, Año, Mes, 
				ClaveSSA, DescripcionClave, PrecioLicitacion 
		End 	
	Else 
		Begin 
			Insert Into #tmpVentasClaves 
			Select 
				IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
				IdMedico, Medico, IdDiagnostico, Diagnostico,
				Año, Mes, Dia, ClaveSSA, DescripcionClave, 0 as PrecioLicitacion, sum(CantidadVendida) as CantidadVendida  
			From Rpt_Concentrado__Dispensacion_Mensual_Estadisticas E (NoLock) 
			Where 
				Exists ( Select * From #tmpFarmacias LF Where E.IdEstado = LF.IdEstado And E.IdFarmacia = LF.IdFarmacia )
				And cast(Año as varchar) + right('00' + cast(Mes as varchar), 2) + right('00' + cast(Dia as varchar), 2) 
				Between @sFechaAñoMesInicial and @sFechaAñoMesFinal			 
			Group By 
				IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
				IdMedico, Medico, IdDiagnostico, Diagnostico, Año, Mes, Dia, 
				ClaveSSA, DescripcionClave, PrecioLicitacion 
		End 		
	
-------------------------------------------------  Tipo de reportes 	
	If @Claves = 0
		Begin
			Update #tmpVentasClaves Set ClaveSSA = '0', DescripcionClave = 'Todas las Claves'
		End

	If @Diagnosticos = 0
		Begin
			Update #tmpVentasClaves Set IdDiagnostico = '0000', Diagnostico = 'Todas los Diagnosticos'
		End

 
	If @Medicos = 0
		Begin
			Update #tmpVentasClaves Set IdMedico = '000000', Medico = 'Todos los Medicos'
		End
	
-------------------------------------------------  Tipo de reportes 	
	
--          spp_Rpt_Medicos_Diagnosticos_VentasClavesMensual 	


--- 
	Select 	   	
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
		IdMedico, Medico, IdDiagnostico, Diagnostico, Año, Mes, -- Dia, 
		ClaveSSA, DescripcionClave,
		PrecioLicitacion, 		 
		sum(CantidadVendida) as CantidadVendida				
	Into  #tmpVentasClaves_Claves	
	From #tmpVentasClaves 	   		   	
	Group by 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
		IdMedico, Medico, IdDiagnostico, Diagnostico, Año, Mes, -- Dia,  
		ClaveSSA, DescripcionClave, PrecioLicitacion    
		   		   	
---    spp_Rpt_Medicos_Diagnosticos_VentasClavesMensual


------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		 IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		 IdMedico, Medico, IdDiagnostico, Diagnostico, 
		 ClaveSSA, DescripcionClave, 
		 PrecioLicitacion, Año, 
		 0 as Enero, 0 as Febrero, 0 as Marzo, 0 as Abril, 0 as Mayo, 0 as Junio, 
		 0 as Julio, 0 as Agosto, 0 as Septiembre, 0 as Octubre, 0 as Noviembre, 0 as Diciembre, 0 as Total   
	into #tmpVentasClaves_Claves_Cruze	 
	From #tmpVentasClaves_Claves 

--- Agregar cada Clave Localizada 
    Insert Into #tmpVentasClaves_Claves_Cruze 
	Select Distinct 
		 IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		 IdMedico, Medico, IdDiagnostico, Diagnostico, 		 
		 ClaveSSA, DescripcionClave,
		 PrecioLicitacion, Año, 
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
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iEnero and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Febrero = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iFebrero and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iMarzo and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iAbril and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Mayo = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iMayo and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Junio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iJunio and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iJulio and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iAgosto and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iSeptiembre and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Octubre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iOctubre and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iNoviembre and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.Mes = @iDiciembre and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Total = IsNull(( select sum(CantidadVendida) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and X.ClaveSSA = T.ClaveSSA
			      and X.Año = T.Año and X.IdMedico = T.IdMedico and X.IdDiagnostico = T.IdDiagnostico ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	

----------------- Asignar los totales por Mes 

/* 
	Set @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Set @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
*/ 


	Select		
		 T.IdJurisdiccion, T.Jurisdiccion, 
		 T.IdFarmacia, F.NombreFarmacia as Farmacia,		 
		 T.IdMedico, T.Medico, T.IdDiagnostico, T.Diagnostico,
		 'Clave SSA' = T.ClaveSSA, 'Descripción Clave' = T.DescripcionClave,		  
		 T.PrecioLicitacion, T.Año, -- T.Dia, 
		 T.Enero, T.Febrero, T.Marzo, T.Abril, T.Mayo, T.Junio, T.Julio, T.Agosto, T.Septiembre, T.Octubre, T.Noviembre, T.Diciembre, T.Total     
	From #tmpVentasClaves_Claves_Cruze T	 
	Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
	Order By T.ClaveSSA, T.Año 

	-- Select * From #tmpVentasClaves_Claves_Cruze

	
--	spp_Rpt_Medicos_Diagnosticos_VentasClavesMensual 

End	
Go--#SQL 
	