------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__009__Claves_Suministradas__Farmacia' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__009__Claves_Suministradas__Farmacia 
Go--#SQL 

Create Proc spp_BI_RPT__009__Claves_Suministradas__Farmacia 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '2019', @ClaveSSA varchar(100) = '', 
	@Unidad_Beneficiario varchar(1000) = '', 
	@FechaInicial varchar(10) = '2020-03-01', @FechaFinal varchar(10) = '2020-03-31', 
	@NombreBeneficiario varchar(200) = '', @NumeroDeReceta varchar(200) = ''  
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max), 
	@sFiltro varchar(max) 

Declare 
	-- @IdEmpresa varchar(3), 
	-- @IdEstado varchar(2), 
	@IdFarmacia_Almacen varchar(4), 
	@IdCliente varchar(4), 
	@IdSubCliente varchar(4) 

	Set @sSql = '' 
	Set @sFiltro = '' 
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
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  
	Print @sSql 

	Delete From #vw_Farmacias Where IdFarmacia = @IdFarmacia_Almacen  



----------------------------------------------------- OBTENCION DE DATOS  
	Set @ClaveSSA =  replace(@ClaveSSA, ' ', '%')   
	Set @NombreBeneficiario = replace(@NombreBeneficiario, ' ', '%')   
	Set @NumeroDeReceta = replace(@NumeroDeReceta, ' ', '%')   

	Select 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdCliente, E.IdSubCliente, 
		E.FolioVenta, 
		I.IdBeneficiario, cast('' as varchar(200)) as Beneficiario, cast('' as varchar(20)) as NumeroDeReferencia, 
		cast(I.NumReceta as varchar(200)) as NumeroDeReceta, 
		D.IdClaveSSA, P.ClaveSSA, 
		P.ClaveSSA as ClaveSSA_Fisica, 0 as EsRelacionada, 		
		P.DescripcionClave, P.Presentacion, 
		cast('' as varchar(max)) as NombreGenerico, 

		--cast(sum(D.CantidadEntregada + D.CantidadRequerida) as int) as CantidadRecetada,   		
		--cast(sum(D.CantidadEntregada) as int) as Cantidad  

		cast(sum(D.CantidadEntregada) as numeric(14,4)) as CantidadEntregada,   
		cast(0 as numeric(14,4)) as CantidadDevuelta,   
		cast(sum(D.CantidadRequerida) as numeric(14,4)) as CantidadRequerida,  


		cast(sum(D.CantidadEntregada + D.CantidadRequerida) as numeric(14,4)) as CantidadRecetada,   		
		cast(sum(D.CantidadEntregada) as numeric(14,4)) as Cantidad  
	Into #tmp_Claves_NoSurtidas 
	From VentasEnc E (NoLock) 
	Inner Join VentasEstadisticaClavesDispensadas D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	--Left Join vw_ClavesSSA___PRCS P (NoLock) On ( D.IdClaveSSA = P.IdClaveSSA_Sal ) 
	Left Join vw_ClavesSSA_Sales P (NoLock) On ( D.IdClaveSSA = P.IdClaveSSA_Sal ) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )  
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa  
		and P.ClaveSSA like '%' + @ClaveSSA + '%' 
		-- and D.EsCapturada = 1 	
	Group by 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.IdCliente, E.IdSubCliente, 
		E.FolioVenta, 
		I.IdBeneficiario, cast(I.NumReceta as varchar(200)), 
		D.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, P.Presentacion 



------------------------------------------------- INFORMACION ADICIONAL  
	Update E Set Beneficiario = D.Nombre + ' ' + D.ApPaterno + ' ' + D.ApMaterno, NumeroDeReferencia = D.FolioReferencia  
	From #tmp_Claves_NoSurtidas E (NoLock) 
	Inner Join CatBeneficiarios D (NoLock) 
		On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.IdCliente = D.IdCliente and E.IdSubCliente = D.IdSubCliente and E.IdBeneficiario = D.IdBeneficiario ) 


	Update R Set NombreGenerico = C.NombreGenerico 
	From #tmp_Claves_NoSurtidas R   
	Inner Join BI_RPT__DTS__ClavesSSA__CB C (NoLock) On ( R.IdEstado = C.IdEstado and R.IdCliente = C.IdCliente and R.IdSubCliente = C.IdSubCliente and R.ClaveSSA = C.ClaveSSA ) 
------------------------------------------------- INFORMACION ADICIONAL 



------------------------------------------------- BUSQUEDA FINAL 
	Select top 0 * 
	Into #tmp_Claves_NoSurtidas__Final 
	From #tmp_Claves_NoSurtidas 
---	Where Beneficiario like '%' + @NombreBeneficiario + '%' or NumeroDeReceta like '%' + @NumeroDeReceta + '%'

	Set @sFiltro = '' 

	If @NombreBeneficiario <> ''  and @NumeroDeReceta <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' ( Beneficiario like ' + char(39) + '%' + char(39) + ' + ' + char(39) + @NombreBeneficiario + char(39) + ' + ' + char(39) + '%' + char(39) + ' ) ' 	  
			Set @sFiltro = @sFiltro + ' and ( NumeroDeReceta like ' + char(39) + '%' + char(39) + ' + ' + char(39) + @NumeroDeReceta + char(39) + ' + ' + char(39) + '%' + char(39) + ' ) '  
		End 
	Else 
		Begin 
			If @NombreBeneficiario <> ''  and @NumeroDeReceta = ''
				Set @sFiltro = @sFiltro + ' ( Beneficiario like ' + char(39) + '%' + char(39) + ' + ' + char(39) + @NombreBeneficiario + char(39) + ' + ' + char(39) + '%' + char(39) + ' ) ' 
		  
			If @NumeroDeReceta <> '' and @NombreBeneficiario = '' 
				Set @sFiltro = @sFiltro + ' ( NumeroDeReceta like ' + char(39) + '%' + char(39) + ' + ' + char(39) + @NumeroDeReceta + char(39) + ' + ' + char(39) + '%' + char(39) + ' ) '  
	End 



	If @sFiltro <> '' 
	   Set @sFiltro = 'Where ' + @sFiltro


	Set @sSql = 
		'Insert Into #tmp_Claves_NoSurtidas__Final ' + char(13) + 
		'Select * ' + char(13) + 
		'From #tmp_Claves_NoSurtidas ' + char(13) + @sFiltro 
	Exec(@sSql) 
	Print @sSql 

------------------------------------------------- BUSQUEDA FINAL 



------------------------------------------------- DEVOLUCIONES 
	
	Select 
		F.IdEmpresa, F.IdEstado, F.IdFarmacia, 
		F.FolioVenta, P.ClaveSSA, P.DescripcionClave, 
		(sum(D.Cant_Devuelta) / max(P.ContenidoPaquete) )as Cant_Devuelta  
	Into #tmp_Claves_NoSurtidas__Devoluciones 
	From -- #tmp_Claves_NoSurtidas__Final F (NoLock) 
	( 
		Select 
			IdEmpresa, IdEstado, IdFarmacia, FolioVenta
		From #tmp_Claves_NoSurtidas__Final 
		Group by  
			IdEmpresa, IdEstado, IdFarmacia, FolioVenta
	) F 
	Inner Join VentasDet D (NoLock) 
		On ( F.IdEmpresa = D.IdEmpresa and F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia and F.FolioVenta = D.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
	Where D.Cant_Devuelta >  0 
	Group by 
		F.IdEmpresa, F.IdEstado, F.IdFarmacia, 
		F.FolioVenta, P.ClaveSSA, P.DescripcionClave 


	Update F Set CantidadDevuelta = D.Cant_Devuelta 
	From #tmp_Claves_NoSurtidas__Final F (NoLock) 
	Inner Join #tmp_Claves_NoSurtidas__Devoluciones D (NoLock) 
		On ( F.IdEmpresa = D.IdEmpresa and F.IdEstado = D.IdEstado and F.IdFarmacia = D.IdFarmacia and F.FolioVenta = D.FolioVenta and F.ClaveSSA = D.ClaveSSA ) 


	Update F Set 
		--CantidadRecetada = CantidadEntregada - CantidadDevuelta, 
		Cantidad = CantidadEntregada - CantidadDevuelta, 
		CantidadRecetada = ((CantidadEntregada - CantidadDevuelta) + CantidadRequerida) 
	From #tmp_Claves_NoSurtidas__Final F (NoLock) 


--		spp_BI_RPT__009__Claves_Suministradas__Farmacia 


------------------------------------------------- DEVOLUCIONES 


---	Select * From #vw_CB_CuadroBasico_Claves___NuloMovimiento 
	
	
---------------------		spp_BI_RPT__009__Claves_Suministradas__Farmacia  


	-------------------------------------------------------------------------------------------------------- 
	-------------------------- Reemplazo de claves 
	Update L Set EsRelacionada = 1, ClaveSSA = P.ClaveSSA, IdClaveSSA = P.IdClaveSSA 
	From #tmp_Claves_NoSurtidas__Final L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( --L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente and 
			L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 

	---		spp_BI_RPT__006__Beneficiario_Atendido__Detalle  

	Update C Set NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave 
	From #tmp_Claves_NoSurtidas__Final C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where EsRelacionada =  1 
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 
	
		

----------------------------------------------------- SALIDA FINAL 
	Select 
		'Nombre beneficiario' = Beneficiario, 
		'Número de póliza' = NumeroDeReferencia, 
		'Folio de venta' = FolioVenta, 
		'Número de receta' = NumeroDeReceta, 
		--'Clave SSA Física' = ClaveSSA_Fisica, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Nombre genérico' = NombreGenerico, 
		'Cantidad recetada' = sum(CantidadRecetada),  
		'Cantidad dispensada' = sum(Cantidad) 
	From #tmp_Claves_NoSurtidas__Final 
	Group by 
		Beneficiario, 
		FolioVenta, 
		NumeroDeReferencia, NumeroDeReceta, 
		--ClaveSSA_Fisica, 
		ClaveSSA, DescripcionClave, Presentacion, NombreGenerico 
	Order By   
		Beneficiario, 
		FolioVenta, 
		NumeroDeReferencia, ClaveSSA   



End 
Go--#SQL 


