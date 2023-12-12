
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFG_Claves_Excluir_NivelAbasto' and xType = 'P')
    Drop Proc spp_Mtto_CFG_Claves_Excluir_NivelAbasto
Go--#SQL
  
Create Proc spp_Mtto_CFG_Claves_Excluir_NivelAbasto 
(	@IdEstado varchar(2), @IdCliente varchar(4), @IdClaveSSA varchar(4), 
	@iOpcion smallint 
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

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto (NoLock) 
						   Where IdEstado = @IdEstado And IdCliente = @IdCliente And IdClaveSSA = @IdClaveSSA ) 
			  Begin 
				 Insert Into CFG_Claves_Excluir_NivelAbasto 
					 ( IdEstado, IdCliente, IdClaveSSA, Status, Actualizado ) 
				 Select 
					   @IdEstado, @IdCliente, @IdClaveSSA, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update CFG_Claves_Excluir_NivelAbasto Set Status = @sStatus, Actualizado = @iActualizado
				Where IdEstado = @IdEstado And IdCliente = @IdCliente And IdClaveSSA = @IdClaveSSA

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdClaveSSA 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto (NoLock) 
						   Where IdEstado = @IdEstado And IdCliente = @IdCliente And IdClaveSSA = @IdClaveSSA ) 
			  Begin 
				 Insert Into CFG_Claves_Excluir_NivelAbasto 
					 ( IdEstado, IdCliente, IdClaveSSA, Status, Actualizado ) 
				 Select 
					   @IdEstado, @IdCliente, @IdClaveSSA, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update CFG_Claves_Excluir_NivelAbasto Set Status = @sStatus, Actualizado = @iActualizado
				Where IdEstado = @IdEstado And IdCliente = @IdCliente And IdClaveSSA = @IdClaveSSA

              End 
		   Set @sMensaje = 'La información se Cancelo satisfactoriamente con la clave ' + @IdClaveSSA 
	   End 

	-- Regresar la Clave Generada
    Select @IdClaveSSA as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
