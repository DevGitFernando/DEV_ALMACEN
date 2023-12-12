---------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProductos_PreAlta_Imagenes' and xType = 'P')
    Drop Proc spp_Mtto_CatProductos_PreAlta_Imagenes
Go--#SQL
  
Create Proc spp_Mtto_CatProductos_PreAlta_Imagenes 
( 
	@IdProducto varchar(8) = '00000738', @Consecutivo varchar(2) = '1', 
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
			Set @iConsecutivo = (Select IsNull((Max(Consecutivo) + 1), 1) From CatProductos_PreAlta_Imagenes (Nolock) 
								Where IdProducto = @IdProducto and Consecutivo = @iConsecutivo )	
		End
	Else 
	Begin 
		Set @iConsecutivo = CAST(@Consecutivo as Int)
	End
									
	If Not Exists ( Select * From CatProductos_PreAlta_Imagenes (NoLock) 
		Where IdProducto = @IdProducto and Consecutivo = @iConsecutivo )
		Begin
			Insert Into CatProductos_PreAlta_Imagenes 
			( 
				IdProducto, Consecutivo, NombreImagen, Imagen, Ancho, Alto, FechaRegistro, Status, Actualizado 
			) 
			Select @IdProducto, @iConsecutivo, @NombreImagen, @Imagen, @Ancho, @Alto, GetDate(), @sStatus, @iActualizado 
		End
	Else
		Begin
			Update CatProductos_PreAlta_Imagenes 
				Set NombreImagen = @NombreImagen, Imagen = @Imagen, Ancho = @Ancho, Alto = @Alto, 
				FechaRegistro = GetDate(), Status = @sStatus  
			Where IdProducto = @IdProducto and Consecutivo = @iConsecutivo
		End		
			
		
	Set @sMensaje = 'La información se guardo satisfactoriamente ' 
	     

	-- Regresar la Clave Generada
    Select @sMensaje as Mensaje 
    
End
Go--#SQL
