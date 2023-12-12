------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_Pedidos_Cedis_CargarPedidoSurtido' and xType = 'P' ) 
   Drop Proc spp_Mtto_Pedidos_Cedis_CargarPedidoSurtido 
Go--#SQL   

---		Exec spp_Mtto_Pedidos_Cedis_CargarPedidoSurtido '001', '21', '2182', '2182', '*', '000009', '1', '1'   

---		Exec spp_Mtto_Pedidos_Cedis_CargarPedidoSurtido '001', '21', '2182', '2182', '*', '000009', '1', '0'  
--	Exec spp_Mtto_Pedidos_Cedis_CargarPedidoSurtido  @IdEmpresa = '001', @IdEstado = '11', @IdFarmacia = '1005', @IdFarmaciaPedido = '1013',  @FolioSurtido = '*', @FolioPedido = '000111', @AplicarEnTransito = '1', @Manual = '0' 

Create Proc spp_Mtto_Pedidos_Cedis_CargarPedidoSurtido 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '1005', 
	@IdFarmaciaPedido varchar(4) = '', @FolioSurtido varchar(8) = '*', @FolioPedido varchar(6) = '428', 
	@AplicarEnTransito bit = 1, @Manual Bit = 0 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@StatusPedido bit, 
	@Prioridad int  

	Set @IdEmpresa = right('00000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('00000000' + @IdEstado, 2)  
	Set @IdFarmacia = right('00000000' + @IdFarmacia, 4)  
	Set @FolioPedido = right('00000000' + @FolioPedido, 6)  

	----------------------------------------------AplicarEnTransito a Cero

	Set @AplicarEnTransito = 0


	Select * 
	Into #tmpFarmacias 
	From vw_Farmacias 
	Where IdEstado = @IdEstado 


	Set @Prioridad = 1 
	Set @StatusPedido = 0 
	Select @StatusPedido = 1, @Prioridad = Prioridad   
	From Pedidos_Cedis_Enc_Surtido P (NoLock) 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia 
		  --and P.IdFarmaciaPedido = @IdFarmaciaPedido
		  and P.FolioPedido = @FolioPedido and Status = 'F' 	
	Set @StatusPedido = IsNull(@StatusPedido, 0) 
	

	------------- Listado de claves, reducir el tiempo de ejecucion 
	select ClaveSSA 
	Into #Claves_Pedido 
	From Pedidos_Cedis_Det P (NoLock) 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioPedido = @FolioPedido 	 

	Select  
		F.IdEmpresa, F.IdEstado, F.IdFarmacia, IdSubFarmacia, F.IdProducto, F.CodigoEAN, ClaveLote, EsConsignacion, F.IdPasillo, F.IdEstante, F.IdEntrepaño, C.EsDePickeo, (Existencia - (ExistenciaEnTransito+ ExistenciaSurtidos)) As Existencia,
		IdClaveSSA_Sal, P.ClaveSSA, 
		0 As EsAntibiotico, 0 As EsControlado, 0 As EsRefrigerado 
	Into #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock)	
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( F.IdProducto = P.IdProducto And F.CodigoEAN = P.CodigoEAN ) 
	Inner Join #Claves_Pedido CP (NoLock) On ( P.ClaveSSA = CP.ClaveSSA )
	Inner Join CatPasillos_Estantes_Entrepaños C (NoLock) 
		On ( F.IdEmpresa = C.IdEmpresa and F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia And F.IdPasillo = C.IdPasillo And F.IdEstante = C.IdEstante And F.IdEntrepaño = C.IdEntrepaño)
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.Status = 'A' 




	-------------- Clasificacion en base al EAN 
	Select F.ClaveSSA, F.CodigoEAN, Max(cast(P.EsAntibiotico As int)) As EsAntibiotico, Max(cast(P.EsControlado As int)) As EsControlado, Max(cast(P.EsRefrigerado As int)) As EsRefrigerado
	Into #Clasificacion
	From #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( F.CodigoEAN = P.CodigoEAN )
	Where F.Existencia > 0
	Group By F.ClaveSSA, F.CodigoEAN 


	Update F Set EsAntibiotico = C.EsAntibiotico, F.EsControlado = C.EsControlado, EsRefrigerado = C.EsRefrigerado
	From #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F
	Inner Join #Clasificacion C On ( F.ClaveSSA = C.ClaveSSA and F.CodigoEAN = C.CodigoEAN )  
	-------------- Clasificacion en base al EAN 




	Select 
		P.IdEmpresa, P.IdEstado, P.IdFarmacia, (( Case When (P.IdFarmacia <> P.IdFarmaciaSolicita) Then F.Farmacia + ' -- ' + Fs.Farmacia Else F.Farmacia End)) As Farmacia, 
		P.FolioPedido, P.FechaRegistro as FechaPedido, P.IdPersonal, P.Observaciones, P.Status, 
		@StatusPedido as PedidoSurtido, @Prioridad as Prioridad, CajasCompletas 
	Into #tmpEncabezado 	
	From Pedidos_Cedis_Enc P (NoLock) 
	Inner Join #tmpFarmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia )
	Inner Join vw_Farmacias FS (NoLock) On ( P.IdEstadoSolicita = FS.IdEstado and P.IdFarmaciaSolicita = FS.IdFarmacia )  
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioPedido = @FolioPedido 		
	
	
	Select IdEmpresa, IdEstado, IdFarmacia, FolioSurtido, IdFarmaciaPedido, FolioPedido, FechaRegistro, 
		IdPersonal, Observaciones, IdPersonalSurtido as IdSurtidor, IdPersonalTransporte As IdTransportista, 
		Status, MesesCaducidad, MesesCaducidad_Consigna, TipoDeUbicaciones, Prioridad, IdGrupo
	Into #tmpEncabezadoSurtido 
	From Pedidos_Cedis_Enc_Surtido P (NoLock) 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia And P.IdFarmaciaPedido = @IdFarmaciaPedido 
		  and P.IdFarmacia = @IdFarmacia and P.FolioSurtido = @FolioSurtido  
	
	


--------------Detallado 
	Select Top 0 
		Cast('' As Varchar(30)) As IdClaveSSA, Cast('' As Varchar(30)) As ClaveSSA, Cast('' As Varchar(max)) As DescripcionClave,
		Cast(0 As Numeric(14, 4)) as CantidadSolicitada, Cast(0 As Numeric(14, 4)) as CantidadSurtida, Cast(0 As Numeric(14, 4)) as Cantidad,
		Cast(0 As Numeric(14, 4)) As Existencia, 
		Cast(0 As Numeric(14, 4)) As ExistenciaAlmacenaje, 
		'A' As Status, cast(0 as bit) as Terminar, cast(0 as bit) as ClaveTerminada, 0 as ContenidoPaquete, 
		0 as Antibiotico, 0 as Controlado, 0 as Refrigerado, 
		0 as IdClasificacion, cast('' as varchar(100)) as Clasificacion, 0 as Procesado   
	Into #tmpDetalle 
 	 
	If (@Manual = 0)
		Begin 
			Insert Into #tmpDetalle 
			Select 
				P.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, --P.Cantidad
				(Case
					When P.PCM = 0 Then P.Cantidad
					When (P.Cantidad + P.Existencia ) <= P.PCM Then P.Cantidad
					When (P.Cantidad + P.Existencia ) > P.PCM Then P.PCM - P.Existencia
				 End) as CantidadSolicitada, 
				 0 as CantidadSurtida, 0 as Cantidad,  
				--P.Existencia, 
				IsNull(Sum( (case when F.EsDePickeo = 1 then F.Existencia else 0 end) ), 0) As Existencia, 
				IsNull(Sum( (case when F.EsDePickeo = 1 then 0 else F.Existencia end) ), 0) As ExistenciaAlmacenaje, 
				P.Status, cast(0 as bit) as Terminar, cast(0 as bit) as ClaveTerminada, C.ContenidoPaquete, 
				IsNull(max(F.EsAntibiotico), 0) as EsAntibiotico, IsNull(max(F.EsControlado), 0) as EsControlado, IsNull(max(F.EsRefrigerado), 0) as EsRefrigerado,  
				0 as IdClasificacion, cast('' as varchar(100)) as Clasificacion, 0 as Procesado 
			--Into #tmpDetalle 
			From Pedidos_Cedis_Det P (NoLock) 
			Inner Join 	vw_ClavesSSA_Sales C (NoLock) On ( P.IdClaveSSA = C.IdClaveSSA_Sal )
			Left Join  #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (Nolock) On ( P.ClaveSSA = F.ClaveSSA ) 
			Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioPedido = @FolioPedido 
				And Not Exists 
				( 
					select * 
					From Pedidos__Ubicaciones_Excluidas_Surtimiento	P (NoLock) 
					Where F.IdEmpresa = P.IdEmpresa and F.IdEstado = P.IdEstado and F.IdFarmacia = P.IdFarmacia 
						  and F.IdPasillo = P.IdPasillo and F.IdEstante = P.IdEstante and F.IdEntrepaño = P.IdEntrepaño And P.Excluida = 1 
				) 
			Group By  
				P.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, P.Cantidad, P.Existencia, P.PCM, P.Status, C.ContenidoPaquete   
				-- IsNull(F.EsAntibiotico, 0), IsNull(F.EsControlado, 0), IsNull(F.EsRefrigerado, 0) 
		End
	-------------- Obsoleto, Jesús Díaz, 20180807.0735 
	----Else
	----	Begin
	----		Select 
	----			F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdProducto, P.ClaveSSA, F.EsDePickeo, Existencia, Existencia as ExistenciaAlmacenaje, 
	----			datediff(mm, getdate(), IsNull(F.FechaCaducidad, cast('2000-01-01' as datetime))) as MesesCaducar
	----		Into #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
	----		From FarmaciaProductos_CodigoEAN_Lotes F (NoLock)
	----		Inner Join vw_Productos_CodigoEAN P (NoLock) On (F.CodigoEAN = P.CodigoEAN)			
		
	----		Insert Into #tmpDetalle 
	----		Select 
	----			P.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, --P.Cantidad
	----			(Case
	----				When P.PCM = 0 Then P.Cantidad
	----				When (P.Cantidad + P.Existencia ) <= P.PCM Then P.Cantidad
	----				When (P.Cantidad + P.Existencia ) > P.PCM Then P.PCM - P.Existencia
	----			 End) as CantidadSolicitada, 
	----			 0 as CantidadSurtida, 0 as Cantidad,  
	----			--P.Existencia, 
	----			IsNull(Sum( (case when F.EsDePickeo = 1 then F.Existencia else 0 end) ), 0) As Existencia, 
	----			IsNull(Sum( (case when F.EsDePickeo = 1 then 0 else F.Existencia end) ), 0) As ExistenciaAlmacenaje, 
	----			P.Status, cast(0 as bit) as Terminar, cast(0 as bit) as ClaveTerminada 
	----		--Into #tmpDetalle 
	----		From Pedidos_Cedis_Det P (NoLock) 
	----		Inner Join 	vw_ClavesSSA_Sales C (NoLock) On ( P.IdClaveSSA = C.IdClaveSSA_Sal )
	----		Left Join  #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (Nolock)
	----			On (  F.IdEmpresa = @IdEmpresa And F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia And P.ClaveSSA = F.ClaveSSA ) 
	----		Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioPedido = @FolioPedido
	----		Group By  P.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, P.Cantidad, P.Existencia, P.PCM, P.Status
	----	End


	--Select F.ClaveSSA, F.CodigoEAN, Max(cast(P.EsAntibiotico As int)) As EsAntibiotico, Max(cast(P.EsControlado As int)) As EsControlado, Max(cast(P.EsRefrigerado As int)) As EsRefrigerado
	--Into #Clasificacion
	--From #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F (NoLock)
	--Inner Join vw_Productos_CodigoEAN P (NoLock) On ( F.CodigoEAN = P.CodigoEAN )
	--Where F.Existencia > 0
	--Group By F.ClaveSSA, F.CodigoEAN 


	--Update F Set EsAntibiotico = C.EsAntibiotico, F.EsControlado = C.EsControlado, EsRefrigerado = C.EsRefrigerado
	--From #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F
	--Inner Join #Clasificacion C On ( F.ClaveSSA = C.ClaveSSA and F.CodigoEAN = C.CodigoEAN )  


	-- select * from #FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 


	Select Distinct FolioTransferenciaReferencia
	Into #tmpTransferencias
	From Pedidos_Cedis_Enc_Surtido P (NoLock) 
	Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
		On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado --and P.IdFarmacia = @IdFarmacia 
		  and P.IdFarmaciaPedido = @IdFarmacia--Pedido
		  and P.FolioPedido = @FolioPedido
		  And left(FolioTransferenciaReferencia, 2) = 'TS'
			

------ Se le resta la cantidad en transito (transferencias no aplicadas) 
	If @AplicarEnTransito = 1 
	Begin 
		update T Set CantidadSolicitada =  CantidadSolicitada - 
			IsNull(
					(
						Select Sum(CantidadEnviada)
						From TransferenciasEnc Te(NoLock)
						Inner Join TransferenciasDet Td(NoLock) 
							On ( Te.IdEmpresa = Td.IdEmpresa And Te.IdEstado = Td.IdEstado
								 And Te.IdFarmacia = Td.IdFarmacia And Te.FolioTransferencia = Td.FolioTransferencia )
						Inner Join vw_Productos_CodigoEAN P (NoLock) 
							On ( Td.IdProducto = P.IdProducto And Td.COdigoEAN = P.CodigoEAN )
						Where  Te.IdEstadoRecibe = @IdEstado And Te.IdFarmaciaRecibe = @IdFarmaciaPedido
							And Te.IdFarmacia = @IdFarmacia And TipoTransferencia = 'TS' And TransferenciaAplicada = '0' And T.ClaveSSA = P.CLaveSSA
							And te.FolioTransferencia Not in (Select FolioTransferenciaReferencia From #tmpTransferencias)	
					)
				,0 )
		From #tmpDetalle T (NoLock)  
	End 

	
------------------------------- Se le asigna cero si la cantidad resulta menor a cero
	Update #tmpDetalle Set CantidadSolicitada = 0 Where CantidadSolicitada < 0


------------------------------- Obtener Surtido 	
	Select D.Status, D.IdClaveSSA, D.ClaveSSA, sum(IsNull(DD.CantidadAsignada, 0)) as CantidadSurtida
	Into #tmpDetalle_Surtido_Detallado  	
	From Pedidos_Cedis_Enc_Surtido P (NoLock) 
	Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
		On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
	Left Join Pedidos_Cedis_Det_Surtido_Distribucion DD (NoLock) 
		On ( D.IdEmpresa = DD.IdEmpresa and D.IdEstado = DD.IdEstado and D.IdFarmacia = DD.IdFarmacia and D.FolioSurtido = DD.FolioSurtido 
			And D.ClaveSSA = DD.ClaveSSA ) 		
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmaciaPedido = @IdFarmacia 
		  --and P.IdFarmaciaPedido = @IdFarmaciaPedido 
		  and P.FolioPedido = @FolioPedido 	
	Group by D.Status, D.IdClaveSSA, D.ClaveSSA    
------------------------------- Obtener Surtido 		

	
	

----------		spp_Mtto_Pedidos_Cedis_CargarPedidoSurtido 


	----Select * 
	----From #tmpDetalle 
	----Where ClaveSSA like '%408%' 

	----Select * 
	----From #tmpDetalle_Surtido_Detallado  
	----Where IdClaveSSA like '%0538%' 

	--select * 
	--from #tmpDetalle_Surtido_Detallado 
	--Where ClaveSSA like '%105%'
	

--- Consulta de Folio de Surtido 	
	If @FolioSurtido <> '*' or @FolioSurtido <> '' 
	Begin 
		Delete From #tmpDetalle_Surtido_Detallado 
	
		Insert Into #tmpDetalle_Surtido_Detallado 
		Select 
			(Case When P.Status = 'C' Then 'C' else D.Status End) As Status, 
			D.IdClaveSSA, D.ClaveSSA, 
			sum(IsNull((case when P.Status not in ( 'E', 'R', 'P' ) Then (Case When P.Status = 'C' Then 0 else DD.CantidadRequerida End) Else DD.CantidadAsignada End), 0)) as CantidadSurtida 
		From Pedidos_Cedis_Enc_Surtido P (NoLock) 
		Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
			On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
		Left Join Pedidos_Cedis_Det_Surtido_Distribucion DD (NoLock) 
			On ( D.IdEmpresa = DD.IdEmpresa and D.IdEstado = DD.IdEstado and D.IdFarmacia = DD.IdFarmacia and D.FolioSurtido = DD.FolioSurtido 
				And D.ClaveSSA = DD.ClaveSSA ) 		
		Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmaciaPedido = @IdFarmacia 
			  --and P.IdFarmaciaPedido = @IdFarmaciaPedido
			  and P.FolioPedido = @FolioPedido 		
		Group by P.Status, D.Status, D.IdClaveSSA, D.ClaveSSA   		
	
		----select * 
		----from #tmpDetalle_Surtido_Detallado 
		----Where ClaveSSA like '%105%'  


		Select IdClaveSSA, CantidadAsignada as CantidadSurtida 
		Into #tmpDetalle_Surtido_Guardado   	
		From Pedidos_Cedis_Enc_Surtido P (NoLock) 
		Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
			On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
		Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmaciaPedido = @IdFarmacia 
			  --and P.IdFarmacia = @IdFarmacia
			  and P.FolioSurtido = @FolioSurtido 		
		
		Update D Set Cantidad = S.CantidadSurtida  
		From #tmpDetalle D  
		Inner Join #tmpDetalle_Surtido_Guardado S On ( D.IdClaveSSA = S.IdClaveSSA )		
			
	End 




		
-------------------------------------------- Obtener Surtido 		
	Select 'A' as Status, IdClaveSSA, sum(CantidadSurtida) as CantidadSurtida 
	Into #tmpDetalle_Surtido  	
	From #tmpDetalle_Surtido_Detallado
	Group by IdClaveSSA 
	
	Update S Set Status = 'F'
	From #tmpDetalle_Surtido S 
	Inner Join #tmpDetalle_Surtido_Detallado D On ( S.IdClaveSSA = D.IdClaveSSA ) 
	Where D.Status = 'F' 
	
----Select * from #tmpDetalle_Surtido_Detallado 	
----Select * from #tmpDetalle_Surtido 
	
-------------------------------------------- Obtener Surtido 		


	Update D Set CantidadSurtida = S.CantidadSurtida, ClaveTerminada = (case when S.Status = 'F' Then 1 Else 0 End) 
	From #tmpDetalle D  
	Inner Join #tmpDetalle_Surtido S On ( D.IdClaveSSA = S.IdClaveSSA )		
		
	Update D Set ClaveTerminada = 1 
	From #tmpDetalle D  
	Inner Join #tmpDetalle_Surtido S On ( D.IdClaveSSA = S.IdClaveSSA )		
	Where ClaveTerminada = 0 and D.CantidadSolicitada <= S.CantidadSurtida 
		
---		spp_Mtto_Pedidos_Cedis_CargarPedidoSurtido			
	




--------------------------------------------------- Asignar codigos de color 

		--0 as Antibiotico, 0 as Controlado, 0 as Refrigerado, 
		--0 as IdClasificacion, cast('' as varchar(100)) as Clasificacion, 0 as Procesado   

	Update D Set Procesado = 1, IdClasificacion = 3, Clasificacion = 'REFRIGERADO'
	From #tmpDetalle D (NoLock)
	Where Refrigerado = 1 and Procesado = 0 


	Update D Set Procesado = 1, IdClasificacion = 1, Clasificacion = 'CONTROLADO'
	From #tmpDetalle D (NoLock)
	Where Controlado = 1 and Procesado = 0 

	Update D Set Procesado = 1, IdClasificacion = 2, Clasificacion = 'ANTIBIOTICO'
	From #tmpDetalle D (NoLock)
	Where Antibiotico = 1 and Procesado = 0 

	Update D Set IdClasificacion = 99, Clasificacion = ''
	From #tmpDetalle D (NoLock)
	Where Procesado = 0 

--------------------------------------------------- Asignar codigos de color 

	

--------------------------------------------------- SALIDA FINAL 
	Select * From #tmpEncabezado 
	
	Select 
		ClaveSSA, IdClaveSSA, DescripcionClave, Existencia, ExistenciaAlmacenaje, 
		CantidadSolicitada, CantidadSurtida, Cantidad, Terminar, ClaveTerminada, ContenidoPaquete, 
		0 as CajasCompletas, 
		IdClasificacion, Clasificacion 
	From #tmpDetalle 
	Order By 
		IdClasificacion, 
		ClaveSSA 
		----- DescripcionClave -- ClaveSSA 
	
	Select * From #tmpEncabezadoSurtido  	


	--Select * from #Clasificacion 
	
--------------------------------------------------- SALIDA FINAL 


End 
Go--#SQL 

	
