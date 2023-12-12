
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProductos_CodigosRelacionados' and xType = 'P')
    Drop Proc spp_Mtto_CatProductos_CodigosRelacionados
Go--#SQL
  
Create Proc spp_Mtto_CatProductos_CodigosRelacionados 
( 
	@IdProducto varchar(10) = '00000001', @CodigoEAN varchar(32) = '7501563010091', @iStatus smallint = 1,
	@ContenidoCorrugado int = 0, @Cajas_Cama int = 0, @Cajas_Tarima int = 0, @ContenidoPiezasUnitario int = 0
)
With Encryption 
As
Begin
Set NoCount On

Declare 
	@iActualizado smallint, 
	@sStatus varchar(1),
	@CodigoEAN_Interno varchar(32)
	
	Set @sStatus = ''
	Set @CodigoEAN_Interno = ''
	Set @iActualizado = 0

	If @iStatus = 1 
		Set @sStatus = 'A'
	Else
		Set @sStatus = 'C'

	-- Se obtiene el CodigoEAN_Interno 
    ---- Select @CodigoEAN_Interno = cast( (max(CodigoEAN_Interno) + 1) as varchar)  From CatProductos_CodigosRelacionados (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @CodigoEAN_Interno = IsNull(@IdProducto, '1')    ---- El Codigo Interno es el IdProducto formateado a 13 caracteres 
	Set @CodigoEAN_Interno = right(replicate('0', 30) + @CodigoEAN_Interno, 13)

	-- Si el Producto no maneja codigo EAN se asigna el codigo interno.
	If @CodigoEAN = ''
	  Begin
		Set @CodigoEAN = @CodigoEAN_Interno
		Set @sStatus = 'A'
	  End

	If Not Exists ( Select * From CatProductos_CodigosRelacionados (NoLock) Where IdProducto = @IdProducto And CodigoEAN =@CodigoEAN ) 
	  Begin 
		 Insert Into CatProductos_CodigosRelacionados 
		 ( 
				IdProducto, CodigoEAN, CodigoEAN_Interno, Status, Actualizado, ContenidoCorrugado, Cajas_Cama, Cajas_Tarima, ContenidoPiezasUnitario 
		 ) 
		 Select @IdProducto, @CodigoEAN, @CodigoEAN_Interno, @sStatus, @iActualizado, @ContenidoCorrugado, @Cajas_Cama, @Cajas_Tarima, @ContenidoPiezasUnitario
      End 

   Else 
	  Begin 
	     Update CatProductos_CodigosRelacionados Set Status = @sStatus, Actualizado = @iActualizado, 
	     ContenidoCorrugado = @ContenidoCorrugado, Cajas_Cama = @Cajas_Cama, Cajas_Tarima = @Cajas_Tarima, ContenidoPiezasUnitario = @ContenidoPiezasUnitario 
		 Where IdProducto = @IdProducto And CodigoEAN = @CodigoEAN
      End 

	-- Regresar la Clave Generada
    Select @CodigoEAN_Interno as Clave --, @sMensaje as Mensaje 
End
Go--#SQL
