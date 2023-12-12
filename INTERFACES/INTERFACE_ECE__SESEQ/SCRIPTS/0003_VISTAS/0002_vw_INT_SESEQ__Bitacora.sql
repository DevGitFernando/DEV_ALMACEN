---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_INT_SESEQ__Bitacora' and xType = 'V' ) 
	Drop view vw_INT_SESEQ__Bitacora 
Go--#SQL  

Create View vw_INT_SESEQ__Bitacora 
As 
	select  
		I.IdEmpresa, 
		I.IdEstado, F.Estado, I.IdFarmacia, F.Farmacia, 
		--I.Referencia_SESEQ, I.URL_Interface, 
		--I.CapturaInformacion, 
		I.Referencia_SESEQ_CentroDeCostos, I.Referencia_SESEQ_CCC, 
		CC.Nombre as NombreDelCentroDeCostos, 

		B.Tipo as ClaveMovimiento, 
		M.Nombre as Movimiento, 
		M.Descripcion as Movimiento_Descripcion, 
		B.FechaProcesada, 
		B.ExistenDatos, 
		(case when B.ExistenDatos = 1 then 'SI' else 'NO' end) as ExistenDatos_Descripcion, 
		B.HuboError, 
		(case when B.HuboError = 1 then 'SI' else 'NO' end) as HuboError_Descripcion, 		
		B.Respuesta_Integracion, B.FechaRegistro 
		
	From INT_SESEQ__CFG_Farmacias_UMedicas I (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( I.IdEstado = F.IdEstado and I.IdFarmacia = F.IdFarmacia ) 
	Inner Join INT_SESEQ__CentrosDeCostos CC (NoLock) On ( I.Referencia_SESEQ_CentroDeCostos = CC.Clave ) 
	Inner Join INT_SESEQ__Informacion_Bitacora B (NoLock) 
		On ( I.IdEmpresa = I.IdEmpresa and F.IdEstado = B.IdEstado and F.IdFarmacia = B.IdFarmacia ) 
	Inner Join INT_SESEQ__Informacion_Movimientos M (NoLock) On ( B.Tipo = M.Tipo ) 	

Go--#SQL 



	Select * 
	from vw_INT_SESEQ__Bitacora 
	order by 
		FechaProcesada, ClaveMovimiento 
		
	
		