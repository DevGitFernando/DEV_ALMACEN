If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_FAR_Pedidos' And xType = 'P' ) 
	Drop Proc spp_Mtto_COM_FAR_Pedidos
Go--#SQL

Create Procedure spp_Mtto_COM_FAR_Pedidos
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdTipoPedido varchar(2), 
	@FolioPedido varchar(6), @IdPersonal varchar(4), @FechaSistema varchar(10), @Observaciones varchar(200), 
	@TipoDePedido int, @TipoDeClavesDePedido int = 0 
) 
With Encryption 
As
Begin
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint 

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	If @FolioPedido = '*' 
	 Begin
		Select @FolioPedido = cast( (max(FolioPedido) + 1) as varchar) From COM_FAR_Pedidos (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdTipoPedido = @IdTipoPedido	 
	 End
		
	-- Asegurar que FolioPedido sea valido y formatear la cadena 
	Set @FolioPedido = IsNull(@FolioPedido, '1')
	Set @FolioPedido = right(replicate('0', 6) + @FolioPedido, 6)

	-- Se Inserta en la tabla COM_FAR_Pedidos
	If Not Exists ( Select FolioPedido From COM_FAR_Pedidos (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		And IdTipoPedido = @IdTipoPedido And FolioPedido = @FolioPedido )
	 Begin
		Insert Into COM_FAR_Pedidos ( IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, TipoDePedido, FolioPedido, IdPersonal, FechaSistema, FechaRegistro, 
		Observaciones, Status, Actualizado, TipoDeClavesDePedido )
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdTipoPedido, @TipoDePedido, @FolioPedido, @IdPersonal, @FechaSistema, GetDate(), 
		@Observaciones, @sStatus, @iActualizado, @TipoDeClavesDePedido
		
		Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido 

		-- Se Actualiza la tabla COM_FAR_Pedidos_Tipos_Farmacias
		Update COM_FAR_Pedidos_Tipos_Farmacias Set Consecutivo = @FolioPedido 
		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdTipoPedido = @IdTipoPedido		
	 End	

	-- Regresar el FolioPedido Generado
    Select @FolioPedido as FolioPedido, @sMensaje as Mensaje 
    
End
Go--#SQL

