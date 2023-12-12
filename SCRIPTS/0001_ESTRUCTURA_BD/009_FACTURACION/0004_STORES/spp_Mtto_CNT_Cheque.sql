If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CNT_Cheque' and xType = 'P')
    Drop Proc spp_Mtto_CNT_Cheque
Go--#SQL
  
Create Proc spp_Mtto_CNT_Cheque
	( @IdEmpresa Varchar(3), @IdEstado Varchar(2), @IdCheque varchar(6), @FolioCheque Int, @Descripcion varchar(102),
	  @IdChequera Varchar(6), @IdBeneficiario Varchar(6), @Cantidad Numeric(14,4), @FechaRegistro DateTime, @iOpcion smallint )
With Encryption 
As
Begin
Set NoCount On
Set DateFormat YMD

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdCheque = '*'
		Begin
			Select @IdCheque = cast( (max(IdCheque) + 1) as varchar)  From CNT_Cheque (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado
		End

	Set @IdCheque = IsNull(@IdCheque, '1')
	Set @IdCheque = dbo.fg_FormatearCadena(@IdCheque, '0', 6)
	


	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From CNT_Cheque (NoLock) 
							Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdCheque =  @IdCheque) 
			  Begin 
				 Insert Into CNT_Cheque ( IdEmpresa, IdEstado, IdCheque, FolioCheque, Descripcion, IdChequera, IdBeneficiario, Cantidad, FechaRegistro, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdCheque, @FolioCheque, @Descripcion, @IdChequera, @IdBeneficiario, @Cantidad, @FechaRegistro, @sStatus, @iActualizado
				 Update CNT_CatChequeras Set UltimoFolio =  @FolioCheque Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdChequera = @IdChequera
              End
		   Else 
			  Begin 
			     Update CNT_Cheque
			     Set Descripcion = @Descripcion, IdChequera = @IdChequera, IdBeneficiario = @IdBeneficiario, Cantidad = @Cantidad,
					FechaRegistro = @FechaRegistro, Status = @sStatus, Actualizado = @iActualizado
			     Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdCheque =  @IdCheque 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdCheque 
	   End
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CNT_Cheque Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdCheque =  @IdCheque 
		   Set @sMensaje = 'La información del cheque ' + @IdCheque + ' ha sido cancelada satisfactoriamente.' 
	   End 

	---- Regresar la Clave Generada
    Select @IdCheque as Clave, @sMensaje as Mensaje 
End
Go--#SQL