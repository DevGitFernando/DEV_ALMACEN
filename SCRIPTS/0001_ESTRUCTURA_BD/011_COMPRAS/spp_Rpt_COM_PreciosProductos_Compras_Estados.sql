

If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_COM_PreciosProductos_Compras_Estados' And xType = 'P' )
	Drop Proc spp_Rpt_COM_PreciosProductos_Compras_Estados  
Go--#SQL

Create Procedure spp_Rpt_COM_PreciosProductos_Compras_Estados ( @IdEstado varchar(2) = '21' )  
As 
Begin 
Set NoCount On

	Select 'Id Proveedor' =  L.IdProveedor, 'Nombre' = V.Nombre, 'Clave SSA' = C.ClaveSSA, 'Descripción SSA' = C.DescripcionClave, 
	'Producto' = P.IdProducto, 'Codigo EAN' = L.CodigoEAN, 'Descripción' = P.Descripcion, 
	'Precio Compras' = L.PrecioUnitario, 'Precio Estado' = C.Precio
	From COM_OCEN_ListaDePrecios L (Nolock)
	Left Join vw_Claves_Precios_Asignados C (Nolock) On ( C.IdClaveSSA = L.IdClaveSSA )
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdClaveSSA = P.IdClaveSSA_Sal and L.CodigoEAN = P.CodigoEAN ) 
	Inner Join vw_Proveedores V(NoLock) On( L.IdProveedor = V.IdProveedor )
	Where C.IdEstado = @IdEstado
	Order By L.IdProveedor, C.DescripcionClave, P.Descripcion
				
End 
Go--#SQL 