-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis___ListaPedidosParaSurtido' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis___ListaPedidosParaSurtido
Go--#SQL   

Create Proc spp_Mtto_Pedidos_Cedis___ListaPedidosParaSurtido 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', 
	@IdJurisdiccion varchar(3) = '*', 
	
	@IdFarmacia varchar(4) = '*', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@IdBeneficiario varchar(max) = '', 

	@Filtro_Folios int = 0, 
	@Folio_Inicial varchar(20) = '15', @Folio_Final varchar(20) = '15', 

	@Filtro_Fechas int = 0,  
	@FechaInicial varchar(10) = '2013-01-01', @FechaFinal varchar(10) = '2013-03-31', 

	@Filtro_Fechas_Entrega int = 0,  
	@FechaInicial_Entrega varchar(10) = '2013-01-01', @FechaFinal_Entrega varchar(10) = '2013-03-31', 

	@StatusDePedido smallint = 0, @IdRuta Varchar(4) = '*'   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
	@sSql varchar(max), 
	@sFiltroBeneficiario varchar(500), 
	@sFiltroFechas varchar(500), 
	@sFiltroFechas_Entrega varchar(500), 
	@sFiltroFolios varchar(500) 


	Set @sFiltroFechas_Entrega = '' 
	Set @sFiltroFechas = '' 
	Set @sFiltroFolios = '' 
	Set @sFiltroBeneficiario = '' 

	If @Filtro_Fechas = 1 
	Begin  
		Set @sFiltroFechas = ' and ( convert(varchar(10), P.FechaRegistro, 120)  Between ' + char(39) +  @FechaInicial + char(39) + ' and ' + char(39) +  convert(varchar(10), @FechaFinal, 120) + char(39) + ' ) ' + char(10) 
	End 
	
	If @Filtro_Fechas_Entrega = 1 
	Begin  
		Set @sFiltroFechas_Entrega = ' and ( convert(varchar(10), P.FechaEntrega, 120)  Between ' + char(39) +  @FechaInicial_Entrega + char(39) + ' and ' + char(39) +  convert(varchar(10), @FechaFinal_Entrega, 120) + char(39) + ' ) ' + char(10) 
	End 


	If @IdBeneficiario <> '' 
	Begin 
		Set @sFiltroBeneficiario = 
			' and  ( P.IdCliente = ' + char(39) + RIGHT('000000000000000' + @IdCliente, 4) + char(39) +   
			' and P.IdSubCliente = ' + char(39) + RIGHT('000000000000000' + @IdSubCliente, 4) + char(39) +  
			' and P.IdBeneficiario = ' + char(39) + RIGHT('000000000000000' + @IdBeneficiario, 8) + char(39) + ' ) ' + CHAR(13) 
	End 


	If @Filtro_Folios = 1 
	Begin  
		If @Folio_Inicial <> ''  and @Folio_Final <> '' 
			Begin 
				Set @sFiltroFolios = ' and ( FolioPedido between ' + char(39) + RIGHT('000000000000000' + @Folio_Inicial, 6) + char(39) + ' and ' + char(39) + RIGHT('000000000000000' + @Folio_Final, 6) + char(39) + ' ) ' + char(10) 
			End 
		Else 
			Begin 
				If @Folio_Inicial <> '' 
					Begin 
						Set @sFiltroFolios = ' and FolioPedido >= ' + char(39) + RIGHT('000000000000000' + @Folio_Inicial, 6) + char(39) 
					End 

				If @Folio_Final <> '' 
					Begin 
						Set @sFiltroFolios = ' and FolioPedido <= ' + char(39) + RIGHT('000000000000000' + @Folio_Final, 6) + char(39)  
					End 
			End 
		Set @sFiltroFolios = @sFiltroFolios + char(10) 
	End 




--- Jurisdicciones 
	Select IdEstado, IdJurisdiccion, Descripcion as Jurisdiccion 
	Into #tmpJuris 
	From CatJurisdicciones J (NoLock) 
	Where IdEstado = @IdEstado 

	----If @IdJurisdiccion <> '*' 
	----Begin 
	----   Delete From #tmpJuris Where IdJurisdiccion <> right('000' + @IdJurisdiccion, 3)  
	----End 

	
--- Farmacias 
	Select J.IdEstado, J.IdJurisdiccion, J.Jurisdiccion, F.IdFarmacia, F.Farmacia  
	Into #tmpFarmacias  
	From #tmpJuris J (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( J.IdEstado = F.IdEstado and J.IdJurisdiccion = F.IdJurisdiccion  )  



	Select D.*
	Into #CFGC_ALMN__RutaDistribucion_Beneficiario
	From CFGC_ALMN__RutaDistribucion C (NoLock)
	Inner Join CFGC_ALMN__RutaDistribucion_Beneficiario D (NoLock) On (C.IdEstado = D.IdEstado And C.IdFarmacia = D.IdFarmacia And C.IdRuta = D.IdRuta)
	Where C.Status = 'A' And D.Status = 'A'

	Select D.*
	Into #CFGC_ALMN__RutaDistribucion_Transferencia
	From CFGC_ALMN__RutaDistribucion C (NoLock)
	Inner Join CFGC_ALMN__RutaDistribucion_Transferencia D (NoLock) On (C.IdEstado = D.IdEstado And C.IdFarmacia = D.IdFarmacia And C.IdRuta = D.IdRuta)
	Where C.Status = 'A' And D.Status = 'A'




	--If @IdJurisdiccion <> '*' 
	--Begin 
	--	If @IdFarmacia <> '*' 
	--	Begin 
	--	   Delete From #tmpFarmacias Where IdEstado = @IdEstado and IdFarmacia <> @IdFarmacia  
	--	End 
	--End 

	
---	 spp_Mtto_Pedidos_Cedis___ListaPedidosParaSurtido  

--	Select * From #tmpJuris		
--	Select * From #tmpFarmacias 	


----		Crear Tabla Temporal 
	Select P.IdEmpresa, P.IdEstado, 
		F.IdJurisdiccion, F.Jurisdiccion, 
		P.EsTransferencia, 
		P.IdCliente, P.IdSubCliente, P.IdBeneficiario, 
		P.IdFarmacia, F.Farmacia, P.IdEstadoSolicita, F.IdJurisdiccion As IdJurisdiccionSolicita, F.Jurisdiccion As JurisdiccionSolicita, IdFarmaciaSolicita, 
		cast(C.NombreFarmacia as varchar(max) ) As FarmaciaSolicita, P.FolioPedido, 0 As NumClaves, 0 As NumPiezas, Convert(Varchar(10), FechaEntrega,120) As FechaEntrega, 0 as Surtimientos, 
		convert(varchar(10), P.FechaRegistro, 120) as FechaPedido, P.Status, '0000' As IdRuta
	Into #tmpPedidos 
	From Pedidos_Cedis_Enc P (NoLock) 
	Inner Join #tmpFarmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia  )
	Inner Join CatFarmacias C (NoLock) On (P.IdEstado = C.IdEstado And P.IdFarmaciaSolicita = C.IdFarmacia)
	Where convert(varchar(10), P.FechaRegistro, 120) Between @FechaInicial and @FechaFinal
		and 1 = 0  



	Set @sSql = 
		'Insert Into #tmpPedidos ' + CHAR(13) + 
		'Select P.IdEmpresa, P.IdEstado, ' + CHAR(13) +  
		'	F.IdJurisdiccion, F.Jurisdiccion, ' + CHAR(13) +  
		'	P.EsTransferencia, ' + CHAR(13) +  
		'   P.IdCliente, P.IdSubCliente, P.IdBeneficiario, ' + CHAR(13) +  
		'	P.IdFarmacia, F.Farmacia, P.IdEstadoSolicita, F.IdJurisdiccion As IdJurisdiccionSolicita, F.Jurisdiccion As JurisdiccionSolicita, IdFarmaciaSolicita, ' + CHAR(13) +  
		'	C.NombreFarmacia As FarmaciaSolicita, P.FolioPedido, 0 As NumClaves, 0 As NumPiezas, Convert(Varchar(10), FechaEntrega,120) As FechaEntrega, 0 as Surtimientos, ' + CHAR(13) +  
		'	convert(varchar(10), P.FechaRegistro, 120) as FechaPedido, P.Status, ' +Char(39) + '0000' + Char(39) + ' As IdRuta ' + CHAR(13) +     	
		'From Pedidos_Cedis_Enc P (NoLock) ' + CHAR(13) +  
		'Inner Join #tmpFarmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia  )  ' + CHAR(13) +  
		'Inner Join CatFarmacias C (NoLock) On (P.IdEstadoSolicita = C.IdEstado And P.IdFarmaciaSolicita = C.IdFarmacia)  ' + CHAR(13) + 
		'Where 1 = 1  ' + @sFiltroFechas +  CHAR(13) + @sFiltroFechas_Entrega + char(13) + @sFiltroFolios + CHAR(13) + @sFiltroBeneficiario 
	Exec( @sSql ) 
	print @sSql 

	--select * from #tmpPedidos

	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioPedido, Sum(Cantidad) As Piezas, Count(Distinct(ClaveSSA)) As Claves
	Into #Totales
	From #tmpPedidos E (NoLock)
	Inner Join Pedidos_Cedis_Det P (NoLock) On ( P.IdEmpresa = E.IdEmpresa And P.IdEstado = E.IdEstado And P.IdFarmacia = E.IdFarmacia And P.FolioPedido = E.FolioPedido)
	Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioPedido


	Update E Set NumClaves = Claves, NumPiezas = Piezas
	From #tmpPedidos E (NoLock)
	Inner Join #Totales P (NoLock) On ( P.IdEmpresa = E.IdEmpresa And P.IdEstado = E.IdEstado And P.IdFarmacia = E.IdFarmacia And P.FolioPedido = E.FolioPedido)

	------------------------------- ASIGNAR LA JURISDICCÓN A LA QUE PERTENECE EL BENEFICIARIO ( VENTAS DIRECTAS ) 
	Update P Set IdJurisdiccion = B.IdJurisdiccion, FarmaciaSolicita = B.IdBeneficiario + '.....' + (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) -- B.NombreCompleto  
	From #tmpPedidos P (NoLock) 
	Inner Join CatBeneficiarios B (NoLock) On ( P.IdEstado = B.IdEstado And P.IdFarmacia = B.IdFarmacia and P.IdCliente = B.IdCliente and P.IdSubCliente = B.IdSubCliente  and P.IdBeneficiario = B.IdBeneficiario ) 
	where P.EsTransferencia = 0

	Update P Set P.IdRuta = B.IdRuta
	From #tmpPedidos P
	Inner Join #CFGC_ALMN__RutaDistribucion_Beneficiario B On (P.IdEstado = B.IdEstado And P.IdFarmacia = B.IdFarmacia and P.IdCliente = B.IdCliente and P.IdSubCliente = B.IdSubCliente  and P.IdBeneficiario = B.IdBeneficiario)
	where P.EsTransferencia = 0 


	------------------------------- ASIGNAR INFORMACIÓN DE LA FARMACIA QUE SOLICITA EL PEDIDO  
	Update P Set 
		-- FarmaciaPedido = B.IdBeneficiario + '.....' + (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre),  -- B.NombreCompleto 
		--IdFarmaciaPedido = B.IdFarmacia,  
		FarmaciaSolicita = B.Farmacia, -- B.NombreCompleto 
		IdJurisdiccionSolicita = B.IdJurisdiccion, JurisdiccionSolicita = B.Jurisdiccion
	From #tmpPedidos P (NoLock) 
	Inner Join vw_Farmacias B (NoLock) 
		On ( P.IdEstadoSolicita = B.IdEstado And P.IdFarmaciaSolicita = B.IdFarmacia ) 
	where P.EsTransferencia = 1

	Update P Set P.IdRuta = B.IdRuta
	From #tmpPedidos P
	Inner Join #CFGC_ALMN__RutaDistribucion_Transferencia B On (P.IdEstadoSolicita = B.IdEstadoEnvia And P.IdFarmaciaSolicita = B.IdFarmaciaEnvia)
	where P.EsTransferencia = 1

	if (@IdRuta <> '*')
	Begin
		Delete From #tmpPedidos   
		Where IdRuta <> @IdRuta
	End


	--select * from #tmpPedidos

	If @IdJurisdiccion <> '*' 
	Begin 
		-- print 'POR AQUI   ' + @IdJurisdiccion 
		Delete From #tmpPedidos   
		Where IdEstado = @IdEstado and IdJurisdiccionSolicita <> @IdJurisdiccion --And EsTransferencia = 1 		
	End 



	If @IdFarmacia <> '*' 
	Begin 
		Delete From #tmpPedidos Where IdEstado = @IdEstado and IdFarmaciaSolicita <> @IdFarmacia --And EsTransferencia = 1
	End 

	--select * from #tmpPedidos

	------------------------------- ASIGNAR LA JURISDICCÓN A LA QUE PERTENECE EL BENEFICIARIO ( VENTAS DIRECTAS ) 


--	select * from  #tmpPedidos 

------------------------------- Determinar Status del Pedido 
	Update P Set Status = 'F' 
	From #tmpPedidos P 
	Inner Join Pedidos_Cedis_Enc_Surtido S (NoLock) 
		On ( P.IdEmpresa = S.IdEmpresa and P.IdEstado = S.IdEstado and P.IdFarmacia = S.IdFarmacia and P.FolioPedido = S.FolioPedido ) 
	Where S.Status = 'F' 
	
	Update P Set Surtimientos = 
		IsNull(( 
			Select count(*) 
			From Pedidos_Cedis_Enc_Surtido S (NoLock) 
			Where P.IdEmpresa = S.IdEmpresa and P.IdEstado = S.IdEstado and P.IdFarmacia = S.IdFarmacia and P.FolioPedido = S.FolioPedido ), 0) 
	From #tmpPedidos P 	
	
	Update P Set Status = ( case when Surtimientos = 0 then 'A' else 'E' end )
	From #tmpPedidos P 	
	Where P.Status <> 'F' 
------------------------------- Determinar Status del Pedido 



	
----------------------------------- Filtrar la informacion a mostrar 
	if @StatusDePedido <> 0 
	Begin 

		if @StatusDePedido = 1 
		Begin 
			Delete From #tmpPedidos Where Status <> 'A' 
		End 

		if @StatusDePedido = 2 
		Begin 
			Delete From #tmpPedidos Where Status <> 'E' 
		End 

	End 
----------------------------------- Filtrar la informacion a mostrar 


----------------------------------- Resultado final 	
	Select --IdRuta, P.IdEstadoSolicita, P.IdFarmaciaSolicita,
		(P.IdJurisdiccion + ' - ' + J.Jurisdiccion) as Jurisdiccion, 
		--'Tipo de pedido' = (case when P.EsTransferencia = 1 then 'TRANSFERENCIA' Else 'VENTA' End ), 
		'Tipo de pedido' = (case when P.EsTransferencia = 1 then (case when P.IdEstado = P.IdEstadoSolicita Then 'TRANSFERENCIA' else 'TRANSFERENCIA INTERESTATAL' end ) Else 'VENTA' End ), 
		P.IdFarmacia, P.Farmacia, P.IdFarmaciaSolicita, P.FarmaciaSolicita As 'Farmacia Solicita', 
		'Folio' = P.FolioPedido, NumClaves As 'Núm. De Claves', NumPiezas As 'Núm. de Piezas', P.FechaEntrega As 'Fecha Entrega', P.Surtimientos, 'Fecha' = P.FechaPedido, Status, 
		'Status pedido' = 
		( 
			case 
				when P.Status = 'F' then 'Surtido completo (En proceso de surtido)' 
				when P.Status = 'A' then 'Pendiente de surtir' 
				when P.Status = 'E' then 'En proceso de surtido' 
			else 
				'En proceso de surtido' 
			end 
		) 
	From #tmpPedidos P (NoLock) 
	Inner Join #tmpJuris J (NoLock) On ( P.IdJurisdiccion = J.IdJurisdiccion ) 
	Order By FolioPedido 
----------------------------------- Resultado final 	



	-- Where Status <> 'T' 	

--	select * 	from Pedidos_Cedis_Enc 
	
End 
Go--#SQL 



