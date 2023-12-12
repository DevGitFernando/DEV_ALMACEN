
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_ComprasDet_Lotes_Ubicaciones_Ubicaciones' and xType = 'P')
    Drop Proc spp_Mtto_ComprasDet_Lotes_Ubicaciones_Ubicaciones
Go--#SQL

  
Create Proc spp_Mtto_ComprasDet_Lotes_Ubicaciones_Ubicaciones 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @IdSubFarmacia varchar(2), @FolioCompra varchar(32), 
	@IdProducto varchar(10), @CodigoEAN varchar(32), @ClaveLote varchar(32), @iRenglon int, 
	@IdPasillo int, @IdEstante int, @IdEntrepaño int, 
	@CantidadRecibida numeric(14, 4), @iOpcion smallint
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

		   If Not Exists ( Select * From ComprasDet_Lotes_Ubicaciones (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
								And FolioCompra = @FolioCompra 
								And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN 
								And ClaveLote = @ClaveLote 
								and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño ) 
			  Begin 
				Insert Into ComprasDet_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioCompra, IdProducto, CodigoEAN, 
					ClaveLote, Renglon, IdPasillo, IdEstante, IdEntrepaño, Cant_Recibida, Cant_Devuelta, CantidadRecibida, EsConsignacion, Status, Actualizado )
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioCompra, @IdProducto, @CodigoEAN, 
					   @ClaveLote, @iRenglon, @IdPasillo, @IdEstante, @IdEntrepaño, @CantidadRecibida, 0, @CantidadRecibida, @EsConsignacion, @sStatus, @iActualizado 

              End 
--		   Else 
--			  Begin 
--			     
--				Update ComprasDet_Lotes_Ubicaciones Set 
--						IdEmpresa = @IdEmpresa, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, FolioCompra = @FolioCompra, 
--						IdProducto = @IdProducto, CodigoEAN = @CodigoEAN, ClaveLote = @ClaveLote, Renglon = @Renglon, 
--						Cant_Recibida = @CantidadRecibida, Status = @sStatus, Actualizado = @iActualizado
--				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra 
--					  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
--					  And Renglon = @Renglon 
-- 
--              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioCompra 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C'
			Update ComprasDet_Lotes_Ubicaciones Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
				And FolioCompra = @FolioCompra And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote and Renglon = @iRenglon 
				and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño 

		   Set @sMensaje = 'La información del Folio ' + @FolioCompra + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioCompra as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
