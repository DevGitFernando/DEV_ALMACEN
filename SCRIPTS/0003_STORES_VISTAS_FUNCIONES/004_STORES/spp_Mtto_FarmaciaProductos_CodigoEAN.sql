If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FarmaciaProductos_CodigoEAN' And xType = 'P' )
	Drop Proc spp_Mtto_FarmaciaProductos_CodigoEAN
Go--#SQL

Create Proc spp_Mtto_FarmaciaProductos_CodigoEAN   
(  
 @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdProducto varchar(8), @CodigoEAN varchar(30)    
)  
With Encryption 
As   
Begin   
Set NoCount On   
  
 If Not Exists ( Select * From FarmaciaProductos_CodigoEAN (NoLock)   
     Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN )   
    Begin   
     Insert Into FarmaciaProductos_CodigoEAN ( IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN )  
     Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdProducto, @CodigoEAN   
    End   
  
End
Go--#SQL	

