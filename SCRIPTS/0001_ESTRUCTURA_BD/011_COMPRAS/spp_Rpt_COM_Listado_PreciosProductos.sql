

If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_COM_Listado_PreciosProductos' And xType = 'P' )
	Drop Proc spp_Rpt_COM_Listado_PreciosProductos  
Go--#SQL

Create Procedure spp_Rpt_COM_Listado_PreciosProductos  
As 
Begin 
Set NoCount On	


	Set DateFormat YMD	
	Select	'Id Proveedor' = V.IdProveedor, 'Nombre' = V.Nombre, 'Clave SSA' = P.ClaveSSA, 'Descripción SSA' = P.DescripcionSal, 
			'Producto' = P.IdProducto, 'Codigo EAN' = L.CodigoEAN, 'Descripción' = P.Descripcion,
			'Status Precio' = (case when L.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' end),			 
			'Precio' = L.Precio, 'Descuento' = L.Descuento, 'Tasa Iva' = L.TasaIva, 'Iva' = L.Iva, 'Precio Unitario' = L.PrecioUnitario, 
			'Fecha de Registro' = Convert(varchar(10), L.FechaRegistro, 120), 
			'Fecha de Vigencia' = Convert(varchar(10), L.FechaFinVigencia, 120)				   
	From COM_OCEN_ListaDePrecios L (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdClaveSSA = P.IdClaveSSA_Sal and L.CodigoEAN = P.CodigoEAN ) 
	Inner Join vw_Proveedores V(NoLock) On( L.IdProveedor = V.IdProveedor )
	Order By V.IdProveedor, P.DescripcionSal, P.Descripcion
				
End 
Go--#SQL 