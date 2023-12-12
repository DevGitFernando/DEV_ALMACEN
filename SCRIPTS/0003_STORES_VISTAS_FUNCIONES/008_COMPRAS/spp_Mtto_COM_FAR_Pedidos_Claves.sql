If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_FAR_Pedidos_Claves' And xType = 'P' ) 
	Drop Proc spp_Mtto_COM_FAR_Pedidos_Claves
Go--#SQL 

Create Procedure spp_Mtto_COM_FAR_Pedidos_Claves
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdTipoPedido varchar(2), 
	@FolioPedido varchar(6), @IdClaveSSA varchar(4), @Cantidad_Pedido int, @CantidadPiezas int 
) 
With Encryption 
As
Begin
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint 

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	-- Se Inserta en la tabla COM_FAR_Pedidos_Productos
	If Not Exists ( Select FolioPedido From COM_FAR_Pedidos_Claves (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdTipoPedido = @IdTipoPedido 
		And FolioPedido = @FolioPedido And IdClaveSSA = @IdClaveSSA  )
	 Begin
		Insert Into COM_FAR_Pedidos_Claves( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdClaveSSA, Cantidad_Pedido, CantidadPiezas, Status, Actualizado )
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdTipoPedido, @FolioPedido, @IdClaveSSA, @Cantidad_Pedido, @CantidadPiezas, @sStatus, @iActualizado

		Set @sMensaje = 'La información del Folio ' + @FolioPedido + ' se guardo satisfactoriamente'
	 End	

	-- Regresar la Clave Generada
    Select @FolioPedido as Clave, @sMensaje as Mensaje 
    
End 
Go--#SQL 
