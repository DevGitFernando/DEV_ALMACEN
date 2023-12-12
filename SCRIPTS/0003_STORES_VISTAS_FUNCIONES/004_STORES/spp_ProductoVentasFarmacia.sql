---------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_ProductoVentasFarmacia' And xType = 'P' )
	Drop Proc spp_ProductoVentasFarmacia 
Go--#SQL 

/* 

Exec spp_ProductoVentasFarmacia  @Tipo = '2', @IdCliente = '0002', @IdSubCliente = '0006', 
	@IdCodigo = '0008400001207', @CodigoEAN = '8400001207',  @IdEstado = '11', @IdFarmacia = '0005', 
	@EsSectorSalud = '1', @EsClienteIMach = '0', @ClavesRecetaElectronica = '',   @INT_OPM_ProcesoActivo = '0' 




Exec Spp_ProductoVentasFarmacia 
	@Tipo = '2', @IdCliente = '0002', @IdSubCliente = '0005', 
	@IdCodigo = '7506359900020', @CodigoEAN = '7506359900020', 
	@IdEstado = '21', @IdFarmacia = '3224', @EsSectorSalud = '1', @EsClienteIMach = '0', 
	@ClavesRecetaElectronica = '',  
	@INT_OPM_ProcesoActivo = '0', 
	@Validar_ClavesDe_SubPerfil = '1', @IdPrograma = '0202', @IdSubPrograma = '0001' 


*/ 

Create Procedure dbo.spp_ProductoVentasFarmacia 
( 
	@Tipo tinyint = 1, 
	@IdCliente varchar(4) = '0005', @IdSubCliente varchar(4) = '0001', 
	@IdCodigo varchar(30) = '00011969', @CodigoEAN varchar(30) = '', 
	@IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0101', 
	@EsSectorSalud tinyint = 1, @EsClienteIMach tinyint = 0, 
	@ClavesRecetaElectronica varchar(max) = '', @INT_OPM_ProcesoActivo int = 0, @EsCodigoEANClave bit = 0, 
	@Validar_ClavesDe_SubPerfil bit = 0, 
	@IdPrograma varchar(4) = '', @IdSubPrograma varchar(4) = ''  	 
) 
With Encryption 	
As 
Begin 
Declare 
    @sSql varchar(7500),  
    @bSoloCuadroBasico bit, 
    @iLenCodEAN int, 
	@iTipoInterface int, 
	@sClavesRecetaElectronica_Parametro varchar(max) 


	Set @iLenCodEAN = 15 
	Set @iTipoInterface = 0 
	Set @sClavesRecetaElectronica_Parametro = '' 

--- Determinar si la Farmacia tiene restriccion de Claves 
    Select @bSoloCuadroBasico = dbo.fg_Parametro_CTE_DispensarSoloCuadroBasico(@IdEstado, @IdFarmacia) 


--	select @IdCodigo, @CodigoEAN, @IdEstado, @EsSectorSalud 
--- Crear la estructura vacia 
	Select Top 1 
		@IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
		cast(C.ClaveSSA as varchar(20)) as ClaveSSA, 
		cast(C.ClaveSSA as varchar(20)) as ClaveSSA_Auxiliar, 
		CR.CodigoEAN, CR.CodigoEAN_Interno, P.IdProducto, P.Descripcion, TP.PorcIva, (TP.PorcIva / 100.0) as TasaIva, 
		--dbo.fg_CalcularPrecioVenta( @Tipo, @IdCliente, @IdSubCliente, P.IdProducto ) as PrecioVenta, 

		dbo.fg_CalcularPrecioVenta_Comercial( @Tipo, FP.IdEmpresa, FP.IdEstado, FP.IdFarmacia, @IdCliente, @IdSubCliente, P.IdProducto ) as PrecioVenta, 
		cast(0 as numeric(14,4)) as PrecioVenta_ConImpuestos, 
		dbo.fg_CalcularPrecioMinimoDeVenta( @Tipo, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, P.IdProducto ) as PrecioMinimoDeVenta, 
		cast(0 as numeric(14,4)) as PrecioMinimoVenta_ConImpuestos, 
		cast(IsNull(( Select top 1 1 From FarmaciaProductos FP (NoLock) 
	      Where FP.IdEstado = @IdEstado and FP.IdFarmacia = @IdFarmacia and FP.IdProducto = P.IdProducto ),0) as bit) as EsDeFarmacia, 
	    cast(0 as bit) as EsMach4, 
	    cast(0 as bit) as DCB, 
	    cast((case when @Validar_ClavesDe_SubPerfil = 1 then 0 else 1 end) as bit) as DCB__Programa_SubPrograma, 

		UltimoCosto, 
		cast((case when cast(FP.IdProducto as int) = 0 then 1 else 0 end) as bit) as EsDeRecetaElectronica, 
		cast(0 as bit) as EsDeOperacionMaquila  
	Into #tmpDatosProducto     
	From CatProductos_CodigosRelacionados CR (NoLock) 
	Inner Join FarmaciaProductos FP (NoLock) On ( CR.IdProducto = FP.IdProducto )
	Inner Join CatProductos P (NoLock) On ( CR.IdProducto = P.IdProducto ) 
	-- Inner Join CatProductos_Estado PE (NoLock) On ( P.IdProducto = PE.IdProducto And PE.IdEstado = Fp.IdEstado )
	Inner Join CatTiposDeProducto TP (NoLock) On ( P.IdTipoProducto = TP.IdTipoProducto ) 
	Inner Join CatClavesSSA_Sales C (NoLock) On ( P.IdClaveSSA_Sal = C.IdClaveSSA_Sal ) 
	Where 
		( 
			------right(replicate('0', @iLenCodEAN) + CR.CodigoEAN_Interno, @iLenCodEAN) = right(replicate('0', @iLenCodEAN) + @IdCodigo, @iLenCodEAN) 
			------Or 
			------right(replicate('0', @iLenCodEAN) + CR.CodigoEAN, @iLenCodEAN) = right(replicate('0', @iLenCodEAN) + @CodigoEAN, @iLenCodEAN )  
			
			right(replicate('0', @iLenCodEAN) + CR.CodigoEAN_Interno, @iLenCodEAN) = right(replicate('0', @iLenCodEAN) + @IdCodigo, @iLenCodEAN) 
			Or 
			CR.CodigoEAN = @CodigoEAN 
		) 
		And CR.Status = 'A' and P.EsSectorSalud in ( 0, @EsSectorSalud ) And FP.IdEstado = @IdEstado And FP.IdFarmacia = @IdFarmacia


	-----	spp_ProductoVentasFarmacia 				

	Update P Set PrecioVenta_ConImpuestos = round(PrecioVenta * ( 1 + TasaIva ), 2), 
		PrecioMinimoVenta_ConImpuestos = round(PrecioMinimoDeVenta * ( 1 + TasaIva ), 2)  
	From #tmpDatosProducto P (NoLock) 

	Update P Set PrecioVenta = PrecioMinimoDeVenta 
	From #tmpDatosProducto P (NoLock) 
	Where PrecioMinimoVenta_ConImpuestos > PrecioVenta_ConImpuestos 




	--------------------------------------------- Validar catalogo de operaciones maquila (NADRO y otros Clientes externos) 
	---------------- Jesús Díaz  2K150710.2350 
	----If @INT_OPM_ProcesoActivo = 1 
	----Begin 
	----	Update V Set EsDeOperacionMaquila = 1  
	----	From #tmpDatosProducto V (NoLock) 
	----	Inner Join INT_ND_Productos I (NoLock) 
	----		On ( I.IdEstado = V.IdEstado and right(replicate('0', 20) + V.CodigoEAN, 20) = right(replicate('0', 20) + I.CodigoEAN_ND, 20)  ) 
	----	Inner Join INT_ND_CFG_CB_CuadrosBasicos C (NoLock) On ( I.ClaveSSA_ND = C.ClaveSSA_ND  ) 
						
	----	Delete From #tmpDatosProducto Where EsDeOperacionMaquila = 0 			
	----End 
	--------------------------------------------- Validar catalogo de operaciones maquila (NADRO y otros Clientes externos) 


	If @EsClienteIMach = 1 
		Begin  
			If Exists ( Select  * From sysobjects (Nolock) Where Name = 'INT_RobotDispensador' and xType = 'U' ) 
			Begin 
				Select top 1 @iTipoInterface = Interface From INT_RobotDispensador (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Status = 'A' 


				-- select top 10 * from INT_RobotDispensador 


				If @iTipoInterface = 1 
				Begin 
	   				Set @sSql = '' 
					Set @sSql = ' Update T Set EsMach4 = 1 ' + 
								' From #tmpDatosProducto T ' + 
								' Inner Join IMach_CFGC_Clientes_Productos I (NoLock) On ( T.CodigoEAN = I.CodigoEAN  ) '  
					Exec(@sSql) 
				End 

				If @iTipoInterface = 3  
				Begin 
	   				Set @sSql = '' 
					Set @sSql = ' Update T Set EsMach4 = 1 ' + 
								' From #tmpDatosProducto T ' + 
								' Inner Join IGPI_CFGC_Clientes_Productos I (NoLock) On ( T.CodigoEAN = I.CodigoEAN  ) '  
					Exec(@sSql) 
				End 

			End 
		End 


--		select * from INT_RobotDispensador

    If @bSoloCuadroBasico = 1 
	   Begin 
		
			Update T Set ClaveSSA_Auxiliar = R.ClaveSSA 
			From #tmpDatosProducto T 
			Inner Join vw_Relacion_ClavesSSA_Claves R On ( R.IdEstado = @IdEstado and T.ClaveSSA = R.ClaveSSA_Relacionada and R.Status = 'A' ) 

	        Update T Set DCB = 1 
	        From #tmpDatosProducto T 
	        Inner Join vw_Claves_Precios_Asignados CB (NoLock) 
	            On ( CB.IdEstado = @IdEstado and CB.IdCliente = @IdCliente and CB.IdSubCliente = @IdSubCliente and T.ClaveSSA = CB.ClaveSSA and CB.Status in ( 'A' ) ) 
	                
	        -- Delete From #tmpDatosProducto Where DCB = 0 
	   End 


	If @Validar_ClavesDe_SubPerfil = 1 
	Begin 
		Update P Set DCB__Programa_SubPrograma = 1 
		From #tmpDatosProducto P 
		Inner Join vw_CFG_CB_Sub_CuadroBasico_Claves C (NoLock) 
			On ( C.IdEstado = @IdEstado and C.IdFarmacia = @IdFarmacia and C.IdCliente = @IdCliente and C.IdPrograma = @IdPrograma and C.IdSubPrograma = @IdSubPrograma and P.ClaveSSA = C.ClaveSSA )  
	End 
	--- DCB__Programa_SubPrograma 


	-------  Habilitar el producto 0 para emision de vales completos 
    Update T Set DCB = 1 
    From #tmpDatosProducto T 
    Where IdProducto = 0 


--    Select  top 1 *     from vw_Claves_Precios_Asignados 


---------------------- Receta Electrónica  	
	If @ClavesRecetaElectronica <> '' 
	Begin 		

		Set @sClavesRecetaElectronica_Parametro = replace(replace(replace(@ClavesRecetaElectronica, ',', '|'), ' ', ''), char(39), '') 				
		-- Select * From dbo.fg_SplitClaves(@sClavesRecetaElectronica_Parametro) 


		Set @sSql = '' 
		Set @sSql = ' Update T Set EsDeRecetaElectronica = 1, ClaveSSA = C.ClaveSSA ' + 
			' From #tmpDatosProducto T ' + 
			' Inner Join vw_Productos_CodigoEAN C (NoLock) On ( T.CodigoEAN = C.CodigoEAN  ) '  + 
			' Where C.ClaveSSA in ( ' + @ClavesRecetaElectronica + ' ) ' 
				   
		if ( @EsCodigoEANClave = 1 )
		Begin
			Set @sSql = ' Update T Set EsDeRecetaElectronica = 1, ClaveSSA = C.ClaveSSA ' + 
			' From #tmpDatosProducto T ' + 
			' Inner Join vw_Productos_CodigoEAN C (NoLock) On ( T.CodigoEAN = C.CodigoEAN  ) '  + 
			' Where C.CodigoEAN in ( ' + @ClavesRecetaElectronica + ' ) ' 
		End

		Exec(@sSql) 


	   ----If Exists (	Select top 1 * From FarmaciaProductos_CodigoEAN_Lotes (NoLock) 
				----	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and CodigoEAN = and ClaveLote like '%*%' 
				----) 

		Update T Set EsDeRecetaElectronica = 1 
		From #tmpDatosProducto T 
		Inner Join dbo.fg_SplitCadena(@sClavesRecetaElectronica_Parametro) C On ( replace(replace(C.Valor, '.', ''), '-', '')  like '%' + replace(replace(T.ClaveSSA, '.', ''), '-', '') + '%' ) 
		Where EsDeRecetaElectronica = 0  
			
	End 
---------------------- Receta Electrónica  	



	--- Salida Final del Datos 
	Select * -- IdEstado, IdFarmacia, CodigoEAN, CodigoEAN_Interno, IdProducto, Descripcion, PorcIva, PrecioVenta, EsDeFarmacia, EsMach4 
	From #tmpDatosProducto 
	
	
--	Exec spp_ProductoVentasFarmacia '2', '', '', '7501563010091', '7501563010091', '21', '1224', '1', '0', [ '101', '3422' ]  

	   	   
End 
Go--#SQL
 

	