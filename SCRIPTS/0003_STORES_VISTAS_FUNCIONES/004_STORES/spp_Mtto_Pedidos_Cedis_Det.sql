If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_Det' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_Det 
Go--#SQL   

Create Proc spp_Mtto_Pedidos_Cedis_Det 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0008', 
    @FolioPedido varchar(6) = '*', @IdClaveSSA varchar(4), 
    @Existencia int = 0, 
    @CantidadSolicitada int = 0, @CantidadEnCajas int = 0, 
	@ClaveSSA varchar(50) = '', @ContenidoPaquete int = 0, @ExistenciaSugerida int = 0 
)  
With Encryption 
As 
Begin 
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint, 
		@iExistencia_Aux int    

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar. 
	*/

	Set @sMensaje = '' 
	Set @sStatus = 'A' 
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso         
    Set @iExistencia_Aux = 0 
    
    Set @CantidadEnCajas = Ceiling( ((@CantidadSolicitada * 1.0) / (@ContenidoPaquete * 1.0)) )  
    
    If Not Exists 
        ( Select FolioPedido From Pedidos_Cedis_Det (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		   and FolioPedido = @FolioPedido and IdClaveSSA = @IdClaveSSA 
		)
	Begin 	
	    ------Select @iExistencia = Existencia From vw_ExistenciaPorSales 
	    ------Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ClaveSSA = @ClaveSSA 
	    
        Insert Into Pedidos_Cedis_Det ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, IdClaveSSA, Cantidad, Existencia, CantidadEnCajas, ClaveSSA, ContenidoPaquete, ExistenciaSugerida, Status, Actualizado ) 
        Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @IdClaveSSA, @CantidadSolicitada, @Existencia, @CantidadEnCajas, @ClaveSSA, @ContenidoPaquete, @ExistenciaSugerida, @sStatus, @iActualizado
    End 
    
End 
Go--#SQL   
