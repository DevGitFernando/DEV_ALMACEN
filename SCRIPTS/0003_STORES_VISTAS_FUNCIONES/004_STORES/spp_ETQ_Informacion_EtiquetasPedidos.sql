------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_ETQ_Informacion_EtiquetasPedidos' and xType = 'P' )  
    Drop Proc spp_ETQ_Informacion_EtiquetasPedidos  
Go--#SQL 

--		Exec spp_ETQ_Informacion_EtiquetasPedidos  @IdEmpresa = '002', @IdEstado = '09', @IdFarmacia = '0023', @FolioSurtido = '00003026' 	

Create Proc spp_ETQ_Informacion_EtiquetasPedidos
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '1005', 
	@FolioSurtido varchar(8) = '2', 
	@EtiquetadoManual int = 1, @FolioETQ_Inicial int = 2, @FolioETQ_Final int = 5, 
	@TipoEtiqueta int = 1   
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 
	
Declare 
	@iNumeroDeCajas int  

	Set @IdEmpresa = right('00000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('00000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('00000000' + @IdFarmacia, 4) 
	Set @FolioSurtido = right('00000000' + @FolioSurtido, 8) 
	Set @iNumeroDeCajas = 0 

	--Select * 
	--From Pedidos_Cedis_Enc_Surtido E (NoLock) 
	--Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioSurtido = @FolioSurtido 
	
---		spp_ETQ_Informacion_EtiquetasPedidos  

-------------------------------------------------------------------------------------------------	

	Select 
		-- convert(varchar(100), getdate(), 103) as FechaImpresion_X, 	
		E.IdEmpresa, cast('' as varchar(500)) as Empresa, 
		E.IdEstado, cast('' as varchar(500)) as Estado, 
		E.IdFarmacia, cast('' as varchar(500)) as Farmacia, 
		PD.IdCliente, PD.IdSubCliente, 
		cast('' as varchar(20)) as IdBeneficiario, 
		cast('' as varchar(500)) as Beneficiario, 
		cast('' as varchar(20)) as IdTipoDispensacion, 
		cast('' as varchar(20)) as TipoDispensacion, 		
		PD.IdFarmaciaSolicita as IdFarmaciaPedido, cast('' as varchar(500)) as FarmaciaPedido,
		E.FolioPedido, E.FolioSurtido, E.FolioTransferenciaReferencia As FolioReferencia, 
		right(E.FolioTransferenciaReferencia, 8) as Folio, 
		PD.ReferenciaInterna, 
		0 as Tipo, 

		E.IdPersonalSurtido, 
		cast('' as varchar(10)) as IdPersonalValido, 


		cast('PROGRAMA' as varchar(500)) as TituloPrograma, 
		cast('REFERENCIA' as varchar(500)) as TituloReferencia, 
		cast('REFERENCIA' as varchar(500)) as TituloReferencia_ETQ, 			
		cast('SURTIÓ :' as varchar(500)) as TituloSurtio, 
		cast('VALIDÓ :' as varchar(500)) as TituloValido, 
		cast('FECHA :' as varchar(500)) as TituloFecha, 
		cast('' as varchar(500)) as LeyendaSurtio, 
		cast('' as varchar(500)) as LeyendaValido, 
		cast('X LEYENDA PROGRAMA' as varchar(500)) as LeyendaPrograma, 
		cast('X LEYENDA REFERENCIA' as varchar(500)) as LeyendaReferencia, 	
		--cast(0 as bit) as ContieneRefrigerados, 
		
		

		cast(max(cast(P.EsControlado as int)) as bit) as ContieneControlados, 
		cast(max(cast(P.EsAntibiotico as int)) as bit) as ContieneAntibioticos, 
		cast(max(cast(P.EsRefrigerado as int)) as bit)  as ContieneRefrigerados, 
		cast('CONTROLADOS : NO' as varchar(500)) as LeyendaControlados, 
		cast('ANTIBIÓTICOS : NO' as varchar(500)) as LeyendaAntibioticos, 
		cast('REFRIGERADOS : NO' as varchar(500)) as LeyendaRefrigerados, 


		cast('PO-AL-07-3.1' as varchar(500)) as LeyendaPNO, 
		getdate() as FechaImpresion, 
		D.IdCaja, 		
		cast((case when @TipoEtiqueta = 1 Then 'TARIMAS' else 'CAJAS' end)as varchar(500)) as TituloEtiquetas, 
		0 as EtiquetadoManual, 
		0 as NumeroDeEtiquetas, 
		0 as ETQ_Inicial, 
		0 as ETQ_Final, 
		cast('' as varchar(500)) as LeyandaEtiquetas, 
		identity(int, 1, 1) as Orden  
	Into #tmpEtiquetas_Pedidos 
	From Pedidos_Cedis_Enc_Surtido E (NoLock) 
	Inner Join Pedidos_Cedis_Enc PD (NoLock) 
		On ( E.IdEmpresa = PD.IdEmpresa and E.IdEstado = PD.IdEstado and E.IdFarmacia = PD.IdFarmacia and E.FolioPedido = PD.FolioPedido ) 
	Inner Join Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioSurtido = D.FolioSurtido ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.CodigoEAN = P.CodigoEAN ) 
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and E.FolioSurtido = @FolioSurtido 
	Group by 
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, 
		PD.IdCliente, PD.IdSubCliente, 
		E.FolioPedido, PD.IdFarmaciaSolicita, 
		E.FolioPedido, E.FolioSurtido, E.FolioTransferenciaReferencia, 
		PD.ReferenciaInterna, 
		E.IdPersonalSurtido, 
		D.IdCaja 		




	---------------------------- INFORMACIÓN COMPLEMENTARIA 
	Update E Set Empresa = D.Nombre -- 'INTERCONTINENTAL DE MEDICAMENTOS S.A. DE C.V.' -- D.Nombre 
	From #tmpEtiquetas_Pedidos E (NoLock) 
	Inner Join CatEmpresas D (NoLock) On ( E.IdEmpresa = D.IdEmpresa ) 

	Update E Set TituloReferencia = 'FOLIO DE TRANSFERENCIA', Tipo = 1  
	From #tmpEtiquetas_Pedidos E (NoLock) 
	Where FolioReferencia like '%TS%' 

	Update E Set TituloReferencia = 'FOLIO DE VENTA', Tipo = 2 
	From #tmpEtiquetas_Pedidos E (NoLock) 
	Where FolioReferencia like '%sv%' 


	Update E Set Estado = F.Estado, Farmacia = F.Farmacia 
	From #tmpEtiquetas_Pedidos E (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 

	Update E Set  FarmaciaPedido = F.Farmacia 
	From #tmpEtiquetas_Pedidos E (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmaciaPedido = F.IdFarmacia ) 

	Select @iNumeroDeCajas = count(*) From #tmpEtiquetas_Pedidos 
	Update E Set NumeroDeEtiquetas = @iNumeroDeCajas, 
		ETQ_Inicial = 1, 
		ETQ_Final = @iNumeroDeCajas, 
		LeyandaEtiquetas = cast(Orden as varchar(100)) + ' DE ' + cast(@iNumeroDeCajas as varchar(100)) 
	From #tmpEtiquetas_Pedidos E (NoLock) 


	----------------------- Excepcion   
	Update E Set 
		EtiquetadoManual = 1, 
		NumeroDeEtiquetas = @FolioETQ_Final, 
		ETQ_Inicial = @FolioETQ_Inicial, 
		--ETQ_Inicial = 1, 
		ETQ_Final = @FolioETQ_Final, 
		LeyandaEtiquetas = cast(@FolioETQ_Inicial as varchar(100)) + ' DE ' + cast(@FolioETQ_Final as varchar(100)) 
	From #tmpEtiquetas_Pedidos E (NoLock) 
	Where @EtiquetadoManual = 1 
	----------------------- Excepcion   


	----------------------------- SURTIO 

	---- VIEJO 
	Update E Set LeyendaSurtio = LeyendaSurtio + ' ' + P.Nombre 
	From #tmpEtiquetas_Pedidos E (NoLock) 
	Inner Join CatPersonalCEDIS P (NoLock) On ( E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonalSurtido = P.IdPersonal ) 
	---- VIEJO 

	Update E Set IdPersonalSurtido = 
		( 
			Select top 1 IdPersonal 
			From Pedidos_Cedis_Enc_Surtido_Atenciones P (NoLock) 
			Where E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.FolioSurtido = P.FolioSurtido and P.Status = 'S' 
			order by P.FechaRegistro desc 
		) 
	From #tmpEtiquetas_Pedidos E (NoLock) 


	Update E Set LeyendaSurtio = ' ' + P.NombreCompleto  
	From #tmpEtiquetas_Pedidos E (NoLock) 
	Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonalSurtido = P.IdPersonal ) 
	----------------------------- SURTIO 



----		spp_ETQ_Informacion_EtiquetasPedidos  


	----Update E Set LeyendaValido = ' ' + P.Nombre 
	----From #tmpEtiquetas_Pedidos E (NoLock) 
	----Inner Join CatPersonalCEDIS P (NoLock) On ( E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonalSurtido = P.IdPersonal ) 

	----------------------------- VALIDO 
	Update E Set IdPersonalValido = 
		( 
			Select top 1 IdPersonal 
			From Pedidos_Cedis_Enc_Surtido_Atenciones P (NoLock) 
			Where E.IdEmpresa = P.IdEmpresa and E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.FolioSurtido = P.FolioSurtido and P.Status = 'V' 
			order by P.FechaRegistro desc 
		) 
	From #tmpEtiquetas_Pedidos E (NoLock) 


	Update E Set LeyendaValido = ' ' + P.NombreCompleto  
	From #tmpEtiquetas_Pedidos E (NoLock) 
	Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonalValido = P.IdPersonal ) 
	----------------------------- VALIDO 


----		spp_ETQ_Informacion_EtiquetasPedidos  


	Update E Set 
		LeyendaControlados = (case when ContieneControlados = 1 then 'CONTROLADOS : SI' else 'CONTROLADOS : NO' end), 
		LeyendaAntibioticos = (case when ContieneAntibioticos = 1 then 'ANTIBIÓTICOS : SI' else 'ANTIBIÓTICOS : NO' end), 
		LeyendaRefrigerados = (case when ContieneRefrigerados = 1 then 'REFRIGERADOS : SI' else 'REFRIGERADOS : NO' end) 
	From #tmpEtiquetas_Pedidos E (NoLock) 

		--cast(0 as bit) as ContieneRefrigerados, 


	
	--------------------- Informacion de transferencias 
	If exists ( Select top 1 * from #tmpEtiquetas_Pedidos Where Tipo = 1 ) 
	Begin 
		Update E Set LeyendaPrograma = 'PEDIDOS ORDINARIOS', LeyendaReferencia = E.ReferenciaInterna   
		From #tmpEtiquetas_Pedidos E (NoLock) 
	End 


	--------------------- Informacion de ventas 
	If exists ( Select top 1 * from #tmpEtiquetas_Pedidos Where Tipo = 2 ) 
	Begin 
		Update E Set LeyendaPrograma = 'VENTAS DIRECTAS', LeyendaReferencia = I.NumReceta, IdBeneficiario = I.IdBeneficiario, IdTipoDispensacion = I.IdTipoDeDispensacion     
		From #tmpEtiquetas_Pedidos E (NoLock) 
		Inner Join VentasInformacionAdicional I (NoLock) On ( E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.IdFarmaciaPedido = I.IdFarmacia and E.FolioReferencia = 'SV' + I.FolioVenta ) 


		Update E Set Beneficiario = B.NombreCompleto, FarmaciaPedido = B.NombreCompleto, LeyendaReferencia = B.FolioReferencia 
		From #tmpEtiquetas_Pedidos E (NoLock) 
		Inner Join vw_Beneficiarios B (NoLock) On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente and E.IdBeneficiario = B.IdBeneficiario ) 


		Update E Set LeyendaPrograma = B.Descripcion 
		From #tmpEtiquetas_Pedidos E (NoLock) 
		Inner Join CatTiposDispensacion B (NoLock) On ( E.IdTipoDispensacion = B.IdTipoDeDispensacion ) 

	End 


	Update E Set FolioReferencia = @FolioSurtido, TituloReferencia = 'FOLIO DE SURTIDO' 
	From #tmpEtiquetas_Pedidos E (NoLock) 
	Where @TipoEtiqueta = 2   


	--Update E Set ContieneControlados = 0, ContieneRefrigerados = 0 
	--From #tmpEtiquetas_Pedidos E (NoLock) 
	--Inner Join vw_Productos_CodigoEAN (NoLock) On ( E.Codigo
	---------------------------- INFORMACIÓN COMPLEMENTARIA 	


---		spp_ETQ_Informacion_EtiquetasPedidos  

----------------------- SALIDA 
	Select * 
	From #tmpEtiquetas_Pedidos 

End 
Go--#SQL 



