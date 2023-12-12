--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__019_018__ProgramacionDeConsumos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__019_018__ProgramacionDeConsumos 
Go--#SQL 

---		Exec spp_BI_RPT__019_018__ProgramacionDeConsumos  @IdEmpresa = '001', @IdEstado = '11', @IdMunicipio = '', @IdJurisdiccion = '', @IdFarmacia = '88', @Año = 2017, @Mes = 3   

Create Proc spp_BI_RPT__019_018__ProgramacionDeConsumos  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '3188', @Año int = 2018, @Mes int = 1 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


------------------------------------------ Generar tablas de catalogos  
--	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEstado, E.IdFarmacia, F.Farmacia, 
		year(E.FechaRegistro) as Año, month(E.FechaRegistro) as Mes, 	
		P.ClaveSSA, P.DescripcionClave, -- P.Presentacion, P.Laboratorio, 
		cast(sum((D.CantidadVendida) + 0) as int) as CantidadRecetada,  
		cast(sum(D.CantidadVendida) as int) as CantidadDispensada,  
		0 as CantidadNoDispensada, 
		0 as CantidadProgramada, 
		cast(0 as numeric(14,4)) as PrecioUnitario, 
		cast(0 as numeric(14,4)) as CostoTotal 
	Into #tmp_Dispensacion 
	From VentasEnc E (NoLock) 
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 		
	Left Join vw_Productos_CodigoEAN__PRCS P (NoLock) 
		On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where year(E.FechaRegistro) = @Año and month(E.FechaRegistro) = @Mes and E.IdEmpresa = @IdEmpresa 	
	Group by 
		E.IdEstado, E.IdFarmacia, F.Farmacia, 
		year(E.FechaRegistro), month(E.FechaRegistro), P.ClaveSSA, P.DescripcionClave 
	-- Order By P.ClaveSSA, P.DescripcionClave 

---	Select * from #tmp_Dispensacion 
	
		

	Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	From #tmp_Dispensacion E 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 




	------------------------------------------------------ PROGRAMACION 
	Select  IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, cast('' as varchar(30)) as ClaveSSA, 
		0 as CantidadProgramada, 0 as CantidadAmpliacion, 0 as Ampliacion 	
	Into #tmp__CFG_CB_CuadroBasico_Claves_Programacion 
	From CFG_CB_CuadroBasico_Claves_Programacion (NoLock) -- ( Programación normal ) 
	Where 1 = 0  



	Insert Into #tmp__CFG_CB_CuadroBasico_Claves_Programacion ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, ClaveSSA, CantidadProgramada, CantidadAmpliacion, Ampliacion )  
	Select E.IdEstado, E.IdFarmacia, E.IdCliente, E.IdSubCliente, E.Año, E.Mes, E.IdClaveSSA, P.ClaveSSA, E.Cantidad, 0 as CantidadAmpliacion, 0 as Ampliacion
	From CFG_CB_CuadroBasico_Claves_Programacion E (NoLock) -- ( Programación normal ) 
	Inner Join CatClavesSSA_Sales P (NoLock) On ( E.IdClaveSSA = P.IdClaveSSA_Sal ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where Año = @Año and Mes = @Mes and E.Status = 'A' 


	Insert Into #tmp__CFG_CB_CuadroBasico_Claves_Programacion ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, ClaveSSA, CantidadProgramada, CantidadAmpliacion, Ampliacion )  
	Select E.IdEstado, E.IdFarmacia, E.IdCliente, E.IdSubCliente, E.Año, E.Mes, E.IdClaveSSA, P.ClaveSSA, 0 as CantidadProgramada, E.Cantidad, 1 as Ampliacion
	From CFG_CB_CuadroBasico_Claves_Programacion_Excepciones E (NoLock) -- ( Ampliaciones ) 
	Inner Join CatClavesSSA_Sales P (NoLock) On ( E.IdClaveSSA = P.IdClaveSSA_Sal ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where Año = @Año and Mes = @Mes and E.Status = 'A' 


	Select IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, ClaveSSA, 
		sum(CantidadProgramada) as CantidadProgramada, sum(CantidadAmpliacion) as CantidadAmpliacion, 
		sum(CantidadProgramada + CantidadAmpliacion) as CantidadTotalProgramada 
	Into #tmp__Claves_Programacion
	From #tmp__CFG_CB_CuadroBasico_Claves_Programacion (NoLock) 
	Group by IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, ClaveSSA 


	Update E Set CantidadProgramada = F.CantidadTotalProgramada  
	From #tmp_Dispensacion E (NoLock) 
	Inner Join #tmp__Claves_Programacion F (NoLock) 
		On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia and E.Año = F.Año and E.Mes = F.Mes and E.ClaveSSA = F.ClaveSSA)
		 
----------------------------------------------------- OBTENCION DE DATOS  





---------------------		spp_BI_RPT__019_018__ProgramacionDeConsumos 


----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Dispensacion 

	Select 
		'Año' = Año, 'Mes' = Mes,  	
		-- IdFarmacia, 
		-- 'Fecha de dispensación' = FechaRegistro, 
		IdFarmacia, Farmacia, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		-- 'Presentación' = Presentacion, 		
		-- 'Lote' = ClaveLote, 
		-- 'Caducidad' = FechaCaducidad, 		
		-- 'Laboratorio' = Laboratorio,  		
		
		----'Nombre de beneficiario' = Beneficiario, 
		----'Número de poliza' = Referencia, 
		-- 'Folio de receta' = NumReceta, 		
		--'CIE 10' = CIE10, 
		--'Diagnóstico' = Diagnostico,   
		
		-- 'Nombre del médico' = NombreMedico, 
		-- 'CedulaMedico' = CedulaMedico, 
		-- 'DomicilioMedico' = DomicilioMedico, 				
		
		'Cantidad programada' = (CantidadProgramada),  		
		'Cantidad dispensada' = (CantidadDispensada)  
		--'Precio unitario' = (PrecioUnitario) 
		--'Costo total' = (CostoTotal)  
	From #tmp_Dispensacion 
	Where CantidadProgramada >= 0 and CantidadDispensada > 0  
	----Group by -- IdFarmacia, 
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA, DescripcionClave     
	Order By   
		IdFarmacia, ClaveSSA  

End 
Go--#SQL 


