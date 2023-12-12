
--RollBack Tran
--Go 
--		Begin Tran 
Go

--		Commit Tran 

--		RollBack Tran 


--		select db_name() 

--	Select * From VentasInformacionAdicional
--	Select * From VentasEnc

		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes' and xType = 'u' ) Drop Table PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes

		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'u' )  Drop Table PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones

		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_CNSGN__003__FarmaciaProductos_CodigoEAN' and xType = 'u' )  Drop Table PRCS_CNSGN__003__FarmaciaProductos_CodigoEAN

		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_CNSGN__004__FarmaciaProductos' and xType = 'u' )  Drop Table PRCS_CNSGN__004__FarmaciaProductos

Go--#SQL 


Set DateFormat YMD
Set nocount on  
Go--#SQL 

Declare 
		@MeseCaducidad int = 1,  

		@Folio_EntradaPorConsignacion varchar(20), 
		@FolioMovtoInv varchar(20), 
		@IdTipoMovto_Inv varchar(10), 

		@IdSubFarmacia_Origen Varchar(3) = '05',
		@IdSubFarmacia_Destino Varchar(3) = '05', 
		@IdEmpresa Varchar(3) = '001', @IdEstado Varchar(2) = '21', 
		@IdFarmacia Varchar(4) = '4224', 
		@IdFarmacia_Destino Varchar(4) = '4224', 
		@IdCliente varchar(6) = '0042', @IdSubCliente varchar(4) = '0001', 
		@IdPrograma varchar(4) = '0202', @IdSubPrograma varchar(4) = '0002', 
		@IdBeneficiario varchar(8) = '', @NumReceta varchar(20) = 'VENTA-MASIVA', @FechaReceta datetime = Getdate(), 
		@IdTipoDeDispensacion varchar(2) = '00', @IdUnidadMedica varchar(6) = '000000', 
		@IdMedico varchar(6) = '000001', @IdBeneficioSP varchar(4) = '0000', @IdDiagnostico varchar(6) = '0000', 
		@IdServicio varchar(3) = '001', @IdArea varchar(3) = '001', @RefObservaciones varchar(100) = '', 
		@IdTipoMovtoEntrada Varchar(8) = 'EPC',
		@iOpcion smallint = 1


Declare @FolioVenta varchar(32) = '', @FolioMovto VarChar(32) = '*', @FechaSistema varchar(10) = '', @IdCaja varchar(2) = '', @IdPersonal varchar(6) = '0001',
		@TipoDeVenta smallint = 2, @Descuento numeric(14, 4) = 0,
		@Importe numeric(14, 4), @SubTotal numeric(14, 4) = 0, @Iva numeric(14, 4) = 0, @Total numeric(14, 4) = 0,
		@EsAlmacen bit = 0,


		--Detallado
		@IdProducto varchar(10), 
		@CodigoEAN varchar(32), @Renglon int = 0, @Cantidad Numeric(14, 4), @CostoPromedio numeric(14, 4), @UltimoCosto numeric(14, 4),
		@ImpteIva numeric(14, 4), @TasaIva numeric(14, 4), 

		--Lotes
		@ClaveLote varchar(32), @IdSubFarmacia Varchar(3), @FechaCaduca varchar(10),
		--Ubicaciones
		@IdPasillo int, @IdEstante int, @IdEntrepaño int, 
		@SKU_Generado varchar(100), 
		@sObservaciones as varchar(200) 

		 
--		Commit Tran 

--		RollBack Tran 


		Set @IdEmpresa = '004' 
		Set @IdEstado = '11' 
		Set @IdFarmacia = '5002' 
		Set @IdFarmacia_Destino = '5002'
		Set @IdSubFarmacia_Origen = '003' 
		Set @IdSubFarmacia_Destino = '003' 
		Set @IdTipoMovto_Inv = 'EPC' 


		Set @SKU_Generado = '' 
		--Set @FolioVenta = RIGHT('0000000000000' + '726', 8)	-- 721, 722, 723, 724, 725, 726 
		--Set @FolioVenta = RIGHT('0000000000000' + '1476', 8)	-- 1467, 1468, 1469, 1470, 1471, 1472, 1473, 1474, 1475, 1476 

		Set @FolioVenta = ''

		------- 220230418.1500 
		Set @FolioVenta = RIGHT('0000000000000' + @FolioVenta, 8)	-- 1100, 1101, 1102, 1103, 1104, 1105, 1106, 1107  
		-- 1100, 

		Set @sObservaciones = 'ENTRADA POR CONSIGNACIÓN MASIVA ( REFERENCIA ' + @FolioVenta + ' )'  		


		--  721, 722, 723, 724 

		--  725, 726  

		--------------- Datos iniciales 
		Select @EsAlmacen = EsAlmacen From CatFarmacias Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
		Select @FechaSistema = Valor From Net_CFGC_Parametros 
		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And NombreParametro = 'FechaOperacionSistema' 
			And ArbolModulo = (case when @EsAlmacen = 1 then 'ALMN' else 'PFAR' end)
		--------------- Datos iniciales 

--		RollBack Tran 


		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes' and xType = 'u' )  Drop Table PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes  
		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones' and xType = 'u' )  Drop Table PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_CNSGN__003__FarmaciaProductos_CodigoEAN' and xType = 'u' )  Drop Table PRCS_CNSGN__003__FarmaciaProductos_CodigoEAN
		If Exists ( Select * from sysobjects (nolock) Where Name = 'PRCS_CNSGN__004__FarmaciaProductos' and xType = 'u' )  Drop Table PRCS_CNSGN__004__FarmaciaProductos



		Select  
			--datediff(month, F.FechaCaducidad, getdate()) as MesesCaducidad, 
			F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdSubFarmacia, @IdSubFarmacia_Destino as IdSubFarmacia_Destino, 
			F.IdProducto, F.CodigoEAN, '*' + F.ClaveLote as ClaveLote, 1 as EsConsignacion, cast(0 as numeric(14,4)) as Existencia, 
			F.FechaCaducidad, F.FechaRegistro, F.IdPersonal, F.Status, F.Actualizado, 
			cast(0 as numeric(14,4)) as ExistenciaEnTransito, 
			cast(0 as numeric(14,4)) as CostoPromedio, cast(0 as numeric(14,4)) as UltimoCosto, 
			cast(0 as numeric(14,4)) as ExistenciaSurtidos, F.SKU, 
			cast(0 as numeric(14,4)) as TasaIVA, 
			cast(0 as numeric(14,4)) as Costo, 
			cast(0 as numeric(14,4)) as CostoUnitario, 
			cast(0 as numeric(14,4)) as SubTotal, 
			cast(0 as numeric(14,4)) as IVA, 
			cast(0 as numeric(14,4)) as Total
		Into PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes 
		From FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		----Inner Join VentasDet_Lotes L (NoLock) 
		----	On ( 
		----			F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia and L.SKU = F.SKU 
		----			and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote 
		----			and L.FolioVenta = @FolioVenta 
		----		) 
		Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia 
			--and F.Existencia > 0 
			and Exists 
			( 
				Select * 
				From VentasDet_Lotes L (NoLock) 
				Where F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia and L.SKU = F.SKU 
					and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and '*' + F.ClaveLote = '*' + L.ClaveLote 
					and L.FolioVenta = @FolioVenta 
			) 
		Group by 
			F.IdEmpresa, F.IdEstado, F.IdFarmacia, F.IdSubFarmacia,  
			F.IdProducto, F.CodigoEAN, '*' + F.ClaveLote, F.SKU, 
			F.FechaCaducidad, F.FechaRegistro, F.IdPersonal, F.Status, F.Actualizado 



		Update L Set TasaIVA = P.TasaIva
		From PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes L 
		Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) 

		Update L Set SubTotal = Existencia * Costo 
		From PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes L 

		Update L Set IVA = SubTotal * ( TasaIVA / 100.0 )
		From PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes L 

		Update L Set Total = SubTotal + IVA 
		From PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes L 


		Select  
			U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, @IdSubFarmacia_Destino as IdSubFarmacia_Destino, 
			U.IdProducto, U.CodigoEAN, '*' + U.ClaveLote as ClaveLote, 1 as EsConsignacion, 
			U.IdPasillo, U.IdEstante, U.IdEntrepaño, 
			cast(0 as numeric(14,4)) as Existencia, U.Status, U.Actualizado, 
			cast(0 as numeric(14,4)) as ExistenciaEnTransito, 
			cast(0 as numeric(14,4)) as ExistenciaSurtidos, U.SKU, 
			cast(0 as numeric(14,4)) as TasaIVA, 
			cast(0 as numeric(14,4)) as Costo, 
			cast(0 as numeric(14,4)) as SubTotal, 
			cast(0 as numeric(14,4)) as IVA, 
			cast(0 as numeric(14,4)) as Total 
		Into PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
		From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
			--and U.Existencia > 0 
			and Exists 
			(
				Select * 
				From VentasDet_Lotes_Ubicaciones L (NoLock) 
				Where L.IdEmpresa = U.IdEmpresa and L.IdEstado = U.IdEstado and L.IdFarmacia = U.IdFarmacia and L.IdSubFarmacia = U.IdSubFarmacia 
					and L.IdProducto = U.IdProducto and L.CodigoEAN = U.CodigoEAN and '*' + L.ClaveLote = '*' + U.ClaveLote and L.SKU = U.SKU 
					and L.FolioVenta = @FolioVenta 
			) 
		Group by 
			U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, 
			U.IdProducto, U.CodigoEAN, '*' + U.ClaveLote,  
			U.IdPasillo, U.IdEstante, U.IdEntrepaño, U.Status, U.Actualizado,  U.SKU 


			 
		Update U Set TasaIVA = L.TasaIVA, Costo = L.Costo 
		From PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) 
		Inner join PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
			On ( L.IdEmpresa = U.IdEmpresa and L.IdEstado = U.IdEstado and L.IdFarmacia = U.IdFarmacia and L.IdSubFarmacia = U.IdSubFarmacia 
			and L.IdProducto = U.IdProducto and L.CodigoEAN = U.CodigoEAN and L.ClaveLote = U.ClaveLote and L.SKU = U.SKU ) 

		Update L Set SubTotal = Existencia * Costo 
		From PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L 

		Update L Set IVA = SubTotal * ( TasaIVA / 100.0 )
		From PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L 

		Update L Set Total = SubTotal + IVA 
		From PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L 



		Select 
			IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, sum(Existencia) as Existencia, Status, Actualizado, 
			sum(ExistenciaEnTransito) as ExistenciaEnTransito, sum(ExistenciaSurtidos) as ExistenciaSurtidos, 
			--sum(TasaIVA) as TasaIVA, 
			--sum(Costo) as Costo, 
			TasaIVA, Costo, 
			sum(SubTotal) as SubTotal, 
			sum(IVA) as IVA, 
			sum(Total) as Total 
		Into PRCS_CNSGN__003__FarmaciaProductos_CodigoEAN 
		From PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes 
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
		Into PRCS_CNSGN__004__FarmaciaProductos 
		From PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes 
		Group by 
			IdEmpresa, IdEstado, IdFarmacia, IdProducto, 
			TasaIVA, Costo, 
			--CostoPromedio, UltimoCosto
			Status, Actualizado 


---		sp_listacolumnas	EntradasEnc_Consignacion 


---		rollback tran 


----- AQUI 
 

--	Begin tran 


	Set @Folio_EntradaPorConsignacion = ''  
	Set @FolioMovtoInv = '' 
	Set @IdTipoMovto_Inv = 'EPC' 


	--------------------------------- OBTENER LOS FOLIADORES 
	Select @Folio_EntradaPorConsignacion = max(cast(right(FolioEntrada, 8) as int)) + 1   From EntradasEnc_Consignacion --(NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	Set @Folio_EntradaPorConsignacion = IsNull(@Folio_EntradaPorConsignacion, '1') 
	--Set @sFoliador = right(replicate('0', 8) + @Folio_EntradaPorConsignacion, 8)  
	Set @FolioMovtoInv = right(replicate('0', 8) + @Folio_EntradaPorConsignacion, 8) 	


	Exec spp_Mtto_SKU_GetCodigoSKU	
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, 
		@IdTipoMovto_Inv = @IdTipoMovto_Inv, @Folio = @Folio_EntradaPorConsignacion, @SKU_Generado = @SKU_Generado out, @MostrarResultado = 0 

	
	Set @Folio_EntradaPorConsignacion = right(replicate('0', 8) + @Folio_EntradaPorConsignacion, 8)  
	Set @FolioMovtoInv = @IdTipoMovto_Inv + right(replicate('0', 8) + @Folio_EntradaPorConsignacion, 8) 	


	Update F Set SKU = @SKU_Generado, IdSubFarmacia = @IdSubFarmacia_Destino  
	From PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes F 

	Update F Set SKU = @SKU_Generado, IdSubFarmacia = @IdSubFarmacia_Destino  
	From PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F 
	--------------------------------- OBTENER LOS FOLIADORES 

 
	--------------------------------- REGISTRAR LOS NUEVO LOTES-SKU  

	Insert Into FarmaciaProductos 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, CostoPromedio, UltimoCosto, Existencia, StockMinimo, StockMaximo, Status, Actualizado, 
		ExistenciaEnTransito, ExistenciaSurtidos
	) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, 0 as CostoPromedio, 0 as UltimoCosto, 
		0 as Existencia, 0 as StockMinimo, 0 as StockMaximo, 'A' as Status, 0 as Actualizado, 0 as ExistenciaEnTransito, 0 as ExistenciaSurtidos
	From VentasDet T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioVenta = @FolioVenta 
		and Not Exists 
		( 
			Select * 
			From FarmaciaProductos F 
			Where T.IdEmpresa = F.IdEmpresa and T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia and T.IdProducto = F.IdProducto 
		) 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto  

	Insert Into FarmaciaProductos_CodigoEAN 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, Existencia, Status, Actualizado, ExistenciaEnTransito, ExistenciaSurtidos
	) 
	Select 
		distinct 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, 0 as Existencia, 'A' as Status, 0 as Actualizado, 0 as ExistenciaEnTransito, 0 as ExistenciaSurtidos
	From VentasDet T (NoLock) 
	Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia and T.FolioVenta = @FolioVenta  
		and Not Exists 
		( 
			Select * 
			From FarmaciaProductos_CodigoEAN F 
			Where T.IdEmpresa = F.IdEmpresa and T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia and T.IdProducto = F.IdProducto and T.CodigoEAN = T.CodigoEAN
		) 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN 

	
	-- set nocount off 

	Insert Into FarmaciaProductos_CodigoEAN_Lotes 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		Existencia, FechaCaducidad, FechaRegistro, IdPersonal, Status, Actualizado, ExistenciaEnTransito, CostoPromedio, UltimoCosto, ExistenciaSurtidos, SKU
	) 
	Select 
		distinct 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia_Destino as IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		0 as Existencia, FechaCaducidad, getdate() as FechaRegistro, '0001' as IdPersonal, 'A' as Status, 0 as Actualizado, 
		0 as ExistenciaEnTransito, CostoUnitario as CostoPromedio, CostoUnitario as UltimoCosto, 0 as ExistenciaSurtidos, SKU	
	From PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes  T (NoLock) 
	--Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen  
	Where --Existencia > 0 and 
		Not Exists 
		( 
			Select * 
			From FarmaciaProductos_CodigoEAN_Lotes F 
			Where T.IdEmpresa = F.IdEmpresa and T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia and T.IdProducto = F.IdProducto and T.CodigoEAN = F.CodigoEAN 
				and T.IdSubFarmacia_Destino = F.IdSubFarmacia and T.ClaveLote = '*' + F.ClaveLote and @SKU_Generado = F.SKU
		) 
	Group by  
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia_Destino, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		FechaCaducidad, CostoUnitario, SKU 


	--select t.* 
	--From PRCS_CNSGN__001__FarmaciaProductos_CodigoEAN_Lotes  T (NoLock) 
	----Where T.IdEmpresa = @IdEmpresa and T.IdEstadoEnvia = @IdEstado and T.IdFarmaciaEnvia = @IdFarmacia and T.FolioTransferencia = @FolioTransferencia_Origen  
	--Where Not Exists 
	--	( 
	--		Select * 
	--		From FarmaciaProductos_CodigoEAN_Lotes F 
	--		Where T.IdEmpresa = F.IdEmpresa and T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia and T.IdProducto = F.IdProducto and T.CodigoEAN = F.CodigoEAN 
	--			and T.IdSubFarmacia_Destino = F.IdSubFarmacia and T.ClaveLote = '*' + F.ClaveLote and @SKU_Generado = F.SKU
	--	) 


	Insert Into FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, Existencia, Status, Actualizado, ExistenciaEnTransito, ExistenciaSurtidos, SKU  
	) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia_Destino as IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, 0 as Existencia, 'A' as Status, 0 as Actualizado, 0 as ExistenciaEnTransito, 0 as ExistenciaSurtidos, SKU  
	From PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones T (NoLock)  
	Where --Existencia > 0 and 
	Not Exists 
		( 
			Select * 
			From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F 
			Where T.IdEmpresa = F.IdEmpresa and T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia and T.IdProducto = F.IdProducto and T.CodigoEAN = F.CodigoEAN 
				and T.IdSubFarmacia_Destino = F.IdSubFarmacia and T.ClaveLote = '*' + F.ClaveLote and @SKU_Generado = F.SKU 
				and T.IdPasillo = F.IdPasillo and T.IdEstante = F.IdEstante and T.IdEntrepaño = F.IdEntrepaño 
		) 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia_Destino, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, SKU  		


	--select * from FarmaciaProductos_CodigoEAN_Lotes (nolock) Where SKU = @SKU_Generado 
	--select * from FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (nolock) Where SKU = @SKU_Generado 

	-- set nocount on  

	--		rollback tran 


	----Select 
	----	IdEmpresa, IdEstado, IdFarmacia, @IdSubFarmacia_Destino as IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
	----	IdPasillo, IdEstante, IdEntrepaño, 0 as Existencia, 'A' as Status, 0 as Actualizado, 0 as ExistenciaEnTransito, 0 as ExistenciaSurtidos, SKU  
	----From PRCS_CNSGN__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones T (NoLock)  
	----Where Not Exists 
	----	( 
	----		Select * 
	----		From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F 
	----		Where T.IdEmpresa = F.IdEmpresa and T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia and T.IdProducto = F.IdProducto and T.CodigoEAN = T.CodigoEAN 
	----			and @IdSubFarmacia_Destino = F.IdSubFarmacia and T.ClaveLote = '*' + F.ClaveLote and @SKU_Generado = F.SKU 
	----			and T.IdPasillo = F.IdPasillo and T.IdEstante = F.IdEstante and T.IdEntrepaño = F.IdEntrepaño 
	----	) 
	--------------------------------- REGISTRAR LOS NUEVO LOTES-SKU  



 	--------------------------------- REGISTRAR MOVIMIENTOS DE INVENTARIO 	
	Insert Into MovtosInv_Enc 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdTipoMovto_Inv, TipoES, FechaSistema, FechaRegistro, Referencia, MovtoAplicado, 
		IdPersonalRegistra, Observaciones, SubTotal, Iva, Total, Status, Actualizado, FolioCierre, Cierre, IdPersonalHuella, SKU 
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovtoInv as FolioMovtoInv, 
		@IdTipoMovto_Inv, 'E' as TipoES, getdate() as FechaSistema, getdate() as FechaRegistro, '' as Referencia, 'N' as MovtoAplicado, 
		'0001' as IdPersonalRegistra, @sObservaciones as Observaciones, 
		sum(SubTotal) as SubTotal, sum(Iva) as IVA, sum(Total) as Total, 'A' as Status, 0 as Actualizado, 0 as FolioCierre, 0 as Cierre, '' as IdPersonalHuella, @SKU_Generado as SKU 
	From PRCS_CNSGN__004__FarmaciaProductos 


	Insert Into MovtosInv_Det_CodigosEAN 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, FechaSistema, UnidadDeSalida, TasaIva, Cantidad, Costo, Importe, Existencia, Status, Actualizado 
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, @FolioMovtoInv as FolioMovtoInv, IdProducto, CodigoEAN, getdate() as FechaSistema, '' as UnidadDeSalida,  
		TasaIva, 
		sum(cantidadVendida) as Cantidad, 
		--0 as Cant_Devuelta, sum(cantidadVendida) as CantidadRecibida, 
		avg(CostoUnitario) as CostoUnitario, 
		sum(CostoUnitario * CantidadVendida) as Importe, 
		0 as Existencia, 
		'A' as Status, 0 as Actualizado 
	From VentasDet F (NoLock)  
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and F.FolioVenta = @FolioVenta 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, TasaIVA 


	Insert Into MovtosInv_Det_CodigosEAN_Lotes 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, 
		EsConsignacion, Cantidad, Costo, Importe, Existencia, Status, Actualizado, SKU
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia_Destino as IdSubFarmacia, @FolioMovtoInv as FolioMovtoInv, 
		IdProducto, CodigoEAN, '*' + ClaveLote, 1 as EsConsignacion, 
		sum(cantidadVendida) as Cantidad, 
		AVG(CostoUnitario) as Costo, (sum(cantidadVendida) * AVG(CostoUnitario)) as Importe, 0 as Existencia, 
		'A' as Status, 0 as Actualizado, @SKU_Generado as SKU 
	From VentasDet_Lotes F (NoLock)  
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and F.FolioVenta = @FolioVenta 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, --IdSubFarmacia, 
		IdProducto, CodigoEAN, '*' + ClaveLote--, --EsConsignacion, 
		--SKU 


	--Select 
	--	@IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia_Destino as IdSubFarmacia, @FolioMovtoInv as FolioMovtoInv, 
	--	IdProducto, CodigoEAN, '*' + ClaveLote, 1 as EsConsignacion, 
	--	sum(cantidadVendida) as Cantidad, 
	--	AVG(CostoUnitario) as Costo, (sum(cantidadVendida) * AVG(CostoUnitario)) as Importe, 0 as Existencia, 
	--	'A' as Status, 0 as Actualizado, @SKU_Generado as SKU 
	--From VentasDet_Lotes F (NoLock)  
	--Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and F.FolioVenta = @FolioVenta 
	--Group by 
	--	IdEmpresa, IdEstado, IdFarmacia, --IdSubFarmacia, 
	--	IdProducto, CodigoEAN, '*' + ClaveLote --EsConsignacion, 
	--	--, SKU 



	--		rollback tran 


	Insert Into MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, Cantidad, Existencia, Status, Actualizado, SKU
	) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, @IdSubFarmacia_Destino as IdSubFarmacia, @FolioMovtoInv as FolioMovtoInv, 
		IdProducto, CodigoEAN, '*' + ClaveLote, 1 as EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño, sum(cantidadVendida) as Cantidad, 0 as Existencia, 
		'A' as Status, 0 as Actualizado, @SKU_Generado as SKU 
	From VentasDet_Lotes_Ubicaciones F (NoLock)  
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and F.FolioVenta = @FolioVenta 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, --IdSubFarmacia, 
		IdProducto, CodigoEAN, '*' + ClaveLote, --EsConsignacion, 
		IdPasillo, IdEstante, IdEntrepaño   


 	--------------------------------- REGISTRAR MOVIMIENTOS DE INVENTARIO 	



 	--------------------------------- REGISTRAR ENTRADA POR CONSIGNA  	
	Insert Into EntradasEnc_Consignacion 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioEntrada, FolioMovtoInv, 
		IdPersonal, FechaRegistro, 
		ReferenciaPedido, Observaciones, SubTotal, Iva, Total, Status, Actualizado, 
		ReferenciaDePedidoOC, EsReferenciaDePedido, FolioPedido, EsPosFechado, IdProveedor
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, @Folio_EntradaPorConsignacion as FolioEntrada, @FolioMovtoInv as FolioMovtoInv, 
		'0001' as IdPersonalRegistra, getdate() as FechaRegistro, 
		'' as ReferenciaPedido, @sObservaciones as Observaciones, sum(SubTotal) as SubTotal, sum(Iva) as IVA, sum(Total) as Total, 
		'A' as Status, 0 as Actualizado, 
		'' as ReferenciaDePedidoOC, 0 as EsReferenciaDePedido, '' as FolioPedido, 0 as EsPosFechado, '1309' as IdProveedor 
	From VentasEnc F (NoLock)  
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and F.FolioVenta = @FolioVenta 

	--		rollback tran 

	Insert Into EntradasDet_Consignacion 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioEntrada, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
		Cant_Recibida, Cant_Devuelta, CantidadRecibida, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe, Status, Actualizado 
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, @Folio_EntradaPorConsignacion as FolioEntrada, IdProducto, CodigoEAN, Renglon, '' as UnidadDeEntrada,  
		sum(cantidadVendida) as Cantidad, 0 as Cant_Devuelta, sum(cantidadVendida) as CantidadRecibida, 
		avg(CostoUnitario) as CostoUnitario, TasaIva, 
		sum(CostoUnitario * CantidadVendida) as Importe, 
		sum((CostoUnitario * CantidadVendida) * ( TasaIVA / 100.0)) as ImpteIva, 
		sum((CostoUnitario * CantidadVendida) * ( 1 + (TasaIVA / 100.0))) as Importe, 
		'A' as Status, 0 as Actualizado 
	From VentasDet F (NoLock)  
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and F.FolioVenta = @FolioVenta 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN, Renglon, TasaIVA 



	Insert Into EntradasDet_Consignacion_Lotes 
	(
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioEntrada, IdProducto, CodigoEAN, ClaveLote, 
		Renglon, EsConsignacion, Cant_Recibida, Cant_Devuelta, CantidadRecibida, Status, Actualizado, SKU	
	) 
	Select 
		@IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia_Destino as IdSubFarmacia, @Folio_EntradaPorConsignacion as FolioEntrada, 
		IdProducto, CodigoEAN, '*' + ClaveLote, Renglon, 1 as EsConsignacion, 
		sum(cantidadVendida) as Cantidad, 0 as Cant_Devuelta, sum(cantidadVendida) as CantidadRecibida, 
		--AVG(Costo) as Costo, 
		'A' as Status, 0 as Actualizado, @SKU_Generado as SKU 
	From VentasDet_Lotes F (NoLock)  
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and F.FolioVenta = @FolioVenta 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, -- IdSubFarmacia, 
		IdProducto, CodigoEAN, Renglon, '*' + ClaveLote --, EsConsignacion --, SKU 



	Insert Into EntradasDet_Consignacion_Lotes_Ubicaciones 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioEntrada, 
		IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Renglon, 
		EsConsignacion, Cant_Recibida, Cant_Devuelta, CantidadRecibida, Status, Actualizado, SKU
	) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, @IdSubFarmacia_Destino as IdSubFarmacia, @Folio_EntradaPorConsignacion as FolioEntrada, 
		IdProducto, CodigoEAN, '*' + ClaveLote, IdPasillo, IdEstante, IdEntrepaño, Renglon, 
		1 as EsConsignacion, sum(cantidadVendida) as Cantidad, 0 as Cant_Devuelta, sum(cantidadVendida) as CantidadRecibida, 		
		'A' as Status, 0 as Actualizado, @SKU_Generado as SKU
	From VentasDet_Lotes_Ubicaciones F (NoLock)  
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and F.FolioVenta = @FolioVenta 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, --IdSubFarmacia, 
		IdProducto, CodigoEAN, ClaveLote, --EsConsignacion, 
	  IdPasillo, IdEstante, IdEntrepaño, Renglon  
		--SKU
 	--------------------------------- REGISTRAR ENTRADA POR CONSIGNA  	





 	--------------------------------- AFECTAR EXISTENCIAS  
	
	Exec spp_INV_AplicarDesaplicarExistencia  
		@IdEmpresa = @IdEmpresa,  
		@IdEstado = @IdEstado, 
		@IdFarmacia = @IdFarmacia, 
		@FolioMovto = @FolioMovtoInv,  
		@Aplica = 1, @AfectarCostos = 1 

 	--------------------------------- AFECTAR EXISTENCIAS  


---		sp_listacolumnas	EntradasDet_Consignacion_Lotes_Ubicaciones 

--			rollback tran   


--			commit tran   
	

 
----- AQUI 



	---------- Salida final 
	Select @Folio_EntradaPorConsignacion as Folio, @SKU_Generado as SKU 

	----select * 
	----from MovtosInv_Enc F (noLock) 
	----Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and FolioMovtoInv = @Folio_EntradaPorConsignacion 

	----select * 
	----from MovtosInv_Det_CodigosEAN F (noLock) 
	----Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and FolioMovtoInv = @Folio_EntradaPorConsignacion 

	----select * 
	----from MovtosInv_Det_CodigosEAN_Lotes F (noLock) 
	----Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and FolioMovtoInv = @Folio_EntradaPorConsignacion 

	----select * 
	----from MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones F (noLock) 
	----Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and FolioMovtoInv = @Folio_EntradaPorConsignacion 



	select sum(cantidadVendida) as Cantidad_Vendida 
	From VentasDet_Lotes_Ubicaciones F (NoLock)  
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and F.FolioVenta = @FolioVenta 


	select sum(CantidadRecibida) as Cantidad_EPC 
	from EntradasDet_Consignacion_Lotes_Ubicaciones F (noLock) 
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and FolioEntrada = @Folio_EntradaPorConsignacion 


	select sum(cantidad) as Cantidad_Inventarios, sum(Existencia) as Existencia  
	from MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones F (noLock) 
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado And F.IdFarmacia = @IdFarmacia and FolioMovtoInv = @FolioMovtoInv  


--			rollback tran   


--			commit tran   
	

