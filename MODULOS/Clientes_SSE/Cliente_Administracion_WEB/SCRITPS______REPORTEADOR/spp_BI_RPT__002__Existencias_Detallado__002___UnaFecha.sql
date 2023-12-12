------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha 
Go--#SQL 

Create Proc spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '', @IdJurisdiccion varchar(3) = '', 
	@IdFarmacia varchar(4) = '', @ClaveSSA varchar(20) = '5165', @FuenteDeFinanciamiento varchar(200) = '',
	@Fecha  varchar(10) = '2020-05-19' 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 



------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  


----------------------------------------------------- DATOS FILTRO 
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
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  
	Print @sSql 


--------------------------------------------- FILTRO DE CLAVES 
	Select ClaveSSA, ClaveSSA as ClaveSSA_Fisica, 0 as Relacionada  
	Into #tmpListaClaves 
	From CatClavesSSA_Sales 
	Where ClaveSSA like '%' + @ClaveSSA + '%' 

	Insert Into #tmpListaClaves 
	Select ClaveSSA_Relacionada, ClaveSSA as ClaveSSA_Fisica, 1 as Relacionada  	 
	From vw_Relacion_ClavesSSA_Claves P (NoLock) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 
	Where P.ClaveSSA like '%' + @ClaveSSA + '%' 
		--and 1 = 0 

	-- Select * from #tmpListaClaves 

--		spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha 

--------------------------------------------- FILTRO DE CLAVES 


---------------------		spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha  


----------------------------------------------------- OBTENCION DE DATOS  
	Select DM.IdEmpresa, DM.IdEstado, 
		F.Jurisdiccion, 
		DM.IdFarmacia, 
		F.Farmacia, 
		P.ClaveSSA, DM.IdProducto, DM.CodigoEAN, 
		DM.IdSubFarmacia, DM.ClaveLote, 
		max(DM.Keyx) as Keyx,   
		max(EM.FechaSistema) as FechaUltMovto, 
		0 as Existencia    
	into #tmp_Existencia_A_Una_Fecha___Movtos  
	from MovtosInv_Det_CodigosEAN_Lotes DM (NoLock) 
	Inner Join MovtosInv_Enc EM (NoLock) 
		On ( 
		       DM.IdEmpresa = EM.IdEmpresa and DM.IdEstado = EM.IdEstado 
		       and DM.IdFarmacia = EM.IdFarmacia and DM.FolioMovtoInv = EM.FolioMovtoInv 
     	   ) 	
	Inner Join #vw_Farmacias F (Nolock) On ( EM.IdEstado = F.IdEstado and EM.IdFarmacia = F.IdFarmacia )      	   
    Inner Join vw_Productos_CodigoEAN__PRCS P (NoLock) On ( DM.IdProducto = P.IdProducto and DM.CodigoEAN = P.CodigoEAN )  	   
	Inner Join #tmpListaClaves LC (NoLock) On ( P.ClaveSSA = LC.ClaveSSA )
	Where EM.IdEmpresa = @IdEmpresa and EM.IdEstado = @IdEstado 
		--- and ClaveSSA like '%010.000.0101.00%' 
		--- and P.ClaveSSA like '%' + @ClaveSSA + '%'
		and convert(varchar(10), EM.FechaSistema, 120) <= @Fecha 
	Group by DM.IdEmpresa, DM.IdEstado, 
		F.Jurisdiccion, 
		DM.IdFarmacia, 
		F.Farmacia, 
		P.ClaveSSA, DM.IdProducto, DM.CodigoEAN,	
		DM.IdSubFarmacia, DM.ClaveLote   	
	


			 
	---------------- Obtener datos de Existencias 			 
	Update T Set Existencia = E.Existencia --, FechaUltMovto = E.FechaSistema   
	From #tmp_Existencia_A_Una_Fecha___Movtos T 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes E On ( E.Keyx = T.Keyx )	
	
	
	Select 
		E.Jurisdiccion, 
		E.IdFarmacia, 
		E.Farmacia, 
		P.IdClaveSSA_Sal as IdClaveSSA, 
		E.ClaveSSA as ClaveSSA_Fisica, 
		0 as EsRelacionada, 
		E.ClaveSSA, P.DescripcionClave, 
		cast(P.DescripcionClave as varchar(7000)) as NombreGenerico, 
		P.Presentacion, 
		E.IdSubFarmacia, E.CodigoEAN, E.ClaveLote, 
		convert(varchar(7), C.FechaCaducidad, 120) as FechaCaducidad, 
		datediff(month, getdate(), C.FechaCaducidad) as MesesCaducidad, 
		C.FechaRegistro as FechaRegistro_Lote, 
		P.TipoDeClave, P.TipoDeClaveDescripcion, 
		P.Laboratorio, 
		cast('' as varchar(200)) as FuenteDeFinanciamiento, 
		cast((case when E.ClaveLote like '%*%' then 'CONSIGNACIÓN' else 'INTERMED' end) as varchar(200)) as Procedencia, 		
		(FechaUltMovto) as FechaUltMovto, 
		(E.Existencia) as Existencia, 
		0 as CantidadMinima, 0 as CantidadMaxima, 
		0 as Diferencia_Existencia_vs_CantidadMinima  
	Into #tmp_Existencia_A_Una_Fecha 
	From #tmp_Existencia_A_Una_Fecha___Movtos E 
    Inner Join vw_Productos_CodigoEAN__PRCS P (NoLock) On ( E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN ) 
    Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) On ( P.ClaveSSA = CB.ClaveSSA ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes C (NoLock) 
		On ( E.IdEmpresa = C.IdEmpresa and E.IdEstado = C.IdEstado and E.IdFarmacia = C.IdFarmacia and E.IdSubFarmacia = C.IdSubFarmacia and  
			 E.IdProducto = C.IdProducto and E.CodigoEAN = C.CodigoEAN and E.ClaveLote = C.ClaveLote ) 
    
    
--	Group by 		E.ClaveSSA, P.DescripcionClave, P.Presentacion, P.TipoDeClave, P.TipoDeClaveDescripcion  
----------------------------------------------------- OBTENCION DE DATOS  	
	

	-------------------------------------------------------------------------------------------------------- 
	-------------------------- Reemplazo de claves 
	Update L Set EsRelacionada = 1, ClaveSSA = P.ClaveSSA, IdClaveSSA = P.IdClaveSSA 
	From #tmp_Existencia_A_Una_Fecha L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( --L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente and 
			L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 

	---		spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha  

	Update C Set NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave 
	From #tmp_Existencia_A_Una_Fecha C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where EsRelacionada =  1 
		--and C.ClaveSSA like '%4148%'  

	----Select * 
	----From #tmp_Existencia_A_Una_Fecha C (NoLock) 
	----Where EsRelacionada =  1 
		--and ClaveSSA like '%4148%'  
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 
	
	
---------------------		spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha  


---------------------    FUENTE DE FINANCIAMIENTO 
	Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento 
	From #tmp_Existencia_A_Una_Fecha D 
	Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 




----------------------------------------------------- SALIDA FINAL  
	--Select ClaveSSA, ClaveSSA_Fisica, DescripcionClave From #tmp_Existencia_A_Una_Fecha Group by ClaveSSA, ClaveSSA_Fisica, DescripcionClave

	Select 
		'Ultimo movimiento' = FechaUltMovto, 
		'Jurisdicción' = Jurisdiccion, 
		IdFarmacia, 
		'Farmacia' = Farmacia, 
		'Clave SSA' = ClaveSSA, 

		'Nombre genérico' = NombreGenerico, ---- 
		
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		IdSubFarmacia, 
		'Código EAN' = CodigoEAN, 
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 

		'Meses de vigencia' = MesesCaducidad,  ---- 
		'Fecha de registro de lote' = FechaRegistro_Lote, ---- 

		'Laboratorio' = Laboratorio, 
		'Tipo de insumo' = TipoDeClaveDescripcion,  		
		'Cantidad' = Existencia,   

		'Tipo de rotación' = '', ----

		'Procedencia' = Procedencia, 
		'Fuente de financiamiento' = FuenteDeFinanciamiento,  

		'Fecha de generación' = getdate() ---- 
	From #tmp_Existencia_A_Una_Fecha 
	Where Existencia > 0 
	Order By   
		IdFarmacia, 
		ClaveSSA, CodigoEAN, ClaveLote   


End 
Go--#SQL 


