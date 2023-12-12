If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpProceso_Reindexado' and xType = 'U' ) 
	Drop Table tmpProceso_Reindexado
Go--#SQL   

Declare 
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4)  

	Set @IdEmpresa = '001'  
	Set @IdEstado = '22' 
	Set @IdFarmacia = '0003' 
		
	Select IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, Status, 
	     identity(int, 1, 1) as Orden, 0 as Procesado, 0 as Error     
	Into tmpProceso_Reindexado 
	From MovtosInv_Enc (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		  -- and IdTipoMovto_Inv in (  'SPR', 'EPR'   )  
	Order by FechaRegistro 
	
	Update M Set IdTipoMovto_Inv = ( case when M.TipoES = 'E' Then 1 else 0 end) 
	From tmpProceso_Reindexado M 
	-- Inner Join Movtos_Inv_Tipos T On ( M.IdTipoMovto_Inv = T.IdTipoMovto_Inv ) 
Go--#SQL   	
	
	
