If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_Det_Surtido' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_Det_Surtido 
Go--#SQL   

Create Proc spp_Mtto_Pedidos_Cedis_Det_Surtido 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0008', 
    @FolioSurtido varchar(8) = '*', @IdClaveSSA varchar(4), @ClaveSSA varchar(30), 
    @CantidadSolicitada int = 0, @sStatus varchar(1) = 'A' 
)  
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@sMensaje varchar(1000), 
	-- @ClaveSSA varchar(30), 
	@iActualizado smallint, 
	@iExistencia int    



	/*Opciones 
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	-- Set @sStatus = 'A'
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso         
    Set @iExistencia = 0 
    --Select @ClaveSSA = ClaveSSA From vw_ClavesSSA_Sales Where IdClaveSSA_Sal = @IdClaveSSA 
    
    
    If Not Exists 
        ( Select FolioSurtido From Pedidos_Cedis_Det_Surtido (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		   and FolioSurtido = @FolioSurtido and IdClaveSSA = @IdClaveSSA 
		)
	Begin 	
		Insert Into Pedidos_Cedis_Det_Surtido ( IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdClaveSSA, ClaveSSA, CantidadAsignada, Status, Actualizado ) 
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioSurtido, @IdClaveSSA, @ClaveSSA, @CantidadSolicitada, @sStatus, @iActualizado 
    End    
    
End 
Go--#SQL   
