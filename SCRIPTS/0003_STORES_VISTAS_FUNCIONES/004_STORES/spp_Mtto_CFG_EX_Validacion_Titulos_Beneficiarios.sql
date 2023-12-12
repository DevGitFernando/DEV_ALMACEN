If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_EX_Validacion_Titulos_Beneficiarios' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_EX_Validacion_Titulos_Beneficiarios 
Go--#SQL   

Create Proc spp_Mtto_CFG_EX_Validacion_Titulos_Beneficiarios 
( 
    @IdEstado varchar(2) = '21', @IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005',
	@IdPrograma varchar(4) = '0002', @IdSubPrograma varchar(4) = '1313', 
	@FolioReferencia varchar(20) = 'N/A', @ReemplazarFolioReferencia bit = 0, 	
	@Beneficiario varchar(200) = 'BENEFICIARIO', @ReemplazarBeneficiario bit = 0, 
	@iOpcion tinyint = 1
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
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso     
    
	If @iOpcion = 1
		Begin 
			If Not Exists ( Select * From CFG_EX_Validacion_Titulos_Beneficiarios (NoLock) 
							   Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
								and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma ) 
				Begin 
					Insert Into CFG_EX_Validacion_Titulos_Beneficiarios 
					( 
						IdEstado, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
						FolioReferencia, ReemplazarFolioReferencia, Beneficiario, ReemplazarBeneficiario, Status, Actualizado 
					) 
					Select 
						@IdEstado, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, 
						@FolioReferencia, @ReemplazarFolioReferencia, @Beneficiario, @ReemplazarBeneficiario, @sStatus, @iActualizado 
				End 
			Else 
				Begin 	          
					Update C  Set 
						FolioReferencia = @FolioReferencia, ReemplazarFolioReferencia = @ReemplazarFolioReferencia,  	             
						Beneficiario = @Beneficiario, ReemplazarBeneficiario = @ReemplazarBeneficiario,  
						Status = @sStatus, Actualizado = @iActualizado 
					From CFG_EX_Validacion_Titulos_Beneficiarios C 	
					Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
						  and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma 	          
				End 
				Set @sMensaje = 'La información se guardo satisfactoriamente con el Titulo: ' + @Beneficiario
		End 	  
	Else
		Begin
			Set @sStatus = 'C'
			Update CFG_EX_Validacion_Titulos_Beneficiarios Set Status = @sStatus
			Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
				  and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma 

			Set @sMensaje = 'Información cancelada satisfactoriamente' 
		End 
    
	-- Se devuelve el resultado.
	Select @sMensaje as Mensaje 
    
End 
Go--#SQL   


