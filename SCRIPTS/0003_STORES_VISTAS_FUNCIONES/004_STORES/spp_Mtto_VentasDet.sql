------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Mtto_VentasDet' and xType = 'P' )
    Drop Proc spp_Mtto_VentasDet
Go--#SQL
  
Create Proc spp_Mtto_VentasDet 
(
	@IdEmpresa varchar(3), 
	@IdEstado varchar(4), @IdFarmacia varchar(6), @FolioVenta varchar(32), @IdProducto varchar(10), 
    @CodigoEAN varchar(32), @Renglon int, @UnidadDeSalida smallint, @Cant_Entregada numeric(14, 4),
	@Cant_Devuelta numeric(14,4), @CantVendida numeric(14, 4), @CostoUnitario numeric(14, 4), @PrecioUnitario numeric(14, 4), 
	@ImpteIva numeric(14, 4), @TasaIva numeric(14, 4), @PorcDescto numeric(14, 4), @ImpteDescto numeric(14, 4), @iOpcion smallint 	 
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
	Set @UnidadDeSalida = 1
	Set @Cant_Devuelta = 0
	--- Set @CostoUnitario = 0
	Set @PorcDescto = 0
	Set @ImpteDescto = 0 
	Set @CostoUnitario = round(@CostoUnitario, 2, 0) 

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From VentasDet (NoLock) 
						   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 
								 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon ) 
			  Begin 
				Insert Into VentasDet 
				(  
					IdEmpresa, 
					IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon, UnidadDeSalida, 
					Cant_Entregada, Cant_Devuelta, CantidadVendida, CostoUnitario, PrecioUnitario, ImpteIva, 
					TasaIva, PorcDescto, ImpteDescto, Status, Actualizado
				 ) 
				 Select @IdEmpresa, 
					@IdEstado, @IdFarmacia, @FolioVenta, @IdProducto, @CodigoEAN, @Renglon, @UnidadDeSalida, 
					@Cant_Entregada, @Cant_Devuelta, @CantVendida, @CostoUnitario, @PrecioUnitario, @ImpteIva,
					@TasaIva, @PorcDescto, @ImpteDescto, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update VentasDet Set 
					-- IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, FolioVenta = @FolioVenta, 
					-- IdProducto = @IdProducto, CodigoEAN = @CodigoEAN, Renglon = @Renglon, 
					UnidadDeSalida = @UnidadDeSalida, Cant_Entregada = @Cant_Entregada, 
					Cant_Devuelta = @Cant_Devuelta, CantidadVendida = @CantVendida, CostoUnitario = @CostoUnitario, 
					PrecioUnitario = @PrecioUnitario, ImpteIva = @ImpteIva,  TasaIva = @TasaIva,
					PorcDescto = @PorcDescto, ImpteDescto = @ImpteDescto, Status = @sStatus, Actualizado = @iActualizado
			   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 
					 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el folio ' + @FolioVenta 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update VentasDet Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And Renglon = @Renglon 

		   Set @sMensaje = 'La información del Folio ' + @FolioVenta + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioVenta as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
