


------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_COM_CFG_Productos_Compras' and xType = 'P' )
   Drop Proc spp_Mtto_COM_CFG_Productos_Compras 
Go--#SQL

Create Proc spp_Mtto_COM_CFG_Productos_Compras ( @IdEstado varchar(2), @IdProducto varchar(8), @CodigoEAN varchar(30), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 


	If @Status = 'C'
		Set @IdProducto = ( Select IdProducto From vw_Productos_CodigoEAN (NoLock)  Where CodigoEAN = @CodigoEAN )
	
	
	If Not Exists ( Select * From COM_CFG_Productos_Compras  (NoLock) 
					Where IdEstado = @IdEstado and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN  ) 
	   Insert Into COM_CFG_Productos_Compras  ( IdEstado, IdProducto, CodigoEAN, Status ) 
	   Select @IdEstado, @IdProducto, @CodigoEAN, @Status 
	Else 
	   Update COM_CFG_Productos_Compras  Set Status = @Status 
	   Where IdEstado = @IdEstado and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN 

End 
Go--#SQL