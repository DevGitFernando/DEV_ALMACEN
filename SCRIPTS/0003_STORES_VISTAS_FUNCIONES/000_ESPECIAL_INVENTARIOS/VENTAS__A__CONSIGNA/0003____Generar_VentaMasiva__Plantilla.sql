

--RollBack Tran
--Go 

--		Begin Tran 

Go 

--		Commit Tran 

--		RollBack Tran 


--		select db_name() 

--	Select * From VentasInformacionAdicional
--	Select * From VentasEnc


Set DateFormat YMD
Set nocount on  


Declare 
		@MeseCaducidad int = 1,  

		@SKU varchar(100) = '', 
		@IdSubFarmacia_Destino Varchar(10) = '01',
		@IdEmpresa Varchar(3) = '004', @IdEstado Varchar(2) = '11', @IdFarmacia Varchar(4) = '5002',
		@IdCliente varchar(6) = '0042', @IdSubCliente varchar(4) = '0004', 
		@IdPrograma varchar(4) = '0202', @IdSubPrograma varchar(4) = '0011', 
		@IdBeneficiario varchar(8) = '00000403', @NumReceta varchar(20) = 'VENTA ESPECIAL', @FechaReceta datetime = Getdate(), 
		@IdTipoDeDispensacion varchar(2) = '21', @IdUnidadMedica varchar(6) = '000000', 
		@IdMedico varchar(6) = '000001', @IdBeneficioSP varchar(4) = '0000', @IdDiagnostico varchar(6) = '0000', 
		@IdServicio varchar(3) = '001', @IdArea varchar(3) = '001', @RefObservaciones varchar(100) = '', 
		@IdTipoMovtoEntrada Varchar(8) = 'EPC',
		@iOpcion smallint = 1


Declare @FolioVenta varchar(32) = '*', @FolioMovto VarChar(32) = '*', @FechaSistema varchar(10) = '', @IdCaja varchar(2) = '', @IdPersonal varchar(6) = '0003',
		@TipoDeVenta smallint = 2, @Descuento numeric(14, 4) = 0,
		@Importe numeric(14, 4), @SubTotal numeric(14, 4) = 0, @Iva numeric(14, 4) = 0, @Total numeric(14, 4) = 0,
		@EsAlmacen bit = 0, 


		--Detallado
		@IdProducto varchar(10), 
		@CodigoEAN varchar(32), @Renglon int = 0, @Cantidad Numeric(14, 4), @CostoPromedio numeric(14, 4), @UltimoCosto numeric(14, 4),
		@ImpteIva numeric(14, 4), @TasaIva numeric(14, 4), 

		--Lotes
		@ClaveLote varchar(32), @IdSubFarmacia Varchar(10), @FechaCaduca varchar(10),
		--Ubicaciones
		@IdPasillo int, @IdEstante int, @IdEntrepaño int

		 
		
		create table #Folio   (  FolioVenta Varchar(32), Descripcion Varchar(300) )
		create table #FolioSal (  FolioVenta Varchar(32), Descripcion Varchar(300) ) 
		create table #FolioMSj (  FolioVenta Varchar(32), Descripcion Varchar(300) ) 

		Set @SKU = '' 

		Select @FechaSistema = Valor From Net_CFGC_Parametros Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And NombreParametro = 'FechaOperacionSistema' And ArbolModulo = 'PFAR'
		Select @EsAlmacen = EsAlmacen From CatFarmacias Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia


	Select
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, PC.IdSubFarmacia, PC.IdSubFarmacia As IdSubFarmacia_Destino, 

		L.SKU, 
		L.IdProducto, L.CodigoEAN, L.ClaveLote, L.ClaveLote As ClaveLote_Consigna,
		L.EsConsignacion, L.CostoPromedio, L.UltimoCosto, Convert(Varchar(10), L.FechaCaducidad, 120) As FechaCaducidad,
		P.ContenidoPaquete, PC.cantidad As Cantidad,
			
		----Cast(((L.Existencia - L.ExistenciaEnTransito) / P.ContenidoPaquete) As Int) * P.ContenidoPaquete As Cantidad_Contemplada, --DateDiff(mm,GetDate(), FechaCaducidad),
		--Cast(((L.Existencia - L.ExistenciaEnTransito) * 1.0) As Int) As Cantidad_Contemplada, --DateDiff(mm,GetDate(), FechaCaducidad),
		PC.cantidad As Cantidad_Contemplada, 
		0 as CantidadNoCompleta, 
		
		--(L.Existencia - L.ExistenciaEnTransito) As CantidadNoCompleta,
		TasaIva, Cast(0.0000 as Numeric(18,4)) As SubTotal, Cast(0.0000 as Numeric(18,4)) As Iva, Cast(0.0000 as Numeric(18,4)) As Total,
		P.IdClaveSSA_Sal As IdClaveSSA, P.ClaveSSA, P.IdClaveSSA_Sal As IdClaveSSA_AUX, P.ClaveSSA As ClaveSSA_AUX,  0 As Renglon, 0 As EsCuadroBasico
	Into #TempInv_Lotes
	From FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( L.IdProducto = P.idProducto And L.CodigoEAN = P.CodigoEAN )
	inner join  
	( 
		select IdEmpresa, idestado, idfarmacia, IdsubFarmacia, SKU, codigoEan, Clavelote, sum(cantidad) as Cantidad 	 
		From VentaMasiva_Plantilla (nolock) 
		Group by 
			IdEmpresa, idestado, idfarmacia, IdsubFarmacia, SKU, codigoEan, Clavelote 
	) PC On 
		( 
			L.idEmpresa = PC.IdEmpresa and L.IdEstado = PC.IdEstado and 
			L.IdFarmacia = PC.idfarmacia and L.IdSubFarmacia = PC.idsubFarmacia 

			and right('00000000000000000000' + L.CodigoEAN, 20) = right('00000000000000000000' + PC.codigoEAN, 20)  
			
			and L.ClaveLote = PC.ClaveLote 	
			--and L.idPasillo = PC.IdPasillo and L.IdEstante = PC.IdEstante and L.IdEntrepaño = PC.IdEntrepano 
			and L.IdSubFarmacia = PC.idsubfarmacia 
			and L.SKU = PC.sku 
			--and L.Existencia = P.Cantidad 
		) 
	Where
		--P.IdClaveSSA_Sal Is null And
		L.ClaveLote not like '%*%' 
		--And  (L.Existencia - L.ExistenciaEnTransito) > 0 
		And DateDiff(mm,GetDate(), FechaCaducidad) >= @MeseCaducidad And--P.ContenidoPaquete > 1 And --Existencia = 16 And
		L.IdEmpresa = @IdEmpresa And L.IdEstado = @IdEstado And L.IdFarmacia = @IdFarmacia 
		--and L.CodigoEAN = '7501122965602' 

	Update #TempInv_Lotes Set CantidadNoCompleta = Cantidad - Cantidad_Contemplada

	Update T Set EsCuadroBasico = 1 
	From #TempInv_Lotes T 
	Inner Join vw_Claves_Precios_Asignados CB (NoLock) 
	    On ( CB.IdEstado = T.IdEstado And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente and T.ClaveSSA_AUX = CB.ClaveSSA and CB.Status in ( 'A' ) ) 


	Update T Set IdClaveSSA_AUX = R.IdClaveSSA, ClaveSSA_AUX = R.ClaveSSA
	From #TempInv_Lotes T 
	Inner Join vw_Relacion_ClavesSSA_Claves R On ( R.IdEstado = T.IdEstado And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente and T.IdClaveSSA = R.IdClaveSSA_Relacionada and R.Status = 'A' ) 
	Where EsCuadroBasico = 0 

	Update T Set EsCuadroBasico = 1 
	From #TempInv_Lotes T 
	Inner Join vw_Claves_Precios_Asignados CB (NoLock) 
	    On ( CB.IdEstado = T.IdEstado And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente and T.ClaveSSA_AUX = CB.ClaveSSA and CB.Status in ( 'A' ) ) 
	Where EsCuadroBasico = 0 



--		RollBack Tran   

	--Delete #TempInv_Lotes Where EsCuadroBasico = 0 


--	select sum(cantidad) from #TempInv_Lotes 

	
	----Ubicaciones
	Select
		U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, IdSubFarmacia_Destino, U.SKU, U.IdProducto, U.CodigoEAN, U.ClaveLote, U.ClaveLote As ClaveLote_Consigna,
		U.EsConsignacion, TasaIva, L.CostoPromedio, L.UltimoCosto, FechaCaducidad, U.IdPasillo, U.IdEstante, U.IdEntrepaño, Renglon, ContenidoPaquete,
		PC.Cantidad As Cantidad,
		Cast((PC.Cantidad / L.ContenidoPaquete) As Int) * L.ContenidoPaquete As Cantidad_Contemplada, --DateDiff(mm,GetDate(), FechaCaducidad),
		0 As CantidadNoCompleta,
		IdClaveSSA, ClaveSSA, IdClaveSSA_AUX, ClaveSSA_AUX, EsCuadroBasico
	Into #TempInv_Ubica
	From #TempInv_Lotes L
	Inner JOin FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) 
		On (L.IdEmpresa = U.IdEmpresa And L.IdEstado = U.IdEstado And L.IdFarmacia = U.IdFarmacia 
			And L.IdSubFarmacia = U.IdSubFarmacia And L.IdProducto = U.IdProducto And L.CodigoEAN = U.CodigoEAN And L.ClaveLote = U.ClaveLote and L.sku = U.SKU )
	inner join VentaMasiva_Plantilla PC (Nolock) 
		on 
		( 
			U.idEmpresa = PC.IdEmpresa and U.IdEstado = PC.IdEstado and 
			U.IdFarmacia = PC.idfarmacia and U.IdSubFarmacia = PC.idsubFarmacia 
			
			--and U.CodigoEAN = PC.codigoEAN 
			and right('00000000000000000000' + U.CodigoEAN, 20) = right('00000000000000000000' + PC.codigoEAN, 20)  

			and U.ClaveLote = PC.ClaveLote 
			and U.idPasillo = PC.IdPasillo and U.IdEstante = PC.IdEstante and U.IdEntrepaño = PC.IdEntrepano 
			and U.IdSubFarmacia = PC.idsubfarmacia 
			and U.SKU = PC.sku 
			--and L.Existencia = P.Cantidad 
		) 
	Where
		--P.IdClaveSSA_Sal Is null And
		L.ClaveLote not like '%*%' 
		--And  (L.Existencia - L.ExistenciaEnTransito) > 0 
		And DateDiff(mm,GetDate(), FechaCaducidad) >= @MeseCaducidad And--P.ContenidoPaquete > 1 And --Existencia = 16 And
		L.IdEmpresa = @IdEmpresa And L.IdEstado = @IdEstado And L.IdFarmacia = @IdFarmacia 
		--and L.CodigoEAN = '7501122965602' 


	Update #TempInv_Ubica Set CantidadNoCompleta = Cantidad - Cantidad_Contemplada
	----Ubicaciones


	-------
	If (@EsAlmacen = 1)
	Begin 
		print 'borrando' 
		Delete #TempInv_Lotes
		

		Insert Into #TempInv_Lotes
		Select 	
		U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, IdSubFarmacia_Destino, 
		U.SKU, 
		U.IdProducto, U.CodigoEAN, U.ClaveLote, U.ClaveLote As ClaveLote_Consigna,
		U.EsConsignacion, U.CostoPromedio, U.UltimoCosto, FechaCaducidad, ContenidoPaquete, 
		Sum(Cantidad) As Cantidad,
		Sum(Cantidad_Contemplada) As Cantidad_Contemplada, 
		Sum(CantidadNoCompleta) As CantidadNoCompleta,
		TasaIva, Cast(0.0000 as Numeric(18,4)) As SubTotal, Cast(0.0000 as Numeric(18,4)) As Iva, Cast(0.0000 as Numeric(18,4)) As Total, 
		IdClaveSSA, ClaveSSA, IdClaveSSA_AUX, ClaveSSA_AUX,  Renglon, EsCuadroBasico
		From #TempInv_Ubica U
		--Where IdProducto = 2743 --And ClaveLote = 'B18A970'
		Group By U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, U.IdSubFarmacia_Destino, U.sku, U.IdProducto, U.CodigoEAN, U.ClaveLote,
			U.EsConsignacion, U.CostoPromedio, U.UltimoCosto, FechaCaducidad, ContenidoPaquete, TasaIva,
			IdClaveSSA, ClaveSSA, IdClaveSSA_AUX, ClaveSSA_AUX,  Renglon, EsCuadroBasico
	End

	
	UpDate #TempInv_Lotes 
	Set
		SubTotal = (Cantidad * CostoPromedio),
		Iva		 = (Cantidad * CostoPromedio) * (Iva /100),
		ToTal	 = (Cantidad * CostoPromedio) * (1 + (Iva /100))

	Select @SubTotal = Sum(SubTotal), @Iva = Sum(Iva), @Total = Sum(ToTal) From #TempInv_Lotes



	------Tabla CodigosEAN
	Select 
		L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto, L.CodigoEAN, TasaIva,
		Cast(0 As  numeric(14, 4)) As CostoPromedio, Cast (0 As  numeric(14, 4)) As UltimoCosto,
		Sum(Cantidad) As Cantidad, Sum(Cantidad_Contemplada) As Cantidad_Contemplada, Sum(CantidadNoCompleta) As CantidadNoCompleta,
		Sum(SubTotal) As SubTotal, Sum(Iva) As Iva, Sum(Total) As Total,
		ROW_NUMBER() OVER(ORDER BY L.IdProducto, L.CodigoEAN ) AS Renglon
	Into #TempInv_CodigoEAN
	From #TempInv_Lotes L
	Group By L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdProducto, L.CodigoEAN, TasaIva


	Update I Set CostoPromedio = F.CostoPromedio, UltimoCosto = F.UltimoCosto
	From #TempInv_CodigoEAN I
	Inner Join FarmaciaProductos F (Nolock) On ( I.IdEmpresa = F.IdEmpresa And I.IdEstado = F.IdEstado And I.IdFarmacia = F.IdFarmacia And  I.Idproducto = F.IdProducto)


	----Select *
	Update L Set L.Renglon = C.Renglon
	From #TempInv_Lotes L
	Inner Join  #TempInv_CodigoEAN C On (L.IdEmpresa = C.IdEmpresa And L.IdEstado = C.IdEstado And L.IdFarmacia = C.IdFarmacia And L.IdProducto = C.IdProducto And L.CodigoEAN = C.CodigoEAN)

	Update L Set L.Renglon = C.Renglon
	From #TempInv_Ubica L
	Inner Join  #TempInv_CodigoEAN C On (L.IdEmpresa = C.IdEmpresa And L.IdEstado = C.IdEstado And L.IdFarmacia = C.IdFarmacia And L.IdProducto = C.IdProducto And L.CodigoEAN = C.CodigoEAN)
	
	Update #TempInv_Ubica Set Cantidad = Cantidad_Contemplada
	Update #TempInv_Lotes Set Cantidad = Cantidad_Contemplada
	Update #TempInv_CodigoEAN Set Cantidad = Cantidad_Contemplada
	



	-------------------- INSERTAR DATOS FINALES 

	--SELECT SUM(CANTIDAD)  FROM #TempInv_CodigoEAN 
	--SELECT SUM(CANTIDAD)  FROM #TempInv_Lotes 
	--SELECT SUM(CANTIDAD)  FROM #TempInv_Ubica 


	---------------------------------- ENCABEZADOS   
	Insert Into #Folio
	EXEC spp_Mtto_VentasEnc 
                @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @FolioVenta = @FolioVenta, @FechaSistema = @FechaSistema, @IdCaja = @IdCaja,
                @IdPersonal = @IdPersonal, @IdCliente = @IdCliente, @IdSubCliente = @IdSubCliente, @IdPrograma = @IdPrograma, @IdSubPrograma = @IdSubPrograma,
                @SubTotal = @SubTotal, @Iva = @Iva, @Total = @Total, @TipoDeVenta = @TipoDeVenta, @iOpcion = @iOpcion, @Descuento = @Descuento 
				
	Select @FolioVenta = FolioVenta, @FolioMovto = 'SV' + FolioVenta From #Folio 
	

	EXEC spp_Mtto_VentasInformacionAdicional 
                 @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @FolioVenta = @FolioVenta,
				 @IdBeneficiario = @IdBeneficiario, @NumReceta = @NumReceta, @FechaReceta = @FechaReceta,
                 @IdTipoDeDispensacion = @IdTipoDeDispensacion, @IdUnidadMedica = @IdUnidadMedica, @IdMedico = @IdMedico, @IdBeneficioSP = @IdBeneficioSP, @IdDiagnostico = @IdDiagnostico,
                 @IdServicio = @IdServicio, @IdArea = @IdArea, @RefObservaciones = @RefObservaciones, @iOpcion = @iOpcion,  
				 @NumeroDeHabitacion = '', @NumeroDeCama = '', @IdEstadoResidencia = @IdEstado, 
				 @IdTipoDerechoHabiencia = '001' 


	Exec spp_Mtto_MovtoInv_Enc 
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, 
		@FolioMovtoInv = @FolioVenta, @IdTipoMovto_Inv = 'SV', @TipoES = 'S', 
		@Referencia = '', -- @MovtoAplicado varchar(1), 
		@IdPersonal = @IdPersonal, @Observaciones = '.', 
		@SubTotal = @SubTotal, @Iva = @Iva, @Total = @Total, @iOpcion = @iOpcion

	---------------------------------- ENCABEZADOS   

---		rollback tran 




	----------------------------------------------- Detallado

	Declare tmpExistencia  
    Cursor For 
		Select IdProducto, CodigoEAN, Cantidad, CostoPromedio, UltimoCosto, Iva, TasaIva, Renglon 
		From #TempInv_CodigoEAN
		Where Cantidad > 0 --and codigoEAN = '7501765150359' 
	open tmpExistencia
    FETCH NEXT FROM tmpExistencia Into @IdProducto, @CodigoEAN, @Cantidad, @CostoPromedio, @UltimoCosto, @ImpteIva, @TasaIva, @Renglon
        WHILE @@FETCH_STATUS = 0
        BEGIN

		Set @Importe = @Cantidad * @CostoPromedio

	--Select @IdEmpresa As IdEmpresa, @IdEstado As IdEstado, @IdFarmacia As IdFarmacia, @FolioVenta As FolioVenta,
		--@IdProducto As IdProducto,  @CodigoEAN As CodigoEAN, @Renglon As Renglon, 1 As UnidadDeSalida,
		--@Cantidad As Cantidad, 0 As Cant_Devuelta, @Cantidad As Cantidad,
		--@CostoPromedio As CostoPromedio, @UltimoCosto As UltimoCosto, @ImpteIva As ImpteIva, @TasaIva As TasaIva, 0 As PorcDescto, 0 As ImpteDescto, @iOpcion As iOpcion
	Insert Into #FolioMSj
	Exec spp_Mtto_VentasDet
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @FolioVenta = @FolioVenta,
		@IdProducto = @IdProducto,  @CodigoEAN = @CodigoEAN, @Renglon = @Renglon, @UnidadDeSalida = 1, @Cant_Entregada = @Cantidad,
		@Cant_Devuelta = 0, @CantVendida = @Cantidad, @CostoUnitario = @CostoPromedio, @PrecioUnitario = @UltimoCosto, 
		@ImpteIva = @ImpteIva, @TasaIva = @TasaIva, @PorcDescto = 0, @ImpteDescto = 0, @iOpcion = @iOpcion


	Exec spp_Mtto_MovtosInv_Det
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia,
		@FolioMovtoInv = @FolioMovto, @IdProducto = @IdProducto, @CodigoEAN = @CodigoEAN, @UnidadDeSalida = 1, 
		@TasaIva = @TasaIva, @Cantidad = @Cantidad, 
		@Costo = @CostoPromedio, @Importe = @Importe, @Status = 'A'


			FETCH NEXT FROM tmpExistencia Into @IdProducto, @CodigoEAN, @Cantidad, @CostoPromedio, @UltimoCosto, @ImpteIva, @TasaIva, @Renglon
		End
	close tmpExistencia
	deallocate tmpExistencia
	----------------------------------------------- Detallado


	------------------------------------------------- Lotes
---		rollback tran 

	Declare tmpExistencia  
    Cursor For 
		Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, Cantidad, CostoPromedio, UltimoCosto, Iva, TasaIva, Renglon, SKU 
		From #TempInv_Lotes
		Where Cantidad > 0 --and codigoEAN = '7501765150359' 
	open tmpExistencia
    FETCH NEXT FROM tmpExistencia Into @IdSubFarmacia, @IdProducto, @CodigoEAN, @ClaveLote, @Cantidad, @CostoPromedio, @UltimoCosto, @ImpteIva, @TasaIva, @Renglon, @SKU 
        WHILE @@FETCH_STATUS = 0
        BEGIN

		Set @Importe = @Cantidad * @CostoPromedio


		--select 
		--	@IdSubFarmacia as IdSubFarmacia, 
		--	@IdProducto as IdProducto, 
		--	@CodigoEAN as CodigoEAN, 
		--	@ClaveLote as ClaveLote, 
		--	@SKU as SKU, 
		--	@Cantidad as Cantidad, 
		--	@CostoPromedio as CostoPromedio, 
		--	@UltimoCosto as UltimoCosto, @ImpteIva as ImpteIva, @TasaIva as TasaIva, @Renglon as Renglon  

		--Select 
		--	@IdEmpresa IdEmpresa, @IdEstado IdEstado, @IdFarmacia IdFarmacia, @IdSubFarmacia IdSubFarmacia, @FolioVenta FolioVenta,
		--	@IdProducto IdProducto, @CodigoEAN CodigoEAN, @ClaveLote ClaveLote, @Renglon Renglon, @Cantidad CantidadVendida, 
		--	@iOpcion iOpcion

		Exec spp_Mtto_VentasDet_Lotes
			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @IdSubFarmacia = @IdSubFarmacia, @FolioVenta = @FolioVenta,
			@IdProducto = @IdProducto, @CodigoEAN = @CodigoEAN, @ClaveLote = @ClaveLote, @Renglon = @Renglon, @CantidadVendida = @Cantidad, 
			@iOpcion = @iOpcion, @SKU = @SKU 

		Exec spp_Mtto_MovtosInv_DetLotes
			@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia,
			@IdSubFarmacia = @IdSubFarmacia, @FolioMovtoInv = @FolioMovto, @IdProducto = @IdProducto, @CodigoEAN = @CodigoEAN, @ClaveLote = @ClaveLote,
			@Cantidad = @Cantidad, @Costo = @CostoPromedio, @Importe = @Importe, @Status = 'A', @SKU = @SKU  

			FETCH NEXT FROM tmpExistencia Into @IdSubFarmacia, @IdProducto, @CodigoEAN, @ClaveLote, @Cantidad, @CostoPromedio, @UltimoCosto, @ImpteIva, @TasaIva, @Renglon, @SKU 
		End
	close tmpExistencia
	deallocate tmpExistencia
	----------------------------------------------- Lotes


	------------------------------------------------- Ubicaciones

	Declare tmpExistencia  
    Cursor For 
		Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, Cantidad, Renglon, IdPasillo, IdEstante, IdEntrepaño, SKU 
		From #TempInv_Ubica
		Where Cantidad > 0
	open tmpExistencia
    FETCH NEXT FROM tmpExistencia Into @IdSubFarmacia, @IdProducto, @CodigoEAN, @ClaveLote, @Cantidad, @Renglon, @IdPasillo, @IdEstante, @IdEntrepaño, @SKU 
        WHILE @@FETCH_STATUS = 0
        BEGIN

			Exec spp_Mtto_VentasDet_Lotes_Ubicaciones
				@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @IdSubFarmacia = @IdSubFarmacia, @FolioVenta = @FolioVenta,
				@IdProducto = @IdProducto, @CodigoEAN = @CodigoEAN, @ClaveLote = @ClaveLote, @Renglon = @Renglon, 
				@IdPasillo = @IdPasillo, @IdEstante = @IdEstante, @IdEntrepaño = @IdEntrepaño,
				@CantidadVendida  = @Cantidad, @iOpcion = @iOpcion, @SKU = @SKU 

			Exec spp_Mtto_MovtosInv_DetLotes_Ubicaciones
				@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia,
				@IdSubFarmacia = @IdSubFarmacia, @FolioMovtoInv = @FolioMovto, @IdProducto = @IdProducto, @CodigoEAN = @CodigoEAN, @ClaveLote = @ClaveLote,
				@Cantidad = @Cantidad, @IdPasillo = @IdPasillo, @IdEstante = @IdEstante, @IdEntrepaño = @IdEntrepaño, @Status = 'A', @SKU = @SKU 


			FETCH NEXT FROM tmpExistencia Into @IdSubFarmacia, @IdProducto, @CodigoEAN, @ClaveLote, @Cantidad, @Renglon, @IdPasillo, @IdEstante, @IdEntrepaño, @SKU 
		End
	close tmpExistencia
	deallocate tmpExistencia 
	------------------------------------------------- Ubicaciones
	





	Exec spp_INV_AplicarDesaplicarExistencia @IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @FolioMovto = @FolioMovto

	
----------------------------	SALIDA FINAL 

	select @FolioVenta as FolioDeVenta 
	
	select sum(CantidadVendida) From VentasDet (nolock) where FolioVenta = @FolioVenta 
	select sum(CantidadVendida) From VentasDet_Lotes (nolock) where FolioVenta = @FolioVenta 
	select sum(CantidadVendida) From VentasDet_Lotes_Ubicaciones (nolock) where FolioVenta = @FolioVenta 


	select sum(Cantidad) From MovtosInv_Det_CodigosEAN (nolock) where FolioMovtoInv = @FolioMovto  
	select sum(Cantidad) From MovtosInv_Det_CodigosEAN_Lotes (nolock) where FolioMovtoInv = @FolioMovto 
	select sum(Cantidad) From MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones (nolock) where FolioMovtoInv = @FolioMovto 



--		Commit Tran 

--		RollBack Tran 

