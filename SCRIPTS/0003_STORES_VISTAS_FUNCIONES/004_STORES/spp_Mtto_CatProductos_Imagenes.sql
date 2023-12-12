---------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProductos_Imagenes' and xType = 'P')
    Drop Proc spp_Mtto_CatProductos_Imagenes
Go--#SQL
  
Create Proc spp_Mtto_CatProductos_Imagenes 
( 
	@IdProducto varchar(8) = '00000738', @CodigoEAN varchar(30) = '7501008496701', @Consecutivo varchar(2) = '1', 
	@NombreImagen varchar(200) = 'Nombre', @Imagen varchar(max) = 'Imagen', @Ancho numeric(14, 4) = 0, @Alto numeric(14, 4) = 0,  
	@IdPersonal varchar(4) = '0001', 
	@Status varchar(1) = ''  
)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), @sStatus varchar(1), @iActualizado smallint, @iConsecutivo int

	
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @iConsecutivo = 0
	
	If @Status <> '' 
	   Set @sStatus = @Status 
	
	
	If @Consecutivo = '*' 
		Begin
			Set @iConsecutivo = (Select IsNull((Max(Consecutivo) + 1), 1) From CatProductos_Imagenes (Nolock) 
								Where IdProducto = @IdProducto and CodigoEAN = @CodigoEAN )	
		End
	Else
	Begin 
		Set @iConsecutivo = CAST(@Consecutivo as Int)
	End
									
	If Not Exists ( Select * From CatProductos_Imagenes (NoLock) 
		Where IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and Consecutivo = @iConsecutivo )
		Begin
			Insert Into CatProductos_Imagenes 
			( 
				IdProducto, CodigoEAN, Consecutivo, NombreImagen, Imagen, Ancho, Alto, FechaRegistro, IdPersonal, Status, Actualizado 
			) 
			Select @IdProducto, @CodigoEAN, @iConsecutivo, @NombreImagen, @Imagen, @Ancho, @Alto, GetDate(), @IdPersonal, @sStatus, @iActualizado 
		End
	Else
		Begin
			Update CatProductos_Imagenes 
				Set NombreImagen = @NombreImagen, Imagen = @Imagen, Ancho = @Ancho, Alto = @Alto, 
				FechaRegistro = GetDate(), 
				IdPersonal = @IdPersonal, Status = @sStatus  
			Where IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and Consecutivo = @iConsecutivo
		End		
			
		
	Set @sMensaje = 'La información se guardo satisfactoriamente ' 
	     

	-- Regresar la Clave Generada
    Select @sMensaje as Mensaje 
    
End
Go--#SQL
