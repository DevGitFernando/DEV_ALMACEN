If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_vw_UbicacionProductosLotes_Existencia' And xType = 'P' )
	Drop Proc spp_Rpt_vw_UbicacionProductosLotes_Existencia
Go--#SQL 

-- Exec spp_Rpt_vw_UbicacionProductosLotes_Existencia '001', '21', '0182', '', '', '', '', '', ''

Create Procedure spp_Rpt_vw_UbicacionProductosLotes_Existencia 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @IdSubFarmacia varchar(2) = '', 
	@IdPasillo varchar(8) = '', @IdEstante varchar(8) = '', @IdEntrepaño varchar(8) = '', 
	@IdClaveSSA varchar(4) = '', @ClaveSSA varchar(30) = '', @TipoExistencia int = 1  
) 
With Encryption 	
As
Begin 
Declare @sSql varchar(8000),   
		@sWhere varchar(8000)	 


    Set @sSql = '' 
    Set @sWhere = ''     


	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia,
		IdClaveSSA_Sal, ClaveSSA, DescripcionClave, 
		IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, ContenidoPaquete_ClaveSSA,		
		IdProducto, CodigoEAN, ClaveLote, FechaCaducidad,
		MesesParaCaducar, FechaRegistro, GetDate() As FechaImpresion, DescripcionProducto,
		IdPresentacion, Presentacion, ContenidoPaquete, 
		IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,
		Existencia As CantidadPosicion, Existencia
	Into #tmpReporteUbicacionLotes
	From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (Nolock)	
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
		 

----	---- Preparar el Select 
----	Set @sWhere = ' ' + char(13) +
----	              ' IdEmpresa like ' + char(39) + '%' + @IdEmpresa + '%' + char(39) + ' and ' + char(13) + 
----				  '	IdEstado like ' + char(39) + '%' + @IdEstado + '%' + char(39) + ' and ' + char(13) + 
----				  '	IdFarmacia like ' + char(39) + '%' + @IdFarmacia + '%' + char(39) + ' and ' + char(13) +
----				  '	IdSubFarmacia like ' + char(39) + '%' + @IdSubFarmacia + '%' + char(39) + ' and ' + char(13) +
----				  '	IdPasillo like ' + char(39) + '%' + @IdPasillo + '%' + char(39) + ' and ' + char(13) +
----				  '	IdEstante like ' + char(39) + '%' + @IdEstante + '%' + char(39) + ' and ' + char(13) +
----				  '	IdEntrepaño like ' + char(39) + '%' + @IdEntrepaño + '%' + char(39) + ' and ' + char(13) + 
----				  '	IdClaveSSA_Sal like ' + char(39) + '%' + @IdClaveSSA + '%' + char(39) + ' and ' + char(13) + 
----				  '	ClaveSSA like ' + char(39) + '%' + @ClaveSSA + '%' + char(39) 

	----------- Filtros 
	---- Preparar el Select 
	Set @sWhere = ' ' + char(13) +
	              ' IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and ' + char(13) + 
				  '	IdEstado = ' + char(39) + @IdEstado + char(39) + ' and ' + char(13) + 
				  '	IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + ' ' + char(13) 
				  --- '	IdSubFarmacia = ' + char(39) + '%' + @IdSubFarmacia + '%' + char(39) + ' ' + char(13)
				
				
    If @IdSubFarmacia <> '' 
       Set @sWhere = @sWhere + ' and IdSubFarmacia = ' + char(39) + @IdSubFarmacia + char(39)  
       				
    If @IdPasillo <> '' 
       Set @sWhere = @sWhere + ' and IdPasillo = ' + char(39) + @IdPasillo + char(39)  

    If @IdEstante <> '' 
       Set @sWhere = @sWhere + ' and IdEstante = ' + char(39) + @IdEstante + char(39)  
       
    If @IdEntrepaño <> '' 
       Set @sWhere = @sWhere + ' and IdEntrepaño = ' + char(39) + @IdEntrepaño + char(39)         

    --If @IdClaveSSA <> '' 
    --   Set @sWhere = @sWhere + ' and IdClaveSSA_Sal = ' + char(39) + @IdClaveSSA + char(39)         

    If @ClaveSSA <> '' 
       Set @sWhere = @sWhere + ' and ClaveSSA = ' + char(39) + @ClaveSSA + char(39)        

    If @TipoExistencia = 2
	   Set @sWhere = @sWhere + ' and Existencia = 0'
        
    If @TipoExistencia = 3 
	   Set @sWhere = @sWhere + ' and Existencia > 0'

----------- Filtros 

	Set @sSql = 
		' Insert Into #tmpReporteUbicacionLotes ( IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia,
			IdClaveSSA_Sal, ClaveSSA, DescripcionClave,
			IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, ContenidoPaquete_ClaveSSA, 
			IdProducto, CodigoEAN, ClaveLote, FechaCaducidad,
			MesesParaCaducar, FechaRegistro, FechaImpresion, DescripcionProducto,
			IdPresentacion, Presentacion, ContenidoPaquete, 
			IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,
			CantidadPosicion, Existencia ) ' + char(13) + 	
		' Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia,
			IdClaveSSA_Sal, ClaveSSA, DescripcionClave, 
			IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, ContenidoPaquete_ClaveSSA,
			IdProducto, CodigoEAN, ClaveLote, FechaCaducidad,
			MesesParaCaducar, FechaRegistro, GetDate(), DescripcionProducto,
			IdPresentacion, Presentacion, ContenidoPaquete,
			IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,
			Existencia As CantidadPosicion, Existencia ' + char(13) + 
		' From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) ' + char(13) + 
		' Where ' + @sWhere   
	Exec(@sSql) 
	--print @sSql 
	
	--- Salida final 
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia,
			IdClaveSSA_Sal, ClaveSSA, DescripcionClave, 
			IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, ContenidoPaquete_ClaveSSA,
			IdProducto, CodigoEAN, ClaveLote, FechaCaducidad,
			MesesParaCaducar, FechaRegistro, FechaImpresion, DescripcionProducto,
			IdPresentacion, Presentacion, ContenidoPaquete, 
			IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,
			CantidadPosicion, Existencia, Cast(( Existencia/ContenidoPaquete ) As Numeric(14,4) ) As Cajas   
	From #tmpReporteUbicacionLotes (NoLock)
	Order By IdPasillo, IdEstante, IdEntrepaño, IdClaveSSA_Sal
	   	   
End
Go--#SQL 
