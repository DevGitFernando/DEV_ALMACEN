
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatDistribuidores' and xType = 'P')
    Drop Proc spp_Mtto_CatDistribuidores
Go--#SQL
  
Create Proc spp_Mtto_CatDistribuidores ( @IdDistribuidor varchar(4), @NombreDistribuidor varchar(102), @iOpcion smallint )
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


	If @IdDistribuidor = '*' 
	   Select @IdDistribuidor = cast( (max(IdDistribuidor) + 1) as varchar)  From CatDistribuidores (NoLock) 

	-- Asegurar que IdDistribuidor sea valido y formatear la cadena 
	Set @IdDistribuidor = IsNull(@IdDistribuidor, '1')
	Set @IdDistribuidor = right(replicate('0', 4) + @IdDistribuidor, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatDistribuidores (NoLock) Where IdDistribuidor = @IdDistribuidor ) 
			  Begin 
				 Insert Into CatDistribuidores ( IdDistribuidor, NombreDistribuidor, Status, Actualizado ) 
				 Select @IdDistribuidor, @NombreDistribuidor, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatDistribuidores Set NombreDistribuidor = @NombreDistribuidor, Status = @sStatus, Actualizado = @iActualizado
				 Where IdDistribuidor = @IdDistribuidor  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdDistribuidor 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatDistribuidores Set Status = @sStatus, Actualizado = @iActualizado Where IdDistribuidor = @IdDistribuidor 
		   Set @sMensaje = 'La información del Distribuidor ' + @IdDistribuidor + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdDistribuidor as Clave, @sMensaje as Mensaje 
End
Go--#SQL
