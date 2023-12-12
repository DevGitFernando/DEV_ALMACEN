
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'vw_CatProductos_Estado' And xType = 'V' )
	Drop View vw_CatProductos_Estado 
Go--#SQL	


Create View vw_CatProductos_Estado 
With Encryption 
As
	Select a.IdEstado, a.Nombre, 
		( Case When IsNull(b.Status, 'C') = 'A' Then 1 Else 0 End ) as Status, IsNull(b.IdProducto, '') as IdProducto
	From CatEstados a (NoLock) 
	Left Join CatProductos_Estado b (NoLock) On( a.IdEstado = b.IdEstado )
Go--#SQL	


---------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_CatProductos_Estado' And xType = 'P' )
	Drop Proc spp_CatProductos_Estado 
Go--#SQL	


Create Proc spp_CatProductos_Estado ( @IdProducto varchar(8) = '' ) 
With Encryption 
As
Begin 

	Select * 
	From 
	( 
	Select a.IdEstado, a.Nombre as Estado, 
		( Case When IsNull(b.Status, 'C') = 'A' Then 1 Else 0 End ) as Status, 
		IsNull(b.IdProducto, '') as IdProducto, IsNull(P.Descripcion, '') as Descripcion, 
		IsNull(P.ClaveSSA, '') as ClaveSSA, IsNull(P.DescripcionSal, '') as DescripcionSal 				
	From CatEstados a (NoLock) 
	Left Join CatProductos_Estado b (NoLock) On( a.IdEstado = b.IdEstado and b.IdProducto = @IdProducto  ) 
	Left Join vw_Productos P (NoLock) On ( b.IdProducto = P.IdProducto ) 	
	) as Estados 
	Order By Status Desc, IdEstado
	
End 
Go--#SQL
 
