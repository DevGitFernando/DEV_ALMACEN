If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_CteReg_Admon_ExistenciaFarmacias' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_Admon_ExistenciaFarmacias 
Go--#SQL

Create Proc spp_Rpt_CteReg_Admon_ExistenciaFarmacias 
(   
	@IdEstado varchar(2) = '25', @iOpcion smallint = 0  
)  
With Encryption 
As 
Begin 
Set NoCount On 

Declare @sCondicion varchar(1000),
		@sQuery varchar(7500),
		@sQuery2 varchar(7500)


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
	Begin
	
		Set @sQuery = ' Select F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, 
			F.IdProducto, F.DescripcionProducto As Descripcion, ' + 
		' ( Case F.Existencia When 0 Then ''NO'' Else ''SI'' End ) As Existencia, F.Existencia As Cantidad, ' +
		' F.ClaveLote, ( Case When F.ClaveLote like ''%*%'' Then 1 Else 0 End ) As EsConsignacion,  ' + 
		' ( Case When F.ClaveLote like ''%*%'' Then Existencia Else 0 End ) As CantidadConsignacion, ' + 
		' ( Case When F.ClaveLote not like ''%*%'' Then Existencia Else 0 End ) As CantidadVenta  		' + 			
		' Into tmpExistenciaConcentrado ' +
		' From vw_ExistenciaPorCodigoEAN_Lotes F ( Nolock ) ' + 
		' Where  F.IdEstado = ' + @IdEstado + 
		'   And F.IdFarmacia 
			IN ( Select IdFarmacia From CteReg_Farmacias_Procesar_Existencia Lf ( Nolock )  
				 Where Lf.IdEstado = F.IdEstado and Lf.IdFarmacia = F.IdFarmacia 
				) ' +
		' ' + @sCondicion +  
		' Order By F.DescripcionProducto ' 
		Exec (@sQuery) 
		

--		--- Agrupar por CodigoEAN 
--		Select IdEstado, Estado, -- IdFarmacia, Farmacia, 
--			IdProducto, Descripcion, 
--			 ( Case Sum(Cantidad) When 0 Then 'NO' Else 'SI' End )as Existencia,   
--			 EsConsignacion, Sum(Cantidad) As Cantidad  
--		into #tmpExistenciaConcentrado	  
--		From tmpExistenciaConcentrado 
--		Group by IdEstado, Estado, IdFarmacia, Farmacia, 
--			IdProducto, Descripcion, EsConsignacion  
		


		Set @sQuery2 = ' Select IdEstado, Estado, IdFarmacia, Farmacia, ' + 
			' IdProducto, Descripcion, ' + 
			' ( Case sum(Cantidad) When 0 Then ''NO'' Else ''SI'' End ) as Existencia, ' + 
			' ' + 
			' Sum(Cantidad) As Cantidad, ' + 
			' Sum(CantidadConsignacion) As CantidadConsignacion, ' + 
			' Sum(CantidadVenta) As CantidadVenta  		' + 
			' Into Rpt_CteReg_Admon_ConcentradoExistenciaFarmacia ' + 
			' From tmpExistenciaConcentrado ( Nolock ) ' + 
			' Group By IdEstado, Estado, IdFarmacia, Farmacia, IdProducto, Descripcion  '
		Exec (@sQuery2) 
		-- print @sQuery2 
	End
-- Select top 1 * From Rpt_CteReg_Admon_ConcentradoExistenciaFarmacia (Nolock)
	
--	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Rpt_CteReg_Admon_ConcentradoExistencia' and xType = 'U' )
--	   Drop Table Rpt_CteReg_Admon_ConcentradoExistencia
--	Begin
--	
--		Set @sQuery = ' Select E.IdEstado, E.Nombre, F.IdProducto, V.DescripcionProducto As Descripcion, ' + 
--		' ( Case Sum( F.Existencia )  When 0 Then ''NO'' Else ''SI'' End ) As Existencia, Sum(F.Existencia) As Cantidad, ' +
--		' ( Case When V.ClaveLote like ''%*%'' Then 0 Else 1 End ) As Tipo ' +
--		' Into Rpt_CteReg_Admon_ConcentradoExistencia ' +
--		' From FarmaciaProductos F ( Nolock ) ' +
--		' Inner Join vw_ExistenciaPorCodigoEAN_Lotes V ( Nolock ) ' +
--		' On ( F.IdProducto = V.IdProducto ) ' +
--		' Inner Join CatEstados E ( Nolock ) ' +
--		' On ( F.IdEstado = E.IdEstado ) ' +
--		' Where  F.IdEstado = ' + @IdEstado + 
--		' And F.Existencia ' + @sCondicion + 
--		' Group By E.IdEstado, E.Nombre, F.IdProducto, V.DescripcionProducto, V.ClaveLote ' + 
--		' Order By V.DescripcionProducto '
--
--		Exec (@sQuery)
--	End 
-- Select top 1 * From Rpt_CteReg_Admon_ConcentradoExistencia (Nolock)

End 
Go--#SQL