
------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_Proceso_Pedidos_Cedis__CargaMasiva_001_Integrar' and xType = 'P' ) 
   Drop Proc sp_Proceso_Pedidos_Cedis__CargaMasiva_001_Integrar 
Go--#SQL 

Create Proc sp_Proceso_Pedidos_Cedis__CargaMasiva_001_Integrar  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '0004', @IdPersonal varchar(6) = '0001',
	@Tipo int = 1, @GUID Varchar(200) = 'a7dfe8cf-d80c-4675-bdd2-b0f9f68d23fd' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
 Set Ansi_Warnings Off  --- Especial, peligroso 
Declare 
	@sPoliza_Salida varchar(8), 
	@sPoliza_Entrada varchar(8), 	
	@Observaciones varchar(500), 
	@IdCliente Varchar(4),
	@IdSubCliente Varchar(4),
	@IdPrograma Varchar(4),
	@IdSubPrograma Varchar(4),
	@IdBeneficiario Varchar(8),
	@IdEstadoSolicita Varchar(8),
	@IdFarmaciaSolicita varchar(4) = '0008', 
    @FolioPedido varchar(6) = '*',
	--@IdPersonal varchar(4) = '0001', 
 --   @Observaciones varchar(200) = 'S.O',
	@Status varchar(2) = 'A',
	@EsTransferencia bit = 1,
	@TipoPedido int = 1,
    @Cliente Varchar(4) = '0000',
	@SubCliente Varchar(4) = '0000',
	@Programa Varchar(4) = '0000',
	@SubPrograma Varchar(4) = '0000', 
    @PedidoNoAdministrado smallint = 1,
	@TipoDeClavesDePedido int = 3,
	@ReferenciaPedido varchar(200) = '',
	@FechaEntrega datetime = GetDate(),
	@IdClaveSSA varchar(4), 
    @Existencia int = 0, 
    @CantidadSolicitada int = 0,
	@CantidadEnCajas int = 0, 
	@ClaveSSA varchar(50) = '',
	@ContenidoPaquete int = 0



	Select Top 0
		IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma,
		IdBeneficiario, Observaciones, FechaEntrega, IdEstado as IdEstadoSolicita, IdFarmacia As IdFarmaciaSolicita, 0 As EsTransferencia, TipoPedido, ReferenciaInterna
	Into #Folio
	From Pedidos_Cedis__CargaMasiva



	If ( @Tipo = 1 )
		Begin
			Insert Into #Folio 
			( 
				IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma,
				IdBeneficiario, Observaciones, FechaEntrega, IdEstadoSolicita, IdFarmaciaSolicita, EsTransferencia, TipoPedido, ReferenciaInterna 
			) 
			Select Distinct
				IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma,
				IdBeneficiario, Observaciones, FechaEntrega, IdEstado as IdEstadoSolicita, IdFarmacia As IdFarmaciaSolicita, 0 As EsTransferencia, TipoPedido, ReferenciaInterna
			From Pedidos_Cedis__CargaMasiva
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID

		End
	Else
		Begin  

			Insert Into #Folio
			( 
				IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma,
				IdBeneficiario, Observaciones, FechaEntrega, IdEstadoSolicita, IdFarmaciaSolicita, EsTransferencia, TipoPedido, ReferenciaInterna 
			)  
			Select Distinct
				IdEmpresa, IdEstado, IdFarmacia, '0000' As IdCliente, '0000' As IdSubCliente, '0000' As IdPrograma, '0000' As IdSubPrograma,
				'' As IdBeneficiario, Observaciones, FechaEntrega, IdEstadoSolicita, IdFarmaciaSolicita, 1 As EsTransferencia, TipoPedido, ReferenciaInterna
			From Pedidos_Cedis__CargaMasiva
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID 

		End


		Declare #Enc Cursor For
			Select 
				IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
				Observaciones, FechaEntrega, IdBeneficiario, IdEstadoSolicita, IdFarmaciaSolicita, EsTransferencia, TipoPedido, ReferenciaInterna as ReferenciaPedido
			From #Folio
		Open #Enc Fetch #Enc 
			Into 
				@Cliente, @SubCliente, @Programa, @SubPrograma, 
				@Observaciones, @FechaEntrega, @IdBeneficiario, @IdEstadoSolicita, @IdFarmaciaSolicita, @EsTransferencia, @TipoPedido, @ReferenciaPedido
		While (@@Fetch_Status = 0 )  
			Begin

			Set @FolioPedido = '*'

				Exec spp_Mtto_Pedidos_Cedis_Enc
					@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, 
					@IdEstadoSolicita = @IdEstadoSolicita, 
					@IdFarmaciaSolicita = @IdFarmaciaSolicita, @FolioPedido = @FolioPedido output,
					@IdPersonal = @IdPersonal, @Observaciones = @Observaciones, @Status = @Status, @EsTransferencia = @EsTransferencia,
					@Cliente = @Cliente, @SubCliente = @SubCliente, @Programa = @Programa, @SubPrograma = @SubPrograma, @PedidoNoAdministrado = @PedidoNoAdministrado,
					@TipoDeClavesDePedido = @TipoPedido, @ReferenciaPedido = @ReferenciaPedido, @FechaEntrega = @FechaEntrega, @IdBeneficiario = @IdBeneficiario

					--Select
					--		@IdEmpresa IdEmpresa, @IdEstado IdEstado, @IdFarmacia IdFarmacia, @IdFarmaciaSolicita IdFarmaciaSolicita, @FolioPedido FolioPedido,
					--		@IdPersonal IdPersonal, @Observaciones Observaciones, @Status Status, @EsTransferencia EsTransferencia,
					--		@Cliente Cliente, @SubCliente SubCliente, @Programa Programa, @SubPrograma SubPrograma, @PedidoNoAdministrado PedidoNoAdministrado,
					--		@TipoDeClavesDePedido TipoDeClavesDePedido, @ReferenciaPedido ReferenciaPedido, @FechaEntrega FechaEntrega, @IdBeneficiario IdBeneficiario

					Declare #Det Cursor For
						Select IdClaveSSA, Existencia, Cantidad, CantidadEnCajas, ClaveSSA, ContenidoPaquete
						From Pedidos_Cedis__CargaMasiva
						Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And GUID = @GUID And
							  IdFarmaciaSolicita = @IdFarmaciaSolicita And IdBeneficiario = @IdBeneficiario And Observaciones = @Observaciones And
							  IdCliente = @Cliente And IdSubCliente = @SubCliente And IdPrograma = @Programa And IdSubPrograma = @SubPrograma And TipoPedido = @TipoPedido
					Open #Det Fetch #Det Into @IdClaveSSA, @Existencia, @CantidadSolicitada, @CantidadEnCajas, @ClaveSSA, @ContenidoPaquete
					While (@@Fetch_Status = 0 )  
					Begin
						Exec spp_Mtto_Pedidos_Cedis_Det
							@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @FolioPedido = @FolioPedido,
							@IdClaveSSA = @IdClaveSSA, @Existencia = @Existencia, @CantidadSolicitada = @CantidadSolicitada, @CantidadEnCajas = @CantidadEnCajas, @ClaveSSA = @ClaveSSA,
							@ContenidoPaquete = @ContenidoPaquete, @ExistenciaSugerida = 0

						--Select
						--	@IdEmpresa IdEmpresa, @IdEstado IdEstado, @IdFarmacia IdFarmacia, @FolioPedido FolioPedido,
						--	@IdClaveSSA IdClaveSSA, @Existencia Existencia, @CantidadSolicitada CantidadSolicitada, @CantidadEnCajas CantidadEnCajas, @ClaveSSA ClaveSSA,
						--	@ContenidoPaquete ContenidoPaquete, 0  ExistenciaSugerida

						Fetch #Det Into @IdClaveSSA, @Existencia, @CantidadSolicitada, @CantidadEnCajas, @ClaveSSA, @ContenidoPaquete
					End		
					Close #Det 
					DeAllocate #Det 


				Fetch #Enc Into 
					@Cliente, @SubCliente, @Programa, @SubPrograma, 
					@Observaciones, @FechaEntrega, @IdBeneficiario, @IdEstadoSolicita, @IdFarmaciaSolicita, @EsTransferencia, @TipoPedido, @ReferenciaPedido
			End		
		Close #Enc 
		DeAllocate #Enc 

 

End 
Go--#SQL  

