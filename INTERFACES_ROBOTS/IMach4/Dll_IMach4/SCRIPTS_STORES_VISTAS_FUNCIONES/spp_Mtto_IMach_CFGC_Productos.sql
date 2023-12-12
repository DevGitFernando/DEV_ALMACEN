------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Mtto_IMach_CFGC_Productos' and xType = 'P' )
    Drop Proc spp_Mtto_IMach_CFGC_Productos
Go--#SQL  
  
Create Proc spp_Mtto_IMach_CFGC_Productos 
( 
	@IdProducto varchar(8) = '', @CodigoEAN varchar(30) = '', @Descripcion varchar(100) = '', 
    @Status varchar(1) = '', @StatusIMach smallint = 0, @EsMultipicking smallint = 0  
) 
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
	Set @iActualizado = 0 

	If Not Exists ( Select * From IMach_CFGC_Productos (NoLock) 
		Where IdProducto = @IdProducto And CodigoEAN = @CodigoEAN ) 
	   Begin 
	      Insert Into IMach_CFGC_Productos ( IdProducto, CodigoEAN, StatusIMach, EsMultipicking, Status, Actualizado ) 
		  Select @IdProducto, @CodigoEAN, @StatusIMach, @EsMultipicking, @Status, @iActualizado 
	   End 
	Else 
	   Begin 
	      Update IMach_CFGC_Productos Set StatusIMach = @StatusIMach, EsMultipicking = @EsMultipicking, Status = @Status, Actualizado = @iActualizado
	      Where IdProducto = @IdProducto And CodigoEAN = @CodigoEAN
	   End 
	   
	   Set @sMensaje = 'La información de ' + @Descripcion + ' se guardo satisfactoriamente.'

	-- Regresar la Clave Generada
    Select @CodigoEAN as CodigoEAN, @sMensaje as Mensaje 
End
Go--#SQL  


