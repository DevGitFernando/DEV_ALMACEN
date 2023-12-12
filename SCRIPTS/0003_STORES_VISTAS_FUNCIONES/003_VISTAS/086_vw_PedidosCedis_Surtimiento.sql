If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_PedidosCedis_Surtimiento' and xType = 'V' )  
   Drop View vw_PedidosCedis_Surtimiento  	
Go--#SQL 

Create View vw_PedidosCedis_Surtimiento 
As 
	
	Select
		P.IdEmpresa, CE.Nombre as Empresa, P.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
		F.IdJurisdiccion, J.Descripcion as Jurisdiccion, P.IdFarmacia, F.NombreFarmacia as Farmacia,P.FolioSurtido, 
		FP.IdJurisdiccion as IdJurisdiccionPedido, JP.Descripcion as JurisdiccionPedido,
		P.IdFarmaciaPedido, FP.NombreFarmacia as FarmaciaPedido, 
		P.FolioPedido, P.FolioTransferenciaReferencia, P.FechaRegistro,
		PE.EsTransferencia, 
		P.IdPersonal, vP.NombreCompleto as NombrePersonal, P.IdPersonalSurtido, PC.Personal as PersonalSurtido, P.Observaciones, P.Status, 
		(
			Case When P.Status = 'A' Then 'SURTIMIENTO' 
				 When P.Status = 'S' Then 'SURTIDO' 
				 When P.Status = 'V' Then 'EN VALIDACIÓN' 				 
				 When P.Status = 'D' Then 'DISTRIBUCIÓN' 
				 When P.Status = 'T' Then 'TRANSITO' 
				 When P.Status = 'R' Then 'REGISTRADO'	
				 When P.Status = 'F' Then 'FINALIZADO'					 
			Else 'CANCELADO' End 
		) As StatusPedido 
	From Pedidos_Cedis_Enc_Surtido P (NoLock)
	Inner Join Pedidos_Cedis_Enc PE (NoLock)
		On (P.IdEmpresa = PE.IdEmpresa And P.IdEstado = PE.IdEstado and P.IdFarmaciaPedido = PE.IdFarmacia And P.FolioPedido = PE.FolioPedido)
	Inner Join CatEmpresas CE (NoLock) On ( P.IdEmpresa = CE.IdEmpresa )
	Inner Join CatEstados E (NoLock) On ( P.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia ) 	 
	Inner Join CatFarmacias FP (NoLock) On ( P.IdEstado = FP.IdEstado and P.IdFarmaciaPedido = FP.IdFarmacia ) 
	Inner Join vw_Personal vP (NoLock) On ( P.IdEstado = vP.IdEstado and P.IdFarmacia = vP.IdFarmacia and P.IdPersonal = vP.IdPersonal )
	Inner Join CatJurisdicciones J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion )
	Inner Join CatJurisdicciones JP (NoLock) On ( FP.IdEstado = JP.IdEstado and FP.IdJurisdiccion = JP.IdJurisdiccion )		
	Inner Join vw_PersonalCEDIS PC (NoLock) 
		On ( P.IdEmpresa = PC.IdEmpresa and P.IdEstado = PC.IdEstado and P.IdFarmacia = PC.IdFarmacia and P.IdPersonalSurtido = PC.IdPersonal )
	
Go--#SQL 
	
--	Select * From Pedidos_Cedis_Enc_Surtido (Nolock)
--	Select * From vw_PedidosCedis_Surtimiento (Nolock)