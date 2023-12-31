----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_RPT_Existencias' and xType = 'P' ) 
   Drop Proc spp_RPT_Existencias 
Go--#SQL 

Create Proc spp_RPT_Existencias 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '3182', 
	@TipoDeReporte int = 2, @SubFarmacias varchar(500) = '', @TipoDeExistencia int = 1, @MostrarCostos int = 0 
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max), 
	@sFiltroExistencia varchar(100), 
	@sFiltroSubFarmacias varchar(max), 
	@sFiltroUbicaciones  varchar(max), 
	@sTabla_Consulta varchar(1000), 
	@iEsAlmacen int 

	Set @sSql = '' 
	Set @IdEmpresa = RIGHT('000000' + @IdEmpresa, 3) 
	Set @IdEstado = RIGHT('000000' + @IdEstado, 2) 
	Set @IdFarmacia = RIGHT('000000' + @IdFarmacia, 4) 
	Set @sFiltroExistencia = '' 
	Set @sFiltroSubFarmacias = '' 
	Set @iEsAlmacen = 0 
	Set @sFiltroUbicaciones = '' 
	Set @sTabla_Consulta = ' vw_ExistenciaPorCodigoEAN_Lotes ' 

	If @TipoDeExistencia <> 0 
	Begin 
		Set @sFiltroExistencia = ' and Existencia = 0 ' 
		If @TipoDeExistencia = 1  
			Begin 
				Set @sFiltroExistencia = ' and Existencia > 0 ' 
			End 
	End 


	If @SubFarmacias <> '' 
	   Set @sFiltroSubFarmacias = @SubFarmacias 


	------------------------------ EXCLUSION DE UBICACIONES 
	Select Top 0 0 as IdPasillo, 0 as IdEstante, 0 as IdEntrepaño 
	Into #tmpUbicacionesExcluidas 

	-- Insert Into #tmpUbicacionesExcluidas Select 0, 0, 0 
	------------------------------ EXCLUSION DE UBICACIONES 


	Select @iEsAlmacen = EsAlmacen From CatFarmacias (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Set @iEsAlmacen = ISNULL(@iEsAlmacen, 0) 
	
	Set @sFiltroUbicaciones = '' 
	If @iEsAlmacen = 1 
	Begin 
		Set @sTabla_Consulta = ' vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones ' 
		Set @sFiltroUbicaciones = ' and Not Exists ( Select * From #tmpUbicacionesExcluidas U Where V.IdPasillo = U.IdPasillo and U.IdEstante = V.IdEstante and U.IdEntrepaño = V.IdEntrepaño ) ' 
	End 



	If @TipoDeReporte = 1 
	Begin 

		Set @sSql = 
			'Select ' +
			--' V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, ' + char(13) + char(10) + 
			' V.IdFarmacia, V.Farmacia, ' + char(13) + char(10) + 
			' S.TipoDeClaveDescripcion as TipoDeInsumo, ' + char(13) + char(10) + 
			' S.IdClaveSSA_Sal as IdClaveSSA, S.ClaveSSA, S.DescripcionSal as DescripcionClave,  ' + char(13) + char(10) +
			' (case when V.EsControlado = 1 Then ' + char(39) + 'SI' + char(39) + ' else ' + char(39) + 'NO'  + char(39) + ' end) as EsControlado, ' +  char(13) + char(10) +
			' (case when V.EsAntibiotico = 1 Then ' + char(39) + 'SI' + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end) as EsAntibiotico, ' + char(13) + char(10) + 
			' (case when V.EsRefrigerado = 1 Then ' + char(39) + 'SI'  + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end) as EsRefrigerado, ' + char(13) + char(10) +
			' sum(IsNull(V.Existencia, 0)) as ExistenciaActual, ' + char(13) + char(10) + 
			' sum(IsNull(V.ExistenciaEnTransito, 0)) as ExistenciaEnTransito, sum(IsNull(V.ExistenciaSurtidos, 0)) as ExistenciaSurtidos, sum(IsNull(V.ExistenciaAux, 0)) as ExistenciaTotal, ' + char(13) + char(10) +
			' getdate() as FechaEmisionReporte ' + char(13) + char(10) +
			' From vw_ClavesSSA_Sales S (NoLock) ' + char(13) + char(10) + 
			' Left Join  ' + @sTabla_Consulta + '  V (NoLock) On ( S.IdClaveSSA_Sal = V.IdClaveSSA_Sal ) ' + char(13) + char(10) +
			' Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + 
				' and IdFarmacia = '+ char(39) + @IdFarmacia + char(39) + '  ' + @sFiltroExistencia + '  ' + @sFiltroSubFarmacias + char(13) + char(10) + @sFiltroUbicaciones +  char(13) + char(10) + 
			' Group by V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, Farmacia, S.TipoDeClaveDescripcion, ' + char(13) + char(10) + 
			'		(case when V.EsControlado = 1 Then ' + char(39) + 'SI' + char(39) + ' else ' + char(39) + 'NO'  + char(39) + ' end), ' +  char(13) + char(10) +
			'		(case when V.EsAntibiotico = 1 Then ' + char(39) + 'SI' + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end), ' + char(13) + char(10) + 
			'		(case when V.EsRefrigerado = 1 Then ' + char(39) + 'SI'  + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end), ' + char(13) + char(10) +
			'		S.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionSal ' + char(13) + char(10) +
			' Order by S.TipoDeClaveDescripcion, S.DescripcionSal, S.ClaveSSA '  

	End 


	If @TipoDeReporte = 2  
	Begin 

		Set @sSql = 
		'Select ' +
		-- ' V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, ' + char(13) + char(10) + 
		' V.IdFarmacia, V.Farmacia, ' + char(13) + char(10) + 
		' S.TipoDeClaveDescripcion as TipoDeInsumo, ' + char(13) + char(10) + 
		' S.IdClaveSSA_Sal as IdClaveSSA, S.ClaveSSA, S.DescripcionSal as DescripcionClave,  ' + char(13) + char(10) +
		' (case when V.EsControlado = 1 Then ' + char(39) + 'SI' + char(39) + ' else ' + char(39) + 'NO'  + char(39) + ' end) as EsControlado, ' +  char(13) + char(10) +
		' (case when V.EsAntibiotico = 1 Then ' + char(39) + 'SI' + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end) as EsAntibiotico, ' + char(13) + char(10) + 
		' (case when V.EsRefrigerado = 1 Then ' + char(39) + 'SI'  + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end) as EsRefrigerado, ' + char(13) + char(10) +

		' V.IdSubFarmacia, V.SubFarmacia, V.IdProducto, V.CodigoEAN, V.DescripcionProducto, ' + char(13) + char(10) + 
		' V.Presentacion, V.ContenidoPaquete, V.IdLaboratorio, V.Laboratorio, V.ClaveLote, ' + char(13) + char(10) + 
		' convert(varchar(7), V.FechaCaducidad, 120) as Caduca, MesesParaCaducar as MesesCaduca, ' + char(13) + char(10) + 

		' (IsNull(V.Existencia, 0)) as ExistenciaActual, ' + char(13) + char(10) + 
		' (IsNull(V.ExistenciaEnTransito, 0)) as ExistenciaEnTransito, sum(IsNull(V.ExistenciaSurtidos, 0)) as ExistenciaSurtidos, (IsNull(V.ExistenciaAux, 0)) as ExistenciaTotal, ' + char(13) + char(10) + 
		' (case when ' + cast(@MostrarCostos as varchar(5)) + ' = 1 then CostoPromedio else 0 end) as CostoPromedio, ' + char(13) + char(10) + 
		' (case when ' + cast(@MostrarCostos as varchar(5)) + ' = 1 then UltimoCosto else 0 end) as UltimoCosto,  ' + char(13) + char(10) + 
		' getdate() as FechaEmisionReporte ' + char(13) + char(10) + 
		' From vw_ClavesSSA_Sales S (NoLock) ' + char(13) + char(10) + 
		' Left Join  ' + @sTabla_Consulta + '  V (NoLock) On ( S.IdClaveSSA_Sal = V.IdClaveSSA_Sal ) ' + char(13) + char(10) +
		' Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + 
		'	and IdFarmacia = '+ char(39) + @IdFarmacia + char(39) + '  ' + @sFiltroExistencia + '  ' + @sFiltroSubFarmacias + char(13) + char(10) +
		' ' + @sFiltroUbicaciones +  char(13) + char(10) + char(13) + char(10) +
		' Group by V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, Farmacia, S.TipoDeClaveDescripcion, ' + char(13) + char(10) + 
		'		(case when V.EsControlado = 1 Then ' + char(39) + 'SI' + char(39) + ' else ' + char(39) + 'NO'  + char(39) + ' end), ' +  char(13) + char(10) +
		'		(case when V.EsAntibiotico = 1 Then ' + char(39) + 'SI' + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end), ' + char(13) + char(10) + 
		'		(case when V.EsRefrigerado = 1 Then ' + char(39) + 'SI'  + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end), ' + char(13) + char(10) +
		'		S.IdClaveSSA_Sal, S.ClaveSSA, S.DescripcionSal, ' + char(13) + char(10) +
		' V.IdSubFarmacia, V.SubFarmacia, V.IdProducto, V.CodigoEAN, V.DescripcionProducto, ' + char(13) + char(10) + 
		' V.Presentacion, V.ContenidoPaquete, V.IdLaboratorio, V.Laboratorio, V.ClaveLote, ' + char(13) + char(10) + 
		' convert(varchar(7), V.FechaCaducidad, 120), MesesParaCaducar, ' + char(13) + char(10) +
		' (IsNull(V.Existencia, 0)), (IsNull(V.ExistenciaEnTransito, 0)), IsNull(V.ExistenciaAux, 0)' + char(13) + char(10) + 
		' Order by S.TipoDeClaveDescripcion, S.DescripcionSal, S.ClaveSSA ' 

	End 

---		spp_RPT_Existencias  

	Print @sSql 
	Exec(@sSql)  

End 
Go--#SQL  


