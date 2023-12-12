-------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFG_ALMN_Unidades_Atendidas' and xType = 'V' ) 
   Drop View vw_CFG_ALMN_Unidades_Atendidas  
Go--#SQL 

Create View vw_CFG_ALMN_Unidades_Atendidas 
As 

	Select C.IdEstado, C.IdAlmacen, F.Farmacia as Almacen, C.IdFarmacia, F2.Farmacia as Farmacia, C.EsAsignado, C.Status 
	From CFG_ALMN_Unidades_Atendidas C (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdAlmacen = F.IdFarmacia ) 
	Inner Join vw_Farmacias F2 (NoLock) On ( C.IdEstado = F2.IdEstado and C.IdFarmacia = F2.IdFarmacia ) 	
	
Go--#SQL 
	
	