If Exists (Select Name From SysObjects (NoLock) Where Name = 'spp_Mtto_Productos_RegistrosSanitarios' And xType = 'P')
	Drop Proc spp_Mtto_Productos_RegistrosSanitarios
Go--#SQL 

Create Proc spp_Mtto_Productos_RegistrosSanitarios 
( 
	@Consecutivo varchar(4), @Tipo varchar(3), @Año varchar(4), @IdProducto varchar(8), 
	@CodigoEAN varchar(30), @Vigencia DateTime, @Status varchar(4), @Documento Text, @NombreDocto varchar(200), @iOpcion smallint 
) 
With Encryption 
As
Begin
Set NoCount On
Set DateFormat YMD
Declare 
	@Mensaje varchar(1000),
	@Actualizado smallint,   
	@FechaVigencia datetime,  
	@sVigencia varchar(100) 

	/*Opciones
	  Opcion 1.- Insercion / Actualizacion*/ 


	Set @Mensaje = ''	
	Set @Actualizado = 0 
	Set @sVigencia = cast(year(@Vigencia) as varchar) + '-' + right('00' + cast(month(@Vigencia) as varchar), 2) + '-01' 
	Set @FechaVigencia = cast(@sVigencia as datetime) 
	

	If @IdProducto = '*' 
	   Select @IdProducto = cast( (max(IdProducto) + 1) as varchar)  From CatProductos_RegistrosSanitarios (NoLock) 
	
	-- Asegurar que IdProducto sea valido y formatear la cadena 
	Set @IdProducto = IsNull(@IdProducto, '1')
	Set @IdProducto = right(replicate('0', 8) + @IdProducto, 8)
	
	If @iOpcion = 1 
    Begin 
		If Not Exists ( Select * From CatProductos_RegistrosSanitarios (NoLock) Where IdProducto = @IdProducto And CodigoEAN = @CodigoEAN) 
		   Begin 
			  Insert Into CatProductos_RegistrosSanitarios ( Consecutivo, Tipo, Año, IdProducto, CodigoEAN, FechaVigencia, Status, Documento, NombreDocto, Actualizado )
			  Select  @Consecutivo, @Tipo, @Año, @IdProducto, @CodigoEAN, @FechaVigencia, @Status, @Documento, @NombreDocto, @Actualizado 
		   End
        Else
		   Begin
			  Update CatProductos_RegistrosSanitarios Set Consecutivo = @Consecutivo,  Tipo = @Tipo, Año = @Año, FechaVigencia = @FechaVigencia, 
				     Status = @Status, Documento = @Documento, NombreDocto = @NombreDocto, Actualizado = @Actualizado 
			  Where  IdProducto = @IdProducto And CodigoEAN = @CodigoEAN 
		   End			
	    Set @Mensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdProducto 
	End
	
	-- Regresar la Clave Generada
    Select @IdProducto as Clave, @Mensaje as Mensaje
	
End
Go--#SQL 