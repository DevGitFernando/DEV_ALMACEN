


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProveedores_Certificacion' And xType = 'P' )
	Drop Proc spp_Mtto_CatProveedores_Certificacion
Go--#SQL

		----		Exec spp_Mtto_CatProveedores_Certificacion '0005', '', '', 1

Create Procedure spp_Mtto_CatProveedores_Certificacion 
( 
	@IdProveedor varchar(4) = '0000', @IdPersonal varchar(8) = '', @Personal varchar(200) = '', @TipoRpte tinyint = 0
)
With Encryption 
As
Begin
	Declare @Status varchar(1), 
			@Actualizado int,
			@sMensaje varchar(8000), 
			@IdPersonalLegal varchar(8), @IdPersonalSanitario varchar(8),
			@iNumDoctos int, @iNumDoctos_Prov int, @iCertifica tinyint

	/*Opciones
	@TipoRpte = 1.- Representante Legal
	@TipoRpte = 2.- Representante Sanitario
	*/

	Set @Status = 'A'
	Set @Actualizado = 0
	Set @sMensaje = ''		

	Set @IdPersonalLegal = ''
	Set @IdPersonalSanitario = ''
	
	Set @iNumDoctos = 0
	Set @iNumDoctos_Prov = 0
	Set @iCertifica = 0
	
	Set @iNumDoctos = IsNull( (Select Count(*) From CatProveedores_TipoDoctos ), 0 )
	Set @iNumDoctos_Prov = IsNull( (Select Count(*) From CatProveedores_Certificacion_Doctos Where IdProveedor = @IdProveedor ), 0 )
	
	If @iNumDoctos = @iNumDoctos_Prov
	Begin
		Set @iCertifica = 1
	End
	
	If @TipoRpte = 0
	Begin
		 If Not Exists( Select * From CatProveedores_Certificacion (NoLock) Where IdProveedor = @IdProveedor  )
			 Begin
				Insert Into CatProveedores_Certificacion ( IdProveedor, FechaRegistro, Status, Actualizado )
				Select @IdProveedor, GetDate(), @Status, @Actualizado
			 End
	End
	
	If @iCertifica = 1
	Begin
		If @TipoRpte = 1
		Begin
			Update CatProveedores_Certificacion
			Set IdPersonalLegal = @IdPersonal, PersonalLegal = @Personal, FechaLegal = GetDate(), Status = @Status, Actualizado = @Actualizado
			Where IdProveedor = @IdProveedor
		End 
		
		If @TipoRpte = 2
		Begin
			Update CatProveedores_Certificacion
			Set IdPersonalSanitario = @IdPersonal, PersonalSanitario = @Personal, FechaSanitario = GetDate(), Status = @Status, Actualizado = @Actualizado
			Where IdProveedor = @IdProveedor
		End		 
		 
		Set @IdPersonalLegal = ( Select IdPersonalLegal From CatProveedores_Certificacion Where IdProveedor = @IdProveedor )
		Set @IdPersonalSanitario = ( Select IdPersonalSanitario From CatProveedores_Certificacion Where IdProveedor = @IdProveedor )
		
		If @IdPersonalLegal <> '' and @IdPersonalSanitario <> ''
		Begin 
			Update CatProveedores_Certificacion Set EsCertificado = 1 Where IdProveedor = @IdProveedor
		End

		Set @sMensaje = 'La información del Proveedor ' + @IdProveedor + ' se guardo exitosamente'
		 
		---Select @IdProveedor as IdProveedor, @sMensaje as Mensaje
	
	End 


	Select @IdProveedor as IdProveedor, @sMensaje as Mensaje, @iCertifica as Certifica
	
	-----		spp_Mtto_CatProveedores_Certificacion

End
Go--#SQL
