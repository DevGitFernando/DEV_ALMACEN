
--	Select * From TraspasosDet_Lotes_Ubicaciones (Nolock)

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_TraspasosDet_Lotes_Ubicaciones' and xType = 'P')
    Drop Proc spp_Mtto_TraspasosDet_Lotes_Ubicaciones
Go--#SQL
  
Create Proc spp_Mtto_TraspasosDet_Lotes_Ubicaciones
( 
	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @IdSubFarmacia varchar(2), @FolioTraspaso varchar(32), 
	@IdProducto varchar(10), @CodigoEAN varchar(32), @ClaveLote varchar(32), @Renglon int,
	@IdPasillo int, @IdEstante int, @IdEntrepa�o int, 
	@Cantidad numeric(14, 4), @iOpcion smallint
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

		   If Not Exists ( Select * From TraspasosDet_Lotes_Ubicaciones (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
								And FolioTraspaso = @FolioTraspaso 
								And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote And Renglon = @Renglon
								And IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepa�o = @IdEntrepa�o ) 
			  Begin 
				Insert Into TraspasosDet_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioTraspaso, IdProducto, CodigoEAN, 
					ClaveLote, Renglon, IdPasillo, IdEstante, IdEntrepa�o,
					Cantidad, EsConsignacion, Status, Actualizado )
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FolioTraspaso, @IdProducto, @CodigoEAN, 
					   @ClaveLote, @Renglon, @IdPasillo, @IdEstante, @IdEntrepa�o, 
					   @Cantidad, @EsConsignacion, @sStatus, @iActualizado 

              End 
 
		   Set @sMensaje = 'La informaci�n se guardo satisfactoriamente con la clave ' + @FolioTraspaso 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C'
			Update TraspasosDet_Lotes_Ubicaciones Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
				And FolioTraspaso = @FolioTraspaso And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
				And Renglon = @Renglon And IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepa�o = @IdEntrepa�o

		   Set @sMensaje = 'La informaci�n del Folio ' + @FolioTraspaso + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioTraspaso as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
