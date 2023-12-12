----------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_INT_MA__FACT_Contrarecibos' and xType = 'P')
    Drop Proc spp_INT_MA__FACT_Contrarecibos
Go--#SQL 
  
Create Proc spp_INT_MA__FACT_Contrarecibos 
(	
	@IdEmpresa varchar(5) = '002', @IdEstado varchar(4) = '09', @IdFarmaciaGenera varchar(6) = '0001', 
	@FolioContrarecibo varchar(12) = '*', 
	@Contrarecibo varchar(100) = '', @FechaDocumento varchar(10) = '', 
	@Observaciones varchar(102) = '', @IdPersonalCaptura varchar(6) = '0001', 
	@iOpcion smallint = 1 
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
	--Set @FolioMovtoInv = ''	


	If @FolioContrarecibo = '*' 
	  Begin 
		Select @FolioContrarecibo = cast( (max(FolioContrarecibo) + 1) as varchar)  
		From FACT_Contrarecibos (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera 
	  End 

	-- Asegurar que FolioContrarecibo sea valido y formatear la cadena 
	Set @FolioContrarecibo = IsNull(@FolioContrarecibo, '1')
	Set @FolioContrarecibo = right(replicate('0', 10) + @FolioContrarecibo, 10)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From FACT_Contrarecibos (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera 
							And FolioContrarecibo = @FolioContrarecibo ) 
			  Begin 
				 Insert Into FACT_Contrarecibos ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioContrarecibo, FechaRegistro, Contrarecibo, FechaDocumento, 
								Observaciones, IdPersonalCaptura, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmaciaGenera, @FolioContrarecibo, GetDate(), @Contrarecibo,  
						@FechaDocumento, @Observaciones, @IdPersonalCaptura, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update FACT_Contrarecibos Set Contrarecibo = @Contrarecibo, FechaDocumento = @FechaDocumento, Observaciones = @Observaciones, 
					IdPersonalCaptura = @IdPersonalCaptura, Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioContrarecibo = @FolioContrarecibo
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioContrarecibo 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update FACT_Contrarecibos Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmaciaGenera = @IdFarmaciaGenera And FolioContrarecibo = @FolioContrarecibo 
			 
		    Set @sMensaje = 'La información del Folio ' + @FolioContrarecibo + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioContrarecibo as Folio, @sMensaje as Mensaje 
    
End
Go--#SQL 	
