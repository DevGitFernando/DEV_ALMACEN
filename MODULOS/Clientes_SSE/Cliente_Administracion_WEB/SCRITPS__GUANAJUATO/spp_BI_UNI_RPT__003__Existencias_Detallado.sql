------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__003__Existencias_Detallado' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__003__Existencias_Detallado 
Go--#SQL 

Create Proc spp_BI_UNI_RPT__003__Existencias_Detallado 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '88', 
	@Fecha  varchar(10) = '2015-10-05' 
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
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia  
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  
	Print @sSql 



---------------------		spp_BI_UNI_RPT__003__Existencias_Detallado  


----------------------------------------------------- OBTENCION DE DATOS  
	Select DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, P.ClaveSSA, DM.IdProducto, DM.CodigoEAN, 
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
    Inner Join BI_UNI_RPT__DTS__ClavesSSA	M ON ( P.ClaveSSA = M.ClaveSSA )      
	Where EM.IdEmpresa = @IdEmpresa and EM.IdEstado = @IdEstado 
		--- and ClaveSSA like '%010.000.0101.00%' 
		and convert(varchar(10), EM.FechaSistema, 120) <= @Fecha 
	Group by DM.IdEmpresa, DM.IdEstado, DM.IdFarmacia, P.ClaveSSA, DM.IdProducto, DM.CodigoEAN,	
		DM.IdSubFarmacia, DM.ClaveLote   	
	
			 
	---------------- Obtener datos de Existencias 			 
	Update T Set Existencia = E.Existencia --, FechaUltMovto = E.FechaSistema   
	From #tmp_Existencia_A_Una_Fecha___Movtos T 
	Inner Join MovtosInv_Det_CodigosEAN_Lotes E On ( E.Keyx = T.Keyx )	
	
	
	Select 
		E.ClaveSSA, P.DescripcionClave, P.Presentacion, 
		E.IdSubFarmacia, E.ClaveLote, 
		convert(varchar(7), C.FechaCaducidad, 120) as FechaCaducidad, 
		P.TipoDeClave, P.TipoDeClaveDescripcion, 
		P.Laboratorio, 
		cast('' as varchar(200)) as FuenteDeFinanciamiento, 
		cast('' as varchar(200)) as Procedencia, 		
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
	
	
--		Select * From #tmp_Existencia_A_Una_Fecha 
	
	
---------------------		spp_BI_UNI_RPT__003__Existencias_Detallado  



----------------------------------------------------- SALIDA FINAL  
--	Select * From #tmp_Existencia_A_Una_Fecha___Movtos 

	Select 
		'Ultimo movimiento' = FechaUltMovto, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 
		'Laboratorio' = Laboratorio, 
		'Tipo de insumo' = TipoDeClaveDescripcion,  		
		'Cantidad' = Existencia,   
		'Procedencia' = Procedencia, 
		'Fuente de financiamiento' = FuenteDeFinanciamiento 
	From #tmp_Existencia_A_Una_Fecha 
	Order By   
		ClaveSSA   



End 
Go--#SQL 


