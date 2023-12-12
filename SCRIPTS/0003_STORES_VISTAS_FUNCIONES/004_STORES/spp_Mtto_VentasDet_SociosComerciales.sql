

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_VentasDet_SociosComerciales' and xType = 'P')
    Drop Proc spp_Mtto_VentasDet_SociosComerciales
Go--#SQL
 
  
Create Proc spp_Mtto_VentasDet_SociosComerciales 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioVenta varchar(32), @IdProducto varchar(10), 
    @CodigoEAN varchar(32), @Renglon int, @UnidadDeEntrada smallint, @Cant_Vendida numeric(14, 4),
	@Precio numeric(14, 4), @TasaIva numeric(14, 4), @SubTotal numeric(14, 4), @ImpteIva numeric(14, 4), 
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

		   If Not Exists ( Select * From VentasDet_SociosComerciales (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
								 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon ) 
			  Begin 
				Insert Into VentasDet_SociosComerciales 
				(  
					IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
					Cant_Vendida, CantidadVendida, Precio, TasaIva, SubTotal, 
					ImpteIva, Importe, Status, Actualizado
				 ) 
				 Select 
					@IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, @IdProducto, @CodigoEAN, @Renglon, @UnidadDeEntrada, 
					@Cant_Vendida, @Cant_Vendida, @Precio, @TasaIva, @SubTotal, 
					@ImpteIva, @Importe, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update VentasDet_SociosComerciales Set 
					IdEmpresa = @IdEmpresa , IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, FolioVenta = @FolioVenta, 
					IdProducto = @IdProducto, CodigoEAN = @CodigoEAN, Renglon = @Renglon, 
					UnidadDeEntrada = @UnidadDeEntrada, Cant_Vendida = @Cant_Vendida, 
					CantidadVendida = @Cant_Vendida, Precio = @Precio, 
					TasaIva = @TasaIva, SubTotal = @SubTotal, ImpteIva = @ImpteIva, 
					Importe = @Importe, Status = @sStatus, Actualizado = @iActualizado
			   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 
					 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioVenta 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update VentasDet_SociosComerciales Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioVenta + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioVenta as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
