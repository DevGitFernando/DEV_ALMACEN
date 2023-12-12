------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__008__Claves_Suministradas__Stock____02_Ventas_Pedidos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__008__Claves_Suministradas__Stock____02_Ventas_Pedidos 
Go--#SQL 

Create Proc spp_BI_RPT__008__Claves_Suministradas__Stock____02_Ventas_Pedidos 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '3005', @ClaveSSA varchar(100) = '',  
	@ClaveUnidad_Beneficiario varchar(20) = '', 
	@Unidad_Beneficiario varchar(1000) = '', 
	@FechaInicial varchar(10) = '2021-11-01', @FechaFinal varchar(10) = '2021-11-28', 
	@IdJurisdiccion_Entrega varchar(3) = '*', 
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
	Set @ClaveUnidad_Beneficiario = ltrim(rtrim(@ClaveUnidad_Beneficiario)) 
	Set @ClaveUnidad_Beneficiario = (case when @ClaveUnidad_Beneficiario = '*' then '' else @ClaveUnidad_Beneficiario end) 


------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  



----------------------------------------------------- DATOS FILTRO 
	--Set @IdFarmacia = '2005' 

	------------------------ Tomar los parámetros configurados 
	Select Top 1 @IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdFarmacia_Almacen = IdFarmacia_Almacen, @IdCliente = IdCliente, @IdSubCliente = IdSubCliente 
	From BI_RPT__DTS__Configuracion_Operacion (NoLock) 

	
	Select IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, Estado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsAlmacen = 1 and EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10) 
	   
	----If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	----Begin 
	----   --Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia_Almacen, 4) + char(39) + char(13) + char(10) 	   
	----   Set @sSql = @sSql + ' and IdFarmacia in ( ' + char(39) + right('0000' + '2005', 4) + char(39) + ', ' + + char(39) + right('0000' + '3005', 4) + char(39) + ' ) ' + char(13) + char(10) 	   
	----End 

	Exec(@sSql)  
	Print @sSql 

--	select * 	from #vw_Farmacias 


--------------------------------------------------	LISTADO DE FOLIOS VS SURTIDOS 
	Select PE.IdEmpresa, PE.IdEstado, PE.IdFarmacia, 
		PE.FechaRegistro, PE.FechaEntrega, 
		PS.FolioPedido, PS.FolioSurtido, PS.FolioTransferenciaReferencia as FolioVenta 
	Into #tmp___Pedidos_Cedis_Enc_Surtido 
	From Pedidos_Cedis_Enc_Surtido PS (NoLock) 
		-- On ( E.IdEmpresa = PS.IdEmpresa and E.IdEstado = PS.IdEstado and E.IdFarmacia = PS.IdFarmacia and 'SV' + E.FolioVenta = PS.FolioTransferenciaReferencia )   
	Inner Join Pedidos_Cedis_Enc PE (NoLock) 
		On ( PS.IdEmpresa = PE.IdEmpresa and PS.IdEstado = PE.IdEstado and PS.IdFarmacia = PE.IdFarmacia and PS.FolioPedido = PE.FolioPedido ) 
	inner Join #vw_Farmacias F (Nolock) On ( PE.IdEstado = F.IdEstado and PE.IdFarmacia = F.IdFarmacia )  
	Where 
		--convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		convert(varchar(10), PE.FechaEntrega, 120) between @FechaInicial and @FechaFinal and 		
		PE.IdEmpresa = @IdEmpresa 
		and PE.IdCliente = @IdCliente and PE.IdSubCliente = @IdSubCliente 
		and ( PE.IdBeneficiario like '%' + @ClaveUnidad_Beneficiario + '%' ) 
		--and PS.FolioSurtido = 2611  

	--select * from #tmp___Pedidos_Cedis_Enc_Surtido  

--		spp_BI_RPT__008__Claves_Suministradas__Stock____02_Ventas_Pedidos 

	--select count(*) from #tmp___Pedidos_Cedis_Enc_Surtido 



---------------------		spp_BI_RPT__008__Claves_Suministradas__Stock____02_Ventas_Pedidos  

----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEmpresa, E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia, F.Jurisdiccion, E.IdCliente, E.IdSubCliente, 
		PS.FolioSurtido, 
		PS.FolioPedido, 
		E.FolioVenta, convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro,  
		I.IdBeneficiario, 
		cast('' as varchar(200)) as Beneficiario, 
		cast('' as varchar(200)) as IdJurisdiccionBeneficiario, 
		cast('' as varchar(200)) as JurisdiccionBeneficiario, 
		P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.Presentacion, 
		cast('' as varchar(max)) as NombreGenerico, 
		--cast(sum(D.CantidadEntregada + D.CantidadRequerida) as int) as CantidadProgramada, 
		0 as CantidadProgramada, 
		cast(sum(D.CantidadVendida) as int) as Cantidad  	
	Into #tmp_Claves_NoSurtidas 
	From VentasEnc E (NoLock) 
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta )  
	Inner Join #tmp___Pedidos_Cedis_Enc_Surtido PS (NoLock) 
		On ( E.IdEmpresa = PS.IdEmpresa and E.IdEstado = PS.IdEstado and E.IdFarmacia = PS.IdFarmacia and 'SV' + E.FolioVenta = PS.FolioVenta )   
	--Inner Join Pedidos_Cedis_Enc_Surtido PS (NoLock) 
	--	On ( E.IdEmpresa = PS.IdEmpresa and E.IdEstado = PS.IdEstado and E.IdFarmacia = PS.IdFarmacia and 'SV' + E.FolioVenta = PS.FolioTransferenciaReferencia )   
	--Inner Join Pedidos_Cedis_Enc PE (NoLock) 
	--	On ( PS.IdEmpresa = PE.IdEmpresa and PS.IdEstado = PE.IdEstado and PS.IdFarmacia = PE.IdFarmacia and PS.FolioPedido = PE.FolioPedido ) 
	Left Join vw_Productos_CodigoEAN__PRCS P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where 
		--1 = 0 and 
		--convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		convert(varchar(10), PS.FechaEntrega, 120) between @FechaInicial and @FechaFinal and 		
		E.IdEmpresa = @IdEmpresa 
		and E.IdCliente = @IdCliente and E.IdSubCliente = @IdSubCliente 
		--and P.ClaveSSA like '%' + @ClaveSSA + '%' 
		-- and D.EsCapturada = 1 	
	Group by 
		E.IdEmpresa, E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia, F.Jurisdiccion, E.IdCliente, E.IdSubCliente, 
		E.FolioVenta, 
		PS.FolioSurtido, 
		PS.FolioPedido, 
		convert(varchar(10), E.FechaRegistro, 120), 
		I.IdBeneficiario, 
		P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, P.Presentacion 


---------------------		spp_BI_RPT__008__Claves_Suministradas__Stock____02_Ventas_Pedidos  



	------------------------- INFORMAACION COMPLEMENTARIA 
	Update E Set Beneficiario = D.Nombre + ' ' + D.ApPaterno + ' ' + D.ApMaterno, IdJurisdiccionBeneficiario = IdJurisdiccion, JurisdiccionBeneficiario = D.ApPaterno  
	From #tmp_Claves_NoSurtidas E (NoLock) 
	Inner Join CatBeneficiarios D (NoLock) 
		On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdCliente = D.IdCliente and E.IdSubCliente = D.IdSubCliente and E.IdBeneficiario = D.IdBeneficiario ) 

	Update E Set JurisdiccionBeneficiario = J.Descripcion 
	From #tmp_Claves_NoSurtidas E (NoLock) 
	Inner Join CatJurisdicciones J (NoLock) On ( E.IdEstado = J.IdEstado and E.IdJurisdiccionBeneficiario = J.IdJurisdiccion ) 


	if ( @IdJurisdiccion_Entrega <> '*' and @IdJurisdiccion_Entrega <> '' ) 
	Begin 
		Delete from #tmp_Claves_NoSurtidas Where IdJurisdiccionBeneficiario = @IdJurisdiccion_Entrega 
	End 


	Update R Set NombreGenerico = C.NombreGenerico 
	From #tmp_Claves_NoSurtidas R   
	Inner Join BI_RPT__DTS__ClavesSSA__CB C (NoLock) On ( R.IdEstado = C.IdEstado and R.IdCliente = C.IdCliente and R.IdSubCliente = C.IdSubCliente and R.ClaveSSA = C.ClaveSSA ) 
	------------------------- INFORMAACION COMPLEMENTARIA 



---	Select * From #vw_CB_CuadroBasico_Claves___NuloMovimiento 
	
	
---------------------		spp_BI_RPT__008__Claves_Suministradas__Stock____02_Ventas_Pedidos  

	

----------------------------------------------------- SALIDA FINAL 
	Set @Jurisdiccion_Entrega = replace(@Jurisdiccion_Entrega, ' ', '%')    
	Set @Unidad_Beneficiario = replace(@Unidad_Beneficiario, ' ', '%') 

	Select 
		-- IdClaveSSA, 
		'Cve Origen' = IdFarmacia, 
		'Origen' = Farmacia, 
		'IdJurisdiccion' = IdJurisdiccionBeneficiario, 
		'Jurisdicción' = Jurisdiccion,   
		'Unidad de stock' = Beneficiario,   
		'Folio de pedido' = FolioPedido, 
		'Folio de surtido' = FolioSurtido, 
		'Folio de venta' = FolioVenta,   
		'Fecha de surtimiento' = FechaRegistro,   
		'Clave SSA' = ClaveSSA,   
		'Descripción Clave SSA' = DescripcionClave,    
		'Presentación' = Presentacion,   
		'Nombre genérico' = NombreGenerico,  
		'Cantidad programada' = sum(CantidadProgramada),   
		--'Cantidad programada' = 0,  
		'Cantidad surtida' = sum(Cantidad) 
	From #tmp_Claves_NoSurtidas 
	--Where JurisdiccionBeneficiario like '%' + @Jurisdiccion_Entrega + '%' 
	Where ( JurisdiccionBeneficiario like '%' + @Jurisdiccion_Entrega + '%' and Beneficiario like '%' + @Unidad_Beneficiario + '%') 
		--and FolioSurtido = 2611 
	Group by 
		IdFarmacia, Farmacia, 
		IdJurisdiccionBeneficiario, 
		Jurisdiccion, Beneficiario, 
		FolioVenta, 
		FolioSurtido, 
		FolioPedido, 		
		FechaRegistro, ClaveSSA, DescripcionClave, Presentacion, NombreGenerico 
	Order By   
		FolioPedido, FolioSurtido, FolioVenta, 
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


