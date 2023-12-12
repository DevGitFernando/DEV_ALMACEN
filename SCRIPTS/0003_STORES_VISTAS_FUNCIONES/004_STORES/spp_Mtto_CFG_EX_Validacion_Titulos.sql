If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_EX_Validacion_Titulos' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_EX_Validacion_Titulos 
Go--#SQL   

Create Proc spp_Mtto_CFG_EX_Validacion_Titulos 
( 
    @IdEstado varchar(2) = '21', 
    @IdCliente varchar(4) = '0002', @Cliente varchar(500) = '', 
    @IdSubCliente varchar(4) = '0005', @SubCliente varchar(500) = '',     
	@IdPrograma varchar(4) = '0002', @Programa varchar(500) = '', 	
	@IdSubPrograma varchar(4) = '1313', @SubPrograma varchar(500) = '', 	
	@iActivo tinyint = 1 
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
	Set @sStatus = (case when @iActivo = 1 then 'A' else 'C' end)
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso     
    
	If Not Exists ( Select * From CFG_EX_Validacion_Titulos  (NoLock) 
					   Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
						and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma ) 
		Begin 
			Insert Into CFG_EX_Validacion_Titulos  ( IdEstado, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
				Cliente, SubCliente, Programa, SubPrograma, Status, Actualizado ) 
			Select 
				@IdEstado, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, 
				@Cliente, @SubCliente, @Programa, @SubPrograma, @sStatus, @iActualizado 
		End 
	
	
	If @iActivo = 1 
		Begin 
			Update C Set 
				Cliente = @Cliente, SubCliente = @SubCliente, Programa = @Programa, SubPrograma = @SubPrograma,  
				Status = @sStatus, Actualizado = @iActualizado 
			From CFG_EX_Validacion_Titulos  C 	
			Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
				  and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma 	          
		End 				 
	Else 
		Begin 
			Update C Set Status = @sStatus, Actualizado = @iActualizado 
			From CFG_EX_Validacion_Titulos  C 	
			Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
				  and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma 	          
		End 				 	
    
	-- Se devuelve el resultado.
	Select @sMensaje as Mensaje 
    
End 
Go--#SQL   


