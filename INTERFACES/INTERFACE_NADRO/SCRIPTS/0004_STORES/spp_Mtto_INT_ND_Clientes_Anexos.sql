------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_INT_ND_CFG_CB_Anexos_Miembros' and xType = 'P')
    Drop Proc spp_Mtto_INT_ND_CFG_CB_Anexos_Miembros
Go--#SQL 
  
Create Proc spp_Mtto_INT_ND_CFG_CB_Anexos_Miembros 
( 
	@IdEstado varchar(2) = '16', @CodigoCliente varchar(20) = '12345678', @IdAnexo varchar(50) = 'TEST', @iOpcion smallint = 1  
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


	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From INT_ND_CFG_CB_Anexos_Miembros (NoLock) 
				Where IdEstado = @IdEstado And CodigoCliente = @CodigoCliente and IdAnexo = @IdAnexo ) 
			  Begin 
				 Insert Into INT_ND_CFG_CB_Anexos_Miembros ( IdEstado, CodigoCliente, IdAnexo, Status  )
				 Select @IdEstado, @CodigoCliente, @IdAnexo, @sStatus 
              End 
           Else 
			   Begin  
				   Update INT_ND_CFG_CB_Anexos_Miembros Set Status = @sStatus 
				   Where IdEstado = @IdEstado And CodigoCliente = @CodigoCliente and IdAnexo = @IdAnexo        
			   End    
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update INT_ND_CFG_CB_Anexos_Miembros Set Status = @sStatus 
	       Where IdEstado = @IdEstado And CodigoCliente = @CodigoCliente  and IdAnexo = @IdAnexo 
	        
	       -- Set @sMensaje = 'La información del Anexo ' + @IdCliente + ' ha sido cancelada satisfactoriamente.' 
	   End 

--- Regresar la Clave Generada 
--- Select @IdCliente as Clave, @sMensaje as Mensaje 



End 
Go--#SQL