----------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_CartasFaltantes' and xType = 'V' ) 
   Drop View vw_COM_CartasFaltantes 
Go--#SQL 
 	
Create View vw_COM_CartasFaltantes 
With Encryption 
As 
	Select 
		C.Folio, C.IdProveedor, P.Nombre as Proveedor, C.FechaRegistro, C.Observaciones, C.Documento, C.NombreDocto, C.Status
	From COM_CartasFaltantes C (NoLock)	 
	Inner Join CatProveedores P (NoLock) On ( C.IdProveedor = P.IdProveedor ) 
	
Go--#SQL 


--		Select * From vw_COM_CartasFaltantes (Nolock)
---------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_CartasFaltantes_Detalles' and xType = 'V' ) 
   Drop View vw_COM_CartasFaltantes_Detalles 
Go--#SQL 
 	
Create View vw_COM_CartasFaltantes_Detalles 
With Encryption 
As 
	Select 
		E.Folio, E.IdProveedor, E.Proveedor, E.FechaRegistro, E.Observaciones, E.Documento, 
		D.ClaveSSA, C.DescripcionSal as Descripcion
	From vw_COM_CartasFaltantes E (NoLock)	 
	Inner Join COM_CartasFaltantes_Detalles D (NoLock) On ( E.Folio = D.Folio ) 
	Inner Join vw_ClavesSSA_Sales C (Nolock) On ( D.ClaveSSA = C.ClaveSSA )

Go--#SQL

--		Select * From vw_COM_CartasFaltantes_Detalles (Nolock)
-------------------------------------------------------------------------------------------------- 
