----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__12__CFG_EmpresasFarmacias' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__12__CFG_EmpresasFarmacias 
Go--#SQL 

Create Proc spp_CFG_OP__12__CFG_EmpresasFarmacias  
(
	@IdEmpresaBase varchar(3) = '001', @IdEstadoBase varchar(2) = '21', @IdFarmaciaBase varchar(4) = '2005',
	@IdEmpresaDestino varchar(3) = '001', @IdEstadoDestino varchar(2) = '21', @IdFarmaciaDestino varchar(4) = '3005' 
) 
As 
Begin 
Set NoCount On 

	Set @IdEmpresaBase = right('0000' + @IdEmpresaBase, 3) 
	Set @IdEmpresaDestino = right('0000' + @IdEmpresaDestino, 3)
	Set @IdEstadoBase = right('0000' + @IdEstadoBase, 2) 
	Set @IdFarmaciaBase = right('0000' + @IdFarmaciaBase, 4) 
	Set @IdEstadoDestino = right('0000' + @IdEstadoDestino, 2) 
	Set @IdFarmaciaDestino = right('0000' + @IdFarmaciaDestino, 4) 
	
---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEmpresaDestino As IdEmpresa, @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, Status, 0 As Actualizado
	Into #tmp_CFG_EmpresasFarmacias 
	From CFG_EmpresasFarmacias P (NoLock) 
	Where IdEmpresa = @IdEmpresaBase And IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
	

	Insert Into CFG_EmpresasFarmacias (  IdEmpresa, IdEstado, IdFarmacia, Status, Actualizado ) 
	Select  IdEmpresa, IdEstado, IdFarmacia, Status, Actualizado 
	from #tmp_CFG_EmpresasFarmacias P 
	Where Not Exists 
		( 
			Select * From CFG_EmpresasFarmacias C (NoLock) 
			Where P.IdEmpresa = C.IdEmpresa And P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia
		) 

End 
Go--#SQL 			
		