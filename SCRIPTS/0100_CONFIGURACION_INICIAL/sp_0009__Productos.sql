----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__09__ProductosEstado' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__09__ProductosEstado  
Go--#SQL 

Create Proc spp_CFG_OP__09__ProductosEstado   
(
	@IdEstado varchar(2) = '13'  
) 
As 
Begin 
Set NoCount On 

	
	Set @IdEstado = right('0000' + @IdEstado, 2)  



---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEstado as IdEstado, IdProducto, Status, Actualizado
	Into #tmpSubFarmacias 
	From CatProductos P (NoLock) 
	

	Insert Into CatProductos_Estado (   IdEstado, IdProducto, Status, Actualizado ) 
	Select   IdEstado, IdProducto, Status, Actualizado
	from #tmpSubFarmacias P 
	Where Not Exists 
		( 
			Select * From CatProductos_Estado C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdProducto = C.IdProducto  
		) 


End 
Go--#SQL 

	


