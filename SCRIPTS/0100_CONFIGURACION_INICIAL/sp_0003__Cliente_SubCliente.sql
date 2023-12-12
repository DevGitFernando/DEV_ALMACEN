----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__03__Clientes_SubClientes' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__03__Clientes_SubClientes 
Go--#SQL 

Create Proc spp_CFG_OP__03__Clientes_SubClientes  
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
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, IdCliente, Status, Actualizado	
	Into #tmpSubFarmacias 
	From CFG_EstadosFarmaciasClientes P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase 
	

	Insert Into CFG_EstadosFarmaciasClientes (  IdEstado, IdFarmacia, IdCliente, Status, Actualizado ) 
	Select  IdEstado, IdFarmacia, IdCliente, Status, Actualizado
	from #tmpSubFarmacias P 
	Where Not Exists 
		( 
			Select * From CFG_EstadosFarmaciasClientes C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdCliente = C.IdCliente 
		) 



---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, IdCliente, IdSubCliente, Status, Actualizado	
	Into #tmpSubFarmacias_Detalles  
	From CFG_EstadosFarmaciasClientesSubClientes P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
	

	Insert Into CFG_EstadosFarmaciasClientesSubClientes (  IdEstado, IdFarmacia, IdCliente, IdSubCliente, Status, Actualizado ) 
	Select  IdEstado, IdFarmacia, IdCliente, IdSubCliente, Status, Actualizado
	from #tmpSubFarmacias_Detalles P 
	Where Not Exists 
		( 
			Select * From CFG_EstadosFarmaciasClientesSubClientes C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdCliente = C.IdCliente and C.IdSubCliente = P.IdSubCliente 
		) 


End 
Go--#SQL 

	
