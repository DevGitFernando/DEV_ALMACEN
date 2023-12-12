

--RollBack Tran
--Go 

--		Begin Tran 

Go 


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


		Set @SKU = '' 



	if exists ( select * from sysobjects (nolock) Where Name = 'TempInv_Lotes' and xType = 'U' ) Drop table TempInv_Lotes  
	if exists ( select * from sysobjects (nolock) Where Name = 'TempInv_Ubica' and xType = 'U' ) Drop table TempInv_Ubica  



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
	Into TempInv_Lotes
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

	Update TempInv_Lotes Set CantidadNoCompleta = Cantidad - Cantidad_Contemplada


	Select
		U.IdEmpresa, U.IdEstado, U.IdFarmacia, U.IdSubFarmacia, IdSubFarmacia_Destino, U.SKU, U.IdProducto, U.CodigoEAN, U.ClaveLote, U.ClaveLote As ClaveLote_Consigna,
		U.EsConsignacion, TasaIva, L.CostoPromedio, L.UltimoCosto, FechaCaducidad, U.IdPasillo, U.IdEstante, U.IdEntrepaño, Renglon, ContenidoPaquete,
		PC.Cantidad As Cantidad,
		Cast((PC.Cantidad / L.ContenidoPaquete) As Int) * L.ContenidoPaquete As Cantidad_Contemplada, --DateDiff(mm,GetDate(), FechaCaducidad),
		0 As CantidadNoCompleta,
		IdClaveSSA, ClaveSSA, IdClaveSSA_AUX, ClaveSSA_AUX, EsCuadroBasico 
	Into TempInv_Ubica
	From TempInv_Lotes L
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


Go 

	select sum(cantidad),count(*) 
	from TempInv_Lotes 

	select sum(cantidad),count(*) 
	from TempInv_Ubica 

go 

	--select * 
	--From VentaMasiva_Plantilla PC
	--left join TempInv_Ubica U (Nolock) 
	--	on 
	--	( 
	--		U.idEmpresa = PC.IdEmpresa and U.IdEstado = PC.IdEstado and 
	--		U.IdFarmacia = PC.idfarmacia and U.IdSubFarmacia = PC.idsubFarmacia 
			
	--		--and U.CodigoEAN = PC.codigoEAN 
	--		and right('00000000000000000000' + U.CodigoEAN, 20) = right('00000000000000000000' + PC.codigoEAN, 20)  

	--		and U.ClaveLote = PC.ClaveLote 
	--		and U.idPasillo = PC.IdPasillo and U.IdEstante = PC.IdEstante and U.IdEntrepaño = PC.IdEntrepano 
	--		and U.IdSubFarmacia = PC.idsubfarmacia 
	--		and U.SKU = PC.sku 
	--		--and L.Existencia = P.Cantidad 
	--	) 
	--where U.IdEmpresa is null


	--select *
	--from FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
	--where SKU = '20230511-134658-4-11-5002-EOC-371' and CodigoEAN = '7501324402950' and ClaveLote = 'SB555' 

