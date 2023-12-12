If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_RemisionesDistEnc' and xType = 'V' ) 
	Drop View vw_RemisionesDistEnc 
Go--#SQL 
 	
Create View vw_RemisionesDistEnc 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa,  
	     M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
	     M.IdFarmacia, F.NombreFarmacia as Farmacia, 
	     M.FolioRemision as Folio, M.IdDistribuidor, P.NombreDistribuidor as Distribuidor, 
		 M.ReferenciaPedido, M.FechaDocumento, M.CodigoCliente, 
		 P.NombreCliente as Cliente, 
		 P.NombreCliente, P.IdFarmacia as IdFarmaciaRelacionada, P.Farmacia as FarmaciaRelacionada, P.EsAdministrado, 		 
		 M.Observaciones, 
		 M.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		 M.FechaRegistro, M.FolioCargaMasiva, M.EsExcepcion, M.EsConsignacion, M.Status  
	From RemisionesDistEnc M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join vw_Distribuidores_Clientes P (NoLock) 
		On ( P.IdEstado = M.IdEstado and P.IdDistribuidor = M.IdDistribuidor and P.CodigoCliente = M.CodigoCliente ) 
------	Inner Join CatDistribuidores P (NoLock) On ( M.IdDistribuidor = P.IdDistribuidor ) 
------	Inner Join CFGC_ConfigurarDistribuidor C(NoLock) On ( M.CodigoCliente = C.CodigoCliente ) 
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )	
Go--#SQL	
 
------------------------------------------------------------------------------   
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_RemisionesDistDet' and xType = 'V' ) 
	Drop View vw_RemisionesDistDet 
Go--#SQL  	

Create View vw_RemisionesDistDet 
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.Folio, 
		D.IdClaveSSA, P.ClaveSSA, P.DescripcionSal, P.Presentacion, P.ContenidoPaquete, D.Precio,
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast(D.Cant_Recibida as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida, 
		D.Status as StatusDet
	From vw_RemisionesDistEnc M (NoLock) 
	Inner Join RemisionesDistDet D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioRemision ) 
	Inner Join vw_ClavesSSA_Sales P (NoLock) On ( D.IdClaveSSA = P.IdClaveSSA_Sal ) 
	-- Where D.Status = 'A' 
Go--#SQL

-----------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Impresion_RemisionesDistribuidor' and xType = 'V' ) 
	Drop View vw_Impresion_RemisionesDistribuidor  
Go--#SQL 
 	
Create View vw_Impresion_RemisionesDistribuidor
With Encryption 
As 
		Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, 
			M.IdFarmacia, M.Farmacia, M.Folio, M.IdDistribuidor, M.Distribuidor, 
			M.ReferenciaPedido, M.FechaDocumento, M.CodigoCliente, M.Cliente, 
			M.IdFarmaciaRelacionada, M.FarmaciaRelacionada,
			D.IdClaveSSA, D.ClaveSSA, D.DescripcionSal, D.Presentacion, D.ContenidoPaquete, D.Precio,
			D.Cantidad, D.Cant_Recibida, D.Cant_Devuelta, D.CantidadRecibida,  
			M.Observaciones, M.IdPersonal, M.NombrePersonal, M.FechaRegistro, M.FolioCargaMasiva,  
			M.EsExcepcion, M.EsConsignacion, Cast(M.EsConsignacion as Int) as EsdeConsignacion, M.Status  
		From vw_RemisionesDistEnc M (NoLock) 
		Inner Join vw_RemisionesDistDet D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.Folio )	
Go--#SQL	
 
------------------------------------------------------------------------------ 

