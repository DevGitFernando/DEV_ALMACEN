If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_MovtoInv_Enc' and xType = 'P' ) 
   Drop Proc spp_Mtto_MovtoInv_Enc 
Go--#SQL

Create Proc spp_Mtto_MovtoInv_Enc (  @IdEmpresa varchar(5), @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001',
	@FolioMovtoInv varchar(14) = '*', @IdTipoMovto_Inv varchar(6) = 'II', @TipoES varchar(1) = 'E', 
	@Referencia varchar(30) = '', -- @MovtoAplicado varchar(1), 
	@IdPersonal varchar(4) = '0001', @Observaciones varchar(500) = '', 
	@SubTotal numeric(14,4) = 0, @Iva numeric(14,4) = 0, @Total numeric(14,4) = 0, @iOpcion smallint = 1  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	If @FolioMovtoInv = '*' 
	   Begin 
	       Select @FolioMovtoInv = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 From MovtosInv_Enc (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdTipoMovto_Inv 
		   
		   -- Actualizar el registro de folios 
		   Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = cast(IsNull(@FolioMovtoInv, 1) as int), Actualizado = 0 
		   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdTipoMovto_Inv 
	   End 
	   
	Set @FolioMovtoInv = IsNull(@FolioMovtoInv, '1') 
	Set @FolioMovtoInv = @IdTipoMovto_Inv + right(replicate('0', 8) + @FolioMovtoInv, 8) 


	--- Iniciar el proceso de guardado 
	If @iOpcion = 1 
	   Begin 
	       If Not Exists ( Select * From MovtosInv_Enc (NoLock) 
	                           Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioMovtoInv = @FolioMovtoInv and IdTipoMovto_Inv = @IdTipoMovto_Inv ) 
	          Begin 
	             Insert Into MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, 
					Referencia, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total ) 
	             Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovtoInv, @IdTipoMovto_Inv, @TipoES, 
					@Referencia, @IdPersonal, @Observaciones, @SubTotal, @Iva, @Total 
	          End 
	       Else 
	          Begin 	          
	             Update MovtosInv_Enc Set IdPersonalRegistra = @IdPersonal, Observaciones = @Observaciones, 
					SubTotal = @SubTotal, Iva = @Iva, Total = @Total, Status = @sStatus, Actualizado = @iActualizado 
	             Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioMovtoInv = @FolioMovtoInv and IdTipoMovto_Inv = @IdTipoMovto_Inv 	          
	          End 
	       Set @sMensaje = 'La información se guardo satisfactoriamente con el folio ' + @FolioMovtoInv     
	   End 
	Else 
	   Begin 
	       Set @sStatus = 'C' 
	       Update MovtosInv_Enc Set Status = @sStatus, Actualizado = @iActualizado 
	       Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioMovtoInv = @FolioMovtoInv and IdTipoMovto_Inv = @IdTipoMovto_Inv 
		   Set @sMensaje = 'La información de movimiento de inventario ' + @FolioMovtoInv + ' ha sido cancelada satisfactoriamente.' 	       
	   End    
	

----------- Agregar el movimiento al Registro de Auditoria 
	If Not Exists ( Select * From MovtosInv_ADT (NoLock) 
					   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioMovtoInv = @FolioMovtoInv ) 
	  Begin 
		 Insert Into MovtosInv_ADT ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 
		 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovtoInv 
	  End  
----------- Agregar el movimiento al Registro de Auditoria 		
	
	-- Devolver el resultado
	Select @FolioMovtoInv as Folio, @sMensaje as Mensaje  
	
End 
Go--#SQL
