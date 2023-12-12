If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_RevisarStatus' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_RevisarStatus 
Go--#SQL   

Create Proc spp_Mtto_Pedidos_Cedis_RevisarStatus
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11'
) 
With Encryption 
As 
Begin 
Set NoCount On
Declare
	@IdFarmacia varchar(4) = '0005', 
	@IdFarmaciaPedido varchar(4) = '0011', @FolioPedido varchar(6) = '000055'

	Declare pedidos
	cursor Local For
		Select IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita, FolioPedido
		From Pedidos_Cedis_Enc
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And Status Not In ('F')
		OPEN pedidos
			FETCH NEXT FROM pedidos INTO @IdEmpresa, @IdEstado, @IdFarmacia, @IdFarmaciaPedido, @FolioPedido
				WHILE ( @@FETCH_STATUS = 0 ) 
					BEGIN
									
					Exec spp_Mtto_Pedidos_Cedis_RevisarPedidoCompleto
							@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, 
							@IdFarmaciaPedido = @IdFarmaciaPedido, @FolioPedido = @FolioPedido									
															
						FETCH NEXT FROM pedidos INTO  @IdEmpresa, @IdEstado, @IdFarmacia, @IdFarmaciaPedido, @FolioPedido
					END 
		CLOSE pedidos
	DEALLOCATE pedidos

End 
Go--#SQL 