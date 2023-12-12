If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_ALMJ_Pedidos_RC_Det' and xType = 'P')
    Drop Proc spp_Mtto_ALMJ_Pedidos_RC_Det
Go--#SQL

Create Proc spp_Mtto_ALMJ_Pedidos_RC_Det 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdJurisdiccion varchar(3), @IdFarmacia varchar(6), @FolioPedidoRC varchar(32), 
	@IdClaveSSA varchar(10), @Cantidad numeric(14, 4), @iOpcion smallint 
) 
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint,
		@Existencia numeric(14,4)

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 1 
	Set @Existencia = 0

	If @iOpcion = 1 
       Begin 
			-- Se obtiene la Existencia de la Sal 
			Select @Existencia = 0 
--			Select @Existencia = IsNull( Sum( F.Existencia), 0 )
--			From FarmaciaProductos F (NoLock)
--			Inner Join CatProductos P (NoLock) On ( F.IdProducto = P.IdProducto )
--			Where F.IdEmpresa = @IdEmpresa And F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia And P.IdClaveSSa_Sal = @IdClaveSSA

			If Not Exists ( Select * From ALMJ_Pedidos_RC_Det (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdJurisdiccion = @IdJurisdiccion 
								 And IdFarmacia = @IdFarmacia And FolioPedidoRC = @FolioPedidoRC And IdClaveSSA = @IdClaveSSA ) 
			  Begin 
				Insert Into ALMJ_Pedidos_RC_Det ( IdEmpresa, IdEstado, IdJurisdiccion, IdFarmacia, FolioPedidoRC, IdClaveSSA, Cantidad, Existencia, Status, Actualizado ) 
				Select @IdEmpresa, @IdEstado, @IdJurisdiccion, @IdFarmacia, @FolioPedidoRC, @IdClaveSSA, @Cantidad, @Existencia, @sStatus, @iActualizado 
              End 

		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedidoRC 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update ALMJ_Pedidos_RC_Det Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdJurisdiccion = @IdJurisdiccion  And IdFarmacia = @IdFarmacia 
				  And FolioPedidoRC = @FolioPedidoRC 
				  And IdClaveSSA = @IdClaveSSA

		   Set @sMensaje = 'La información del Folio ' + @FolioPedidoRC + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioPedidoRC as Clave, @sMensaje as Mensaje 
End
Go--#SQL
