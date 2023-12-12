
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_ValesEnc' and xType = 'P')
    Drop Proc spp_Mtto_ValesEnc
Go--#SQL
  
Create Proc spp_Mtto_ValesEnc 
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @Folio varchar(32), @FolioVale varchar(32), 
	@IdPersonal varchar(6), @IdProveedor varchar(6), 
	@ReferenciaDocto varchar(22), 
	@FechaDocto datetime, @FechaVenceDocto datetime, @Observaciones varchar(102), 
    @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4), 
	@FechaRegistro datetime, @EsReembolso tinyint = 0, @iOpcion smallint
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
	
	If @Folio = '*' 
	  Begin 
		Select @Folio = cast( (max(Folio) + 1) as varchar)  
		From ValesEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
	  End 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @Folio = IsNull(@Folio, '1')
	Set @Folio = right(replicate('0', 8) + @Folio, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From ValesEnc (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio ) 
			  Begin 
				 Insert Into ValesEnc 
					 ( IdEmpresa, IdEstado, IdFarmacia, Folio, FolioVale, IdPersonal, 
					   IdProveedor, ReferenciaDocto, FechaDocto, FechaVenceDocto, Observaciones, SubTotal, Iva, 
					   Total, -- FechaRegistro,
					   EsReembolso, 
					   Status, Actualizado 
					 ) 
				 Select 
					   @IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @FolioVale, @IdPersonal, 
					   @IdProveedor, @ReferenciaDocto, @FechaDocto, @FechaVenceDocto, @Observaciones,@SubTotal, @Iva, 
					   @Total, -- @FechaRegistro,
					   @EsReembolso, 
					   @sStatus, @iActualizado 
				
				--- Marcar el Vale como ya Canjeado 	   
                Set @sStatus = 'R' 
				Update Vales_EmisionEnc Set FechaCanje = getdate(), Status = @sStatus, Actualizado = @iActualizado 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale 
									   
              End 
		   Else 
			  Begin 
				Update ValesEnc Set IdProveedor = @IdProveedor, ReferenciaDocto = @ReferenciaDocto, 
					FechaDocto = @FechaDocto, FechaVenceDocto = @FechaVenceDocto, Observaciones = @Observaciones, 
					SubTotal = @SubTotal, Iva = @Iva, Total = @Total, -- FechaRegistro = @FechaRegistro, 
					Status = @sStatus, Actualizado = @iActualizado, EsReembolso = @EsReembolso
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @Folio 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update ValesEnc Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Folio = @Folio
 
		   Set @sMensaje = 'La información del Folio ' + @Folio + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @Folio as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
