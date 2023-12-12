
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_MotivosSobrePrecio' and xType = 'P' )
    Drop Proc spp_Mtto_MotivosSobrePrecio
Go--#SQL
  
Create Proc spp_Mtto_MotivosSobrePrecio ( @Folio varchar(50), @Descripcion varchar(100) = '',@iOpcion smallint)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@FechaCanje varchar(10)		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	
	 If (@Folio = '*')
	Begin
		Select @Folio = Max(Folio) + 1 From CatMotivosSobrePrecio (NoLock)
		Set @Folio = ISNULL(@Folio, 1)
		Set @Folio = RIGHT('00000000' + @Folio, 4)
	End
		

	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From CatMotivosSobrePrecio (NoLock) Where Folio = @Folio) 
			  Begin 
				 Insert Into CatMotivosSobrePrecio ( Folio, Descripcion, Status, Actualizado ) 
				 Select @Folio, @Descripcion, @sStatus, @iActualizado
              End 
		   Else 
			  Begin				
				Update CatMotivosSobrePrecio Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado Where Folio = @Folio
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el folio ' + @Folio 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatMotivosSobrePrecio Set Status = @sStatus, Actualizado = @iActualizado 
	   	   Where Folio = @Folio
       
	   	   Set @sMensaje = 'La información del Folio' + @Folio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @Folio as Folio, @sMensaje as Mensaje 
End
Go--#SQL	
