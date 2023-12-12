------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_INT_ND_ClavesND_ValidarDatosDeEntrada' and xType = 'P') 
    Drop Proc spp_Proceso_INT_ND_ClavesND_ValidarDatosDeEntrada
Go--#SQL 
  
--  Exec spp_Proceso_INT_ND_ClavesND_ValidarDatosDeEntrada '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Proceso_INT_ND_ClavesND_ValidarDatosDeEntrada 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0003' 
) 
With Encryption 
As 
Begin 
Set NoCount On  
Set DateFormat YMD  

Declare 
	@FolioPedido varchar(8), @sMensaje varchar(1000),			
	@sStatus varchar(1), 
	@iLenEAN smallint, 
	@sCadena varchar(100) 
		
	Set @iLenEAN = 20  
	Set @sCadena = replicate('0', @iLenEAN) 
	
	
-- drop table #tmpClaves_Existentes 
---		spp_Proceso_INT_ND_ClavesND_ValidarDatosDeEntrada 
	
------------------------------- Validar las Claves y Descripciones previamente cargadas 	
	Select ClaveSSA_ND, Descripcion, Descripcion as Descripcion_Nueva, 0 as Diferencia 
	Into #tmpClaves_Existentes  
	From INT_ND_Claves 
	
	Update C Set Descripcion_Nueva = '' 
	From #tmpClaves_Existentes C 

	Update H Set Descripcion_Nueva = C.Descripcion, Diferencia = (case when C.Descripcion <> H.Descripcion then 1 else 0 end)
	From #tmpClaves_Existentes H 
	Inner Join INT_ND_Claves_CargaMasiva C On ( H.ClaveSSA_ND = C.ClaveSSA_ND ) 



------------------------------- Validar las Claves nuevas 
	Select ClaveSSA_ND, Descripcion 
	Into #tmpClaves_Nuevas 
	From INT_ND_Claves_CargaMasiva C (NoLock) 
	Where Not Exists ( Select * From INT_ND_Claves H (NoLock) Where H.ClaveSSA_ND = C.ClaveSSA_ND ) 



------------------------------- Validar las Claves eliminadas  
	Select ClaveSSA_ND, Descripcion 
	Into #tmpClaves_Eliminadas 
	From INT_ND_Claves C (NoLock) 
	Where Not Exists ( Select * From INT_ND_Claves_CargaMasiva H (NoLock) Where H.ClaveSSA_ND = C.ClaveSSA_ND )

---		spp_Proceso_INT_ND_ClavesND_ValidarDatosDeEntrada 


	
	
------------------------------------------- RESULTADOS 
	Select 
		'Clave SSA Nadro' = ClaveSSA_ND, 'Descripción Nadro' = Descripcion, 'Descripción Nueva' = Descripcion_Nueva  
	From #tmpClaves_Existentes (NoLock) 
	Where Diferencia = 1   
	Order by Descripcion 
	
	
	Select 'Clave SSA Nadro' = ClaveSSA_ND, 'Descripción Nadro' = Descripcion 
	From #tmpClaves_Nuevas 	
	Order by Descripcion 


	Select 'Clave SSA Nadro' = ClaveSSA_ND, 'Descripción Nadro' = Descripcion 
	From #tmpClaves_Eliminadas 	
	Order by Descripcion 		
	
	
	
	
	----Select 
	----	Keyx as Renglon, 
	----	'Clave SSA Nadro' = ClaveSSA_ND, 'Código EAN Nadro' = CodigoEAN_ND, 
	----	'Clave SSA' = ClaveSSA, 'Código EAN' = CodigoEAN   
	----From #tmpClaves_Existentes (NoLock) 
	----Where EsClaveSSA_Valida = 0 And CodigoEAN_Existe = 1 
	--------	  -- and 1 = 0 
------------------------------------------- RESULTADOS  


	
	
End  
Go--#SQL 