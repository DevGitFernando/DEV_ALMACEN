
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

		@FolioTransferencia varchar(20), 
		@TipoTransferencia varchar(10), 

		@IdSubFarmacia_Origen Varchar(2) = '05',
		@IdSubFarmacia_Destino Varchar(2) = '05', 
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

		 

		Set @IdEmpresa = '004' 
		Set @IdEstado = '11' 
		Set @IdFarmacia = '4005' 
		Set @IdFarmacia_Destino = '4002'
		Set @IdSubFarmacia_Origen = '05' 
		Set @IdSubFarmacia_Destino = '05' 
		Set @TipoTransferencia = 'TS' 


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
			IdEmpresa, IdEstado, IdFarmacia, @IdFarmacia_Destino as IdFarmacia_Destino, IdSubFarmacia, IdSubFarmacia as IdSubFarmacia_Destino, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, Existencia, 
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
			--and datediff(month, FechaCaducidad, getdate()) <= 0  

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
			IdEmpresa, IdEstado, IdFarmacia, @IdFarmacia_Destino as IdFarmacia_Destino, IdSubFarmacia, IdSubFarmacia as IdSubFarmacia_Destino, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, 
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
			IdEmpresa, IdEstado, IdFarmacia, IdFarmacia_Destino, IdProducto, CodigoEAN, sum(Existencia) as Existencia, Status, Actualizado, 
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
			IdEmpresa, IdEstado, IdFarmacia, IdFarmacia_Destino, IdProducto, CodigoEAN, 
			TasaIVA, Costo, 
			--CostoPromedio, UltimoCosto
			Status, Actualizado 


		Select 
			IdEmpresa, IdEstado, IdFarmacia, IdFarmacia_Destino, IdProducto, 
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
			IdEmpresa, IdEstado, IdFarmacia, IdFarmacia_Destino, IdProducto, 
			TasaIVA, Costo, 
			--CostoPromedio, UltimoCosto
			Status, Actualizado 


---		sp_listacolumnas	TransferenciasEnc 


---		rollback tran 




Begin tran 


	Set @FolioTransferencia = ''  
	Set @TipoTransferencia = 'TS' 


	Select @FolioTransferencia = max(cast(right(FolioTransferencia, 8) as int)) + 1   From TransferenciasEnc --(NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and TipoTransferencia = @TipoTransferencia 

	Set @FolioTransferencia = IsNull(@FolioTransferencia, '1') 
	--Set @sFoliador = right(replicate('0', 8) + @FolioTransferencia, 8)  
	Set @FolioTransferencia = @TipoTransferencia + right(replicate('0', 8) + @FolioTransferencia, 8) 	



	Insert Into TransferenciasEnc 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdAlmacen, EsTransferenciaAlmacen, FolioTransferencia, FolioMovtoInv, FolioMovtoInvRef, FolioTransferenciaRef, TipoTransferencia, 
		DestinoEsAlmacen, FechaTransferencia, FechaRegistro, IdPersonal, Observaciones, SubTotal, Iva, Total, 
		IdEstadoRecibe, IdFarmaciaRecibe, IdAlmacenRecibe, Status, Actualizado, TransferenciaAplicada, IdPersonalRegistra, FechaAplicada, 
		IdPersonalCancela, FechaCancelacion, TieneDevolucion
	) 
	Select 
		@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
		'00' as IdAlmacen, 0 as EsTransferenciaAlmacen, @FolioTransferencia as FolioTransferencia, @FolioTransferencia as FolioMovtoInv, '' as FolioMovtoInvRef, 
		'' as FolioTransferenciaRef, @TipoTransferencia as TipoTransferencia, 
		'00' as DestinoEsAlmacen, getdate() as FechaTransferencia, getdate() as FechaRegistro, '0001' as IdPersonal, 
		'TRANSFERENCIA MASIVA' as Observaciones, 
		sum(SubTotal) as SubTotal, sum(Iva) as IVA, sum(Total) as Total, 
		@IdEstado as IdEstadoRecibe, @IdFarmacia_Destino as IdFarmaciaRecibe, '00' as IdAlmacenRecibe, 'A' as Status, 0 as Actualizado, 
		0 as TransferenciaAplicada, '0001' as IdPersonalRegistra, getdate() as FechaAplicada, 
		'0001' as IdPersonalCancela, getdate() as FechaCancelacion, 0 as TieneDevolucion
	From PRCS_TRS__004__FarmaciaProductos 


	Insert Into TransferenciasDet 
	( 
		IdEmpresa, IdEstado, IdFarmacia, FolioTransferencia, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, 
		Cant_Enviada, Cant_Devuelta, CantidadEnviada, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe, Status, Actualizado 
	) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, @FolioTransferencia as FolioTransferencia, IdProducto, CodigoEAN, 1 as Renglon, '' as UnidadDeEntrada, 
		sum(Existencia) as Cant_Enviada, 0 as Cant_Devuelta, sum(Existencia) as CantidadEnviada, 
		avg(Costo) as CostoUnitario, max(TasaIva), sum(SubTotal) as SubTotal, sum(IVA) as ImpteIva, sum(Total) as Importe, 'A' as Status, 0 as Actualizado 
	From PRCS_TRS__003__FarmaciaProductos_CodigoEAN
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, IdProducto, CodigoEAN


	Insert Into TransferenciasDet_Lotes 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, IdSubFarmaciaRecibe, FolioTransferencia, 
		IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, CantidadEnviada, Status, Actualizado, CostoUnitario, Cant_Enviada, Cant_Devuelta, SKU
	) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia as IdSubFarmaciaEnvia, IdSubFarmacia as IdSubFarmaciaRecibe, @FolioTransferencia as FolioTransferencia, 
		IdProducto, CodigoEAN, ClaveLote, 1 as Renglon, EsConsignacion, sum(Existencia) as CantidadEnviada, 'A' as Status, 0 as Actualizado, 
		avg(Costo) as CostoUnitario, sum(Existencia) as Cant_Enviada, 0 as Cant_Devuelta, SKU
	From PRCS_TRS__001__FarmaciaProductos_CodigoEAN_Lotes 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
		IdProducto, CodigoEAN, ClaveLote, EsConsignacion, SKU 


	Insert Into TransferenciasDet_Lotes_Ubicaciones 
	( 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, IdSubFarmaciaRecibe, FolioTransferencia, 
		IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, CantidadEnviada, 
		Status, Actualizado, Cant_Enviada, Cant_Devuelta, SKU
	) 
	Select 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia as IdSubFarmaciaEnvia, IdSubFarmacia as IdSubFarmaciaRecibe, @FolioTransferencia as FolioTransferencia, 
		IdProducto, CodigoEAN, ClaveLote, 1 as Renglon, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, sum(Existencia) as CantidadEnviada, 
		'A' as Status, 0 as Actualizado, sum(Existencia) as Cant_Enviada, 0 as Cant_Devuelta, SKU
	From PRCS_TRS__002__FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
	Group by 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
		IdProducto, CodigoEAN, ClaveLote, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, 
		SKU


	------------------------	
	Update F Set ExistenciaEnTransito = Existencia 
	From FarmaciaProductos F 

	Update F Set ExistenciaEnTransito = Existencia 
	From FarmaciaProductos_CodigoEAN F 

	Update F Set ExistenciaEnTransito = Existencia 
	From FarmaciaProductos_CodigoEAN_Lotes F 

	Update F Set ExistenciaEnTransito = Existencia 
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones F 





	------------------------ GENERAR TRANSFERENCIA ENVIO  
	Select * 
	From vw_TransferenciaEnvio_Enc (nolock) 
	Where 
		IdEmpresa = @IdEmpresa and IdEstadoEnvia = @IdEstado and IdFarmaciaEnvia = @IdFarmacia 
		and Folio = @FolioTransferencia 
	
	Exec spp_Mtto_TransferenciasEnvioGenerar 
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @FolioTransferencia = @FolioTransferencia, @IncluirUbicaciones = 1  


	Select * 
	From vw_TransferenciaEnvio_Enc (nolock) 
	Where 
		IdEmpresa = @IdEmpresa and IdEstadoEnvia = @IdEstado and IdFarmaciaEnvia = @IdFarmacia 
		and Folio = @FolioTransferencia  	


	--Where IdEmpresa = @IdEmpresa and IdEstadoEnvia = @IdEstado and IdFarmaciaEnvia = @IdFarmacia and Folio = @FolioTransferencia  	

--Select 'If Not Exists ( Select * From Transferencias_UUID_Registrar Where ' + 'UUID = ' + char(39) + ltrim(rtrim(UUID)) + char(39) + ' and ' + 'IdEmpresa = ' + char(39) + ltrim(rtrim(IdEmpresa)) + char(39) + ' and ' + 'IdEstadoEnvia = ' + char(39) + ltrim(rtrim(IdEstadoEnvia)) + char(39) + ' and ' + 'IdFarmaciaEnvia = ' + char(39) + ltrim(rtrim(IdFarmaciaEnvia)) + char(39) + ' and ' + 'FolioTransferencia = ' + char(39) + ltrim(rtrim(FolioTransferencia)) + char(39) + ' ) ' +    ' Insert Into Transferencias_UUID_Registrar (  UUID, IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdEstadoRecibe, IdFarmaciaRecibe, FolioTransferencia, Status, Actualizado )  Values ( '    + Case When UUID Is Null Then 'Null' Else char(39) + ltrim(rtrim( UUID)) + char(39) End  + ', '   + Case When IdEmpresa Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEmpresa)) + char(39) End  + ', '   + Case When IdEstadoEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstadoEnvia)) + char(39) End  + ', '   + Case When IdFarmaciaEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmaciaEnvia)) + char(39) End  + ', '   + Case When IdEstadoRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstadoRecibe)) + char(39) End  + ', '   + Case When IdFarmaciaRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmaciaRecibe)) + char(39) End  + ', '   + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + ltrim(rtrim( FolioTransferencia)) + char(39) End  + ', '   + Case When Status Is Null Then 'Null' Else char(39) + ltrim(rtrim( Status)) + char(39) End  + ', '   + Case When Actualizado Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Actualizado))) End  +   ' ) '  + ' Else Update Transferencias_UUID_Registrar Set '   + 'IdEstadoRecibe = ' + Case When IdEstadoRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEstadoRecibe)) + char(39) End  + ', '   + 'IdFarmaciaRecibe = ' + Case When IdFarmaciaRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdFarmaciaRecibe)) + char(39) End  + ', '   + 'Status = ' + Case When Status Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( Status)) + char(39) End  + ', '   + 'Actualizado = ' + Case When Actualizado Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Actualizado))) End  + ' Where '   + 'UUID = ' + Case When UUID Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( UUID)) + char(39) End  + ' and '   + 'IdEmpresa = ' + Case When IdEmpresa Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEmpresa)) + char(39) End  + ' and '   + 'IdEstadoEnvia = ' + Case When IdEstadoEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEstadoEnvia)) + char(39) End  + ' and '   + 'IdFarmaciaEnvia = ' + Case When IdFarmaciaEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdFarmaciaEnvia)) + char(39) End  + ' and '   + 'FolioTransferencia = ' + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( FolioTransferencia)) + char(39) End  + space(2)    From Transferencias_UUID_Registrar (NoLock) 
Select 'If Not Exists ( Select * From TransferenciasEnvioEnc Where ' + 'IdEmpresa = ' + char(39) + ltrim(rtrim(IdEmpresa)) + char(39) + ' and ' + 'IdEstadoEnvia = ' + char(39) + ltrim(rtrim(IdEstadoEnvia)) + char(39) + ' and ' + 'IdFarmaciaEnvia = ' + char(39) + ltrim(rtrim(IdFarmaciaEnvia)) + char(39) + ' and ' + 'FolioTransferencia = ' + char(39) + ltrim(rtrim(FolioTransferencia)) + char(39) + ' ) ' +    ' Insert Into TransferenciasEnvioEnc (  IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdAlmacenEnvia, EsTransferenciaAlmacen, FolioTransferencia, FolioMovtoInv, TipoTransferencia, DestinoEsAlmacen, FechaTransferencia, FechaRegistro, IdPersonal, SubTotal, Iva, Total, IdEstadoRecibe, IdFarmaciaRecibe, IdAlmacenRecibe, Status, Actualizado )  Values ( '    + Case When IdEmpresa Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEmpresa)) + char(39) End  + ', '   + Case When IdEstadoEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstadoEnvia)) + char(39) End  + ', '   + Case When IdFarmaciaEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmaciaEnvia)) + char(39) End  + ', '   + Case When IdAlmacenEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdAlmacenEnvia)) + char(39) End  + ', '   + Case When EsTransferenciaAlmacen Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), EsTransferenciaAlmacen))) End  + ', '   + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + ltrim(rtrim( FolioTransferencia)) + char(39) End  + ', '   + Case When FolioMovtoInv Is Null Then 'Null' Else char(39) + ltrim(rtrim( FolioMovtoInv)) + char(39) End  + ', '   + Case When TipoTransferencia Is Null Then 'Null' Else char(39) + ltrim(rtrim( TipoTransferencia)) + char(39) End  + ', '   + Case When DestinoEsAlmacen Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), DestinoEsAlmacen))) End  + ', '   + Case When FechaTransferencia Is Null Then 'null' Else char(39) + ltrim(rtrim(convert(char(20), FechaTransferencia,120))) + char(39) End  + ', '   + Case When FechaRegistro Is Null Then 'null' Else char(39) + ltrim(rtrim(convert(char(20), FechaRegistro,120))) + char(39) End  + ', '   + Case When IdPersonal Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdPersonal)) + char(39) End  + ', '   + Case When SubTotal Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), SubTotal))) End  + ', '   + Case When Iva Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Iva))) End  + ', '   + Case When Total Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Total))) End  + ', '   + Case When IdEstadoRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstadoRecibe)) + char(39) End  + ', '   + Case When IdFarmaciaRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmaciaRecibe)) + char(39) End  + ', '   + Case When IdAlmacenRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdAlmacenRecibe)) + char(39) End  + ', '   + Case When Status Is Null Then 'Null' Else char(39) + ltrim(rtrim( Status)) + char(39) End  + ', '   + Case When Actualizado Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Actualizado))) End  +   ' ) '  + ' Else Update TransferenciasEnvioEnc Set '   + 'IdAlmacenEnvia = ' + Case When IdAlmacenEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdAlmacenEnvia)) + char(39) End  + ', '   + 'EsTransferenciaAlmacen = ' + Case When EsTransferenciaAlmacen Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),EsTransferenciaAlmacen))) End  + ', '   + 'FolioMovtoInv = ' + Case When FolioMovtoInv Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( FolioMovtoInv)) + char(39) End  + ', '   + 'TipoTransferencia = ' + Case When TipoTransferencia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( TipoTransferencia)) + char(39) End  + ', '   + 'DestinoEsAlmacen = ' + Case When DestinoEsAlmacen Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),DestinoEsAlmacen))) End  + ', '   + 'FechaTransferencia = ' + Case When FechaTransferencia Is Null Then 'null' Else char(39) + Ltrim(Rtrim(convert(char(20),FechaTransferencia,120))) + char(39) End  + ', '   + 'FechaRegistro = ' + Case When FechaRegistro Is Null Then 'null' Else char(39) + Ltrim(Rtrim(convert(char(20),FechaRegistro,120))) + char(39) End  + ', '   + 'IdPersonal = ' + Case When IdPersonal Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdPersonal)) + char(39) End  + ', '   + 'SubTotal = ' + Case When SubTotal Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),SubTotal))) End  + ', '   + 'Iva = ' + Case When Iva Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Iva))) End  + ', '   + 'Total = ' + Case When Total Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Total))) End  + ', '   + 'IdEstadoRecibe = ' + Case When IdEstadoRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEstadoRecibe)) + char(39) End  + ', '   + 'IdFarmaciaRecibe = ' + Case When IdFarmaciaRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdFarmaciaRecibe)) + char(39) End  + ', '   + 'IdAlmacenRecibe = ' + Case When IdAlmacenRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdAlmacenRecibe)) + char(39) End  + ', '   + 'Status = ' + Case When Status Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( Status)) + char(39) End  + ', '   + 'Actualizado = ' + Case When Actualizado Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Actualizado))) End  + ' Where '   + 'IdEmpresa = ' + Case When IdEmpresa Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEmpresa)) + char(39) End  + ' and '   + 'IdEstadoEnvia = ' + Case When IdEstadoEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEstadoEnvia)) + char(39) End  + ' and '   + 'IdFarmaciaEnvia = ' + Case When IdFarmaciaEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdFarmaciaEnvia)) + char(39) End  + ' and '   + 'FolioTransferencia = ' + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( FolioTransferencia)) + char(39) End  + space(2)    From TransferenciasEnvioEnc (NoLock) Where IdEmpresa = @IdEmpresa and IdEstadoEnvia = @IdEstado and IdFarmaciaEnvia = @IdFarmacia and FolioTransferencia = @FolioTransferencia  
Select 'If Not Exists ( Select * From TransferenciasEnvioDet Where ' + 'IdEmpresa = ' + char(39) + ltrim(rtrim(IdEmpresa)) + char(39) + ' and ' + 'IdEstadoEnvia = ' + char(39) + ltrim(rtrim(IdEstadoEnvia)) + char(39) + ' and ' + 'IdFarmaciaEnvia = ' + char(39) + ltrim(rtrim(IdFarmaciaEnvia)) + char(39) + ' and ' + 'FolioTransferencia = ' + char(39) + ltrim(rtrim(FolioTransferencia)) + char(39) + ' and ' + 'IdProducto = ' + char(39) + ltrim(rtrim(IdProducto)) + char(39) + ' and ' + 'CodigoEAN = ' + char(39) + ltrim(rtrim(CodigoEAN)) + char(39) + ' and ' + 'Renglon = ' + ltrim(rtrim(convert(varchar(50), Renglon))) + ' ) ' +    ' Insert Into TransferenciasEnvioDet (  IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdEstadoRecibe, IdFarmaciaRecibe, FolioTransferencia, IdProducto, CodigoEAN, Renglon, UnidadDeEntrada, Cant_Enviada, Cant_Devuelta, CantidadEnviada, CostoUnitario, TasaIva, SubTotal, ImpteIva, Importe, Status, Actualizado )  Values ( '    + Case When IdEmpresa Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEmpresa)) + char(39) End  + ', '   + Case When IdEstadoEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstadoEnvia)) + char(39) End  + ', '   + Case When IdFarmaciaEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmaciaEnvia)) + char(39) End  + ', '   + Case When IdEstadoRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstadoRecibe)) + char(39) End  + ', '   + Case When IdFarmaciaRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmaciaRecibe)) + char(39) End  + ', '   + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + ltrim(rtrim( FolioTransferencia)) + char(39) End  + ', '   + Case When IdProducto Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdProducto)) + char(39) End  + ', '   + Case When CodigoEAN Is Null Then 'Null' Else char(39) + ltrim(rtrim( CodigoEAN)) + char(39) End  + ', '   + Case When Renglon Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Renglon))) End  + ', '   + Case When UnidadDeEntrada Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), UnidadDeEntrada))) End  + ', '   + Case When Cant_Enviada Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Cant_Enviada))) End  + ', '   + Case When Cant_Devuelta Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Cant_Devuelta))) End  + ', '   + Case When CantidadEnviada Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), CantidadEnviada))) End  + ', '   + Case When CostoUnitario Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), CostoUnitario))) End  + ', '   + Case When TasaIva Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), TasaIva))) End  + ', '   + Case When SubTotal Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), SubTotal))) End  + ', '   + Case When ImpteIva Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), ImpteIva))) End  + ', '   + Case When Importe Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Importe))) End  + ', '   + Case When Status Is Null Then 'Null' Else char(39) + ltrim(rtrim( Status)) + char(39) End  + ', '   + Case When Actualizado Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Actualizado))) End  +   ' ) '  + ' Else Update TransferenciasEnvioDet Set '   + 'IdEstadoRecibe = ' + Case When IdEstadoRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEstadoRecibe)) + char(39) End  + ', '   + 'IdFarmaciaRecibe = ' + Case When IdFarmaciaRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdFarmaciaRecibe)) + char(39) End  + ', '   + 'UnidadDeEntrada = ' + Case When UnidadDeEntrada Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),UnidadDeEntrada))) End  + ', '   + 'Cant_Enviada = ' + Case When Cant_Enviada Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Cant_Enviada))) End  + ', '   + 'Cant_Devuelta = ' + Case When Cant_Devuelta Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Cant_Devuelta))) End  + ', '   + 'CantidadEnviada = ' + Case When CantidadEnviada Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),CantidadEnviada))) End  + ', '   + 'CostoUnitario = ' + Case When CostoUnitario Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),CostoUnitario))) End  + ', '   + 'TasaIva = ' + Case When TasaIva Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),TasaIva))) End  + ', '   + 'SubTotal = ' + Case When SubTotal Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),SubTotal))) End  + ', '   + 'ImpteIva = ' + Case When ImpteIva Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),ImpteIva))) End  + ', '   + 'Importe = ' + Case When Importe Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Importe))) End  + ', '   + 'Status = ' + Case When Status Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( Status)) + char(39) End  + ', '   + 'Actualizado = ' + Case When Actualizado Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Actualizado))) End  + ' Where '   + 'IdEmpresa = ' + Case When IdEmpresa Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEmpresa)) + char(39) End  + ' and '   + 'IdEstadoEnvia = ' + Case When IdEstadoEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEstadoEnvia)) + char(39) End  + ' and '   + 'IdFarmaciaEnvia = ' + Case When IdFarmaciaEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdFarmaciaEnvia)) + char(39) End  + ' and '   + 'FolioTransferencia = ' + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( FolioTransferencia)) + char(39) End  + ' and '   + 'IdProducto = ' + Case When IdProducto Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdProducto)) + char(39) End  + ' and '   + 'CodigoEAN = ' + Case When CodigoEAN Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( CodigoEAN)) + char(39) End  + ' and '   + 'Renglon = ' + Case When Renglon Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Renglon))) End  + space(2)    From TransferenciasEnvioDet (NoLock) Where IdEmpresa = @IdEmpresa and IdEstadoEnvia = @IdEstado and IdFarmaciaEnvia = @IdFarmacia and FolioTransferencia = @FolioTransferencia  

Select 'If Not Exists ( Select * From TransferenciasEnvioDet_Lotes Where ' + 'IdEmpresa = ' + char(39) + ltrim(rtrim(IdEmpresa)) + char(39) + ' and ' + 'IdEstadoEnvia = ' + char(39) + ltrim(rtrim(IdEstadoEnvia)) + char(39) + ' and ' + 'IdFarmaciaEnvia = ' + char(39) + ltrim(rtrim(IdFarmaciaEnvia)) + char(39) + ' and ' + 'IdSubFarmaciaEnvia = ' + char(39) + ltrim(rtrim(IdSubFarmaciaEnvia)) + char(39) + ' and ' + 'FolioTransferencia = ' + char(39) + ltrim(rtrim(FolioTransferencia)) + char(39) + ' and ' + 'IdProducto = ' + char(39) + ltrim(rtrim(IdProducto)) + char(39) + ' and ' + 'CodigoEAN = ' + char(39) + ltrim(rtrim(CodigoEAN)) + char(39) + ' and ' + 'ClaveLote = ' + char(39) + ltrim(rtrim(ClaveLote)) + char(39) + ' and ' + 'Renglon = ' + ltrim(rtrim(convert(varchar(50), Renglon))) + ' and ' + 'SKU = ' + char(39) + ltrim(rtrim(SKU)) + char(39) + ' ) ' +    ' Insert Into TransferenciasEnvioDet_Lotes (  IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, IdEstadoRecibe, IdFarmaciaRecibe, IdSubFarmaciaRecibe, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, CantidadEnviada, Status, Actualizado, Cant_Enviada, Cant_Devuelta, CostoUnitario, SKU )  Values ( '    + Case When IdEmpresa Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEmpresa)) + char(39) End  + ', '   + Case When IdEstadoEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstadoEnvia)) + char(39) End  + ', '   + Case When IdFarmaciaEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmaciaEnvia)) + char(39) End  + ', '   + Case When IdSubFarmaciaEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdSubFarmaciaEnvia)) + char(39) End  + ', '   + Case When IdEstadoRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstadoRecibe)) + char(39) End  + ', '   + Case When IdFarmaciaRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmaciaRecibe)) + char(39) End  + ', '   + Case When IdSubFarmaciaRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdSubFarmaciaRecibe)) + char(39) End  + ', '   + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + ltrim(rtrim( FolioTransferencia)) + char(39) End  + ', '   + Case When IdProducto Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdProducto)) + char(39) End  + ', '   + Case When CodigoEAN Is Null Then 'Null' Else char(39) + ltrim(rtrim( CodigoEAN)) + char(39) End  + ', '   + Case When ClaveLote Is Null Then 'Null' Else char(39) + ltrim(rtrim( ClaveLote)) + char(39) End  + ', '   + Case When Renglon Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Renglon))) End  + ', '   + Case When EsConsignacion Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), EsConsignacion))) End  + ', '   + Case When CantidadEnviada Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), CantidadEnviada))) End  + ', '   + Case When Status Is Null Then 'Null' Else char(39) + ltrim(rtrim( Status)) + char(39) End  + ', '   + Case When Actualizado Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Actualizado))) End  + ', '   + Case When Cant_Enviada Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Cant_Enviada))) End  + ', '   + Case When Cant_Devuelta Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Cant_Devuelta))) End  + ', '   + Case When CostoUnitario Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), CostoUnitario))) End  + ', '   + Case When SKU Is Null Then 'Null' Else char(39) + ltrim(rtrim( SKU)) + char(39) End  +   ' ) '  + ' Else Update TransferenciasEnvioDet_Lotes Set '   + 'IdEstadoRecibe = ' + Case When IdEstadoRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEstadoRecibe)) + char(39) End  + ', '   + 'IdFarmaciaRecibe = ' + Case When IdFarmaciaRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdFarmaciaRecibe)) + char(39) End  + ', '   + 'IdSubFarmaciaRecibe = ' + Case When IdSubFarmaciaRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdSubFarmaciaRecibe)) + char(39) End  + ', '   + 'EsConsignacion = ' + Case When EsConsignacion Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),EsConsignacion))) End  + ', '   + 'CantidadEnviada = ' + Case When CantidadEnviada Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),CantidadEnviada))) End  + ', '   + 'Status = ' + Case When Status Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( Status)) + char(39) End  + ', '   + 'Actualizado = ' + Case When Actualizado Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Actualizado))) End  + ', '   + 'Cant_Enviada = ' + Case When Cant_Enviada Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Cant_Enviada))) End  + ', '   + 'Cant_Devuelta = ' + Case When Cant_Devuelta Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Cant_Devuelta))) End  + ', '   + 'CostoUnitario = ' + Case When CostoUnitario Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),CostoUnitario))) End  + ' Where '   + 'IdEmpresa = ' + Case When IdEmpresa Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEmpresa)) + char(39) End  + ' and '   + 'IdEstadoEnvia = ' + Case When IdEstadoEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEstadoEnvia)) + char(39) End  + ' and '   + 'IdFarmaciaEnvia = ' + Case When IdFarmaciaEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdFarmaciaEnvia)) + char(39) End  + ' and '   + 'IdSubFarmaciaEnvia = ' + Case When IdSubFarmaciaEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdSubFarmaciaEnvia)) + char(39) End  + ' and '   + 'FolioTransferencia = ' + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( FolioTransferencia)) + char(39) End  + ' and '   + 'IdProducto = ' + Case When IdProducto Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdProducto)) + char(39) End  + ' and '   + 'CodigoEAN = ' + Case When CodigoEAN Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( CodigoEAN)) + char(39) End  + ' and '   + 'ClaveLote = ' + Case When ClaveLote Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( ClaveLote)) + char(39) End  + ' and '   + 'Renglon = ' + Case When Renglon Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Renglon))) End  + ' and '   + 'SKU = ' + Case When SKU Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( SKU)) + char(39) End  + space(2)    From TransferenciasEnvioDet_Lotes (NoLock) Where IdEmpresa = @IdEmpresa and IdEstadoEnvia = @IdEstado and IdFarmaciaEnvia = @IdFarmacia and FolioTransferencia = @FolioTransferencia  
Select 'Insert Into TransferenciasEnvioDet_Lotes_Ubicaciones (  IdEmpresa, IdEstado, IdFarmacia, IdSubFarmaciaEnvia, IdSubFarmaciaRecibe, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, IdPasillo, IdEstante, IdEntrepaño, CantidadEnviada, Status, Actualizado, Cant_Enviada, Cant_Devuelta, SKU )  Values ( '    + Case When IdEmpresa Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEmpresa)) + char(39) End  + ', '   + Case When IdEstado Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstado)) + char(39) End  + ', '   + Case When IdFarmacia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmacia)) + char(39) End  + ', '   + Case When IdSubFarmaciaEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdSubFarmaciaEnvia)) + char(39) End  + ', '   + Case When IdSubFarmaciaRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdSubFarmaciaRecibe)) + char(39) End  + ', '   + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + ltrim(rtrim( FolioTransferencia)) + char(39) End  + ', '   + Case When IdProducto Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdProducto)) + char(39) End  + ', '   + Case When CodigoEAN Is Null Then 'Null' Else char(39) + ltrim(rtrim( CodigoEAN)) + char(39) End  + ', '   + Case When ClaveLote Is Null Then 'Null' Else char(39) + ltrim(rtrim( ClaveLote)) + char(39) End  + ', '   + Case When Renglon Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Renglon))) End  + ', '   + Case When EsConsignacion Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), EsConsignacion))) End  + ', '   + Case When IdPasillo Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), IdPasillo))) End  + ', '   + Case When IdEstante Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), IdEstante))) End  + ', '   + Case When IdEntrepaño Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), IdEntrepaño))) End  + ', '   + Case When CantidadEnviada Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), CantidadEnviada))) End  + ', '   + Case When Status Is Null Then 'Null' Else char(39) + ltrim(rtrim( Status)) + char(39) End  + ', '   + Case When Actualizado Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Actualizado))) End  + ', '   + Case When Cant_Enviada Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Cant_Enviada))) End  + ', '   + Case When Cant_Devuelta Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Cant_Devuelta))) End  + ', '   + Case When SKU Is Null Then 'Null' Else char(39) + ltrim(rtrim( SKU)) + char(39) End  +   ' ) ' + space(2)    From TransferenciasEnvioDet_Lotes_Ubicaciones (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioTransferencia = @FolioTransferencia  
Select 'If Not Exists ( Select * From TransferenciasEnvioDet_LotesRegistrar Where ' + 'IdEmpresa = ' + char(39) + ltrim(rtrim(IdEmpresa)) + char(39) + ' and ' + 'IdEstadoEnvia = ' + char(39) + ltrim(rtrim(IdEstadoEnvia)) + char(39) + ' and ' + 'IdFarmaciaEnvia = ' + char(39) + ltrim(rtrim(IdFarmaciaEnvia)) + char(39) + ' and ' + 'IdSubFarmaciaEnvia = ' + char(39) + ltrim(rtrim(IdSubFarmaciaEnvia)) + char(39) + ' and ' + 'FolioTransferencia = ' + char(39) + ltrim(rtrim(FolioTransferencia)) + char(39) + ' and ' + 'IdProducto = ' + char(39) + ltrim(rtrim(IdProducto)) + char(39) + ' and ' + 'CodigoEAN = ' + char(39) + ltrim(rtrim(CodigoEAN)) + char(39) + ' and ' + 'ClaveLote = ' + char(39) + ltrim(rtrim(ClaveLote)) + char(39) + ' and ' + 'Renglon = ' + ltrim(rtrim(convert(varchar(50), Renglon))) + ' and ' + 'SKU = ' + char(39) + ltrim(rtrim(SKU)) + char(39) + ' ) ' +    ' Insert Into TransferenciasEnvioDet_LotesRegistrar (  IdEmpresa, IdEstadoEnvia, IdFarmaciaEnvia, IdSubFarmaciaEnvia, IdEstadoRecibe, IdFarmaciaRecibe, IdSubFarmaciaRecibe, FolioTransferencia, IdProducto, CodigoEAN, ClaveLote, Renglon, EsConsignacion, Existencia, FechaCaducidad, FechaRegistro, Status, Actualizado, CostoUnitario, SKU )  Values ( '    + Case When IdEmpresa Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEmpresa)) + char(39) End  + ', '   + Case When IdEstadoEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstadoEnvia)) + char(39) End  + ', '   + Case When IdFarmaciaEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmaciaEnvia)) + char(39) End  + ', '   + Case When IdSubFarmaciaEnvia Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdSubFarmaciaEnvia)) + char(39) End  + ', '   + Case When IdEstadoRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdEstadoRecibe)) + char(39) End  + ', '   + Case When IdFarmaciaRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdFarmaciaRecibe)) + char(39) End  + ', '   + Case When IdSubFarmaciaRecibe Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdSubFarmaciaRecibe)) + char(39) End  + ', '   + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + ltrim(rtrim( FolioTransferencia)) + char(39) End  + ', '   + Case When IdProducto Is Null Then 'Null' Else char(39) + ltrim(rtrim( IdProducto)) + char(39) End  + ', '   + Case When CodigoEAN Is Null Then 'Null' Else char(39) + ltrim(rtrim( CodigoEAN)) + char(39) End  + ', '   + Case When ClaveLote Is Null Then 'Null' Else char(39) + ltrim(rtrim( ClaveLote)) + char(39) End  + ', '   + Case When Renglon Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Renglon))) End  + ', '   + Case When EsConsignacion Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), EsConsignacion))) End  + ', '   + Case When Existencia Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Existencia))) End  + ', '   + Case When FechaCaducidad Is Null Then 'null' Else char(39) + ltrim(rtrim(convert(char(20), FechaCaducidad,120))) + char(39) End  + ', '   + Case When FechaRegistro Is Null Then 'null' Else char(39) + ltrim(rtrim(convert(char(20), FechaRegistro,120))) + char(39) End  + ', '   + Case When Status Is Null Then 'Null' Else char(39) + ltrim(rtrim( Status)) + char(39) End  + ', '   + Case When Actualizado Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), Actualizado))) End  + ', '   + Case When CostoUnitario Is Null Then 'Null' Else ltrim(rtrim(convert(varchar(50), CostoUnitario))) End  + ', '   + Case When SKU Is Null Then 'Null' Else char(39) + ltrim(rtrim( SKU)) + char(39) End  +   ' ) '  + ' Else Update TransferenciasEnvioDet_LotesRegistrar Set '   + 'IdEstadoRecibe = ' + Case When IdEstadoRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEstadoRecibe)) + char(39) End  + ', '   + 'IdFarmaciaRecibe = ' + Case When IdFarmaciaRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdFarmaciaRecibe)) + char(39) End  + ', '   + 'IdSubFarmaciaRecibe = ' + Case When IdSubFarmaciaRecibe Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdSubFarmaciaRecibe)) + char(39) End  + ', '   + 'EsConsignacion = ' + Case When EsConsignacion Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),EsConsignacion))) End  + ', '   + 'Existencia = ' + Case When Existencia Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Existencia))) End  + ', '   + 'FechaCaducidad = ' + Case When FechaCaducidad Is Null Then 'null' Else char(39) + Ltrim(Rtrim(convert(char(20),FechaCaducidad,120))) + char(39) End  + ', '   + 'FechaRegistro = ' + Case When FechaRegistro Is Null Then 'null' Else char(39) + Ltrim(Rtrim(convert(char(20),FechaRegistro,120))) + char(39) End  + ', '   + 'Status = ' + Case When Status Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( Status)) + char(39) End  + ', '   + 'Actualizado = ' + Case When Actualizado Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Actualizado))) End  + ', '   + 'CostoUnitario = ' + Case When CostoUnitario Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),CostoUnitario))) End  + ' Where '   + 'IdEmpresa = ' + Case When IdEmpresa Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEmpresa)) + char(39) End  + ' and '   + 'IdEstadoEnvia = ' + Case When IdEstadoEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdEstadoEnvia)) + char(39) End  + ' and '   + 'IdFarmaciaEnvia = ' + Case When IdFarmaciaEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdFarmaciaEnvia)) + char(39) End  + ' and '   + 'IdSubFarmaciaEnvia = ' + Case When IdSubFarmaciaEnvia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdSubFarmaciaEnvia)) + char(39) End  + ' and '   + 'FolioTransferencia = ' + Case When FolioTransferencia Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( FolioTransferencia)) + char(39) End  + ' and '   + 'IdProducto = ' + Case When IdProducto Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( IdProducto)) + char(39) End  + ' and '   + 'CodigoEAN = ' + Case When CodigoEAN Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( CodigoEAN)) + char(39) End  + ' and '   + 'ClaveLote = ' + Case When ClaveLote Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( ClaveLote)) + char(39) End  + ' and '   + 'Renglon = ' + Case When Renglon Is Null Then 'Null' Else Ltrim(Rtrim(convert(varchar(50),Renglon))) End  + ' and '   + 'SKU = ' + Case When SKU Is Null Then 'Null' Else char(39) + Ltrim(Rtrim( SKU)) + char(39) End  + space(2)    From TransferenciasEnvioDet_LotesRegistrar (NoLock) Where IdEmpresa = @IdEmpresa and IdEstadoEnvia = @IdEstado and IdFarmaciaEnvia = @IdFarmacia and FolioTransferencia = @FolioTransferencia  


---		sp_listacolumnas	TransferenciasDet_Lotes_Ubicaciones 



	---------- Salida final 
	Select @FolioTransferencia as Folio 


--			rollback tran   

--			commit tran   
	
