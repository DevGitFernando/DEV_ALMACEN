---------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ClavesSSA_Proveedores_Externos' and xType = 'V' ) 
   Drop view vw_ClavesSSA_Proveedores_Externos  
Go--#SQL     

Create view vw_ClavesSSA_Proveedores_Externos 
As 
	Select 
		P.IdEstado, P.IdProveedor, P.Nombre as Proveedor, P.Status as StatusProveedor, 
		C.TipoDeClave, C.TipoDeClaveDescripcion as Insumo, 
		C.IdClaveSSA_sal as IdClaveSSA, C.ClaveSSA, C.DescripcionClave, C.Presentacion, 
		CP.Status as Status_Clave    
	From CatProveedores_Externos P (NoLock) 
	Inner Join CatProveedores_Externos_Claves CP (NoLock) On ( P.IdEstado = CP.IdEstado and P.IdProveedor = CP.IdProveedor )  
	Inner Join vw_ClavesSSA_Sales C On ( C.IdClaveSSA_Sal = CP.IdClaveSSA )  

Go--#SQL     	
	
----	select * 	from vw_ClavesSSA_Proveedores_Externos 
	
	