If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Proceso_VentasPorClaveMensual' and xType = 'P' ) 
   Drop Proc spp_Rpt_Proceso_VentasPorClaveMensual
Go--#SQL 

---- Exec   spp_Rpt_Proceso_VentasPorClaveMensual  '20', '0038', '', '2011-07-09', '2011-07-10', '0'  

--		Exec   spp_Rpt_Proceso_VentasPorClaveMensual  '002', '09', '*', '*', '2014-08-01', '2014-08-01'

Create Proc spp_Rpt_Proceso_VentasPorClaveMensual   
( 
	@IdEmpresa varchar(3) = '002', 
	@IdEstado varchar(2) = '09', @IdJurisdiccion varchar(4) = '006', @IdFarmacia varchar(4) = '1188',
	@FechaInicial varchar(10) = '2013-12-01', @FechaFinal varchar(10) = '2014-01-31' 
	-- @IdClaveSSA varchar(4) = '*',		
	-- , @TipoDispensacion smallint = 0, @TipoInsumo tinyint = 0, @SubFarmacias varchar(200) = ''
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

Declare 
	@sFechaAñoMesInicial varchar(10), 
	@sFechaAñoMesFinal varchar(10)

----------- Preparar el filtro de informacion	
	Set @sFechaAñoMesInicial = cast(datepart(yy, @FechaInicial) as varchar) + right('00' + cast(datepart(mm, @FechaInicial) as varchar), 2) 
	Set @sFechaAñoMesFinal = cast(datepart(yy, @FechaFinal) as varchar) + right('00' + cast(datepart(mm, @FechaFinal) as varchar), 2) 

	

--- Asignar valores iniciales 
	--If @IdClaveSSA = '' 
	  -- Set @IdClaveSSA = '*' 
	
	
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

	-- Se obtiene la Lista de Farmacias de las Jurisdicciones.	
	Select F.IdEstado, F.IdJurisdiccion, J.Jurisdiccion, F.IdFarmacia, F.NombreFarmacia as Farmacia  
	Into #tmpFarmacias 
	From CatFarmacias F (NoLock) 
	Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion ) 
	Where F.Status = 'A' --- and F.IdTipoUnidad <> '006' -- Excluir almacenes 

	If @IdFarmacia <> '*' 
	  Begin
		Delete From #tmpFarmacias Where IdEstado = @IdEstado And IdFarmacia <> @IdFarmacia
	  End

---------------------- Se obtiene la Lista de Claves 
----	Select Top 0 IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, Status, Actualizado  
----	Into #CTE_ClavesAProcesar 
----	From CTE_ClavesAProcesar 
----
----	Insert Into #CTE_ClavesAProcesar ( IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, Status, Actualizado ) 
----	Select E.IdEstado, E.IdFarmacia, E.IdClaveSSA_Sal, E.ClaveSSA, 'A', 0 
----	From vw_ExistenciaPorSales E (NoLock) 
----	Inner Join #tmpFarmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
---------------------- Se obtiene la Lista de Claves 

--          spp_Rpt_Proceso_VentasPorClaveMensual 	

----	if @SubFarmacias <> '' 
----		Begin 
----			Set @sWhereSubFarmacias = ' And L.IdSubFarmacia In ( ' + @SubFarmacias + ' ) '
----		End 
	
----	if @TipoDispensacion = 1 
----	   Set @iConsignacion = 0     	
----	
----	if @TipoDispensacion = 2 
----	   Set @iVenta = 1     	


	Select @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Select @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
	
	Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario 
	from dbo.fg_Unidad_EncabezadoReportesClientesSSA()


--- Obtener los Datos Principales 
	Select -- *  
		T.IdEmpresa, T.IdEstado, 
		space(4) as IdJurisdiccion, space(100) as Jurisdiccion, 
		T.IdFarmacia, space(150) as Farmacia,
		T.IdPrograma, T.IdSubPrograma,
		T.IdMedico, T.IdDiagnostico, T.IdBeneficiario, 
		T.Año, T.Mes, T.Dia, 
		T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave,
		T.TasaIva,		
		T.PrecioLicitacion, T.EsDeConsignacion,
		T.IdTipoDeDispensacion, T.Descripcion as DescTipoDispensacion, 
		sum(T.CantidadVendida) as CantidadVendida 		 		  
	Into #tmpVentasClaves   
	From 
	( 
		Select E.IdEmpresa, E.IdEstado, E.IdFarmacia,
			   E.IdPrograma, E.IdSubPrograma,
			   VA.IdMedico, VA.IdDiagnostico, VA.IdBeneficiario,
			   datepart(yy, E.FechaRegistro) as Año, 
			   datepart(mm, E.FechaRegistro) as Mes,	
			   datepart(dd, E.FechaRegistro) as Dia,				   		   
			   P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, D.TasaIva,			   
			   cast(0 as numeric(14,4)) as PrecioLicitacion,   	
			   (case when L.ClaveLote like '%*%' then 1 else 0 end) as EsDeConsignacion,
			   VA.IdTipoDeDispensacion,	TD.Descripcion, 		   
			   sum(L.CantidadVendida) as CantidadVendida			    
		From VentasEnc E (NoLock)	 
		Inner Join VentasDet D (NoLock) 
			On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
		Inner Join VentasDet_Lotes L (NoLock) 
			On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
		 		 and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )
		Inner Join VentasInformacionAdicional VA (Nolock)
			On ( E.IdEmpresa = VA.IdEmpresa and E.IdEstado = VA.IdEstado and E.IdFarmacia = VA.IdFarmacia and E.FolioVenta = VA.FolioVenta )
		Inner Join CatTiposDispensacion TD (Nolock) On (VA.IdTipoDeDispensacion = TD.IdTipoDeDispensacion) 		
		Inner Join #vw_Productos_CodigoEAN P (NOLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
		Where  E.IdEmpresa = @IdEmpresa and 
			Exists ( Select * From #tmpFarmacias LF Where E.IdEstado = LF.IdEstado And E.IdFarmacia = LF.IdFarmacia )
			-- E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia 
		    -- And convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  
		    -- And convert(varchar(10), E.FechaRegistro, 120) 
			And cast(datepart(yy, E.FechaRegistro) as varchar) + right('00' + cast(datepart(mm, E.FechaRegistro) as varchar), 2) 
			Between @sFechaAñoMesInicial and @sFechaAñoMesFinal 
		Group by  E.IdEmpresa, E.IdEstado, E.IdFarmacia,
			   E.IdPrograma, E.IdSubPrograma, 
			   VA.IdMedico, VA.IdDiagnostico, VA.IdBeneficiario,
			   datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro), datepart(dd, E.FechaRegistro), 		    
			   P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, D.TasaIva, L.ClaveLote, VA.IdTipoDeDispensacion, TD.Descripcion			   
	) as T 
    Where T.EsDeConsignacion in ( @iVenta, @iConsignacion ) 
	Group by T.IdEmpresa, T.IdEstado, T.IdFarmacia,
		T.IdPrograma, T.IdSubPrograma,
		T.IdMedico, T.IdDiagnostico, T.IdBeneficiario,
		T.Año, T.Mes, T.Dia, 
		T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, T.TasaIva,		
		T.PrecioLicitacion, T.EsDeConsignacion, T.IdTipoDeDispensacion, T.Descripcion  




	-- Se Borran los tipos de dispensacion que se dieron por Receta Generada por vales
	Delete From #tmpVentasClaves Where IdTipoDeDispensacion = '07'  --- Receta Generada por vales 
--	select count(*) from #tmpVentasClaves 
	
--          spp_Rpt_Proceso_VentasPorClaveMensual 	
	
	
	--select * from #tmpVentasClaves

----    spp_Rpt_Proceso_VentasPorClaveMensual 
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


	Update C Set Farmacia = F.Farmacia, 
		IdJurisdiccion = F.IdJurisdiccion, Jurisdiccion = F.Jurisdiccion 
	From #tmpVentasClaves C 
	Inner Join #tmpFarmacias F On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 



----------------------------------------------- GENERAR EL CONCENTRADO 
--Set NoCount Off 
---		Drop Table Rpt_Concentrado__DispensacionVentasPorClaveMensual 

	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'Rpt_Concentrado__DispensacionVentasPorClaveMensual' and xType = 'U' ) 
	Begin 
		Select 
			T.IdEmpresa, T.IdEstado, T.IdJurisdiccion, T.Jurisdiccion, T.IdFarmacia, T.Farmacia, 
			T.Año, T.Mes, T.Dia, T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave,
			T.TasaIva,	T.PrecioLicitacion, T.EsDeConsignacion,
			T.IdTipoDeDispensacion, T.DescTipoDispensacion, 
			T.CantidadVendida, 0 as Actualizado   
		Into Rpt_Concentrado__DispensacionVentasPorClaveMensual
		From #tmpVentasClaves T(NoLock) 
	End 

	Update C Set Actualizado = 5 
	From Rpt_Concentrado__DispensacionVentasPorClaveMensual C (NoLock) 
	Inner Join #tmpVentasClaves D (NoLock) 
		On ( C.IdEmpresa = D.IdEmpresa and C.IdEstado = D.IdEstado and C.IdFarmacia = D.IdFarmacia
			 and C.Año = D.Año and C.Mes = D.Mes and C.Dia = D.Dia And C.ClaveSSA = D.ClaveSSA ) 

	
	Delete From Rpt_Concentrado__DispensacionVentasPorClaveMensual Where Actualizado = 5 


	Insert Into Rpt_Concentrado__DispensacionVentasPorClaveMensual 
		(   IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes, Dia, 
		    IdClaveSSA, ClaveSSA, DescripcionClave, TasaIva, PrecioLicitacion, EsDeConsignacion, 
			IdTipoDeDispensacion, DescTipoDispensacion, CantidadVendida, Actualizado ) 
	Select IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, Año, Mes, Dia, 
			IdClaveSSA, ClaveSSA, DescripcionClave, TasaIva, PrecioLicitacion, EsDeConsignacion, 
			IdTipoDeDispensacion, DescTipoDispensacion, CantidadVendida, 0  
	From #tmpVentasClaves 

--Set NoCount On 
----------------------------------------------- GENERAR EL CONCENTRADO 

----------------------------------------------- GENERAR EL CONCENTRADO DE ESTADISTICAS
--Set NoCount Off 
---		Drop Table Rpt_Concentrado__Dispensacion_Mensual_Estadisticas 

		Select 
			T.IdEmpresa, T.IdEstado, T.IdJurisdiccion, T.Jurisdiccion, T.IdFarmacia, T.Farmacia,
			T.IdMedico, (M.Nombre + ' ' + M.ApPaterno + ' ' + M.ApMaterno) as Medico, M.NumCedula,
			T.IdDiagnostico, D.Descripcion as Diagnostico,
			T.Año, T.Mes, T.Dia, T.ClaveSSA, T.DescripcionClave, 
			T.PrecioLicitacion, T.CantidadVendida, 0 as Actualizado   
		Into #tmpVentasClavesEstadisticas
		From #tmpVentasClaves T (NoLock) 
		Inner Join CatMedicos M (Nolock) 
			On ( T.IdEstado = M.IdEstado and T.IdFarmacia = M.IdFarmacia and T.IdMedico = M.IdMedico )
		Inner Join CatCIE10_Diagnosticos D (NoLock) On ( T.IdDiagnostico = D.ClaveDiagnostico )

	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'Rpt_Concentrado__Dispensacion_Mensual_Estadisticas' and xType = 'U' ) 
	Begin 
		Select 
			T.IdEmpresa, T.IdEstado, T.IdJurisdiccion, T.Jurisdiccion, T.IdFarmacia, T.Farmacia,
			T.IdMedico, T.Medico, T.NumCedula,
			T.IdDiagnostico, T.Diagnostico,
			T.Año, T.Mes, T.Dia, T.ClaveSSA, T.DescripcionClave,
			T.PrecioLicitacion, T.CantidadVendida, 0 as Actualizado   
		Into Rpt_Concentrado__Dispensacion_Mensual_Estadisticas
		From #tmpVentasClavesEstadisticas T(NoLock) 
	End 

	Update C Set Actualizado = 5 
	From Rpt_Concentrado__Dispensacion_Mensual_Estadisticas C (NoLock) 
	Inner Join #tmpVentasClavesEstadisticas D (NoLock) 
		On ( C.IdEmpresa = D.IdEmpresa and C.IdEstado = D.IdEstado and C.IdFarmacia = D.IdFarmacia
			 and C.Año = D.Año and C.Mes = D.Mes And C.Dia = D.Dia and C.ClaveSSA = D.ClaveSSA ) 

	
	Delete From Rpt_Concentrado__Dispensacion_Mensual_Estadisticas Where Actualizado = 5 


	Insert Into Rpt_Concentrado__Dispensacion_Mensual_Estadisticas 
		(   IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
			IdMedico, Medico, NumCedula,
			IdDiagnostico, Diagnostico, 
			Año, Mes, Dia, 
		    ClaveSSA, DescripcionClave, PrecioLicitacion,  
			CantidadVendida, Actualizado ) 
	Select IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
			IdMedico, Medico, NumCedula,
			IdDiagnostico, Diagnostico,
			Año, Mes, Dia, 
			ClaveSSA, DescripcionClave, PrecioLicitacion, Sum(CantidadVendida) as CantidadVendida, 0  
	From #tmpVentasClavesEstadisticas
	Group By  IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
			IdMedico, Medico, NumCedula, IdDiagnostico, Diagnostico,
			Año, Mes, Dia, ClaveSSA, DescripcionClave, PrecioLicitacion

--Set NoCount On 
----------------------------------------------- GENERAR EL CONCENTRADO DE ESTADISTICAS



----------------------------------------------- GENERAR EL CONCENTRADO DE COSTOS DE BENEFICIARIOS
--Set NoCount Off 
---		Drop Table Rpt_Concentrado__Dispensacion_Mensual_CostoBeneficiarios 

	Select 
			T.IdEmpresa, T.IdEstado, T.IdJurisdiccion, T.Jurisdiccion, T.IdFarmacia, T.Farmacia,
			T.Año, T.Mes, T.Dia, T.IdPrograma, T.IdSubPrograma,
			-- ((sum(CantidadVendida)) * (Max(T.PrecioLicitacion)) ) as CostoTotalBeneficiarios, 
			sum(CantidadVendida * T.PrecioLicitacion) as CostoTotalBeneficiarios, 			
			Count(T.IdBeneficiario) as TotalBeneficiarios,			
			(Cast(0 as Numeric(14, 4))) as CostoPromedioBeneficiarios,
			0 as Actualizado   
		Into #tmpVentasClavesCostoBeneficiarios
		From #tmpVentasClaves T(NoLock)
		Group By  T.IdEmpresa, T.IdEstado, T.IdJurisdiccion, T.Jurisdiccion, T.IdFarmacia, T.Farmacia,
		T.Año, T.Mes, T.Dia, T.IdPrograma, T.IdSubPrograma


	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'Rpt_Concentrado__Dispensacion_Mensual_CostoBeneficiarios' and xType = 'U' ) 
	Begin 
		Select 
			T.IdEmpresa, T.IdEstado, T.IdJurisdiccion, T.Jurisdiccion, T.IdFarmacia, T.Farmacia,
			T.Año, T.Mes, T.Dia, T.IdPrograma, space(100) as Programa, T.IdSubPrograma, space(100) as SubPrograma, 
			T.CostoTotalBeneficiarios, T.TotalBeneficiarios, T.CostoPromedioBeneficiarios, 0 as Actualizado   
		Into Rpt_Concentrado__Dispensacion_Mensual_CostoBeneficiarios
		From #tmpVentasClavesCostoBeneficiarios T(NoLock)
		
	End 

	Update C Set Actualizado = 5 
	From Rpt_Concentrado__Dispensacion_Mensual_CostoBeneficiarios C (NoLock) 
	Inner Join #tmpVentasClavesCostoBeneficiarios D (NoLock) 
		On ( C.IdEmpresa = D.IdEmpresa and C.IdEstado = D.IdEstado and C.IdFarmacia = D.IdFarmacia
			 and C.Año = D.Año and C.Mes = D.Mes and C.Dia = D.Dia ) 

	
	Delete From Rpt_Concentrado__Dispensacion_Mensual_CostoBeneficiarios Where Actualizado = 5 


	Insert Into Rpt_Concentrado__Dispensacion_Mensual_CostoBeneficiarios 
		(   IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
			Año, Mes, Dia, IdPrograma, Programa, IdSubPrograma, SubPrograma, CostoTotalBeneficiarios, TotalBeneficiarios,
			CostoPromedioBeneficiarios, Actualizado ) 
	Select IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
			Año, Mes, Dia, IdPrograma, '', IdSubPrograma, '', CostoTotalBeneficiarios, TotalBeneficiarios, 
			cast((CostoTotalBeneficiarios / TotalBeneficiarios) as Numeric(14,4)), 0  
	From #tmpVentasClavesCostoBeneficiarios

	Update T Set T.Programa = P.Programa, T.SubPrograma = P.SubPrograma
	From Rpt_Concentrado__Dispensacion_Mensual_CostoBeneficiarios T (Nolock)
	Inner Join vw_Programas_SubProgramas P (Nolock) 
		On ( T.IdPrograma = P.IdPrograma and T.IdSubPrograma = P.IdSubPrograma )
	

--Set NoCount On 
----------------------------------------------- GENERAR EL CONCENTRADO DE COSTOS DE BENEFICIARIOS


--		spp_Rpt_Proceso_VentasPorClaveMensual   

--	Select * From Rpt_Concentrado__DispensacionVentasPorClaveMensual 

--	select * 	from #tmpFarmacias  
--	Select * 	from #tmpVentasClaves 





End	
Go--#SQL 
	