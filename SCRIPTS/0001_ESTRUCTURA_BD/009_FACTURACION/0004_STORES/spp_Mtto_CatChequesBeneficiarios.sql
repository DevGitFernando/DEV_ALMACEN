If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatChequesBeneficiarios' and xType = 'P')
    Drop Proc spp_Mtto_CatChequesBeneficiarios
Go--#SQL
  
Create Proc spp_Mtto_CatChequesBeneficiarios
	( @IdBeneficiario varchar(6), @Descripcion varchar(500), @RFC Varchar(13), @Telefono Varchar(13), @Estado Varchar(30),
	  @Municipio Varchar(50), @Direccion Varchar(500), @CP Varchar(10), @EsMoral Bit, @iOpcion smallint )
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


	If @IdBeneficiario = '*'
		Begin
			Select @IdBeneficiario = cast( (max(IdBeneficiario) + 1) as varchar)  From CNT_CatChequesBeneficiarios (NoLock)
		End

	Set @IdBeneficiario = IsNull(@IdBeneficiario, '1')
	Set @IdBeneficiario = dbo.fg_FormatearCadena(@IdBeneficiario, '0', 6)

	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From CNT_CatChequesBeneficiarios (NoLock) 
							Where IdBeneficiario =  @IdBeneficiario) 
			  Begin 
				 Insert Into CNT_CatChequesBeneficiarios
					   (IdBeneficiario, Descripcion, RFC, Telefono, Estado , Municipio, Direccion, CP, EsMoral, Status, Actualizado ) 
				 Select @IdBeneficiario, @Descripcion, @RFC, @Telefono, @Estado , @Municipio, @Direccion, @CP, @EsMoral, @sStatus, @iActualizado
              End
		   Else 
			  Begin 
			     Update CNT_CatChequesBeneficiarios
			     Set IdBeneficiario = @IdBeneficiario, Descripcion = @Descripcion, RFC = @RFC, Telefono = @Telefono,
					 Estado = @Estado , Municipio = @Municipio, Direccion = @Direccion, CP = @CP,
						EsMoral = @EsMoral, Status = @sStatus, Actualizado = @iActualizado
			     Where IdBeneficiario =  @IdBeneficiario 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdBeneficiario 
	   End
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CNT_CatChequesBeneficiarios Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdBeneficiario =  @IdBeneficiario 
		   Set @sMensaje = 'La información del Beneficiario ' + @IdBeneficiario + ' ha sido cancelada satisfactoriamente.' 
	   End 

	---- Regresar la Clave Generada
    Select @IdBeneficiario as Clave, @sMensaje as Mensaje 
End
Go--#SQL