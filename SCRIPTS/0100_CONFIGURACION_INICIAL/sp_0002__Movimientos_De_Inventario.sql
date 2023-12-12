----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__02__Movimientos' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__02__Movimientos 
Go--#SQL 

Create Proc spp_CFG_OP__02__Movimientos 
(
	@IdEstadoBase varchar(2) = '13', @IdFarmaciaBase varchar(4) = '0011', @IdEstadoDestino varchar(2) = '13', @IdFarmaciaDestino varchar(4) = '0012' 
) 
As 
Begin 
Set NoCount On 

	
	Set @IdEstadoBase = right('0000' + @IdEstadoBase, 2) 
	Set @IdFarmaciaBase = right('0000' + @IdFarmaciaBase, 4) 
	Set @IdEstadoDestino = right('0000' + @IdEstadoDestino, 2) 
	Set @IdFarmaciaDestino = right('0000' + @IdFarmaciaDestino, 4) 




	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, IdTipoMovto_Inv, Consecutivo, Status, Actualizado 		
	Into #tmpSubFarmacias 
	From Movtos_Inv_Tipos_Farmacia P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
	

	Insert Into Movtos_Inv_Tipos_Farmacia (  IdEstado, IdFarmacia, IdTipoMovto_Inv, Consecutivo, Status, Actualizado ) 
	Select  IdEstado, IdFarmacia, IdTipoMovto_Inv, Consecutivo, Status, Actualizado  
	from #tmpSubFarmacias P 
	Where Not Exists 
		( 
			Select * From Movtos_Inv_Tipos_Farmacia C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdTipoMovto_Inv = C.IdTipoMovto_Inv 
		) 


End 
Go--#SQL 

	
