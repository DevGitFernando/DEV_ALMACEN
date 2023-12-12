If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_COM_OCEN_PrecioProductos_Claves' And xType = 'P' )
	Drop Proc spp_COM_OCEN_PrecioProductos_Claves 
Go--#SQL

Create Procedure dbo.spp_COM_OCEN_PrecioProductos_Claves 
( @IdProveedor varchar(4), @IdClaveSSA varchar(4)) 
As 
Begin 
Set NoCount On 

	Select IdProveedor, IdClaveSSA, Precio, Descuento, TasaIva, Iva, PrecioUnitario, FechaRegistro, FechaFinVigencia, 
		 cast((case when datediff(dd, L.FechaFinVigencia, getdate()) > 0 then 1 else 0 end) as bit ) as EsVigente    
	From COM_OCEN_ListaDePrecios_Claves L (NoLock) 
	Where IdProveedor = @IdProveedor and IdClaveSSA = @IdClaveSSA  
	
End 
Go--#SQL  


If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_COM_OCEN_PrecioProductosProveedor_Claves' And xType = 'P' )
	Drop Proc spp_COM_OCEN_PrecioProductosProveedor_Claves  
Go--#SQL

Create Procedure dbo.spp_COM_OCEN_PrecioProductosProveedor_Claves ( @IdProveedor varchar(4) = '0001' ) 
As 
Begin 
Set NoCount On 

	Select L.IdClaveSSA, S.ClaveSSA,  
		   (case when L.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' end) as StatusPrecio, 
		   S.DescripcionSal as DescProducto, 
		   L.Precio, L.Descuento, L.TasaIva, L.Iva, L.PrecioUnitario, 
		   Convert(varchar(10), L.FechaRegistro, 120) as FechaRegistro, 
		   Convert(varchar(10), L.FechaFinVigencia, 120) as FechaFinVigencia  		   
		   
	From COM_OCEN_ListaDePrecios_Claves L (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( L.IdClaveSSA = S.IdClaveSSA_Sal) 
	Where IdProveedor = @IdProveedor 
			
End 
Go--#SQL 