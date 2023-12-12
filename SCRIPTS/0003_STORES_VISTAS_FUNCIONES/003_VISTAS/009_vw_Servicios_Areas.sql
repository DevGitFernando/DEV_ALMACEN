
-------------------- 
If Exists ( Select Name From sysobjects (noLock) Where Name = 'vw_Servicios_Areas' and xType = 'V' ) 
   Drop View vw_Servicios_Areas 
Go--#SQL

Create View vw_Servicios_Areas 
With Encryption 
As 
	Select S.IdServicio, S.Descripcion as Servicio, IsNull(A.IdArea, '') as IdArea, IsNull(A.Descripcion, '') as Area_Servicio  
	From CatServicios S (NoLock) 
	Left Join CatServicios_Areas A (NoLock) On ( S.IdServicio = A.IdServicio ) 	
Go--#SQL

----------------- 
If Exists ( Select Name From sysobjects (noLock) Where Name = 'vw_Servicios_Areas_Farmacias' and xType = 'V' ) 
   Drop View vw_Servicios_Areas_Farmacias  
Go--#SQL
 

Create View vw_Servicios_Areas_Farmacias 
With Encryption 
As 

	Select F.IdEstado, F.Estado, F.ClaveRenapo, F.EdoStatus, F.IdFarmacia, F.Farmacia, F.FarmaciaStatus, F.FarmaciaStatusAux, 
		 IsNull(R.IdServicio, '') as IdServicio, IsNull(S.Servicio, '') as Servicio, 
		 IsNull(R.IdArea, '') as IdArea, IsNull(S.Area_Servicio, '') as Area_Servicio, IsNull(R.Status, 'C') as StatusAsignacion      
	From vw_Farmacias F (NoLock) 
	Left Join CatServicios_Areas_Farmacias R On ( F.IdEstado = R.IdEstado and F.IdFarmacia = R.IdFarmacia ) 
	Left Join vw_Servicios_Areas S (NoLock) On ( R.IdServicio = S.IdServicio and R.IdArea = S.IdArea ) 
	
Go--#SQL
 


