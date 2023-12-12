----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__05__Servicios_Areas' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__05__Servicios_Areas 
Go--#SQL 

Create Proc spp_CFG_OP__05__Servicios_Areas   
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
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, IdServicio, IdArea, Status, Actualizado 	
	Into #tmpSubFarmacias 
	From CatServicios_Areas_Farmacias P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
	

	Insert Into CatServicios_Areas_Farmacias (  IdEstado, IdFarmacia, IdServicio, IdArea, Status, Actualizado) 
	Select  IdEstado, IdFarmacia, IdServicio, IdArea, Status, Actualizado
	from #tmpSubFarmacias P 
	Where Not Exists 
		( 
			Select * From CatServicios_Areas_Farmacias C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdServicio = C.IdServicio and P.IdArea = C.IdArea 
		) 


End 
Go--#SQL 

	


