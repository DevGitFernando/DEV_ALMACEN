
--	Select * From EntradasDet_Consignacion_Lotes_Ubicaciones (Nolock)

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_EntradasDet_Consignacion_Lotes_Ubicaciones' and xType = 'P')
    Drop Proc spp_Mtto_EntradasDet_Consignacion_Lotes_Ubicaciones
Go--#SQL
  
Create Proc spp_Mtto_EntradasDet_Consignacion_Lotes_Ubicaciones
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @IdSubFarmacia varchar(2), @FolioEntrada varchar(32), 
	@IdProducto varchar(10), @CodigoEAN varchar(32), @ClaveLote varchar(32),
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
	Set @iActualizado = 3 
	Set @EsConsignacion = (case when @ClaveLote like '%*%' then 1 else 0 end)

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From EntradasDet_Consignacion_Lotes_Ubicaciones (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
								And FolioEntrada = @FolioEntrada 
								And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote
								And IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño ) 
			  Begin 
				Insert Into EntradasDet_Consignacion_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioEntrada, IdProducto, CodigoEAN, 
					ClaveLote, IdPasillo, IdEstante, IdEntrepaño,
					Cant_Recibida, Cant_Devuelta, CantidadRecibida, EsConsignacion, Status, Actualizado )
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioEntrada, @IdProducto, @CodigoEAN, 
					   @ClaveLote, @IdPasillo, @IdEstante, @IdEntrepaño, 
					   @CantidadRecibida, 0, @CantidadRecibida, @EsConsignacion, @sStatus, @iActualizado 

              End 
--		   Else 
--			  Begin 
--			     
--				Update EntradasDet_Consignacion_Lotes_Ubicaciones Set 
--						IdEmpresa = @IdEmpresa, IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, FolioEntrada = @FolioEntrada, 
--						IdProducto = @IdProducto, CodigoEAN = @CodigoEAN, ClaveLote = @ClaveLote, Renglon = @Renglon, 
--						Cant_Recibida = @CantidadRecibida, Status = @sStatus, Actualizado = @iActualizado
--				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioEntrada = @FolioEntrada 
--					  And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
--					  And Renglon = @Renglon 
-- 
--              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioEntrada 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C'
			Update EntradasDet_Consignacion_Lotes_Ubicaciones Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
				And FolioEntrada = @FolioEntrada And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
				And IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño

		   Set @sMensaje = 'La información del Folio ' + @FolioEntrada + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioEntrada as Folio, @sMensaje as Mensaje 
End
Go--#SQL 	
