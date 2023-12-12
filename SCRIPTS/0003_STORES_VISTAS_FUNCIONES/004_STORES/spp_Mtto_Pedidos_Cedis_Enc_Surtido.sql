-------------------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_Enc_Surtido' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_Enc_Surtido 
Go--#SQL   

Create Proc spp_Mtto_Pedidos_Cedis_Enc_Surtido 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0008',     
	@IdFarmaciaPedido varchar(4) = '0008', 
	@FolioSurtido varchar(8) = '*', @FolioPedido varchar(6) = '', 
	@MesesCaducidad int = 0, 
	@MesesCaducidad_Consigna int = 0, 
	@IdPersonal varchar(4) = '0001', 
    @Observaciones varchar(200) = 'S.O', @IdPersonalSurtido varchar(4) = '0001', @Status varchar(2) = 'A', @EsManual Bit = 0, 
    @TipoDeUbicaciones int = 0, @Prioridad int = 0, @TipoDeInventario int = 0, @IdGrupo varchar(3) = '000'
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
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso     

	If @MesesCaducidad_Consigna = 0 
	Begin 
	   Set @MesesCaducidad_Consigna = 1 
	End 


	If @FolioSurtido = '*' 
	   Begin 
	       Select @FolioSurtido = cast( (max(FolioSurtido) + 1) as varchar) 
	       From Pedidos_Cedis_Enc_Surtido (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	   End 
	   
	Set @FolioSurtido = IsNull(@FolioSurtido, '1') 
	Set @FolioSurtido = right(replicate('0', 8) + @FolioSurtido, 8)      
    

    If Not Exists 
        ( Select * From Pedidos_Cedis_Enc_Surtido (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido
		)
	Begin 	
        Insert Into Pedidos_Cedis_Enc_Surtido 
        ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdFarmaciaPedido, FolioPedido, MesesCaducidad, MesesCaducidad_Consigna, FechaRegistro, IdPersonal, Observaciones, 
			IdPersonalSurtido, IdPersonalTransporte, EsManual, TipoDeUbicaciones, Status, Actualizado, Prioridad, TipoDeInventario, IdGrupo ) 
        Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @FolioSurtido, @IdFarmaciaPedido, @FolioPedido, @MesesCaducidad, @MesesCaducidad_Consigna, getdate() as FechaRegistro, 
			@IdPersonal, @Observaciones, @IdPersonalSurtido, '0000', @EsManual, @TipoDeUbicaciones, @sStatus, @iActualizado, @Prioridad, @TipoDeInventario, @IdGrupo   
    End 
        

    Set @sMensaje = 'Información guardada satisfactoriamente con el Folio  ' + @FolioSurtido 
    Select @FolioSurtido as Folio, @sMensaje as Mensaje 
    
End 
Go--#SQL   

