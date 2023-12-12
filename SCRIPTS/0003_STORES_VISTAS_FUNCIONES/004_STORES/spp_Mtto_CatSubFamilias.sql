If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatSubFamilias' and xType = 'P')
    Drop Proc spp_Mtto_CatSubFamilias
Go--#SQL
  
Create Proc spp_Mtto_CatSubFamilias ( @IdFamilia varchar(4), @IdSubFamilia varchar(4), @Descripcion varchar(52), @iOpcion smallint )
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


	If @IdSubFamilia = '*' 
	  Begin
	    Select @IdSubFamilia = cast( (max(IdSubFamilia) + 1) as varchar) From CatSubFamilias (NoLock)
		Where IdFamilia = @IdFamilia
	  End

	-- Asegurar que IdSubFamilia sea valido y formatear la cadena 
	Set @IdSubFamilia = IsNull(@IdSubFamilia, '1')
	Set @IdSubFamilia = right(replicate('0', 2) + @IdSubFamilia, 2)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatSubFamilias (NoLock) Where IdFamilia = @IdFamilia And IdSubFamilia = @IdSubFamilia ) 
			  Begin 
				 Insert Into CatSubFamilias ( IdFamilia, IdSubFamilia, Descripcion, Status, Actualizado ) 
				 Select @IdFamilia, @IdSubFamilia, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatSubFamilias Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdFamilia = @IdFamilia And IdSubFamilia = @IdSubFamilia
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdSubFamilia 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatSubFamilias Set Status = @sStatus, Actualizado = @iActualizado Where IdFamilia = @IdFamilia And IdSubFamilia = @IdSubFamilia
		   Set @sMensaje = 'La información de la SubFamilia ' + @IdSubFamilia + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdFamilia as Clave, @sMensaje as Mensaje 
End
Go--#SQL
