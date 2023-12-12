

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_EntradasDet_Consignacion' and xType = 'P')
    Drop Proc spp_Mtto_EntradasDet_Consignacion
Go--#SQL
 
  
Create Proc spp_Mtto_EntradasDet_Consignacion 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioEntrada varchar(32), @IdProducto varchar(10), 
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
	Set @iActualizado = 3 

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From EntradasDet_Consignacion (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioEntrada = @FolioEntrada 
								 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon ) 
			  Begin 
				Insert Into EntradasDet_Consignacion 
				(  
					IdEmpresa, IdEstado, IdFarmacia, FolioEntrada, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
					Cant_Recibida, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, 
					ImpteIva, Importe, Status, Actualizado
				 ) 
				 Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @FolioEntrada, @IdProducto, @CodigoEAN, @Renglon, @UnidadDeEntrada, 
					@Cant_Recibida, @Cant_Recibida, @CostoUnitario, @TasaIva, @SubTotal, 
					@ImpteIva, @Importe, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update EntradasDet_Consignacion Set 
					IdEmpresa = @IdEmpresa , IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, FolioEntrada = @FolioEntrada, 
					IdProducto = @IdProducto, CodigoEAN = @CodigoEAN, Renglon = @Renglon, 
					UnidadDeEntrada = @UnidadDeEntrada, Cant_Recibida = @Cant_Recibida, 
					CantidadRecibida = @Cant_Recibida, CostoUnitario = @CostoUnitario, 
					TasaIva = @TasaIva, SubTotal = @SubTotal, ImpteIva = @ImpteIva, 
					Importe = @Importe, Status = @sStatus, Actualizado = @iActualizado
			   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioEntrada = @FolioEntrada 
					 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioEntrada 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update EntradasDet_Consignacion Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioEntrada = @FolioEntrada 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioEntrada + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioEntrada as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
