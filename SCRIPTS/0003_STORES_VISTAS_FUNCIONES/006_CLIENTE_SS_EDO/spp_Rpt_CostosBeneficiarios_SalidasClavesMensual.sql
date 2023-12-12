If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_CostosBeneficiarios_SalidasClavesMensual' and xType = 'P' ) 
   Drop Proc spp_Rpt_CostosBeneficiarios_SalidasClavesMensual
Go--#SQL 


---	Exec  spp_Rpt_CostosBeneficiarios_SalidasClavesMensual '21', '001', '*', '*', '2012-10-01', '2012-10-01' 

Create Proc spp_Rpt_CostosBeneficiarios_SalidasClavesMensual 
( 
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '006', @IdFarmacia varchar(4) = '*',
	@IdMunicipio varchar(4) = '*', @IdTipoUnidad varchar(3) = '*',		
	@FechaInicial varchar(10) = '2013-04-01', @FechaFinal varchar(10) = '2013-05-01', 
	@TipoReporte int = 1 
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

	Set @sFechaAñoMesInicial = cast(datepart(yy, @FechaInicial) as varchar) + right('00' + cast(datepart(mm, @FechaInicial) as varchar), 2) 
	Set @sFechaAñoMesFinal = cast(datepart(yy, @FechaFinal) as varchar) + right('00' + cast(datepart(mm, @FechaFinal) as varchar), 2) 


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

--- Obtener los Datos Principales 
	Select 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		Año, Mes, IdPrograma, Programa, IdSubPrograma, SubPrograma, CostoTotalBeneficiarios, TotalBeneficiarios,
		CostoPromedioBeneficiarios
	Into #tmpVentasClaves 
	From Rpt_Concentrado__Dispensacion_Mensual_CostoBeneficiarios E (NoLock) 
	Where  
		Exists ( Select * From #tmpFarmacias LF Where E.IdEstado = LF.IdEstado And E.IdFarmacia = LF.IdFarmacia )
		And cast(Año as varchar) + right('00' + cast(Mes as varchar), 2) Between @sFechaAñoMesInicial and @sFechaAñoMesFinal			 
	
--          spp_Rpt_CostosBeneficiarios_SalidasClavesMensual 	


---------------------------------- RESUMEN 
	If @TipoReporte = 1 
	Begin 
		Update V Set IdPrograma = '', Programa = '', IdSubPrograma = '', SubPrograma = '' From #tmpVentasClaves V 

		Delete From #tmpVentasClaves 
		
		Insert Into #tmpVentasClaves	
		Select 
			IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
			Año, Mes, '' as IdPrograma, '' as Programa, '' as IdSubPrograma, '' as SubPrograma, 
			sum(CostoTotalBeneficiarios) as CostoTotalBeneficiarios, sum(TotalBeneficiarios) as TotalBeneficiarios,
			(sum(CostoTotalBeneficiarios) / sum(TotalBeneficiarios)) as CostoPromedioBeneficiarios 
		From Rpt_Concentrado__Dispensacion_Mensual_CostoBeneficiarios E (NoLock) 
		Where  
			Exists ( Select * From #tmpFarmacias LF Where E.IdEstado = LF.IdEstado And E.IdFarmacia = LF.IdFarmacia )
			And cast(Año as varchar) + right('00' + cast(Mes as varchar), 2) Between @sFechaAñoMesInicial and @sFechaAñoMesFinal	
		Group by IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes   			
	End 
---------------------------------- RESUMEN 



--- 
	Select 	   	
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		Año, Mes, IdPrograma, Programa, IdSubPrograma, SubPrograma, CostoTotalBeneficiarios, TotalBeneficiarios,
		CostoPromedioBeneficiarios				
	Into  #tmpVentasClaves_Claves	
	From #tmpVentasClaves 	   		   	
----	Group by 
----		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
----		IdMedico, Medico, IdDiagnostico, Diagnostico, Año, Mes, 
----		ClaveSSA, DescripcionClave, PrecioLicitacion    
		   		   	
---    spp_Rpt_CostosBeneficiarios_SalidasClavesMensual


------ Crear Tabla de Referencia Cruzada 
	Select Top 0 
		 IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		 IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		 Año, 
		 (Cast(0 as Numeric(14, 4))) as Enero, (Cast(0 as Numeric(14, 4))) as Febrero, (Cast(0 as Numeric(14, 4))) as Marzo, 
		 (Cast(0 as Numeric(14, 4))) as Abril, (Cast(0 as Numeric(14, 4))) as Mayo, (Cast(0 as Numeric(14, 4))) as Junio, 
		 (Cast(0 as Numeric(14, 4))) as Julio, (Cast(0 as Numeric(14, 4))) as Agosto, (Cast(0 as Numeric(14, 4))) as Septiembre, 
		 (Cast(0 as Numeric(14, 4))) as Octubre, (Cast(0 as Numeric(14, 4))) as Noviembre, (Cast(0 as Numeric(14, 4))) as Diciembre, 
		 (Cast(0 as Numeric(14, 4))) as Total   
	into #tmpVentasClaves_Claves_Cruze	 
	From #tmpVentasClaves_Claves 

--- Agregar cada Clave Localizada 
    Insert Into #tmpVentasClaves_Claves_Cruze 
	Select Distinct 
		 IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		 IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		 Año, 
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
	Update T Set Enero = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iEnero and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma  ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Febrero = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iFebrero and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Marzo = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iMarzo and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Abril = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iAbril and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Mayo = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iMayo and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Junio = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iJunio and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Julio = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iJulio and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Agosto = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iAgosto and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Septiembre = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iSeptiembre and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 

	Update T Set Octubre = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iOctubre and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Noviembre = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iNoviembre and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	Update T Set Diciembre = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.Mes = @iDiciembre and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 


	Update T Set Total = IsNull(( select sum(CostoPromedioBeneficiarios) 
			From #tmpVentasClaves_Claves X 
			Where X.IdEmpresa = T.IdEmpresa and X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia 
			      and X.Año = T.Año and X.IdPrograma = T.IdPrograma and X.IdSubPrograma = T.IdSubPrograma ), 0 )  
	From #tmpVentasClaves_Claves_Cruze T 
	
	

----------------- Asignar los totales por Mes 

/* 
	Set @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Set @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
*/ 

---------------------------------- SALIDA FINAL 
	If @TipoReporte = 1 
		Begin 
			Select		
				 T.IdJurisdiccion, T.Jurisdiccion, 
				 T.IdFarmacia, F.NombreFarmacia as Farmacia, 
				 T.Año, 
				 T.Enero, T.Febrero, T.Marzo, T.Abril, T.Mayo, T.Junio, T.Julio, T.Agosto, T.Septiembre, T.Octubre, T.Noviembre, T.Diciembre, T.Total     
			From #tmpVentasClaves_Claves_Cruze T	 
			-- Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
			-- Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
			Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
			Order By T.IdJurisdiccion, T.IdFarmacia, T.Año 
		End 
	Else 
		Begin 
			Select		
				 T.IdJurisdiccion, T.Jurisdiccion, 
				 T.IdFarmacia, F.NombreFarmacia as Farmacia,		 
				 T.IdPrograma, T.Programa, T.IdSubPrograma, T.SubPrograma,
				 T.Año, 
				 T.Enero, T.Febrero, T.Marzo, T.Abril, T.Mayo, T.Junio, T.Julio, T.Agosto, T.Septiembre, T.Octubre, T.Noviembre, T.Diciembre, T.Total     
			From #tmpVentasClaves_Claves_Cruze T	 
			-- Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
			-- Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
			Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
			Order By T.IdJurisdiccion, T.IdFarmacia, T.Año 
		End 
---------------------------------- SALIDA FINAL 

	
--	spp_Rpt_CostosBeneficiarios_SalidasClavesMensual 

End	
Go--#SQL 
	