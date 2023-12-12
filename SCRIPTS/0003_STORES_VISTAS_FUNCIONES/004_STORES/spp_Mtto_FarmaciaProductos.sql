If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FarmaciaProductos' And xType = 'P' )
	Drop Proc spp_Mtto_FarmaciaProductos
Go--#SQL
  
Create Proc spp_Mtto_FarmaciaProductos   
(  
 @IdEmpresa varchar(5), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdProducto varchar(8)   
)  
With Encryption 
As   
Begin   
Set NoCount On   
  
 If Not Exists ( Select * From FarmaciaProductos (NoLock)   
     Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdProducto = @IdProducto )   
    Begin   
     Insert Into FarmaciaProductos ( IdEmpresa, IdEstado, IdFarmacia, IdProducto )  
     Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdProducto   
    End   
  
End
Go--#SQL