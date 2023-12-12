If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' And xType = 'P' )
	Drop Proc spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
Go--#SQL

Create Proc spp_Mtto_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones   
(  
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmacia varchar(2), 
	@IdProducto varchar(8), @CodigoEAN varchar(30), @ClaveLote varchar(30),   
	@IdPasillo int, @IdEstante int, @IdEntrepano int 
	
)  
With Encryption 
As   
Begin   
Set NoCount On   
Set DateFormat YMD   
Declare @EsConsignacion bit 

	Set @EsConsignacion = (case when @ClaveLote like '%*%' then 1 else 0 end)

    Exec spp_Mtto_Registro_Ubicaciones @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, @IdEstante, @IdEntrepano 

	  
	If Not Exists ( Select * From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock)   
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia and 
			IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 
			and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepano )   
		Begin   
			Insert Into FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
			    (  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
			       IdPasillo, IdEstante, IdEntrepaño, Existencia, Status, Actualizado )  
		    Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @IdProducto, @CodigoEAN, @ClaveLote, @EsConsignacion, 
		           @IdPasillo, @IdEstante, @IdEntrepano, 0, 'A', '0'
	End   
   
End  
Go--#SQL
