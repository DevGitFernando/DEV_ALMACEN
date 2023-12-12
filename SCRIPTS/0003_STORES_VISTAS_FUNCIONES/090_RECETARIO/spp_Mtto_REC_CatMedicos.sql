
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_REC_CatMedicos' and xType = 'P')
    Drop Proc spp_Mtto_REC_CatMedicos
Go--#SQL
  
Create Proc spp_Mtto_REC_CatMedicos ( @IdEstado varchar(2), @IdFarmacia varchar(6), @IdMedico varchar(6), @Nombre varchar(52), 
								  @ApPaterno varchar(52), @ApMaterno varchar(52), @NumCedula varchar(32), @iOpcion smallint )
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


	If @IdMedico = '*'
	 Begin 
		Select @IdMedico = cast( (max(IdMedico) + 1) as varchar)  
		From REC_CatMedicos (NoLock) 
		Where IdEstado = @IdEstado And @IdFarmacia = @IdFarmacia
	 End

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdMedico = IsNull(@IdMedico, '1')
	Set @IdMedico = right(replicate('0', 6) + @IdMedico, 6)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From REC_CatMedicos (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdMedico = @IdMedico ) 
			  Begin 
				 Insert Into REC_CatMedicos ( IdEstado, IdFarmacia, IdMedico, Nombre, ApPaterno, ApMaterno, NumCedula, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdMedico, @Nombre, @ApPaterno, @ApMaterno, @NumCedula, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update REC_CatMedicos Set Nombre = @Nombre, ApPaterno = @ApPaterno, ApMaterno = @ApMaterno, NumCedula = @NumCedula,
									   Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdMedico = @IdMedico  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdMedico 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update REC_CatMedicos Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdMedico = @IdMedico 
		   Set @sMensaje = 'La información del Medico ' + @IdMedico + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdMedico as Clave, @sMensaje as Mensaje 
End
Go--#SQL