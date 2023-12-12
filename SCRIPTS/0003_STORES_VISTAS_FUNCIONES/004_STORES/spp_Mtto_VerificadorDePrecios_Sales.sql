----------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_VerificadorDePrecios_Sales' And xType = 'P' ) 
	Drop Proc spp_Mtto_VerificadorDePrecios_Sales
Go--#SQL

Create Procedure dbo.spp_Mtto_VerificadorDePrecios_Sales 
( 
	@Tipo tinyint = 1, @IdEmpresa varchar(3) = '002', 
	@IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0013', 
	@IdClaveSSA_Sal varchar(4) = '', @CodigoEAN varchar(30) = '77012678' 

)  --	7501318646223
With Encryption 
As  
Begin  
Declare 
	@IdCliente varchar(4), 
	@IdSubCliente varchar(4), 
	@Renglones int  


	Select @IdCliente = Valor From Net_CFGC_Parametros (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and NombreParametro = 'CtePubGeneral' 
	Select @IdSubCliente = Valor From Net_CFGC_Parametros (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and NombreParametro = 'CteSubPubGeneral' 	

	If IsNull(@IdCliente, '') = '' 
	   Begin 
	      Set @IdCliente = '0001' 
	      Set @IdSubCliente = '0001' 
	   End

	Select Identity(int, 1, 1) as Keyx, 
		IdProducto, 
		CodigoEAN, ClaveSSA, Descripcion as DescripcionProducto, Presentacion, 
		TasaIva as PorcIva, cast(0 as numeric(14,4)) as TasaIva, cast(0 as numeric(14,4)) as ImporteIva, 
		PrecioVenta, cast(0 as numeric(14,4)) as PrecioVenta_ConImpuestos, 
		dbo.fg_CalcularPrecioMinimoDeVenta( @Tipo, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, IdProducto ) as PrecioMinimoDeVenta, 	
		cast(0 as numeric(14,4)) as PrecioMinimoVenta_ConImpuestos, 

		0 as Existencia, 0 as Renglon
	Into #tmpProductos 	
	From vw_Productos_CodigoEAN 
	Where CodigoEAN = @CodigoEAN 
	
		
	Insert Into #tmpProductos 
	( 
		IdProducto, CodigoEAN, ClaveSSA, DescripcionProducto, Presentacion, PorcIva, TasaIva,  ImporteIva, 
		PrecioVenta, PrecioVenta_ConImpuestos, PrecioMinimoDeVenta, PrecioMinimoVenta_ConImpuestos, Existencia, Renglon  
	) 
	Select IdProducto, 
		CodigoEAN, ClaveSSA, DescripcionProducto, Presentacion, R.TasaIva as PorcIva, (max(TasaIVA) / 100.0) as TasaIva, 0 as ImporteIva, 
		
		--max(PrecioVenta) as PrecioVenta, 	
		dbo.fg_CalcularPrecioVenta_Comercial( @Tipo, @IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, IdProducto ) as PrecioVenta, 
		cast(0 as numeric(14,4)) as PrecioVenta_ConImpuestos, 
		dbo.fg_CalcularPrecioMinimoDeVenta( @Tipo, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, IdProducto ) as PrecioMinimoDeVenta, 
		cast(0 as numeric(14,4)) as PrecioMinimoVenta_ConImpuestos, 
		sum(Existencia) as Existencia, 1 as Renglon 
	From 
	( 
		Select E.ClaveSSA, E.IdProducto, E.CodigoEAN, P.TasaIva, E.DescripcionProducto, E.Presentacion, E.ContenidoPaquete,
			-- dbo.fg_CalcularPrecioVenta(@Tipo, @IdCliente, @IdSubCliente, E.IdProducto) as PrecioVenta, 
			sum(E.Existencia) as Existencia 
		From vw_ExistenciaPorCodigoEAN_Lotes E (NoLock) 
		Inner Join vw_Productos P On ( E.IdProducto = P.IdProducto ) 	
		Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia 
			And E.IdClaveSSA_Sal = @IdClaveSSA_Sal -- and CodigoEAN <> @CodigoEAN 
		Group by E.ClaveSSA, E.CodigoEAN, E.DescripcionProducto, E.IdProducto, E.Presentacion, E.ContenidoPaquete, P.TasaIva   
    ) R 
    Group by ClaveSSA, IdProducto, TasaIva, 
		CodigoEAN, DescripcionProducto, Presentacion  
        

    --		spp_Mtto_VerificadorDePrecios_Sales 

	------------------ Verificar precios segun costos 
	--Select * from #tmpProductos 

	Update P Set 
		--PrecioVenta = round(PrecioVenta * ( 1 + TasaIva ), 2), 
		PrecioVenta = round(PrecioVenta * ( 1 + (PorcIVA / 100.00) ), 2), 
		PrecioVenta_ConImpuestos = round(PrecioVenta * ( 1 + (PorcIVA / 100.00) ), 2), 
		PrecioMinimoVenta_ConImpuestos = round(PrecioMinimoDeVenta * ( 1 + (PorcIVA / 100.00) ), 2)  
	From #tmpProductos P (NoLock) 

	----Update P Set 
	----	PrecioVenta = round(PrecioVenta * ( 1 + (PorcIVA / 100.00) ), 2) 
	----From #tmpProductos P (NoLock) 

	Update P Set PrecioVenta = PrecioMinimoDeVenta 
	From #tmpProductos P (NoLock) 
	Where PrecioMinimoVenta_ConImpuestos > PrecioVenta_ConImpuestos 
	------------------ Verificar precios segun costos 

        

	-- Se asigna el 0 al CodigoEAN que se ingreso para que lo muestre primero en el grid. 	
	Select @Renglones = count(*) From #tmpProductos Where CodigoEAN = @CodigoEAN 
	IF  @Renglones > 1 -- exists ( select count(*) From #tmpProductos Where CodigoEAN = @CodigoEAN ) 
	Begin 
		Update #tmpProductos 
		Set Renglon = 3 
		Where CodigoEAN = @CodigoEAN and Renglon = 0 
		
		Delete From #tmpProductos Where Renglon = 3 
	End 

	
	----Delete #tmpProductos 
	----From #tmpProductos 
	----Where Renglon = 3 
	----Group by CodigoEAN 
	------ Having count(distinct CodigoEAN) > 1 
	
	
	
	-- Se asigna el 0 al CodigoEAN que se ingreso para que lo muestre primero en el grid. 	
	Update #tmpProductos 
	Set Renglon = 2
	Where CodigoEAN <> @CodigoEAN 
	        


--------------------------- SALIDA FINAL  
	Select 
		IdProducto, CodigoEAN, DescripcionProducto, Presentacion, PorcIva, 
		--round( (PrecioVenta * (1 + (PorcIva / 100.00))), 2, 1)  as PrecioVenta, 
		round(PrecioVenta, 2, 1) as PrecioVenta, 
		ImporteIva, PrecioVenta, Existencia, Renglon 
		, PrecioVenta_ConImpuestos,   PrecioMinimoVenta_ConImpuestos  
	From #tmpProductos         
	Order By Renglon 

	----Select 
	----	IdProducto, CodigoEAN, DescripcionProducto, Presentacion, PorcIva, 
	----	--round( (PrecioVenta * (1 + (PorcIva / 100.00))), 2, 1)  as PrecioVenta, 
	----	--cast(0 as numeric(14,4)) as PrecioVenta, cast(0 as numeric(14,4))  as ImporteIva, 
	----	--round(PrecioVenta, 2, 1) as PrecioDeVenta, 
	----	ImporteIva, PrecioDeVenta, 
	----	Existencia, Renglon  
	----	--, PrecioVenta_ConImpuestos,   PrecioMinimoVenta_ConImpuestos 
	----From #tmpProductos         
	----Order By Renglon 
	


---		spp_Mtto_VerificadorDePrecios_Sales 	
	
End
Go--#SQL
