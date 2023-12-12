


If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_CatServicios' and xType = 'P')
    Drop Proc spp_Mtto_FACT_CatServicios
Go--#SQL

  
Create Proc spp_Mtto_FACT_CatServicios 
(	
	@IdServicio varchar(3), @Descripcion varchar(100), @iOpcion smallint 
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint
		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0


	If @IdServicio = '*' 
	  Begin 
		Select @IdServicio = cast( (max(IdServicio) + 1) as varchar)  
		From FACT_CatServicios (NoLock)		
	  End 

	-- Asegurar que FolioContrarecibo sea valido y formatear la cadena 
	Set @IdServicio = IsNull(@IdServicio, '1')
	Set @IdServicio = right(replicate('0', 3) + @IdServicio, 3)


	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From FACT_CatServicios (NoLock) 
						   Where IdServicio = @IdServicio ) 
			  Begin 
				 Insert Into FACT_CatServicios ( IdServicio, Descripcion, Status, Actualizado ) 
				 Select @IdServicio, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update FACT_CatServicios Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				Where IdServicio = @IdServicio
              End
 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @IdServicio 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update FACT_CatServicios Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdServicio = @IdServicio
			 
		    Set @sMensaje = 'La información del Folio ' + @IdServicio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdServicio as Folio, @sMensaje as Mensaje 
End
Go--#SQL 	
