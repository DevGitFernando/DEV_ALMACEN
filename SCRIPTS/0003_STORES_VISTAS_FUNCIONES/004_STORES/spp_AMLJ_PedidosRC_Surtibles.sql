If Exists ( Select Name From sysobjects(NoLock) Where Name = 'spp_AMLJ_PedidosRC_Surtibles' And xType = 'P' )
	Drop Proc spp_AMLJ_PedidosRC_Surtibles
Go--#SQL	


Create Procedure spp_AMLJ_PedidosRC_Surtibles 
With Encryption 
As
Begin
	
	Select Enc.IdEmpresa, Enc.IdEstado, Enc.IdJurisdiccion, Enc.IdFarmacia, Enc.FolioPedidoRC, Enc.StatusPedidoDesc,
	(	Select Count( * ) 
		From Ventas_ALMJ_PedidosRC_Surtido Sur(NoLock)
		Where Enc.IdEmpresa = Sur.IdEmpresa And Enc.IdEstado = Sur.IdEstado
		And Enc.IdFarmacia = Sur.IdFarmacia And Enc.FolioPedidoRC = Sur.FolioPedidoRC 
	) as NumeroSurtimientos,
	Convert(varchar(10), Enc.FechaRecepcion, 111) as FechaRecepcion, Enc.NombreCentro,
	(	Select Sum( Cantidad ) 
		From vw_ALMJ_Pedidos_RC_Det Det(NoLock) 
		Where Enc.IdEmpresa = Det.IdEmpresa And Enc.IdEstado = Det.IdEstado And Enc.IdJurisdiccion = Det.IdJurisdiccion
		And Enc.IdFarmacia = Det.IdFarmacia And Enc.FolioPedidoRC = Det.FolioPedidoRC ) as CantidadSolicitada,
	(	Select IsNull( Sum( CantidadSurtida ), 0 ) 
		From Ventas_ALMJ_PedidosRC_Surtido Sur(NoLock)
		Where Enc.IdEmpresa = Sur.IdEmpresa And Enc.IdEstado = Sur.IdEstado
		And Enc.IdFarmacia = Sur.IdFarmacia And Enc.FolioPedidoRC = Sur.FolioPedidoRC 
	) as CantidadEntregada
	From vw_ALMJ_Pedidos_RC Enc (NoLock)
	Where Enc.StatusPedido in ( 1, 2 )
		And 
		(  
			Exists ( Select * From CFGC_AlmacenesJurisdiccionales AL (NoLock) 
					Where Enc.IdEmpresa = AL.IdEmpresa And Enc.IdEstado = AL.IdEstado And Enc.IdFarmacia = AL.IdFarmacia )  
				Or  
			Exists ( Select * From CFGC_AlmacenesJurisdiccionales AL (NoLock) 
					Where Enc.IdEmpresa = AL.IdEmpresaCSGN And Enc.IdEstado = AL.IdEstadoCSGN And Enc.IdFarmacia = AL.IdFarmaciaCSGN ) 
		) 

End
Go--#SQL	

