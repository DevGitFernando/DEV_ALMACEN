
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Ventas_TiempoAire' and xType = 'P')
    Drop Proc spp_Mtto_Ventas_TiempoAire
Go--#SQL 	

  
Create Proc spp_Mtto_Ventas_TiempoAire ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdCompania varchar(2), 
@IdFolioTiempoAire varchar(8), @IdMonto varchar(2), @Monto int, @TipoTA tinyint, @IdPersonalTA varchar(4), @NumeroCelular varchar(50), 
@IdPersonal varchar(4), @IdPersonalAutoriza varchar(4), @FechaSistema varchar(10), @iOpcion smallint )
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


	If @IdFolioTiempoAire = '*' 
	 Begin
		Select @IdFolioTiempoAire = cast( (max(IdFolioTiempoAire) + 1) as varchar)  
		From Ventas_TiempoAire (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	 End

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdFolioTiempoAire = IsNull(@IdFolioTiempoAire, '1')
	Set @IdFolioTiempoAire = right(replicate('0', 8) + @IdFolioTiempoAire, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From Ventas_TiempoAire (NoLock) Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCompania = @IdCompania And IdFolioTiempoAire = @IdFolioTiempoAire ) 
			  Begin 
				 Insert Into Ventas_TiempoAire ( IdEmpresa, IdEstado, IdFarmacia, IdCompania, IdFolioTiempoAire, IdMonto, Monto, TipoTA, IdPersonalTA, NumeroCelular, IdPersonal, IdPersonalAutoriza, FechaSistema, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdCompania, @IdFolioTiempoAire, @IdMonto, @Monto, @TipoTA, @IdPersonalTA, @NumeroCelular, @IdPersonal, @IdPersonalAutoriza, @FechaSistema, @sStatus, @iActualizado 
              End 

		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdFolioTiempoAire 
	   End 
    Else 
       Begin 
        Set @sStatus = 'C' 
	    Update Ventas_TiempoAire Set Status = @sStatus, Actualizado = @iActualizado 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCompania = @IdCompania 
		And IdFolioTiempoAire = @IdFolioTiempoAire 

		Set @sMensaje = 'La información del Folio de Tiempo Aire ' + @IdFolioTiempoAire + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdFolioTiempoAire as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
