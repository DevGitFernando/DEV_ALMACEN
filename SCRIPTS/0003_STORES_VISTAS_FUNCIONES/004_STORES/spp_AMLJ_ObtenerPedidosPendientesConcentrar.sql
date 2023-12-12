If Exists ( Select Name From sysobjects (NoLock) Where Name = 'spp_AMLJ_ObtenerPedidosPendientesConcentrar' and xType = 'P' ) 
   Drop Proc spp_AMLJ_ObtenerPedidosPendientesConcentrar 
Go--#SQL
 

Create Proc spp_AMLJ_ObtenerPedidosPendientesConcentrar 
With Encryption 
As 
Begin 
Set NoCount On 

	Select P.IdEmpresa, P.IdEstado, P.IdFarmacia, P.IdJurisdiccion, P.FolioPedidoRC, 
		 P.FechaCaptura, C.Descripcion as NombreCentroDestino,  
		 P.Entrego, count(*) as Claves, sum(D.Cantidad) as Cantidad, 0 as Concentrar  
	From ALMJ_Pedidos_RC P (NoLock) 
	Inner Join ALMJ_Pedidos_RC_Det D (NoLock) 
		On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdJurisdiccion = D.IdJurisdiccion 
			and P.IdFarmacia = D.IdFarmacia and P.FolioPedidoRC = D.FolioPedidoRC ) 
	Inner Join CatCentrosDeSalud C (NoLock) On ( P.IdEstado = C.IdEstado and P.IdCentro = C.IdCentro ) 
	Where P.StatusPedido = 0 and P.Status = 'A' 
	Group by P.IdEmpresa, P.IdEstado, P.IdFarmacia, P.IdJurisdiccion, P.FolioPedidoRC, P.FechaCaptura, C.Descripcion, P.Entrego	
	Order by P.IdEmpresa, P.IdEstado, P.IdFarmacia, P.IdJurisdiccion, P.FolioPedidoRC 
	
End 
Go--#SQL	
 
