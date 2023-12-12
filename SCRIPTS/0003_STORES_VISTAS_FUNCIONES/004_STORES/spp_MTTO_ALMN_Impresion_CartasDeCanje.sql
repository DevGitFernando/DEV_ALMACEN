
----------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_MTTO_ALMN_Impresion_CartasDeCanje' and Type = 'P' ) 
   Drop Proc spp_MTTO_ALMN_Impresion_CartasDeCanje
Go--#SQL 

---		Exec spp_Rpt_ALMN_Impresion_CartasDeCanje  1, 21, 1182, 6383, 4, 0    
--   use SII_21_1182_CEDIS_PUEBLA 
   
Create Proc spp_MTTO_ALMN_Impresion_CartasDeCanje 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '0003', 
	@Folio varchar(20) = '00000001', @FolioTransferenciaVenta Varchar(20) = '00000591', @TipoFolio varchar(1) = 'V'
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFiltro_Atencion varchar(1000),   
	@sFiltro varchar(1000), 
	@sTipoReporte varchar(10), 
	@PerfilDeAtencion_Nombre varchar(200),
	@FolioCompleto varchar(20),
	@bContiene Bit,
	@FolioCarta varchar(8)
	
Declare 
	@IdCliente varchar(4), 
	@IdSubCliente varchar(4), 
	@IdPrograma varchar(4), 
	@IdSubPrograma varchar(4) 		


Declare 
	--@ExpedidoEn varchar(500), 
	--@AQuienCorresponda varchar(500), 
	@MesesCaducar int--, 
	--@Titulo_01 varchar(500), 
	--@Titulo_02 varchar(500), 
	--@Titulo_03 varchar(500), 
	--@Firma varchar(500)  	
				
---------------	Formatear valores 
	Set @IdEmpresa = right('0000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000000000000' + @IdFarmacia, 4) 
	Set @Folio = right('0000000000000' + @Folio, 8)
	set @bContiene = 0
---------------	Formatear valores 	


-------------------	Datos de Carta de Canje 
	Select 
		--@ExpedidoEn = ExpedidoEn, @AQuienCorresponda = AQuienCorresponda,
		@MesesCaducar = MesesCaducar--, 
		--@Titulo_01 = Titulo_01, @Titulo_02 = Titulo_02, @Titulo_03 = Titulo_03, @Firma = Firma
	From CFGC_Titulos_CartaCanje (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	
	--Set @ExpedidoEn = IsNull(@ExpedidoEn, 'Expedido en ') 
	--Set @AQuienCorresponda = IsNull(@AQuienCorresponda, 'A QUIEN CORRESPONDA') 
	Set @MesesCaducar = IsNull(@MesesCaducar, 120) 
	--Set @Titulo_01 = IsNull(@Titulo_01, 'PRIMER PARRAFO') 
	--Set @Titulo_02 = IsNull(@Titulo_02, 'SEGUNDO PARRAFO') 
	--Set @Titulo_03 = IsNull(@Titulo_03, 'TERCER PARRAFO')
	--Set @Firma = IsNull(@Firma, 'ALMACEN')
	
	Select @FolioCarta = MAX(FolioCarta) + 1 From RutasDistribucionDet_CartasCanje
	
	Set @FolioCarta = IsNull(@FolioCarta, '1')
	Set @FolioCarta = Right(replicate('0', 8) + @FolioCarta, 8) 
	
------		spp_Rpt_ALMN_Impresion_CartasDeCanje  
	
-------------------	Datos de Carta de Canje 

---------------	Base del proceso 
	Select top 0  
		IdEmpresa, IdEstado, IdFarmacia, space(500) as Titulo_00, @MesesCaducar as MesesCaducar,     
		@TipoFolio as TipoFolio, space(20) as Folio, getdate() as FechaExpedicion, 
		ClaveSSA, DescripcionClave, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, convert(varchar(7), FechaCaducidad, 120) as FechaCaducidad, 0 as Cantidad 
	into #tmpProceso 	
	From vw_ExistenciaPorCodigoEAN_Lotes  
---------------	Base del proceso 


	If @TipoFolio = 'V'
	Begin
		If Not Exists ( Select * 
			   From RutasDistribucionDet_CartasCanje C (NoLock) 
			   Where @IdEmpresa = IdEmpresa And @IdEstado = IdEstado And @IdFarmacia = IdFarmacia And @Folio = FolioRuta And @FolioTransferenciaVenta = FolioTransferenciaVenta )
			Begin
				Set @FolioCompleto = right('0000000000000' + @FolioTransferenciaVenta, 8) 
				
				Insert Into #tmpProceso ( IdEmpresa, IdEstado, IdFarmacia, Titulo_00, MesesCaducar, 
					TipoFolio, Folio, FechaExpedicion, ClaveSSA, DescripcionClave, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, FechaCaducidad, Cantidad  ) 
				Select 
					V.IdEmpresa, V.IdEstado, V.IdFarmacia, '' as Titulo_00, @MesesCaducar as MesesCaducar,
					@TipoFolio as TipoFolio, FolioVenta, getdate(), 
					'' as Clave, '' as DescripcionClave, V.IdProducto, V.CodigoEAN, V.IdSubFarmacia, V.ClaveLote, 
					convert(varchar(7), FechaCaducidad, 120) as FechaCaducidad, CantidadVendida 
					-- *, datediff(mm, getdate(), IsNull(FechaCad, cast('2000-01-01' as datetime))) as MesesParaCaducar  
				From VentasDet_Lotes V (NoLock)--vw_VentasDet_CodigosEAN_Lotes
				Inner Join FarmaciaProductos_CodigoEAN_Lotes P On 
					(V.IdEmpresa = P.IdEmpresa And V.IdEstado = P.IdEstado And V.IdFarmacia = P.IdFarmacia And
					V.IdProducto = P.IdProducto And V.CodigoEAN = P.CodigoEAN And V.IdSubFarmacia = P.IdSubFarmacia And V.ClaveLote = P.ClaveLote )
				--Inner Join vw_EmpresasEstados E On (V.IdEmpresa = E.IdEmpresa And V.IdEstado = E.IdEstado)
				--Inner Join CatFarmacias F On (V.IdEstado = F.IdEstado And V.IdFarmacia = F.IdFarmacia)
				Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and FolioVenta = @FolioCompleto 
					And datediff(mm, getdate(), IsNull(FechaCaducidad, cast('2000-01-01' as datetime))) <= @MesesCaducar And V.ClaveLote Not like '%*%'
					
				Update P Set Titulo_00 = NombreCompleto, FechaExpedicion = V.FechaRegistro 
				From #tmpProceso P (NoLock) 
				--Inner Join vw_Impresion_Ventas_Credito V (NoLock)
				Inner Join vw_VentasEnc V (NoLock) On ( P.IdEmpresa = V.IdEmpresa and P.IdEstado = V.IdEstado and P.IdFarmacia = V.IdFarmacia and P.Folio = V.Folio )
				Inner Join VentasInformacionAdicional I (NoLock) On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.FolioVenta ) 
				Inner Join vw_Beneficiarios B (NoLock) 
					On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and I.IdBeneficiario = B.IdBeneficiario ) 
			

				--Update P Set Titulo_00 = (B.ApPaterno + ' ' + B.Ap.Materno + ' ' + B.Nombre), FechaExpedicion = V.FechaRegistro 
				--From #tmpProceso P (NoLock) 
				----Inner Join vw_Impresion_Ventas_Credito V (NoLock)
				--Inner Join vw_VentasEnc V (NoLock) On ( P.IdEmpresa = V.IdEmpresa and P.IdEstado = V.IdEstado and P.IdFarmacia = V.IdFarmacia and P.Folio = V.Folio )
				--Inner Join VentasInformacionAdicional I (NoLock) On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.FolioVenta ) 
				--Inner Join CatBeneficiarios B (NoLock) 
				--	On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and I.IdBeneficiario = B.IdBeneficiario ) 
				--Where 1 = 0 


			End 
		Else
			Begin
				Set @bContiene = 1
			End	
	End 


	
---		select top 1 * from vw_Impresion_Ventas_Credito 	
---		select top 1 * from TransferenciasEnc 		
	
	If @TipoFolio = 'T'
	Begin
		If Not Exists ( Select *
				   From RutasDistribucionDet_CartasCanje C (NoLock)
				   Where @IdEmpresa = IdEmpresa And @IdEstado = IdEstado And @IdFarmacia = IdFarmacia And @Folio = FolioRuta And @FolioTransferenciaVenta = FolioTransferenciaVenta )
			Begin
				Set @FolioCompleto = 'TS' + right('0000000000000' + @FolioTransferenciaVenta, 8) 
				
				Insert Into #tmpProceso ( IdEmpresa, IdEstado, IdFarmacia, Titulo_00, MesesCaducar, 
						TipoFolio, Folio, FechaExpedicion, ClaveSSA, DescripcionClave, IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, FechaCaducidad, Cantidad  ) 
				Select 
					IdEmpresa, IdEstado, IdFarmacia, '' as Titulo_00, @MesesCaducar as MesesCaducar,    
					@TipoFolio as TipoFolio, Folio, getdate(), 
					'' as Clave, '' as DescripcionClave, IdProducto, CodigoEAN, IdSubFarmaciaEnvia, ClaveLote, 
					convert(varchar(7), FechaCad, 120) as FechaCaducidad, Cantidad 
					-- *, datediff(mm, getdate(), IsNull(FechaCad, cast('2000-01-01' as datetime))) as MesesParaCaducar  
				From vw_TransferenciaDet_CodigosEAN_Lotes 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @FolioCompleto And ClaveLote Not like '%*%'
					And datediff(mm, getdate(), IsNull(FechaCad, cast('2000-01-01' as datetime))) <= @MesesCaducar  	
					
				Update P Set Titulo_00 = 'TRANSFERENCIA', FechaExpedicion = V.FechaRegistro 
				From #tmpProceso P (NoLock) 
				Inner Join TransferenciasEnc V (NoLock) 
					On P.IdEmpresa = V.IdEmpresa and P.IdEstado = V.IdEstado and P.IdFarmacia = V.IdFarmacia and P.Folio = V.FolioTransferencia
			End
		Else
			Begin
				Set @bContiene = 1
			End	
	End 
	

---------------	SALIDA FINAL 
	Update T Set ClaveSSA = P.ClaveSSA, DescripcionClave = P.DescripcionClave 
	From #tmpProceso T 
	Inner Join vw_Productos_CodigoEAN P On ( T.CodigoEAN = P.CodigoEAN ) 

	Insert RutasDistribucionDet_CartasCanje (IdEmpresa, IdEstado, IdFarmacia, FolioRuta, FolioCarta, Titulo_00, MesesCaducar, FolioTransferenciaVenta, Tipo,
		IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote,Cant_Enviada, Cant_Devuelta, CantidadEnviada)
	Select IdEmpresa, IdEstado, IdFarmacia, @Folio As Folio, @FolioCarta, Titulo_00, @MesesCaducar As MesesCaducar, @FolioTransferenciaVenta As FolioTransferenciaVenta, @TipoFolio As Tipo,
		IdProducto, CodigoEAN, IdSubFarmacia, ClaveLote, Cantidad As Cant_Enviada, 0 As Cant_Devuelta, Cantidad As CantidadEnviada
	From #tmpProceso
	
	If Exists (Select * From #tmpProceso)
		Begin
			Set @bContiene = 1
		End
	
	
	----- SALIDA FINAL 	
	Select @bContiene As Contiene 


End  
Go--#SQL 
	
