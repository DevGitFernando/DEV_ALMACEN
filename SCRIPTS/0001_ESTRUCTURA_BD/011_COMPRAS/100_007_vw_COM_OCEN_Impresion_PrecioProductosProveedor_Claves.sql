If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_COM_OCEN_Impresion_PrecioProductosProveedor_Claves' And xType = 'V' )
	Drop View vw_COM_OCEN_Impresion_PrecioProductosProveedor_Claves
Go--#SQL

Create View dbo.vw_COM_OCEN_Impresion_PrecioProductosProveedor_Claves 
With Encryption 
As 	
	Select	Distinct 
			V.IdProveedor, V.Nombre, 
			L.IdClaveSSA, C.ClaveSSA_Base, 
			C.DescripcionSal, 
			(case when L.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' end) as StatusPrecio, 
			L.Precio, L.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario, 
			Convert(varchar(10), L.FechaRegistro, 120) as FechaRegistro, 
			Convert(varchar(10), L.FechaFinVigencia, 120) as FechaFinVigencia, 
			GetDate() as FechaDeImpresion	   
	From COM_OCEN_ListaDePrecios_Claves L (NoLock)	
	Inner Join vw_ClavesSSA_Sales C (NoLock) On ( L.IdClaveSSA = C.IdClaveSSA_Sal ) 
	Inner Join vw_Proveedores V(NoLock) On( L.IdProveedor = V.IdProveedor )
			
Go--#SQL 