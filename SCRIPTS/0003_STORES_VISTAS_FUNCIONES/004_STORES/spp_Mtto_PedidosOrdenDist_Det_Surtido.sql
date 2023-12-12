
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_PedidosOrdenDist_Det_Surtido' and xType = 'P' ) 
   Drop Proc spp_Mtto_PedidosOrdenDist_Det_Surtido 
Go--#SQL   

Create Proc spp_Mtto_PedidosOrdenDist_Det_Surtido 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0008', 
    @FolioPedido varchar(6) = '*', @IdClaveSSA varchar(4), 
    @CantidadSolicitada int = 0, @CantidadSugerida int = 0 
)  
With Encryption 
As 
Begin 
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint, 
		@iExistencia int    

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso         
    Set @iExistencia = 0 
    
    If Not Exists 
        ( Select FolioPedido From PedidosOrdenDist_Det_Surtido (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		   and FolioPedido = @FolioPedido and IdClaveSSA = @IdClaveSSA 
		)
	Begin 
        Insert Into PedidosOrdenDist_Det_Surtido ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdClaveSSA, CantidadSolicitada, CantidadSugerida, Status, Actualizado ) 
        Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @IdClaveSSA, @CantidadSolicitada, @CantidadSugerida, @sStatus as Status, @iActualizado as Actualizado 
    End 
    
End 
Go--#SQL   

