If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_PorcentajeSurtimientoPedidos' and xType = 'P' ) 
   Drop Proc spp_Rpt_PorcentajeSurtimientoPedidos
Go--#SQL   

Create Proc spp_Rpt_PorcentajeSurtimientoPedidos 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', 
	@IdJurisdiccion varchar(3) = '*', @IdFarmacia varchar(4) = '*', 
	@FechaInicial varchar(10) = '2018-05-01', @FechaFinal varchar(10) = '2020-07-25', 
	@StatusDePedido smallint = 0, @Referencia Varchar(100) = ''
) 
With Encryption 
As 
Begin 
Set NoCount On
Set DateFormat YMD

--Declare @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', 
--		@IdJurisdiccion varchar(3) = '*', @IdFarmacia varchar(4) = '*', 
--		@FechaInicial varchar(10) = '2013-01-01', @FechaFinal varchar(10) = '2013-08-31', 
--		@StatusDePedido smallint = 0 

Declare @sSql varchar(300) 

	Select *
	into #tmpFarmacias
	From vw_Farmacias
	Where IdEstado = @IdEstado

	--If (@IdJurisdiccion <> '*')
	--	Begin
	--		Delete #tmpFarmacias Where IdJurisdiccion <> right('000' + @IdJurisdiccion, 3)  
	--	End
		
	If (@IdFarmacia <> '*')
		Begin
			Delete #tmpFarmacias Where IdFarmacia <> right('0000' + @IdFarmacia, 4)  
		End


---------------------------------- Obtener informacion del(os) pedido(s)  
	Select
		(case when P.EsTransferencia = 1 then (case when P.IdEstado = P.IdEstadoSolicita Then 'TRANSFERENCIA' else 'TRANSFERENCIA INTERESTATAL' end ) Else 'VENTA' End ) as TipoDePedido, 
		D.IdEmpresa, D.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, P.IdFarmacia, F.Farmacia, P.EsTransferencia, P.IdCliente, P.IdSubCliente, P.IdBeneficiario,		   
		P.IdEstadoSolicita, 
		IdFarmaciaSolicita, Cast(A.NombreFarmacia As Varchar(800)) As FarmaciaSolicita, P.ReferenciaInterna,
		D.FolioPedido, D.IdClaveSSA , D.ClaveSSA, (IsNull(D.Cantidad, 0)) As Cantidad, D.Status, 
		-- Sum(IsNull(DS.CantidadAsignada, S.CantidadAsignada)) As CantidadAsignada 
		IsNull(Sum(DS.CantidadAsignada), 0) As CantidadAsignada   	   
	Into #Detallado_PRCS 
	From Pedidos_Cedis_Enc P (NoLock) 
	Inner Join Pedidos_Cedis_Det D (NoLock) 
		On (P.IdEmpresa = D.IdEmpresa And P.IdEstado = D.IdEstado And P.IdFarmacia = D.IdFarmacia And P.FolioPedido = D.FolioPedido)
	Left Join Pedidos_Cedis_Enc_Surtido E (NoLock)
		On (D.IdEmpresa = E.IdEmpresa And D.IdEstado = E.IdEstado And D.IdFarmacia = E.IdFarmaciaPedido And D.FolioPedido = E.FolioPedido)
	Left Join Pedidos_Cedis_Det_Surtido S (NoLock) 
		On (E.IdEmpresa = S.IdEmpresa And E.IdEstado = S.IdEstado And E.IdFarmacia = S.IdFarmacia And E.FolioSurtido = S.FolioSurtido And
			D.ClaveSSA = S.ClaveSSA) 
	Left Join Pedidos_Cedis_Det_Surtido_Distribucion DS (NoLock) 
		On ( S.IdEmpresa = DS.IdEmpresa And S.IdEstado = DS.IdEstado And S.IdFarmacia = DS.IdFarmacia And S.FolioSurtido = DS.FolioSurtido And
			S.ClaveSSA = DS.ClaveSSA ) 				
	Inner Join CatFarmacias A (NoLock) On ( P.IdEstadoSolicita = A.IdEstado And P.IdFarmaciaSolicita = A.IdFarmacia)
	Inner Join #tmpFarmacias F (NoLock) On (P.IdEstado = F.IdEstado And P.IdFarmacia = F.IdFarmacia)
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado And convert(varchar(10), P.FechaRegistro, 120) Between @FechaInicial and @FechaFinal
	Group By 
		D.IdEmpresa, D.IdEstado, P.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, P.IdFarmacia, F.Farmacia, P.EsTransferencia,  P.IdCliente, P.IdSubCliente, P.IdBeneficiario,
		P.IdEstadoSolicita, P.IdFarmaciaSolicita, A.NombreFarmacia, P.ReferenciaInterna, 
		D.FolioPedido, D.IdClaveSSA, D.ClaveSSA, S.CantidadAsignada, D.Cantidad, D.Status



	Update P Set IdJurisdiccion = B.IdJurisdiccion, FarmaciaSolicita = B.IdBeneficiario + '.....' + (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) -- B.NombreCompleto  
	From #Detallado_PRCS P (NoLock) 
	Inner Join CatBeneficiarios B (NoLock) On ( P.IdEstado = B.IdEstado And P.IdFarmacia = B.IdFarmacia and P.IdCliente = B.IdCliente and P.IdSubCliente = B.IdSubCliente  and P.IdBeneficiario = B.IdBeneficiario ) 
	where P.EsTransferencia = 0

	If @IdJurisdiccion <> '*' 
	Begin 
		-- print 'POR AQUI   ' + @IdJurisdiccion 
		Delete From #Detallado_PRCS   
		Where IdEstado = @IdEstado and IdJurisdiccion <> @IdJurisdiccion  
	End 
	
	If @Referencia <> '' 
	Begin 
		-- print 'POR AQUI   ' + @IdJurisdiccion 
		Delete From #Detallado_PRCS   
		Where ReferenciaInterna <> @Referencia  
	End 

	


	Select Distinct 
		TipoDePedido, 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		IdCliente, IdSubCliente, IdFarmaciaSolicita, FarmaciaSolicita,
		FolioPedido, IdClaveSSA, ClaveSSA, 0 As Cantidad, 0 As CantidadAsignada, Status 
	Into #Detallado 
	From #Detallado_PRCS 

	
	Update D set Cantidad = P.Cantidad 
	From #Detallado D 
	Inner Join #Detallado_PRCS P On ( D.FolioPedido = P.FolioPedido and D.IdClaveSSA = P.IdClaveSSA and D.ClaveSSA = P.ClaveSSA ) 

	Update D set CantidadAsignada = 
		IsNull( 
		( 
			Select sum(P.CantidadAsignada)  
			From #Detallado_PRCS P Where D.FolioPedido = P.FolioPedido and D.IdClaveSSA = P.IdClaveSSA and D.ClaveSSA = P.ClaveSSA 
		), 0 )  	
	From #Detallado D 
---------------------------------- Obtener informacion del(os) pedido(s)  



	if (@StatusDePedido <> 0)
	Begin 
		if (@StatusDePedido = 1)
		Begin 
			Delete From #Detallado Where Status <> 'A' 
		End 

		if (@StatusDePedido = 2)
		Begin 
			Delete From #Detallado Where Status <> 'E' 
		End 
	End 
			

----------------- Informacion concentrada 
	Select 
		TipoDePedido, 
		IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, D.IdFarmaciaSolicita, FarmaciaSolicita,
		FolioPedido, Sum(Cantidad) As PiezasSolicitadas, Sum(CantidadAsignada) As PiezasAsignadas,
		cast(0 As Numeric(14,2)) As PorcentajeDePiezas,
		Count(*) As ClavesSolicitadas,  CAST(0 as Int) As ClavesAsignadas, cast(0 As Numeric(14,2)) As PorcentajeDeClaves, 
		cast('' as varchar(2)) as Status
	into #Concentrado
	From #Detallado D 
	Group By 
		TipoDePedido, IdEmpresa, IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, D.IdFarmaciaSolicita, FarmaciaSolicita, FolioPedido, Status

	Update #Concentrado
	Set PorcentajeDePiezas = ((PiezasAsignadas * 100) / Cast(PiezasSolicitadas As Numeric(14, 4)))
	Where PiezasAsignadas > 0 And PiezasSolicitadas > 0

	Update C
	Set ClavesAsignadas = (Select COUNT(*)
								  From #Detallado D
								  Where C.IdEmpresa = D.IdEmPresa And C.IdEstado = D.IdEstado And C.IdFarmacia = D.IdFarmacia And
										C.FolioPedido = D.FolioPedido And D.CantidadAsignada > 0)
	From #Concentrado C

	Update C  
	Set PorcentajeDeClaves = ((ClavesAsignadas * 100) / Cast(ClavesSolicitadas As Numeric(14, 4)))
	From #Concentrado C  
	
	
	
	Update C set Status = 'A'   
	From #Concentrado C 
	Where Status = '' and ( PorcentajeDePiezas = 0 and PorcentajeDeClaves = 0 ) 	
	
	Update C set Status = 'E'   
	From #Concentrado C 
	Where Status = '' and ( PorcentajeDePiezas < 100 or PorcentajeDeClaves < 100 ) 		
	
	Update C set Status = 'F'  
	From #Concentrado C 
	Where Status = '' and ( PorcentajeDePiezas = 100 and PorcentajeDeClaves = 100 ) 
----------------- Informacion concentrada 


----------------- SALIDA FINAL  
	--Select (IdJurisdiccion + ' -- ' + Jurisdiccion) As Jurisdiccion, IdFarmacia, Farmacia, FarmaciaSolicita,
	--	FolioPedido, PiezasSolicitadas, PiezasAsignadas, PorcentajeDePiezas, ClavesSolicitadas,
	--	ClavesAsignadas, PorcentajeDeClaves, 
	--	Status = ( 
	--				case 
	--					when Status = 'F' then 'Surtido completo (En proceso de surtido)' 
	--					when Status = 'A' then 'Pendiente de surtir' 
	--					when Status = 'E' then 'En proceso de surtido' 
	--					else 
	--						'En proceso de surtido' 
	--				end 
	--			 ) 
		Select 
			TipoDePedido as 'Tipo de pedido',
			(IdJurisdiccion + ' -- ' + Jurisdiccion) As Jurisdiccion, IdFarmacia as 'Clave Farmacia', Farmacia, FarmaciaSolicita As 'Clave Farmacia Solicita', FolioPedido As 'Folio Pedido',
			PiezasSolicitadas As 'Piezas Solicitadas', PiezasAsignadas As 'Piezas Asignadas', PorcentajeDePiezas As 'Porcentaje De Piezas',
			 ClavesSolicitadas As 'Claves Solicitadas', ClavesAsignadas As 'Claves Asignadas', PorcentajeDeClaves As 'Porcentaje De Claves',
			Status = ( 
			case 
				when Status = 'F' then 'Surtido completo (En proceso de surtido)' 
				when Status = 'A' then 'Pendiente de surtir' 
				when Status = 'E' then 'En proceso de surtido' 
				else 
					'En proceso de surtido' 
			end 
			) 
	From #Concentrado
	Order By  IdEstado, IdJurisdiccion, IdFarmacia, FolioPedido


	Select  
		TipoDePedido as 'Tipo de pedido',
		(IdJurisdiccion + ' -- ' + Jurisdiccion) As Jurisdiccion, IdFarmacia as 'Clave Farmacia', Farmacia, IdCliente, IdSubCliente, IdFarmaciaSolicita As 'Clave Farmacia Solicita', FarmaciaSolicita, FolioPedido As 'Folio Pedido',
			TipoDeClaveDescripcion As 'Tipo De Clave', D.ClaveSSA, DescripcionCortaClave As Descripción, Presentacion As Presentación, ContenidoPaquete As 'Contenido Paquete', Cantidad, CantidadAsignada As 'Cantidad Asignada',
			(Case When CantidadAsignada  = 0 or Cantidad = 0
				Then 0
				Else 
			((CantidadAsignada * 100) / Cast(Cantidad As Numeric(14, 4)))
				End ) As Porcentaje, 
			Status = ( 
					case 
						when Status = 'F' then 'Surtido completo (En proceso de surtido)' 
						when Status = 'A' then 'Pendiente de surtir' 
						when Status = 'E' then 'En proceso de surtido' 
						else 
							'En proceso de surtido' 
						end 
					  ) 
	From #Detallado D (NoLock)
	Inner Join vw_ClavesSSA_Sales S (NoLock) On (D.IdClaveSSA = S.IdClaveSSA_Sal)
	Order By IdEstado, IdJurisdiccion, IdFarmacia, IdFarmaciaSolicita, FolioPedido, ClaveSSA

----------------- SALIDA FINAL  

End 
Go--#SQL 

