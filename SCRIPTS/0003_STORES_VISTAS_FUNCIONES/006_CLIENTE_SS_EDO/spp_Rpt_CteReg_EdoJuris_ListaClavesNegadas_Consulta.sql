/* 

If Exists ( Select Name From tempdb..sysobjects (nolock) Where Name = '##tmp_Mes' and xType = 'U' ) 
   Drop Table tempdb..##tmp_Mes 
Go--#xxSQL 

If Exists ( Select Name From sysobjects (nolock) Where Name = '##tmpClaves_Negadas' and xType = 'U' ) 
   Drop Table ##tmpClaves_Negadas 
Go--#xxSQL 	   


*/ 
 
--		Exec spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Consulta '21', '*', '*', '2012-01-26', 0, 1, 0, 1      

--		Exec          spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas '21', '*', '*', '2012-01-26', 0, 0, 0, 0       


--		Exec spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Consulta '21', '*', '2011-09-26', 0, 0, 0, 0     
 	   
 	   
--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Consulta' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Consulta 
Go--#SQL 

Create Proc spp_Rpt_CteReg_EdoJuris_ListaClavesNegadas_Consulta 
( 
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(3) = '006', @IdFarmacia varchar(4) = '*', 
	@FechaProceso varchar(10) = '2012-11-05', @bEsGeneral int = 0, @ProcesoPorDia int = 0, @MostrarTodo int = 0,  
	@iExecute int = 1 
) 
With Encryption
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@sSql varchar(max),  
	@sCampos varchar(max),  
	@sCampos_Aux varchar(max),  	
	@sCampos_Aux_Base varchar(max),  		
	@sCampos_Aux_Base_2 varchar(max),  			
	@sCampos_Base varchar(max), 
	@sFiltroExec varchar(100)  
		
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

	Set @sFiltroExec = ''  
	if @iExecute = 0 
	   Set @sFiltroExec = ' and 1 = 0 ' 
	
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
	-- Where F.Status = 'A' 
	
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
	   
	
----	Create Table ##tmp_Mes 
----	( 
------		Keyx int identity(1,1), 
----		IdEstado varchar(2) Not Null Default '', 
----		Estado varchar(200) Not Null Default '', 
----		IdJurisdiccion  varchar(4) Not Null Default '', 
----		Jurisdiccion varchar(200) Not Null Default '', 
----		
----		IdFarmacia  varchar(4) Not Null Default '', 
----		Farmacia varchar(200) Not Null Default '', 
----		
----		ClaveSSA varchar(30) Not Null Default '', 
----		DescripcionClave varchar(7800) Not Null Default '', 
----		Presentacion varchar(50) Not Null Default '', 
----		PrecioLicitacion numeric(14,4) Not Null Default 0, 
----		Tipo int Not Null Default 1, 
----		DescTipo varchar(50) Not Null Default '', 
----		Año int Not Null Default 0,  
----		Mes int Not Null Default 0,  		
----		NombreMes varchar(20) Not Null Default '', 
----		Total Numeric(14,4) Not Null Default 0,   
----		Sancion Numeric(14,4) Not Null Default 0 
----	)    

--- Generar la Tabla con los dias del periodo solicitado  --- tempdb.. 
	If Exists ( Select Name From tempdb..sysobjects (nolock) Where Name = '##tmp_Mes' and xType = 'U' ) 
	   Drop Table tempdb..##tmp_Mes 

--- Crear el Repositorio 
	Select S.* 
	Into ##tmp_Mes 
	From Rpt_CTE_Sanciones_Detalles S (NoLock) 
	Inner Join #tmpFarmacias J (NoLock) On ( S.IdEstado = J.IdEstado and S.IdJurisdiccion = J.IdJurisdiccion and S.IdFarmacia = J.IdFarmacia )
	Where S.Año = @Año and S.Mes = @Mes 


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
			-- Set @sCampos = char(39) + cast(@DiaProceso as varchar) + char(39) + ' = _' + cast(@DiaProceso as varchar) 
			Set @sCampos = char(39) + cast(@DiaProceso as varchar) + char(39) + ' = Dia_' + right('00' + cast(@DiaProceso as varchar),2)			
		End 
	Else 
		Begin 
			Set @sSql = 'Alter Table ##tmp_Mes Add ' + cast(@DiaProceso as varchar) + ' Int Not Null Default 0 ' 		
			While ( @iDia <= @DiasMes ) 
				Begin 
					-- Print @iDia 
					Set @sCampos = @sCampos + char(39) + cast(@iDia as varchar) + char(39) + ' = cast(Dia_' + right('00' + cast(@iDia as varchar),2) + ' as int) , '  				
					Set @iDia = @iDia + 1 					
				End 
			--print @sCampos 	
			Set @sCampos = ltrim(rtrim(@sCampos)) 
			Set @sCampos = left(@sCampos, len(@sCampos) - 1)	
			--print @sCampos 	
------			
------			Set @sCampos_Aux_Base = ltrim(rtrim(@sCampos_Aux_Base)) 
------			Set @sCampos_Aux_Base = left(@sCampos_Aux_Base, len(@sCampos_Aux_Base) - 1)				
------			
------			Set @sCampos_Aux_Base_2 = ltrim(rtrim(@sCampos_Aux_Base_2)) 
------			Set @sCampos_Aux_Base_2 = left(@sCampos_Aux_Base_2, len(@sCampos_Aux_Base_2) - 1)							
		End 	
--------------------------------------- Generar Matriz	



	If @MostrarTodo = 0 
		Set @sSql = 'Select ' + @sCampos_Base + ', ' + @sCampos + ', cast(Total as Int) as Total  From ##tmp_Mes ' + 
		' Where ( Total > 0 ' + @sFiltroExec + ' ) ' + 
		' Order by IdJurisdiccion, DescripcionClave, Tipo   '		
	Else 
		Set @sSql = 
		' Select * From ##tmp_Mes ' + -- Where (Total > 0 or Sancion > 0) ' + 
		' Where (Total > 0 or (Tipo in ( 2, 3 )))' + 
		' Order by IdJurisdiccion, DescripcionClave, Tipo   '	
--- Salida Final 
	Set @sSql = @sSql + 
			'  Select count(distinct ClaveSSA) as Claves, isNull(sum(Total), 0) as TotalPiezas ' + 
			'  From ##tmp_Mes Where ( Tipo = 1 and Total > 0 ' + @sFiltroExec + ' )  '  	
	-- Print @sSql 
	Exec(@sSql) 	
	
----	If @MostrarResultado = 1 
----		Begin 
----			Exec(@sSql) 
----		End 	

End 
Go--#SQL 
   
