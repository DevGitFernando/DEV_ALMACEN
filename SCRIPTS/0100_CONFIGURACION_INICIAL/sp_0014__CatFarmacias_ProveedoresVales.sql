----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__14__CatFarmacias_ProveedoresVales' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__14__CatFarmacias_ProveedoresVales 
Go--#SQL 

Create Proc spp_CFG_OP__14__CatFarmacias_ProveedoresVales
(
	@IdEstadoBase varchar(2) = '21', @IdFarmaciaBase varchar(4) = '2005', @IdEstadoDestino varchar(2) = '21', @IdFarmaciaDestino varchar(4) = '3005' 
) 
As 
Begin 
Set NoCount On 

	Set @IdEstadoBase = right('0000' + @IdEstadoBase, 2) 
	Set @IdFarmaciaBase = right('0000' + @IdFarmaciaBase, 4) 
	Set @IdEstadoDestino = right('0000' + @IdEstadoDestino, 2) 
	Set @IdFarmaciaDestino = right('0000' + @IdFarmaciaDestino, 4) 
	
---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, IdProveedor, EsProv_Reembolso, Status, 0 As Actualizado
	Into #tmp_CatFarmacias_ProveedoresVales
	From CatFarmacias_ProveedoresVales P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
		and Status = 'A' 
			

	Insert Into CatFarmacias_ProveedoresVales (  IdEstado, IdFarmacia, IdProveedor, EsProv_Reembolso, Status, Actualizado ) 
	Select  IdEstado, IdFarmacia, IdProveedor, EsProv_Reembolso, Status, Actualizado 
	from #tmp_CatFarmacias_ProveedoresVales P 
	Where Not Exists 
		( 
			Select * From CatFarmacias_ProveedoresVales C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia And P.IdProveedor = C.IdProveedor
		) 

End 
Go--#SQL