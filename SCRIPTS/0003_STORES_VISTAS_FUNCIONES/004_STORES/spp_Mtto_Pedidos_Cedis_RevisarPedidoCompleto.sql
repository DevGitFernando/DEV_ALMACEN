
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto 
Go--#SQL   

--	Exec spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto '001', '11', '0005', '0005', '000002' 

--	Exec spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto '001', '21', '1182', '1101', '000001' 

Create Proc spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '28', @IdFarmacia varchar(4) = '0003', 
	@IdFarmaciaPedido varchar(4) = '0020', @FolioPedido varchar(6) = '000003' 
) 
----( 
----	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0103', @FolioPedido varchar(6) = '000001' 
----) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@ClavesPedido int,  
	@ClavesTerminadas int


	--Select 
	--	P.IdEmpresa, P.IdEstado, P.IdFarmacia, P.FolioPedido, 
	--	D.IdClaveSSA, D.Cantidad as CantidadSolicitada, 0 as CantidadSurtida, 0 as Cantidad, 
	--	(case when D.Status = 'F' Then 1 Else 0 End) as ClaveTerminada   
	--Into #tmpDetalle 	
	--From Pedidos_Cedis_Enc P (NoLock) 
	--Inner Join Pedidos_Cedis_Det D (NoLock) 
	--	On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioPedido = D.FolioPedido )  	
	--Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioPedido = @FolioPedido

	Select Distinct FolioTransferenciaReferencia
	Into #tmpTransferencias
	From Pedidos_Cedis_Enc_Surtido P (NoLock) 
	Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
		On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado --and P.IdFarmacia = @IdFarmacia 
		  and P.IdFarmaciaPedido = @IdFarmacia--Pedido
		  and P.FolioPedido = @FolioPedido
		  And left(FolioTransferenciaReferencia, 2) = 'TS'

	--Select * From #tmpTransferencias


	Select 
		F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdProducto, P.ClaveSSA, Existencia,
		datediff(mm, getdate(), IsNull(F.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesCaducar
	Into #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
	From FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.CodigoEAN = P.CodigoEAN)	
	
	Select
		P.IdEmpresa, P.IdEstado, P.IdFarmacia, P.FolioPedido, 
		P.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, --P.Cantidad
		(Case
			When P.PCM = 0 Then P.Cantidad
			When (P.Cantidad + P.Existencia ) <= P.PCM Then P.Cantidad
			When (P.Cantidad + P.Existencia ) > P.PCM Then P.PCM - P.Existencia
			End) as CantidadSolicitada, 
			0 as CantidadSurtida, 0 as Cantidad,  
		--P.Existencia, 
		IsNull(Sum( F.Existencia ), 0) As Existencia,
		P.Status, cast(0 as bit) as Terminar, cast(0 as bit) as ClaveTerminada 
	Into #tmpDetalle 
	From Pedidos_Cedis_Det P (NoLock) 
	Inner Join 	vw_ClavesSSA_Sales C (NoLock) On ( P.IdClaveSSA = C.IdClaveSSA_Sal )
	Left Join  #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (Nolock)
		On (  F.IdEmpresa = @IdEmpresa And F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia And P.ClaveSSA = F.ClaveSSA ) 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioPedido = @FolioPedido
	Group By  P.IdEmpresa, P.IdEstado, P.IdFarmacia, P.FolioPedido, P.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, P.Cantidad, P.Existencia, P.PCM, P.Status 	
	
	update T Set CantidadSolicitada =  CantidadSolicitada - 
		IsNull(
				(
					Select Sum(CantidadEnviada)
					From TransferenciasEnc Te(NoLock)
					Inner Join TransferenciasDet Td(NoLock) 
						On ( Te.IdEmpresa = Td.IdEmpresa And Te.IdEstado = Td.IdEstado
								And Te.IdFarmacia = Td.IdFarmacia And Te.FolioTransferencia = Td.FolioTransferencia )
					Inner Join vw_Productos_CodigoEAN P (NoLock) 
						On ( Td.IdProducto = P.IdProducto And Td.COdigoEAN = P.CodigoEAN )
					Where  Te.IdEstadoRecibe = @IdEstado And Te.IdFarmaciaRecibe = @IdFarmaciaPedido
						And Te.IdFarmacia = @IdFarmacia And TipoTransferencia = 'TS' And TransferenciaAplicada = '0' And T.IdClaveSSA = P.IdClaveSSA_Sal
						And te.FolioTransferencia Not in (Select FolioTransferenciaReferencia From #tmpTransferencias)	
				)
			,0 )
	From #tmpDetalle T (NoLock)

--- Obtener Surtido 	
	--Select D.Status, IdClaveSSA, sum(IsNull((case when P.Status not in ('E', 'R') Then (Case When P.Status = 'C' Then 0 else DD.CantidadRequerida End) Else DD.CantidadAsignada End), 0)) as CantidadSurtida 
	--Into #tmpDetalle_Surtido_Detallado  	
	--From Pedidos_Cedis_Enc_Surtido P (NoLock) 
	--Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
	--	On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
	--Left Join Pedidos_Cedis_Det_Surtido_Distribucion DD (NoLock) 
	--		On ( D.IdEmpresa = DD.IdEmpresa and D.IdEstado = DD.IdEstado and D.IdFarmacia = DD.IdFarmacia and D.FolioSurtido = DD.FolioSurtido 
	--			And D.ClaveSSA = DD.ClaveSSA ) 	
	--Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado --and P.IdFarmacia = @IdFarmacia 
	--	  and P.IdFarmaciaPedido = @IdFarmacia--Pedido
	--	  and P.FolioPedido = @FolioPedido 	
	--Group by D.Status, IdClaveSSA 

	--Delete From #tmpDetalle_Surtido_Detallado 
	
	--Insert Into #tmpDetalle_Surtido_Detallado 
	Select D.Status, D.IdClaveSSA, sum(IsNull((case when P.Status = 'E' Then DD.CantidadAsignada else 0 End), 0)) as CantidadSurtida 
	Into #tmpDetalle_Surtido_Detallado 
	From Pedidos_Cedis_Enc_Surtido P (NoLock) 
	Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
		On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
	Left Join Pedidos_Cedis_Det_Surtido_Distribucion DD (NoLock) 
		On ( D.IdEmpresa = DD.IdEmpresa and D.IdEstado = DD.IdEstado and D.IdFarmacia = DD.IdFarmacia and D.FolioSurtido = DD.FolioSurtido 
			And D.ClaveSSA = DD.ClaveSSA ) 		
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmaciaPedido = @IdFarmacia 
			--and P.IdFarmaciaPedido = @IdFarmaciaPedido
			and P.FolioPedido = @FolioPedido 		
	Group by D.Status, D.IdClaveSSA  
		
	--Select * From #tmpDetalle_Surtido_Detallado

	Select 'A' as Status, IdClaveSSA, sum(CantidadSurtida) as CantidadSurtida 
	Into #tmpDetalle_Surtido  	
	From #tmpDetalle_Surtido_Detallado
	Group by IdClaveSSA 

---		spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto 

	Update S Set Status = 'F' 
	From #tmpDetalle_Surtido S 
	Inner Join #tmpDetalle_Surtido_Detallado D On ( S.IdClaveSSA = D.IdClaveSSA ) 
	Where D.Status = 'F' 

	Update D Set CantidadSurtida = S.CantidadSurtida  --, ClaveTerminada = (case when D.Status = 'T' Then 1 Else 0 End) 
	From #tmpDetalle D  
	Inner Join #tmpDetalle_Surtido S On ( D.IdClaveSSA = S.IdClaveSSA )		
	
	Update D Set ClaveTerminada = 1 
	From #tmpDetalle D  
	Inner Join #tmpDetalle_Surtido S On ( D.IdClaveSSA = S.IdClaveSSA )			
	Where ClaveTerminada = 0 and D.CantidadSolicitada <= S.CantidadSurtida 
	
	Update D Set ClaveTerminada = 1 
	From #tmpDetalle D  
	Inner Join #tmpDetalle_Surtido S On ( D.IdClaveSSA = S.IdClaveSSA )				
	Where ClaveTerminada = 0 and S.Status = 'F'
	

--- Revisar el Status del Pedido 	
	Select @ClavesPedido = count(*) From #tmpDetalle 
	Select @ClavesTerminadas = count(*) From #tmpDetalle Where ClaveTerminada = 1 
	
	Select @ClavesPedido, @ClavesTerminadas 
	select * from #tmpDetalle
	--Select * From #tmpDetalle_Surtido

	If @ClavesPedido = @ClavesTerminadas -- and 1 = 0 
	Begin 
----	   Update P Set Status = 'F' 
----	   From Pedidos_Cedis_Enc_Surtido P (NoLock) 
----	   Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia 
----		  and P.IdFarmaciaPedido = @IdFarmaciaPedido and P.FolioPedido = @FolioPedido 	
			  
	   Update P Set Status = 'F' 
	   From Pedidos_Cedis_Enc P (NoLock) 
	   Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmaciaSolicita = @IdFarmaciaPedido and P.FolioPedido = @FolioPedido 	
		  		  
	End 
	
---		spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto  	
	
	
End 
Go--#SQL 


