If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes' And xType = 'P' )
	Drop Proc spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes
Go--#SQL

Create Proc spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes   
(  
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmacia varchar(2), 
	@IdProducto varchar(8), @CodigoEAN varchar(30), @ClaveLote varchar(30),   
	@FechaCaduca varchar(10), @IdPersonal varchar(4)
)  
With Encryption 
As   
Begin   
Set NoCount On   
Set DateFormat YMD   
Declare @EsConsignacion bit 

	Set @EsConsignacion = (case when @ClaveLote like '%*%' then 1 else 0 end)
	  
	If Not Exists ( Select * From FarmaciaProductos_CodigoEAN_Lotes (NoLock)   
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia and 
			IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote )   
		Begin   
			Insert Into FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, FechaRegistro, IdPersonal, EsConsignacion )  
		    Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @IdProducto, @CodigoEAN, @ClaveLote, @FechaCaduca, getdate() as FechaCaptura, @IdPersonal, @EsConsignacion
	End   
  
End  
Go--#SQL
