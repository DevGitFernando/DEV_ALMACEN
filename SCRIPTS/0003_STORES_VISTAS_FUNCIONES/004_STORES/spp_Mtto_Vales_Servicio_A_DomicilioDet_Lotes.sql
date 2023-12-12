
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vales_Servicio_A_DomicilioDet_Lotes' and xType = 'P')
    Drop Proc spp_Mtto_Vales_Servicio_A_DomicilioDet_Lotes
Go--#SQL

  
Create Proc spp_Mtto_Vales_Servicio_A_DomicilioDet_Lotes 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @IdSubFarmacia varchar(2), @FolioServicioDomicilio varchar(32), 
	@IdProducto varchar(10), @CodigoEAN varchar(32), @ClaveLote varchar(32), @Renglon int, @CantidadRecibida numeric(14, 4), 
    @iOpcion smallint
) 
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,   
	    @EsConsignacion bit 

	
	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	Set @EsConsignacion = (case when @ClaveLote like '%*%' then 1 else 0 end)

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From Vales_Servicio_A_DomicilioDet_Lotes (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
								And FolioServicioDomicilio = @FolioServicioDomicilio
								And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN 
								And ClaveLote = @ClaveLote And Renglon = @Renglon ) 
			  Begin 
				Insert Into Vales_Servicio_A_DomicilioDet_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioServicioDomicilio, IdProducto, CodigoEAN, 
					ClaveLote, Renglon, Cant_Recibida, Cant_Devuelta, CantidadRecibida, EsConsignacion, Status, Actualizado )
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioServicioDomicilio, @IdProducto, @CodigoEAN, 
					   @ClaveLote, @Renglon, @CantidadRecibida, 0, @CantidadRecibida, @EsConsignacion, @sStatus, @iActualizado 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioServicioDomicilio 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C'
			Update Vales_Servicio_A_DomicilioDet_Lotes Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
				And FolioServicioDomicilio = @FolioServicioDomicilio And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
				And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioServicioDomicilio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioServicioDomicilio as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
