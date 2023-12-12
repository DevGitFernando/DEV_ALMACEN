------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__002__Existencias_Detallado__001___General' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__002__Existencias_Detallado__001___General 
Go--#SQL 

Create Proc spp_BI_RPT__002__Existencias_Detallado__001___General 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '', @ClaveSSA varchar(20) = '5165', @FuenteDeFinanciamiento varchar(200) = '',
	@Fecha  varchar(10) = '2021-04-20' 
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@IdEmpresa_Proceso varchar(3), 
	@IdEstado_Proceso varchar(2), 
	@IdFarmacia_Proceso varchar(4), 
	@IdCliente_Proceso varchar(4),   
	@IdSubCliente_Proceso varchar(4)  

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 



------------------------------------------ Obtener la informacion General de la Operacion 
	Select Top 1 @IdEmpresa_Proceso = IdEmpresa, @IdEstado_Proceso = IdEstado, @IdCliente_Proceso = IdCliente, @IdSubCliente_Proceso = IdSubCliente 
	From BI_RPT__DTS__Configuracion_Operacion (NoLock) 


	Set @IdEmpresa_Proceso = right('00000000' + @IdEmpresa_Proceso, 3 ) 
	Set @IdEstado_Proceso = right('00000000' + @IdEstado_Proceso, 2 ) 
	Set @IdCliente_Proceso = right('00000000' + @IdCliente_Proceso, 4 ) 
	Set @IdSubCliente_Proceso = right('00000000' + @IdSubCliente_Proceso, 4 ) 
------------------------------------------ Obtener la informacion General de la Operacion 



------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '10' 
------------------------------------------ Generar tablas de catalogos  


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, EsAlmacen  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, EsAlmacen  ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, EsAlmacen  ' + char(13) + char(10) + 
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




	------------------------------------------ Determinar los Productos a filtrar 
	Select P.*, 0 as Relacionada 
	Into #vw_Productos_CodigoEAN____Filtro 
	From vw_Productos_CodigoEAN P 
	Where P.ClaveSSA like '%' + replace(@ClaveSSA, ' ', '%') + '%' 

	Insert Into #vw_Productos_CodigoEAN____Filtro 
	Select distinct P.*, 1 as Relacionada  
	From vw_Productos_CodigoEAN P (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( C.IdEstado = @IdEstado_Proceso and C.IdCliente = @IdCliente_Proceso and C.IdSubCliente = @IdSubCliente_Proceso and P.ClaveSSA = C.ClaveSSA_Relacionada )
	Where C.ClaveSSA like '%' + replace(@ClaveSSA, ' ', '%') + '%' 
	and Not Exists 
	( 
		Select * 
		From #vw_Productos_CodigoEAN____Filtro F 
		Where P.IdProducto = F.IdProducto and P.CodigoEAN = F.CodigoEAN 
	) 

	--Select * from #vw_Productos_CodigoEAN____Filtro 


	--		spp_BI_RPT__002__Existencias_Detallado__001___General


	----@IdEmpresa_Proceso varchar(3), 
	----@IdEstado_Proceso varchar(2), 
	----@IdFarmacia_Proceso varchar(4), 
	----@IdCliente_Proceso varchar(4),   
	----@IdSubCliente_Proceso varchar(4)  
	------------------------------------------ Determinar los Productos a filtrar 


	-- select * from #vw_Farmacias  


---------------------		spp_BI_RPT__002__Existencias_Detallado__001___General  


--------------------------------------------------------- OBTENCION DE DATOS  
----	Select DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, P.ClaveSSA, DM.IdProducto, DM.CodigoEAN, 
----		DM.IdSubFarmacia, DM.ClaveLote, 
----		max(DM.Keyx) as Keyx,   
----		max(EM.FechaSistema) as FechaUltMovto, 
----		0 as Existencia    
----	into #tmp_Existencia_A_Una_Fecha___Movtos  
----	from MovtosInv_Det_CodigosEAN_Lotes DM (NoLock) 
----	Inner Join MovtosInv_Enc EM (NoLock) 
----		On ( 
----		       DM.IdEmpresa = EM.IdEmpresa and DM.IdEstado = EM.IdEstado 
----		       and DM.IdFarmacia = EM.IdFarmacia and DM.FolioMovtoInv = EM.FolioMovtoInv 
----     	   ) 	
----	Inner Join #vw_Farmacias F (Nolock) On ( EM.IdEstado = F.IdEstado and EM.IdFarmacia = F.IdFarmacia )      	   
----    Inner Join vw_Productos_CodigoEAN__PRCS P (NoLock) On ( DM.IdProducto = P.IdProducto and DM.CodigoEAN = P.CodigoEAN )  	   
----	Where EM.IdEmpresa = @IdEmpresa and EM.IdEstado = @IdEstado 
----		--- and ClaveSSA like '%010.000.0101.00%' 
----		and convert(varchar(10), EM.FechaSistema, 120) <= @Fecha 
----	Group by DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, P.ClaveSSA, DM.IdProducto, DM.CodigoEAN,	
----		DM.IdSubFarmacia, DM.ClaveLote   	
	
			 
----	---------------- Obtener datos de Existencias 			 
----	Update T Set Existencia = E.Existencia --, FechaUltMovto = E.FechaSistema   
----	From #tmp_Existencia_A_Una_Fecha___Movtos T 
----	Inner Join MovtosInv_Det_CodigosEAN_Lotes E On ( E.Keyx = T.Keyx )	

		Select Top 0
			L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Existencia, L.FechaCaducidad, L.FechaRegistro, L.IdPersonal,
			L.IdSubFarmacia, L.EsConsignacion, L.ExistenciaEnTransito, L.CostoPromedio, L.UltimoCosto
		Into #FarmaciaProductos_CodigoEAN_Lotes
		From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		Where 1 = 0

		Insert Into #FarmaciaProductos_CodigoEAN_Lotes
				(IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, ClaveLote, Existencia, FechaCaducidad, FechaRegistro, IdPersonal,
				IdSubFarmacia, EsConsignacion, ExistenciaEnTransito, CostoPromedio, UltimoCosto)
		Select 
			L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, Sum(U.Existencia) As Existencia, L.FechaCaducidad, L.FechaRegistro, L.IdPersonal,
			L.IdSubFarmacia, L.EsConsignacion, Sum(U.ExistenciaEnTransito) As ExistenciaEnTransito, L.CostoPromedio, L.UltimoCosto
		From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		Inner Join #vw_Productos_CodigoEAN____Filtro FL (NoLock) On ( L.IdProducto = FL.IdProducto and L.CodigoEAN = FL.CodigoEAN ) 
		Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock)
			On ( L.IdEmpresa = U.IdEmpresa And L.IdEstado = U.IdEstado And L.IdFarmacia = U.IdFarmacia And
				 L.IdProducto = U.IdProducto And L.CodigoEAN = U.CodigoEAN And L.IdSubFarmacia = U.IdSubFarmacia And L.ClaveLote = U.ClaveLote )
		Inner Join #vw_Farmacias F (NoLock) On ( L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And EsAlmacen = 1 )
		Where Not Exists (Select *
						  From BI_RPT__CatFarmacias_Ubicaciones_Excluidas E (NOLock)
						  Where U.IdEmpresa = E.IdEmpresa And U.IdEstado = E.IdEstado And U.IdFarmacia = E.IdFarmacia And
						  U.IdPasillo = E.IdPasillo And U.IdEstante = E.IdEstante And U.IdEntrepaño = E.IdEntrepaño And Status = 'A' )
		Group BY L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote,
				 L.FechaCaducidad, L.FechaRegistro, L.IdPersonal, L.IdSubFarmacia, L.EsConsignacion, L.CostoPromedio, L.UltimoCosto

		Insert Into #FarmaciaProductos_CodigoEAN_Lotes
				(IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, ClaveLote, Existencia, FechaCaducidad, FechaRegistro, IdPersonal,
				IdSubFarmacia, EsConsignacion, ExistenciaEnTransito, CostoPromedio, UltimoCosto)
		Select 
			L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.Existencia, L.FechaCaducidad, L.FechaRegistro, L.IdPersonal,
			L.IdSubFarmacia, L.EsConsignacion, L.ExistenciaEnTransito, L.CostoPromedio, L.UltimoCosto
		From FarmaciaProductos_CodigoEAN_Lotes L (NoLock)
		Inner Join #vw_Farmacias F (NoLock) On ( L.IdEstado = F.IdEstado And L.IdFarmacia = F.IdFarmacia And EsAlmacen = 0 )	
		Inner Join #vw_Productos_CodigoEAN____Filtro FL (NoLock) On ( L.IdProducto = FL.IdProducto and L.CodigoEAN = FL.CodigoEAN )  
			
	

	Select 
		F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia,
		P.IdClaveSSA_Sal as IdClaveSSA, 
		P.ClaveSSA, 
		P.ClaveSSA as ClaveSSA_Fisica, 
		0 as EsRelacionada, 
		cast(P.DescripcionClave as varchar(5000)) as NombreGenerico, 
		P.DescripcionClave, 
		cast('' as varchar(500)) as Presentacion, 
		E.IdSubFarmacia, E.CodigoEAN, E.ClaveLote, 
		-- cast('' as varchar(200)) as IdSubFarmacia, cast('' as varchar(200)) as ClaveLote, 		
		convert(varchar(7), E.FechaCaducidad, 120) as FechaCaducidad, 
		datediff(month, getdate(), E.FechaCaducidad) as MesesCaducidad, 
		E.FechaRegistro as FechaRegistro_Lote, 
		P.TipoDeClave, P.TipoDeClaveDescripcion, 
		P.Laboratorio, 
		cast('' as varchar(200)) as FuenteDeFinanciamiento, 
		cast((case when E.ClaveLote like '%*%' then 'CONSIGNACIÓN' else 'INTERMED' end) as varchar(200)) as Procedencia, 		
		(getdate()) as FechaUltMovto, 
		(E.Existencia - E.ExistenciaEnTransito) as Existencia, 
		0 as CantidadMinima, 0 as CantidadMaxima, 
		0 as Diferencia_Existencia_vs_CantidadMinima  
	Into #tmp_Existencia_A_Una_Fecha 
	From #FarmaciaProductos_CodigoEAN_Lotes E (NoLock) 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )      	
    Inner Join vw_Productos_CodigoEAN__PRCS P (NoLock) On ( E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN ) 
    Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) On ( P.ClaveSSA = CB.ClaveSSA ) 
    --Inner Join FarmaciaProductos_CodigoEAN_Lotes C (NoLock) 
	--	On ( E.IdEmpresa = C.IdEmpresa and E.IdEstado = C.IdEstado and E.IdFarmacia = C.IdFarmacia and E.IdSubFarmacia = C.IdSubFarmacia and  
	--		 E.IdProducto = C.IdProducto and E.CodigoEAN = C.CodigoEAN and E.ClaveLote = C.ClaveLote ) 
	--Where P.ClaveSSA like '%' + @ClaveSSA + '%'   -----20200507.1735    
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

	---		spp_BI_RPT__002__Existencias_Detallado__001___General  

	Update C Set NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave 
	From #tmp_Existencia_A_Una_Fecha C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where EsRelacionada =  1 
		--and C.ClaveSSA like '%4148%'  

	--Select * 
	--From #tmp_Existencia_A_Una_Fecha C (NoLock) 
	--Where EsRelacionada =  1 
	--	and ClaveSSA like '%4148%'  
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 

	
--		Select * From #tmp_Existencia_A_Una_Fecha 
	
	
---------------------		spp_BI_RPT__002__Existencias_Detallado__001___General  

---------------------    FUENTE DE FINANCIAMIENTO 
	Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento 
	From #tmp_Existencia_A_Una_Fecha D 
	Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 


	---------------------- ASIGNAR INFORMACION SEGUN REQUERIMIENTOS DEL CLIENTE 
	If @IdEstado = 13 
	Begin 

		Update D Set FuenteDeFinanciamiento = C.Financiamiento  
		From #tmp_Existencia_A_Una_Fecha D 
		Inner Join vw_FACT_FuentesDeFinanciamiento_Claves_PRCS C (NoLock) On ( D.ClaveSSA = C.ClaveSSA )
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

		Update D Set NombreGenerico = C.NombreGenerico, Presentacion = C.Presentacion 
		From #tmp_Existencia_A_Una_Fecha D 
		Inner Join BI_RPT__DTS__ClavesSSA__CB C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

	End 


----------------------------------------------------- SALIDA FINAL  
--	Select * From #tmp_Existencia_A_Una_Fecha___Movtos 

	---------------------------------------------------------------------------------------------------------------- 
	---------------- Poner descripciones de acuerdo a las Mascaras 


	Select 
		'Ultimo movimiento' = FechaUltMovto, 
		'Jurisdicción' = Jurisdiccion, 
		IdFarmacia, 
		'Farmacia' = Farmacia, 
		'Clave SSA' = ClaveSSA, 
		'Nombre genérico' = NombreGenerico, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		IdSubFarmacia, 
		'Código EAN' = CodigoEAN, 
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 
		'Meses de vigencia' = MesesCaducidad, 
		'Fecha de registro de lote' = FechaRegistro_Lote, 
		'Laboratorio' = Laboratorio, 
		'Tipo de insumo' = TipoDeClaveDescripcion,  		
		'Cantidad' = Existencia,   
		'Tipo de rotación' = '', 
		'Procedencia' = Procedencia, 
		'Fuente de financiamiento' = FuenteDeFinanciamiento,  ---- tomar la primera que aparesca según CB 
		'Fecha de generación' = getdate() 
	From #tmp_Existencia_A_Una_Fecha 
	Where Existencia > 0 and ClaveSSA like '%' + @ClaveSSA + '%' 
	Order By   
		IdFarmacia, 
		ClaveSSA, CodigoEAN, ClaveLote  



End 
Go--#SQL 


