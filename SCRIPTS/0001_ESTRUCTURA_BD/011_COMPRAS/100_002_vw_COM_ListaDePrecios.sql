------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_OCEN_ListaDePreciosClaves' and xType = 'V' ) 
	Drop View vw_COM_OCEN_ListaDePreciosClaves 
Go--#SQL

Create View vw_COM_OCEN_ListaDePreciosClaves 
As 

	Select L.IdProveedor, P.Nombre, 
		   C.IdClaveSSA_Sal as IdClaveSSA, C.ClaveSSA, C.ClaveSSA_Base, C.DescripcionSal, C.DescripcionSal as DescripcionClave, 
		   L.CodigoEAN, C.Descripcion as DescripcionProducto, 
	       L.Precio, L.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario as Importe, 
		   convert(varchar(10), L.FechaRegistro, 120) as FechaRegistro, 
		   convert(varchar(10), L.FechaFinVigencia, 120) as FechaFinVigencia, 
	       L.Status as StatusPrecio, (case when L.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' end) as StatusPrecioAux 
	From COM_OCEN_ListaDePrecios L (NoLock) 
	Inner Join vw_Proveedores P (NoLock) On ( L.IdProveedor = P.IdProveedor ) 
	Inner Join vw_Productos_CodigoEAN C (NoLock) On ( L.CodigoEAN = C.CodigoEAN ) 
	
Go--#SQL 

------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_COM_OCEN_ListaDePreciosProductos' and xType = 'V' ) 
	Drop View vw_COM_OCEN_ListaDePreciosProductos 
Go--#SQL

Create View vw_COM_OCEN_ListaDePreciosProductos 
As 

	Select L.IdProveedor, P.Nombre, 
		   C.IdClaveSSA_Sal as IdClaveSSA, C.ClaveSSA, C.DescripcionSal as DescripcionClave, 
		   L.CodigoEAN, C.Descripcion as DescripcionProducto, 
	       L.Precio, L.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario as Importe, 
		   convert(varchar(10), L.FechaRegistro, 120) as FechaRegistro, 
		   convert(varchar(10), L.FechaFinVigencia, 120) as FechaFinVigencia, 
	       L.Status as StatusPrecio, (case when L.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' end) as StatusPrecioAux 	        
	From COM_OCEN_ListaDePrecios L (NoLock) 
	Inner Join vw_Proveedores P (NoLock) On ( L.IdProveedor = P.IdProveedor ) 
	Inner Join vw_Productos_CodigoEAN C (NoLock) On ( L.CodigoEAN = C.CodigoEAN ) 
	
Go--#SQL 

--	select * from COM_OCEN_ListaDePreciosContado 
	
--	Select * from vw_Proveedores 

--Select top 10 * from vw_Productos_CodigoEAN 
