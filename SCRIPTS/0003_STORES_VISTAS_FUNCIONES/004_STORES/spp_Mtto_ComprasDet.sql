
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_ComprasDet' and xType = 'P')
    Drop Proc spp_Mtto_ComprasDet
Go--#SQL
  
Create Proc spp_Mtto_ComprasDet 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioCompra varchar(32), @IdProducto varchar(10), 
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

		   If Not Exists ( Select * From ComprasDet (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra 
								 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon ) 
			  Begin 
				Insert Into ComprasDet 
				(  
					IdEmpresa, IdEstado, IdFarmacia, FolioCompra, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
					Cant_Recibida, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, 
					ImpteIva, Importe, Status, Actualizado
				 ) 
				 Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @FolioCompra, @IdProducto, @CodigoEAN, @Renglon, @UnidadDeEntrada, 
					@Cant_Recibida, @Cant_Recibida, @CostoUnitario, @TasaIva, @SubTotal, 
					@ImpteIva, @Importe, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update ComprasDet Set 
					IdEmpresa = @IdEmpresa , IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, FolioCompra = @FolioCompra, 
					IdProducto = @IdProducto, CodigoEAN = @CodigoEAN, Renglon = @Renglon, 
					UnidadDeEntrada = @UnidadDeEntrada, Cant_Recibida = @Cant_Recibida, 
					CantidadRecibida = @Cant_Recibida, CostoUnitario = @CostoUnitario, 
					TasaIva = @TasaIva, SubTotal = @SubTotal, ImpteIva = @ImpteIva, 
					Importe = @Importe, Status = @sStatus, Actualizado = @iActualizado
			   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra 
					 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioCompra 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update ComprasDet Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioCompra + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioCompra as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
