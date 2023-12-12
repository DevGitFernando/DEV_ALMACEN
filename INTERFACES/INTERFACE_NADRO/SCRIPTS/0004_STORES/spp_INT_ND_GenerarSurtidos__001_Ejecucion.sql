------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_INT_ND_GenerarSurtidos__001_Ejecucion' and xType = 'P' ) 
    Drop Proc spp_INT_ND_GenerarSurtidos__001_Ejecucion 
Go--#SQL 
  
--		ExCB spp_INT_ND_GenerarSurtidos__001_Ejecucion '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_GenerarSurtidos__001_Ejecucion 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0011', 
    @CodigoCliente varchar(20) = '2181002', 
    @FechaInicial varchar(10) = '2015-01-05', @FechaFinal varchar(10) = '2015-01-05', 
    -- @FechaDeProceso varchar(10) = '2014-09-19', 
    @GUID varchar(100) = '', 
    @MostrarResultado int = 1   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD  

Declare 
	@IdSucursal varchar(20), 
	@SucursalNombre varchar(200), 	
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



	----Select @IdFarmacia = IdFarmacia, @sFarmacia = Farmacia  
	----From #tmpClientes F 
	----Where CodigoCliente = @CodigoCliente 

	----If @CodigoCliente = '*' or @CodigoCliente = ''
	----Begin 
	----	Delete From #tmpClientes 
		
	----	Insert Into #tmpClientes  
	----	Select * 
	----	From vw_INT_ND_Clientes F 
	----	Where F.IdEstado = @IdEstado 
				
	----End 
---------------------- Intermedio 				

---		spp_INT_ND_GenerarSurtidos__001_Ejecucion  	

	If Exists ( Select * From tempdb..Sysobjects (NoLock) Where Name like '#INT_ND__tmpSurtidos%' and xType = 'U' ) 
	   Drop Table tempdb..#INT_ND__tmpSurtidos  

	Create Table #INT_ND__tmpSurtidos 
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
		FechaDeSurtido datetime Not Null Default getdate(), 
		FechaDeDocumento datetime Not Null Default getdate(), 
		FechaRegistro varchar(10) Not Null Default '', 
		
		IdCliente varchar(4) Not Null Default '', 
		IdSubCliente varchar(4) Not Null Default '', 
		IdPrograma varchar(4) Not Null Default '', 						
		IdSubPrograma varchar(4) Not Null Default '', 
		
		TipoDeSeguro varchar(100) Not Null Default '', 
		IdDiagnostico varchar(6) Not Null Default '', 
		Diagnostico varchar(1000) Not Null Default '', 
		
		IdBeneficiario varchar(8) Not Null Default '', 		
		NombreAfiliado varchar(200) Not Null Default '', 			
		NumeroDePoliza varchar(50) Not Null Default '', 
		
		IdMedico varchar(10) Not Null Default '', 				
		NombreMedico varchar(200) Not Null Default '', 		
		CedulaMedico varchar(50) Not Null Default '', 
		
		IdTipoDeDispensacion varchar(2) Not Null, 
		IdServicio varchar(3) Not Null Default '', 
		IdArea varchar(3) Not Null Default '', 		
		Area varchar(100) Not Null Default '', 
		
		IdFarmaciaRecibe varchar(4) Not Null Default '', 		
		IdHospitalDestino varchar(20) Not Null Default '', 
		IdModuloDestino varchar(2) Not Null Default '', 
		Motivo varchar(500) Not Null Default '', 		
		IdSucursalDestino varchar(2) Not Null Default '00',   ---- 43 Sucursal Morelia  
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
		CantidadPedida int Not Null Default 0, 
		CantidadSurtida int Not Null Default 0,
		ClaveLote varchar(20) Not Null Default '', 
		Caducidad datetime Not Null Default getdate(), 
		EstatusArticulo varchar(1) Not Null Default '', 
		CodigoRelacionado int Not Null Default 0 
		
	) 

------------------------------- Temporal de Productos 
	Select * 
	Into #tmpProductos 
	From vw_Productos_CodigoEAN 
	


---		sp_listacolumnas #INT_ND__tmpSurtidos 

---		spp_INT_ND_GenerarSurtidos__001_Ejecucion  	

------------------------------------------  OBTENER LAS VENTAS DE LA FARMACIA Y EL PERIODO SOLICITADO	
/* 	
	TipoRegistro, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeSurtido, FechaDeDocumento, 
	TipoDeSeguro, Diagnostico, NumeroDePoliza, NombreAfiliado, CedulaMedico, NombreMedico, Area, 
	
	IdHospitalDestino, IdModuloDestino, Motivo, IdSucursalDestino, Motivo2, 
	Estatus, 
	ClaveSSA, DescripcionClave, DescripcionClaveComercial, CodigoEAN, CantidadPedida, CantidadSurtida, ClaveLote, Caducidad
*/ 

------------------------ Obtener las ventas 
	Insert Into #INT_ND__tmpSurtidos 
		( 
			IdEmpresa, IdEstado, IdFarmacia, TipoRegistro, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeSurtido, FechaDeDocumento, FechaRegistro, 
			IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, IdBeneficiario, IdMedico, IdDiagnostico, IdTipoDeDispensacion, IdServicio, IdArea, 
			IdHospitalDestino, IdModuloDestino, Motivo, IdSucursalDestino, Motivo2, Estatus, IdSubFarmacia, 
			IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, 
			CantidadPedida, CantidadSurtida, ClaveLote, EstatusArticulo, CodigoRelacionado  
		 ) 
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
		1 as TipoRegistro, @CodigoCliente as CodigoCliente, '01' as Modulo, 
		V.FolioVenta as Ticket, 1 as Origen, IA.NumReceta as Folio, 
		V.FechaRegistro as FechaDeSurtido, V.FechaRegistro as FechaDeDocumento,  
		convert(varchar(10), V.FechaRegistro, 120) as FechaRegistro, 
		V.IdCliente, V.IdSubCliente, V.IdPrograma, V.IdSubPrograma, 
		IA.IdBeneficiario, IA.IdMedico, IA.IdDiagnostico, IA.IdTipoDeDispensacion, IA.IdServicio, IA.IdArea, 
		'' as IdHospitalDestino, '' as IdModuloDestino, '' as Motivo, 
		'' as IdSucursalDestino, '' as Motivo2, '1' as Estatus, 
		L.IdSubFarmacia, D.IdProducto, D.CodigoEAN, D.CodigoEAN as CodigoEAN_Base, '' as CodigoEAN_ND,
		L.CantidadVendida as CantidadPedida, L.CantidadVendida as CantidadSurtida, L.ClaveLote, '1' as EstatusArticulo, 0 as CodigoRelacionado  
	From VentasEnc V (NoLock) 
	Inner Join VentasDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioVenta = D.FolioVenta ) 
	Inner Join VentasDet_Lotes L (NoLock) 
	-- Inner Join #tmpProductos P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = L.CodigoEAN )  
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
		     and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Left Join VentasInformacionAdicional IA (NoLock) 
		On ( V.IdEmpresa = IA.IdEmpresa and V.IdEstado = IA.IdEstado and V.IdFarmacia = IA.IdFarmacia and V.FolioVenta = IA.FolioVenta )  	
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia -- and L.IdSubFarmacia = '05'
		and convert(varchar(10), V.FechaRegistro, 120) between @FechaInicial and @FechaFinal 
		and Not Exists 
			( 
				Select * 
				From INT_ND_SubFarmaciasConsigna C (NoLock) 
				Where L.IdEstado = C.IdEstado and L.IdSubFarmacia = C.IdSubFarmacia  		
			) 
		
		
--	select * from #INT_ND__tmpSurtidos 	
------------------------ Obtener las ventas 	
------------------------------------------  OBTENER LAS VENTAS DE LA FARMACIA Y EL PERIODO SOLICITADO	



------------------------------------------  OBTENER LAS TRANSFERENCIAS DE LA FARMACIA Y EL PERIODO SOLICITADO	
	Insert Into #INT_ND__tmpSurtidos 
		( 
			IdEmpresa, IdEstado, IdFarmacia, TipoRegistro, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeSurtido, FechaDeDocumento, FechaRegistro, 
			IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, IdBeneficiario, IdMedico, IdDiagnostico, IdTipoDeDispensacion, IdServicio, IdArea, 
			IdFarmaciaRecibe, IdHospitalDestino, IdModuloDestino, Motivo, IdSucursalDestino, Motivo2, Estatus, IdSubFarmacia, 
			IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, 
			CantidadPedida, CantidadSurtida, ClaveLote, EstatusArticulo, CodigoRelacionado  
		 ) 
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, 
		2 as TipoRegistro, @CodigoCliente as CodigoCliente, '01' as Modulo, 
		right(V.FolioTransferencia, 8) as Ticket, 1 as Origen, V.FolioTransferencia as Folio, 
		V.FechaRegistro as FechaDeSurtido, V.FechaRegistro as FechaDeDocumento,  
		convert(varchar(10), V.FechaRegistro, 120) as FechaRegistro, 
		'' as IdCliente, '' as IdSubCliente, '' as IdPrograma, '' as IdSubPrograma, 
		'' as IdBeneficiario, '' as IdMedico, '' as IdDiagnostico, '' as IdTipoDeDispensacion, '' as IdServicio, '' as IdArea, 
		
		V.IdFarmaciaRecibe, 
		'' as IdHospitalDestino, 
		'' as IdModuloDestino, '' as Motivo, 
		'' as IdSucursalDestino, '' as Motivo2, '1' as Estatus, 
		L.IdSubFarmaciaRecibe, D.IdProducto, D.CodigoEAN, D.CodigoEAN as CodigoEAN_Base, '' as CodigoEAN_ND, 
		L.CantidadEnviada as CantidadPedida, L.CantidadEnviada as CantidadSurtida, L.ClaveLote, '1' as EstatusArticulo, 0 as CodigoRelacionado  
	From TransferenciasEnc V (NoLock) 
	Inner Join TransferenciasDet D (NoLock) 
		On ( V.IdEmpresa = D.IdEmpresa and V.IdEstado = D.IdEstado and V.IdFarmacia = D.IdFarmacia and V.FolioTransferencia = D.FolioTransferencia ) 
	Inner Join TransferenciasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioTransferencia = L.FolioTransferencia 
		     and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )  
	Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia -- and L.IdSubFarmaciaRecibe = '05' 
		and convert(varchar(10), V.FechaRegistro, 120) between @FechaInicial and @FechaFinal  
		and V.TipoTransferencia = 'TS' 
		and Not Exists 
			( 
				Select * 
				From INT_ND_SubFarmaciasConsigna C (NoLock) 
				Where L.IdEstado = C.IdEstado and L.IdSubFarmaciaEnvia = C.IdSubFarmacia  		
			) 	

---		spp_INT_ND_GenerarSurtidos__001_Ejecucion  		
	
------------------------------------------  OBTENER LAS TRANSFERENCIAS DE LA FARMACIA Y EL PERIODO SOLICITADO	
	
	
	
	
	
-------------------------------------------- Asignar Clave SSA y Descripciones 
	----Update I Set ClaveSSA = P.ClaveSSA, ClaveSSA_Base = P.ClaveSSA_Base, DescripcionClave = P.DescripcionClave, DescripcionClaveComercial = P.DescripcionCortaClave 
	----From #INT_ND__tmpSurtidos I (NoLock) 
	----Inner Join #tmpProductos P (NoLock) On ( I.IdProducto = P.IdProducto and I.CodigoEAN = P.CodigoEAN )  


	----Update E Set ClaveSSA_ND = '', DescripcionClave = '', DescripcionClaveComercial = '' 
	----From #INT_ND__tmpSurtidos E 
	
	Update E Set CodigoEAN_ND = H.CodigoEAN_ND, CodigoRelacionado = 1 
	From #INT_ND__tmpSurtidos E 
	Inner Join INT_ND_Productos H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) ) 
		
	Update E Set CodigoEAN_ND = H.CodigoEAN_ND, CodigoRelacionado = 2 
	From #INT_ND__tmpSurtidos E 
	Inner Join INT_ND_CFG_CodigosEAN H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN, 30) )   	
	
	Update E Set ClaveSSA_ND = H.ClaveSSA_ND, CodigoEAN_ND = H.CodigoEAN_ND, DescripcionClaveComercial = H.Descripcion
	From #INT_ND__tmpSurtidos E 
	Inner Join INT_ND_Productos H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN_ND, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) ) 
	
	Update E Set DescripcionClave = L.Descripcion --, Procesado = E.Procesado + 1 
	From #INT_ND__tmpSurtidos E 
	inner Join INT_ND_Claves L (NoLock) On ( E.ClaveSSA_ND = L.ClaveSSA_ND ) 		


-------------------------------------------- Asignar Caducidades 
	Update I Set Caducidad = P.FechaCaducidad 
	From #INT_ND__tmpSurtidos I (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes P (NoLock) 
		On ( I.IdEmpresa = P.IdEmpresa and I.IdEstado = P.IdEstado and I.IdFarmacia = P.IdFarmacia  
			 and I.IdSubFarmacia = P.IdSubFarmacia and I.IdProducto = P.IdProducto and I.CodigoEAN = P.CodigoEAN and I.ClaveLote = P.ClaveLote )  	
	
	
--------------------------------------------  Asignar informacion de diagnostico 
	Update I Set Diagnostico = P.Descripcion 
	From #INT_ND__tmpSurtidos I (NoLock) 
	Inner Join CatCIE10_Diagnosticos P (NoLock) On ( I.IdDiagnostico = P.ClaveDiagnostico ) 
	Where TipoRegistro = 1 
		
---		spp_INT_ND_GenerarSurtidos__001_Ejecucion  			
	
	
--------------------------------------------  Asignar informacion del beneficiario 
	Update I Set 
		NumeroDePoliza = FolioReferencia, 
		NombreAfiliado = P.ApPaterno + '' + (case when P.ApMaterno <> '' then ' ' + P.ApMaterno + '  ' else ' ' end) + '' + P.Nombre
	From #INT_ND__tmpSurtidos I (NoLock) 
	Inner Join CatBeneficiarios P (NoLock) 
		On ( I.IdEstado = P.IdEstado and I.IdFarmacia = P.IdFarmacia 
			and I.IdCliente = P.IdCliente and I.IdSubCliente = P.IdSubCliente and I.IdBeneficiario = P.IdBeneficiario )  
		Where TipoRegistro = 1 
	
	
-------------------------------------------- Asignar informacion del médico 
	Update I Set 
		CedulaMedico = NumCedula,  
		NombreMedico = P.ApPaterno + '' + (case when P.ApMaterno <> '' then ' ' + P.ApMaterno + '  ' else ' ' end) + '' + P.Nombre
	From #INT_ND__tmpSurtidos I (NoLock) 
	Inner Join CatMedicos P (NoLock) On ( I.IdEstado = P.IdEstado and I.IdFarmacia = P.IdFarmacia and I.IdMedico = P.IdMedico ) 
	Where TipoRegistro = 1 		


-------------------------------------------- Asignar informacion del origen de receta  
	Update I Set Origen = 2  
	From #INT_ND__tmpSurtidos I (NoLock) 
	Where TipoRegistro = 1 and IdTipoDeDispensacion Not in ( '00', '01', '02', '06' ) 



-------------------------------------------- Asignar informacion del programa 
	Update I Set TipoDeSeguro = P.Descripcion   
	From #INT_ND__tmpSurtidos I (NoLock) 
	Inner Join CatProgramas P (NoLock) On ( I.IdPrograma = P.IdPrograma )
	Where TipoRegistro = 1 



-------------------------------------------- Asignar informacion de la unidad destino de la transferencia  
	Update I Set IdHospitalDestino = P.CodigoCliente, IdModuloDestino = '01', Motivo = 'TRASPASO ENTRE UNIDADES' 
	From #INT_ND__tmpSurtidos I (NoLock) 
	Inner Join #tmpClientes P (NoLock) On ( I.IdFarmaciaRecibe = P.IdFarmacia )
	Where TipoRegistro = 2 


-------------------------------------------- Asignar informacion del almacen destino de la transferencia  
	Update I Set IdHospitalDestino = @IdSucursal, IdModuloDestino = '01', IdSucursalDestino = @IdSucursal, Motivo2 = 'TRASPASO DE UNIDAD A ALMACEN' 
	From #INT_ND__tmpSurtidos I (NoLock) 
	Inner Join #tmpFarmacias P (NoLock) On ( I.IdFarmaciaRecibe = P.IdFarmacia and P.EsAlmacen = 1 )
	Where TipoRegistro = 2 
	
---		spp_INT_ND_GenerarSurtidos__001_Ejecucion  	




-------------------------------------------------- GENERAR Y ALIMENTAR LA TABLA DE HISTORICO 
--	Drop table INT_ND__PRCS_Surtidos 
	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND__PRCS_Surtidos' and xType = 'U' ) 
	Begin 	
		Select Top 0 
			Identity(int, 1, 1) as Keyx, getdate() as FechaEjecucion, 
			TipoRegistro, IdEmpresa, IdEstado, IdFarmacia, @sFarmacia as Farmacia, CodigoCliente, Modulo, Ticket, Origen, Folio, 
			FechaDeSurtido, FechaDeDocumento, FechaRegistro, 
			IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, TipoDeSeguro, IdDiagnostico, Diagnostico, IdBeneficiario, NombreAfiliado, 
			NumeroDePoliza, IdMedico, NombreMedico, CedulaMedico, IdTipoDeDispensacion, IdServicio, IdArea, Area, 
			IdFarmaciaRecibe, IdHospitalDestino, IdModuloDestino, Motivo, IdSucursalDestino, Motivo2, Estatus, 
			ClaveSSA_ND, ClaveSSA, ClaveSSA_Base, DescripcionClave, DescripcionClaveComercial, IdSubFarmacia, 
			IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, CantidadPedida, CantidadSurtida, ClaveLote, Caducidad, EstatusArticulo 
		Into INT_ND__PRCS_Surtidos 
		From #INT_ND__tmpSurtidos 
		Where 1 = 0 
		
---	sp_listacolumnas INT_ND__PRCS_Surtidos 		

	End 
	
	---- Quitar la informacion si ya existe 
	Delete From INT_ND__PRCS_Surtidos 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and convert(varchar(10), FechaDeSurtido, 120) between @FechaInicial and @FechaFinal 
	
	Insert Into INT_ND__PRCS_Surtidos 
	( 
		FechaEjecucion,  
		TipoRegistro, IdEmpresa, IdEstado, IdFarmacia, Farmacia, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeSurtido, FechaDeDocumento, FechaRegistro, 
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, TipoDeSeguro, IdDiagnostico, Diagnostico, IdBeneficiario, NombreAfiliado, 
		NumeroDePoliza, IdMedico, NombreMedico, CedulaMedico, IdTipoDeDispensacion, IdServicio, IdArea, Area, 
		IdFarmaciaRecibe, IdHospitalDestino, IdModuloDestino, Motivo, IdSucursalDestino, Motivo2, Estatus, 
		ClaveSSA_ND, ClaveSSA, ClaveSSA_Base, DescripcionClave, DescripcionClaveComercial, IdSubFarmacia, 
		IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, CantidadPedida, CantidadSurtida, ClaveLote, Caducidad, EstatusArticulo 	 	
	) 
	Select 
		getdate() as FechaEjecucion, 
		TipoRegistro, IdEmpresa, IdEstado, IdFarmacia, @sFarmacia as Farmacia, CodigoCliente, Modulo, Ticket, Origen, Folio, FechaDeSurtido, FechaDeDocumento, FechaRegistro,  
		IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, TipoDeSeguro, IdDiagnostico, Diagnostico, IdBeneficiario, NombreAfiliado, 
		NumeroDePoliza, IdMedico, NombreMedico, CedulaMedico, IdTipoDeDispensacion, IdServicio, IdArea, Area, 
		IdFarmaciaRecibe, IdHospitalDestino, IdModuloDestino, Motivo, IdSucursalDestino, Motivo2, Estatus, 
		ClaveSSA_ND, ClaveSSA, ClaveSSA_Base, DescripcionClave, DescripcionClaveComercial, IdSubFarmacia, 
		IdProducto, CodigoEAN, CodigoEAN_Base, CodigoEAN_ND, CantidadPedida, CantidadSurtida, ClaveLote, Caducidad, EstatusArticulo 	 
	From #INT_ND__tmpSurtidos 
	Order By TipoRegistro, DescripcionClave, Origen  	
-------------------------------------------------- GENERAR Y ALIMENTAR LA TABLA DE HISTORICO 



------------------------- SALIDA FINAL 
	If @MostrarResultado = 1 
	Begin 
		Select 
			@IdFarmacia as IdFarmacia, @sFarmacia as Farmacia,   
			TipoRegistro, CodigoCliente, Modulo, 
			(cast(TipoRegistro as varchar) + Ticket) as Ticket, Origen, Folio, 
			replace(convert(varchar(10), FechaDeSurtido, 120), '-', '') as FechaDeSurtido, 
			replace(convert(varchar(10), FechaDeDocumento, 120), '-', '') as FechaDeDocumento, 
			TipoDeSeguro, Diagnostico, NumeroDePoliza, NombreAfiliado, CedulaMedico, NombreMedico, Area, 	
			IdHospitalDestino, IdModuloDestino, Motivo, IdSucursalDestino, Motivo2, Estatus, 
			ClaveSSA_ND, ClaveSSA, ClaveSSA_Base, DescripcionClave, DescripcionClaveComercial as DescripcionComercial, 
			CodigoEAN_Base, CodigoEAN_ND as CodigoEAN, 
			CantidadPedida, CantidadSurtida, ClaveLote, 
			replace(convert(varchar(10), Caducidad, 120), '-', '') as Caducidad, 
			EstatusArticulo, 
			replace(convert(varchar(10), FechaDeSurtido, 120), '-', '') as FechaGeneracion 
		From #INT_ND__tmpSurtidos 
		--Where ClaveSSA_ND <> ''   
		Order By TipoRegistro, Ticket 
	End 

------------------------- SALIDA FINAL 
	
---		spp_INT_ND_GenerarSurtidos__001_Ejecucion  	

	
End  
Go--#SQL 

