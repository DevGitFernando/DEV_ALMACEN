If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_vw_UbicacionProductosLotes_Existencia' And xType = 'P' )
	Drop Proc spp_Rpt_vw_UbicacionProductosLotes_Existencia
Go--#SQL 

-- Exec spp_Rpt_vw_UbicacionProductosLotes_Existencia '002', '20', '0029', '01', '', '', '', '', ''

Create Procedure spp_Rpt_vw_UbicacionProductosLotes_Existencia 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @IdSubFarmacia varchar(2) = '', 
	@IdPasillo varchar(8) = '', @IdEstante varchar(8) = '', @IdEntrepaño varchar(8) = '', 
	@IdClaveSSA varchar(4) = '', @ClaveSSA varchar(30) = '' 
) 
With Encryption 	
As
Begin 
Declare @sSql varchar(8000),   
		@sWhere varchar(8000)	 


    Set @sSql = '' 
    Set @sWhere = ''     


	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia,
		IdClaveSSA_Sal, ClaveSSA, DescripcionClave, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad,
		MesesParaCaducar, FechaRegistro, GetDate() As FechaImpresion, DescripcionProducto, 
		IdPasillo, NombrePasillo, IdEstante, NombreEstante, IdEntrepaño, NombreEntrepaño,
		CantidadPosicion, Existencia
	Into #tmpReporteUbicacionLotes
	From vw_UbicacionProductosLotes_Existencia (Nolock)	
	Where 1 = 0

	Set @IdEmpresa = IsNull(@IdEmpresa, '') 
	Set @IdEstado = IsNull(@IdEstado, '') 
	Set @IdFarmacia = IsNull(@IdFarmacia, '')
	Set @IdSubFarmacia = IsNull(@IdSubFarmacia, '')
	Set @IdPasillo = IsNull(@IdPasillo, '')
	Set @IdEstante = IsNull(@IdEstante, '')
	Set @IdEntrepaño = IsNull(@IdEntrepaño, '')  
	Set @IdClaveSSA = IsNull(@IdClaveSSA, '') 
	Set @ClaveSSA = IsNull(@ClaveSSA, '') 
		 

	---- Preparar el Select 
	Set @sWhere = ' ' + char(13) +
	              ' IdEmpresa like ' + char(39) + '%' + @IdEmpresa + '%' + char(39) + ' and ' + char(13) + 
				  '	IdEstado like ' + char(39) + '%' + @IdEstado + '%' + char(39) + ' and ' + char(13) + 
				  '	IdFarmacia like ' + char(39) + '%' + @IdFarmacia + '%' + char(39) + ' and ' + char(13) +
				  '	IdSubFarmacia like ' + char(39) + '%' + @IdSubFarmacia + '%' + char(39) + ' and ' + char(13) +
				  '	IdPasillo like ' + char(39) + '%' + @IdPasillo + '%' + char(39) + ' and ' + char(13) +
				  '	IdEstante like ' + char(39) + '%' + @IdEstante + '%' + char(39) + ' and ' + char(13) +
				  '	IdEntrepaño like ' + char(39) + '%' + @IdEntrepaño + '%' + char(39) + ' and ' + char(13) + 
				  '	IdClaveSSA_Sal like ' + char(39) + '%' + @IdClaveSSA + '%' + char(39) + ' and ' + char(13) + 
				  '	ClaveSSA like ' + char(39) + '%' + @ClaveSSA + '%' + char(39) 

	Set @sSql = 
		' Insert Into #tmpReporteUbicacionLotes ( IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia,
			IdClaveSSA_Sal, ClaveSSA, DescripcionClave, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad,
			MesesParaCaducar, FechaRegistro, FechaImpresion, DescripcionProducto, 
			IdPasillo, NombrePasillo, IdEstante, NombreEstante, IdEntrepaño, NombreEntrepaño,
			CantidadPosicion, Existencia ) ' + char(13) + 	
		' Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia,
			IdClaveSSA_Sal, ClaveSSA, DescripcionClave, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad,
			MesesParaCaducar, FechaRegistro, GetDate(), DescripcionProducto, 
			IdPasillo, NombrePasillo, IdEstante, NombreEstante, IdEntrepaño, NombreEntrepaño,
			CantidadPosicion, Existencia ' + char(13) + 
		' From vw_UbicacionProductosLotes_Existencia (NoLock) ' + char(13) + 
		' Where ' + @sWhere   
	Exec(@sSql) 
	--print @sSql 
	
	--- Salida final 
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia,
			IdClaveSSA_Sal, ClaveSSA, DescripcionClave, IdProducto, CodigoEAN, ClaveLote, FechaCaducidad,
			MesesParaCaducar, FechaRegistro, FechaImpresion, DescripcionProducto, 
			IdPasillo, NombrePasillo, IdEstante, NombreEstante, IdEntrepaño, NombreEntrepaño,
			CantidadPosicion, Existencia  
	From #tmpReporteUbicacionLotes (NoLock)
	Order By IdPasillo, IdEstante, IdEntrepaño, IdClaveSSA_Sal
	   	   
End
Go--#SQL 
