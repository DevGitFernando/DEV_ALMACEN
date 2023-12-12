
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatBancos' and xType = 'P')
    Drop Proc spp_Mtto_CatBancos
Go--#SQL
  
Create Proc spp_Mtto_CatBancos ( @IdBanco varchar(3), @Descripcion varchar(102), @iOpcion smallint )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdBanco = '*'
		Begin
			Select @IdBanco = cast( (max(IdBanco) + 1) as varchar)  From CNT_CatBancos (NoLock)
		End

	Set @IdBanco = IsNull(@IdBanco, '1')
	Set @IdBanco = dbo.fg_FormatearCadena(@IdBanco, '0', 3)

	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From CNT_CatBancos (NoLock) 
							Where IdBanco =  @IdBanco) 
			  Begin 
				 Insert Into CNT_CatBancos ( IdBanco, Descripcion, Status, Actualizado ) 
				 Select @IdBanco, @Descripcion, @sStatus, @iActualizado 
              End
		   Else 
			  Begin 
			     Update CNT_CatBancos Set IdBanco = @IdBanco, Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
			     Where IdBanco =  @IdBanco 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdBanco 
	   End
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CNT_CatBancos Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdBanco =  @IdBanco 
		   Set @sMensaje = 'La información del banco ' + @IdBanco + ' ha sido cancelada satisfactoriamente.' 
	   End 

	---- Regresar la Clave Generada
    Select @IdBanco as Clave, @sMensaje as Mensaje 
End
Go--#SQL
