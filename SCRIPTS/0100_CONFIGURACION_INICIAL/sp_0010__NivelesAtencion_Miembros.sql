----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__10__NivelesAtencion_Miembros' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__10__NivelesAtencion_Miembros
Go--#SQL 

Create Proc spp_CFG_OP__10__NivelesAtencion_Miembros
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



	 --IdEstado, IdCliente, IdNivel, IdFarmacia, FechaUpdate, Status, Actualizado
	Select @IdEstadoDestino as IdEstado, IdCliente, IdNivel, @IdFarmaciaDestino as IdFarmacia, GetDate() As FechaUpdate, Status, 0 As Actualizado 
	Into #tmpNivelesAtencion_Miembros
	From CFG_CB_NivelesAtencion_Miembros P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
		and Status = 'A' 
	

	Insert Into CFG_CB_NivelesAtencion_Miembros ( IdEstado, IdCliente, IdNivel, IdFarmacia, FechaUpdate, Status, Actualizado ) 
	Select IdEstado, IdCliente, IdNivel, IdFarmacia, FechaUpdate, Status, Actualizado
	from #tmpNivelesAtencion_Miembros P 
	Where Not Exists 
		( 
			Select * From CFG_CB_NivelesAtencion_Miembros C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdCliente = C.IdCliente And P.IdNivel = C.IdNivel
		) 


End 
Go--#SQL 

	