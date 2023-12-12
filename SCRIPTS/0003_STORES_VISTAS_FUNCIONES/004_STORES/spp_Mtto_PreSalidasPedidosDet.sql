If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_PreSalidasPedidosDet' and xType = 'P' )
   Drop Proc spp_Mtto_PreSalidasPedidosDet 
Go--#SQL

Create Proc spp_Mtto_PreSalidasPedidosDet 
(
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @FolioPreSalida varchar(8), @IdClaveSSA varchar(4), 
	@CantidadRequerida numeric(14,4) 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  
		

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	If Not Exists ( Select FolioPreSalida From PreSalidasPedidosDet (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioPreSalida = @FolioPreSalida and 
			  IdClaveSSA = @IdClaveSSA   ) 
	   Begin 
	       Insert Into PreSalidasPedidosDet ( IdEmpresa, IdEstado, IdFarmacia, FolioPreSalida, IdClaveSSA, CantidadRequerida, CantidadAsignada, 
				  Status, Actualizado  ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPreSalida, @IdClaveSSA, @CantidadRequerida, 0, @sStatus, @iActualizado  
	   End 

End 
Go--#SQL
