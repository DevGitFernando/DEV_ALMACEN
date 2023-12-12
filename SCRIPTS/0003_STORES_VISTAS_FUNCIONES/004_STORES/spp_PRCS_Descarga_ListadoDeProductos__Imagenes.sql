--------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_PRCS_Descarga_ListadoDeProductos__Imagenes' and xType = 'P')
    Drop Proc spp_PRCS_Descarga_ListadoDeProductos__Imagenes
Go--#SQL	  
  
Create Proc spp_PRCS_Descarga_ListadoDeProductos__Imagenes  
( 	
	@ListaClaveSSA varchar(max) = '', 		
	@ListaDescripcionClaveSSA varchar(max) = '',  	
	
	@ListaClaveSSA_Controlados varchar(max) = '', 		
	@ListaDescripcionClaveSSA_Controlados varchar(max) = '',  	
	
	@ListaClaveSSA_Antibioticos varchar(max) = '', 		
	@ListaDescripcionClaveSSA_Antibioticos varchar(max) = '',  
	
	@ListaLaboratorios varchar(max) = '', 		
	@ListaDescripcionLaboratorios varchar(max) = '',  	
	
	@ListaProductos varchar(max) = '', 		
	@ListaDescripcionProductos varchar(max) = '' 	
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFiltro varchar(max),  
	@sFiltro_Ventas varchar(max),  	
	@sFiltro_Like varchar(max),  
	@sFiltro_Existencias varchar(max),  	
	@sTop varchar(100),  	
	@sOrder varchar(100), 
	@sOrden_01 varchar(max),   
	@sOrden_02 varchar(max)
	
	Set @sSql = '' 
	Set @sFiltro = '' 
	Set @sFiltro_Ventas = ''  
	Set @sFiltro_Like = '' 
	Set @sFiltro_Existencias = '' 
	Set @sOrder = 'Order by P.ClaveSSA, P.Laboratorio '
	Set @sTop = '' 
	Set @sOrden_01 = 'ClaveSSA'  
	Set @sOrden_02 = 'Laboratorio'	
	
	
	
	
-------------------------------------- PREPARAR TABLA DE PROCESO   
	Select 		 
		ClaveSSA, IdProducto, CodigoEAN, Laboratorio, Descripcion, TipoDeClave, TipoDeClaveDescripcion, 0 as Descargar, 
		EsControlado, EsAntibiotico, EsRefrigerado, 
		identity(int, 1, 1) as Keyx  
	Into #tmpProductos_Imagenes 
	From vw_Productos_CodigoEAN E (NoLock)  
	Where 1 = 0
	
	
	
--		select top 10 * from vw_Productos_CodigoEAN
	
	
-------------------------------------- PREPARAR TABLA DE PROCESO   

-------------------------------------- ARMAR FILTRO 
	Set @sFiltro = ''  ---- 'Where E.IdSucursal = ' + char(39) + right('0000' + @IdSucursal, 4) + char(39) + char(13) 
	Set @sFiltro_Ventas = @sFiltro
	
		---------------------------------------------------------------------------------------------------------- 
		----If @FiltroRegistro <> 0 
		----Begin 
		----	---- Set @sFiltro = @sFiltro + ' And convert(varchar(10), E.FechaRegistro, 120) <= ' + char(39) + @FechaRegistroRevision + char(39) + char(13) 		
		----	Set @sFiltro_Ventas = @sFiltro_Ventas + ' And convert(varchar(10), E.FechaRegistro, 120) <= ' + char(39) + @FechaRegistroRevision + char(39) + char(13) 
		----End 		
		---------------------------------------------------------------------------------------------------------- 		
				
		----If @Top > 0 
		----   Set @sTop = ' Top ' + cast(@Top as varchar) + '  ' 
		
		
		------------ Filtro  		
		If @ListaClaveSSA <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' And ' + @ListaClaveSSA + char(13) 
		End 			
		
		If @ListaClaveSSA_Controlados <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' And ' + @ListaClaveSSA_Controlados	+ char(13) 
		End 			
		
		If @ListaClaveSSA_Antibioticos <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' And ' + @ListaClaveSSA_Antibioticos	+ char(13) 
		End 			
		
		If @ListaLaboratorios <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' And ' + @ListaLaboratorios	+ char(13) 
		End 									
		
		If @ListaProductos <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' And ' + @ListaProductos	+ char(13) 
		End 			
	
	
		------------ Filtro por Descripciones 
		If @ListaDescripcionClaveSSA <> '' 
		Begin 
			Set @sFiltro_Like = @sFiltro_Like + char(13) + @ListaDescripcionClaveSSA + ' Or '
		End 
		
		If @ListaDescripcionClaveSSA_Controlados <> '' 
		Begin 
			Set @sFiltro_Like = @sFiltro_Like + char(13) + @ListaDescripcionClaveSSA_Controlados + ' Or '
		End 
		
		If @ListaDescripcionClaveSSA_Antibioticos <> '' 
		Begin 
			Set @sFiltro_Like = @sFiltro_Like + char(13) + @ListaDescripcionClaveSSA_Antibioticos + ' Or '
		End 
						
		If @ListaDescripcionLaboratorios <> '' 
		Begin 
			Set @sFiltro_Like = @sFiltro_Like + char(13) + @ListaDescripcionLaboratorios + ' Or '
		End 
						
		If @ListaDescripcionProductos <> '' 
		Begin 
			Set @sFiltro_Like = @sFiltro_Like + char(13) + @ListaDescripcionProductos + ' Or '
		End 

		
		If @sFiltro_Like <> '' 
		Begin 
			-- print char(39) + @sFiltro_Like + char(39) 			
			Set @sFiltro_Like = ltrim(rtrim(@sFiltro_Like))
			Set @sFiltro_Like = left(@sFiltro_Like, len(@sFiltro_Like) - 2) 
			Set @sFiltro = @sFiltro + ' and ( ' + @sFiltro_Like + ' ) '   
			
			-- print char(39) + @sFiltro_Like + char(39) 
		End 
				
-------------------------------------- ARMAR FILTRO 



---		spp_PRCS_Descarga_ListadoDeProductos__Imagenes  

-------------------------------------- OBTENER INFORMACION 
	Set @sSql = '' + 
		'Insert Into #tmpProductos_Imagenes ' + char(13) + 
		'(' + char(13) + 
		'	ClaveSSA, IdProducto, CodigoEAN, Laboratorio, Descripcion, TipoDeClave, TipoDeClaveDescripcion, Descargar, EsControlado, EsAntibiotico, EsRefrigerado ' + char(13) + 
		')' + char(13) + 
		'Select Distinct ' + char(13) + 
		'	P.ClaveSSA, P.IdProducto, P.CodigoEAN, P.Laboratorio, P.Descripcion, P.TipoDeClave, P.TipoDeClaveDescripcion, ' + char(13) + 
		'   0 as Descargar, P.EsControlado, P.EsAntibiotico, P.EsRefrigerado ' + char(13) + 
		'From vw_Productos_CodigoEAN P (NoLock) ' + char(13) + 
		'Inner Join CatProductos_Imagenes I (NoLock) ' + char(13) + 
		'	On ( P.IdProducto = I.IdProducto and P.CodigoEAN = I.CodigoEAN and I.Status = ' + char(39) + 'A' + char(39) + ' ) ' + char(13) + 		
		@sFiltro + @sOrder  + char(13) 
	Exec(@sSql) 
	print @sSql 
	print '' 
	print '' 
	print '' 	
	
-------------------------------------- OBTENER INFORMACION 


------		spp_PRCS_Descarga_ListadoDeProductos__Imagenes 

-------------------------------------- RESULTADOS   

	Select * 
	From #tmpProductos_Imagenes	-- Order By 1 	 	
	Order by ClaveSSA, Laboratorio 
	--Order by @sOrden_01
	
-------------------------------------- RESULTADOS   


------		spp_PRCS_Descarga_ListadoDeProductos__Imagenes 	


End
Go--#SQL

