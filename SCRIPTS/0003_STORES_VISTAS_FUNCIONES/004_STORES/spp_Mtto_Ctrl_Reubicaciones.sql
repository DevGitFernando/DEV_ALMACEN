


If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Ctrl_Reubicaciones' And xType = 'P' )
	Drop Proc spp_Mtto_Ctrl_Reubicaciones
Go--#SQL

Create Procedure spp_Mtto_Ctrl_Reubicaciones 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @Folio_Inv varchar(30), @FolioMovto_Referencia varchar(30),
	@IdPersonal varchar(4), @IdPersonal_Asignado varchar(4), @iOpcion int = 1 
)
With Encryption
As
Begin
	Declare @Status varchar(1), 
			@Actualizado int,
			@sMensaje varchar(8000)
			
	Set @Actualizado = 0
	Set @sMensaje = ''
	
	If @iOpcion = 1
	 Begin
		If Not Exists( Select *
					   From Ctrl_Reubicaciones(NoLock)
					   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio_Inv = @Folio_Inv)
		 Begin
			Insert Into Ctrl_Reubicaciones ( IdEmpresa, IdEstado, IdFarmacia, Folio_Inv, FolioMovto_Referencia,
								IdPersonal, IdPersonal_Asignado,Actualizado )
			Select 	@IdEmpresa, @IdEstado, @IdFarmacia, @Folio_Inv, @FolioMovto_Referencia,
								@IdPersonal, @IdPersonal_Asignado, @Actualizado
		 End
		Set @sMensaje = 'La información de la reubicación ' + @Folio_Inv + ' se guardo exitosamente'
	 End
	Else
	 Begin
		Update Ctrl_Reubicaciones
		Set Status = @Status, Actualizado = @Actualizado
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio_Inv = @Folio_Inv
		Set @sMensaje = 'La información de la reubicación ' + @Folio_Inv + ' ha sido cancelada satisfactoriamente.' 
	 End

	-- Regresar la Clave Generada
    Select @Folio_Inv as Folio_Inv, @sMensaje as Mensaje 

End
Go--#SQL


