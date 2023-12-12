----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__11__ConfigurarConexiones' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__11__ConfigurarConexiones 
Go--#SQL 

Create Proc spp_CFG_OP__11__ConfigurarConexiones
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
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, Servidor, WebService, PaginaWeb, Status, Actualizado, SSL, ModoActivoDeTransferenciaFTP
	Into #tmpCFGS_ConfigurarConexiones
	From CFGS_ConfigurarConexiones P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
	

	Insert Into CFGS_ConfigurarConexiones (  IdEstado, IdFarmacia, Servidor, WebService, PaginaWeb, Status, Actualizado, SSL, ModoActivoDeTransferenciaFTP ) 
	Select  IdEstado, IdFarmacia, Servidor, WebService, PaginaWeb, Status, Actualizado, SSL, ModoActivoDeTransferenciaFTP 
	from #tmpCFGS_ConfigurarConexiones P 
	Where Not Exists 
		( 
			Select * From CFGS_ConfigurarConexiones C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia
		) 


End 
Go--#SQL 

	


