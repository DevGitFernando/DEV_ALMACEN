
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_Devolucion_Pedido' and xType = 'P')
    Drop Proc spp_Mtto_COM_OCEN_Devolucion_Pedido
Go--#SQL
  
Create Proc spp_Mtto_COM_OCEN_Devolucion_Pedido 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0001', @FolioOrden varchar(8) = '00003773'  
)
With Encryption 
As
Begin
Set NoCount On

Declare @sStatus varchar(2), @EsAutomatica bit, @FolioPedido varchar(6), 
		@IdTipoPedido varchar(2), @EsCentral bit, @UnidadPedido varchar(4)  

	
	Set @sStatus = 'A'
	Set @EsAutomatica = 0 
	Set @FolioPedido = ''
	Set @IdTipoPedido = '04'
	Set @EsCentral = 0
	Set @UnidadPedido = ''
	
	Select @EsAutomatica = EsAutomatica, @FolioPedido = FolioPedido, @EsCentral = EsCentral, @UnidadPedido = EntregarEn 
	From COM_OCEN_OrdenesCompra_Claves_Enc (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia and FolioOrden = @FolioOrden

	If @EsAutomatica = 0
		Begin 
			Set @sStatus = 'C'
			Update D Set D.Status = @sStatus
			From COM_OCEN_OrdenesCompra_CodigosEAN_Det D (Nolock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia and FolioOrden = @FolioOrden
		End
	Else
		Begin 
			Set @sStatus = 'D'
			
			Update D Set D.Status = @sStatus
			From COM_OCEN_OrdenesCompra_CodigosEAN_Det D (Nolock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia and FolioOrden = @FolioOrden
			
			Select IdEmpresa, IdEstado, EntregarEn as IdFarmacia, @FolioPedido as FolioPedido, 
			IdClaveSSA_Sal as IdClaveSSA, SUM(Cantidad) As Cantidad
			Into #tmp_Pedido_Devolucion
			From vw_Impresion_OrdenesCompras_CodigosEAN D (Nolock)
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia and Folio = @FolioOrden
			Group By IdEmpresa, IdEstado, EntregarEn, IdClaveSSA_Sal
			
			If @EsCentral = 1
				Begin 
					Update C Set C.Cantidad_Surtida = (C.Cantidad_Surtida - D.Cantidad), C.CantidadASurtir = ( C.CantidadASurtir + D.Cantidad )
					From COM_OCEN_REG_PedidosDet_Claves C (Nolock)
					Inner Join #tmp_Pedido_Devolucion D (Nolock)
						On ( C.IdEmpresa = D.IdEmpresa And C.IdEstado = D.IdEstado And C.IdFarmacia = D.IdFarmacia 
							and C.FolioPedido = D.FolioPedido and C.IdClaveSSA = D.IdClaveSSA )
					Where C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado And C.IdFarmacia = @UnidadPedido And C.IdTipoPedido = @IdTipoPedido 
					And C.FolioPedido =  @FolioPedido 
				End
			Else
				Begin
					Update C Set C.Cantidad_Surtida = (C.Cantidad_Surtida - D.Cantidad), C.CantidadAComprar = (C.CantidadAComprar + D.Cantidad)
					From COM_OCEN_PedidosDet_Claves C (Nolock)
					Inner Join #tmp_Pedido_Devolucion D (Nolock)
						On ( C.IdEmpresa = D.IdEmpresa And C.IdEstado = D.IdEstado And C.IdFarmacia = D.IdFarmacia 
							and C.FolioPedido = D.FolioPedido and C.IdClaveSSA = D.IdClaveSSA )
					Where C.IdEmpresa = @IdEmpresa And C.IdEstado = @IdEstado And C.IdFarmacia = @UnidadPedido And C.IdTipoPedido = @IdTipoPedido 
					And C.FolioPedido =  @FolioPedido
				End
			
		End
	
End
Go--#SQL
