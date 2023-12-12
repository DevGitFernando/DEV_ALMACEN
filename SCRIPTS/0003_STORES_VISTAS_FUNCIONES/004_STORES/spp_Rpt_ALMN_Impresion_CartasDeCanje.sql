-------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Rpt_ALMN_Impresion_CartasDeCanje' and Type = 'P' ) 
   Drop Proc spp_Rpt_ALMN_Impresion_CartasDeCanje 
Go--#SQL 

---		Exec spp_Rpt_ALMN_Impresion_CartasDeCanje  1, 21, 1182, 6383, 4, 0    
--   use SII_21_1182_CEDIS_PUEBLA 
   
Create Proc spp_Rpt_ALMN_Impresion_CartasDeCanje 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '1005', 
	@TipoFolio varchar(1) = 'V', @Folio varchar(20) = '00000764'  
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@sFolioCartaCanje varchar(500), 
	@ExpedidoEn varchar(500), 
	@AQuienCorresponda varchar(500), 
	@MesesCaducar int, 
	@Titulo_01 varchar(500), 
	@Titulo_02 varchar(500), 
	@Titulo_03 varchar(500), 
	@Firma varchar(500) 
	
	Select 
		@ExpedidoEn = ExpedidoEn, @AQuienCorresponda = AQuienCorresponda, @MesesCaducar = MesesCaducar, 
		@Titulo_01 = Titulo_01, @Titulo_02 = Titulo_02, @Titulo_03 = Titulo_03, @Firma = Firma
	From CFGC_Titulos_CartaCanje (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	
	Set @ExpedidoEn = IsNull(@ExpedidoEn, 'Expedido en ') 
	Set @AQuienCorresponda = IsNull(@AQuienCorresponda, 'A QUIEN CORRESPONDA') 
	Set @MesesCaducar = IsNull(@MesesCaducar, 120) 
	Set @Titulo_01 = IsNull(@Titulo_01, 'PRIMER PARRAFO') 
	Set @Titulo_02 = IsNull(@Titulo_02, 'SEGUNDO PARRAFO') 
	Set @Titulo_03 = IsNull(@Titulo_03, 'TERCER PARRAFO')
	Set @Firma = IsNull(@Firma, 'ALMACEN') 	


	------------- Obtener el ultimo folio de carta canje generado 
	Select top 1 @sFolioCartaCanje = FolioCarta 
	From RutasDistribucionDet_CartasCanje U (NoLock)  
	Where U.IdEmpresa = @IdEmpresa and U.IdEstado = @IdEstado and U.IdFarmacia = @IdFarmacia and FolioTransferenciaVenta = @Folio And U.Tipo = @TipoFolio 
	Order By FolioCarta desc 



	Select 
		U.IdEmpresa, E.Nombre, U.IdEstado, Estado, U.IdFarmacia, Farmacia, Titulo_00, 
		@ExpedidoEn as ExpedidoEn, @AQuienCorresponda as AQuienCorresponda, U.MesesCaducar, 
		@Titulo_01 as Titulo_01, @Titulo_02 as Titulo_02, @Titulo_03 as Titulo_03, @Firma as Firma,    
		Tipo, FolioCarta, FolioTransferenciaVenta As Folio, getdate() as FechaExpedicion, 
		ClaveSSA, DescripcionClave, U.CodigoEAN, U.ClaveLote, convert(varchar(7), FechaCaducidad, 120) as FechaCaducidad, CantidadEnviada as Cantidad 	
	Into #tmp__InformacionCartaCanje  
	From RutasDistribucionDet_CartasCanje U (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( U.IdProducto = P.IdProducto And U.CodigoEAN = P.CodigoEAN ) 
	Inner Join vw_Farmacias F (NoLock) On ( U.IdEstado = F.IdEstado and U.IdFarmacia = F.IdFarmacia )	
	Inner Join CatEmpresas E (NoLock) On ( U.IdEmpresa = E.IdEmpresa ) 
	Inner Join CatFarmacias_SubFarmacias FS(NoLock) On ( U.IdEstado = FS.IdEstado And U.IdFarmacia = FS.IdFarmacia And U.IdSubFarmacia = FS.IdSubFarmacia ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes FP (NoLock) 
		On ( U.IdEmpresa = FP.IdEmpresa and U.IdEstado = FP.IdEstado and U.IdFarmacia = FP.IdFarmacia and U.IdSubFarmacia = FP.IdSubFarmacia 
		and U.IdProducto = FP.IdProducto and U.CodigoEAN = FP.CodigoEAN and U.ClaveLote = FP.ClaveLote  )
	Where U.IdEmpresa = @IdEmpresa and U.IdEstado = @IdEstado and U.IdFarmacia = @IdFarmacia and FolioTransferenciaVenta = @Folio And U.Tipo = @TipoFolio 
		and FolioCarta = @sFolioCartaCanje



	----------------------------------------- Actualizar el Nombre del destinatario de la Carta Canje 
	Update C Set Titulo_00 = B.nombreCompleto  
	From #tmp__InformacionCartaCanje C 
	Inner Join VentasEnc V (NoLock) On ( C.IdEmpresa = V.IdEmpresa and C.IdEstado = V.IdEstado and C.IdFarmacia = V.IdFarmacia and C.Folio = V.FolioVenta ) 
	Inner Join VentasInformacionAdicional VI (NoLock) On ( V.IdEmpresa = VI.IdEmpresa and V.IdEstado = VI.IdEstado and V.IdFarmacia = VI.IdFarmacia and V.FolioVenta = VI.FolioVenta ) 
	Inner Join vw_Beneficiarios B (NoLock) On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and VI.IdBeneficiario = B.IdBeneficiario )
	Where Tipo = 'V' 

----		spp_Rpt_ALMN_Impresion_CartasDeCanje  


	------------------------- SALIDA FINAL   
	Select 

		IdEmpresa, Nombre, IdEstado, Estado, IdFarmacia, Farmacia, Titulo_00, 
		ExpedidoEn, AQuienCorresponda, MesesCaducar, 
		Titulo_01, Titulo_02, Titulo_03, Firma,    
		Tipo, FolioCarta, Folio, FechaExpedicion, 
		ClaveSSA, DescripcionClave, CodigoEAN, ClaveLote, FechaCaducidad, Cantidad 	
	 
	From #tmp__InformacionCartaCanje 
	Order by ClaveSSA 


End  
Go--#SQL  

