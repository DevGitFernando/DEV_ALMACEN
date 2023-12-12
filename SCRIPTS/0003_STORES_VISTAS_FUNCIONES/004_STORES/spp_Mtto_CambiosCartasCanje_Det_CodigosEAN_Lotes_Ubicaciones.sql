
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'P')
    Drop Proc spp_Mtto_CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones
Go--#SQL

  
Create Proc spp_Mtto_CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @IdSubFarmacia varchar(2), @FolioCambio varchar(32), 
	@IdProducto varchar(10), @CodigoEAN varchar(32), @ClaveLote varchar(32), -- @iRenglon int, 
	@Cantidad numeric(14, 4), 
	@IdPasillo int, @IdEstante int, @IdEntrepaño int, 
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

		   If Not Exists ( Select * From CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
								And FolioCambio = @FolioCambio 
								And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN 
								And ClaveLote = @ClaveLote 
								and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño ) 
			  Begin 
				Insert Into CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones 
					( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioCambio, IdProducto, CodigoEAN, 
					ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Cantidad, EsConsignacion, Status, Actualizado )
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioCambio, @IdProducto, @CodigoEAN, 
					   @ClaveLote, @IdPasillo, @IdEstante, @IdEntrepaño, @Cantidad, @EsConsignacion, @sStatus, @iActualizado 

              End 
--		   Else 
--			  Begin 
--			     
--				Update CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones Set 
--						IdEmpresa = @IdEmpresa, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, FolioCompra = @FolioCompra, 
--						IdProducto = @IdProducto, CodigoEAN = @CodigoEAN, ClaveLote = @ClaveLote, Renglon = @Renglon, 
--						Cant_Recibida = @CantidadRecibida, Status = @sStatus, Actualizado = @iActualizado
--				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra 
--					  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
--					  And Renglon = @Renglon 
-- 
--              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioCambio  
	   End 
    Else 
       Begin 
			Set @sStatus = 'C'
			Update CambiosCartasCanje_Det_CodigosEAN_Lotes_Ubicaciones Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
				And FolioCambio = @FolioCambio And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote -- and Renglon = @iRenglon 
				and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño 

		   Set @sMensaje = 'La información del Folio ' + @FolioCambio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioCambio as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
