---------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Rpt_UbicacionProductosClaves_Existencia' And xType = 'P' )
	Drop Proc spp_Rpt_UbicacionProductosClaves_Existencia
Go--#SQL 

-- Exec spp_Rpt_UbicacionProductosClaves_Existencia '001', '21', '0182', '', '', '', '', '', ''

--   Exec spp_Rpt_UbicacionProductosClaves_Existencia '001', '21', '0182', '', '8', '1', '1', '1226', '060.550.2608', '2' 

Create Procedure spp_Rpt_UbicacionProductosClaves_Existencia 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '2005', @IdSubFarmacia varchar(2) = '', 
	@IdPasillo varchar(8) = '3', @IdEstante varchar(8) = '16', @IdEntrepaño varchar(8) = '20', 
	@IdClaveSSA varchar(4) = '', @ClaveSSA varchar(30) = '060.168.6645', @TipoExistencia int = 1, @TipoReporte int = 4, @TipoUbicacion int = 2
) 
With Encryption 	
As
Begin 

-- Set NoCount On 

Declare @sSql varchar(8000),   
		@sWhere varchar(8000)	 


    Set @sSql = '' 
    Set @sWhere = ''     


----	Select 
----		IdClaveSSA_Sal, ClaveSSA, DescripcionClave, 
----		IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,
----		Existencia As CantidadPosicion, Existencia
----	Into #tmpReporteUbicacionLotes
----	From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (Nolock)	
----	Where 1 = 0

	Set @IdEmpresa = IsNull(@IdEmpresa, '') 
	Set @IdEstado = IsNull(@IdEstado, '') 
	Set @IdFarmacia = IsNull(@IdFarmacia, '')
	Set @IdSubFarmacia = IsNull(@IdSubFarmacia, '')
	Set @IdPasillo = IsNull(@IdPasillo, '')
	Set @IdEstante = IsNull(@IdEstante, '')
	Set @IdEntrepaño = IsNull(@IdEntrepaño, '')  
	Set @IdClaveSSA = IsNull(@IdClaveSSA, '') 
	Set @ClaveSSA = IsNull(@ClaveSSA, '') 


	Set @IdEmpresa = right('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000000000' + @IdFarmacia, 4) 

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

----    If @IdClaveSSA <> '' 
----       Set @sWhere = @sWhere + ' and IdClaveSSA_Sal = ' + char(39) + @IdClaveSSA + char(39)         

    If @ClaveSSA <> '' 
       Set @sWhere = @sWhere + ' and ClaveSSA = ' + char(39) + @ClaveSSA + char(39)
		
    If @Tipoexistencia = 2
	   Set @sWhere = @sWhere + ' and Existencia = 0 '
        
    If @Tipoexistencia = 3
	   Set @sWhere = @sWhere + ' and Existencia > 0 '
	
	If @TipoUbicacion <> 2
		Set @sWhere = @sWhere + ' and EsDePickeo = ' + char(39) + @TipoUbicacion + char(39)

----------- Filtros 



----	Select * 
----	Into #tmpExistencia
----	From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) Where 1 = 0 
----	
----	Set @sSql = 'Insert Into #tmpExistencia ' + char(13) + 
----	    ' Select * From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) Where ' + @sWhere 
----	Exec(@sSql)  
----    -- print (@sSql)   
    
----	Set @sSql = 
----		' Insert Into #tmpReporteUbicacionLotes (
----			IdClaveSSA_Sal, ClaveSSA, DescripcionClave, 
----			IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,
----			CantidadPosicion, Existencia ) ' + char(13) + 	
----		' Select 
----			IdClaveSSA_Sal, ClaveSSA, DescripcionClave, 
----			IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,
----			Existencia As CantidadPosicion, Existencia ' + char(13) + 
----		' From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) ' + char(13) + 
----		' Where ' + @sWhere  + '
----		' 	'' 

-------------- Crear temporal 
	Select Top 0 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
		IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, DescripcionSal, DescripcionClave, DescripcionCortaClave, TipoDeClave, TipoDeClaveDescripcion, 
		EsControlado, EsAntibiotico, EsRefrigerado, IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, ContenidoPaquete_ClaveSSA, 
		IdProducto, CodigoEAN, CodigoEAN_Interno, DescripcionProducto, FechaCaducidad, MesesParaCaducar, FechaRegistro, 
		IdPresentacion, Presentacion, ContenidoPaquete, IdLaboratorio, Laboratorio, ClaveLote, EsConsignacion, 
		IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, ExistenciaAux, ExistenciaEnTransito, ExistenciaSurtidos, Existencia, 
		-- (case when EsDePickeo = 1 then 'SI' else 'NO' end) as EsDePickeo, Status
		cast('' as varchar(10)) as EsDePickeo, Status 
	Into #vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones  
	From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones  


	Set @sSql = 
	'Insert Into #vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones ' + 
	' Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
		IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, DescripcionSal, DescripcionClave, DescripcionCortaClave, TipoDeClave, TipoDeClaveDescripcion, 
		EsControlado, EsAntibiotico, EsRefrigerado, IdPresentacion_ClaveSSA, Presentacion_ClaveSSA, ContenidoPaquete_ClaveSSA, 
		IdProducto, CodigoEAN, CodigoEAN_Interno, DescripcionProducto, FechaCaducidad, MesesParaCaducar, FechaRegistro, 
		IdPresentacion, Presentacion, ContenidoPaquete, IdLaboratorio, Laboratorio, ClaveLote, EsConsignacion, 
		IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, ExistenciaAux, ExistenciaEnTransito, ExistenciaSurtidos, Existencia, 
		(case when EsDePickeo = 1 then ' + char(39) + 'SI' + char(39) + ' else ' + char(39) + 'NO' + char(39) + ' end) as EsDePickeo, Status ' + 
	' From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) ' + char(13) +  
	' Where ' + @sWhere  
	Exec(@sSql)  
	

	-------------------------- Excluision de ubicaciones 
	----Delete #vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
	----From #vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
	----Where cast(IdPasillo as varchar(100)) + '-' + cast(IdEstante as varchar(100)) + '-'+ cast(IdEntrepaño as varchar(100)) 
	----	in  ( '0-0-100', '70-0-0' )  


    If @TipoReporte = 1 
       Begin 
            Set @sSql =	
		        ' Select 
			        IdClaveSSA_Sal, ClaveSSA, DescripcionClave, 
			        IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,
			        Sum(Existencia) As CantidadPosicion, Sum(Existencia) As Existencia  ' + char(13) + 
		        ' From #vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) ' + char(13) +  		        
		        '  Group By IdClaveSSA_Sal, ClaveSSA, DescripcionClave, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño ' + char(3) + 
		        '  Order By IdPasillo, IdEstante, IdEntrepaño, IdClaveSSA_Sal '

            Set @sSql =	
		        ' Select 
			        IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, DescripcionClave, 
			        IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,
			        Sum(Existencia) As Existencia  ' + char(13) + 
		        ' From #vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) ' + char(13) +  		        
		        '  Group By IdClaveSSA_Sal, ClaveSSA, DescripcionClave, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño ' + char(3) + 
		        '  Order By IdPasillo, IdEstante, IdEntrepaño, IdClaveSSA_Sal '
      End 

    If @TipoReporte = 2 
       Begin 
            Set @sSql =	
		        ' Select ' + char(13) + 
                ' SubFarmacia, IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, Laboratorio, ' + char(13) + 
                ' convert(varchar(7), FechaCaducidad, 120) as FechaCaducidad, Existencia' + char(13) + 
		        ' From #vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) ' + char(13) + 
		        ' Order By convert(varchar(10), FechaCaducidad, 120), ClaveLote '
      End 
      
    If @TipoReporte = 3 
       Begin 
            Set @sSql =	
		        ' Select ' + char(13) + 
                ' IdPasillo, IdEstante, IdEntrepaño, IdProducto, CodigoEAN, ClaveLote, Laboratorio, ' + char(13) + 
                ' convert(varchar(7), FechaCaducidad, 120) as FechaCaducidad, Existencia' + char(13) + 
		        ' From #vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) ' + char(13) + 		        
		        ' Order By convert(varchar(10), FechaCaducidad, 120), ClaveLote ' 
      End
      
     If @TipoReporte = 4
       Begin 
            Set @sSql =	
		        ' Select 
			        IdClaveSSA_Sal, ClaveSSA, DescripcionClave, CodigoEAN, ClaveLote, Laboratorio, EsDePickeo,
			        IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, FechaCaducidad,
			        Sum(Existencia) As CantidadPosicion, Sum(Existencia) As Existencia, Sum(ExistenciaEnTransito) As ExistenciaEnTransito, Sum(ExistenciaAux) As ExistenciaAux   ' + char(13) + 
		        ' From #vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) ' + char(13) +  		        
		        '  Group By IdClaveSSA_Sal, ClaveSSA, DescripcionClave, CodigoEAN, ClaveLote, Laboratorio, EsDePickeo, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño , Entrepaño, FechaCaducidad ' + char(3) + 
		        '  Order By IdPasillo, IdEstante, IdEntrepaño, IdClaveSSA_Sal ' 


            Set @sSql =	
		        ' Select 
			        IdClaveSSA_Sal as IdClaveSSA, ClaveSSA, DescripcionClave, CodigoEAN, ClaveLote, Laboratorio, EsDePickeo,
			        IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño, FechaCaducidad, 
			        Sum(Existencia) As Existencia, Sum(ExistenciaEnTransito) As ExistenciaEnTransito, sum(ExistenciaSurtidos) as ExistenciaSurtidos ' + char(13) + 
		        ' From #vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones (NoLock) ' + char(13) +  		        
		        ' Group By IdClaveSSA_Sal, ClaveSSA, DescripcionClave, CodigoEAN, ClaveLote, Laboratorio, EsDePickeo, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño , Entrepaño, FechaCaducidad ' + char(3) + 
		        ' Order By IdPasillo, IdEstante, IdEntrepaño, IdClaveSSA_Sal ' 
      End    
      
----
----    select top 1 SubFarmacia, IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, convert(varchar(10), FechaCaducidad, 120) as FechaCaducidad, Existencia 
----        * 
----    from vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 

---- Salida Final 
	Exec(@sSql)  
	print (@sSql)  
	
	
	
	--- Salida final
	
----	Select 
----		IdClaveSSA_Sal, ClaveSSA, DescripcionClave, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño,
----		Sum(Existencia) As CantidadPosicion, Sum(Existencia) As Existencia 
----	From #tmpReporteUbicacionLotes (NoLock) 	
----	Group By IdClaveSSA_Sal, ClaveSSA, DescripcionClave, IdPasillo, Pasillo, IdEstante, Estante, IdEntrepaño, Entrepaño
----	Order By IdPasillo, IdEstante, IdEntrepaño, IdClaveSSA_Sal
		
	   	   
End
Go--#SQL 
