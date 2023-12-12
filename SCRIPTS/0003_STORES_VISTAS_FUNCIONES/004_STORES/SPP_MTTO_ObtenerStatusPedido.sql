If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'SPP_MTTO_ObtenerStatusPedido' and xType = 'P' ) 
   Drop Proc SPP_MTTO_ObtenerStatusPedido 
Go--#SQL

Create Proc SPP_MTTO_ObtenerStatusPedido ( @IdEmpresa Varchar(3), @IdEstado Varchar(2), @IdAlmacen Varchar(4), @IdFarmacia Varchar(4), @FolioPedido Varchar(8) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	Select Cast('' As Varchar(8)) As FolioSurtido, Cast('' As Varchar(100)) As Status, Cast('' As Varchar(10)) As Fecha
	Into #TmpBusquedaStatus
	Delete #TmpBusquedaStatus

	If Not Exists (Select *
				   From Pedidos_Cedis_Enc (NoLock)
				   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido)
		Begin
			Insert Into #TmpBusquedaStatus
			Select '' , 'SIN PROCESAR', ''
		End
	Else
		Begin
			If Not Exists (Select *
						   From vw_PedidosCedis_Surtimiento (NoLock)
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdAlmacen And IdFarmaciaPedido = @IdFarmacia And FolioPedido = @FolioPedido)
				Begin
					Insert Into #TmpBusquedaStatus
					Select '', 'SIN ATENDER', ''
				End
			Else
				Begin
					Insert Into #TmpBusquedaStatus
					Select FolioSurtido, StatusPedido, Convert(Varchar(10), FechaRegistro, 120) As Fecha
					From vw_PedidosCedis_Surtimiento (NoLock)
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdAlmacen And IdFarmaciaPedido = @IdFarmacia And FolioPedido = @FolioPedido
				End
		End

	Select Top 1 Status, Fecha From #TmpBusquedaStatus (NoLock) Order By FolioSurtido Desc

End