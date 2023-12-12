
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_ListaDePrecios' And xType = 'P' )
	Drop Proc spp_Mtto_COM_OCEN_ListaDePrecios 
Go--#SQL

Create Procedure spp_Mtto_COM_OCEN_ListaDePrecios ( 
	@IdProveedor varchar(4), @IdClaveSSA varchar(4), @CodigoEAN varchar(30), 
	@Precio varchar(20), @Descuento Numeric(14,4), @TasaIva Numeric(14,4), @Iva varchar(20), @PrecioUnitario varchar(20), 
	@FechaRegistro varchar(10), @FechaFinVigencia varchar(10), @iOpcion smallint )
As
Begin
	Declare
	  @Actualizado tinyint,
	  @sMensaje varchar(1000),
	  @Status varchar(1),
	  @Cadena varchar(30),
	  @nPrecioUnitario numeric(14, 4),
	  @nPrecio numeric(14, 4),
	  @nIva numeric(14, 4)

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar 
	*/

	Set @Actualizado = 0
	Set @sMensaje = ''
	Set @Status = 'A'
	Set @Cadena = ''
	Set @nPrecioUnitario = 0
	Set @nIva = 0
	Set @nPrecio = 0		

	Set @Cadena = Replace(@Precio,',','')
	Set @Precio = @Cadena
	
	Set @Cadena = Replace(@Iva,',','')
	Set @Iva = @Cadena

	Set @Cadena = Replace(@PrecioUnitario,',','')
	Set @PrecioUnitario = @Cadena

	Set @nPrecio = Cast(@Precio As Numeric(14, 4))
	Set @nPrecioUnitario = Cast(@PrecioUnitario As Numeric(14, 4))
	Set @nIva = Cast(@Iva As Numeric(14, 4))

	If @TasaIva > 0
		Begin
			Set @nIva = ( Select Round( @nIva, 2) )
			Set @nPrecioUnitario = @nPrecio + @nIva
		End

	if @iOpcion = 1 
	  Begin
		-- Se guarda la informacion en la tabla spp_Mtto_COM_OCEN_ListaDePrecios
		If Not Exists ( Select * From COM_OCEN_ListaDePrecios (NoLock) 
						Where IdProveedor = @IdProveedor And @IdClaveSSA = IdClaveSSA And CodigoEAN = @CodigoEAN )
		  Begin
			Insert Into COM_OCEN_ListaDePrecios ( IdProveedor, IdClaveSSA, CodigoEAN, Precio, Descuento, TasaIva, Iva, 
												PrecioUnitario, FechaRegistro, FechaFinVigencia, Status, Actualizado )
			Select @IdProveedor, @IdClaveSSA, @CodigoEAN, @nPrecio, @Descuento, @TasaIva, @nIva, 
			@nPrecioUnitario, @FechaRegistro, @FechaFinVigencia, @Status, @Actualizado
		  End
		Else
		  Begin
			Update COM_OCEN_ListaDePrecios 
			Set Precio = @nPrecio, Descuento = @Descuento, TasaIva = @TasaIva, Iva = @nIva, 
				PrecioUnitario = @nPrecioUnitario, FechaRegistro = @FechaRegistro, 
				FechaFinVigencia = @FechaFinVigencia, Status = @Status, Actualizado = @Actualizado
			Where IdProveedor = @IdProveedor And IdClaveSSA = @IdClaveSSA And CodigoEAN = @CodigoEAN
		  End

		Set @sMensaje = 'La información de Clave - Codigo EAN ' + @IdClaveSSA + ' - ' + @CodigoEAN + ' se guardo satisfactoriamente. ' 
	  End
	Else
	  Begin
		Set @Status = 'C'
		Update COM_OCEN_ListaDePrecios Set Status = @Status, Actualizado = @Actualizado
		Where IdProveedor = @IdProveedor And @IdClaveSSA = IdClaveSSA And CodigoEAN = @CodigoEAN

		Set @sMensaje = 'La información de Clave - Codigo EAN ' + @IdClaveSSA + ' - ' + @CodigoEAN + ' ha sido cancelada satisfactoriamente.'
	  End 
	  
	-- Se guarda la informacion en la tabla COM_OCEN_ListaDePreciosHistorico 
	Insert Into COM_OCEN_ListaDePreciosHistorico 
		( IdProveedor, IdClaveSSA, CodigoEAN, Precio, Descuento, TasaIva, Iva, PrecioUnitario, FechaActualizacion, FechaRegistro, FechaFinVigencia, Status, Actualizado ) 
	Select IdProveedor, IdClaveSSA, CodigoEAN, Precio, Descuento, TasaIva, Iva, PrecioUnitario, Getdate(), FechaRegistro, FechaFinVigencia, Status, Actualizado 	
	From COM_OCEN_ListaDePrecios (Nolock)
	Where IdProveedor = @IdProveedor And IdClaveSSA = @IdClaveSSA And CodigoEAN = @CodigoEAN  
			
	Select @sMensaje as Mensaje  
	
End
Go--#SQL
