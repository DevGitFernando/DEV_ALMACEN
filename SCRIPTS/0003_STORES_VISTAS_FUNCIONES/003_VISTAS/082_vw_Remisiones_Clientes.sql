If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Remisiones_Clientes' and xType = 'V' ) 
	Drop View vw_Remisiones_Clientes 
Go--#SQL 
 	
Create View vw_Remisiones_Clientes 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa, M.IdEstado, E.Nombre as Estado, 
	     M.IdFarmacia, F.NombreFarmacia as Farmacia, 
	     M.IdDistribuidor, D.NombreDistribuidor as Distribuidor, 
		 M.CodigoCliente, M.Referencia, M.FechaDocumento, M.EsConsignacion 
	From Remisiones_Clientes M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia )
	Inner Join CatDistribuidores D (NoLock) 
		On ( D.IdDistribuidor = M.IdDistribuidor )
Go--#SQL


