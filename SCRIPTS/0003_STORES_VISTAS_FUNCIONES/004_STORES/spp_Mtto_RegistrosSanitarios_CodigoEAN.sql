If Exists (Select Name From SysObjects (NoLock) Where Name = 'spp_Mtto_RegistrosSanitarios_CodigoEAN' And xType = 'P')
	Drop Proc spp_Mtto_RegistrosSanitarios_CodigoEAN
Go--#SQL 

Create Proc spp_Mtto_RegistrosSanitarios_CodigoEAN
( 
	@Folio Varchar(8), @CodigoEAN varchar(30)
) 
With Encryption 
As
Begin
Set NoCount On
Set DateFormat YMD
Declare 
	@Mensaje varchar(1000),
	@Actualizado smallint,
	@IdProducto varchar(8),
	@Status Varchar(1)
	
	Set @Status = 'A'

	/*Opciones
	  Opcion 1.- Insercion / Actualizacion*/ 


	Set @Mensaje = ''	
	Set @Actualizado = 0
	
	Select @IdProducto = IdProducto From CatProductos_CodigosRelacionados Where CodigoEAN = @CodigoEAN

	

	If Not Exists ( Select * From CatRegistrosSanitarios_CodigoEAN (NoLock) Where IdProducto = @IdProducto And CodigoEAN = @CodigoEAN) 
	   Begin 
		  Insert Into CatRegistrosSanitarios_CodigoEAN ( Folio, IdProducto, CodigoEAN, Actualizado, Status )
		  Select  @Folio, @IdProducto, @CodigoEAN, @Actualizado, @Status
	   End
    Else
	   Begin
		  Update CatRegistrosSanitarios_CodigoEAN
		  Set Folio = @Folio, Status = 'A'
		  Where  IdProducto = @IdProducto And CodigoEAN = @CodigoEAN
	   End
	   	
    Select  @Mensaje as Mensaje
	
End
Go--#SQL 