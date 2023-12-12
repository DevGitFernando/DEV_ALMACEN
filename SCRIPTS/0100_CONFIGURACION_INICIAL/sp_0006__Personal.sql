----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__06__Personal' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__06__Personal 
Go--#SQL 

Create Proc spp_CFG_OP__06__Personal   
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



---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Status, Actualizado 
	Into #tmpSubFarmacias 
	From CatPersonal P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
	

	Insert Into CatPersonal (  IdEstado, IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Status, Actualizado ) 
	Select  IdEstado, IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Status, Actualizado 
	from #tmpSubFarmacias P 
	Where Not Exists 
		( 
			Select * From CatPersonal C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdPersonal = C.IdPersonal 
		) 


End 
Go--#SQL 

	


