If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_CteUnidad_Admon_ExistenciaFarmacias' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteUnidad_Admon_ExistenciaFarmacias 
Go--#SQL 

Create Proc spp_Rpt_CteUnidad_Admon_ExistenciaFarmacias 
(   
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005',  
	@IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0101', @iOpcion smallint = 0, @TipoInsumo tinyint = 0,
	@TipoDispensacion tinyint = 0, @TipoClave tinyint = 0, @NivelFarmacia tinyint = 1 )  
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sEncPricipal varchar(500), 
	@sEncSecundario varchar(500),
	@sCondicion varchar(1000),
	@sCondicionTipoClave varchar(5000),
	@sQuery varchar(7500),
	@sQuery2 varchar(7500)

	Select @sEncPricipal = EncabezadoPrincipal, @sEncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 
        Set @sEncPricipal = IsNull(@sEncPricipal, 'SISTEMA INTEGRAL DE INFORMACIÓN') 
        Set @sEncSecundario = IsNull(@sEncSecundario, 'ADMINISTRACION UNIDAD')     

	Set @sCondicion = '' 
	Set @sCondicionTipoClave = ''
	
	If @iOpcion = 0
		Set @sCondicion = ' '
	
	If @iOpcion = 1
		Set @sCondicion = ' and F.Existencia > 0'

	If @iOpcion = 2
		Set @sCondicion = ' and F.Existencia = 0'
		
		
----------- Se obtienen las claves del cuadro basico del nivel al que pertenece la farmacia
	Select * 
	Into #tmpCuadroFarmacia 
	From vw_CB_CuadroBasico_Farmacias (NoLock) 
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 


	
	If @TipoClave = 1
	  Begin
	    Set @sCondicionTipoClave = ' And F.ClaveSSA In ( Select ClaveSSA From #tmpCuadroFarmacia(NoLock) ) '
	  End
	  
	If @TipoClave = 2
	  Begin
	    Set @sCondicionTipoClave = ' And F.ClaveSSA Not In ( Select ClaveSSA From #tmpCuadroFarmacia(NoLock) ) '
	  End

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpExistenciaConcentradoUnidad' and xType = 'U' )
	   Drop Table tmpExistenciaConcentradoUnidad 
	
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CteUnidad_Admon_ExistenciaFarmacia' and xType = 'U' )
	   Drop Table Rpt_CteUnidad_Admon_ExistenciaFarmacia

	Begin 
		Set @sQuery = ' Select ' + char(39) + @sEncPricipal + char(39) + ' as EncabezadoPrincipal, ' + char(39) + @sEncSecundario + char(39) + ' as EncabezadoSecundario, ' +  	
		' IdEstado, Estado, IdFarmacia, Farmacia, ClaveLote, IdProducto, DescripcionProducto As Descripcion, TasaIva, ' +
		' IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, DescripcionSal As DescripcionClave, IdPresentacion_ClaveSSA, Presentacion_ClaveSSA,  ' + 
		' ( Case Sum( Existencia )  When 0 Then ''NO'' Else ''SI'' End ) As Existencia, Sum(Existencia) As Cantidad, ' +
		' ( Case When ClaveLote like ''%*%'' Then Existencia Else 0 End ) As CantidadConsignacion, ' +
		' ( Case When ClaveLote not like ''%*%'' Then Existencia Else 0 End ) As CantidadVenta ' + 
		' Into tmpExistenciaConcentradoUnidad ' +
		' From vw_ExistenciaPorCodigoEAN_Lotes F (Nolock) ' + 
		' Where  IdEstado = ' + char(39) + @IdEstado + char(39) + ' And IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + 
		' ' + @sCondicion + @sCondicionTipoClave + 
		' Group By IdEstado, Estado, IdFarmacia, Farmacia, ClaveLote, IdProducto, DescripcionProducto, TasaIva, ' +		
		' IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, Existencia ' +
		' Order By DescripcionSal, DescripcionProducto '  
		Exec (@sQuery)

	
		--- Remover los Lotes segun sea el caso 
		If @TipoDispensacion <> 0 
		   Begin 
			  If @TipoDispensacion = 1 
				 Delete From tmpExistenciaConcentradoUnidad Where ClaveLote Not Like '%*%'  -- Consignacion 

			  If @TipoDispensacion = 2
				 Delete From tmpExistenciaConcentradoUnidad Where ClaveLote Like '%*%'  -- Venta 
		   End

		If @TipoInsumo <> 0 
		   Begin 
			  If @TipoInsumo = 1 
				 Delete From tmpExistenciaConcentradoUnidad Where TasaIva <> 0  --- Medicamentos 

			  If @TipoInsumo = 2
				 Delete From tmpExistenciaConcentradoUnidad Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
		   End


		--Print (@sQuery)

		Set @sQuery2 = ' Select EncabezadoPrincipal, EncabezadoSecundario, IdEstado, Estado, IdFarmacia, Farmacia,  IdProducto, Descripcion, TasaIva, ' +
		' IdClaveSSA, ClaveSSA, DescripcionClave, IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, ' +
		' ( Case sum(Cantidad) When 0 Then ''NO'' Else ''SI'' End ) as Existencia, ' +
		' Sum (Cantidad) As Cantidad, ' + 
		' Sum(CantidadConsignacion) As CantidadConsignacion, ' +
		' Sum(CantidadVenta) As CantidadVenta ' + 		 
		' Into Rpt_CteUnidad_Admon_ExistenciaFarmacia ' +
		' From tmpExistenciaConcentradoUnidad ( Nolock ) ' +
		' Group By EncabezadoPrincipal, EncabezadoSecundario, IdEstado, Estado, IdFarmacia, Farmacia,  IdProducto, Descripcion, TasaIva, ' + 
		' IdClaveSSA, ClaveSSA, DescripcionClave, IdPresentacion_ClaveSSA, Presentacion_ClaveSSA ' +
		' Order By IdClaveSSA, Descripcion '
		Exec (@sQuery2)
		-- Print (@sQuery2)

	End

--	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpExistenciaConcentradoUnidad' and xType = 'U' )
--	   Drop Table tmpExistenciaConcentradoUnidad 
--	
--	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CteUnidad_Admon_ExistenciaFarmacia' and xType = 'U' )
--	   Drop Table Rpt_CteUnidad_Admon_ExistenciaFarmacia
--
--	Begin
--	
--		Set @sQuery = ' Select F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, 
--			F.IdProducto, F.DescripcionProducto As Descripcion, ' + 
--		' ( Case F.Existencia When 0 Then ''NO'' Else ''SI'' End ) As Existencia, F.Existencia As Cantidad, ' +
--		' F.ClaveLote, ( Case When F.ClaveLote like ''%*%'' Then 1 Else 0 End ) As EsConsignacion,  ' + 
--		' ( Case When F.ClaveLote like ''%*%'' Then Existencia Else 0 End ) As CantidadConsignacion, ' + 
--		' ( Case When F.ClaveLote not like ''%*%'' Then Existencia Else 0 End ) As CantidadVenta  		' + 			
--		' Into tmpExistenciaConcentradoUnidad ' +
--		' From vw_ExistenciaPorCodigoEAN_Lotes F ( Nolock ) ' + 
--		' Where  F.IdEstado = ' + @IdEstado + 
--		'   And F.IdFarmacia = ' + @IdFarmacia +			
--		' ' + @sCondicion +  
--		' Order By F.DescripcionProducto ' 
--		Exec (@sQuery) 
--		
--
----		--- Agrupar por CodigoEAN 
----		Select IdEstado, Estado, -- IdFarmacia, Farmacia, 
----			IdProducto, Descripcion, 
----			 ( Case Sum(Cantidad) When 0 Then 'NO' Else 'SI' End )as Existencia,   
----			 EsConsignacion, Sum(Cantidad) As Cantidad  
----		into #tmpExistenciaConcentrado	  
----		From tmpExistenciaConcentrado 
----		Group by IdEstado, Estado, IdFarmacia, Farmacia, 
----			IdProducto, Descripcion, EsConsignacion  
--		
--
--
--		Set @sQuery2 = ' Select IdEstado, Estado, IdFarmacia, Farmacia, ' + 
--			' IdProducto, Descripcion, ' + 
--			' ( Case sum(Cantidad) When 0 Then ''NO'' Else ''SI'' End ) as Existencia, ' + 
--			' ' + 
--			' Sum(Cantidad) As Cantidad, ' + 
--			' Sum(CantidadConsignacion) As CantidadConsignacion, ' + 
--			' Sum(CantidadVenta) As CantidadVenta  		' + 
--			' Into Rpt_CteUnidad_Admon_ExistenciaFarmacia ' + 
--			' From tmpExistenciaConcentradoUnidad ( Nolock ) ' + 
--			' Group By IdEstado, Estado, IdFarmacia, Farmacia, IdProducto, Descripcion  '
--		Exec (@sQuery2) 
--		-- print @sQuery2 
--	End



End 
Go--#SQL 