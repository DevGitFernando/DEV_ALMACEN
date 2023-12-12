----------------------------------------------------------------------------------------------------------------------------------------------------------------  
----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_RPT_CB_Programacion_Dispensacion_Concentrado' and xType = 'P' ) 
	Drop Proc spp_RPT_CB_Programacion_Dispensacion_Concentrado
Go--#SQL 

Create Proc spp_RPT_CB_Programacion_Dispensacion_Concentrado 
( 
	@IdEmpresa varchar(3) = '1', 
	@IdEstado varchar(2) = '11', -- @IdFarmacia varchar(4) = '133', 
	@IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '6', 
	@Año int = 2017, @Mes int = 5     
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFiltro varchar(max), 
	@sSQL varchar(max), 
	@iDiasPeriodo int 


	Set @sSQL = '' 
	Set @sFiltro = '' 
	Set @IdEmpresa = right('00000' + @IdEmpresa, 3) 
	Set @IdEstado = right('00000' + @IdEstado, 2) 
	--Set @IdFarmacia = right('00000' + @IdFarmacia, 4) 
	Set @IdCliente = right('00000' + @IdCliente, 4) 
	Set @IdSubCliente = right('00000' + @IdSubCliente, 4) 
	Set @iDiasPeriodo = 30 

------------------------------------------------- PROGRAMACION DE CONSUMO	
	Select  IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, 0 as Ampliacion   	
	Into #tmp__CFG_CB_CuadroBasico_Claves_Programacion 
	From CFG_CB_CuadroBasico_Claves_Programacion (NoLock) -- ( Programación normal ) 
	Where 1 = 0  



	Insert Into #tmp__CFG_CB_CuadroBasico_Claves_Programacion ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, Ampliacion )  
	Select IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, 0 as Ampliacion
	From CFG_CB_CuadroBasico_Claves_Programacion (NoLock) -- ( Programación normal ) 
	Where IdEstado = @IdEstado --and IdFarmacia = @IdFarmacia 
		  and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
		  And Año = @Año and Mes = @Mes 
	Order by FechaRegistro 


	Insert Into #tmp__CFG_CB_CuadroBasico_Claves_Programacion ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, Ampliacion )  
	Select IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, 1 as Ampliacion
	From CFG_CB_CuadroBasico_Claves_Programacion_Excepciones (NoLock) -- ( Ampliaciones ) 
	Where IdEstado = @IdEstado -- and IdFarmacia = @IdFarmacia 
		  and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
		  And Año = @Año and Mes = @Mes 
	Order by FechaRegistro 

---		spp_RPT_CB_Programacion_Dispensacion_Concentrado    

	Select IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, 
		cast(sum(Programacion) as numeric(14, 4)) as Programacion, cast(sum(Ampliacion) as numeric(14, 4)) as Ampliacion, 
		cast(sum(Programacion + Ampliacion)as numeric(14, 4)) as ProgramacionGeneral, 
		cast(0 as numeric(14, 4)) as Dispensacion, 
		cast(0 as numeric(14, 4)) as RequerimientoDiario, 
		cast(0 as numeric(14, 2)) as Porcentaje_Dispensacion    
	Into #tmp__CFG_CB_CuadroBasico_Claves_Programacion_Concentrado  
	From 
	(  
		Select IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, 
			(case when Ampliacion = 0 then (Cantidad) else 0 end) as Programacion, 
			(case when Ampliacion = 1 then (Cantidad) else 0 end) as Ampliacion, 
			0 as Dispensacion  
		From #tmp__CFG_CB_CuadroBasico_Claves_Programacion 
	) T 
	Group by IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA  
------------------------------------------------- PROGRAMACION DE CONSUMO	


	------------------------------------------- SE OBTIENE LO SURTIDO DEL MES EN CURSO  -------------------------------------------------	
	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, cast(Sum(CantidadVendida) as int) as Cantidad, 0 as Relacionada   
	Into #tmpClavesSurtidasMes   
	From VentasEnc E (Nolock) 
	Inner Join VentasDet D (Nolock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta )  
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )  
	--Inner Join #tmp__CFG_CB_CuadroBasico_Claves_Programacion_Concentrado C (NoLock) On ( P.IdClaveSSA_Sal = C.IdClaveSSA ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado -- and E.IdFarmacia = @IdFarmacia 
		and E.IdCliente = @IdCliente and E.IdSubCliente = @IdSubCliente 
		and year(E.FechaRegistro) = @Año and month(E.FechaRegistro) = @Mes 
		-- and P.IdClaveSSA_Sal In ( Select IdClaveSSA From #tmp__CFG_CB_CuadroBasico_Claves_Programacion_Concentrado (Nolock) ) 
	Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia, P.IdClaveSSA_Sal, P.ClaveSSA 



	Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, Relacionada = 1  
	From #tmpClavesSurtidasMes B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	


	Update P Set Dispensacion = IsNull(C.Cantidad, 0)  
	From #tmp__CFG_CB_CuadroBasico_Claves_Programacion_Concentrado P (NoLock) 
	Left Join #tmpClavesSurtidasMes C (NoLock) On ( P.IdClaveSSA = C.IdClaveSSA ) 

	Update C Set Porcentaje_Dispensacion = ( Dispensacion / ProgramacionGeneral) * 100
	From #tmp__CFG_CB_CuadroBasico_Claves_Programacion_Concentrado C 
	Where ProgramacionGeneral > 0 and Dispensacion >= 0 

---		spp_RPT_CB_Programacion_Dispensacion_Concentrado    


---------------------------------------------------------	SALIDA FINAL 
	------Select 
	------	C.IdEstado, C.IdFarmacia, C.IdCliente, C.IdSubCliente, C.Año, C.Mes, 
	------	C.IdClaveSSA, C.Cantidad, 
	------	P.ClaveSSA, P.Descripcion, 
	------	C.FechaRegistro, C.Status, C.Ampliacion 	
	------From #tmp__CFG_CB_CuadroBasico_Claves_Programacion C (NoLock) 
	------Inner Join CatClavesSSA_Sales P (NoLock) On ( C.IdClaveSSA = P.IdClaveSSA_Sal ) 
	------Order By P.Descripcion, C.Ampliacion 

----	spp_RPT_CB_Programacion_Dispensacion_Concentrado  	

	Select 
		-- C.IdEstado, C.IdFarmacia, C.IdCliente, C.IdSubCliente, C.Año, C.Mes, 
		-- C.IdClaveSSA, 
		P.ClaveSSA, P.Descripcion as DescripcionClave, 
		cast(C.Programacion as int) as Programacion, cast(C.Ampliacion as int) as Ampliacion, 
		--'Programación general' = cast(C.ProgramacionGeneral as int) , 
		cast(C.Dispensacion as int) as Dispensacion, 
		'PorcentajeDispensado' = Porcentaje_Dispensacion, 
		('REPORTE DE PROGRACIÓN DE CONSUMOS DEL AÑO ' + cast(@año as varchar(20)) + ' DEL MES ' + CAST(@Mes as varchar(20))) as TituloReporte, 
		getdate() as FechaEmisionReporte     	
	From #tmp__CFG_CB_CuadroBasico_Claves_Programacion_Concentrado C (NoLock) 
	Inner Join CatClavesSSA_Sales P (NoLock) On ( C.IdClaveSSA = P.IdClaveSSA_Sal ) 
	-- Order By P.Descripcion, C.Ampliacion 
	order by Descripcion   
	


End 
Go--#SQL 
