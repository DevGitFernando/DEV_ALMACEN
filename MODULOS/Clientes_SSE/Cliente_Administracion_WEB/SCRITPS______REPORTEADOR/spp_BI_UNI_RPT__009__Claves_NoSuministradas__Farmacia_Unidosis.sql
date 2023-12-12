------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__009__Claves_NoSuministradas__Farmacia_Unidosis' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__009__Claves_NoSuministradas__Farmacia_Unidosis 
Go--#SQL 

/* 

Exec spp_BI_UNI_RPT__009__Claves_NoSuministradas__Farmacia_Unidosis 
	@IdEmpresa = '004', 
	@IdEstado = '11', @IdMunicipio = '*', @IdJurisdiccion = '*', 
	@IdFarmacia = '', @ClaveSSA = '', 
	@Unidad_Beneficiario = '', 
	@FechaInicial = '2023-01-01', @FechaFinal = '2025-11-30'   

*/ 

Create Proc spp_BI_UNI_RPT__009__Claves_NoSuministradas__Farmacia_Unidosis 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '2512', @ClaveSSA varchar(100) = '', 
	@Unidad_Beneficiario varchar(1000) = '', 
	@FechaInicial varchar(10) = '2020-01-01', @FechaFinal varchar(10) = '2025-11-30'  
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

Declare 
	-- @IdEmpresa varchar(3), 
	-- @IdEstado varchar(2), 
	@IdFarmacia_Almacen varchar(4), 
	@IdCliente varchar(4), 
	@IdSubCliente varchar(4) 

	Set @sSql = '' 
	Set @IdEmpresa = '' 
	Set @IdEstado = ''  
	Set @IdFarmacia_Almacen  = ''  
	Set @IdCliente = '' 
	Set @IdSubCliente = ''   


------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  



----------------------------------------------------- DATOS FILTRO 
	-- Set @IdFarmacia = '5' 
	------------------------ Tomar los parámetros configurados 
	Select Top 1 @IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdFarmacia_Almacen = IdFarmacia_Almacen, @IdCliente = IdCliente, @IdSubCliente = IdSubCliente 
	From BI_RPT__DTS__Configuracion_Operacion (NoLock) 
		
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 1 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  
	Print @sSql 

	--select * from #vw_Farmacias 
	Delete From #vw_Farmacias Where IdFarmacia = @IdFarmacia_Almacen  
	--select * from #vw_Farmacias 

---		spp_BI_UNI_RPT__009__Claves_NoSuministradas__Farmacia_Unidosis 


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, 
		E.IdCliente, E.IdSubCliente, 
		E.FolioVenta, 
		cast('' as varchar(20)) as IdBeneficiario, 
		cast('' as varchar(300)) as Beneficiario, 
		cast('' as varchar(30)) as Referencia, 
		D.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, 
		P.Presentacion, 				
		
		cast('' as varchar(500)) as FuenteDeFinanciamiento, 
		cast('' as varchar(500)) as IdSubFarmacia, 
		cast('' as varchar(500)) as ClaveLote, 
		cast('' as varchar(10)) as Caducidad, 
		cast('' as varchar(500)) as Laboratorio, 
		cast('' as varchar(500)) as Procedencia, 		

		cast(sum(D.CantidadRequerida) as int) as Cantidad 
	Into #tmp_Claves_NoSurtidas 
	From VentasEnc E (NoLock) 
	Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join vw_ClavesSSA___PRCS P (NoLock) On ( D.IdClaveSSA = P.IdClaveSSA_Sal ) 
	Inner Join BI_UNI_RPT__DTS__ClavesSSA	M ON ( P.ClaveSSA = M.ClaveSSA )  
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		--and P.ClaveSSA like '%' + @ClaveSSA + '%' 
		and D.EsCapturada = 1 	
	Group by 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, 
		E.IdCliente, E.IdSubCliente, 
		E.FolioVenta, 
		D.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.Presentacion 



	Update E Set IdBeneficiario = D.IdBeneficiario 
	From #tmp_Claves_NoSurtidas E 
	Inner Join VentasInformacionAdicional D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 


	Update E Set 
		Beneficiario = (D.ApPaterno + ' ' + D.ApMaterno + ' ' + D.Nombre), 
		Referencia = (case when D.CURP <> '' then D.CURP else D.FolioReferencia end) 
	From #tmp_Claves_NoSurtidas E 
	Inner Join CatBeneficiarios D (NoLock) 
		On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdCliente = D.IdCliente and E.IdSubCliente = D.IdSubCliente and E.IdBeneficiario = D.IdBeneficiario ) 

---	Select * From #vw_CB_CuadroBasico_Claves___NuloMovimiento 
	
	
---------------------		spp_BI_UNI_RPT__009__Claves_NoSuministradas__Farmacia_Unidosis  




----------------------------------------------------- SALIDA FINAL 
	Select 
		-- IdClaveSSA, 
		'Nombre de beneficiario' = Beneficiario, 
		'Número de poliza' = Referencia, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 

		'Presentación' = Presentacion, 
		'Lote' = ClaveLote, 
		'Caducidad' = Caducidad, 
		'Fuente de financiamiento' = FuenteDeFinanciamiento, 
		'Laboratorio' = Laboratorio, 
		'Procedencia' = Procedencia, 


		'Cantidad recetada' = sum(Cantidad),  
		'Cantidad recetada no atendida' = 0  
	From #tmp_Claves_NoSurtidas 
	Group by 
		Beneficiario, Referencia, 
		ClaveSSA, DescripcionClave, Presentacion, 
		ClaveLote, Caducidad, FuenteDeFinanciamiento, Laboratorio, Procedencia 
	Order By   
		ClaveSSA, Referencia      



End 
Go--#SQL 


