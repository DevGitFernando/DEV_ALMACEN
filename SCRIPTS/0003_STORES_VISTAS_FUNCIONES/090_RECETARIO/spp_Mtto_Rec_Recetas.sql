

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Rec_Recetas' and xType = 'P')
    Drop Proc spp_Mtto_Rec_Recetas
Go--#SQL
  
Create Proc spp_Mtto_Rec_Recetas ( @IdEstado varchar(2), @IdFarmacia varchar(6), @IdReceta varchar(8),
	@IdMedico varchar(6), @IdBeneficiario varchar(8), @iOpcion smallint )
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

	
    --- Validación Estandar para Recetas 
	If @IdReceta = '*' 
	 Begin
		Select @IdReceta = cast( (max(IdReceta) + 1) as varchar)  
		From Rec_Recetas (NoLock) 
		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	 End 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdReceta = IsNull(@IdReceta, '1')
	Set @IdReceta = right(replicate('0', 8) + @IdReceta, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists (  Select * From Rec_Recetas (NoLock) 
							Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								  And IdReceta = @IdReceta ) 
			  Begin 
				 Insert Into Rec_Recetas ( IdEstado, IdFarmacia, IdReceta, IdMedico, IdBeneficiario, 
												 FechaRegistro, Status, Actualizado ) 

				 Select @IdEstado, @IdFarmacia, @IdReceta, @IdMedico, @IdBeneficiario, 
						 GetDate(), @sStatus, @iActualizado 
              End 
		    
				Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdReceta 
	   End 
    

	-- Regresar la Clave Generada
    Select @IdReceta as Clave, @sMensaje as Mensaje 
End
Go--#SQL
