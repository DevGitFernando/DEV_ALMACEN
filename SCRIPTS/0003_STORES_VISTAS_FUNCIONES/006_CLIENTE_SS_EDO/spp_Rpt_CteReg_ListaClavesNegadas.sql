/* 
If Exists ( Select Name From sysobjects (nolock) Where Name = '#tmp_Mes' and xType = 'U' ) 
   Drop Table #tmp_Mes 
Go--#xxSQL 

If Exists ( Select Name From sysobjects (nolock) Where Name = '#tmpClaves_Negadas' and xType = 'U' ) 
   Drop Table #tmpClaves_Negadas 
Go--#xxSQL 	   
*/
 
--		Exec spp_Rpt_CteReg_ListaClavesNegadas '21', '0188', '2011-08-26', '0', 1    

 	   
--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_CteReg_ListaClavesNegadas' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_ListaClavesNegadas 
Go--#SQL 

Create Proc spp_Rpt_CteReg_ListaClavesNegadas 
( 
	@IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0188', 
	@FechaProceso varchar(10) = '2012-01-05', @ProcesoPorDia int = 0, @MostrarTodo int = 0   
) 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@sSql varchar(8000),  
	@sCampos varchar(8000),  
	@sCampos_Aux varchar(8000),  	
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
	
	Set @sSql = '' 
	Set @sCampos_Base = '' 	
	Set @sCampos = '' 
	Set @sCampos_Aux = '' 
	Set @iDia = 1 
		
		
	Select @Fecha = cast(@FechaProceso as datetime) 
	Select @Año = datepart(yy, @Fecha) 
	Select @Mes = datepart(mm, @Fecha) 
	Select @NombreMes = dbo.fg_NombresDeMes(@Fecha) 
	Select @DiaProceso = datepart(dd, @Fecha) 
	Select @DiasMes = dbo.fg_NumeroDiasFecha(@Fecha) 
	Select @iDiaInicial = 1, @iDiaFinal = @DiasMes  

--- Datos 
	Select @sEstado = Estado, @sFarmacia = Farmacia  
	From vw_Farmacias (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	
    Select IdClaveSSA, ClaveSSA 
    Into #tmpPerfil_Unidad 
    From vw_CB_CuadroBasico_Farmacias  
    Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	

	If @ProcesoPorDia = 1 
	Begin
		Set @iDiaInicial = @DiaProceso 
		Set @iDiaFinal = @DiaProceso 		
		Set @DiasMes = 1 
	End 

--- Generar la Tabla con los dias del periodo solicitado 
	If Exists ( Select Name From sysobjects (nolock) Where Name = '#tmp_Mes' and xType = 'U' ) 
	   Drop Table #tmp_Mes 
	
	Create Table #tmp_Mes 
	( 
		Keyx int identity(1,1), 
		IdEstado varchar(2) Not Null Default '', 
		Estado varchar(200) Not Null Default '', 
		IdFarmacia varchar(4) Not Null Default '', 
		Farmacia varchar(200) Not Null Default '', 
		ClaveSSA varchar(30) Not Null Default '', 
		DescripcionClave varchar(7800) Not Null Default '', 
		Presentacion varchar(50) Not Null Default '', 
		PrecioLicitacion numeric(14,4) Not Null Default 0, 
		Año int Not Null Default 0,  
		Mes int Not Null Default 0,  		
		NombreMes varchar(20) Not Null Default '', 
		Total int Not Null Default 0  
	)    
	-- Set @sCampos_Base = 'Keyx, ' 
	Set @sCampos_Base = ' IdEstado, Estado, IdFarmacia, Farmacia, ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion, Año, Mes ' 
	Set @sCampos_Base = ' ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion, Año, Mes '	
	---- Select '' as Campo_Base Into #tmp_Mes 


--------------------------------------- Generar Matriz	
	If @ProcesoPorDia = 1 
		Begin 
			Set @sCampos = char(39) + cast(@DiaProceso as varchar) + char(39) + ' = _' + cast(@DiaProceso as varchar) 
			Set @sSql = 'Alter Table #tmp_Mes Add _' + cast(@DiaProceso as varchar) + ' Int Not Null Default 0 ' 
			Exec(@sSql) 
			--print @sSql 
		End 
	Else 
		Begin 
			Set @sSql = 'Alter Table #tmp_Mes Add ' + cast(@DiaProceso as varchar) + ' Int Not Null Default 0 ' 		
			While ( @iDia <= @DiasMes ) 
				Begin 
					-- Print @iDia 
					Set @sCampos = @sCampos + char(39) + cast(@iDia as varchar) + char(39) + ' = _' + cast(@iDia as varchar) + ', ' 
					Set @sSql = 'Alter Table #tmp_Mes Add _' + cast(@iDia as varchar) + ' Int Not Null Default 0 ' 
					Exec(@sSql) 
					
					Set @iDia = @iDia + 1 					
				End 
			Set @sCampos = ltrim(rtrim(@sCampos)) 
			Set @sCampos = left(@sCampos, len(@sCampos) - 1)	
		End 	
--------------------------------------- Generar Matriz	



--		spp_Rpt_CteReg_ListaClavesNegadas  

--	Select @DiaProceso, @DiasMes 


------------------------------------------------------------------------------ Obtener listado de Claves 
	If Exists ( Select Name From sysobjects (nolock) Where Name = '#tmpClaves_Negadas' and xType = 'U' ) 
	   Drop Table #tmpClaves_Negadas 
	   
	Select 
		datepart(yy, V.FechaRegistro) as Año, datepart(mm, V.FechaRegistro) as Mes, datepart(dd, V.FechaRegistro) as Dia, 
		C.ClaveSSA, C.DescripcionClave, C.Presentacion, 
		sum(floor(D.CantidadRequerida)) as CantidadRequerida,  
		cast(0 as numeric(14,4)) as CantidadVales,   		
		cast(0 as numeric(14,4)) as Cantidad  
		-- sum(ceiling(D.CantidadEntregada)) as Cantidad 		
	into #tmpClaves_Negadas 
	From VentasEnc V (NoLock) 
	Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
	Inner Join 	vw_ClavesSSA_Sales C (NoLock) On ( D.IdClaveSSA = C.IdClaveSSA_Sal ) 
	Inner Join #tmpPerfil_Unidad P (NoLock) On ( C.ClaveSSA = P.ClaveSSA )
	Where D.EsCapturada = 1 and 
		  V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia  
		  and 
		  ( 
			  datepart(yy, V.FechaRegistro) = @Año 
			  and 	datepart(mm, V.FechaRegistro) = @Mes 
			  and datepart(d, V.FechaRegistro) between @iDiaInicial and @iDiaFinal 
		  ) 
	Group by 
		datepart(yy, V.FechaRegistro), datepart(mm, V.FechaRegistro), datepart(dd, V.FechaRegistro), 
		C.ClaveSSA, C.DescripcionClave, C.Presentacion 
	Order By C.DescripcionClave, datepart(dd, V.FechaRegistro) 
	
	
-------------------------------- 	
	If Exists ( Select Name From sysobjects (nolock) Where Name = '#tmpClaves_Vales' and xType = 'U' ) 
	   Drop Table #tmpClaves_Vales  	
	
	Select 
		datepart(yy, V.FechaRegistro) as Año, datepart(mm, V.FechaRegistro) as Mes, datepart(dd, V.FechaRegistro) as Dia, 
		C.ClaveSSA, -- C.DescripcionClave, C.Presentacion, 	
		sum(D.Cantidad) as Cantidad  
		-- sum(ceiling(D.CantidadEntregada)) as Cantidad 		
	into #tmpClaves_Vales  
	From Vales_EmisionEnc V (NoLock) 
	Inner Join Vales_EmisionDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVale = D.FolioVale ) 
	Inner Join 	vw_ClavesSSA_Sales C (NoLock) On ( D.IdClaveSSA_Sal = C.IdClaveSSA_Sal ) 
	Inner Join #tmpPerfil_Unidad P (NoLock) On ( C.ClaveSSA = P.ClaveSSA ) 
	Where V.FolioVenta <> '' and 
		  V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia  
		  and 
		  ( 
			  datepart(yy, V.FechaRegistro) = @Año 
			  and 	datepart(mm, V.FechaRegistro) = @Mes 
			  and datepart(d, V.FechaRegistro) between @iDiaInicial and @iDiaFinal 
		  ) 
	Group by 
		datepart(yy, V.FechaRegistro), datepart(mm, V.FechaRegistro), datepart(dd, V.FechaRegistro), 
		C.ClaveSSA --, C.DescripcionClave, C.Presentacion 
	--  Order By C.DescripcionClave, datepart(dd, V.FechaRegistro) 
	
	
	
	
---- Descontar la Dispensacion por vales 	
	Update N Set CantidadVales = V.Cantidad, Cantidad = ( N.CantidadRequerida - V.Cantidad) 
	From #tmpClaves_Negadas N 
	Inner Join #tmpClaves_Vales V -- On ( N.IdEstado = V.IdEstado and N.IdFarmacia = V.IdFarmacia and N.ClaveSSA = V.ClaveSSA ) 
		On ( N.ClaveSSA = V.ClaveSSA and N.Año = V.Año and N.Mes = V.Mes and N.Dia = V.Dia ) 

	Update N Set Cantidad = 0 
	From #tmpClaves_Negadas N 
	Where Cantidad < 0 
		
--	select * from #tmpClaves_Negadas 
	
--	select count(*) from #tmpClaves_Negadas 	  

--- vw_ClavesSSA_Sales	

--		select  top 1 * from VentasEstadisticaClavesDispensadas 
	
------------------------------------------------------------------------------ Obtener listado de Claves 




--------------------------------------------- Agregar la informacion Inicial	
	Insert Into #tmp_Mes ( IdEstado, Estado, IdFarmacia, Farmacia, Año, Mes, NombreMes, ClaveSSA, DescripcionClave, Presentacion, PrecioLicitacion ) 
	Select Distinct @IdEstado, @sEstado, @IdFarmacia, @sFarmacia, @Año, @Mes, @NombreMes, ClaveSSA, DescripcionClave, Presentacion, 0 as PrecioLicitacion  
	From #tmpClaves_Negadas 
	Order By DescripcionClave 

	Update L Set PrecioLicitacion = P.Precio  
	From #tmp_Mes L 
	Inner Join vw_Claves_Precios_Asignados P (NoLock) 
		On ( L.IdEstado = P.IdEstado and L.ClaveSSA = P.ClaveSSA )



	Set @iDia = 1 
	Set @sCampos_Aux = '' 
	If @ProcesoPorDia = 1 
		Begin 
			Set @sSql = 
				'Update M Set _' + cast(@DiaProceso as varchar) + ' = N.Cantidad '  + char(13) + 
				' , Total = N.Cantidad '  + char(13) +
				'From #tmp_Mes M ' + char(13) + 
				'Inner Join #tmpClaves_Negadas N On ( M.Año = N.Año and M.Mes = N.Mes and M.ClaveSSA = N.ClaveSSA and N.Dia = ' + cast(@DiaProceso as varchar ) +  ' )' 			 
			Exec(@sSql) 		
		End 
	Else 
	Begin 
		While ( @iDia <= @DiasMes ) 
			Begin 			
				Set @sCampos_Aux = @sCampos_Aux + ' _' + cast(@iDia as varchar) + ' + ' 
				Set @sSql = 
					'Update M Set _' + cast(@iDia as varchar) + ' = N.Cantidad '     + char(13) + 
					'From #tmp_Mes M ' + char(13) + 
					'Inner Join #tmpClaves_Negadas N On ( M.Año = N.Año and M.Mes = N.Mes and M.ClaveSSA = N.ClaveSSA and N.Dia = ' + cast(@iDia as varchar ) +  ' )' 			 
				Exec(@sSql) 
				
				Set @iDia = @iDia + 1 					
			End 
			
		Set @sCampos_Aux = ltrim(rtrim(@sCampos_Aux)) 
		Set @sCampos_Aux = '(' + left(@sCampos_Aux, len(@sCampos_Aux) - 1)+ ' ) ' 
		Set @sSql = 'Update #tmp_Mes Set Total = ' + @sCampos_Aux 
		Exec(@sSql) 
		
	End 	
	

--		spp_Rpt_CteReg_ListaClavesNegadas  	

--		select * from #tmpClaves_Negadas 	  		
--------------------------------------------- Agregar la informacion Inicial		
	
	
	
---	Select * from #tmp_Mes 
---	select * from #tmpClaves_Negadas where ClaveSSA = '624'


---------------------------------------------	
--------------------------------------------- Salida Final del Proceso 	
---------------------------------------------
	If @MostrarTodo = 0 
		Set @sSql = 'Select ' + @sCampos_Base + ', ' + @sCampos + ', Total  From #tmp_Mes Where Total > 0 ' 
	Else 
		Set @sSql = 'Select * From #tmp_Mes Where Total > 0 ' 	

--- Salida Final 
	Exec(@sSql) 

End 
Go--#SQL 
   
