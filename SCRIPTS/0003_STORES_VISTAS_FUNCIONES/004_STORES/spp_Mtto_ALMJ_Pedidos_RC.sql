If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_ALMJ_Pedidos_RC' and xType = 'P')
    Drop Proc spp_Mtto_ALMJ_Pedidos_RC
Go--#SQL
  
Create Proc spp_Mtto_ALMJ_Pedidos_RC 
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdJurisdiccion varchar(3), @IdFarmacia varchar(6), @FolioPedidoRC varchar(32), 
	@IdCentro varchar(4), @Entrego varchar(102), @FechaSistema datetime, @FechaCaptura datetime, @IdPersonal varchar(6), @iOpcion smallint 
) 
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 1 -- En esta tabla se guarda con 1.


	If @FolioPedidoRC = '*' 
	  Begin
		Select @FolioPedidoRC = cast( (max(FolioPedidoRC) + 1) as varchar)  
		From ALMJ_Pedidos_RC (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado -- And IdJurisdiccion = @IdJurisdiccion 
			And IdFarmacia = @IdFarmacia
	  End

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioPedidoRC = IsNull(@FolioPedidoRC, '1')
	Set @FolioPedidoRC = right(replicate('0', 6) + @FolioPedidoRC, 6)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From ALMJ_Pedidos_RC (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado -- And IdJurisdiccion = @IdJurisdiccion
						   And IdFarmacia = @IdFarmacia And FolioPedidoRC = @FolioPedidoRC ) 
			  Begin 
				 Insert Into ALMJ_Pedidos_RC 
					 (  IdEmpresa, IdEstado, IdJurisdiccion, IdFarmacia, FolioPedidoRC, IdCentro, Entrego, FechaSistema, FechaCaptura, 
						IdPersonal, Status, Actualizado
					 ) 
				 Select @IdEmpresa, @IdEstado, @IdJurisdiccion, @IdFarmacia, @FolioPedidoRC, @IdCentro, @Entrego, @FechaSistema, @FechaCaptura,
						@IdPersonal, @sStatus, @iActualizado 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedidoRC 

	   End 
    Else 
      Begin 
		Set @sStatus = 'C' 
		Update ALMJ_Pedidos_RC Set Status = @sStatus, Actualizado = @iActualizado 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado -- And IdJurisdiccion = @IdJurisdiccion 
			And IdFarmacia = @IdFarmacia And FolioPedidoRC = @FolioPedidoRC
 
		-- Se Cancela el detalle
		Update ALMJ_Pedidos_RC_Det Set Status = @sStatus, Actualizado = @iActualizado 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado -- And IdJurisdiccion = @IdJurisdiccion 
			And IdFarmacia = @IdFarmacia And FolioPedidoRC = @FolioPedidoRC				

		Set @sMensaje = 'La información del Folio ' + @FolioPedidoRC + ' ha sido cancelada satisfactoriamente.' 
	  End 

	-- Regresar la Clave Generada
    Select @FolioPedidoRC as Folio, @sMensaje as Mensaje 
End
Go--#SQL