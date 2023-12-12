If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Parentesco' and xType = 'P')
    Drop Proc spp_Mtto_Parentesco
Go--#SQL 

Create Proc spp_Mtto_Parentesco ( @Folio varchar(2), @Descripcion varchar(100), @iOpcion int)

With Encryption 
As 
Begin 
Set NoCount On 
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	
	If @Folio = '*'
	Begin 
		Select @Folio = max(Folio) + 1 
		From CatParentescos (NoLock) 
	End
	
	print @Folio
	
	Set @Folio = IsNull(@Folio, '1') 
	Set @Folio = right(replicate('0', 2) + @Folio, 2)
	
	print @Folio


	If @iOpcion = 1
		Begin 

			If Not Exists ( Select * From CatParentescos (NoLock) Where Folio = @Folio ) 
			   Insert Into CatParentescos ( Folio, Descripcion, Status, Actualizado ) 
			   Select @Folio, @Descripcion, @sStatus, @iActualizado
			Else 
			   Update CatParentescos Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = 0 
			   Where Folio = @Folio
			Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @Folio 
		End
	Else
		Begin 
			Set @sStatus = 'C'
			Update CatParentescos Set Status = @sStatus, Actualizado = 0 
			Where Folio = @Folio
			Set @sMensaje = 'La información del parentesco ' + @Folio + ' ha sido cancelada satisfactoriamente.' 
		End

	-- Devolver el resultado
	Select @Folio as Folio, @sMensaje as Mensaje
End 
Go--#SQL 