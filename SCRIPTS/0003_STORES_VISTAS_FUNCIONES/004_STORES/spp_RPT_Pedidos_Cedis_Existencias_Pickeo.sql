-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_RPT_Pedidos_Cedis_Existencias_Pickeo' and xType = 'P' ) 
   Drop Proc spp_RPT_Pedidos_Cedis_Existencias_Pickeo  
Go--#SQL   

Create Proc spp_RPT_Pedidos_Cedis_Existencias_Pickeo 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmaciaSurte varchar(4) = '1005', @MesesCaducar int = 1, 
	@FechaEntrega varchar(10) = '2019-10-22', @FechaEntregaFinal varchar(10) = '2019-12-31'
) 
With Encryption 
As 
Begin 
Set NoCount On  


	If @FechaEntregaFinal  = '' 
	Begin
		Set @FechaEntregaFinal = @FechaEntrega 
	End 



	Select 
		P.IdEmpresa, P.IdEstado, P.IdFarmacia, P.IdFarmaciaSolicita, (( Case When (P.IdFarmacia <> P.IdFarmaciaSolicita) Then F.Farmacia + ' -- ' + Fs.Farmacia Else F.Farmacia End)) As Farmacia, 
		P.FolioPedido, P.FechaRegistro as FechaPedido, P.IdPersonal, P.Observaciones, P.Status, 
		(case when P.EsTransferencia = 1 then 1 else 2 end) as TipoDePedido 
	Into #tmpEncabezado 	
	From Pedidos_Cedis_Enc P (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia )
	Inner Join vw_Farmacias FS (NoLock) On ( P.IdEstado = FS.IdEstado and P.IdFarmaciaSolicita = FS.IdFarmacia )  
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.Status not in ('F') 
		And Convert(varchar(10), FechaEntrega, 120) between @FechaEntrega and @FechaEntregaFinal 


	--Select * From #tmpEncabezado


	Select IdFarmaciaPedido, FolioPedido, FolioTransferenciaReferencia 
	Into #tmpTransferencias 
	From Pedidos_Cedis_Enc_Surtido P (NoLock) 
	Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
		On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado --and P.IdFarmacia = @IdFarmacia 
		  And left(FolioTransferenciaReferencia, 2) = 'TS'
	

	--------------Detallado 
	Select Top 0 
		0 as TipoDePedido, 
		Cast('' As varchar(3)) As IdEmpresa, Cast('' As varchar(2)) As IdEstado,
		Cast('' As varchar(4)) As IdFarmacia, Cast('' As varchar(4)) As IdFarmaciaSolicita, Cast('' As varchar(8)) As FolioPedido,
		Cast('' As varchar(30)) As IdClaveSSA, Cast('' As varchar(30)) As ClaveSSA, Cast('' As varchar(max)) As DescripcionClave,
		Cast(0 As Numeric(14, 4)) as Cantidad_En_Pedido, Cast(0 As Numeric(14, 4)) as Cantidad_En_Surtimiento,
		Cast(0 As Numeric(14, 4)) as Cantidad_Asignada, Cast(0 As Numeric(14, 4)) as Cantidad_Aplicada,
		--Cast(0 As Numeric(14, 4)) As Existencia, 
		Cast(0 As Numeric(14, 4)) as Cantidad_Por_Surtir,
		'A' As Status, cast(0 as bit) as Terminar, cast(0 as bit) as ClaveTerminada,
		Cast(0 As Numeric(14, 4)) as Cantidad_En_Transferencias
	Into #tmpDetalle 
			
		
	Insert Into #tmpDetalle 
	Select 
		TipoDePedido, 
		P.IdEmpresa, P.IdEstado, P.IdFarmacia, T.IdFarmaciaSolicita, P.FolioPedido,
		P.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, --P.Cantidad
		(Case
			When P.PCM = 0 Then P.Cantidad
			When (P.Cantidad + P.Existencia ) <= P.PCM Then P.Cantidad
			When (P.Cantidad + P.Existencia ) > P.PCM Then P.PCM - P.Existencia
			End) as Cantidad_En_Pedido,
			0 As Cantidad_En_Surtimiento,
			0 as Cantidad_Asignada, 0 as Cantidad_Aplicada,  
		--P.Existencia, 
			0 As Cantidad_Por_Surtir,  
		--IsNull(Sum( F.Existencia ), 0) As Existencia,
		P.Status, cast(0 as bit) as Terminar, cast(0 as bit) as ClaveTerminada, 0 As Cantidad_En_Transferencias 
	--Into #tmpDetalle 
	From Pedidos_Cedis_Det P (NoLock) 
	Inner Join #tmpEncabezado T (NoLock) On (P.IdEmpresa = T.IdEmpresa And P.IdEstado = T.IdEstado And P.IdFarmacia = T.IdFarmacia And P.FolioPedido = T.FolioPedido)
	Inner Join 	vw_ClavesSSA_Sales C (NoLock) On ( P.IdClaveSSA = C.IdClaveSSA_Sal ) 
	--Left Join  #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (Nolock)
	--	On (  F.IdEmpresa = @IdEmpresa And F.IdEstado = @IdEstado And F.IdFarmacia = T.IdFarmacia And P.ClaveSSA = F.ClaveSSA )
	Group By 
		T.TipoDePedido, 
		P.IdEmpresa, P.IdEstado, P.IdFarmacia, T.IdFarmaciaSolicita, P.FolioPedido,
		P.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, P.Cantidad, P.Existencia, P.PCM, P.Status  
			
	------ Se le resta la cantidad en transito (transferencias no aplicadas) 

	--Select * From #tmpDetalle -- Where IdClaveSSA = '0032'
	--Select * From #tmpTransferencias
	--Select * From #tmpDetalle  --Where IdClaveSSA = '0032'


	--------------------------------- Se le asigna cero si la cantidad resulta menor a cero
	Update #tmpDetalle Set Cantidad_En_Pedido = 0 Where Cantidad_En_Pedido < 0 


	

	
------------------------------- Obtener Surtido 	
	Select 
		T.IdEmpresa, T.IdEstado, T.IdFarmacia, IdFarmaciaSolicita, T.FolioPedido, D.Status,
		T.IdClaveSSA, T.Cantidad_En_Pedido,
		sum(IsNull(DD.CantidadRequerida, 0)) as CantidadRequerida, sum(IsNull(DD.CantidadAsignada, 0)) as CantidadSurtida,
		sum(IsNull((Case when P.Status in ( 'E', 'R' ) Then DD.CantidadAsignada else 0 end) , 0)) as CantidadAplicada
	Into #tmpDetalle_Surtido_Detallado  	
	From #tmpDetalle T (NoLock) 
	Left Join Pedidos_Cedis_Enc_Surtido P (NoLock) On ( P.IdEmpresa = T.IdEmpresa And P.IdEstado = T.IdEstado And P.IdFarmaciaPedido = T.IdFarmacia And P.FolioPedido = T.FolioPedido )
	Left Join Pedidos_Cedis_Det_Surtido D (NoLock) 
		On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido And T.ClaveSSA = D.ClaveSSA ) 
	Left Join Pedidos_Cedis_Det_Surtido_Distribucion DD (NoLock) 
		On ( D.IdEmpresa = DD.IdEmpresa and D.IdEstado = DD.IdEstado and D.IdFarmacia = DD.IdFarmacia and D.FolioSurtido = DD.FolioSurtido 
			And D.ClaveSSA = DD.ClaveSSA )
	Where P.Status <> 'C'
	--Where IdClaveSSA = '1526' And P.FolioSurtido in ('00000089','00000316', '00000317')
	Group by T.IdEmpresa, T.IdEstado, T.IdFarmacia, IdFarmaciaSolicita, T.FolioPedido, D.Status, T.IdClaveSSA, T.Cantidad_En_Pedido
	------------------------------- Obtener Surtido
	
	--Select * From #tmpDetalle_Surtido_Detallado	--Where ClaveSSA = '010.000.0101.00' And FolioSurtido in ('00000319')

	-------------------------------------------- Obtener Surtido 		
	Select
		IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita, FolioPedido, 'A' as Status, IdClaveSSA,
		Cantidad_En_Pedido, Sum(CantidadRequerida) As CantidadRequerida, sum(CantidadSurtida) as CantidadSurtida, Sum(CantidadAplicada) As CantidadAplicada
	Into #tmpDetalle_Surtido  	
	From #tmpDetalle_Surtido_Detallado
	Group by IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita, FolioPedido, IdClaveSSA, Cantidad_En_Pedido

	--Select * From #tmpDetalle_Surtido_Detallado Where IdClaveSSA = '0032'
	
	Update S Set Status = 'F' 
	From #tmpDetalle_Surtido S 
	Inner Join #tmpDetalle_Surtido_Detallado D
		On ( D.IdEmpresa = S.IdEmpresa And D.IdEstado = S.IdEstado And D.IdFarmacia = S.IdFarmacia And D.IdFarmaciaSolicita = S.IdFarmaciaSolicita
			 And D.FolioPedido = S.FolioPedido And S.IdClaveSSA = D.IdClaveSSA ) 
	Where D.Status = 'F'  
	
------Select * from #tmpDetalle_Surtido_Detallado 	
------Select * from #tmpDetalle_Surtido 
	
---------------------------------------------- Obtener Surtido 	

	--Select * From #tmpDetalle		  Where FolioPedido = 5	
	--Select * From #tmpDetalle_Surtido Where FolioPedido = 5

	--Select *
	Update D Set Cantidad_En_Surtimiento = S.CantidadRequerida, Cantidad_Asignada = S.CantidadSurtida, Cantidad_Aplicada = S.CantidadAplicada, ClaveTerminada = ( case when S.Status = 'F' Then 1 Else 0 End ) 
	From #tmpDetalle D  
	Inner Join #tmpDetalle_Surtido S
	On ( D.IdEmpresa = S.IdEmpresa And D.IdEstado = S.IdEstado And D.IdFarmaciaSolicita = S.IdFarmaciaSolicita
			 And D.FolioPedido = S.FolioPedido And D.IdClaveSSA = S.IdClaveSSA)
	Update #tmpDetalle Set Cantidad_En_Pedido = Cantidad_En_Pedido - Cantidad_En_Transferencias, Cantidad_En_Surtimiento = Cantidad_En_Surtimiento - Cantidad_En_Transferencias

	--Select * From #tmpDetalle_Surtido
		
	Update D Set ClaveTerminada = 1 
	From #tmpDetalle D  
	Inner Join #tmpDetalle_Surtido S 
		On ( D.IdEmpresa = S.IdEmpresa And D.IdEstado = S.IdEstado And D.IdFarmacia = S.IdFarmacia And D.IdFarmaciaSolicita = S.IdFarmaciaSolicita
				 And D.FolioPedido = S.FolioPedido And D.IdClaveSSA = S.IdClaveSSA )		
	Where ClaveTerminada = 0 and D.Cantidad_En_Pedido <= S.CantidadSurtida 

	
	
----------------------------------------------------- SALIDA FINAL 

	Delete #tmpDetalle Where Cantidad_En_Pedido < Cantidad_En_Surtimiento

	Update #tmpDetalle Set Cantidad_Por_Surtir = Cantidad_En_Surtimiento - Cantidad_Aplicada Where ClaveTerminada = 1

	Update #tmpDetalle Set Cantidad_Por_Surtir =	  Cantidad_En_Pedido - Cantidad_Aplicada Where ClaveTerminada = 0

	
	--Select
	--	IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita, FolioPedido,
	--	ClaveSSA, IdClaveSSA, DescripcionClave, --Existencia,
	--	Cantidad_En_Pedido, Cantidad_En_Surtimiento, Cantidad_Asignada, Cantidad_Aplicada, Cantidad_Por_Surtir,
	--	Terminar, ClaveTerminada  
	--From #tmpDetalle
	----Where FolioPedido = 5 --And IdClaveSSA = '1526'
	--Order By IdEmpresa, IdEstado, IdFarmacia, IdFarmaciaSolicita, FolioPedido, ClaveSSA 


	--Select * From #tmpDetalle


	Select 
		ClaveSSA, IdClaveSSA, DescripcionClave, 
		Sum(Cantidad_Por_Surtir) As Cantidad_Por_Surtir, 
		sum(Cantidad_Por_Surtir__Ventas) as Cantidad_Por_Surtir__Ventas, 
		sum(Cantidad_Por_Surtir__Transferencias) as Cantidad_Por_Surtir__Transferencias, 
		Cast(0 As Numeric(14, 4)) As Existencia, Cast(0 As Numeric(14, 4)) As Existencia_Almacenaje, Cast(0 As Numeric(14, 4)) As Existencia_Pickeo
	Into #tmpSalidaFinal 
	From 
	( 
		Select
			ClaveSSA, IdClaveSSA, DescripcionClave,
			Sum(Cantidad_Por_Surtir) As Cantidad_Por_Surtir, 

			(case when TipoDePedido = 2 then Sum(Cantidad_Por_Surtir) else 0 end) As Cantidad_Por_Surtir__Ventas, 
			(case when TipoDePedido = 1 then Sum(Cantidad_Por_Surtir) else 0 end) As Cantidad_Por_Surtir__Transferencias, 

			Cast(0 As Numeric(14, 4)) As Existencia, Cast(0 As Numeric(14, 4)) As Existencia_Almacenaje, Cast(0 As Numeric(14, 4)) As Existencia_Pickeo
		From #tmpDetalle
		Group BY TipoDePedido, ClaveSSA, IdClaveSSA, DescripcionClave 
	) T 
	Group by ClaveSSA, IdClaveSSA, DescripcionClave  
	Order By ClaveSSA 



	Select 
		IdClaveSSA_Sal As IdClaveSSA, ClaveSSA,
		Sum(( U.Existencia - ( U.ExistenciaEnTransito + U.ExistenciaSurtidos ))) As Existencia,
		Sum((Case When C.EsDePickeo = 0 then ( U.Existencia - ( U.ExistenciaEnTransito + U.ExistenciaSurtidos ) ) Else 0 End)) As Existencia_Almacenaje,
		Sum((Case When C.EsDePickeo = 1 then ( U.Existencia - ( U.ExistenciaEnTransito + U.ExistenciaSurtidos ) ) Else 0 End)) As Existencia_Pickeo
	Into #TmpExistencias 
	From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (L.CodigoEAN = P.CodigoEAN) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock)
		On ( U.IdEmpresa = L.IdEmpresa And U.IdEstado = L.IdEstado And U.IdFarmacia = L.IdFarmacia And
			U.IdSubFarmacia = L.IdSubFarmacia And U.IdProducto = L.IdProducto And U.CodigoEAN = L.CodigoEAN And U.ClaveLote = L.ClaveLote )
	Inner Join CatPasillos_Estantes_Entrepaños C (NoLock)
		On ( U.IdEmpresa = C.IdEmpresa And U.IdEstado = C.IdEstado And U.IdFarmacia = C.IdFarmacia And  U.IdPasillo = C.IdPasillo And U.IdEstante = C.IdEstante And U.IdEntrepaño = C.IdEntrepaño )
	Where 
		C.Status = 'A' And EsExclusiva = 0 And datediff(mm, getdate(), IsNull(L.FechaCaducidad, cast('2000-01-01' as datetime))) >= @MesesCaducar And
		( L.Existencia - L.ExistenciaEnTransito ) > 0 And L.IdEmpresa = @IdEmpresa And L.IdEstado = @IdEstado And L.IdFarmacia = @IdFarmaciaSurte
	Group By IdClaveSSA_Sal, ClaveSSA


	Update F Set Existencia = E.Existencia, F.Existencia_Almacenaje = E.Existencia_Almacenaje, F.Existencia_Pickeo = E.Existencia_Pickeo
	From #tmpSalidaFinal F
	Inner Join #TmpExistencias E On ( F.IdClaveSSA = E.IdClaveSSA And F.ClaveSSA = E.ClaveSSA )


	----------------------- REPORTE DE SALIDA 
	Select 
		IdClaveSSA As Clave, 
		ClaveSSA As 'Clave SSA', 
		DescripcionClave As Descripción,
		Cantidad_Por_Surtir As 'Cantidad por surtir', 
		'Cantidad ventas' = Cantidad_Por_Surtir__Ventas, 
		'Cantidad transferencias' = Cantidad_Por_Surtir__Transferencias, 
		Existencia, 
		Existencia_Almacenaje As Almacenaje, 
		Existencia_Pickeo As Pickeo
	From #tmpSalidaFinal

	
------------------------------------------------------- SALIDA FINAL


End 
Go--#SQL 

