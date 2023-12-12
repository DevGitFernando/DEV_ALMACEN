


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_EliminarAsignacionPedido' And xType = 'P' ) 
	Drop Proc spp_Mtto_COM_EliminarAsignacionPedido
Go--#SQL

Create Procedure spp_Mtto_COM_EliminarAsignacionPedido( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@IdTipoPedido varchar(2), @FolioPedido varchar(6), @IdPersonal varchar(4), @EsCentral tinyint )
As
Begin
	Declare @sMensaje varchar(1000), @Status varchar(1), @iStatusPedido tinyint  

	Set @sMensaje = ''
	Set @Status = 'R'

	

	-- Se Borra en la tabla COM_OCEN_AsignacionDePedidosCompradores
	If Exists ( Select FolioPedido From COM_OCEN_AsignacionDePedidosCompradores (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		And IdTipoPedido = @IdTipoPedido And FolioPedido = @FolioPedido And IdPersonal = @IdPersonal And EsCentral = @EsCentral )
	 Begin
		Update COM_OCEN_AsignacionDePedidosCompradores Set StatusPedido = 2, Status = @Status
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		And IdTipoPedido = @IdTipoPedido And FolioPedido = @FolioPedido And IdPersonal = @IdPersonal And EsCentral = @EsCentral
		
		Set @sMensaje = 'La información se Proceso Satisfactoriamente '  				
	 End	

	-- Regresar el Mensaje Generado
    Select @sMensaje as Mensaje
End
Go--#SQL
