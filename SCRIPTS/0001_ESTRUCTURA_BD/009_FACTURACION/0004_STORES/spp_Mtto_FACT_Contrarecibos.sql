
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_Contrarecibos' and xType = 'P')
    Drop Proc spp_Mtto_FACT_Contrarecibos
Go--#SQL

  
Create Proc spp_Mtto_FACT_Contrarecibos 
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioContrarecibo varchar(12), 
	@Contrarecibo varchar(100), @FechaDocumento varchar(10), @Observaciones varchar(102), @IdPersonalFactura varchar(6), 
	@iOpcion smallint 
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
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	  End 

	-- Asegurar que FolioContrarecibo sea valido y formatear la cadena 
	Set @FolioContrarecibo = IsNull(@FolioContrarecibo, '1')
	Set @FolioContrarecibo = right(replicate('0', 10) + @FolioContrarecibo, 10)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From FACT_Contrarecibos (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
							And FolioContrarecibo = @FolioContrarecibo ) 
			  Begin 
				 Insert Into FACT_Contrarecibos ( IdEmpresa, IdEstado, IdFarmacia, FolioContrarecibo, FechaRegistro, Contrarecibo, FechaDocumento, 
								Observaciones, IdPersonalFactura, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioContrarecibo, GetDate(), @Contrarecibo,  
						@FechaDocumento, @Observaciones, @IdPersonalFactura, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update FACT_Contrarecibos Set Contrarecibo = @Contrarecibo, FechaDocumento = @FechaDocumento, Observaciones = @Observaciones, 
					IdPersonalFactura = @IdPersonalFactura, Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioContrarecibo = @FolioContrarecibo
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioContrarecibo 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update FACT_Contrarecibos Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioContrarecibo = @FolioContrarecibo 
			 
		    Set @sMensaje = 'La información del Folio ' + @FolioContrarecibo + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioContrarecibo as Folio, @sMensaje as Mensaje 
End
Go--#SQL 	
