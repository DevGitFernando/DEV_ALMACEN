
--RollBack Tran
--Go 
--Begin Tran 
Go

--		Commit Tran 

--		RollBack Tran 


--		select db_name() 

--	Select * From VentasInformacionAdicional
--	Select * From VentasEnc

		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes' and xType = 'u' ) Drop Table PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes

		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'u' )  Drop Table PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones

		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_TRS__003__FarmaciaProductos_CodigoEAN' and xType = 'u' )  Drop Table PRCS_TRS__003__FarmaciaProductos_CodigoEAN

		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_TRS__004__FarmaciaProductos' and xType = 'u' )  Drop Table PRCS_TRS__004__FarmaciaProductos

Go--#SQL 


Set DateFormat YMD
Set nocount off 
Go--#SQL 

Declare 
		@MeseCaducidad int = 1,  

		@FolioMovtoInv varchar(20), 
		@IdTipoMovto_Inv varchar(10), 

		@IdSubFarmacia_Origen Varchar(2) = '05',
		@IdSubFarmacia_Destino Varchar(2) = '05', 
		@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '21', @IdFarmacia Varchar(4) = '4224',
		@IdCliente varchar(6) = '0042', @IdSubCliente varchar(4) = '0001', 
		@IdPrograma varchar(4) = '0202', @IdSubPrograma varchar(4) = '0002', 
		@IdBeneficiario varchar(8) = '', @NumReceta varchar(20) = 'VENTA-MASIVA', @FechaReceta datetime = Getdate(), 
		@IdTipoDeDispensacion varchar(2) = '00', @IdUnidadMedica varchar(6) = '000000', 
		@IdMedico varchar(6) = '000001', @IdBeneficioSP varchar(4) = '0000', @IdDiagnostico varchar(6) = '0000', 
		@IdServicio varchar(3) = '001', @IdArea varchar(3) = '001', @RefObservaciones varchar(100) = '', 
		@IdTipoMovtoEntrada Varchar(8) = 'EPC',
		@iOpcion smallint = 1


Declare @FolioVenta varchar(32) = '*', @FolioMovto VarChar(32) = '*', @FechaSistema varchar(10) = '', @IdCaja varchar(2) = '', @IdPersonal varchar(6) = '0001',
		@TipoDeVenta smallint = 2, @Descuento numeric(14, 4) = 0,
		@Importe numeric(14, 4), @SubTotal numeric(14, 4) = 0, @Iva numeric(14, 4) = 0, @Total numeric(14, 4) = 0,
		@EsAlmacen bit = 0,


		--Detallado
		@IdProducto varchar(10), 
		@CodigoEAN varchar(32), @Renglon int = 0, @Cantidad Numeric(14, 4), @CostoPromedio numeric(14, 4), @UltimoCosto numeric(14, 4),
		@ImpteIva numeric(14, 4), @TasaIva numeric(14, 4), 

		--Lotes
		@ClaveLote varchar(32), @IdSubFarmacia Varchar(2), @FechaCaduca varchar(10),
		--Ubicaciones
		@IdPasillo int, @IdEstante int, @IdEntrepaño int

		 

		Set @IdEmpresa = '001' 
		Set @IdEstado = '11' 
		Set @IdFarmacia = '3005'
		Set @IdSubFarmacia_Origen = '05' 
		Set @IdSubFarmacia_Destino = '05' 


		--------------- Datos iniciales 
		Select @EsAlmacen = EsAlmacen From CatFarmacias Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
		Select @FechaSistema = Valor From Net_CFGC_Parametros 
		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And NombreParametro = 'FechaOperacionSistema' 
			And ArbolModulo = (case when @EsAlmacen = 1 then 'ALMN' else 'PFAR' end)
		--------------- Datos iniciales 



		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes' and xType = 'u' )  Drop Table PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes  
		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'u' )  Drop Table PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_TRS__003__FarmaciaProductos_CodigoEAN' and xType = 'u' )  Drop Table PRCS_TRS__003__FarmaciaProductos_CodigoEAN
		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_TRS__004__FarmaciaProductos' and xType = 'u' )  Drop Table PRCS_TRS__004__FarmaciaProductos



		Select 
			datediff(month, FechaCaducidad, getdate()) as MesesCaducidad, 
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Existencia, 
			FechaCaducidad, FechaRegistro, IdPersonal, Status, Actualizado, ExistenciaEnTransito, CostoPromedio, UltimoCosto, ExistenciaSurtidos, SKU, 
			cast(0 as numeric(14,4)) as TasaIVA, 
			cast(UltimoCosto as numeric(14,4)) as Costo, 
			cast(0 as numeric(14,4)) as SubTotal, 
			cast(0 as numeric(14,4)) as IVA, 
			cast(0 as numeric(14,4)) as Total
		Into PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes 
		From FarmaciaProductos_CodigoEAN_Lotes (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
			and ExistenciaEnTransito <= 0 
			and ExistenciaSurtidos <= 0 
			and Existencia > 0 
			and datediff(month, FechaCaducidad, getdate()) <= 0  

		Update L Set TasaIVA = P.TasaIva
		From PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes L 
		Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) 

		Update L Set SubTotal = Existencia * Costo 
		From PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes L 

		Update L Set IVA = SubTotal * ( TasaIVA / 100.0 )
		From PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes L 

		Update L Set Total = SubTotal + IVA 
		From PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes L 


		Select 
			IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
			IdPasillo, IdEstante, IdEntrepaño, Existencia, Status, Actualizado, ExistenciaEnTransito, ExistenciaSurtidos, SKU, 
			cast(0 as numeric(14,4)) as TasaIVA, 
			cast(0 as numeric(14,4)) as Costo, 
			cast(0 as numeric(14,4)) as SubTotal, 
			cast(0 as numeric(14,4)) as IVA, 
			cast(0 as numeric(14,4)) as Total 
		Into PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
		From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
			and ExistenciaEnTransito <= 0 
			and ExistenciaSurtidos <= 0 
			and Existencia > 0 
			and Exists 
			(
				Select * 
				From PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
				Where L.IdEmpresa = U.IdEmpresa and L.IdEstado = U.IdEstado and L.IdFarmacia = U.IdFarmacia and L.IdSubFarmacia = U.IdSubFarmacia 
					and L.IdProducto = U.IdProducto and L.CodigoEAN = U.CodigoEAN and L.ClaveLote = U.ClaveLote and L.SKU = U.SKU
			) 

			 
		Update U Set TasaIVA = L.TasaIVA, Costo = L.Costo 
		From PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) 
		Inner join PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
			On ( L.IdEmpresa = U.IdEmpresa and L.IdEstado = U.IdEstado and L.IdFarmacia = U.IdFarmacia and L.IdSubFarmacia = U.IdSubFarmacia 
			and L.IdProducto = U.IdProducto and L.CodigoEAN = U.CodigoEAN and L.ClaveLote = U.ClaveLote and L.SKU = U.SKU ) 

		Update L Set SubTotal = Existencia * Costo 
		From PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L 

		Update L Set IVA = SubTotal * ( TasaIVA / 100.0 )
		From PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L 

		Update L Set Total = SubTotal + IVA 
		From PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L 



		Select 
			IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, sum(Existencia) as Existencia, Status, Actualizado, 
			sum(ExistenciaEnTransito) as ExistenciaEnTransito, sum(ExistenciaSurtidos) as ExistenciaSurtidos, 
			--sum(TasaIVA) as TasaIVA, 
			--sum(Costo) as Costo, 
			TasaIVA, Costo, 
			sum(SubTotal) as SubTotal, 
			sum(IVA) as IVA, 
			sum(Total) as Total 
		Into PRCS_TRS__003__FarmaciaProductos_CodigoEAN 
		From PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes 
		Group by 
			IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, 
			TasaIVA, Costo, 
			--CostoPromedio, UltimoCosto
			Status, Actualizado 


		Select 
			IdEmpresa, IdEstado, IdFarmacia, IdProducto, 
			avg(CostoPromedio) as CostoPromedio, 
			avg(UltimoCosto) as UltimoCosto, avg(Existencia) as Existencia, 0 as StockMinimo, 0 as StockMaximo, Status, Actualizado, 
			sum(ExistenciaEnTransito) as ExistenciaEnTransito, sum(ExistenciaSurtidos) as ExistenciaSurtidos, 
			--sum(TasaIVA) as TasaIVA, 
			--sum(Costo) as Costo, 
			TasaIVA, Costo, 
			sum(SubTotal) as SubTotal, 
			sum(IVA) as IVA, 
			sum(Total) as Total 			
		Into PRCS_TRS__004__FarmaciaProductos 
		From PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes 
		Group by 
			IdEmpresa, IdEstado, IdFarmacia, IdProducto, 
			TasaIVA, Costo, 
			--CostoPromedio, UltimoCosto
			Status, Actualizado 


---		sp_listacolumnas	MovtosInv_Enc 


---		rollback tran 


Begin Tran 

--	sp_listacolumnas__stores spp_Mtto_VentasInformacionAdicional , 1 


	Set @FolioMovtoInv = ''  
	Set @IdTipoMovto_Inv = 'SC' 


	Select @FolioMovtoInv = max(right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_Inv) )) + 1 From MovtosInv_Enc --(NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdTipoMovto_Inv = @IdTipoMovto_Inv 

	Set @FolioMovtoInv = IsNull(@FolioMovtoInv, '1') 
	--Set @sFoliador = right(replicate('0', 8) + @FolioMovtoInv, 8)  
	Set @FolioMovtoInv = @IdTipoMovto_Inv + right(replicate('0', 8) + @FolioMovtoInv, 8) 


	Select @FolioMovtoInv 

	------------------- Encabezado 
	Insert Into MovtosInv_Enc 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, Referencia, MovtoAplicado, 
		IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, Actualizado, FolioCierre, Cierre, IdPersonalHuella, SKU 
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovtoInv, @IdTipoMovto_Inv, 'S' as TipoES, getdate() as FechaSistema, getdate() as FechaRegistro, '' as Referencia, 'N' as MovtoAplicado, 
		'0001' as IdPersonalRegistra, 'SALIDA DE CADUCADOS MASIVA' as Observaciones, 
		sum(SubTotal) as SubTotal, sum(Iva) as IVA, sum(Total) as Total, 'A' as Status, 0 as Actualizado, 0 as FolioCierre, 0 as Cierre, '' as IdPersonalHuella, '' as SKU 
	From PRCS_TRS__004__FarmaciaProductos 

----	sp_listacolumnas		MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 

	Insert Into MovtosInv_Det_CodigosEAN 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, FechaSistema, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, Existencia, Status, Actualizado 
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovtoInv, IdProducto, CodigoEAN, getdate() as FechaSistema, 
		'' as UnidadDeSalida, TasaIva, sum(Existencia) as Cantidad, avg(Costo), sum(SubTotal) as Importe, 0 as Existencia, 'A' as Status, 0 as Actualizado 
	From PRCS_TRS__003__FarmaciaProductos_CodigoEAN 
	Group by 
		IdProducto, CodigoEAN, TasaIva 


	Insert Into MovtosInv_Det_CodigosEAN_Lotes 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Cantidad, Costo, Importe, Existencia, Status, Actualizado, SKU
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, IdSubFarmacia, @FolioMovtoInv, 
		IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Existencia as Cantidad, Costo, SubTotal as Importe, 0 as Existencia, 'A' as Status, 0 as Actualizado, SKU
	From PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes 


	Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado, SKU
	) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, @FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, Existencia as Cantidad, 0 as Existencia, Status, Actualizado, SKU
	From PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 




	Exec spp_INV_AplicarDesaplicarExistencia  
		@IdEmpresa = @IdEmpresa,  
		@IdEstado = @IdEstado, 
		@IdFarmacia = @IdFarmacia, 
		@FolioMovto = @FolioMovtoInv,  
		@Aplica = 1, @AfectarCostos = 0 


	--Exec spp_Mtto_MovtoInv_Enc
	--	@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, 
	--	@FolioMovtoInv = @FolioVenta, @IdTipoMovto_Inv = 'SV', @TipoES = 'S', 
	--	@Referencia = '', -- @MovtoAplicado varchar(1), 
	--	@IdPersonal = @IdPersonal, @Observaciones = '.', 
	--	@SubTotal = @SubTotal, @Iva = @Iva, @Total = @Total, @iOpcion = @iOpcion




--			rollback tran   

--			commit tran   
	
