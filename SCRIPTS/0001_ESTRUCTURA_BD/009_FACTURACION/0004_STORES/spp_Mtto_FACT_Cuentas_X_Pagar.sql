


If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_Cuentas_X_Pagar' and xType = 'P')
    Drop Proc spp_Mtto_FACT_Cuentas_X_Pagar
Go--#SQL

-- Select * From FACT_Cuentas_X_Pagar (Nolock)
  
Create Proc spp_Mtto_FACT_Cuentas_X_Pagar 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001',
	@FolioCuenta varchar(6) = '*', @IdServicio varchar(3) = '001', @IdAcreedor varchar(4) = '0001',
	@IdPersonal varchar(4) = '0001', @ReferenciaDocumento varchar(20) = '65465', @FechaDocumento varchar(10) = '2012-10-01',
	@IdMetodoPago varchar(2) = '01', @SubTotal numeric(14, 4) = 0, @TasaIva numeric(14, 4) = 0, @Iva numeric(14, 4) = 0,
	@Total numeric(14, 4) = 0, @Observaciones varchar(100) = 'prueba', @iOpcion smallint = 1 
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint
		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0


	If @FolioCuenta = '*' 
	  Begin 
		Select @FolioCuenta = cast( (max(FolioCuenta) + 1) as varchar)  
		From FACT_Cuentas_X_Pagar (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia		
	  End 

	-- Asegurar que FolioContrarecibo sea valido y formatear la cadena 
	Set @FolioCuenta = IsNull(@FolioCuenta, '1')
	Set @FolioCuenta = right(replicate('0', 6) + @FolioCuenta, 6)


	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From FACT_Cuentas_X_Pagar (NoLock) 
						   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCuenta = @FolioCuenta ) 
			  Begin 
				 Insert Into FACT_Cuentas_X_Pagar ( IdEmpresa, IdEstado, IdFarmacia, FolioCuenta, IdServicio, IdAcreedor, FechaRegistro, 
												IdPersonal, ReferenciaDocumento, FechaDocumento, IdMetodoPago, SubTotal, TasaIva, Iva, Total, 
												Observaciones, Status, Actualizado ) 
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioCuenta, @IdServicio, @IdAcreedor, GetDate(), 
				@IdPersonal, @ReferenciaDocumento, @FechaDocumento, @IdMetodoPago, @SubTotal, @TasaIva, @Iva, @Total, 
				@Observaciones, @sStatus, @iActualizado
              End 
		   Else 
			  Begin 
				Update FACT_Cuentas_X_Pagar Set IdServicio = @IdServicio, IdAcreedor = @IdAcreedor, ReferenciaDocumento = @ReferenciaDocumento,
				FechaDocumento = @FechaDocumento, IdMetodoPago = @IdMetodoPago, SubTotal = @SubTotal, TasaIva = @TasaIva, Iva = @Iva, Total = @Total, 
				Observaciones = @Observaciones, Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCuenta = @FolioCuenta
              End
 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioCuenta 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update FACT_Cuentas_X_Pagar Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCuenta = @FolioCuenta
			 
		    Set @sMensaje = 'La información del Folio ' + @FolioCuenta + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioCuenta as Folio, @sMensaje as Mensaje 
End
Go--#SQL 	
