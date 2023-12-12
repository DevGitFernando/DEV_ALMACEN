



If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Rec_Recetas_Diagnosticos' and xType = 'P')
    Drop Proc spp_Mtto_Rec_Recetas_Diagnosticos
Go--#SQL
  
Create Proc spp_Mtto_Rec_Recetas_Diagnosticos ( @IdEstado varchar(2), @IdFarmacia varchar(6), @IdReceta varchar(8),
	@IdDiagnostico varchar(6) )
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

	
       Begin 

		   If Not Exists (  Select * From Rec_Recetas_Diagnosticos (NoLock) 
							Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								  And IdReceta = @IdReceta And IdDiagnostico = @IdDiagnostico ) 
			  Begin 
				 Insert Into Rec_Recetas_Diagnosticos ( IdEstado, IdFarmacia, IdReceta, IdDiagnostico, 
											Status, Actualizado ) 

				 Select @IdEstado, @IdFarmacia, @IdReceta, @IdDiagnostico, 
						 @sStatus, @iActualizado 
              End 
		    
				Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdReceta 
	   End 
    

	-- Regresar la Clave Generada
    Select @IdReceta as Clave, @sMensaje as Mensaje 
End
Go--#SQL
