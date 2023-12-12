------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_OrdenesCompra_CodigosEAN_Det' and xType = 'P')
    Drop Proc spp_Mtto_COM_OCEN_OrdenesCompra_CodigosEAN_Det
Go--#SQL
   
Create Proc spp_Mtto_COM_OCEN_OrdenesCompra_CodigosEAN_Det 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(4) = '', @IdFarmacia varchar(6) = '', @FolioOrden varchar(32) = '', 
	@Partida int = 0,  
	@IdProducto varchar(8) = '', @CodigoEAN varchar(30) = '', 
	@AplicaCosto bit = 0, 
    @Cantidad numeric(14, 4) = 0, @Precio numeric(14, 4) = 0, @Descuento numeric(14, 4) = 0, @TasaIva numeric(14, 4) = 0, @Iva numeric(14, 4) = 0,
	@PrecioUnitario numeric(14, 4) = 0, @Importe numeric(14, 4) = 0, 
	@CantidadSobreCompra int = 0, @ObservacionesSobreCompra varchar(100) = '',
	@IdPersonalRegistra varchar(8) = '', @Observaciones Varchar(200) = '', @dComisionNegociadora numeric(14, 4) = 0.0000
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
	

	If Not Exists ( Select * From COM_OCEN_OrdenesCompra_CodigosEAN_Det (NoLock) 
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden 
							And Partida = @Partida And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN  ) 
		Begin 
		Insert Into COM_OCEN_OrdenesCompra_CodigosEAN_Det 
		(  
			IdEmpresa, IdEstado, IdFarmacia, FolioOrden, Partida, IdProducto, CodigoEAN, 
			Cantidad, Precio, Descuento, TasaIva, Iva, PrecioUnitario, ImpteIva, Importe, Status, Actualizado,
			CantidadSobreCompra, ObservacionesSobreCompra, ComisionNegociadora, AplicaCosto
			) 
			Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @FolioOrden, @Partida, @IdProducto, @CodigoEAN,  
			@Cantidad, @Precio, @Descuento, @TasaIva, @Iva, @PrecioUnitario, 
			((@PrecioUnitario * @Cantidad) * (@TasaIva/100.00) ), @Importe, @sStatus, @iActualizado,
			@CantidadSobreCompra, @ObservacionesSobreCompra, @dComisionNegociadora, @AplicaCosto 
        End 
		   
	Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioOrden
			

	If ( @Observaciones <> '' )
	Begin 

		Insert Into COM_OCEN_OrdenesCompra_CodigosEAN_Historico 
			(	IdEmpresa, IdEstado, IdFarmacia, FolioOrden, IdProducto, CodigoEAN, Cantidad, Precio, Descuento, TasaIva, Iva,
				PrecioUnitario, ImpteIva, Importe, IdPersonalRegistra, Observaciones, FechaActualizacion, Status, Actualizado ) 
		Select	IdEmpresa, IdEstado, IdFarmacia,FolioOrden, IdProducto, CodigoEAN, Cantidad, Precio, Descuento, TasaIva, Iva,
				PrecioUnitario, ImpteIva, Importe, @IdPersonalRegistra, @Observaciones, Getdate(), Status, Actualizado 	
		From COM_OCEN_OrdenesCompra_CodigosEAN_Det (Nolock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioOrden = @FolioOrden 
			And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN 

	End


	-- Regresar la Clave Generada 
    Select @FolioOrden as Clave, @sMensaje as Mensaje 


End 
Go--#SQL	
