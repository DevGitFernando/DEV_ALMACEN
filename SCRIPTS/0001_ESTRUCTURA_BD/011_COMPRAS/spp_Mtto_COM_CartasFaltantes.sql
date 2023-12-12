

	If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_CartasFaltantes' and xType = 'P')
    Drop Proc spp_Mtto_COM_CartasFaltantes
Go--#SQL
  
Create Proc spp_Mtto_COM_CartasFaltantes ( @Folio varchar(8), @IdProveedor varchar(4), @Observaciones varchar(200), @Documento text, 
			@NombreDocto varchar(200), @iOpcion smallint )
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


	If @Folio = '*' 
	   Select @Folio = cast( (max(Folio) + 1) as varchar)  From COM_CartasFaltantes (NoLock) 

	-- Asegurar que el Folio sea valido y formatear la cadena 
	Set @Folio = IsNull(@Folio, '1')
	Set @Folio = right(replicate('0', 8) + @Folio, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From COM_CartasFaltantes (NoLock) Where Folio = @Folio ) 
			  Begin 
				 Insert Into COM_CartasFaltantes ( Folio, IdProveedor, FechaRegistro, Observaciones, Documento, NombreDocto, Status, Actualizado ) 
				 Select @Folio, @IdProveedor, GetDate(), @Observaciones, @Documento, @NombreDocto, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update COM_CartasFaltantes Set Observaciones = @Observaciones, Status = @sStatus, Actualizado = @iActualizado
				 Where Folio = @Folio  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @Folio 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update COM_CartasFaltantes Set Status = @sStatus, Actualizado = @iActualizado Where Folio = @Folio 
		   Set @sMensaje = 'La información de la Carta de Faltante ' + @Folio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @Folio as Folio, @sMensaje as Mensaje 
End
Go--#SQL
