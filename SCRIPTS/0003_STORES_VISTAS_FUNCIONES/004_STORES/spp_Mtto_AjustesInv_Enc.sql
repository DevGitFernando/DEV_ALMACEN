If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_AjustesInv_Enc' and xType = 'P' ) 
   Drop Proc spp_Mtto_AjustesInv_Enc 
Go--#SQL

Create Proc spp_Mtto_AjustesInv_Enc (  @IdEmpresa varchar(5), @IdEstado varchar(2), @IdFarmacia varchar(4), @Poliza varchar(8) output, 
	@IdPersonal varchar(4), @Observaciones varchar(500), @SubTotal numeric(14,4), @Iva numeric(14,4), @Total numeric(14,4), 
	@PolizaAplicada varchar(1), @iOpcion smallint, @iMostrarResultado int = 1 
) 
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
	Set @iActualizado = 3  --- Solo se marca para replicacion cuando se termina el Proceso  
	
	If @Poliza = '*' 
	   Begin 
	       Select @Poliza = cast( (max(Poliza) + 1) as varchar) From AjustesInv_Enc (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
	   End 
	   
	Set @Poliza = IsNull(@Poliza, '1') 
	Set @Poliza = right(replicate('0', 8) + @Poliza, 8) 

	--- Iniciar el proceso de guardado 
	If @iOpcion = 1 
	   Begin 
	       If Not Exists ( Select * From AjustesInv_Enc (NoLock) 
	                           Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Poliza = @Poliza ) 
	          Begin 
	             Insert Into AjustesInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, Poliza, IdPersonalRegistra, Observaciones, SubTotal, Iva, Total ) 
	             Select @IdEmpresa, @IdEstado, @IdFarmacia, @Poliza, @IdPersonal, @Observaciones, @SubTotal, @Iva, @Total 
	          End 
	       Else 
	          Begin 	          
	             Update AjustesInv_Enc Set SubTotal = @SubTotal, Iva = @Iva, Total = @Total, PolizaAplicada = @PolizaAplicada, Status = @sStatus, Actualizado = @iActualizado 
	             Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Poliza = @Poliza 	          
	          End 
	       Set @sMensaje = 'La información se guardo satisfactoriamente con el folio ' + @Poliza     
	   End 
------	Else 
------	   Begin 
------	       Set @sStatus = 'C' 
------	       Update AjustesInv_Enc Set Status = @sStatus, Actualizado = @iActualizado 
------	       Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Poliza = @Poliza  
------		   Set @sMensaje = 'La información de movimiento de inventario ' + @Poliza + ' ha sido cancelada satisfactoriamente.' 	       
------	   End    
	
	if @iMostrarResultado = 1 
	Begin 
		-- Devolver el resultado
		Select @Poliza as Poliza, @sMensaje as Mensaje  
	End
	
End 
Go--#SQL
