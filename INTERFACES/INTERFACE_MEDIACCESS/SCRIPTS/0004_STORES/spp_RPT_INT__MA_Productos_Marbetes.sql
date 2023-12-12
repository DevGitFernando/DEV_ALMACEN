If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_RPT_INT__MA_Productos_Marbetes' and xType = 'P')
    Drop Proc spp_RPT_INT__MA_Productos_Marbetes
Go--#SQL	

  
Create Proc spp_RPT_INT__MA_Productos_Marbetes ( @IdEstado Varchar(2)= '09', @IdFarmacia Varchar(4)= '0012' )
With Encryption 
As
Begin
Set NoCount On

	Select P.IdEstado, P.IdFarmacia, P.IdProducto
	Into #Productos
	From FarmaciaProductos P (NoLock)
	Where P.IdEstado = @IdEstado And P.IdFarmacia = @IdFarmacia And (Existencia - ExistenciaEnTransito) > 0

	Delete P
	From #Productos P (NoLock)
	Inner Join INT__MA_Productos_Marbetes F (NoLock) On (P.IdEstado = F.IdEstado And P.IdFarmacia = F.IdFarmacia And P.IdProducto = F.IdProducto) 

	Insert Into INT__MA_Productos_Marbetes (IdEstado, IdFarmacia, IdProducto)
	Select IdEstado, IdFarmacia, IdProducto From #Productos P (NoLock)


	Select P.IdProducto, V.CodigoEAN, Descripcion, ClaveSSA, DescripcionSal, PrecioVenta
	From INT__MA_Productos_Marbetes P (NoLock)
	Inner Join vw_Productos_CodigoEAN V (NoLock) On (P.IdProducto = V.IdProducto)
	Where MarbeteActualizado = 0 And P.IdEstado = @IdEstado And P.IdFarmacia = @IdFarmacia
		
End
Go--#SQL