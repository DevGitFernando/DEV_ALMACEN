----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__04__Programas_SubProgramas' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__04__Programas_SubProgramas 
Go--#SQL 

Create Proc spp_CFG_OP__04__Programas_SubProgramas   
(
	@IdEstadoBase varchar(2) = '21', @IdFarmaciaBase varchar(4) = '4005', @IdEstadoDestino varchar(2) = '21', @IdFarmaciaDestino varchar(4) = '4007' 
) 
As 
Begin 
Set NoCount On 

	
	Set @IdEstadoBase = right('0000' + @IdEstadoBase, 2) 
	Set @IdFarmaciaBase = right('0000' + @IdFarmaciaBase, 4) 
	Set @IdEstadoDestino = right('0000' + @IdEstadoDestino, 2) 
	Set @IdFarmaciaDestino = right('0000' + @IdFarmaciaDestino, 4) 



---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, Status, Actualizado, Dispensacion_CajasCompletas 	
	Into #tmpSubFarmacias 
	From CFG_EstadosFarmaciasProgramasSubProgramas P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
	

	Insert Into CFG_EstadosFarmaciasProgramasSubProgramas (  IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, Status, Actualizado, Dispensacion_CajasCompletas ) 
	Select  IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, Status, Actualizado, Dispensacion_CajasCompletas 
	from #tmpSubFarmacias P 
	Where Not Exists 
		( 
			Select * From CFG_EstadosFarmaciasProgramasSubProgramas C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdCliente = C.IdCliente and P.IdSubCliente = C.IdSubCliente 
				and P.IdPrograma = C.IdPrograma and P.IdSubPrograma = c.IdSubPrograma 
		) 


End 
Go--#SQL 

	

