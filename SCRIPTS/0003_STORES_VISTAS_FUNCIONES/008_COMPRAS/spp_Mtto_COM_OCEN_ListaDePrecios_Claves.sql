
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_ListaDePrecios_Claves' And xType = 'P' )
	Drop Proc spp_Mtto_COM_OCEN_ListaDePrecios_Claves 
Go--#SQL

Create Procedure dbo.spp_Mtto_COM_OCEN_ListaDePrecios_Claves ( 
	@IdProveedor varchar(4), @IdClaveSSA varchar(4), @Precio varchar(20), @Descuento Numeric(14,4), @TasaIva Numeric(14,4), @Iva varchar(20), 
	@Importe varchar(20), @FechaRegistro varchar(10), @FechaFinVigencia varchar(10), @iOpcion smallint )
As
Begin
	Declare
	  @Actualizado tinyint,
	  @sMensaje varchar(1000),
	  @Status varchar(1),
	  @Cadena varchar(30)

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar 
	*/

	Set @Actualizado = 0
	Set @sMensaje = ''
	Set @Status = 'A'
	Set @Cadena = ''

	Set @Cadena = Replace(@Precio,',','')
	Set @Precio = @Cadena
	
	Set @Cadena = Replace(@Iva,',','')
	Set @Iva = @Cadena

	Set @Cadena = Replace(@Importe,',','')
	Set @Importe = @Cadena

	if @iOpcion = 1 
	  Begin
		-- Se guarda la informacion en la tabla spp_Mtto_COM_OCEN_ListaDePrecios
		If Not Exists ( Select * From COM_OCEN_ListaDePrecios_Claves (NoLock) 
						Where IdProveedor = @IdProveedor And @IdClaveSSA = IdClaveSSA)
		  Begin
			Insert Into COM_OCEN_ListaDePrecios_Claves ( IdProveedor, IdClaveSSA, Precio, Descuento, TasaIva, Iva, 
					PrecioUnitario, FechaRegistro, FechaFinVigencia, Status, Actualizado )
			Select @IdProveedor, @IdClaveSSA, Cast(@Precio As Numeric(14, 4)), @Descuento, @TasaIva, Cast(@Iva As Numeric(14, 4)), 
			Cast(@Importe As Numeric(14,4)), @FechaRegistro, @FechaFinVigencia, @Status, @Actualizado
		  End
		Else
		  Begin
			Update COM_OCEN_ListaDePrecios_Claves 
			Set Precio = Cast(@Precio As Numeric(14, 4)), Descuento = @Descuento, TasaIva = @TasaIva, Iva = Cast(@Iva As Numeric(14, 4)), 
				PrecioUnitario = Cast(@Importe As Numeric(14,4)), FechaRegistro = @FechaRegistro, 
				FechaFinVigencia = @FechaFinVigencia, Status = @Status, Actualizado = @Actualizado
			Where IdProveedor = @IdProveedor And IdClaveSSA = @IdClaveSSA 
		  End

		Set @sMensaje = 'La información de Clave ' + @IdClaveSSA + ' se guardo satisfactoriamente. ' 
	  End
	Else
	  Begin
		Set @Status = 'C'
		Update COM_OCEN_ListaDePrecios_Claves Set Status = @Status, Actualizado = @Actualizado
		Where IdProveedor = @IdProveedor And @IdClaveSSA = IdClaveSSA 

		Set @sMensaje = 'La información de Clave ' + @IdClaveSSA + ' ha sido cancelada satisfactoriamente.'
	  End 
	  
	-- Se guarda la informacion en la tabla COM_OCEN_ListaDePreciosHistorico 
	Insert Into COM_OCEN_ListaDePreciosHistorico_Claves 
		( IdProveedor, IdClaveSSA, Precio, Descuento, TasaIva, Iva, PrecioUnitario, FechaRegistro, FechaFinVigencia, Status, Actualizado ) 
	Select IdProveedor, IdClaveSSA, Precio, Descuento, TasaIva, Iva, PrecioUnitario, FechaRegistro, FechaFinVigencia, Status, Actualizado 	
	From COM_OCEN_ListaDePrecios_Claves 
	Where IdProveedor = @IdProveedor And IdClaveSSA = @IdClaveSSA
			
	Select @sMensaje as Mensaje  
	
End
Go--#SQL
