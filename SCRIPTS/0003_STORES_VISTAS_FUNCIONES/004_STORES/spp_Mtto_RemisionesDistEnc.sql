
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_RemisionesDistEnc' and xType = 'P')
    Drop Proc spp_Mtto_RemisionesDistEnc
Go--#SQL

  
Create Proc spp_Mtto_RemisionesDistEnc 
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioRemision varchar(32), 
	@IdDistribuidor varchar(6), @ReferenciaPedido varchar(22), @CodigoCliente varchar(20) = 'CODIGO', 
	@FechaDocumento varchar(10), 
	@Observaciones varchar(102), @IdPersonal varchar(6), @EsConsignacion tinyint, @EsExcepcion tinyint,
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
	Set @sStatus = 'T'
	Set @iActualizado = 0	
	--Set @FolioMovtoInv = ''	


	If @FolioRemision = '*' 
	  Begin 
		Select @FolioRemision = cast( (max(FolioRemision) + 1) as varchar)  
		From RemisionesDistEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor
	  End 

	-- Asegurar que FolioRemision sea valido y formatear la cadena 
	Set @FolioRemision = IsNull(@FolioRemision, '1')
	Set @FolioRemision = right(replicate('0', 8) + @FolioRemision, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From RemisionesDistEnc (NoLock) 
						   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
							And FolioRemision = @FolioRemision and IdDistribuidor = @IdDistribuidor ) 
			  Begin 
				 Insert Into RemisionesDistEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioRemision, IdDistribuidor, ReferenciaPedido, 
						CodigoCliente, FechaDocumento, Observaciones, IdPersonal, FechaRegistro, EsConsignacion, EsExcepcion, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioRemision, @IdDistribuidor, @ReferenciaPedido, 
						@CodigoCliente, @FechaDocumento, @Observaciones, @IdPersonal, GetDate(), @EsConsignacion, @EsExcepcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update RemisionesDistEnc Set 
					IdPersonal = @IdPersonal, IdDistribuidor = @IdDistribuidor, ReferenciaPedido = @ReferenciaPedido, 
					CodigoCliente = @CodigoCliente, Observaciones = @Observaciones, 
					EsConsignacion = @EsConsignacion, EsExcepcion = @EsExcepcion,
					Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioRemision = @FolioRemision
				and IdDistribuidor = @IdDistribuidor 

              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioRemision 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update RemisionesDistEnc Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioRemision = @FolioRemision 
			and IdDistribuidor = @IdDistribuidor

			Update RemisionesDistDet Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioRemision = @FolioRemision 
			
 
		   Set @sMensaje = 'La información del Folio ' + @FolioRemision + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioRemision as Clave, @sMensaje as Mensaje 
End
Go--#SQL 	
