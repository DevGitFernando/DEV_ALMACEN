----------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Mtto_CatBeneficiarios_Identificacion_Identificacion' and xType = 'P' )
    Drop Proc spp_Mtto_CatBeneficiarios_Identificacion_Identificacion
Go--#SQL 
  
Create Proc spp_Mtto_CatBeneficiarios_Identificacion_Identificacion 
( 
	@IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '',
	@IdBeneficiario varchar(10) = '', @IMG_01_Frontal varchar(max) = '',  @IMG_02_Reverso varchar(max) = '' 

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


	If Not Exists 
		(  
			Select * From CatBeneficiarios_Identificacion (NoLock) 
			Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdBeneficiario = @IdBeneficiario 
		) 
		Begin 
			Insert Into CatBeneficiarios_Identificacion ( 
				IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdBeneficiario, IMG_01_Frontal, FechaModificacion_Frontal, IMG_02_Reverso, FechaModificacion_Reverso ) 
			Select 
				@IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdBeneficiario, @IMG_01_Frontal, getdate() as FechaModificacion_Frontal, @IMG_02_Reverso, getdate() as FechaModificacion_Reverso
		End 
	Else 
		Begin 
			Update CatBeneficiarios_Identificacion 
				Set 
					IMG_01_Frontal = @IMG_01_Frontal, FechaModificacion_Frontal = getdate(), IMG_02_Reverso = @IMG_02_Reverso, FechaModificacion_Reverso = getdate() 
			Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdBeneficiario = @IdBeneficiario 
		End 

    
End
Go--#SQL  


		