------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_PedidosCedis_Surtimiento' and xType = 'V' )  
   Drop View vw_PedidosCedis_Surtimiento  	
Go--#SQL 

Create View vw_PedidosCedis_Surtimiento 
As 
	
	Select
		P.IdEmpresa, CE.Nombre as Empresa, P.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
		F.IdJurisdiccion, J.Descripcion as Jurisdiccion, P.IdFarmacia, F.NombreFarmacia as Farmacia, P.FolioSurtido, 
		FP.IdJurisdiccion as IdJurisdiccionPedido, JP.Descripcion as JurisdiccionPedido, 
		P.IdFarmaciaPedido, FP.NombreFarmacia as FarmaciaPedido, 
		PE.IdCliente, PE.IdSubCliente, PE.IdBeneficiario, 
		P.FolioPedido, P.FolioTransferenciaReferencia, P.FechaRegistro, PE.FechaEntrega, 
		P.Prioridad, PP.Descripcion as PrioridadDesc, 

		P.TipoDeInventario, 
		( 
			case when P.TipoDeInventario = 0 then 'General' 
			     when P.TipoDeInventario = 1 then 'Consignación' 
				 when P.TipoDeInventario = 2 then 'Venta' 
			else 
				 'NO ESPECIFICADO'	
			end 					
		) as TipoDeInventario_Descripcion, 

		PE.EsTransferencia, 
		PE.IdEstadoSolicita, IsNull(ES.Nombre, '') As EstadoSolicita,
		PE.IdFarmaciaSolicita, 
		
		(case when EsTransferencia = 1 then IsNull(FS.NombreFarmacia, '') else (B.ApPaterno + ' ' + B.ApPaterno + ' ' + B.Nombre) end) As FarmaciaSolicita, 


		'TipoDePedido' = (case when PE.EsTransferencia = 1 then (case when PE.IdEstado = PE.IdEstadoSolicita Then 'TRANSFERENCIA' else 'TRANSFERENCIA INTERESTATAL' end ) Else 'VENTA' End ), 
		P.IdPersonal, vP.NombreCompleto as NombrePersonal, P.IdPersonalSurtido, 
		IsNull(PC.Personal, '0000') as PersonalSurtido, P.Observaciones, P.Status,
		IsNull( ( Select Top 1 Descripcion From Pedidos_Status EP Where P.Status = EP.ClaveStatus ), 'SIN ESPECIFICAR' )  As StatusPedido, P.EsManual, 
		Pe.ReferenciaInterna 
	From Pedidos_Cedis_Enc_Surtido P (NoLock) 
	Inner Join Pedidos_Cedis_Enc PE (NoLock) 
		On ( P.IdEmpresa = PE.IdEmpresa And P.IdEstado = PE.IdEstado and P.IdFarmaciaPedido = PE.IdFarmacia And P.FolioPedido = PE.FolioPedido ) 
	Inner Join Pedidos_Prioridades PP (NoLock) On ( P.Prioridad = PP.Prioridad ) 
	Inner Join CatEmpresas CE (NoLock) On ( P.IdEmpresa = CE.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( P.IdEstado = E.IdEstado ) 
	Inner Join CatEstados ES (NoLock) On ( PE.IdEstadoSolicita = ES.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia ) 	 
	Inner Join CatFarmacias FP (NoLock) On ( P.IdEstado = FP.IdEstado and P.IdFarmaciaPedido = FP.IdFarmacia )
	Left Join CatFarmacias FS (NoLock) On ( PE.IdEstadoSolicita = FS.IdEstado and PE.IdFarmaciaSolicita = FS.IdFarmacia )
	Inner Join vw_Personal vP (NoLock) On ( P.IdEstado = vP.IdEstado and P.IdFarmacia = vP.IdFarmacia and P.IdPersonal = vP.IdPersonal )
	Inner Join CatJurisdicciones J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion )
	Inner Join CatJurisdicciones JP (NoLock) On ( FP.IdEstado = JP.IdEstado and FP.IdJurisdiccion = JP.IdJurisdiccion )	
	Left Join vw_PersonalCEDIS PC (NoLock) 
		On ( P.IdEmpresa = PC.IdEmpresa and P.IdEstado = PC.IdEstado and P.IdFarmacia = PC.IdFarmacia and P.IdPersonalSurtido = PC.IdPersonal )
	Left Join CatBeneficiarios B (NoLock) On ( PE.IdEstado = B.IdEstado and PE.IdFarmacia = B.IdFarmacia and PE.IdCliente = B.IdCliente and PE.IdSubCliente = B.IdSubCliente and PE.IdBeneficiario = B.IdBeneficiario ) 
	
	
Go--#SQL 
	
--	Select * From Pedidos_Cedis_Enc_Surtido (Nolock)
--	Select * From vw_PedidosCedis_Surtimiento (Nolock)