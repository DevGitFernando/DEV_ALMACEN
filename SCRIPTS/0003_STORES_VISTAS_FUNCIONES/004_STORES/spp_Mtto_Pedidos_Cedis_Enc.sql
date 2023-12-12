If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_Enc' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_Enc 
Go--#SQL   

Create Proc spp_Mtto_Pedidos_Cedis_Enc 
( 
    @IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0008', 	
	@IdEstadoSolicita varchar(2) = '', @IdFarmaciaSolicita varchar(4) = '0008', 
    @FolioPedido varchar(6) = '*' output, @IdPersonal varchar(4) = '0001', 
    @Observaciones varchar(200) = 'S.O', @Status varchar(2) = 'A', @EsTransferencia bit = 1,
    @Cliente Varchar(4) = '0000', @SubCliente Varchar(4) = '0000', @Programa Varchar(4) = '0000', @SubPrograma Varchar(4) = '0000', 
    @PedidoNoAdministrado smallint = 1, @TipoDeClavesDePedido int = 0, @ReferenciaPedido varchar(200) = '',
	@FechaEntrega datetime = GetDate, @IdBeneficiario Varchar(8) = '', @CajasCompletas Bit = 0 	 
)  
With Encryption 
As 
Begin 
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0  --- Solo se marca para replicacion cuando se termina el Proceso     

	If @FolioPedido = '*' 
	   Begin 
	       Select @FolioPedido = cast( (max(FolioPedido) + 1) as varchar) From Pedidos_Cedis_Enc (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	   End 
	   
	Set @FolioPedido = IsNull(@FolioPedido, '1') 
	Set @FolioPedido = right(replicate('0', 6) + @FolioPedido, 6)     
    

	If @IdEstadoSolicita = '' 
	Begin 
		Set @IdEstadoSolicita = @IdEstado 
	End 

	--------------- Validacion extra para pedidos de farmacia 
	If @FechaEntrega is null 
	Begin 
		Set @FechaEntrega = getdate() 
	End 


    
    If Not Exists 
        ( Select FolioPedido From Pedidos_Cedis_Enc (NoLock) 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioPedido = @FolioPedido
		)
	Begin 	
        Insert Into Pedidos_Cedis_Enc
			( IdEmpresa, IdEstado, IdFarmacia, IdEstadoSolicita, IdFarmaciaSolicita, FolioPedido, FechaRegistro, IdPersonal, Observaciones,
				Status, Actualizado, EsTransferencia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
				PedidoNoAdministrado, TipoDeClavesDePedido, ReferenciaInterna, FechaEntrega, IdBeneficiario, CajasCompletas )
        Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdEstadoSolicita, @IdFarmaciaSolicita, @FolioPedido, getdate() as FechaRegistro, @IdPersonal, @Observaciones,
				@sStatus as Status, @iActualizado as Actualizado, @EsTransferencia, @Cliente, @SubCliente, @Programa, @SubPrograma, 
				@PedidoNoAdministrado, @TipoDeClavesDePedido, @ReferenciaPedido, @FechaEntrega, @IdBeneficiario, @CajasCompletas
    End 
    
    Set @sMensaje = 'Información guardada satisfactoriamente con el Folio  ' + @FolioPedido 
    Select @FolioPedido as Folio, @sMensaje as Mensaje 
   
    
End 
Go--#SQL   

