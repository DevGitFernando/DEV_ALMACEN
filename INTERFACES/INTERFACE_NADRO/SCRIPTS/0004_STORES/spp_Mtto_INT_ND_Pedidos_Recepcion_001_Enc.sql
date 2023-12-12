------------------------------------------------------------------------------------------------------------------------------------ 
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_Pedidos_Recepcion_001_Enc' And xType = 'P' )
	Drop Proc spp_Mtto_INT_ND_Pedidos_Recepcion_001_Enc 
Go--#SQL 

Create Procedure spp_Mtto_INT_ND_Pedidos_Recepcion_001_Enc 
(	
	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioPedido varchar(32), @FolioPedidoReferencia varchar(32),
	@IdPersonal varchar(6), @FechaRegistro datetime, @IdProveedor varchar(6), @ReferenciaFolioPedido varchar(22), 
	@Observaciones varchar(102), @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4), @FechaPromesaEntrega datetime, 
	@EsNoSolicitado smallint = 0, 
	@iOpcion smallint 
) 
With Encryption 
As
Begin 
Set NoCount On
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@FolioMovtoInv varchar(22),
		@PorcPuntualidad int

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @FolioMovtoInv = 'EOC'
	Set @PorcPuntualidad = 0

	-- Se obtiene el Folio de Orden de Compra
	If @FolioPedido = '*' 
	  Begin 
		Select @FolioPedido = cast( (max(FolioPedido) + 1) as varchar)  
		From INT_ND_PedidosEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	  End 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioPedido = IsNull(@FolioPedido, '1')
	Set @FolioPedido = right(replicate('0', 8) + @FolioPedido, 8)

	-- Se obtiene el Porcentaje de Puntualidad.
	Select @PorcPuntualidad = Datediff( Day, GetDate(), @FechaPromesaEntrega ) 

	If @iOpcion = 1
	  Begin
		If Not Exists ( Select * From INT_ND_PedidosEnc(Nolock) 
						Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido ) 
		  Begin
			Insert Into INT_ND_PedidosEnc
				( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, FolioPedidoReferencia, FolioMovtoInv, IdPersonal, FechaRegistro,
				  IdProveedor, ReferenciaFolioPedido, EsNoSolicitado, Observaciones, 
				  SubTotal, Iva, Total, FechaPromesaEntrega, Status, Actualizado 
				)
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @FolioPedidoReferencia, @FolioMovtoInv, @IdPersonal, @FechaRegistro,
				@IdProveedor, @ReferenciaFolioPedido, @EsNoSolicitado, @Observaciones,  
				@SubTotal, @Iva, @Total, @FechaPromesaEntrega, @sStatus, @iActualizado 

			Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido
		  End
	  End
	Else
	  Begin
		Set @sStatus = 'C'

		Update INT_ND_PedidosEnc Set Status = @sStatus, Actualizado = @iActualizado
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido
 
		Set @sMensaje = 'La información del Folio ' + @FolioPedido + ' ha sido cancelada satisfactoriamente.' 
			
	  End

	-- Regresar la Clave Generada
    Select @FolioPedido as Clave, @sMensaje as Mensaje 

End
Go--#SQL 

