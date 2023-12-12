-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM__02_Transferencias' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM__02_Transferencias
Go--#SQL 

Create Proc spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM__02_Transferencias 
( 
	@IdEmpresa varchar(3) = '4', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '104',  
	@Folio varchar(8) = '267', 
	@TipoDeProceso int = 2  
) 
With Encryption 
As 
Begin 
--Set NoCount On 
Set DateFormat YMD 
	
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


	Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000000000' + @IdFarmacia, 4) 
	--Set @FolioVenta = right('000000000000' + @FolioVenta, 8) 
	

	----------------------------- Generar la tabla de control 
	Select *, FolioTransferencia as FolioReferencia, FechaRegistro as FechaReferencia   
	Into #tmp__FoliosDeVenta 
	From TransferenciasEnc D (NoLock) 
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioTransferencia = 'TS' + right('000000000000' + @Folio, 8)  


	If @TipoDeProceso = 2 
	Begin 

		Delete From #tmp__FoliosDeVenta 

		Insert Into #tmp__FoliosDeVenta
		Select D.*, P.FolioPedido as FolioReferencia, P.FechaRegistro as FechaReferencia   		 
		From TransferenciasEnc D (NoLock) 
		Inner Join Pedidos_Cedis_Enc_Surtido P (NoLock) 
			On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdEstado = P.IdEstado and D.FolioTransferencia = P.FolioTransferenciaReferencia ) 
		Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia and P.FolioPedido = right('000000000000' + @Folio, 6)  

		Update V Set FechaReferencia = ( select min(FechaReferencia) From #tmp__FoliosDeVenta ) 
		From #tmp__FoliosDeVenta V 
		
	End 



	--		spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM__02_Transferencias  

	--------------------------------- INFORMACION  
	Select 
		distinct 
		V.FolioReferencia, V.FechaReferencia, 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioTransferencia, 
		'0042' as IdCliente, '0003' as IdSubCliente, '' as IdPrograma, '' as IdSubPrograma, 
		'' as IdBeneficiario, 
		P.ClaveSSA + '' as Mascara, 
		P.ClaveSSA, 
		P.ClaveSSA as ClaveSSA_Base, 
		P.DescripcionClave, 

		replace(VL.ClaveLote, '*', '') as ClaveLote,   
		convert(varchar(7), L.FechaCaducidad, 120) + '-'+ right('0000' + cast(dbo.fg_NumeroDiasFecha(L.FechaCaducidad) as varchar(2)), 2) as Caducidad, 

		--'' as ClaveLote, 
		--'' as Caducidad, 

		0 as EsRelacionado, 

		P.ContenidoPaquete_ClaveSSA, 
		P.ContenidoPaquete_ClaveSSA as ContenidoPaquete_Licitado, 
		P.ContenidoPaquete_ClaveSSA as ContenidoPaquete_Asignado, 
		1 as Factor, 

		P.EsRefrigerado, 
		P.TipoDeClave, P.TipoDeClaveDescripcion, 

		right(V.FolioTransferencia, 4) + '-' + cast(DATEPART(year, v.FechaRegistro) as varchar(10)) as Folio,  
		convert(varchar(10), (case when @TipoDeProceso = 2 then V.FechaReferencia else v.FechaRegistro end), 112) as FechaCreacion, 

		--B.FolioReferenciaAuxiliar as CentroDeCostos, 
		--'JS' + cast(cast(B.IdJurisdiccion as int) as varchar(100)) as Jurisdiccion, 
		'' as CentroDeCostos,
		'' as Jurisdiccion, 

		cast((case when P.TipoDeClave = '02' then 'MD-' else 'MC-' end) as varchar(20)) as Prefijo_TipoClave, 
		cast('' as varchar(500)) as GUID, 
		0 as Procesado, 

		VL.IdProducto, VL.CodigoEAN, 
		cast(VL.CantidadEnviada as int) as Cantidad_Base,    
		cast(VL.CantidadEnviada as int) as Cantidad   
	Into #tmp_Detalles 
	From TransferenciasDet D (NoLock) 
	Inner Join TransferenciasDet_Lotes VL (NoLock) On ( D.IdEmpresa = VL.IdEmpresa and D.IdEstado = VL.IdEstado and D.IdEstado = VL.IdEstado and D.FolioTransferencia = VL.FolioTransferencia ) 
	Inner Join #tmp__FoliosDeVenta V (NoLock) On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdEstado = V.IdEstado and D.FolioTransferencia = V.FolioTransferencia ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( VL.IdEmpresa = L.IdEmpresa and VL.IdEstado = L.IdEstado and VL.IdFarmacia = L.IdFarmacia and VL.IdSubFarmaciaEnvia = L.IdSubFarmacia
			and VL.IdProducto = L.IdProducto and VL.CodigoEAN = L.CodigoEAN and VL.ClaveLote = L.ClaveLote and VL.SKU = L.SKU )
	--Inner Join VentasInformacionAdicional I (NoLock) On ( D.IdEmpresa = I.IdEmpresa and D.IdEstado = I.IdEstado and D.IdEstado = I.IdEstado and D.FolioTransferencia = I.FolioVenta ) 
	--Inner Join CatBeneficiarios B (NoLock) On ( V.IdEstado = B.IdEstado and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and I.IdBeneficiario = B.IdBeneficiario ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) 


	-- Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia and D.FolioVenta = @FolioVenta 
	--------------------------------- INFORMACION  


	--------------------------------- RELACION DE CLAVES 
	Update V Set ClaveSSA = M.ClaveSSA --, EsRelacionado = 1 
	From #tmp_Detalles V (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves M (NoLock) 
		On ( V.IdEstado = M.IdEstado and V.IdCliente = M.IdCliente and V.IdSubCliente = M.IdSubCliente and V.ClaveSSA = M.ClaveSSA_Relacionada )  
	--------------------------------- RELACION DE CLAVES 

	--------------------------------- MASCARAS 
	Update V Set Mascara = M.Mascara, EsRelacionado = 1   
	From #tmp_Detalles V (NoLock) 
	Inner Join vw_ClaveSSA_Mascara M (NoLock) 
		On ( V.IdEstado = M.IdEstado and V.IdCliente = M.IdCliente and V.IdSubCliente = M.IdSubCliente and V.ClaveSSA = M.ClaveSSA )  
	--------------------------------- MASCARAS 
	

	------------------------------------- MASCARAS SIAM 
	----Update V set EsRelacionado = 0 
	----From #tmp_Detalles V (NoLock) 

	----Update V Set Mascara = M.Clave_SIAM, EsRelacionado = 1  
	----From #tmp_Detalles V (NoLock) 
	----Inner Join INT_SESEQ__CGF_ClavesSIAM M (NoLock) On ( V.ClaveSSA = M.ClaveSSA )  
	------------------------------------- MASCARAS SIAM 



	--------------------------------- INFORMACION DE LICITACION 
	Update V Set ContenidoPaquete_Licitado = M.ContenidoPaquete_Licitado, ContenidoPaquete_Asignado = 1, Factor = M.Factor  
	From #tmp_Detalles V (NoLock) 
	Inner Join vw_Claves_Precios_Asignados M (NoLock) On ( V.IdEstado = M.IdEstado and V.IdCliente = M.IdCliente and V.IdSubCliente = M.IdSubCliente and V.ClaveSSA = M.ClaveSSA )  
	--Where ClaveLote not like '%*%' 

	Update V Set Cantidad = (Cantidad / (ContenidoPaquete_Licitado * 1.0)) * Factor 
	From #tmp_Detalles V (NoLock) 
	--------------------------------- INFORMACION DE LICITACION 


	-- select * from vw_ClaveSSA_Mascara 


	--------------------------------- GENERAR GUID 
	Update D Set Prefijo_TipoClave = 'RF-'
	From #tmp_Detalles D 
	Where D.EsRefrigerado = 1 

	Update D Set Prefijo_TipoClave = 'RF-'
	From #tmp_Detalles D 
	Inner Join vw_ClavesSSA_Sales C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
	Where C.EsRefrigerado = 1 and 1 = 1 


	Update D Set GUID = 'rep_' + CentroDeCostos + '_' + Prefijo_TipoClave + Jurisdiccion + '_' + FolioReferencia + '_' + FechaCreacion + '.txt' 
	From #tmp_Detalles D 
	Where EsRelacionado = 1 

	Update D Set GUID = 'rep_' + CentroDeCostos + '_' + Prefijo_TipoClave + Jurisdiccion + '_' + FolioReferencia + '_' + FechaCreacion + '___NoIdentificado' +'.txt' 
	From #tmp_Detalles D 
	Where EsRelacionado = 0 
	--------------------------------- GENERAR GUID 


	--		spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM__02_Transferencias  



	---------------- SALIDA FINAL 
	Select GUID 
	From #tmp_Detalles 
	Group by GUID 
	Order by GUID 



	Select 
		GUID, 
		-- (Mascara +	char(9) + cast(sum(Cantidad) as  varchar(20)) ) as Registro 
		(Mascara +	char(9) + ClaveLote + char(9) + Caducidad + char(9) + cast(sum(Cantidad) as  varchar(20)) ) as Registro 
	From #tmp_Detalles 
	Group by 
		GUID, Mascara, ClaveLote, Caducidad  
	Order by 
		GUID, Mascara, ClaveLote, Caducidad     


	select * 
	From #tmp_Detalles 
	Order by 
		GUID, 
		Mascara   
	---------------- SALIDA FINAL 

	----select C.* 
	----From #tmp_Detalles D 
	----Inner Join vw_ClavesSSA_Sales C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 


	----select 
	----	P.EsRefrigerado, 
	----	P.* 
	----From VentasDet D (NoLock) 
	----Inner Join #tmp__FoliosDeVenta V (NoLock) On ( D.IdEmpresa = V.IdEmpresa and D.IdEstado = V.IdEstado and D.IdEstado = V.IdEstado and D.FolioVenta = V.FolioVenta ) 
	----Inner Join VentasInformacionAdicional I (NoLock) On ( D.IdEmpresa = I.IdEmpresa and D.IdEstado = I.IdEstado and D.IdEstado = I.IdEstado and D.FolioVenta = I.FolioVenta ) 
	----Inner Join CatBeneficiarios B (NoLock) On ( V.IdEstado = B.IdEstado and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and I.IdBeneficiario = B.IdBeneficiario ) 
	----Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 

End 
Go--#SQL 

	