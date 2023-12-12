If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_CteUnidad_Admon_ExistenciaFarmacias' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteUnidad_Admon_ExistenciaFarmacias 
Go--#SQL 

Create Proc spp_Rpt_CteUnidad_Admon_ExistenciaFarmacias 
(   @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0003', @iOpcion smallint = 0 )  
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sEncPricipal varchar(500), 
	@sEncSecundario varchar(500),
	@sCondicion varchar(1000),
	@sQuery varchar(7500),
	@sQuery2 varchar(7500)

	Select @sEncPricipal = EncabezadoPrincipal, @sEncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 

	Set @sCondicion = '' 
	If @iOpcion = 0
		Set @sCondicion = ' '
	
	If @iOpcion = 1
		Set @sCondicion = ' and F.Existencia > 0'

	If @iOpcion = 2
		Set @sCondicion = ' and F.Existencia = 0'


	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpExistenciaConcentradoUnidad' and xType = 'U' )
	   Drop Table tmpExistenciaConcentradoUnidad 
	
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CteUnidad_Admon_ExistenciaFarmacia' and xType = 'U' )
	   Drop Table Rpt_CteUnidad_Admon_ExistenciaFarmacia

	Begin 
		Set @sQuery = ' Select ' + char(39) + @sEncPricipal + char(39) + ' as EncabezadoPrincipal, ' + char(39) + @sEncSecundario + char(39) + ' as EncabezadoSecundario, ' +  	
		' IdEstado, Estado, IdFarmacia, Farmacia, ClaveLote, IdProducto, DescripcionProducto As Descripcion, ' +
		' IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, DescripcionSal As DescripcionClave, ' + 
		' ( Case Sum( Existencia )  When 0 Then ''NO'' Else ''SI'' End ) As Existencia, Sum(Existencia) As Cantidad, ' +
		' ( Case When ClaveLote like ''%*%'' Then Existencia Else 0 End ) As CantidadConsignacion, ' +
		' ( Case When ClaveLote not like ''%*%'' Then Existencia Else 0 End ) As CantidadVenta ' + 
		' Into tmpExistenciaConcentradoUnidad ' +
		' From vw_ExistenciaPorCodigoEAN_Lotes F (Nolock) ' + 
		' Where  IdEstado = ' + char(39) + @IdEstado + char(39) + ' And IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + 
		' ' + @sCondicion +
		' Group By IdEstado, Estado, IdFarmacia, Farmacia, ClaveLote, IdProducto, DescripcionProducto, ' +		
		' IdClaveSSA_Sal, ClaveSSA, DescripcionSal, Existencia ' +
		' Order By DescripcionSal, DescripcionProducto ' 

		Exec (@sQuery)

		--Print (@sQuery)

		Set @sQuery2 = ' Select EncabezadoPrincipal, EncabezadoSecundario, IdEstado, Estado, IdFarmacia, Farmacia,  IdProducto, Descripcion, ' +
		' IdClaveSSA, ClaveSSA, DescripcionClave, ' +
		' ( Case sum(Cantidad) When 0 Then ''NO'' Else ''SI'' End ) as Existencia, ' +
		' Sum (Cantidad) As Cantidad, ' + 
		' Sum(CantidadConsignacion) As CantidadConsignacion, ' +
		' Sum(CantidadVenta) As CantidadVenta ' + 		 
		' Into Rpt_CteUnidad_Admon_ExistenciaFarmacia ' +
		' From tmpExistenciaConcentradoUnidad ( Nolock ) ' +
		' Group By EncabezadoPrincipal, EncabezadoSecundario, IdEstado, Estado, IdFarmacia, Farmacia,  IdProducto, Descripcion, ' + 
		' IdClaveSSA, ClaveSSA, DescripcionClave ' +
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