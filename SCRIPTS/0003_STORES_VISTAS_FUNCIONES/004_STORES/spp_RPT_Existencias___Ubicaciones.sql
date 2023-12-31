----------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_RPT_Existencias_Ubicaciones' and xType = 'P' ) 
   Drop Proc spp_RPT_Existencias_Ubicaciones 
Go--#SQL 

Create Proc spp_RPT_Existencias_Ubicaciones  
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '13', @IdFarmacia varchar(4) = '3', 
	@TipoDeReporte int = 2, @SubFarmacias varchar(500) = '', @TipoDeExistencia int = 1  
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max), 
	@sFiltroExistencia varchar(100), 
	@sFiltroSubFarmacias varchar(max)  

	Set @sSql = '' 
	Set @IdEmpresa = RIGHT('000000' + @IdEmpresa, 3) 
	Set @IdEstado = RIGHT('000000' + @IdEstado, 2) 
	Set @IdFarmacia = RIGHT('000000' + @IdFarmacia, 4) 
	Set @sFiltroExistencia = '' 
	Set @sFiltroSubFarmacias = '' 

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


	If @TipoDeReporte = 1 
	Begin 
		Set @sSql = 
		'Select ' +
		' V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, ' + char(13) + char(10) + 
		' S.TipoDeClaveDescripcion as TipoDeInsumo, ' + char(13) + char(10) + 
		' S.IdClaveSSA_Sal as IdClaveSSA, S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, S.DescripcionSal as DescripcionClave,  ' + char(13) + char(10) +
		' (case when V.EsControlado = 1 Then ' + char(39) + 'SI' + char(39) + ' else ' + char(39) + 'NO'  + char(39) + ' end) as EsControlado, ' +  char(13) + char(10) +
		' (case when V.EsAntibiotico = 1 Then ' + char(39) + 'SI' + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end) as EsAntibiotico, ' + char(13) + char(10) + 
		' (case when V.EsRefrigerado = 1 Then ' + char(39) + 'SI'  + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end) as EsRefrigerado, ' + char(13) + char(10) +
		' sum(IsNull(V.Existencia, 0)) as ExistenciaActual, ' + char(13) + char(10) + 
		' sum(IsNull(V.ExistenciaEnTransito, 0)) as ExistenciaEnTransito, sum(IsNull(V.ExistenciaAux, 0)) as ExistenciaTotal, ' + char(13) + char(10) +
		' getdate() as FechaEmisionReporte ' + char(13) + char(10) +
		' From vw_ClavesSSA_Sales S (NoLock) ' + char(13) + char(10) + 
		' Left Join vw_ExistenciaPorCodigoEAN_Lotes V (NoLock) On ( S.IdClaveSSA_Sal = V.IdClaveSSA_Sal ) ' + char(13) + char(10) +
		' Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + 
			' and IdFarmacia = '+ char(39) + @IdFarmacia + char(39) + '  ' + @sFiltroExistencia + '  ' + @sFiltroSubFarmacias + char(13) + char(10) +
		' Group by V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, Farmacia, S.TipoDeClaveDescripcion, ' + char(13) + char(10) + 
		'		(case when V.EsControlado = 1 Then ' + char(39) + 'SI' + char(39) + ' else ' + char(39) + 'NO'  + char(39) + ' end), ' +  char(13) + char(10) +
		'		(case when V.EsAntibiotico = 1 Then ' + char(39) + 'SI' + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end), ' + char(13) + char(10) + 
		'		(case when V.EsRefrigerado = 1 Then ' + char(39) + 'SI'  + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end), ' + char(13) + char(10) +
		'		S.IdClaveSSA_Sal, S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, S.DescripcionSal ' + char(13) + char(10) +
		' Order by S.TipoDeClaveDescripcion, S.DescripcionSal, S.ClaveSSA ' 
	End 


	If @TipoDeReporte = 2  
	Begin 
		Set @sSql = 
		'Select ' +
		' V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.IdFarmacia, ' + char(13) + char(10) + 
		' S.TipoDeClaveDescripcion as TipoDeInsumo, ' + char(13) + char(10) + 
		' S.IdClaveSSA_Sal as IdClaveSSA, S.ClaveSSA_Base, S.ClaveSSA, S.ClaveSSA_Aux, S.DescripcionSal as DescripcionClave,  ' + char(13) + char(10) +
		' (case when V.EsControlado = 1 Then ' + char(39) + 'SI' + char(39) + ' else ' + char(39) + 'NO'  + char(39) + ' end) as EsControlado, ' +  char(13) + char(10) +
		' (case when V.EsAntibiotico = 1 Then ' + char(39) + 'SI' + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end) as EsAntibiotico, ' + char(13) + char(10) + 
		' (case when V.EsRefrigerado = 1 Then ' + char(39) + 'SI'  + char(39) + ' else '  + char(39) + 'NO'  + char(39) + ' end) as EsRefrigerado, ' + char(13) + char(10) +

		' V.IdSubFarmacia, V.SubFarmacia, V.IdProducto, V.CodigoEAN, V.DescripcionProducto, ' + char(13) + char(10) + 
		' V.Presentacion, V.ContenidoPaquete, V.IdLaboratorio, V.Laboratorio, V.ClaveLote, ' + char(13) + char(10) + 
		' convert(varchar(7), FechaCaducidad, 120) as Caduca, MesesParaCaducar as MesesCaduca, ' + char(13) + char(10) + 

		'V.IdPasillo, V.IdEstante, V.IdEntrepaño, ' + char(13) + char(10) + 

		' (IsNull(V.Existencia, 0)) as ExistenciaActual, ' + char(13) + char(10) + 
		' (IsNull(V.ExistenciaEnTransito, 0)) as ExistenciaEnTransito, (IsNull(V.ExistenciaAux, 0)) as ExistenciaTotal, ' + char(13) + char(10) +
		' getdate() as FechaEmisionReporte ' + char(13) + char(10) + 
		' From vw_ClavesSSA_Sales S (NoLock) ' + char(13) + char(10) + 
		' Left Join vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones V (NoLock) On ( S.IdClaveSSA_Sal = V.IdClaveSSA_Sal ) ' + char(13) + char(10) +
		' Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) + 
			' and IdFarmacia = '+ char(39) + @IdFarmacia + char(39) + '  ' + @sFiltroExistencia + '  ' + @sFiltroSubFarmacias + char(13) + char(10) + 
		' Order by S.TipoDeClaveDescripcion, S.DescripcionSal, S.ClaveSSA ' 
	End 

---		spp_RPT_Existencias_Ubicaciones  

	Print @sSql 
	Exec(@sSql)  

End 
Go--#SQL  


