
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatEstados_SubFarmacias' and xType = 'P')
    Drop Proc spp_Mtto_CatEstados_SubFarmacias
Go--#SQL
  
Create Proc spp_Mtto_CatEstados_SubFarmacias ( @IdEstado varchar(2), @IdSubFarmacia varchar(4), @Descripcion varchar(102), 
	@EsConsignacion smallint, @iOpcion smallint )
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


	If @IdSubFarmacia = '*' 
	   Select @IdSubFarmacia = cast( (max(IdSubFarmacia) + 1) as varchar) From CatEstados_SubFarmacias (NoLock) Where IdEstado = @IdEstado

	-- Asegurar que IdSubFarmacia sea valido y formatear la cadena 
	Set @IdSubFarmacia = IsNull(@IdSubFarmacia, '1')
	Set @IdSubFarmacia = right(replicate('0', 2) + @IdSubFarmacia, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatEstados_SubFarmacias (NoLock) Where IdEstado = @IdEstado And IdSubFarmacia = @IdSubFarmacia ) 
			  Begin 
				 Insert Into CatEstados_SubFarmacias ( IdEstado, IdSubFarmacia, Descripcion, EsConsignacion, Status, Actualizado ) 
				 Select @IdEstado, @IdSubFarmacia, @Descripcion, @EsConsignacion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatEstados_SubFarmacias Set Descripcion = @Descripcion, EsConsignacion = @EsConsignacion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdSubFarmacia = @IdSubFarmacia  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdSubFarmacia 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatEstados_SubFarmacias Set Status = @sStatus, Actualizado = @iActualizado Where IdEstado = @IdEstado And IdSubFarmacia = @IdSubFarmacia 
		   Set @sMensaje = 'La información de la SubFarmacia ' + @IdSubFarmacia + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdSubFarmacia as Clave, @sMensaje as Mensaje 
End
Go--#SQL
