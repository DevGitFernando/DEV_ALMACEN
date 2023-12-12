If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Atenciones_Surtidores_Choferes_Cedis' and xType = 'P' ) 
   Drop Proc spp_Rpt_Atenciones_Surtidores_Choferes_Cedis
Go--#SQL   

Create Proc spp_Rpt_Atenciones_Surtidores_Choferes_Cedis 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmaciaEjecuta varchar(4) = '0003', 
	@IdJurisdiccion varchar(3) = '*', @IdFarmacia varchar(4) = '*', @IdPersonal varchar(4) = '*',
	@FechaInicial varchar(10) = '2013-08-19', @FechaFinal varchar(10) = '2013-08-31', 
	@StatusDePedido varchar(2) = '', @IdPuesto varchar(2) = '01'   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Declare @sSql varchar(7500)
	
	set @sSql = ''
--- Jurisdicciones 
	Select IdEstado, IdJurisdiccion, Descripcion as Jurisdiccion 
	Into #tmpJuris 
	From CatJurisdicciones J (NoLock) 
	Where IdEstado = @IdEstado 

	If @IdJurisdiccion <> '*' 
	   Delete From #tmpJuris Where IdJurisdiccion <> right('000' + @IdJurisdiccion, 3)  

	
--- Farmacias 
	Select J.IdEstado, J.IdJurisdiccion, J.Jurisdiccion, F.IdFarmacia, F.Farmacia  
	Into #tmpFarmacias  
	From #tmpJuris J (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( J.IdEstado = F.IdEstado and J.IdJurisdiccion = F.IdJurisdiccion  )  

	If @IdJurisdiccion <> '*' 
	Begin 
		If @IdFarmacia <> '*'
		   Delete From #tmpFarmacias Where IdEstado = @IdEstado and IdFarmacia <> @IdFarmacia 
	End 
	
	Select IdEstado, IdFarmacia, IdPersonal, Personal, IdPuesto
	Into #tmpSurtidores 
	From vw_PersonalCEDIS(NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaEjecuta and IdPuesto = @IdPuesto
	
	If @IdPersonal <> '*'
	Begin 
		Delete From #tmpSurtidores Where IdEstado = @IdEstado and IdFarmacia = @IdFarmaciaEjecuta 
		and IdPuesto = @IdPuesto and IdPersonal <> @IdPersonal
	End
		
---	 spp_Rpt_Atenciones_Surtidores_Choferes_Cedis  

--	Select * From #tmpJuris		
--	Select * From #tmpFarmacias 	

	Select top 0 P.IdEmpresa, P.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, P.IdFarmacia, F.Farmacia, IdFarmaciaSolicita, 
	C.NombreFarmacia As FarmaciaSolicita, P.FolioPedido, 0 as Surtimientos, S.IdPersonal, SPACE(100) as Personal, 
	convert(varchar(10), P.FechaRegistro, 120) as FechaPedido, S.Status    
	Into #tmpPedidos 
	From Pedidos_Cedis_Enc P (NoLock)
	Inner Join Pedidos_Cedis_Enc_Surtido S (NoLock) 
		On ( P.IdEmpresa = S.IdEmpresa and P.IdEstado = S.IdEstado and P.IdFarmacia = S.IdFarmaciaPedido and P.FolioPedido = S.FolioPedido )
	Inner Join #tmpFarmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia  )
	Inner Join CatFarmacias C (NoLock) On (P.IdEstado = C.IdEstado And P.IdFarmaciaSolicita = C.IdFarmacia)
	Where convert(varchar(10), P.FechaRegistro, 120) Between @FechaInicial and @FechaFinal AND S.IdPersonalSurtido IN 
	( Select T.IdPersonal From #tmpSurtidores T Where T.IdEstado = S.IdEstado and T.IdFarmacia = S.IdFarmacia and T.IdPersonal = S.IdPersonalSurtido ) 


	if @IdPuesto = '01'
	Begin
	
		Set @sSql = ' Insert Into #tmpPedidos ' +
		'Select P.IdEmpresa, P.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, P.IdFarmacia, F.Farmacia, IdFarmaciaSolicita, ' +
		' C.NombreFarmacia As FarmaciaSolicita, P.FolioPedido, 0 as Surtimientos, S.IdPersonalSurtido as IdPersonal, SPACE(100) as Personal, ' +
		' convert(varchar(10), P.FechaRegistro, 120) as FechaPedido, S.Status ' +   
		' From Pedidos_Cedis_Enc P (NoLock)' +
		' Inner Join Pedidos_Cedis_Enc_Surtido S (NoLock) ' +
			' On ( P.IdEmpresa = S.IdEmpresa and P.IdEstado = S.IdEstado and P.IdFarmacia = S.IdFarmaciaPedido and P.FolioPedido = S.FolioPedido )' +
		' Inner Join #tmpFarmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia  )' +
		' Inner Join CatFarmacias C (NoLock) On (P.IdEstado = C.IdEstado And P.IdFarmaciaSolicita = C.IdFarmacia)' +
		' Where convert(varchar(10), P.FechaRegistro, 120) Between ' + char(39) +  @FechaInicial + char(39) + ' and ' 
		+ char(39) +  @FechaFinal + char(39) +  ' AND S.IdPersonalSurtido IN ' +
		' ( Select T.IdPersonal From #tmpSurtidores T Where T.IdEstado = S.IdEstado and T.IdFarmacia = S.IdFarmacia ' +
			' and T.IdPersonal = S.IdPersonalSurtido ) '
	--	print @sSql
		Exec(@sSql)
	End
	
	if @IdPuesto = '02'
	Begin
		Set @sSql = ' Insert Into #tmpPedidos ' +
		'Select P.IdEmpresa, P.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, P.IdFarmacia, F.Farmacia, IdFarmaciaSolicita, ' +
		' C.NombreFarmacia As FarmaciaSolicita, P.FolioPedido, 0 as Surtimientos, S.IdPersonalTransporte as IdPersonal, SPACE(100) as Personal, ' +
		' convert(varchar(10), P.FechaRegistro, 120) as FechaPedido, S.Status ' +   
		' From Pedidos_Cedis_Enc P (NoLock)' +
		' Inner Join Pedidos_Cedis_Enc_Surtido S (NoLock) ' +
			' On ( P.IdEmpresa = S.IdEmpresa and P.IdEstado = S.IdEstado and P.IdFarmacia = S.IdFarmaciaPedido and P.FolioPedido = S.FolioPedido )' +
		' Inner Join #tmpFarmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia  )' +
		' Inner Join CatFarmacias C (NoLock) On (P.IdEstado = C.IdEstado And P.IdFarmaciaSolicita = C.IdFarmacia)' +
		' Where convert(varchar(10), P.FechaRegistro, 120) Between ' + char(39) +  @FechaInicial + char(39) + ' and ' 
		+ char(39) +  @FechaFinal + char(39) +  ' AND S.IdPersonalTransporte IN ' +
		' ( Select T.IdPersonal From #tmpSurtidores T Where T.IdEstado = S.IdEstado and T.IdFarmacia = S.IdFarmacia ' +
			' and T.IdPersonal = S.IdPersonalTransporte ) '
			
		Exec(@sSql)
	End

	

--	select * from  #tmpPedidos 
	
	Update P Set Surtimientos = 
		IsNull(( 
			Select count(*) 
			From Pedidos_Cedis_Enc_Surtido S (NoLock)			 
			Where P.IdEmpresa = S.IdEmpresa and P.IdEstado = S.IdEstado and P.IdFarmacia = S.IdFarmaciaPedido and P.FolioPedido = S.FolioPedido), 0) 
	From #tmpPedidos P 		
	
----------------------------------- Filtrar la informacion a mostrar 
	if @StatusDePedido <> 0 
	Begin 
		Delete From #tmpPedidos Where Status <> @StatusDePedido 
	End 
----------------------------------- Filtrar la informacion a mostrar 

------------- Actualizar el Nombre de Personal de surtido-----------------------------------

	Update P Set Personal = S.Personal
	From #tmpPedidos P
	Inner Join #tmpSurtidores S On ( S.IdEstado = @IdEstado and S.IdFarmacia = @IdFarmaciaEjecuta and P.IdPersonal = S.IdPersonal ) 
	
--------------------------------------------------------------------------------------------

----------------------------------- Resultado final 	
	Select
		'Núm. Personal' = IdPersonal, 'Personal' = Personal, 
		(IdJurisdiccion + ' - ' + Jurisdiccion) as Jurisdiccion,
		IdFarmacia, Farmacia, FarmaciaSolicita As 'Farmacia Solicita', 
		'Folio'= FolioPedido, Surtimientos,		 
		'Fecha' = FechaPedido, Status, 
		'Status pedido' = IsNull( ( Select Top 1 Descripcion From Pedidos_Status EP Where M.Status = EP.ClaveStatus ), 'SIN ESPECIFICAR' ) 
	From #tmpPedidos M 
----------------------------------- Resultado final 	



	-- Where Status <> 'T' 	

--	select * 	from Pedidos_Cedis_Enc 
	
End 
Go--#SQL 

	--	Select * From #tmpSurtidores
