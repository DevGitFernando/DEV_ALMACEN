

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_PedidosEnvioEnc' and xType = 'P')
    Drop Proc spp_Mtto_PedidosEnvioEnc
Go--#SQL

  
Create Proc spp_Mtto_PedidosEnvioEnc 
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioPedido varchar(32), 
	@FolioMovtoInv varchar(22), @IdPersonal varchar(6), @IdDistribuidor varchar(6), 
	@ReferenciaPedido varchar(22), 
	@Observaciones varchar(102), 
    @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4), 
	@iOpcion smallint 
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
	Set @iActualizado = 0	
	--Set @FolioMovtoInv = ''	


	If @FolioPedido = '*' 
	  Begin 
		Select @FolioPedido = cast( (max(FolioPedido) + 1) as varchar)  
		From PedidosEnvioEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	  End 

	-- Asegurar que FolioPedido sea valido y formatear la cadena 
	Set @FolioPedido = IsNull(@FolioPedido, '1')
	Set @FolioPedido = right(replicate('0', 8) + @FolioPedido, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From PedidosEnvioEnc (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido ) 
			  Begin 
				 Insert Into PedidosEnvioEnc 
					 ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, FolioMovtoInv, IdPersonal, FechaRegistro,  
					   IdDistribuidor, ReferenciaPedido, Observaciones, SubTotal, Iva, Total, Status, Actualizado
					 ) 
				 Select 
					   @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @FolioMovtoInv, @IdPersonal, GetDate(),  
					   @IdDistribuidor, @ReferenciaPedido, @Observaciones,@SubTotal, @Iva, @Total, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update PedidosEnvioEnc Set 
					IdEmpresa = @IdEmpresa, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, 
					FolioPedido = @FolioPedido, FolioMovtoInv = @FolioMovtoInv, 
					IdPersonal = @IdPersonal, IdDistribuidor = @IdDistribuidor, ReferenciaPedido = @ReferenciaPedido, 
					Observaciones = @Observaciones, 
					SubTotal = @SubTotal, Iva = @Iva, Total = @Total, 
					Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update PedidosEnvioEnc Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido
 
		   Set @sMensaje = 'La información del Folio ' + @FolioPedido + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioPedido as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
