------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__008__Claves_Suministradas__Stock' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__008__Claves_Suministradas__Stock 
Go--#SQL 

Create Proc spp_BI_RPT__008__Claves_Suministradas__Stock 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '*', @ClaveSSA varchar(100) = '', 
	@Unidad_Beneficiario varchar(1000) = '', 
	@FechaInicial varchar(10) = '2020-01-08', @FechaFinal varchar(10) = '2020-03-15', 
	@Jurisdiccion_Entrega varchar(100) = ''  
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
	Set @IdFarmacia = '2005' 

	------------------------ Tomar los parámetros configurados 
	Select Top 1 @IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdFarmacia_Almacen = IdFarmacia_Almacen, @IdCliente = IdCliente, @IdSubCliente = IdSubCliente 
	From BI_RPT__DTS__Configuracion_Operacion (NoLock) 

	
	Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia_Almacen, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  
	Print @sSql 

--	select * 	from #vw_Farmacias 


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, F.Jurisdiccion, E.IdCliente, E.IdSubCliente, E.FolioVenta, convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro,  
		I.IdBeneficiario, 
		cast('' as varchar(200)) as Beneficiario, 
		cast('' as varchar(200)) as JurisdiccionBeneficiario, 
		D.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.Presentacion, 
		cast('' as varchar(max)) as NombreGenerico, 
		cast(sum(D.CantidadEntregada + D.CantidadRequerida) as int) as CantidadProgramada, 
		cast(sum(D.CantidadEntregada) as int) as Cantidad  	
	Into #tmp_Claves_NoSurtidas 
	From VentasEnc E (NoLock) 
	Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	Left Join vw_ClavesSSA___PRCS P (NoLock) On ( D.IdClaveSSA = P.IdClaveSSA_Sal ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		and E.IdCliente = @IdCliente and E.IdSubCliente = @IdSubCliente 
		--and P.ClaveSSA like '%' + @ClaveSSA + '%' 
		-- and D.EsCapturada = 1 	
	Group by 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, F.Jurisdiccion, E.IdCliente, E.IdSubCliente, E.FolioVenta, convert(varchar(10), E.FechaRegistro, 120), I.IdBeneficiario, 
		D.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.Presentacion 



	Update E Set Beneficiario = D.Nombre + ' ' + D.ApPaterno + ' ' + D.ApMaterno, JurisdiccionBeneficiario = D.ApPaterno  
	From #tmp_Claves_NoSurtidas E (NoLock) 
	Inner Join CatBeneficiarios D (NoLock) 
		On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdCliente = D.IdCliente and E.IdSubCliente = D.IdSubCliente and E.IdBeneficiario = D.IdBeneficiario ) 


	Update R Set NombreGenerico = C.NombreGenerico 
	From #tmp_Claves_NoSurtidas R   
	Inner Join BI_RPT__DTS__ClavesSSA__CB C (NoLock) On ( R.IdEstado = C.IdEstado and R.IdCliente = C.IdCliente and R.IdSubCliente = C.IdSubCliente and R.ClaveSSA = C.ClaveSSA ) 

---	Select * From #vw_CB_CuadroBasico_Claves___NuloMovimiento 
	
	
---------------------		spp_BI_RPT__008__Claves_Suministradas__Stock  

	

----------------------------------------------------- SALIDA FINAL 
	Set @Jurisdiccion_Entrega = replace(@Jurisdiccion_Entrega, ' ', '%')   

	Select 
		-- IdClaveSSA, 
		'Jurisdicción' = Jurisdiccion,   
		'Unidad de stock' = Beneficiario,   
		'Folio de venta' = FolioVenta,   
		'Fecha de surtimiento' = FechaRegistro,   
		'Clave SSA' = ClaveSSA,   
		'Descripción Clave SSA' = DescripcionClave,    
		'Presentación' = Presentacion,   
		'Nombre genérico' = NombreGenerico, 
		'Cantidad programada' = sum(CantidadProgramada),   
		'Cantidad surtida' = sum(CantidadProgramada) 
	From #tmp_Claves_NoSurtidas 
	Where JurisdiccionBeneficiario like '%' + @Jurisdiccion_Entrega + '%' 
	Group by 
		Jurisdiccion, Beneficiario, FolioVenta, FechaRegistro, ClaveSSA, DescripcionClave, Presentacion, NombreGenerico 
	Order By   
		ClaveSSA, Beneficiario 



	--Select 
	--	'Jurisdicción' = 'Jurisdiccion', 
	--	'Unidad de stock' = 'Beneficiario', 
	--	'Folio de venta' = 'FolioVenta', 
	--	'Fecha de surtimiento' = getdate(), 
	--	'Clave SSA' = 'ClaveSSA', 
	--	'Descripción Clave SSA' = 'DescripcionClave', 
	--	'Presentación' = 'Presentacion', 
	--	'Cantidad programada' = 0, 
	--	'Cantidad surtida' = 0 



End 
Go--#SQL 


