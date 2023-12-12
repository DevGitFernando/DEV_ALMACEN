-------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_AsignacionDePedidosCompradores' And xType = 'P' ) 
	Drop Proc spp_Mtto_COM_OCEN_AsignacionDePedidosCompradores
Go--#SQL

Create Procedure spp_Mtto_COM_OCEN_AsignacionDePedidosCompradores
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@IdTipoPedido varchar(2) = '', @FolioPedido varchar(6) = '', @IdPersonal varchar(4) = '', 
	@IdEstadoRegistra varchar(2) = '', @IdFarmaciaRegistra varchar(4) = '', @IdPersonalRegistra varchar(4) = '',
	@FechaRegistro varchar(10) = '', @Observaciones varchar(200) = '', @EsCentral tinyint = 0, @TipoDeClavesDePedido smallint = 0 
) 
As 
Begin
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint, @iStatusPedido smallint  

	Set @sMensaje = ''
	Set @iStatusPedido = 1
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	

	-- Se Inserta en la tabla COM_OCEN_AsignacionDePedidosCompradores
	If Not Exists ( Select FolioPedido From COM_OCEN_AsignacionDePedidosCompradores (NoLock)
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		And IdTipoPedido = @IdTipoPedido And FolioPedido = @FolioPedido And EsCentral = @EsCentral )
	 Begin
		Insert Into COM_OCEN_AsignacionDePedidosCompradores 
		( 
			IdEmpresa, IdEstado, IdFarmacia, IdTipoPedido, FolioPedido, EsCentral, IdPersonal,
			IdEstadoRegistra, IdFarmaciaRegistra, IdPersonalRegistra,
			FechaRegistro, Observaciones, StatusPedido, Status, Actualizado, TipoDeClavesDePedido )
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @IdTipoPedido, @FolioPedido, @EsCentral, @IdPersonal, 
			@IdEstadoRegistra, @IdFarmaciaRegistra, @IdPersonalRegistra,
			@FechaRegistro, @Observaciones, @iStatusPedido, @sStatus, @iActualizado, @TipoDeClavesDePedido 
		
		Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @FolioPedido 				
	 End	

	-- Regresar el FolioPedido Generado
    Select @FolioPedido as FolioPedido, @sMensaje as Mensaje 
    
    
End
Go--#SQL
