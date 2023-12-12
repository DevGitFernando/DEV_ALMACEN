------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_GenerarRecibos__001_Ejecucion' and xType = 'P') 
    Drop Proc spp_INT_ND_GenerarRecibos__001_Ejecucion
Go--#SQL 
  
--		ExCB spp_INT_ND_GenerarRecibos__001_Ejecucion '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_GenerarRecibos__001_Ejecucion 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0011', 
    @CodigoCliente varchar(20) = '2181002', 
    @FechaInicial varchar(10) = '2016-01-05', @FechaFinal varchar(10) = '2016-01-05', 
    -- @FechaDeProceso varchar(10) = '2014-09-19', 
    @GUID varchar(100) = '', 
    @MostrarResultado int = 1    
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  

Declare 
	@IdSucursal varchar(20), 
	@SucursalNombre varchar(200), 	
	-- @IdFarmacia varchar(4), 
	@sFarmacia varchar(200),  
	@Folio varchar(8), 
	@sFCBha varchar(8), 
	@sConsCButivo varchar(3), 
	@sMensaje varchar(1000) 
	
	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	-- Set @IdFarmacia = right('0000' + @IdFarmacia, 4) 
	

---------------------- Intermedio 		
	Select  
		IdCliente, CodigoCliente, NombreCliente, 
		IdEstado, Estado, IdFarmacia, Farmacia, IdTipoUnidad, TipoDeUnidad, EsDeSurtimiento, Status 
	into #tmpClientes 
	From vw_INT_ND_Clientes F  	
	Where F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia 


	Select IdFarmacia, NombreFarmacia, EsAlmacen 
	into #tmpFarmacias 
	From CatFarmacias F 
	Where F.IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	
	
	Select @sFarmacia = NombreFarmacia 
	From #tmpFarmacias F 
	
	Select Top 1 @IdSucursal = IdSucursal, @SucursalNombre = SucursalNombre   
	From INT_ND_Sucursales F (NoLock) 
	Where F.IdEstado = @IdEstado 		
---------------------- Intermedio 		



	------Select @IdFarmacia = IdFarmacia, @sFarmacia = Farmacia  
	------From #tmpClientes F 
	------Where CodigoCliente = @CodigoCliente 


	----If @CodigoCliente = '*' or @CodigoCliente = '' 
	----Begin 
	----	Delete From #tmpClientes 
		
	----	Insert Into #tmpClientes  
	----	Select * 
	----	From vw_INT_ND_Clientes F 
	----	Where F.IdEstado = @IdEstado 
				
	----End 
---------------------- Intermedio 				

---		spp_INT_ND_GenerarRecibos__001_Ejecucion  	

	If Exists ( Select * From tempdb..Sysobjects (NoLock) Where Name like '#INT_ND__tmpRecibos%' and xType = 'U' ) 
	   Drop Table tempdb..#INT_ND__tmpRecibos  
	   
	Create Table #INT_ND__tmpRecibos 
	( 
		TipoRegistro smallint Not Null default 0,   ---		1 ==> Venta, 2 ==> Transferencias 
		IdEmpresa varchar(3) Not Null Default '', 
		IdEstado varchar(2) Not Null Default '', 
		IdFarmacia varchar(4) Not Null Default '', 
		
		CodigoCliente varchar(20) Not Null Default '', 
		Modulo varchar(2) Not Null Default '01', 
		Ticket varchar(20) Not Null Default '', 
		Origen varchar(1) Not Null Default '',	-- 1 = PACIENTE, 2 = COLECTIVO, 3 = TRASPASO, 4 = SUCURSAL 
		Folio varchar(20) Not Null Default '', 
		FechaDeRecibo datetime Not Null Default getdate(), 
		FechaDeDocumento datetime Not Null Default getdate(), 
		FechaRegistro varchar(10) Not Null Default '', 		
		
		IdFarmaciaEnvia varchar(4) Not Null Default '', 		
		IdHospitalOrigen varchar(20) Not Null Default '', 
		IdModuloOrigen varchar(2) Not Null Default '', 
		Motivo varchar(500) Not Null Default '', 		
		
		NombreTercero varchar(200) Not Null Default '43',   ---- 43 Sucursal Morelia  
		Motivo2 varchar(500) Not Null Default '', 
		
		Estatus varchar(1) Not Null Default '', 
		ClaveSSA_ND varchar(20) Not Null Default '', 
		ClaveSSA varchar(20) Not Null Default '', 
		ClaveSSA_Base varchar(20) Not Null Default '', 
		DescripcionClave varchar(max) Not Null Default '', 
		DescripcionClaveComercial varchar(500) Not Null Default '', 
		IdSubFarmacia varchar(2) Not Null Default '', 
		IdProducto varchar(30) Not Null Default '', 
		CodigoEAN varchar(30) Not Null Default '', 		
		CodigoEAN_Base varchar(30) Not Null Default '', 
		CodigoEAN_ND varchar(30) Not Null Default '', 		
		CodigoNadro varchar(30) Not Null Default '', 			
		CantidadPedida int Not Null Default 0, 
		CantidadRecibida int Not Null Default 0,
		ClaveLote varchar(20) Not Null Default '', 
		Caducidad datetime Not Null Default getdate(), 
		EstatusArticulo varchar(1) Not Null Default '', 
		CodigoRelacionado int Not null Default 0 
		
	) 

--	CodigoEAN_Base, CodigoEAN_ND

------------------------------- Temporal de Productos 
	Select * 
	Into #tmpProductos 
	From vw_Productos_CodigoEAN 
	


---		sp_listacolumnas #INT_ND__tmpRecibos 

---		spp_INT_ND_GenerarRecibos__001_Ejecucion  	





------------------------------------------  OBTENER LAS TRANSFERENCIAS DE ENTRADA DE LA FARMACIA Y EL PERIODO SOLICITADO	
	Insert Into #INT_ND__tmpRecibos 
		( 
			IdEmpresa, IdEstado, IdFarmacia, TipoRegistro, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeRecibo, FechaDeDocumento, FechaRegistro, 
			IdFarmaciaEnvia, IdHospitalOrigen, IdModuloOrigen, Motivo, NombreTercero, Motivo2, Estatus, IdSubFarmacia, 
			IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, 
			CantidadPedida, CantidadRecibida, ClaveLote, EstatusArticulo, CodigoRelacionado  
		 ) 
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
		1 as TipoRegistro, @CodigoCliente as CodigoCliente, '01' as Modulo, 
		right(V.FolioTransferencia, 8) as Ticket, 2 as Origen, V.FolioTransferencia as Folio, 
		V.FechaRegistro as FechaDeRecibo, V.FechaRegistro as FechaDeDocumento,  
		convert(varchar(10), V.FechaRegistro, 120) as FechaRegistro, 
		V.IdFarmaciaRecibe as IdFarmaciaEnvia, 
		'' as IdHospitalOrigen, 
		'' as IdModuloOrigen, '' as Motivo, 
		'' as NombreTercero, '' as Motivo2, '1' as Estatus, 
		L.IdSubFarmaciaRecibe, D.IdProducto, D.CodigoEAN, D.CodigoEAN as CodigoEAN_Base, '' as CodigoEAN_ND, 
		D.CantidadEnviada as CantidadPedida, D.CantidadEnviada as CantidadRecibida, L.ClaveLote, '1' as EstatusArticulo, 0 as CodigoRelacionado  
	From TransferenciasEnc V (NoLock) 
	Inner Join TransferenciasDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioTransferencia = D.FolioTransferencia ) 
	Inner Join TransferenciasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioTransferencia = L.FolioTransferencia 
		     and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia --  and L.IdSubFarmaciaRecibe = '05' 
		and convert(varchar(10), V.FechaRegistro, 120) between @FechaInicial and @FechaFinal  
		and V.TipoTransferencia = 'TE' 
		and Not Exists 
			( 
				Select * 
				From INT_ND_SubFarmaciasConsigna C (NoLock) 
				Where L.IdEstado = C.IdEstado and L.IdSubFarmaciaEnvia = C.IdSubFarmacia  		
			) 
	

---		spp_INT_ND_GenerarRecibos__001_Ejecucion  		
	
------------------------------------------  OBTENER LAS TRANSFERENCIAS DE ENTRADA DE LA FARMACIA Y EL PERIODO SOLICITADO	


	

------------------------------------------  OBTENER LOS PEDIDOS(SURTIDO DE NADRO) DE LA FARMACIA Y EL PERIODO SOLICITADO	
	Insert Into #INT_ND__tmpRecibos 
		( 
			IdEmpresa, IdEstado, IdFarmacia, TipoRegistro, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeRecibo, FechaDeDocumento, FechaRegistro, 
			IdFarmaciaEnvia, IdHospitalOrigen, IdModuloOrigen, Motivo, NombreTercero, Motivo2, Estatus, IdSubFarmacia, 
			IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, 
			CantidadPedida, CantidadRecibida, ClaveLote, EstatusArticulo, CodigoRelacionado  
		 ) 
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
		2 as TipoRegistro, @CodigoCliente as CodigoCliente, '01' as Modulo, 
		V.FolioPedido as Ticket, 1 as Origen, V.FolioPedido as Folio, 
		V.FechaRegistro as FechaDeRecibo, V.FechaRegistro as FechaDeDocumento,  
		convert(varchar(10), V.FechaRegistro, 120) as FechaRegistro, 
		'' as IdFarmaciaEnvia, 
		'' as IdHospitalOrigen, 
		'' as IdModuloOrigen, '' as Motivo, 
		'' as NombreTercero, '' as Motivo2, '1' as Estatus, 
		L.IdSubFarmacia, D.IdProducto, D.CodigoEAN, D.CodigoEAN as CodigoEAN_Base, '' as CodigoEAN_ND,
		L.CantidadRecibida as CantidadPedida, L.CantidadRecibida as CantidadRecibida, L.ClaveLote, '1' as EstatusArticulo, 0 as CodigoRelacionado  
	From INT_ND_PedidosEnc V (NoLock) 
	Inner Join INT_ND_PedidosDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioPedido = D.FolioPedido ) 
	Inner Join INT_ND_PedidosDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioPedido = L.FolioPedido 
		     and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )   
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
		and convert(varchar(10), V.FechaRegistro, 120) between @FechaInicial and @FechaFinal  
		and Not Exists 
			( 
				Select * 
				From INT_ND_SubFarmaciasConsigna C (NoLock) 
				Where L.IdEstado = C.IdEstado and L.IdSubFarmacia = C.IdSubFarmacia  		
			) 		
	

---		spp_INT_ND_GenerarRecibos__001_Ejecucion  		
	
------------------------------------------  OBTENER LOS PEDIDOS(SURTIDO DE NADRO) DE LA FARMACIA Y EL PERIODO SOLICITADO	
	
	
	
------------------------------------------  OBTENER LAS ENTRADAS POR CONSIGNACION DE LA FARMACIA Y EL PERIODO SOLICITADO	
	Insert Into #INT_ND__tmpRecibos 
		( 
			IdEmpresa, IdEstado, IdFarmacia, TipoRegistro, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeRecibo, FechaDeDocumento, FechaRegistro, 
			IdFarmaciaEnvia, IdHospitalOrigen, IdModuloOrigen, Motivo, NombreTercero, Motivo2, Estatus, IdSubFarmacia, 
			IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, 
			CantidadPedida, CantidadRecibida, ClaveLote, EstatusArticulo, CodigoRelacionado  
		 ) 
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
		3 as TipoRegistro, @CodigoCliente as CodigoCliente, '01' as Modulo, 
		V.FolioEntrada as Ticket, 1 as Origen, V.FolioEntrada as Folio, 
		V.FechaRegistro as FechaDeRecibo, V.FechaRegistro as FechaDeDocumento,  
		convert(varchar(10), V.FechaRegistro, 120) as FechaRegistro, 
		'' as IdFarmaciaEnvia, 
		'' as IdHospitalOrigen, 
		'' as IdModuloOrigen, '' as Motivo, 
		'' as NombreTercero, '' as Motivo2, '1' as Estatus, 
		L.IdSubFarmacia, D.IdProducto, D.CodigoEAN, D.CodigoEAN as CodigoEAN_Base, '' as CodigoEAN_ND, 
		L.CantidadRecibida as CantidadPedida, L.CantidadRecibida as CantidadRecibida, L.ClaveLote, '1' as EstatusArticulo, 0 as CodigoRelacionado  
	From EntradasEnc_Consignacion V (NoLock) 
	Inner Join EntradasDet_Consignacion D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioEntrada = D.FolioEntrada ) 
	Inner Join EntradasDet_Consignacion_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioEntrada = L.FolioEntrada 
		     and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )   
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia 
		and convert(varchar(10), V.FechaRegistro, 120) between @FechaInicial and @FechaFinal  
		and Not Exists 
			( 
				Select * 
				From INT_ND_SubFarmaciasConsigna C (NoLock) 
				Where L.IdEstado = C.IdEstado and L.IdSubFarmacia = C.IdSubFarmacia  		
			) 		
	

---		spp_INT_ND_GenerarRecibos__001_Ejecucion  		
	
------------------------------------------  OBTENER LAS ENTRADAS POR CONSIGNACION DE LA FARMACIA Y EL PERIODO SOLICITADO	
	
	
	
	
	
-------------------------------------------- Asignar , Clave SSA y Descripciones  	
	Update E Set CodigoEAN_ND = H.CodigoEAN_ND, CodigoRelacionado = 1 
	From #INT_ND__tmpRecibos E 
	Inner Join INT_ND_Productos H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) ) 
		
	Update E Set CodigoEAN_ND = H.CodigoEAN_ND, CodigoRelacionado = 2 
	From #INT_ND__tmpRecibos E 
	Inner Join INT_ND_CFG_CodigosEAN H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN, 30) )   	
	
	Update E Set ClaveSSA_ND = H.ClaveSSA_ND, CodigoEAN_ND = H.CodigoEAN_ND, DescripcionClaveComercial = H.Descripcion
	From #INT_ND__tmpRecibos E 
	Inner Join INT_ND_Productos H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN_ND, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) ) 
	
	Update E Set DescripcionClave = L.Descripcion --, Procesado = E.Procesado + 1 
	From #INT_ND__tmpRecibos E 
	inner Join INT_ND_Claves L (NoLock) On ( E.ClaveSSA_ND = L.ClaveSSA_ND ) 	
	
	

-------------------------------------------- Asignar Caducidades 
	Update I Set Caducidad = P.FechaCaducidad 
	From #INT_ND__tmpRecibos I (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes P (NoLock) 
		On ( I.IdEmpresa = P.IdEmpresa and I.IdEstado = P.IdEstado and I.IdFarmacia = P.IdFarmacia  
			 and I.IdSubFarmacia = P.IdSubFarmacia and I.IdProducto = P.IdProducto and I.CodigoEAN = P.CodigoEAN and I.ClaveLote = P.ClaveLote )  	
	
	

-------------------------------------------- Asignar informacion de la unidad destino de la transferencia  
	Update I Set IdHospitalOrigen = P.CodigoCliente, IdModuloOrigen = '01', Motivo = 'TRASPASO ENTRE UNIDADES' 
	From #INT_ND__tmpRecibos I (NoLock) 
	Inner Join #tmpClientes P (NoLock) On ( I.IdFarmaciaEnvia = P.IdFarmacia ) 
	Where TipoRegistro = 1 


-------------------------------------------- Asignar informacion del almacen destino de la transferencia  
	Update I Set IdHospitalOrigen = @IdSucursal, IdModuloOrigen = '01', Motivo = 'TRASPASO DE ALMACEN A UNIDAD'
	From #INT_ND__tmpRecibos I (NoLock) 
	-- Inner Join #tmpFarmacias P (NoLock) On ( I.IdFarmaciaEnvia = P.IdFarmacia and P.EsAlmacen = 1 )
	Where TipoRegistro in ( 2, 3 ) 

	----Update I Set IdHospitalOrigen = '', IdModuloOrigen = '01', NombreTercero = '43', Motivo2 = 'TRASPASO DE ALMACEN A UNIDAD'
	----From #INT_ND__tmpRecibos I (NoLock) 
	------ Inner Join #tmpFarmacias P (NoLock) On ( I.IdFarmaciaEnvia = P.IdFarmacia and P.EsAlmacen = 1 )
	----Where TipoRegistro in ( 3 )
	 

---		spp_INT_ND_GenerarRecibos__001_Ejecucion  	


-------------------------------------------------- GENERAR Y ALIMENTAR LA TABLA DE HISTORICO 
----	Drop table INT_ND__PRCS_Recibos 

	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND__PRCS_Recibos' and xType = 'U' ) 
	Begin 	
		Select 
			Identity(int, 1, 1) as Keyx, getdate() as FechaEjecucion, 
			TipoRegistro, IdEmpresa, IdEstado, IdFarmacia, @sFarmacia as Farmacia, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeRecibo, FechaDeDocumento, FechaRegistro, 
			IdFarmaciaEnvia, IdHospitalOrigen, IdModuloOrigen, Motivo, NombreTercero, Motivo2, Estatus, 
			ClaveSSA_ND, ClaveSSA, ClaveSSA_Base, DescripcionClave, DescripcionClaveComercial, IdSubFarmacia, 
			IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, CodigoNadro, CantidadPedida, CantidadRecibida, ClaveLote, Caducidad, EstatusArticulo, 
			CodigoRelacionado  
		Into INT_ND__PRCS_Recibos 
		From #INT_ND__tmpRecibos 
		Where 1 = 0 
		
-----	sp_listacolumnas INT_ND__PRCS_Recibos 		

	End 
	
	-- Quitar la informacion si ya existe 
	Delete From INT_ND__PRCS_Recibos 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and convert(varchar(10), FechaDeRecibo, 120) between @FechaInicial and @FechaFinal 
	
	Insert Into INT_ND__PRCS_Recibos 
	( 
		FechaEjecucion,  
			TipoRegistro, IdEmpresa, IdEstado, IdFarmacia, Farmacia, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeRecibo, FechaDeDocumento, FechaRegistro, 
			IdFarmaciaEnvia, IdHospitalOrigen, IdModuloOrigen, Motivo, NombreTercero, Motivo2, Estatus, 
			ClaveSSA_ND, ClaveSSA, ClaveSSA_Base, DescripcionClave, DescripcionClaveComercial, IdSubFarmacia, 
			IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, CodigoNadro, CantidadPedida, CantidadRecibida, ClaveLote, Caducidad, EstatusArticulo, CodigoRelacionado  
	) 
	Select 
		getdate() as FechaEjecucion, 
			TipoRegistro, IdEmpresa, IdEstado, IdFarmacia, @sFarmacia as Farmacia, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeRecibo, FechaDeDocumento, FechaRegistro, 
			IdFarmaciaEnvia, IdHospitalOrigen, IdModuloOrigen, Motivo, NombreTercero, Motivo2, Estatus, 
			ClaveSSA_ND, ClaveSSA, ClaveSSA_Base, DescripcionClave, DescripcionClaveComercial, IdSubFarmacia, 
			IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, CodigoNadro, CantidadPedida, CantidadRecibida, ClaveLote, Caducidad, EstatusArticulo, CodigoRelacionado  
	From #INT_ND__tmpRecibos 
	Order By TipoRegistro, DescripcionClave, Origen  	
-------------------------------------------------- GENERAR Y ALIMENTAR LA TABLA DE HISTORICO 



------------------------- SALIDA FINAL 
	If @MostrarResultado = 1 
	Begin 
		Select 
			@IdFarmacia as IdFarmacia, @sFarmacia as Farmacia, 
			TipoRegistro, CodigoCliente, Modulo, 
			(cast(TipoRegistro as varchar) + Ticket) as Ticket, Origen, Folio, 
			replace(convert(varchar(10), FechaDeRecibo, 120), '-', '') as FechaDeRecibo, 
			replace(convert(varchar(10), FechaDeDocumento, 120), '-', '') as FechaDeDocumento, 		
			IdHospitalOrigen, IdModuloOrigen, Motivo, NombreTercero, Motivo2, Estatus, 
			ClaveSSA_ND, ClaveSSA, ClaveSSA_Base, DescripcionClave, DescripcionClaveComercial as DescripcionComercial, 
			CodigoEAN_Base, CodigoEAN_ND as CodigoEAN, CodigoNadro, 
			CantidadPedida, CantidadRecibida, ClaveLote, 
			replace(convert(varchar(10), Caducidad, 120), '-', '') as Caducidad, 
			EstatusArticulo, 
			replace(convert(varchar(10), FechaDeRecibo, 120), '-', '') as FechaGeneracion  	
		From #INT_ND__tmpRecibos 
		-- Where TipoRegistro = 2  
		Order By TipoRegistro, Ticket 
	End 

------------------------- SALIDA FINAL 
	
---		spp_INT_ND_GenerarRecibos__001_Ejecucion  	

	
End  
Go--#SQL 

