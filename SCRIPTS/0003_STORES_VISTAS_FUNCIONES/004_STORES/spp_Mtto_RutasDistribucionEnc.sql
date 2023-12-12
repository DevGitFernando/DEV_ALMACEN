
	If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_RutasDistribucionEnc' and xType = 'P')
    Drop Proc spp_Mtto_RutasDistribucionEnc
Go--#SQL
  
Create Proc spp_Mtto_RutasDistribucionEnc ( @IdEmpresa Varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @Folio varchar(8), @IdVehiculo varchar(8),
			@IdPersonal varchar(4), @Observaciones Varchar(Max), @iOpcion smallint, @IdPersonalCaptura varchar(4) = '' )
With Encryption 
As
Begin
Set DateFormat YMD
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


	If @Folio = '*' 
	   Select @Folio = cast( (max(Folio) + 1) as varchar)  From RutasDistribucionEnc (NoLock)
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

	-- Asegurar que el Folio sea valido y formatear la cadena 
	Set @Folio = IsNull(@Folio, '1')
	Set @Folio = right(replicate('0', 8) + @Folio, 8)

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From RutasDistribucionEnc (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio ) 
			  Begin 
				 Insert Into RutasDistribucionEnc ( IdEmpresa, IdEstado, IdFarmacia, Folio, IdVehiculo, IdPersonal, Observaciones, Status, Actualizado, IdPersonalCaptura ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @IdVehiculo, @IdPersonal, @Observaciones, @sStatus, @iActualizado, @IdPersonalCaptura
              End  
		   Else 
			  Begin 
			     Update RutasDistribucionEnc
			     Set IdVehiculo = @IdVehiculo, IdPersonal = @IdPersonal, Observaciones = @Observaciones, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @Folio 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update RutasDistribucionEnc Set Status = @sStatus, Actualizado = @iActualizado
	       Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio 
		   Set @sMensaje = 'La información de la ruta ' + @Folio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @Folio As Folio, @sMensaje as Mensaje 
End
Go--#SQL