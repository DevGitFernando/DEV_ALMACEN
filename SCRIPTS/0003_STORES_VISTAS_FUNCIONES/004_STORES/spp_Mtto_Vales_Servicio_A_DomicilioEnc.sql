



If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vales_Servicio_A_DomicilioEnc' and xType = 'P' )
    Drop Proc spp_Mtto_Vales_Servicio_A_DomicilioEnc
Go--#SQL
  
Create Proc spp_Mtto_Vales_Servicio_A_DomicilioEnc 
(	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioServicioDomicilio varchar(32), @FolioVale varchar(32), 
	@IdPersonal varchar(6), @HoraVisitaDesde datetime, @HoraVisitaHasta datetime, @ServicioConfirmado tinyint, @IdPersonalConfirma varchar(6),
	@TipoSurtimiento int , @ReferenciaSurtimiento varchar(30), @iOpcion smallint
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@FechaCanje varchar(10)		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	
	Select @FechaCanje =  Convert(varchar(10), GetDate(), 120 )

	If @FolioServicioDomicilio = '*' 
	   Select @FolioServicioDomicilio = cast( (max(FolioServicioDomicilio) + 1) as varchar)  From Vales_Servicio_A_Domicilio (NoLock) 
			  Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	-- Asegurar que FolioVale sea valido y formatear la cadena 
	Set @FolioServicioDomicilio = IsNull(@FolioServicioDomicilio, '1')
	Set @FolioServicioDomicilio = right(replicate('0', 8) + @FolioServicioDomicilio, 8) 

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From Vales_Servicio_A_Domicilio (NoLock) 
						   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioServicioDomicilio = @FolioServicioDomicilio ) 
			  Begin 
				 Insert Into Vales_Servicio_A_Domicilio 
					 ( IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio, FolioVale, FechaRegistro, 
					   IdPersonal, HoraVisita_Desde, HoraVisita_Hasta, ServicioConfirmado, FechaConfirmacion, 
					   IdPersonalConfirma, TipoSurtimiento, ReferenciaSurtimiento, Status, Actualizado
					 ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioServicioDomicilio, @FolioVale, GetDate(), 
					   @IdPersonal, @HoraVisitaDesde, @HoraVisitaHasta, @ServicioConfirmado, GetDate(), 
					   @IdPersonalConfirma, @TipoSurtimiento, @ReferenciaSurtimiento, @sStatus, @iActualizado
              End 
		   Else 
			  Begin 
				
				Update Vales_Servicio_A_Domicilio Set ServicioConfirmado = @ServicioConfirmado, FechaConfirmacion = GetDate(),
				IdPersonalConfirma = @IdPersonalConfirma, TipoSurtimiento = @TipoSurtimiento, ReferenciaSurtimiento = @ReferenciaSurtimiento, 
				Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioServicioDomicilio = @FolioServicioDomicilio 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioServicioDomicilio 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update Vales_Servicio_A_Domicilio Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioServicioDomicilio = @FolioServicioDomicilio
 
		   Set @sMensaje = 'La información del Folio ' + @FolioServicioDomicilio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioServicioDomicilio as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
