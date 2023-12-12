------------------------------------------------------------------------------------------------------------------------------------------------   
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_Pedidos' And xType = 'P' ) 
	Drop Proc spp_Mtto_COM_OCEN_Pedidos
Go--#SQL

Create Procedure spp_Mtto_COM_OCEN_Pedidos
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @IdTipoPedido varchar(2) = '', 
	@FolioPedido varchar(6) = '', @IdPersonal varchar(4) = '', @FechaSistema varchar(10) = '', @Observaciones varchar(200) = '',
	@IdEstadoRegistra varchar(2) = '', @IdFarmaciaRegistra varchar(4) = '', @TipoDeClavesDePedido int = 0 
)
As
Begin
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint, 
		@sStatusPedido varchar(2)

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @sStatusPedido = 'S'	
 

	-- Se Inserta en la tabla COM_OCEN_Pedidos
--		Select * From COM_OCEN_Pedidos (NoLock)
	If Not Exists ( Select FolioPedido From COM_OCEN_Pedidos (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		And IdTipoPedido = @IdTipoPedido And FolioPedido = @FolioPedido )
	 Begin
		Insert Into COM_OCEN_Pedidos( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdPersonal, FechaSistema, FechaRegistro, 
					Observaciones, Status, Actualizado, IdEstadoRegistra, IdFarmaciaRegistra, StatusPedido, TipoDeClavesDePedido )
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdTipoPedido, @FolioPedido, @IdPersonal, @FechaSistema, GetDate(), 
		@Observaciones, @sStatus, @iActualizado, @IdEstadoRegistra, @IdFarmaciaRegistra, @sStatusPedido, @TipoDeClavesDePedido
		
		Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido 

	 End	

	-- Regresar el FolioPedido Generado
    Select @FolioPedido as FolioPedido, @sMensaje as Mensaje
End
Go--#SQL

------------------------------------------------------------------------------------------------------------------------------------------------   
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_REG_Pedidos' And xType = 'P' ) 
	Drop Proc spp_Mtto_COM_OCEN_REG_Pedidos
Go--#SQL

Create Procedure spp_Mtto_COM_OCEN_REG_Pedidos
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @IdTipoPedido varchar(2) = '', 
	@FolioPedido varchar(6) = '', @IdPersonal varchar(4) = '', @FechaSistema varchar(10) = '', 
	@Observaciones varchar(200) = '', @FolioPedidoUnidad varchar(6) = '',
	@IdEstadoRegistra varchar(2) = '', @IdFarmaciaRegistra varchar(4) = '', @TipoDeClavesDePedido int = 2 
)
As 
Begin
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint, @StatusPedido varchar(2) 

	Set @sMensaje = ''
	Set @sStatus = 'A' 
	Set @iActualizado = 0
	Set @StatusPedido = 'S' 

	-- Se Inserta en la tabla COM_OCEN_REG_Pedidos
	If Not Exists ( Select FolioPedido From COM_OCEN_REG_Pedidos (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		And IdTipoPedido = @IdTipoPedido And FolioPedido = @FolioPedido )
	 Begin
		Insert Into COM_OCEN_REG_Pedidos
			( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, IdPersonal, FechaSistema, FechaRegistro, 
			  Observaciones, Status, Actualizado, StatusPedido, FolioPedidoUnidad, IdEstadoRegistra, IdFarmaciaRegistra, TipoDeClavesDePedido )
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdTipoPedido, @FolioPedido, @IdPersonal, @FechaSistema, GetDate(), 
		@Observaciones, @sStatus, @iActualizado, @StatusPedido, @FolioPedidoUnidad, @IdEstadoRegistra, @IdFarmaciaRegistra, @TipoDeClavesDePedido 
		
		Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido 

	 End	

	-- Regresar el FolioPedido Generado
    Select @FolioPedido as FolioPedido, @sMensaje as Mensaje 
    
    
End
Go--#SQL
