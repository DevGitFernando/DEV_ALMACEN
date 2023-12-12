------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_ClaveSSA_Mascara' and xType = 'P' )
    Drop Proc spp_Mtto_ClaveSSA_Mascara
Go--#SQL
  
Create Proc spp_Mtto_ClaveSSA_Mascara
( 
	@IdEstado varchar(2), @IdCliente varchar(4), @IdSubCliente varchar(4), @IdClaveSSA varchar(4),
	@Mascara varchar(50), @iOpcion smallint, @Descripcion varchar(5000) = '', @DescripcionCorta varchar(100) = '',  
	@Presentacion varchar(100) = '' 
) 
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

	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From CFG_ClaveSSA_Mascara (NoLock) 
						   Where IdEstado = @IdEstado And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdClaveSSA  = @IdClaveSSA) 
			  Begin 
				 Insert Into CFG_ClaveSSA_Mascara ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA, Mascara, Status, Actualizado, Descripcion, DescripcionCorta, Presentacion ) 
				 Select @IdEstado, @IdCliente, @IdSubCliente, @IdClaveSSA, @Mascara, @sStatus, @iActualizado, @Descripcion, @DescripcionCorta, @Presentacion 
              End 
		   Else 
			  Begin				
				Update CFG_ClaveSSA_Mascara
				Set Mascara = @Mascara, Descripcion = @Descripcion, DescripcionCorta = @DescripcionCorta, Presentacion = @Presentacion, 
					Status = @sStatus, Actualizado = @iActualizado
				Where IdEstado = @IdEstado And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdClaveSSA  = @IdClaveSSA 
              End 

		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdClaveSSA 

	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFG_ClaveSSA_Mascara Set Status = @sStatus, Actualizado = @iActualizado 
	   	   Where IdEstado = @IdEstado And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdClaveSSA  = @IdClaveSSA
       
	   	   Set @sMensaje = 'La información de la Clave ' + @IdClaveSSA + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdClaveSSA as Clave, @sMensaje as Mensaje 

End
Go--#SQL	
