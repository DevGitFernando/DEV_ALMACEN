 
---------------------------------------------------------------------------------------------------------------------------------------------  
---------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From tempdb..sysobjects (nolock) Where Name = '##tmp_Mes' and xType = 'U' ) 
   Drop Table tempdb..##tmp_Mes 
Go--#SQL 

If Exists ( Select Name From sysobjects (nolock) Where Name = '##tmpClaves_Negadas' and xType = 'U' ) 
   Drop Table ##tmpClaves_Negadas 
Go--#SQL 	   


--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Proceso_Detallado' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Proceso_Detallado 
Go--#SQL 

Create Proc spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Proceso_Detallado 
( 
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '*', @IdFarmacia varchar(4) = '*', 
	@FechaProceso varchar(10) = '2011-09-05', 
	@bEsGeneral int = 0, @ProcesoPorDia int = 0, @MostrarTodo int = 0, 
	@MostrarResultado smallint = 1, @IntegrarResultado bit = 0, 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005', 
	@FechaMinimaProceso varchar(10),  -- = '2011-11-15' 	 	
	@FechaMaximaProceso varchar(10) -- = '2011-11-15' 	 
) 
With Encryption
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@sSql varchar(8000),  
	@sCampos varchar(8000),  
	@sCampos_Aux varchar(8000),  	
	@sCampos_Aux_Base varchar(8000),  		
	@sCampos_Aux_Base_2 varchar(8000),  			
	@sCampos_Base varchar(8000) 
		
Declare 
	@Fecha datetime, 
	@DiasMes int, 
	@Año int,  
	@Mes int,  
	@NombreMes varchar(20), 
	@iDia int, 
	@iDiaInicial int, @iDiaFinal int, 
	@DiaProceso int, 
	@sEstado varchar(200), @sFarmacia varchar(200)    
	-- @ProcesoPorDia int 
	
	Set @sSql = '' 
	Set @sCampos_Base = '' 	
	Set @sCampos_Aux_Base = '' 
	Set @sCampos_Aux_Base_2 = '' 
	Set @sCampos = '' 
	Set @sCampos_Aux = '' 
	Set @iDia = 1 
	-- Set @ProcesoPorDia = 0 
	
		
		
	Select @Fecha = cast(@FechaProceso as datetime)  
	Select @Año = datepart(yy, @Fecha) 
	Select @Mes = datepart(mm, @Fecha) 
	Select @NombreMes = dbo.fg_NombresDeMes(@Fecha) 
	Select @DiaProceso = datepart(dd, @Fecha) 
	Select @DiasMes = dbo.fg_NumeroDiasFecha(@Fecha) 
	Select @iDiaInicial = 1, @iDiaFinal = @DiasMes  
 

---------------------------------------- Datos 
----	If @IdJurisdiccion = '*'
----		Set @bEsGeneral = 1 
	
	Select Top 1 @sEstado = upper(Estado), @sFarmacia = Farmacia  
	From vw_Farmacias (NoLock) 
	Where IdEstado = @IdEstado -- and IdFarmacia = @IdJurisdiccion 
	
    Select IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA 
    Into #tmpPerfil_Unidad 
    From vw_CB_CuadroBasico_Farmacias  
    Where IdEstado = @IdEstado and StatusNivel = 'A' and StatusMiembro = 'A'  -- and IdFarmacia = @IdJurisdiccion 
	

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

	
	Select F.IdEstado, F.IdJurisdiccion, F.IdFarmacia, F.NombreFarmacia as Farmacia  
	Into #tmpFarmacias 
	From CatFarmacias F (NoLock) 
	Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion ) 
	Where F.IdFarmacia > 3000		-- and F.IdFarmacia = 1005 

	
--- Proceso invocado desde Farmacia 
	If @IdFarmacia <> '*' 
	   Delete From #tmpFarmacias Where IdFarmacia <> @IdFarmacia 
	
	-- where F.IdFarmacia = 188 

------------------------------------------ 
	If @ProcesoPorDia = 1 
	Begin 
		Set @iDiaInicial = @DiaProceso 
		Set @iDiaFinal = @DiaProceso 		
		Set @DiasMes = 1 
	End 
	Select @DiasMes = 31 

	If Exists ( Select Name From sysobjects (nolock) Where Name = '#tmp_Tipos' and xType = 'U' ) 
	   Drop Table #tmp_Tipos 
	   
	Create Table #tmp_Tipos    
	( 
		Tipo int Not Null Default 1, 
		DescTipo varchar(50) Not Null Default '' 
	)    
	
	Insert Into #tmp_Tipos select 1, 'Piezas'  
	Insert Into #tmp_Tipos select 2, 'Sanción'  
	Insert Into #tmp_Tipos select 3, 'Porcentaje sanción'  		
	   
--- Generar la Tabla con los dias del periodo solicitado  --- tempdb.. 
	If Exists ( Select Name From tempdb..sysobjects (nolock) Where Name = '##tmp_Mes' and xType = 'U' ) 
	   Drop Table tempdb..##tmp_Mes 
	
	Create Table ##tmp_Mes 
	( 
		Keyx int identity(1,1), 
		IdEstado varchar(2) Not Null Default '', 
		Estado varchar(200) Not Null Default '', 
		IdJurisdiccion  varchar(4) Not Null Default '', 
		Jurisdiccion varchar(200) Not Null Default '', 
		
		IdFarmacia  varchar(4) Not Null Default '', 
		Farmacia varchar(200) Not Null Default '', 
		
		ClaveSSA varchar(30) Not Null Default '', 
		DescripcionClave varchar(7800) Not Null Default '', 
		Presentacion varchar(50) Not Null Default '', 
		PrecioLicitacion numeric(14,4) Not Null Default 0, 
		Tipo int Not Null Default 1, 
		DescTipo varchar(50) Not Null Default '', 
		Año int Not Null Default 0,  
		Mes int Not Null Default 0,  		
		NombreMes varchar(20) Not Null Default '', 
		Total Numeric(14,4) Not Null Default 0,   
		Sancion Numeric(14,4) Not Null Default 0 
	)    
	-- Set @sCampos_Base = 'Keyx, ' 
	Set @sCampos_Base = ' IdEstado, Estado, IdFarmacia, Farmacia, ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion, Año, Mes ' 
	Set @sCampos_Base = ' IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion, Año, Mes '	
	---- Select '' as Campo_Base Into ##tmp_Mes 

	If @IdFarmacia <> '*'  
	   Set @sCampos_Base = ' ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion, Año, Mes '	


	If @bEsGeneral = 1 
		Set @sCampos_Base = ' IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion, Tipo, DescTipo, Año, Mes '		


--------------------------------------- Generar Matriz	
	If @ProcesoPorDia = 1 
		Begin 
			Set @sCampos = char(39) + cast(@DiaProceso as varchar) + char(39) + ' = _' + cast(@DiaProceso as varchar) 
			Set @sSql = 'Alter Table ##tmp_Mes Add _' + cast(@DiaProceso as varchar) + ' Int Not Null Default 0 ' 
			Set @sSql = 'Alter Table ##tmp_Mes Add _' + cast(@DiaProceso as varchar) + ' numeric(14,4) Not Null Default 0 ' 			
			Exec(@sSql) 
			--print @sSql 
		End 
	Else 
		Begin 
			Set @sSql = 'Alter Table ##tmp_Mes Add ' + cast(@DiaProceso as varchar) + ' Int Not Null Default 0 ' 		
			While ( @iDia <= @DiasMes ) 
				Begin 
					-- Print @iDia 
					Set @sCampos = @sCampos + char(39) + cast(@iDia as varchar) + char(39) + ' = _' + cast(@iDia as varchar) + ', ' 
					
					
					Set @sCampos_Aux_Base_2 = @sCampos_Aux_Base_2 + '_' + cast(@iDia as varchar) + ', ' 					
					Set @sCampos_Aux_Base = @sCampos_Aux_Base + '_' + cast(@iDia as varchar) + ' as Dia_' + right('00' + cast(@iDia as varchar), 2) + ', ' 					
					
					
					Set @sSql = 'Alter Table ##tmp_Mes Add _' + cast(@iDia as varchar) + ' Int Not Null Default 0 ' 
					Set @sSql = 'Alter Table ##tmp_Mes Add _' + cast(@iDia as varchar) + ' numeric(14,4) Not Null Default 0 ' 								
					Exec(@sSql) 
					
					Set @iDia = @iDia + 1 					
				End 
			Set @sCampos = ltrim(rtrim(@sCampos)) 
			Set @sCampos = left(@sCampos, len(@sCampos) - 1)	
			
			Set @sCampos_Aux_Base = ltrim(rtrim(@sCampos_Aux_Base)) 
			Set @sCampos_Aux_Base = left(@sCampos_Aux_Base, len(@sCampos_Aux_Base) - 1)				
			
			Set @sCampos_Aux_Base_2 = ltrim(rtrim(@sCampos_Aux_Base_2)) 
			Set @sCampos_Aux_Base_2 = left(@sCampos_Aux_Base_2, len(@sCampos_Aux_Base_2) - 1)							
		End 	
--------------------------------------- Generar Matriz	

	If @ProcesoPorDia = 0 
	Begin 
	If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CTE_Sanciones_Detalles' and xType = 'U' ) 
	   Begin 
		   -- Set @sCampos_Aux_Base = ' IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion, Tipo, DescTipo, Año, Mes, ' +  @sCampos_Aux_Base 			   
		   Set @sCampos_Aux_Base = ' IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion, Tipo, DescTipo, Año, Mes, Total, ' +  @sCampos_Aux_Base 		
		   Set @sSql = 'Select ' + @sCampos_Aux_Base + ' ' + 
		               'Into Rpt_CTE_Sanciones_Detalles ' + 
		               'From ##tmp_Mes '  
		   -- Print    @sSql 
		   Exec(@sSql) 
		   Set @sCampos_Aux_Base_2 = ' IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion, Tipo, DescTipo, Año, Mes, Total, ' +  @sCampos_Aux_Base_2 				   
------	       Select * 
------	       Into Rpt_Sanciones 
------	       From 	
	   End 
	End 
	Set @sCampos_Aux_Base_2 =        ' IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion, Tipo, DescTipo, Año, Mes, Total, ' +  @sCampos_Aux_Base_2 				   	


--		spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Proceso_Detallado  


------------------------------------------------ Obtener folios a excluir del Proceso 


/* 

	If Exists ( Select Name From sysobjects (nolock) Where Name = 'tmpvw_Beneficiarios' and xType = 'U' ) 
	   Drop Table tmpvw_Beneficiarios	

	Select v.*, (ApPaterno + ' '  +  ApMaterno + ' ' +  Nombre) as NombreCompleto  
	Into tmpvw_Beneficiarios 
	From CatBeneficiarios V 
	Inner Join CatFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 	
	where F.IdEstado = 21 and F.IdFarmacia > 2000 

*/ 



--	Select @DiaProceso, @DiasMes 
	If Exists ( Select Name From tempdb..sysobjects (nolock) Where Name = '##tmp_Folios_Sancion' and xType = 'U' ) 
	   Drop Table tempdb..##tmp_Folios_Sancion 

	Select V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 
		SPACE(4) AS IdCliente, SPACE(4) AS IdSubCliente, 
		V.NumReceta, V.FechaReceta, V.IdBeneficiario, space(200) as Beneficiario, space(50) as NumPoliza, 
		0 as TieneVale  
	Into ##tmp_Folios_Sancion 
	From VentasInformacionAdicional V (NoLock) 	
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
	Where 
		datepart(yy, V.FechaReceta) = @Año   -- V.FechaRegistro 
		and datepart(mm, V.FechaReceta) = @Mes 
		and datepart(d, V.FechaReceta) between @iDiaInicial and @iDiaFinal 
		and ( convert(varchar(10), V.FechaReceta, 120) between @FechaMinimaProceso and @FechaMaximaProceso ) 

		
	Update F Set IdCliente = V.IdCliente, IdSubcliente = V.IdSubCliente 	
	From ##tmp_Folios_Sancion F 
	Inner Join VentasEnc V (NoLock) On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta )

		
	Update F Set Beneficiario = B.NombreCompleto, NumPoliza = B.FolioReferencia   
	From ##tmp_Folios_Sancion F 
	Inner Join tmpvw_Beneficiarios B 
		On ( F.IdEstado = B.IdEstado and F.IdFarmacia = B.IdFarmacia 
			 and F.IdCliente = B.IdCliente and F.IdSubCliente = B.IdSubCliente and F.IdBeneficiario = B.IdBeneficiario ) 
		

	--select * from ##tmp_Folios_Sancion 


----------------------------------------------- 		
------------ Migrar informacion 
----	-- sp_listacolumnas Rpt_CTE_VentasEstadisticaClavesDispensadas 
----	Select 
----		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 
----		V.IdClaveSSA, V.Observaciones, V.EsCapturada, V.CantidadRequerida, V.CantidadEntregada, V.Status, V.Actualizado 
----    Into #tmpEstadisticaClavesDispensadas  
----	From VentasEstadisticaClavesDispensadas V (NoLock) 
----	Inner Join ##tmp_Folios_Sancion F (NoLock) 
----		On ( V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and V.FolioVenta = F.FolioVenta ) 
----
----
----	Insert Into Rpt_CTE_VentasEstadisticaClavesDispensadas 
----	Select V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, 
----		V.IdClaveSSA, V.Observaciones, V.EsCapturada, V.CantidadRequerida, V.CantidadEntregada, V.Status, V.Actualizado 
----	From #tmpEstadisticaClavesDispensadas V 
----	Where Not Exists 
----		( 
----			Select * 
----			From Rpt_CTE_VentasEstadisticaClavesDispensadas F (NoLock) 
----			Where V.IdEmpresa = F.IdEmpresa and V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia and V.FolioVenta = F.FolioVenta 
----				  and V.IdClaveSSA = F.IdClaveSSA 
----		) 
--------------------------------------------------- 		



	Update F Set TieneVale = 1 
	From ##tmp_Folios_Sancion F 
	Inner Join Vales_EmisionEnc V (NoLock) 
		On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta ) 
    
    Delete From ##tmp_Folios_Sancion Where TieneVale = 1              
------------------------------------------------ Obtener folios a excluir del Proceso 





------------------------------------------------------------------------------ Obtener listado de Claves 
	If Exists ( Select Name From tempdb..sysobjects (nolock) Where Name = '##tmpClaves_Negadas' and xType = 'U' ) 
	   Drop Table tempdb..##tmpClaves_Negadas 
	   
	Select 
		-- V.IdEstado, (case when @bEsGeneral = 1 then '' else F.IdJurisdiccion end) as IdJurisdiccion, 
		V.IdEstado, F.IdJurisdiccion, F.IdFarmacia, F.Farmacia, 
		S.FolioVenta, 
		space(20) as NumReceta, getdate() as FechaReceta, space(20) as IdBeneficiario, space(200) Beneficiario, space(50) as NumPoliza, 	
		datepart(yy, V.FechaReceta) as Año, datepart(mm, V.FechaReceta) as Mes, datepart(dd, V.FechaReceta) as Dia, 
		C.ClaveSSA, C.DescripcionClave, C.Presentacion, 
		sum(floor(D.CantidadRequerida)) as CantidadRequerida,  
		cast(0 as int) as CantidadVales,   		
		----cast(0 as numeric(14,4)) as Cantidad  
		---- sum(ceiling(D.CantidadEntregada)) as Cantidad 		

		--sum(floor(D.CantidadRequerida)) as Cantidad, 
		
		sum(floor(D.CantidadRequerida / (C.ContenidoPaquete*1.0))) as Cantidad, 
		-- sum(floor( (D.CantidadRequerida - D.CantidadEntregada) / (C.ContenidoPaquete*1.0))) as Cantidad, 				
		
		cast(0 as numeric(14,4)) as PrecioLicitacion, 
		cast(0 as numeric(14,4)) as PorcSancion, 
		cast(0 as numeric(14,4)) as ValorSancion, 
		1 as Tipo  
	into ##tmpClaves_Negadas 
--	From VentasEnc V (NoLock) 
	From VentasInformacionAdicional V (NoLock) 	
	Inner Join ##tmp_Folios_Sancion S (NoLock) 
		On ( V.IdEmpresa = S.IdEmpresa and V.IdEstado = S.IdEstado and V.IdFarmacia = S.IdFarmacia and V.FolioVenta = S.FolioVenta ) 				
	Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
	Inner Join 	vw_ClavesSSA_Sales C (NoLock) On ( D.IdClaveSSA = C.IdClaveSSA_Sal ) 
	Inner Join #tmpPerfil_Unidad P (NoLock) On ( V.IdEstado = P.IdEstado and V.IdFarmacia = P.IdFarmacia and C.ClaveSSA = P.ClaveSSA ) 
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
	Where D.EsCapturada = 1 
		  and D.CantidadRequerida > 0 --- Especial 
		  and V.IdTipoDeDispensacion in ( '00', '01' )  --- ( '00', '01', '02' ) 
		  and D.CantidadEntregada <= 0 
		  -- V.IdEstado = @IdEstado -- and V.IdFarmacia = @IdJurisdiccion  
		  and 
		  ( 
			  datepart(yy, V.FechaReceta) = @Año   -- V.FechaRegistro 
			  and 	datepart(mm, V.FechaReceta) = @Mes 
			  and datepart(d, V.FechaReceta) between @iDiaInicial and @iDiaFinal 
		  ) 
		  --and C.ClaveSSA = '101' 
	Group by 
		-- V.IdEstado, (case when @bEsGeneral = 1 then '' else F.IdJurisdiccion end) as IdJurisdiccion, 
		V.IdEstado, F.IdJurisdiccion, F.IdFarmacia, F.Farmacia, 
		S.FolioVenta, 
		datepart(yy, V.FechaReceta), datepart(mm, V.FechaReceta), datepart(dd, V.FechaReceta), 
		C.ClaveSSA, C.DescripcionClave, C.Presentacion 
	Order By C.DescripcionClave, datepart(dd, V.FechaReceta) 
	
	
-------------------------------- 	
	If Exists ( Select Name From sysobjects (nolock) Where Name = '#tmpClaves_Vales' and xType = 'U' ) 
	   Drop Table #tmpClaves_Vales  	
	
	Select 
		-- V.IdEstado, (case when @bEsGeneral = 1 then '' else F.IdJurisdiccion end) as IdJurisdiccion, 
		V.IdEstado, F.IdJurisdiccion, F.IdFarmacia, F.Farmacia, 
		E.FolioVenta, 
		datepart(yy, V.FechaReceta) as Año, datepart(mm, V.FechaReceta) as Mes, datepart(dd, V.FechaReceta) as Dia, 
		C.ClaveSSA, -- C.DescripcionClave, C.Presentacion, 	
		sum(floor(D.Cantidad)) as Cantidad 
		-- sum(ceiling(D.CantidadEntregada)) as Cantidad 		
	into #tmpClaves_Vales  
	From Vales_EmisionEnc E (NoLock) 
	Inner Join Vales_Emision_InformacionAdicional V (NoLock) 		
		On ( E.IdEmpresa = V.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = V.IdFarmacia and E.FolioVale = V.FolioVale ) 	
	Inner Join Vales_EmisionDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVale = D.FolioVale ) 
	Inner Join 	vw_ClavesSSA_Sales C (NoLock) On ( D.IdClaveSSA_Sal = C.IdClaveSSA_Sal ) 
	Inner Join #tmpPerfil_Unidad P (NoLock) On ( V.IdEstado = P.IdEstado and V.IdFarmacia = P.IdFarmacia and C.ClaveSSA = P.ClaveSSA ) 
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia )	
	Where E.FolioVenta <> '' 
		  -- V.IdEstado = @IdEstado -- and V.IdFarmacia = @IdJurisdiccion  
		  and V.IdTipoDeDispensacion in ( '00', '01' )  --- ( '00', '01', '02' ) 
		  and 
		  ( 
			  datepart(yy, V.FechaReceta) = @Año   -- V.FechaRegistro 
			  and 	datepart(mm, V.FechaReceta) = @Mes 
			  and datepart(d, V.FechaReceta) between @iDiaInicial and @iDiaFinal 
		  ) 
		  -- and C.ClaveSSA = '101' 		  
	Group by 
		-- V.IdEstado, (case when @bEsGeneral = 1 then '' else F.IdJurisdiccion end) as IdJurisdiccion, 
		V.IdEstado, F.IdJurisdiccion, F.IdFarmacia, F.Farmacia, 
		E.FolioVenta, 
		datepart(yy, V.FechaReceta), datepart(mm, V.FechaReceta), datepart(dd, V.FechaReceta), 
		C.ClaveSSA --, C.DescripcionClave, C.Presentacion 
	--  Order By C.DescripcionClave, datepart(dd, V.FechaRegistro) 
	
--- Vales anteriores a Noviembre 	
	Update N Set Cantidad = 0 
	From #tmpClaves_Vales N 
	Where Cantidad < 0 	
	
	
---- Descontar la Dispensacion por vales 	
--	Update N Set CantidadVales = V.Cantidad, Cantidad = ( N.CantidadRequerida - V.Cantidad) 
	Update N Set CantidadVales = V.Cantidad, Cantidad = ( N.Cantidad - V.Cantidad) 	
	From ##tmpClaves_Negadas N 
	Inner Join #tmpClaves_Vales V -- On ( N.IdEstado = V.IdEstado and N.IdFarmacia = V.IdFarmacia and N.ClaveSSA = V.ClaveSSA ) 
		On ( N.IdEstado = V.IdEstado and N.IdJurisdiccion = V.IdJurisdiccion and N.IdFarmacia = V.IdFarmacia and N.FolioVenta = V.FolioVenta 
			 and N.ClaveSSA = V.ClaveSSA and N.Año = V.Año and N.Mes = V.Mes and N.Dia = V.Dia ) 

	Update N Set Cantidad = 0 
	From ##tmpClaves_Negadas N 
	Where Cantidad < 0 


--------- ESPECIAL 
	Update N Set Cantidad = (N.Cantidad - 1)
	From ##tmpClaves_Negadas N 
	Where Cantidad > 2 and 1 = 0 
--------- ESPECIAL 

		
--------------	 Importante 		
------	Update N Set Cantidad = floor(Cantidad * 0.05)
------	From ##tmpClaves_Negadas N 
--------	Where 
			
		
--	select * from ##tmpClaves_Negadas 
	
--	select count(*) from ##tmpClaves_Negadas 	  

--- vw_ClavesSSA_Sales	

--		select  top 1 * from VentasEstadisticaClavesDispensadas 
	
------------------------------------------------------------------------------ Obtener listado de Claves 




--------------------------------------------- Agregar la informacion Inicial	
	Update L Set PrecioLicitacion = P.Precio  
	From ##tmpClaves_Negadas L 
	Inner Join vw_Claves_Precios_Asignados P (NoLock) 
		On ( L.IdEstado = P.IdEstado and L.ClaveSSA = P.ClaveSSA and P.IdCliente = @IdCliente and P.IdSubCliente = @IdSubCliente ) 

	Update L Set ValorSancion = (PrecioLicitacion * Cantidad) * 1.06   
	From ##tmpClaves_Negadas L 	
	
	
	Update F Set NumReceta = V.NumReceta, FechaReceta = V.FechaReceta, 
		IdBeneficiario = V.IdBeneficiario, Beneficiario = V.Beneficiario, NumPoliza = V.NumPoliza  	
	From ##tmpClaves_Negadas F 	
	Inner Join  ##tmp_Folios_Sancion  V (NoLock) On ( F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.FolioVenta = V.FolioVenta )


--		spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Proceso_Detallado  	

	
----	Select * 
----	From ##tmpClaves_Negadas 
----------------- Valorizar la sancion 	

------ Asignar nombre de Jurisdiccion 
	Update L Set Jurisdiccion = J.Jurisdiccion 
	From ##tmp_Mes L 
	Inner Join #tmpJuris J On ( L.IdEstado = J.IdEstado and L.IdJurisdiccion = J.IdJurisdiccion )  

	Update L Set DescTipo = T.DescTipo 
	From ##tmp_Mes L 
	Inner Join #tmp_Tipos T on ( L.Tipo = T.Tipo ) 

------ Asignar nombre de Jurisdiccion 	
	
----------------------------------------------------- Integrar detalles de sancion 
	If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CTE_Sanciones_Detalles_Dispensacion' and xType = 'U' ) 
	   Begin 
	       Select Top 0 
				IdEstado, IdJurisdiccion, IdFarmacia, Farmacia, FolioVenta, 
				NumReceta, FechaReceta, IdBeneficiario, Beneficiario, NumPoliza, Año, Mes, Dia, 
				ClaveSSA, DescripcionClave, Presentacion, CantidadRequerida, CantidadVales, Cantidad, PrecioLicitacion, ValorSancion 
	       Into Rpt_CTE_Sanciones_Detalles_Dispensacion 
	       From ##tmpClaves_Negadas 
	   End 

--- Quitar cualquier dato almacenado 
	Delete From Rpt_CTE_Sanciones_Detalles_Dispensacion Where IdEstado = @IdEstado and Año = @Año and Mes = @Mes  
	
	Insert Into Rpt_CTE_Sanciones_Detalles_Dispensacion ( IdEstado, IdJurisdiccion, IdFarmacia, Farmacia, FolioVenta, 
				NumReceta, FechaReceta, IdBeneficiario, Beneficiario, NumPoliza, Año, Mes, Dia, 
				ClaveSSA, DescripcionClave, Presentacion, CantidadRequerida, CantidadVales, Cantidad, PrecioLicitacion, ValorSancion ) 
	Select  IdEstado, IdJurisdiccion, IdFarmacia, Farmacia, FolioVenta, 
			NumReceta, FechaReceta, IdBeneficiario, Beneficiario, NumPoliza, Año, Mes, Dia, 
			ClaveSSA, DescripcionClave, Presentacion, CantidadRequerida, CantidadVales, Cantidad, PrecioLicitacion, ValorSancion 			
	From ##tmpClaves_Negadas 

-------------------------------------------------------	
--- Borrador de proceso 
/* 
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CTE_Sanciones_Detalles_Dispensacion_Aux' and xType = 'U' ) 
	   Drop Table Rpt_CTE_Sanciones_Detalles_Dispensacion_Aux 
	   
	Select * 
	Into Rpt_CTE_Sanciones_Detalles_Dispensacion_Aux 
	From Rpt_CTE_Sanciones_Detalles_Dispensacion    
	Where IdEstado = @IdEstado and Año = @Año and Mes = @Mes 	
*/ 
	If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CTE_Sanciones_Detalles_Dispensacion_Aux' and xType = 'U' ) 
		Begin 	   
			Select * 
			Into Rpt_CTE_Sanciones_Detalles_Dispensacion_Aux 
			From Rpt_CTE_Sanciones_Detalles_Dispensacion    
			Where IdEstado = @IdEstado and Año = @Año and Mes = @Mes 	
		End  
	Else 
	Begin 
		Delete From Rpt_CTE_Sanciones_Detalles_Dispensacion_Aux Where IdEstado = @IdEstado and Año = @Año and Mes = @Mes  
	
		Insert Into Rpt_CTE_Sanciones_Detalles_Dispensacion_Aux 
		( 
			IdEstado, IdJurisdiccion, IdFarmacia, Farmacia, FolioVenta, NumReceta, FechaReceta, IdBeneficiario, Beneficiario, NumPoliza, 
			Año, Mes, Dia, ClaveSSA, DescripcionClave, Presentacion, CantidadRequerida, CantidadVales, Cantidad, PrecioLicitacion, ValorSancion, 
			PorcSancion, Tipo  
		) 
		Select distinct 
			IdEstado, IdJurisdiccion, IdFarmacia, Farmacia, FolioVenta, NumReceta, FechaReceta, IdBeneficiario, Beneficiario, NumPoliza, 
			Año, Mes, Dia, ClaveSSA, DescripcionClave, Presentacion, CantidadRequerida, CantidadVales, Cantidad, PrecioLicitacion, ValorSancion, 
			0 as PorcSancion, 1 as Tipo   
		From Rpt_CTE_Sanciones_Detalles_Dispensacion    
		Where IdEstado = @IdEstado and Año = @Año and Mes = @Mes 
	End 
	
-------------------------------------------------------	

---	Salida Final 
	Select * From Rpt_CTE_Sanciones_Detalles_Dispensacion_Aux 
	Where IdEstado = @IdEstado and Año = @Año and Mes = @Mes 

----------------------------------------------------- Integrar detalles de sancion 


	
	If @MostrarResultado = 1 
		Begin 
			Exec(@sSql) 
		End 	
 
End 
Go--#SQL 
   
