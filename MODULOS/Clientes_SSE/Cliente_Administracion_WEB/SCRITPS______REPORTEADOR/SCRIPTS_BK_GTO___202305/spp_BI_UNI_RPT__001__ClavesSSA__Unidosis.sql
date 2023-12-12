------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__001__ClavesSSA__Unidosis' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__001__ClavesSSA__Unidosis 
Go--#SQL 

Create Proc spp_BI_UNI_RPT__001__ClavesSSA__Unidosis  
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


---------------------		spp_BI_UNI_RPT__001__ClavesSSA__Unidosis  


----------------------------------------------------- OBTENCION DE DATOS  
	Select M.Consecutivo, (M.ClaveSSA + 'U') as ClaveSSA, P.DescripcionClave, P.Presentacion, TipoDeClaveDescripcion  
    Into #tmp_CB 
    From BI_UNI_RPT__DTS__ClavesSSA	M  (NoLock) 
    Inner Join vw_CB_CuadroBasico_Claves__PRCS P On ( P.ClaveSSA = M.ClaveSSA )  		
----------------------------------------------------- OBTENCION DE DATOS  	
	
	
--		Select * From #tmp_CB 
	
	
---------------------		spp_BI_UNI_RPT__001__ClavesSSA__Unidosis  



----------------------------------------------------- SALIDA FINAL  
--	Select * From #tmp_Existencia_A_Una_Fecha___Movtos 

	Select 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 
		'Tipo de insumo' = TipoDeClaveDescripcion   
		-- 'Cantidad' = Existencia,   
		-- 'Cantidad miníma' = CantidadMinima, 
		-- 'Cantidad máxima' = CantidadMaxima, 
		-- 'Diferencia entre Cantidad y Cantidad miníma' = Diferencia_Existencia_vs_CantidadMinima  	
	From #tmp_CB 
	Order By   
		ClaveSSA   



End 
Go--#SQL 


