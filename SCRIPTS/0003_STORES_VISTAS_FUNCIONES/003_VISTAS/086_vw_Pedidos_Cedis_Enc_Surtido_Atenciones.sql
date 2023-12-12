


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Pedidos_Cedis_Enc_Surtido_Atenciones' and xType = 'V' ) 
	Drop View vw_Pedidos_Cedis_Enc_Surtido_Atenciones
Go--#SQL	

Create View vw_Pedidos_Cedis_Enc_Surtido_Atenciones 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa, M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
		M.IdFarmacia, F.NombreFarmacia as Farmacia, 
		M.FolioSurtido, M.FechaRegistro, M.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		M.Keyx, M.Status,
		IsNull( ( Select Top 1 Descripcion From Pedidos_Status EP Where M.Status = EP.ClaveStatus ), 'SIN ESPECIFICAR' )  As StatusSurtido, 
		Observaciones  
	From Pedidos_Cedis_Enc_Surtido_Atenciones M (NoLock) 
	Inner Join CatEmpresas Ex On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )
Go--#SQL	