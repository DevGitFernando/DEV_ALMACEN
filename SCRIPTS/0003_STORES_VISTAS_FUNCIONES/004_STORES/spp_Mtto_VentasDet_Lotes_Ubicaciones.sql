If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_VentasDet_Lotes_Ubicaciones' and xType = 'P')
    Drop Proc spp_Mtto_VentasDet_Lotes_Ubicaciones
Go--#SQL
  
Create Proc spp_Mtto_VentasDet_Lotes_Ubicaciones 
( 
	@IdEmpresa varchar(4), 
	@IdEstado varchar(4), @IdFarmacia varchar(6), @IdSubFarmacia varchar(2), @FolioVenta varchar(32), @IdProducto varchar(10), 
    @CodigoEAN varchar(32), @ClaveLote varchar(32), @Renglon int, 
    @IdPasillo int, @IdEstante int, @IdEntrepaño int,
	@CantidadVendida numeric(14, 4), @iOpcion smallint
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

		   If Not Exists ( Select * From VentasDet_Lotes_Ubicaciones (NoLock) 
						   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia
								 And FolioVenta = @FolioVenta 
								 And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN 
								 And ClaveLote = @ClaveLote and Renglon = @Renglon 
								 and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño ) 
			  Begin 
				Insert Into VentasDet_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioVenta, IdProducto, CodigoEAN, 
					ClaveLote, Renglon, IdPasillo, IdEstante, IdEntrepaño,
					Cant_Vendida, Cant_Devuelta, CantidadVendida, EsConsignacion, Status, Actualizado ) 
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioVenta, @IdProducto, @CodigoEAN, 
					   @ClaveLote, @Renglon, @IdPasillo, @IdEstante, @IdEntrepaño, 
					   @CantidadVendida, 0, @CantidadVendida, @EsConsignacion, @sStatus, @iActualizado 
              End 
		   Else 
--------------------------------------------------------------------------------------------------------------------------------------
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioVenta 
	   End 
    Else 
       Begin 

			Set @sStatus = 'C'

			Update VentasDet_Lotes_Ubicaciones Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia
				  And FolioVenta = @FolioVenta 
				  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote and Renglon = @Renglon 
				  and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño 

		   Set @sMensaje = 'La información del Folio ' + @FolioVenta + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioVenta as Clave, @sMensaje as Mensaje 
End
Go--#SQL
