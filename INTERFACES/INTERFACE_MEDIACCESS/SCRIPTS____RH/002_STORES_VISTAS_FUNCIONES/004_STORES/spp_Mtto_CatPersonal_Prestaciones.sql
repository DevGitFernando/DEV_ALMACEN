

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatPersonal_Prestaciones' and xType = 'P')
    Drop Proc spp_Mtto_CatPersonal_Prestaciones
Go--#SQL
  
 ------		 Exec spp_Mtto_CatPersonal_Prestaciones '00001270', '01', 1
  
Create Proc spp_Mtto_CatPersonal_Prestaciones 
( 
		@IdPersonal varchar(8), @IdPrestacion varchar(2), @iOpcion smallint 
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


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatPersonal_Prestaciones (NoLock) 
							Where IdPersonal = @IdPersonal and IdPrestacion = @IdPrestacion 
							 ) 
			  Begin 
				 Insert Into CatPersonal_Prestaciones ( IdPersonal, IdPrestacion, FechaRegistro, FechaModificacion, Status, Actualizado ) 
				 Select @IdPersonal, @IdPrestacion, GETDATE(), GETDATE(), @sStatus, @iActualizado 
              End 
              
           Else
			   Begin
					 Update CatPersonal_Prestaciones Set FechaModificacion = GETDATE(), Status = @sStatus, Actualizado = @iActualizado
					 Where IdPersonal = @IdPersonal and IdPrestacion = @IdPrestacion        
			   End
		   
		   Set @sMensaje = 'La información de la Prestación se guardo satisfactoriamente'
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatPersonal_Prestaciones Set Status = @sStatus, Actualizado = @iActualizado 
	       Where IdPersonal = @IdPersonal and IdPrestacion = @IdPrestacion 			
	       
		   Set @sMensaje = 'La información de la Prestación ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @sMensaje as Mensaje 
End
Go--#SQL
