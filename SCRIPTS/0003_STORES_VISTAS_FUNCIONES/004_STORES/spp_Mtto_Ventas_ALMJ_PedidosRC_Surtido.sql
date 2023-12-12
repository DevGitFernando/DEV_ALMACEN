If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Ventas_ALMJ_PedidosRC_Surtido' and xType = 'P' )
    Drop Proc spp_Mtto_Ventas_ALMJ_PedidosRC_Surtido
Go--#SQL
  
Create Proc spp_Mtto_Ventas_ALMJ_PedidosRC_Surtido 
(	@IdEmpresaRC varchar(3), @IdEstadoRC varchar(4), @IdJurisdiccionRC varchar(3), @IdFarmaciaRC varchar(6), @FolioPedidoRC varchar(6),
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioVenta varchar(32),
    @CantidadSurtida numeric(14, 4), @CantidadEntregada numeric(14, 4), @iOpcion smallint 
 )
 With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1),
		@iStatusPedido tinyint,
		@Cantidad numeric(14, 4),
		@iActualizado smallint

	/*Opciones
	Opcion 1.- Insercion
	Opcion 2.- Cancelar ----Eliminar.
	*/
	
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iStatusPedido = 2
	Set @Cantidad = 0
	Set @iActualizado = 0
	
	If @iOpcion = 1 
       Begin 

		-- Se obtiene la cantidad que se ha entregado hasta el momento
		Select @Cantidad = ( Select IsNull( Sum ( CantidadSurtida ), 0 ) )
		From Ventas_ALMJ_PedidosRC_Surtido(NoLock)
		Where IdEmpresaRC = @IdEmpresaRC and IdEstadoRC = @IdEstadoRC And IdFarmaciaRC = @IdFarmaciaRC And FolioPedidoRC = @FolioPedidoRC
		And IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

		-- Se inserta en la tabla Ventas_ALMJ_PedidosRC_Surtido
		If Not Exists ( Select * From Ventas_ALMJ_PedidosRC_Surtido (NoLock) 
					   Where IdEmpresaRC = @IdEmpresaRC and IdEstadoRC = @IdEstadoRC And IdFarmaciaRC = @IdFarmaciaRC And FolioPedidoRC = @FolioPedidoRC
						And IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta ) 
		  Begin 
			 Insert Into Ventas_ALMJ_PedidosRC_Surtido 
				 ( IdEmpresaRC, IdEstadoRC, IdFarmaciaRC, FolioPedidoRC, IdEmpresa, IdEstado, IdFarmacia, FolioVenta, CantidadSurtida, Status, Actualizado ) 
			 Select @IdEmpresaRC, @IdEstadoRC, @IdFarmaciaRC, @FolioPedidoRC, @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, @CantidadEntregada, @sStatus, @iActualizado 

			-- Si la cantidad que se esta entregando mas la cantidad que ya se entrego es mayor o igual que la surtida, se cambia el status a 3 ( Surtido Completo).
			If ( @CantidadEntregada + @Cantidad ) >= @CantidadSurtida
			  Set @iStatusPedido = 3

			-- Se actualizan los campos StatusPedido y Actualizado de la tabla ALMJ_Pedidos_RC
			Update ALMJ_Pedidos_RC
			Set StatusPedido = @iStatusPedido, Actualizado = 0
			Where IdEmpresa = @IdEmpresaRC and IdEstado = @IdEstadoRC And IdJurisdiccion = @IdJurisdiccionRC 
			And IdFarmacia = @IdFarmaciaRC And FolioPedidoRC = @FolioPedidoRC			

		  End 

		Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioVenta 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update Ventas_ALMJ_PedidosRC_Surtido Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresaRC = @IdEmpresaRC and IdEstadoRC = @IdEstadoRC And IdFarmaciaRC = @IdFarmaciaRC And FolioPedidoRC = @FolioPedidoRC
			And IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
 
			Set @sMensaje = 'La información del Folio ' + @FolioVenta + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioVenta as Clave, @sMensaje as Mensaje 
End
Go--#SQL