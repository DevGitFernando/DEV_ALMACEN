
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CTE_PedidoEspecialRegional_Det' and xType = 'P')
    Drop Proc spp_Mtto_CTE_PedidoEspecialRegional_Det
Go--#SQL
  
Create Proc spp_Mtto_CTE_PedidoEspecialRegional_Det 
(	@IdEstado varchar(2), @FolioPedido varchar(8), @IdClaveSSA varchar(4), @Cantidad_Solicitada int, @iOpcion smallint )
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
		From CTE_PedidoEspecialRegional_Det (NoLock) 
		Where IdEstado = @IdEstado
	  End

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioPedido = IsNull(@FolioPedido, '1')
	Set @FolioPedido = right(replicate('0', 8) + @FolioPedido, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CTE_PedidoEspecialRegional_Det (NoLock) Where IdEstado = @IdEstado And FolioPedido = @FolioPedido And IdClaveSSA = @IdClaveSSA ) 
			  Begin 
				 Insert Into CTE_PedidoEspecialRegional_Det ( IdEstado, FolioPedido, IdClaveSSA, Cantidad_Solicitada, Status, Actualizado ) 
				 Select @IdEstado, @FolioPedido, @IdClaveSSA, @Cantidad_Solicitada, @sStatus, @iActualizado 
              End 
		   Set @sMensaje = 'El Pedido se guardo satisfactoriamente con la clave ' + @FolioPedido 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CTE_PedidoEspecialRegional_Det Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEstado = @IdEstado And FolioPedido = @FolioPedido And IdClaveSSA = @IdClaveSSA
		   Set @sMensaje = 'La información del Pedido ' + @FolioPedido + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioPedido as Folio, @sMensaje as Mensaje 
End
Go--#SQL


