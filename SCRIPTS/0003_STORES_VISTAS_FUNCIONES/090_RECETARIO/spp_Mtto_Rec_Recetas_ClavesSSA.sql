

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Rec_Recetas_ClavesSSA' and xType = 'P')
    Drop Proc spp_Mtto_Rec_Recetas_ClavesSSA
Go--#SQL
  
Create Proc spp_Mtto_Rec_Recetas_ClavesSSA ( @IdEstado varchar(2), @IdFarmacia varchar(6), @IdReceta varchar(8),
	@IdClaveSSA varchar(4), @iCantidad int, @iRenglon smallint )
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

		   If Not Exists (  Select * From Rec_Recetas_ClavesSSA (NoLock) 
							Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								  And IdReceta = @IdReceta And IdClaveSSA = @IdClaveSSA ) 
			  Begin 
				 Insert Into Rec_Recetas_ClavesSSA ( IdEstado, IdFarmacia, IdReceta, IdClaveSSA, Cantidad, 
											Renglon, Status, Actualizado ) 

				 Select @IdEstado, @IdFarmacia, @IdReceta, @IdClaveSSA, @iCantidad, 
						 @iRenglon, @sStatus, @iActualizado 
              End 
		    
				Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdReceta 
	   End 
    

	-- Regresar la Clave Generada
    Select @IdReceta as Clave, @sMensaje as Mensaje 
End
Go--#SQL
