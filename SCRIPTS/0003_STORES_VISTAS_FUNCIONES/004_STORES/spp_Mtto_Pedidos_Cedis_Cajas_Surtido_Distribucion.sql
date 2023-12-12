

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_Cajas_Surtido_Distribucion' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_Cajas_Surtido_Distribucion 
Go--#SQL   

Create Proc spp_Mtto_Pedidos_Cedis_Cajas_Surtido_Distribucion 
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '1182', 
	@FolioPedido varchar(8) = '000001', @FolioSurtido varchar(8) = '00000001', @IdCaja varchar(8) = '0',
	@iOpcion tinyint = 0
)  
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@sMensaje varchar(1000), @sStatus varchar(1),	 
	@iActualizado smallint
	
	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 3       
    
    Set @IdCaja = Right(Replicate('0', 8) + @IdCaja, 8)
    
	If @iOpcion = 0
		Begin
			If Not Exists 
				(	Select * From Pedidos_Cedis_Cajas_Surtido_Distribucion (NoLock) 
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				    and FolioPedido = @FolioPedido and FolioSurtido = @FolioSurtido and IdCaja = @IdCaja					
				)
			Begin 	
				Insert Into Pedidos_Cedis_Cajas_Surtido_Distribucion ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, FolioSurtido, 
																		IdCaja, Status, Actualizado )
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, @FolioSurtido, @IdCaja, @sStatus, @iActualizado

				Set @sMensaje = 'La caja Núm. : ' + @IdCaja + ', se asigno correctamente'			
			End 
			
			Update CatCajasDistribucion Set Disponible = 0
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCaja = @IdCaja
		End
	Else  --- Cancelacion de la caja  -- se elimina el renglon de la tabla.
		Begin
			Delete From Pedidos_Cedis_Cajas_Surtido_Distribucion
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and FolioPedido = @FolioPedido and FolioSurtido = @FolioSurtido and IdCaja = @IdCaja

			Set @sMensaje = 'La caja Núm. : ' + @IdCaja + ', se libero correctamente'

			Update CatCajasDistribucion Set Disponible = 1
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCaja = @IdCaja
		End   
    
	Select @sMensaje as Mensaje
End 
Go--#SQL   