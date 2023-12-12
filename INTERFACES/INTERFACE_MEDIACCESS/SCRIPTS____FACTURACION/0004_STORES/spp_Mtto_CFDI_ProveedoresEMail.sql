If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFDI_ProveedoresEMail' and xType = 'P')
    Drop Proc spp_Mtto_CFDI_ProveedoresEMail
Go--#SQL
  
Create Proc spp_Mtto_CFDI_ProveedoresEMail 
( 
	@IdProveedorEMail varchar(4) = '', @Descripcion varchar(100) = '', @iOpcion smallint = 1 
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


	If @IdProveedorEMail = '*' 
	   Select @IdProveedorEMail = cast( (max(IdProveedorEMail) + 1) as varchar)  From CFDI_ProveedoresEMail (NoLock) 

	-- Asegurar que IdProveedorEMail sea valido y formatear la cadena 
	Set @IdProveedorEMail = IsNull(@IdProveedorEMail, '1')
	Set @IdProveedorEMail = right(replicate('0', 4) + @IdProveedorEMail, 4) 


	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CFDI_ProveedoresEMail (NoLock) Where IdProveedorEMail = @IdProveedorEMail ) 
			  Begin 
				 Insert Into CFDI_ProveedoresEMail ( IdProveedorEMail, Descripcion, Status, Actualizado ) 
				 Select @IdProveedorEMail, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFDI_ProveedoresEMail Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdProveedorEMail = @IdProveedorEMail  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdProveedorEMail 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFDI_ProveedoresEMail Set Status = @sStatus, Actualizado = @iActualizado 
	       Where IdProveedorEMail = @IdProveedorEMail 
		   Set @sMensaje = 'La información de la Marca ' + @IdProveedorEMail + ' ha sido cancelado satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdProveedorEMail as Clave, @sMensaje as Mensaje 
End
Go--#SQL 

