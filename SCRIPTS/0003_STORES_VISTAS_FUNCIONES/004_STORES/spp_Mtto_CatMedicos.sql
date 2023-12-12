-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatMedicos' and xType = 'P' ) 
    Drop Proc spp_Mtto_CatMedicos 
Go--#SQL  
  
Create Proc spp_Mtto_CatMedicos 
( 
	@IdEstado varchar(2) = '', @IdFarmacia varchar(6) = '', @IdMedico varchar(6) = '', @Nombre varchar(150) = '', 
	@ApPaterno varchar(150) = '', @ApMaterno varchar(150) = '', @NumCedula varchar(32) = '', @IdEspecialidad varchar(4) = '', 
	@IdPersonal varchar(4) = '', @iOpcion smallint = 0, @MostrarResultado smallint = 1, @Resultado varchar(6) = '' output     
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


	If @IdMedico = '*'
	 Begin 
		Select @IdMedico = cast( (max(IdMedico) + 1) as varchar)  
		From CatMedicos (NoLock) 
		Where IdEstado = @IdEstado And @IdFarmacia = @IdFarmacia
	 End

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdMedico = IsNull(@IdMedico, '1')
	Set @IdMedico = right(replicate('0', 6) + @IdMedico, 6)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatMedicos (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdMedico = @IdMedico ) 
			  Begin 
				 Insert Into CatMedicos ( IdEstado, IdFarmacia, IdMedico, Nombre, ApPaterno, ApMaterno, NumCedula, IdEspecialidad, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdMedico, @Nombre, @ApPaterno, @ApMaterno, @NumCedula, @IdEspecialidad, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatMedicos Set Nombre = @Nombre, ApPaterno = @ApPaterno, ApMaterno = @ApMaterno, NumCedula = @NumCedula, 
									   IdEspecialidad = @IdEspecialidad, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdMedico = @IdMedico  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdMedico 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatMedicos Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdMedico = @IdMedico 
		   Set @sMensaje = 'La información del Medico ' + @IdMedico + ' ha sido cancelada satisfactoriamente.' 
	   End 


--------------------------- Registrar el cambio del Log  
	Insert Into CatMedicos_Historico 
		( IdEstado, IdFarmacia, IdMedico, Nombre, ApPaterno, ApMaterno, NumCedula, IdEspecialidad, Status, Actualizado, IdPersonal ) 
	Select IdEstado, IdFarmacia, IdMedico, Nombre, ApPaterno, ApMaterno, NumCedula, IdEspecialidad, Status, Actualizado, @IdPersonal as IdPersonal   
	From CatMedicos (nolock) 
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdMedico = @IdMedico  


--	sp_listacolumnas CatMedicos 

	Set @Resultado = @IdMedico 
	If @MostrarResultado = 1 
	Begin 
		-- Regresar la Clave Generada 
		Select @IdMedico as Clave, @sMensaje as Mensaje 
    End 
    
End 
Go--#SQL 

