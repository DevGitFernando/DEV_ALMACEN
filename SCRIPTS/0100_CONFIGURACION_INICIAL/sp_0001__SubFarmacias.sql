----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__01__SubFarmacias' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__01__SubFarmacias 
Go--#SQL 

Create Proc spp_CFG_OP__01__SubFarmacias 
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




	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, IdSubFarmacia, Descripcion, Status, Actualizado 
	Into #tmpSubFarmacias 
	From CatFarmacias_SubFarmacias P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
	

	Insert Into CatFarmacias_SubFarmacias ( IdEstado, IdFarmacia, IdSubFarmacia, Descripcion, Status, Actualizado ) 
	Select IdEstado, IdFarmacia, IdSubFarmacia, Descripcion, Status, Actualizado  
	from #tmpSubFarmacias P 
	Where Not Exists 
		( 
			Select * From CatFarmacias_SubFarmacias C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdSubFarmacia = C.IdSubFarmacia 
		) 


End 
Go--#SQL 

	