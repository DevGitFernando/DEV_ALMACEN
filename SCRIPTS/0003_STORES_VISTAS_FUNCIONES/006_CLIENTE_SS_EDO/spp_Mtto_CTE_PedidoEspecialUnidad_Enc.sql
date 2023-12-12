
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CTE_PedidoEspecialUnidad_Enc' and xType = 'P')
    Drop Proc spp_Mtto_CTE_PedidoEspecialUnidad_Enc
Go--#SQL
  
Create Proc spp_Mtto_CTE_PedidoEspecialUnidad_Enc 
(	@IdEstado varchar(2), @IdFarmacia varchar(4), @FolioPedido varchar(8), @IdUsuario varchar(4), @FechaRequeridaEntrega datetime, 
	@Observaciones varchar(200), @iOpcion smallint )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @FolioPedido = '*' 
	  Begin
		Select @FolioPedido = cast( (max(FolioPedido) + 1) as varchar) 
		From CTE_PedidoEspecialUnidad_Enc (NoLock) 
		Where IdEstado = @IdEstado
	  End

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioPedido = IsNull(@FolioPedido, '1')
	Set @FolioPedido = right(replicate('0', 8) + @FolioPedido, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CTE_PedidoEspecialUnidad_Enc (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido ) 
			  Begin 
				 Insert Into CTE_PedidoEspecialUnidad_Enc ( IdEstado, IdFarmacia, FolioPedido, IdUsuario, FechaRegistro, FechaRequeridaEntrega, Observaciones, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @FolioPedido, @IdUsuario, GetDate(), @FechaRequeridaEntrega, @Observaciones, @sStatus, @iActualizado 
              End 
		   Set @sMensaje = 'El Pedido se guardo satisfactoriamente con la clave ' + @FolioPedido 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CTE_PedidoEspecialUnidad_Enc Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido 
		   Set @sMensaje = 'La información del Pedido ' + @FolioPedido + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioPedido as Folio, @sMensaje as Mensaje 
End
Go--#SQL	

