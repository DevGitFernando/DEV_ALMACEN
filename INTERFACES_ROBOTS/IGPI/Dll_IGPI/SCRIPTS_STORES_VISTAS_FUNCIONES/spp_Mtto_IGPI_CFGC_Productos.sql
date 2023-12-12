If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_IGPI_CFGC_Productos' and xType = 'P')
    Drop Proc spp_Mtto_IGPI_CFGC_Productos
Go--#SQL  
  
Create Proc spp_Mtto_IGPI_CFGC_Productos 
( 
	@IdProducto varchar(8) = '', @CodigoEAN varchar(30) = '', @Descripcion varchar(100) = '', 
    @Status varchar(1) = '', @StatusIGPI smallint = 0, @EsMultipicking smallint = 0  
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

	If Not Exists ( Select * From IGPI_CFGC_Productos (NoLock) 
		Where IdProducto = @IdProducto And CodigoEAN = @CodigoEAN ) 
	   Begin 
	      Insert Into IGPI_CFGC_Productos ( IdProducto, CodigoEAN, StatusIGPI, EsMultipicking, Status, Actualizado ) 
		  Select @IdProducto, @CodigoEAN, @StatusIGPI, @EsMultipicking, @Status, @iActualizado 
	   End 
	Else 
	   Begin 
	      Update IGPI_CFGC_Productos Set StatusIGPI = @StatusIGPI, EsMultipicking = @EsMultipicking, Status = @Status, Actualizado = @iActualizado
	      Where IdProducto = @IdProducto And CodigoEAN = @CodigoEAN
	   End 
	   
	   Set @sMensaje = 'La información de ' + @Descripcion + ' se guardo satisfactoriamente.'

	-- Regresar la Clave Generada
    Select @CodigoEAN as CodigoEAN, @sMensaje as Mensaje 
End
Go--#SQL  

