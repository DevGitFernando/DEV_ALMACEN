If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ALMJ_Pedidos_RC' and xType = 'V' ) 
	Drop View vw_ALMJ_Pedidos_RC 
Go--#SQL

Create View vw_ALMJ_Pedidos_RC 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa,  
	     M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
		 M.IdJurisdiccion, J.Descripcion as Jurisdiccion, 
	     M.IdFarmacia, F.NombreFarmacia as Farmacia,
		 M.FolioPedidoRC, 
		 M.IdCentro, C.Descripcion as NombreCentro,
		 M.Entrego, 
		 M.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		 M.FechaSistema, M.FechaCaptura as FechaRecepcion, M.Status, 
		 M.StatusPedido, 
		 (Case When M.StatusPedido = 0 Then 'RECIBIDO EN ALMANCEN' 
			   When M.StatusPedido = 1 Then 'CONCENTRADO' 
			   When M.StatusPedido = 2 Then 'SURTIDO PARCIAL' 
			   When M.StatusPedido = 3 Then 'SURTIDO COMPLETO' 			   			    	
		  End ) as StatusPedidoDesc 
	From ALMJ_Pedidos_RC M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join CatJurisdicciones J (NoLock) On ( E.IdEstado = J.IdEstado and M.IdJurisdiccion = J.IdJurisdiccion )
	Inner Join CatFarmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 
	Inner Join CatCentrosDeSalud C (NoLock) On ( M.IdEstado = C.IdEstado and M.IdCentro = C.IdCentro )	
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )	
Go--#SQL

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_ALMJ_Pedidos_RC_Det' and xType = 'V' ) 
	Drop View vw_ALMJ_Pedidos_RC_Det 
Go--#SQL	

Create View vw_ALMJ_Pedidos_RC_Det 
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.IdJurisdiccion, M.Jurisdiccion, M.IdFarmacia, M.Farmacia, M.FolioPedidoRC, 
		D.IdClaveSSA, S.ClaveSSA, S.Descripcion as DescripcionSal, 
		cast(D.Cantidad as int) as Cantidad   		
	From vw_ALMJ_Pedidos_RC M (NoLock) 
	Inner Join ALMJ_Pedidos_RC_Det D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdJurisdiccion = D.IdJurisdiccion and M.IdFarmacia = D.IdFarmacia and M.FolioPedidoRC = D.FolioPedidoRC ) 
	Inner Join CatClavesSSA_Sales S (NoLock) On ( D.IdClaveSSA = S.IdClaveSSA_Sal ) 
	-- Where D.Status = 'A' 
Go--#SQL	


