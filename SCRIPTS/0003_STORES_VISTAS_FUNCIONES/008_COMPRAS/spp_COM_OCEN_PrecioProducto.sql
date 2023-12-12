If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_COM_OCEN_PrecioProductos' And xType = 'P' )
	Drop Proc spp_COM_OCEN_PrecioProductos 
Go--#SQL

Create Procedure dbo.spp_COM_OCEN_PrecioProductos 
( @IdProveedor varchar(4), @IdClaveSSA varchar(4), @CodigoEAN varchar(30) ) 
As 
Begin 
Set NoCount On 

	Select IdProveedor, IdClaveSSA, CodigoEAN, Precio, Descuento, TasaIva, Iva, PrecioUnitario as Importe, FechaRegistro, FechaFinVigencia, 
		 cast((case when datediff(dd, L.FechaFinVigencia, getdate()) > 0 then 1 else 0 end) as bit ) as EsVigente    
	From COM_OCEN_ListaDePrecios L (NoLock) 
	Where IdProveedor = @IdProveedor and IdClaveSSA = @IdClaveSSA and CodigoEAN = @CodigoEAN 
	
End 
Go--#SQL  


If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_COM_OCEN_PrecioProductosProveedor' And xType = 'P' )
	Drop Proc spp_COM_OCEN_PrecioProductosProveedor  
Go--#SQL

Create Procedure dbo.spp_COM_OCEN_PrecioProductosProveedor ( @IdProveedor varchar(4) = '0001' ) 
As 
Begin 
Set NoCount On 

	Select L.IdClaveSSA, P.ClaveSSA, L.CodigoEAN, 
		   (case when L.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' end) as StatusPrecio, 
		   P.Descripcion as DescProducto, 
		   L.Precio, L.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario as Importe, 
		   Convert(varchar(10), L.FechaRegistro, 120) as FechaRegistro, 
		   Convert(varchar(10), L.FechaFinVigencia, 120) as FechaFinVigencia  
		   
		   -- cast((case when datediff(dd, L.FechaFinVigencia, getdate()) > 0 then 1 else 0 end) as bit ) as EsVigente   
		   
	From COM_OCEN_ListaDePrecios L (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdClaveSSA = P.IdClaveSSA_Sal and L.CodigoEAN = P.CodigoEAN ) 
	Where IdProveedor = @IdProveedor 
	
--	select top 10 * from vw_Productos_CodigoEAN 
			
End 
Go--#SQL 