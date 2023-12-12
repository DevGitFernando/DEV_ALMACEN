
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CtlCortesParciales' and xType = 'P')
    Drop Proc spp_Mtto_CtlCortesParciales
Go--#SQL
  
Create Proc spp_Mtto_CtlCortesParciales 
( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdPersonal varchar(4), 
  @FechaSistema datetime, @IdPersonalCorte varchar(4) = '', @DotacionInicial numeric(14, 4) = 0, 
  @VentaDiaContado numeric(14, 4) = 0, @VentaDiaCredito numeric(14, 4) = 0, @DevVentaDiaContado numeric(14, 4) = 0, 
  @DevVentaDiaCredito numeric(14, 4) = 0, @DevVentaDiaAntContado numeric(14, 4) = 0, 
  @DevVentaDiaAntCredito numeric(14, 4) = 0, @VentaTAContado int = 0, @VentaTACredito int = 0,
  @Comentario varchar(300) = '', @iOpcion smallint = 1 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), @IdCorte varchar(2), 
		@sStatus varchar(1), @iActualizado smallint

	/*Opciones
	Opcion 1.- Insercion. Esta se lleva a cabo cada vez que el usuario entra a la pantalla de Ventas ó Ventas de Tiempo Aire.
	Opcion 2.- Actualizacion. Esta se lleva a cabo cuando el usuario efectua su corte parcial.
	*/
	Set dateformat YMD 
	Set @sMensaje = ''
	Set @IdCorte = ''
	Set @iActualizado = 0 

	If @iOpcion = 1
	  Begin

		-- Se verifica si existe algun activo.
		Select @IdCorte = IsNull( IdCorte, '*' )
		From CtlCortesParciales(NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdPersonal = @IdPersonal 
			  And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And Status = 'A'

		If @IdCorte <> '*' 
		  Begin
			-- Si no encontro ninguno Activo, genera un nuevo IdCorte
		    Select @IdCorte = cast( (max(IdCorte) + 1) as varchar)  
			From CtlCortesParciales (NoLock) 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdPersonal = @IdPersonal 
				  And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And Status = 'C'

			-- Asegurar que IdFamlia sea valido y formatear la cadena 
			Set @IdCorte = IsNull(@IdCorte, '1')
			Set @IdCorte = right(replicate('0', 2) + @IdCorte, 2)


			If Not Exists ( Select * From CtlCortesParciales (NoLock) 
							Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdPersonal = @IdPersonal 
							And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And Status = 'A' ) 
			  Begin 
				 Insert Into CtlCortesParciales ( IdEmpresa, IdEstado, IdFarmacia,IdPersonal, IdCorte, FechaSistema, IdPersonalCorte, FechaCierre, DotacionInicial, VentaDiaContado, VentaDiaCredito, DevVentaDiaContado, DevVentaDiaCredito,DevVentaDiaAntContado, DevVentaDiaAntCredito, VentaTAContado, VentaTACredito, Comentario, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @IdCorte, @FechaSistema, @IdPersonalCorte, GetDate(), @DotacionInicial, @VentaDiaContado, @VentaDiaCredito, @DevVentaDiaContado, @DevVentaDiaCredito, @DevVentaDiaAntContado, @DevVentaDiaAntCredito, @VentaTAContado, @VentaTACredito, '', 'A', @iActualizado 
			  End 
		  End
	  End
	Else
	  Begin

		-- Se obtiene el IdCorte
		Select @IdCorte = IdCorte
		From CtlCortesParciales(NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdPersonal = @IdPersonal 
			  And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And Status = 'A'

		-- Se actualizan las cantidades y fechas del Corte Parcial del usuario
		Update CtlCortesParciales
		Set FechaCierre = GetDate(), IdPersonalCorte = @IdPersonalCorte, DotacionInicial = @DotacionInicial, 
			VentaDiaContado = @VentaDiaContado, VentaDiaCredito = @VentaDiaCredito,
			DevVentaDiaContado = @DevVentaDiaContado, DevVentaDiaCredito = @DevVentaDiaCredito, DevVentaDiaAntContado = @DevVentaDiaAntContado, 
			DevVentaDiaAntCredito = @DevVentaDiaAntCredito, VentaTAContado = @VentaTAContado, VentaTACredito = @VentaTACredito,
			Comentario = @Comentario, Status = 'C', Actualizado = @iActualizado
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdPersonal = @IdPersonal 
			  And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And Status = 'A'

		Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdCorte

		----------------------------------------------------------------------------------------------
		-- Se actualiza el campo Corte de las tablas VentasEnc, DevolucionesEnc y Ventas_TiempoAire --
		----------------------------------------------------------------------------------------------

		Update VentasEnc
		Set Corte = 1
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
		And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And IdPersonal = @IdPersonal

		Update DevolucionesEnc
		Set Corte = 1
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
		And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And IdPersonal = @IdPersonal

		Update Ventas_TiempoAire
		Set Corte = 1
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
		And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And IdPersonal = @IdPersonal		

	  End
		   
	-- Regresar la Clave Generada
    Select @IdCorte as Clave, @sMensaje as Mensaje 

End
Go--#SQL
