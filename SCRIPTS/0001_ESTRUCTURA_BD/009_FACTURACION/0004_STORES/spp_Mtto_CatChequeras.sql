If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatChequeras' and xType = 'P')
    Drop Proc spp_Mtto_CatChequeras
Go--#SQL
  
Create Proc spp_Mtto_CatChequeras
	( @IdEmpresa Varchar(3), @IdEstado Varchar(2), @IdChequera varchar(6), @Descripcion varchar(102),
	  @IdBanco Varchar(3), @FolioInicio Int, @FolioFin Int, @NumeroDeSerie Varchar(20), @iOpcion smallint )
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


	If @IdChequera = '*'
		Begin
			Select @IdChequera = cast( (max(IdChequera) + 1) as varchar)  From CNT_CatChequeras (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado
		End

	Set @IdChequera = IsNull(@IdChequera, '1')
	Set @IdChequera = dbo.fg_FormatearCadena(@IdChequera, '0', 6)

	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From CNT_CatChequeras (NoLock) 
							Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdChequera =  @IdChequera) 
			  Begin 
				 Insert Into CNT_CatChequeras ( IdEmpresa, IdEstado, IdChequera, Descripcion, IdBanco, FolioInicio, FolioFin, NumeroDeSerie, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdChequera, @Descripcion, @IdBanco, @FolioInicio, @FolioFin, @NumeroDeSerie, @sStatus, @iActualizado 
              End
		   Else 
			  Begin 
			     Update CNT_CatChequeras
			     Set Descripcion = @Descripcion, IdBanco = @IdBanco, FolioInicio = @FolioInicio, FolioFin = @FolioFin,
					NumeroDeSerie = @NumeroDeSerie, Status = @sStatus, Actualizado = @iActualizado
			     Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdChequera =  @IdChequera 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdChequera 
	   End
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CNT_CatChequeras Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdChequera =  @IdChequera 
		   Set @sMensaje = 'La información de la chequera ' + @IdChequera + ' ha sido cancelada satisfactoriamente.' 
	   End 

	---- Regresar la Clave Generada
    Select @IdChequera as Clave, @sMensaje as Mensaje 
End
Go--#SQL