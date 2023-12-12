-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_Enc_ModificarStatus' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_Enc_ModificarStatus 
Go--#SQL


--Exec spp_Mtto_Pedidos_Cedis_Enc_ModificarStatus @IdEmpresa = '001', @IdEstado = '13', @IdFarmacia = '0004', @FolioPedido = '',  @Status = 'F'

Create Proc spp_Mtto_Pedidos_Cedis_Enc_ModificarStatus
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '0004',     
	@FolioPedido varchar(6) = '003819',  @Status varchar(2) = 'C'
)  
With Encryption 
As 
Begin 
Set NoCount On  	
Declare @sMensaje varchar(1000), @iRows Int, @bContinuar Bit = 0

	Set @sMensaje = 'El pedido no se encuentra activo.'

	Set @iRows = 0

	Set @bContinuar = 0

	Select @bContinuar = 1
	From Pedidos_Cedis_Enc (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioPedido = @FolioPedido And Status In ('A', 'E')

	
	if(@bContinuar = 1)
	Begin
	
		if (@Status ='C')
		Begin
			Select @bContinuar = 0--Count(Distinct(Status))
			From Pedidos_Cedis_Enc_Surtido E
			 Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioPedido = @FolioPedido And E.Status Not In ('C')


			 if (@bContinuar <> 1 ) 
				 Begin
					Set @sMensaje = 'El pedido tienen surtidos activos, no se puede cancelar.'
					Set @bContinuar = 0
				 End
			 Else
				Begin
					Set @sMensaje = 'El pedido se canceló correctamente.'
					Set @bContinuar = 1
				End

		End


		if (@Status ='F')
		Begin
			Select @bContinuar = 0 --@iRows = Count(Distinct(Status))
			From Pedidos_Cedis_Enc_Surtido E
			 Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioPedido = @FolioPedido And E.Status Not In ('E', 'R', 'C')

			 if (@bContinuar <> 1 ) 
				 Begin
					Set @sMensaje = 'El pedido tienen surtidos activos, no se pueden Terminar las claves.'
					Set @bContinuar = 0
				 End
			Else
			Begin
				Set @sMensaje = 'El pedido se terminó correctamente.'
				Set @bContinuar = 1
			End
		End
	End

	if (@bContinuar = 1)
	Begin
		Update Pedidos_Cedis_Enc Set Status = @Status
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioPedido = @FolioPedido
	End

    Select @sMensaje as Mensaje 
    
End 
Go--#SQL   
