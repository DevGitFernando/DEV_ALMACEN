-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_Proceso_Pedidos_CargaMasiva_000_Validar_Datos_De_Entrada' and xType = 'P' ) 
   Drop Proc sp_Proceso_Pedidos_CargaMasiva_000_Validar_Datos_De_Entrada
Go--#SQL 

Create Proc sp_Proceso_Pedidos_CargaMasiva_000_Validar_Datos_De_Entrada
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 


	Update P
	Set P.IdClaveSSA = S.IdClaveSSA_Sal	
	From Pedidos_CargaMasiva P 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On (P.ClaveSSA = S.ClaveSSA)
	
	
	Update P
	Set P.IdClaveSSA = S.IdClaveSSA_Sal	
	From Pedidos_CargaMasiva P
	Inner Join vw_ClavesSSA_Sales S (NoLock) On (P.ClaveSSA = S.ClaveSSA_Base)
	Where P.IdClaveSSA = ''
	
	
	Update P
	Set P.IdClaveSSA = S.IdClaveSSA_Sal	
	From Pedidos_CargaMasiva P
	Inner Join vw_ClavesSSA_Sales S (NoLock) On '0'+ P.ClaveSSA = replace(S.ClaveSSA_Base,'.', '' )
	Where P.IdClaveSSA = ''

----------------------- SALIDA FINAL	

	Select * From Pedidos_CargaMasiva P (NoLock) Where P.IdClaveSSA = ''
	
	Select * From Pedidos_CargaMasiva P (NoLock) Where P.Costo < 0 
	
	Select * From Pedidos_CargaMasiva P (NoLock) Where P.Cantidad <= 0
	
	Select * From Pedidos_CargaMasiva P (NoLock) Where P.Iva < 0
	
	Select * From Pedidos_CargaMasiva P (NoLock) Where LEN(P.DescripcionClaveSSA) = 0
	
----------------------- SALIDA FINAL	

End 
Go--#SQL 



