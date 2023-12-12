--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INV_ConteoRapido_CodigoEAN_Enc' and xType = 'V' ) 
	Drop View vw_INV_ConteoRapido_CodigoEAN_Enc 
Go--#SQL

Create View vw_INV_ConteoRapido_CodigoEAN_Enc
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa, M.IdEstado, E.Nombre as Estado, M.IdFarmacia, F.NombreFarmacia as Farmacia, 
	M.Folio, M.IdPersonal, vP.NombreCompleto as Personal, M.FechaRegistro, M.FechaInicio, M.FechaFinal, M.Observaciones,
	M.Conteos, M.Status, Case When M.Status = 'A' Then 'ACTIVO' 
		When M.Status = 'T' Then 'TERMINADO'
		Else 'CANCELADO' End As StatusFolio
	From INV_ConteoRapido_CodigoEAN_Enc M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 		
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal ) 			
	
Go--#SQL

	
	
--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INV_ConteoRapido_CodigoEAN_Det' and xType = 'V' ) 
	Drop View vw_INV_ConteoRapido_CodigoEAN_Det 
Go--#SQL

Create View vw_INV_ConteoRapido_CodigoEAN_Det
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.IdFarmacia, M.Farmacia, 
	M.Folio, M.IdPersonal, M.Personal, M.FechaRegistro, M.FechaInicio, M.FechaFinal, M.Observaciones,
	M.Conteos, M.Status, M.StatusFolio,
	P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, P.ContenidoPaquete_ClaveSSA, P.Presentacion_ClaveSSA,
	P.IdProducto, D.CodigoEAN, P.Descripcion, P.ContenidoPaquete, P.Presentacion,
	D.ExistenciaLogica, D.Inv_Inicial, D.Entradas, D.Salidas, D.ExistenciaFinal, 
	D.Conteo1, D.Conteo1_Bodega, D.EsConteo1, D.Conteo2, D.Conteo2_Bodega, D.EsConteo2, D.Conteo3, D.Conteo3_Bodega, D.EsConteo3
	From vw_INV_ConteoRapido_CodigoEAN_Enc M (NoLock) 
	Inner Join INV_ConteoRapido_CodigoEAN_Det D (NoLock) 
		On ( D.IdEmpresa = M.IdEmpresa and D.IdEstado = M.IdEstado and D.IdFarmacia = M.IdFarmacia and D.Folio = M.Folio )
	Inner Join vw_Productos_CodigoEAN P (Nolock) On (P.CodigoEAN = D.CodigoEAN) 			
	
Go--#SQL
		
-------------------------------------------------------------------------------------------------------------------------------------------------------