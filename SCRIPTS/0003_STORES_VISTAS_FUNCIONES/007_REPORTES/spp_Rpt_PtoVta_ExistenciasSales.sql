If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_PtoVta_ExistenciasSales' and xType = 'P' ) 
   Drop Proc spp_Rpt_PtoVta_ExistenciasSales 
Go--#SQL

Create Proc spp_Rpt_PtoVta_ExistenciasSales ( @IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', @IdClaveSSA_Sal varchar(4) = '', @IdProducto varchar(8) = '', 
	@CodigoEAN varchar(20) = '', @SubFarmacias varchar(100) = '', @TipoRpt smallint = 0 ) 
With Encryption 
As 
Begin 
Set NoCount On 
/* 
	Los valores default de los parametros no deben ser modificados, este Store se implementa a varios grupos de datos. 
	Se utiliza de forma dinamica. 
*/ 

Declare @sSql varchar(8000),   
		@sWhere varchar(8000), 
		@dFecha datetime,
		@Condicion varchar(100) 	 


	---- Preparar la tabla con la estructura vacia 
	Set @dFecha = getdate() 
	Select 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ClaveLote, FechaCaducidad, FechaRegistro, Existencia, 
			getdate() as FechaDeImpresion  
	Into #tmpReporesExistencias 	
	From vw_ExistenciaPorCodigoEAN_Lotes (NoLock)
	Where 1 = 0 		


	Set @IdEmpresa = IsNull(@IdEmpresa, '') 
	Set @IdEstado = IsNull(@IdEstado, '') 
	Set @IdFarmacia = IsNull(@IdFarmacia, '') 
	Set @IdClaveSSA_Sal = IsNull(@IdClaveSSA_Sal, '') 
	Set @IdProducto = IsNull(@IdProducto, '') 
	Set @CodigoEAN = IsNull(@CodigoEAN, '')
	Set @TipoRpt = IsNull(@TipoRpt, '')

	---- Se elige el tipo de reporte
 			
	set @Condicion = ' ' 			
	if @TipoRpt = 0 
		set @Condicion = ' '

	if @TipoRpt = 1
		set @Condicion = ' and Existencia > 0 '

	if @TipoRpt = 2 
		set @Condicion = ' and Existencia = 0 ' 



	---- Preparar el Select 
	Set @sWhere = ' ' + char(13) +
	              '    IdEmpresa like ' + char(39) + '%' + @IdEmpresa + '%' + char(39) + ' and ' + char(13) + 
				  '	IdEstado like ' + char(39) + '%' + @IdEstado + '%' + char(39) + ' and ' + char(13) + 
				  '	IdFarmacia like ' + char(39) + '%' + @IdFarmacia + '%' + char(39) + ' and ' + char(13) + 
				  '	IdClaveSSA_Sal like ' + char(39) + '%' + @IdClaveSSA_Sal + '%' + char(39) + ' and ' + char(13) + 
				  '	IdProducto like ' + char(39) + '%' + @IdProducto + '%' + char(39) + ' and ' + char(13) + 	
				  '	CodigoEAN like ' + char(39) + '%' + @CodigoEAN + '%' + char(39) + char(13) + @Condicion 

	Set @sSql = 
		' Insert Into #tmpReporesExistencias ( IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
			IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ClaveLote, 
			FechaCaducidad, FechaRegistro, Existencia, FechaDeImpresion ) ' + char(13) + 	
		' Select ' + char(13) + 
		'	IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ClaveLote, FechaCaducidad, FechaRegistro, 
			Existencia, getdate() ' + char(13) + 
		' From vw_ExistenciaPorCodigoEAN_Lotes (NoLock) ' + char(13) + 
		' Where ' + @sWhere + @SubFarmacias  
	Exec(@sSql) 
	--print @sSql 
	
	
	--- Salida final 
	Select 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		IdProducto, CodigoEAN, DescripcionProducto, IdPresentacion, Presentacion, ClaveLote, FechaCaducidad, FechaRegistro, Existencia, 
		FechaDeImpresion  
	From #tmpReporesExistencias (NoLock)	
	
	
	--    spp_Rpt_ExistenciasSales 
	
End 
Go--#SQL
	