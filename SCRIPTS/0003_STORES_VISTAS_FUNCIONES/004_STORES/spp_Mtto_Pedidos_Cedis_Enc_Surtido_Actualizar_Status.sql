

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_Enc_Surtido_Actualizar_Status' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_Enc_Surtido_Actualizar_Status
Go--#SQL 

--	Select * From Pedidos_Cedis_Enc_Surtido_Atenciones (Nolock) 

--	 Exec spp_Mtto_Pedidos_Cedis_Enc_Surtido_Atenciones '001', '21', '1182', '00000001', '0001' 

Create Proc spp_Mtto_Pedidos_Cedis_Enc_Surtido_Actualizar_Status
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182',     
	@FolioSurtido varchar(8) = '00000001', @FolioTransferenciaReferencia Varchar(25) = ''
)  
With Encryption 
As 
Begin 
Set NoCount On  	
Declare 
		@FolioTransferenciaReferencia_Anterior varchar(25),
		@FolioTransferenciaReferencia_Nuevo varchar(25),
		@sStatus_Anterior Varchar(1)

	
	Set @FolioTransferenciaReferencia_Anterior = 'x'
	Set @FolioTransferenciaReferencia_Nuevo = 'x'
	Set @sStatus_Anterior = 'x'
	   
	Select @FolioTransferenciaReferencia_Anterior = FolioTransferenciaReferencia, @sStatus_Anterior = Status
	--Select FolioTransferenciaReferencia, Status
	From Pedidos_Cedis_Enc_Surtido (Nolock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido



	if (@FolioTransferenciaReferencia_Anterior = '' And @sStatus_Anterior = 'D')
	Begin
		Update P Set FolioTransferenciaReferencia = @FolioTransferenciaReferencia, Status = 'E'
		From Pedidos_Cedis_Enc_Surtido P
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido
	End
	 	

	Select @FolioTransferenciaReferencia_Nuevo = FolioTransferenciaReferencia
	From Pedidos_Cedis_Enc_Surtido (Nolock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido



    Select @FolioTransferenciaReferencia_Anterior As FolioTransferenciaReferencia_Anterior, @FolioTransferenciaReferencia_Nuevo As FolioTransferenciaReferencia_Nuevo, @sStatus_Anterior As Status_Anterior
    
End 
Go--#SQL   

