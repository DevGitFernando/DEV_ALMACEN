


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProveedores_Certificacion_Doctos' And xType = 'P' )
	Drop Proc spp_Mtto_CatProveedores_Certificacion_Doctos
Go--#SQL

		----		Exec spp_Mtto_CatProveedores_Certificacion_Doctos '0000', '00', '', ''

Create Procedure spp_Mtto_CatProveedores_Certificacion_Doctos 
( 
	@IdProveedor varchar(4) = '0000', @IdDocumento varchar(2) = '00', @NombreDocumento varchar(200) = '', @Documento varchar(max) = ''
)
With Encryption 
As
Begin
	Declare @Status varchar(1), 
			@Actualizado int,
			@sMensaje varchar(8000)

	Set @Status = 'A'
	Set @Actualizado = 0
	Set @sMensaje = ''		
	
	
	If Not Exists( Select * From CatProveedores_Certificacion_Doctos (NoLock) Where IdProveedor = @IdProveedor and IdDocumento = @IdDocumento )
		Begin
			Insert Into CatProveedores_Certificacion_Doctos ( IdProveedor, IdDocumento, NombreDocumento, Documento, FechaRegistro, Status, Actualizado )
			Select @IdProveedor, @IdDocumento, @NombreDocumento, @Documento, GetDate(), @Status, @Actualizado
		End
	Else
		Begin
			Update CatProveedores_Certificacion_Doctos Set NombreDocumento = @NombreDocumento, Documento = @Documento,
			Status = @Status, Actualizado = @Actualizado
			Where IdProveedor = @IdProveedor and IdDocumento = @IdDocumento
		End
	 
	

	Set @sMensaje = 'La información del Proveedor ' + @IdProveedor + ' se guardo exitosamente'
	 
    Select @IdProveedor as IdProveedor, @sMensaje as Mensaje 

	-----		spp_Mtto_CatProveedores_Certificacion_Doctos

End
Go--#SQL
