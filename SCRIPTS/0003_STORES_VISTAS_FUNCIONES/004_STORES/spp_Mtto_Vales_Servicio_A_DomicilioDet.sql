
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vales_Servicio_A_DomicilioDet' and xType = 'P')
    Drop Proc spp_Mtto_Vales_Servicio_A_DomicilioDet
Go--#SQL
  
Create Proc spp_Mtto_Vales_Servicio_A_DomicilioDet 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioServicioDomicilio varchar(32), @IdProducto varchar(10), 
    @CodigoEAN varchar(32), @Renglon int, @UnidadDeEntrada smallint, @Cant_Recibida numeric(14, 4),
	@CostoUnitario numeric(14, 4), @TasaIva numeric(14, 4), @SubTotal numeric(14, 4), @ImpteIva numeric(14, 4), 
    @Importe numeric(14, 4), @iOpcion smallint 
)
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

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From Vales_Servicio_A_DomicilioDet (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
						   And FolioServicioDomicilio = @FolioServicioDomicilio And IdProducto = @IdProducto 
						   And CodigoEAN = @CodigoEAN And Renglon = @Renglon ) 
			  Begin 
				Insert Into Vales_Servicio_A_DomicilioDet 
				(  
					IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
					Cant_Recibida, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, 
					ImpteIva, Importe, Status, Actualizado
				 ) 
				 Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @FolioServicioDomicilio, @IdProducto, @CodigoEAN, @Renglon, @UnidadDeEntrada, 
					@Cant_Recibida, @Cant_Recibida, @CostoUnitario, @TasaIva, @SubTotal, 
					@ImpteIva, @Importe, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update Vales_Servicio_A_DomicilioDet Set 
					IdEmpresa = @IdEmpresa , IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, FolioServicioDomicilio = @FolioServicioDomicilio, 
					IdProducto = @IdProducto, CodigoEAN = @CodigoEAN, Renglon = @Renglon, 
					UnidadDeEntrada = @UnidadDeEntrada, Cant_Recibida = @Cant_Recibida, 
					CantidadRecibida = @Cant_Recibida, CostoUnitario = @CostoUnitario, 
					TasaIva = @TasaIva, SubTotal = @SubTotal, ImpteIva = @ImpteIva, 
					Importe = @Importe, Status = @sStatus, Actualizado = @iActualizado
			   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioServicioDomicilio = @FolioServicioDomicilio 
					 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioServicioDomicilio 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update Vales_Servicio_A_DomicilioDet Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioServicioDomicilio = @FolioServicioDomicilio 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioServicioDomicilio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioServicioDomicilio as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
