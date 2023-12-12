------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__001__Existencias_vs_Maximos_y_Minimos__001___General' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__001__Existencias_vs_Maximos_y_Minimos__001___General 
Go--#SQL 

Create Proc spp_BI_RPT__001__Existencias_vs_Maximos_y_Minimos__001___General 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '3188', 
	@Fecha  varchar(10) = '2018-12-05' 

) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '10' 
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



---------------------		spp_BI_RPT__001__Existencias_vs_Maximos_y_Minimos__001___General  


--------------------------------------------------------- OBTENCION DE DATOS  
----	Select DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, P.ClaveSSA, DM.IdProducto, DM.CodigoEAN, 
----		max(DM.Keyx) as Keyx,   
----		max(EM.FechaSistema) as FechaUltMovto, 
----		0 as Existencia    
----	into #tmp_Existencia_A_Una_Fecha___Movtos  
----	from MovtosInv_Det_CodigosEAN DM (NoLock) 
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
----	Group by DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, P.ClaveSSA, DM.IdProducto, DM.CodigoEAN  	
	
			 
----	---------------- Obtener datos de Existencias 			 
----	Update T Set Existencia = E.Existencia --, FechaUltMovto = E.FechaSistema   
----	From #tmp_Existencia_A_Una_Fecha___Movtos T 
----	Inner Join MovtosInv_Det_CodigosEAN E On ( E.Keyx = T.Keyx )	
	
	
	Select 
		E.IdEmpresa, E.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia, 
		P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.ClaveSSA as ClaveSSA_Fisica, 0 as EsRelacionada,  
		P.DescripcionClave, cast(P.DescripcionClave as varchar(7500)) as NombreGenerico, P.Presentacion, 
		P.TipoDeClave, P.TipoDeClaveDescripcion, 
		(getdate()) as FechaUltMovto, 
		sum(E.Existencia) as Existencia, 
		0 as CantidadMinima, 0 as CantidadMaxima, 
		0 as Diferencia_Existencia_vs_CantidadMinima  
	Into #tmp_Existencia_A_Una_Fecha 
	From FarmaciaProductos_CodigoEAN_Lotes E 
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )      	
    Inner Join vw_Productos_CodigoEAN__PRCS P (NoLock) On ( E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN ) 
    Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) On ( P.ClaveSSA = CB.ClaveSSA ) 
	Group by 
		E.IdEmpresa, E.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia, 
		P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, P.Presentacion, P.TipoDeClave, P.TipoDeClaveDescripcion  
	
	
	Update E Set CantidadMinima = M.CantidadMinima, CantidadMaxima = M.CantidadMaxima 
	From #tmp_Existencia_A_Una_Fecha E 	
	Inner Join BI_RPT__DTS__ClavesSSA__Maximos_Minimos	M ON ( E.ClaveSSA = M.ClaveSSA ) 
	
	-- Update E Set CantidadMinima = (M.CantidadMinima / M.MesesContenidos), CantidadMaxima = (M.CantidadMaxima / M.MesesContenidos)  
	Update E Set CantidadMinima = (M.CantidadMinima / (M.MesesContenidos * 1.0)), CantidadMaxima = (M.CantidadMaxima / 1.0)  
	-- Update E Set CantidadMinima = M.CantidadMinima, CantidadMaxima = M.CantidadMaxima 
	From #tmp_Existencia_A_Una_Fecha E 	
	Inner Join BI_RPT__DTS__Farmacia_ClavesSSA__Maximos_Minimos	M 
		ON ( E.IdEstado = M.IdEstado and E.IdFarmacia = E.IdFarmacia and  E.ClaveSSA = M.ClaveSSA ) 

	Update E Set Diferencia_Existencia_vs_CantidadMinima = Existencia - CantidadMinima 
	From #tmp_Existencia_A_Una_Fecha E 
	
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
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 
	
	
---------------------		spp_BI_RPT__001__Existencias_vs_Maximos_y_Minimos__001___General  



----------------------------------------------------- SALIDA FINAL  
--	Select * From #tmp_Existencia_A_Una_Fecha___Movtos 

	Select 
		'Jurisdicción' = Jurisdiccion, 
		'Farmacia' = Farmacia, 		
		'Ultimo movimiento' = FechaUltMovto, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Tipo de insumo' = TipoDeClaveDescripcion,  
		'Cantidad' = Existencia,   
		'Cantidad miníma' = CantidadMinima, 
		'Cantidad máxima' = CantidadMaxima, 
		'Diferencia entre Cantidad y Cantidad miníma' = Diferencia_Existencia_vs_CantidadMinima  	
	From #tmp_Existencia_A_Una_Fecha 
	Order By   
		IdJurisdiccion, IdFarmacia, 
		ClaveSSA   



End 
Go--#SQL 


