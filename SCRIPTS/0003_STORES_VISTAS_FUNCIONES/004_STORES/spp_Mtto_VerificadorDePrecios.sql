----------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_VerificadorDePrecios' And xType = 'P' ) 
	Drop Proc spp_Mtto_VerificadorDePrecios
Go--#SQL

Create Procedure dbo.spp_Mtto_VerificadorDePrecios   
( 
	@Tipo tinyint = 1, @IdEmpresa varchar(3) = '002', 
	@IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0013', @CodigoEAN varchar(30) = '77012678' --'7501318646223' 
)  
With Encryption 
As  
Begin  
Declare 
	@IdCliente varchar(4), 
	@IdSubCliente varchar(4) 


	Select @IdCliente = Valor From Net_CFGC_Parametros (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and NombreParametro = 'CtePubGeneral' 
	Select @IdSubCliente = Valor From Net_CFGC_Parametros (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and NombreParametro = 'CteSubPubGeneral' 	

	If IsNull(@IdCliente, '') = '' 
	   Begin 
	      Set @IdCliente = '0001' 
	      Set @IdSubCliente = '0001' 	       
	   End 
	
	
	Select Identity(int, 1, 1) as Keyx, 
		CodigoEAN, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, Descripcion as DescripcionProducto, Presentacion, 
		TasaIva as PorcIva, cast(0 as numeric(14,4)) as TasaIva, 
		PrecioVenta, cast(0 as numeric(14,4)) as PrecioVenta_ConImpuestos, 
		PrecioVenta as PrecioMinimoDeVenta, cast(0 as numeric(14,4)) as PrecioMinimoVenta_ConImpuestos,
		0 as Existencia, 0 as Renglon 
	Into #tmpProductos 	
	From vw_Productos_CodigoEAN 
	Where CodigoEAN = @CodigoEAN 
		
  --		spp_Mtto_VerificadorDePrecios 
	
	Insert Into #tmpProductos 
	( 
		CodigoEAN, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, DescripcionProducto, Presentacion, PorcIva, TasaIva, 
		PrecioVenta, PrecioVenta_ConImpuestos, PrecioMinimoDeVenta, PrecioMinimoVenta_ConImpuestos, Existencia, Renglon  
	) 
	Select CodigoEAN, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, DescripcionProducto, Presentacion, 
		max(TasaIVA) as PorcIVA, 
		(max(TasaIVA) / 100.0) as TasaIva, 
		-- dbo.fg_CalcularPrecioVenta_Comercial(@Tipo, @IdCliente, @IdSubCliente, IdProducto) as PrecioVenta, 
		-- dbo.fg_CalcularPrecioMinimoDeVenta(1, E.IdEstado, E.IdFarmacia, @IdCliente, @IdSubCliente, P.IdProducto) as PrecioMinimoDeVenta,  

		--dbo.fg_CalcularPrecioVenta_Comercial(@Tipo, @IdCliente, @IdSubCliente, IdProducto) as PrecioVenta, 
		dbo.fg_CalcularPrecioVenta_Comercial( @Tipo, @IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente,IdProducto ) as PrecioVenta, 
		cast(0 as numeric(14,4)) as PrecioVenta_ConImpuestos, 
		dbo.fg_CalcularPrecioMinimoDeVenta( @Tipo, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, IdProducto ) as PrecioMinimoDeVenta, 
		cast(0 as numeric(14,4)) as PrecioMinimoVenta_ConImpuestos, 


		sum(Existencia) as Existencia, 1 as Renglon  
	From vw_ExistenciaPorCodigoEAN_Lotes (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And CodigoEAN = @CodigoEAN 
	group by CodigoEAN, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, DescripcionProducto, Presentacion 
  
	
	------------------ Verificar precios segun costos 
	-----Select * from #tmpProductos 

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



	-------------------------------------------------------------------------- 
	If exists ( Select * From  #tmpProductos Where Renglon = 1  ) 
	Begin
		Delete From  #tmpProductos Where Renglon = 0 
	End 
  


 	--------------------------------------------------------------------------  
	------- SALIDA FINAL    
	Select CodigoEAN, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, DescripcionProducto, PorcIVA, 
		--round((PrecioVenta * ( 1 + ( PorcIva / 100.00) ) ), 2, 1) as PrecioVenta, 
		round(PrecioVenta, 2, 1) as PrecioVenta, 
		Existencia, Renglon 
		, PrecioVenta_ConImpuestos,   PrecioMinimoVenta_ConImpuestos 
	From #tmpProductos 
	Order by CodigoEAN, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, DescripcionProducto
  
  --		spp_Mtto_VerificadorDePrecios 
  
End 
Go--#SQL 	


