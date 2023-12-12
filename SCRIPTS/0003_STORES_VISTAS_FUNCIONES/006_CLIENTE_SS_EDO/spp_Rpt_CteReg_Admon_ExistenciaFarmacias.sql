If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_CteReg_Admon_ExistenciaFarmacias' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_Admon_ExistenciaFarmacias 
Go--#SQL

Create Proc spp_Rpt_CteReg_Admon_ExistenciaFarmacias 
(   
	@IdEstado varchar(2) = '21', @iOpcion smallint = 0, @TipoInsumo tinyint = 0,
	@TipoDispensacion tinyint = 0 
)  
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

	-- Select @sEncPricipal = EncabezadoPrincipal, @sEncSecundario = EncabezadoSecundario From dbo.fg_Regional_EncabezadoReportesClientesSSA() 
	Select @sEncPricipal = EncabezadoPrincipal, @sEncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 	

	Set @sCondicion = '' 
	If @iOpcion = 0
		Set @sCondicion = ' '
	
	If @iOpcion = 1
		Set @sCondicion = ' and F.Existencia > 0'

	If @iOpcion = 2
		Set @sCondicion = ' and F.Existencia = 0'


	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpExistenciaConcentrado' and xType = 'U' )
	   Drop Table tmpExistenciaConcentrado 
	
	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CteReg_Admon_ConcentradoExistenciaFarmacia' and xType = 'U' )
	   Drop Table Rpt_CteReg_Admon_ConcentradoExistenciaFarmacia

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CteReg_Admon_ConcentradoExistencia' and xType = 'U' )
	   Drop Table Rpt_CteReg_Admon_ConcentradoExistencia


	Begin 	
		Set @sQuery = ' Select ' + char(39) + @sEncPricipal + char(39) + ' as EncabezadoPrincipal, ' + char(39) + @sEncSecundario + char(39) + ' as EncabezadoSecundario, ' +  	
		' IdEstado, Estado, IdFarmacia, Farmacia, ClaveLote, IdProducto, DescripcionProducto As Descripcion, ' +
		' ClaveLote As Lote, FechaCaducidad, MesesParaCaducar, FechaRegistro, TasaIva, ' +
		' Cast( 0 As Numeric(14,4) ) As CostoProducto, Cast( 0 As Numeric(14,4) ) As CostoMin, Cast( 0 As Numeric(14,4) ) As CostoMax, ' +
		' Cast( 0 As Numeric(14,4) ) As ImporteProducto, Cast( 0 As Numeric(14,4) ) As ImporteCostoMin, Cast( 0 As Numeric(14,4) ) As ImporteCostoMax, ' +
		' IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, DescripcionSal As DescripcionClave, IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, ' + 
		' ( Case Sum( Existencia )  When 0 Then ''NO'' Else ''SI'' End ) As Existencia, Sum(Existencia) As Cantidad, ' +
		' ( Case When ClaveLote like ''%*%'' Then Existencia Else 0 End ) As CantidadConsignacion, ' +
		' ( Case When ClaveLote not like ''%*%'' Then Existencia Else 0 End ) As CantidadVenta ' + 
		' Into tmpExistenciaConcentrado ' +
		' From vw_ExistenciaPorCodigoEAN_Lotes F (Nolock) ' + 
		' Where  F.IdEstado = ' + @IdEstado + 
		'   And F.IdFarmacia 
			IN ( Select IdFarmacia From CteReg_Farmacias_Procesar_Existencia Lf ( Nolock )  
				 Where Lf.IdEstado = F.IdEstado and Lf.IdFarmacia = F.IdFarmacia 
				) ' +
		' ' + @sCondicion +  
		' Group By IdEstado, Estado, IdFarmacia, Farmacia, ClaveLote, IdProducto, DescripcionProducto, ' +
		' ClaveLote, FechaCaducidad, MesesParaCaducar, FechaRegistro, TasaIva, ' +		
		' IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, Existencia ' +
		' Order By DescripcionSal, DescripcionProducto '  	
	    Exec(@sQuery) 
	
		--- Remover los Lotes segun sea el caso 
		If @TipoDispensacion <> 0 
		   Begin 
			  If @TipoDispensacion = 1 
				 Delete From tmpExistenciaConcentrado Where ClaveLote Not Like '%*%'  -- Consignacion 

			  If @TipoDispensacion = 2
				 Delete From tmpExistenciaConcentrado Where ClaveLote Like '%*%'  -- Venta 
		   End

		If @TipoInsumo <> 0 
		   Begin 
			  If @TipoInsumo = 1 
				 Delete From tmpExistenciaConcentrado Where TasaIva <> 0  --- Medicamentos 

			  If @TipoInsumo = 2
				 Delete From tmpExistenciaConcentrado Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
		   End


		Set @sQuery2 = ' Select EncabezadoPrincipal,  EncabezadoSecundario, IdEstado, Estado, IdFarmacia, Farmacia,  IdProducto, Descripcion, ' +
		' Lote, FechaCaducidad, MesesParaCaducar, FechaRegistro, TasaIva, ' +
		' CostoProducto, CostoMin, CostoMax, ' +
		' ImporteProducto, ImporteCostoMin, ImporteCostoMax, ' +
		' IdClaveSSA, ClaveSSA, DescripcionClave, IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, ' +
		' ( Case sum(Cantidad) When 0 Then ''NO'' Else ''SI'' End ) as Existencia, ' +
		' Sum (Cantidad) As Cantidad, ' + 
		' Sum(CantidadConsignacion) As CantidadConsignacion, ' +
		' Sum(CantidadVenta) As CantidadVenta ' + 		 
		' Into Rpt_CteReg_Admon_ConcentradoExistenciaFarmacia ' +
		' From tmpExistenciaConcentrado ( Nolock ) ' +
		' Group By EncabezadoPrincipal, EncabezadoSecundario, IdEstado, Estado, IdFarmacia, Farmacia,  IdProducto, Descripcion, ' +
		' Lote, FechaCaducidad, MesesParaCaducar, FechaRegistro, TasaIva, ' +
		' CostoProducto, CostoMin, CostoMax, ' +
		' ImporteProducto, ImporteCostoMin, ImporteCostoMax, ' + 
		' IdClaveSSA, ClaveSSA, DescripcionClave, IdPresentacion_ClaveSSA, Presentacion_ClaveSSA ' +
		' Order By IdClaveSSA, Descripcion '
		Exec (@sQuery2)

		Select R.IdProducto, Case When F.UltimoCosto = 0 Then 1 Else F.UltimoCosto End As Costo,
		Case When Min(F.UltimoCosto) = 0 Then 1 Else Min(F.UltimoCosto) End As CostoMin,
		Case When Max(F.UltimoCosto) = 0 Then 1 Else Max(F.UltimoCosto) End As CostoMax
		Into #tmpCostoProducto
		From Rpt_CteReg_Admon_ConcentradoExistenciaFarmacia R (Nolock)
		Inner Join FarmaciaProductos F (NoLock)
			On ( R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia and R.IdProducto = F.IdProducto )
		Group By R.IdProducto, F.UltimoCosto
		
		Update R Set R.CostoProducto = F.Costo, R.CostoMin = F.CostoMin, R.CostoMax = F.CostoMax 
		From Rpt_CteReg_Admon_ConcentradoExistenciaFarmacia R (Nolock)
		Inner Join #tmpCostoProducto F (NoLock)
			On ( R.IdProducto = F.IdProducto )


		-- Se ponen los precios de licitacion por IdClaveSSA
		Select C.IdClaveSSA, Case When P.Precio = 0 Then 1 Else P.Precio End As Precio
		Into #tmpPreciosLicitacionIdClaveSSA
		From vw_CB_CuadroBasico_Farmacias C (Nolock)
		Inner Join vw_Claves_Precios_Asignados P (Nolock)
			On( C.IdEstado = P.IdEstado and C.IdClaveSSA = P.IdClaveSSA )
		Where C.IdEstado = @IdEstado and C.IdFarmacia  IN ( Select IdFarmacia From CteReg_Farmacias_Procesar_Existencia Lf ( Nolock )  
				 Where Lf.IdEstado = C.IdEstado and Lf.IdFarmacia = C.IdFarmacia )

		Update R Set R.CostoProducto = P.Precio, R.CostoMin = P.Precio, R.CostoMax = P.Precio
			From Rpt_CteReg_Admon_ConcentradoExistenciaFarmacia R (Nolock)
			Inner Join #tmpPreciosLicitacionIdClaveSSA P (Nolock)
				On( R.IdClaveSSA = P.IdClaveSSA  )

		Update R Set R.ImporteProducto = (R.CostoProducto * R.Cantidad), R.ImporteCostoMin = (R.CostoMin * R.Cantidad), 
					 R.ImporteCostoMax = (R.CostoMax * R.Cantidad) 
			From Rpt_CteReg_Admon_ConcentradoExistenciaFarmacia R (Nolock)
			Where R.IdProducto = R.IdProducto

	End

End 
Go--#SQL