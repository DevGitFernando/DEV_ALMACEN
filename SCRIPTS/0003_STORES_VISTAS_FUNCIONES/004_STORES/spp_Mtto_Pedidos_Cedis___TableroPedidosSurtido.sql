-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis___TableroPedidosSurtido' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis___TableroPedidosSurtido
Go--#SQL   

Create Proc spp_Mtto_Pedidos_Cedis___TableroPedidosSurtido
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', 
	@IdJurisdiccion varchar(3) = '*', 
	
	@IdFarmacia varchar(4) = '*', 
	
	@Filtro_Fechas int = 0,  
	@FechaInicial varchar(10) = '2013-01-01', @FechaFinal varchar(10) = '2013-03-31', 

	@Filtro_Fechas_Entrega int = 0,  
	@FechaInicial_Entrega varchar(10) = '2013-01-01', @FechaFinal_Entrega varchar(10) = '2013-03-31', 

	@StatusDePedido Varchar(2) = 0, @IdRuta Varchar(4) = '*'     
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@sSql varchar(max), 
	@sFiltroFechas varchar(500), 
	@sFiltroFechas_Entrega varchar(500)


	Set @sFiltroFechas_Entrega = '' 
	Set @sFiltroFechas = '' 

	If @Filtro_Fechas = 1 
	Begin  
		Set @sFiltroFechas = ' and ( convert(varchar(10), P.FechaRegistro, 120)  Between ' + char(39) +  @FechaInicial + char(39) + ' and ' + char(39) +  convert(varchar(10), @FechaFinal, 120) + char(39) + ' ) ' + char(10) 
	End 
	
	If @Filtro_Fechas_Entrega = 1 
	Begin  
		Set @sFiltroFechas_Entrega = ' and ( convert(varchar(10), P.FechaEntrega, 120)  Between ' + char(39) +  @FechaInicial_Entrega + char(39) + ' and ' + char(39) +  convert(varchar(10), @FechaFinal_Entrega, 120) + char(39) + ' ) ' + char(10) 
	End 
	

--- Jurisdicciones 
	Select IdEstado, IdJurisdiccion, Descripcion as Jurisdiccion 
	Into #tmpJuris 
	From CatJurisdicciones J (NoLock) 
	Where IdEstado = @IdEstado 


	
--- Farmacias 
	Select J.IdEstado, J.IdJurisdiccion, J.Jurisdiccion, F.IdFarmacia, F.Farmacia  
	Into #tmpFarmacias  
	From #tmpJuris J (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( J.IdEstado = F.IdEstado and J.IdJurisdiccion = F.IdJurisdiccion  )

	Select C.Descripcion As Ruta, D.*
	Into #CFGC_ALMN__RutaDistribucion_Beneficiario
	From CFGC_ALMN__RutaDistribucion C (NoLock)
	Inner Join CFGC_ALMN__RutaDistribucion_Beneficiario D (NoLock) On (C.IdEstado = D.IdEstado And C.IdFarmacia = D.IdFarmacia And C.IdRuta = D.IdRuta)
	Where C.Status = 'A' And D.Status = 'A'

	Select C.Descripcion As Ruta, D.*
	Into #CFGC_ALMN__RutaDistribucion_Transferencia
	From CFGC_ALMN__RutaDistribucion C (NoLock)
	Inner Join CFGC_ALMN__RutaDistribucion_Transferencia D (NoLock) On (C.IdEstado = D.IdEstado And C.IdFarmacia = D.IdFarmacia And C.IdRuta = D.IdRuta)
	Where C.Status = 'A' And D.Status = 'A'


	----		Crear Tabla Temporal 
	Select E.IdEstado, E.IdFarmacia, P.EsTransferencia,
		SPACE(100) As TipoPedido, SPACE(3) As IdJurisdiccionSolicita, SPACE(250) As JurisdiccionSolicita,
		P.IdEstadoSolicita, IdFarmaciaSolicita, Space(250) As FarmaciaSolicita, P.FolioPedido,
		Convert(varchar(10), P.FechaRegistro, 120) As FechaPedido, Convert(varchar(10), FechaEntrega, 120) As FechaEntrega,
		E.FolioSurtido, FolioTransferenciaReferencia As Referencia, P.Status As StatusPedido, E.Status, '0000' As IdRuta, 'Sin Asignar' As Ruta, IdCliente, IdSubCliente, IdBeneficiario
	Into #tmpPedidos
    From Pedidos_Cedis_Enc P (NoLock)
	Inner Join Pedidos_Cedis_Enc_Surtido E (NoLock) On (P.IdEmpresa = E.IdEmpresa And P.IdEstado = E.IdEstado And P.IdFarmacia = E.IdFarmacia And P.FolioPedido = E.FolioPedido)
	Inner Join Pedidos_Cedis_Det_Surtido S (NoLock)  On (E.IdEmpresa = S.IdEmpresa And E.IdEstado = S.IdEstado And E.IdFarmacia = S.IdFarmacia And E.FolioSurtido = S.FolioSurtido)
    Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado
		And 1 = 0

	--Select * From Pedidos_Cedis_Enc
	
	Set @sSql = 
		'Insert Into #tmpPedidos ' + CHAR(13) + 
		
		'Select  E.IdEstado, E.IdFarmacia, P.EsTransferencia, ' + CHAR(13) + 
		Char(39) + 'TRANSFERENCIA' + Char(39) + ' As TipoPedido, SPACE(3) As IdJurisdiccionSolicita, SPACE(250) As JurisdiccionSolicita,  ' + CHAR(13) + 
        '	IdEstadoSolicita, IdFarmaciaSolicita, SPACE(250) As FarmaciaSolicita, P.FolioPedido,  ' + CHAR(13) + 
		'	Convert(varchar(10), P.FechaRegistro, 120) As FechaPedido, Convert(varchar(10), FechaEntrega, 120) As FechaEntrega,  ' + CHAR(13) + 
		'	E.FolioSurtido, FolioTransferenciaReferencia As Referencia, P.Status As StatusPedido, E.Status, ' +Char(39) + '0000' + Char(39) + ' As IdRuta, ' + Char(39) + 'Sin Asignar' + Char(39) + ' As Ruta, ' + CHAR(13) + CHAR(13) +
		' IdCliente, IdSubCliente, IdBeneficiario ' + Char(13) +
    'From Pedidos_Cedis_Enc P (NoLock)  ' + CHAR(13) + 
	'Inner Join Pedidos_Cedis_Enc_Surtido E (NoLock) On (P.IdEmpresa = E.IdEmpresa And P.IdEstado = E.IdEstado And P.IdFarmacia = E.IdFarmacia And P.FolioPedido = E.FolioPedido)  ' + CHAR(13) + 
	--'Inner Join Pedidos_Cedis_Det_Surtido S (NoLock)  On (E.IdEmpresa = S.IdEmpresa And E.IdEstado = S.IdEstado And E.IdFarmacia = S.IdFarmacia And E.FolioSurtido = S.FolioSurtido)  ' + CHAR(13) + 
    'Where E.IdEmpresa = ' + Char(39) + @IdEmpresa + Char(39) + ' and E.IdEstado = ' + Char(39) + @IdEstado + Char(39) + CHAR(13) + 
	@sFiltroFechas +  CHAR(13) + @sFiltroFechas_Entrega + char(13)
	Exec( @sSql ) 
	print @sSql 

	--select * from #tmpPedidos

	Update P Set P.IdRuta = B.IdRuta, Ruta = B.Ruta
	From #tmpPedidos P
	Inner Join #CFGC_ALMN__RutaDistribucion_Beneficiario B On (P.IdEstado = B.IdEstado And P.IdFarmacia = B.IdFarmacia and P.IdCliente = B.IdCliente and P.IdSubCliente = B.IdSubCliente  and P.IdBeneficiario = B.IdBeneficiario)
	where P.EsTransferencia = 0

	------------------------------- ASIGNAR INFORMACIÓN DE LA FARMACIA QUE SOLICITA EL PEDIDO  
	Update P Set
		IdJurisdiccionSolicita = B.IdJurisdiccion, JurisdiccionSolicita = B.Jurisdiccion,
		FarmaciaSolicita = B.Farmacia
	From #tmpPedidos P (NoLock) 
	Inner Join vw_Farmacias B (NoLock) 
		On ( P.IdEstadoSolicita = B.IdEstado And P.IdFarmaciaSolicita = B.IdFarmacia )

	Update P Set P.IdRuta = B.IdRuta, Ruta = B.Ruta
	From #tmpPedidos P
	Inner Join #CFGC_ALMN__RutaDistribucion_Transferencia B On (P.IdEstadoSolicita = B.IdEstadoEnvia And P.IdFarmaciaSolicita = B.IdFarmaciaEnvia)
	where P.EsTransferencia = 1

	if (@IdRuta <> '*')
	Begin
		Delete From #tmpPedidos   
		Where IdRuta <> @IdRuta
	End



	If @IdJurisdiccion <> '*' 
	Begin 
		Delete From #tmpPedidos   
		Where IdEstado = @IdEstado and IdJurisdiccionSolicita <> @IdJurisdiccion --And EsTransferencia = 1 		
	End 



	If @IdFarmacia <> '*' 
	Begin 
		Delete From #tmpPedidos Where IdEstado = @IdEstado and IdFarmaciaSolicita <> @IdFarmacia --And EsTransferencia = 1
	End 

	Update P Set TipoPedido = 'VENTA' From #tmpPedidos P Where EsTransferencia = 0


	if (@StatusDePedido <> '0')
	Begin
		Delete From #tmpPedidos Where Status <> @StatusDePedido
	End


	Select TipoPedido As 'Tipo de pedido', IdJurisdiccionsolicita As 'Núm. Jurisdicción', Jurisdiccionsolicita As 'Jurisdicción',
           IdFarmaciaSolicita As 'Núm. Farmacia Pedido', FarmaciaSolicita As 'Farmacia Pedido', FolioPedido As 'Folio Pedido',
           FechaPedido As 'Fecha Pedido', FechaEntrega As 'Fecha Entrega',
           FolioSurtido as 'Folio Surtido', Referencia, IdRuta As 'Clave Ruta', Ruta, StatusPedido As 'Status Pedido', Status 
	From #tmpPedidos


	
End 
Go--#SQL 



