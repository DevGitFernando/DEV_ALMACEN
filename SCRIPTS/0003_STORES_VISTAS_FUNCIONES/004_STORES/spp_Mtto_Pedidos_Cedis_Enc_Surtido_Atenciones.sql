

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones
Go--#SQL 

--	Select * From Pedidos_Cedis_Enc_Surtido_Atenciones (Nolock) 

--	 Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones '001', '21', '1182', '00000001', '0001' 

Create Proc spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182',     
	@FolioSurtido varchar(8) = '00000001', @IdPersonal varchar(4) = '0001', @Observaciones Varchar(250) = ''
)  
With Encryption 
As 
Begin 
Set NoCount On  	
Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint 
	

	Set @sMensaje = ''
	Set @sStatus = 'A' 
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso  	
	   
	Set @sStatus = ( Select Status From Pedidos_Cedis_Enc_Surtido (Nolock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado 
					and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido )
	 	
    Insert Into Pedidos_Cedis_Enc_Surtido_Atenciones ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdPersonal, Status, Actualizado, Observaciones ) 
    Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioSurtido, @IdPersonal, @sStatus as Status, @iActualizado as Actualizado, @Observaciones
        
    Set @sMensaje = 'Información guardada satisfactoriamente con el Folio  ' + @FolioSurtido 
    Select @FolioSurtido as Folio, @sMensaje as Mensaje 
    
End 
Go--#SQL   

