If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Rec_Recetas_Indicaciones' and xType = 'P')
    Drop Proc spp_Mtto_Rec_Recetas_Indicaciones
Go--#SQL
  
Create Proc spp_Mtto_Rec_Recetas_Indicaciones ( @IdEstado varchar(2) = '25', @IdFarmacia varchar(6) = '0001', 
	@IdReceta varchar(8) = '00000001', @IdIndicacion varchar(4) = '0001', @Indicacion varchar(200) = '')
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

--	select * from Rec_Recetas_Indicaciones 
	
       Begin 

		   If Not Exists (  Select * From Rec_Recetas_Indicaciones (NoLock) 
							Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								  And IdReceta = @IdReceta And IdIndicacion = @IdIndicacion ) 
			  Begin 
				 Insert Into Rec_Recetas_Indicaciones ( IdEstado, IdFarmacia, IdReceta, IdIndicacion, Indicacion, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdReceta, @IdIndicacion, @Indicacion, @sStatus, @iActualizado 
              End 
		    
			  --	Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdReceta 
	   End 
    

	-- Regresar la Clave Generada
    Select @IdReceta as Clave, @sMensaje as Mensaje 
End
Go--#SQL
