
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProductos_Estado' and xType = 'P')
    Drop Proc spp_Mtto_CatProductos_Estado
Go--#SQL
  
Create Proc spp_Mtto_CatProductos_Estado ( @IdEstado varchar(4), @IdProducto varchar(10), @iStatus smallint )
With Encryption 
As
Begin
Set NoCount On

Declare 
	@iActualizado smallint, 
	@sStatus varchar(1)
	
	Set @sStatus = ''
	Set @iActualizado = 0

	If @iStatus = 1 
		Set @sStatus = 'A'
	Else
		Set @sStatus = 'C'

   If Not Exists ( Select * From CatProductos_Estado (NoLock) Where IdEstado = @IdEstado And IdProducto = @IdProducto ) 
	  Begin 
		 Insert Into CatProductos_Estado ( IdEstado, IdProducto, Status, Actualizado ) 
		 Select @IdEstado, @IdProducto, @sStatus, @iActualizado 
      End 
   Else 
	  Begin 
	     Update CatProductos_Estado Set Status = @sStatus, Actualizado = @iActualizado
		 Where IdEstado = @IdEstado And IdProducto = @IdProducto
      End 
	   
	-- Regresar la Clave Generada
    Select @IdEstado as Clave --, @sMensaje as Mensaje 
End
Go--#SQL
