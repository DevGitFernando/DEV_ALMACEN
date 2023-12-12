
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_ValesDet' and xType = 'P')
    Drop Proc spp_Mtto_ValesDet
Go--#SQL
  
Create Proc spp_Mtto_ValesDet 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @Folio varchar(32), @IdProducto varchar(10), 
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

		   If Not Exists ( Select * From ValesDet (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio 
								 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon ) 
			  Begin 
				Insert Into ValesDet 
				(  
					IdEmpresa, IdEstado, IdFarmacia, Folio, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
					Cant_Recibida, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, 
					ImpteIva, Importe, Status, Actualizado
				 ) 
				 Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @IdProducto, @CodigoEAN, @Renglon, @UnidadDeEntrada, 
					@Cant_Recibida, @Cant_Recibida, @CostoUnitario, @TasaIva, @SubTotal, 
					@ImpteIva, @Importe, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update ValesDet Set 
					IdEmpresa = @IdEmpresa , IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, Folio = @Folio, 
					IdProducto = @IdProducto, CodigoEAN = @CodigoEAN, Renglon = @Renglon, 
					UnidadDeEntrada = @UnidadDeEntrada, Cant_Recibida = @Cant_Recibida, 
					CantidadRecibida = @Cant_Recibida, CostoUnitario = @CostoUnitario, 
					TasaIva = @TasaIva, SubTotal = @SubTotal, ImpteIva = @ImpteIva, 
					Importe = @Importe, Status = @sStatus, Actualizado = @iActualizado
			   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio 
					 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @Folio 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update ValesDet Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @Folio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @Folio as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
