



--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFG_ALMN_Ubicaciones_Estandar' and xType = 'V' ) 
	Drop View vw_CFG_ALMN_Ubicaciones_Estandar
Go--#SQL

Create View vw_CFG_ALMN_Ubicaciones_Estandar 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Empresa, M.IdEstado, F.Estado as Estado, F.ClaveRenapo, M.IdFarmacia, F.Farmacia, 
	M.NombrePosicion as Posicion, CU.Descripcion as DescripcionPosicion,
	M.IdRack, CP.DescripcionPasillo as Rack, M.IdNivel, CE.DescripcionEstante as Nivel, M.IdEntrepaño, E.DescripcionEntrepaño as Entrepaño, 
	M.FechaRegistro, M.IdPersonal, vP.NombreCompleto as Personal, M.Status, Case When M.Status = 1 Then 'ACTIVA' Else 'CANCELADA' End As StatusAux    
	From CFG_ALMN_Ubicaciones_Estandar M (NoLock) 
	Inner Join vw_Empresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 		
	Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )  	
	Inner Join CatPasillos CP (Nolock) 
		On ( CP.IdEmpresa = M.IdEmpresa and CP.IdEstado = M.IdEstado and CP.IdFarmacia = M.IdFarmacia and CP.IdPasillo = M.IdRack )
	Inner Join CatPasillos_Estantes CE (Nolock) 
		On ( CE.IdEmpresa = M.IdEmpresa and CE.IdEstado = M.IdEstado and CE.IdFarmacia = M.IdFarmacia 
			and CE.IdPasillo = M.IdRack and CE.IdEstante = M.IdNivel )
	Inner Join CatPasillos_Estantes_Entrepaños E (Nolock) 
		On ( E.IdEmpresa = M.IdEmpresa and E.IdEstado = M.IdEstado and E.IdFarmacia = M.IdFarmacia 
			and E.IdPasillo = M.IdRack and E.IdEstante = M.IdNivel and E.IdEntrepaño = M.IdEntrepaño )
	Inner Join Cat_ALMN_Ubicaciones_Estandar CU (Nolock) On ( CU.NombrePosicion = M.NombrePosicion )
		
	
Go--#SQL

