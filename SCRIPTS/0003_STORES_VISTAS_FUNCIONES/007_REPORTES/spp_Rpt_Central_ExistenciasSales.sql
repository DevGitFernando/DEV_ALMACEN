If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Central_ExistenciasSales' and xType = 'P' ) 
   Drop Proc spp_Rpt_Central_ExistenciasSales 
Go--#SQL

Create Proc spp_Rpt_Central_ExistenciasSales 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@IdClaveSSA_Sal varchar(20) = '', @IdProducto varchar(8) = '', @CodigoEAN varchar(20) = '', @Mostrar tinyint = 1,
	@iTipoRpt tinyint = 0 
) 
With Encryption 
As 
Begin 
Set NoCount On 
/* 
	Los valores default de los parametros no deben ser modificados, este Store se implementa a varios grupos de datos. 
	Se utiliza de forma dinamica. 
*/ 

Declare @sSql varchar(8000),   
		@sWhere varchar(8000), @sMostrar varchar(50), 
		@dFecha datetime 	 

Declare @sEncPrinRpt varchar(300), 
		@sEncSecRpt varchar(300),
		@Condicion varchar(100)

	Select @sEncPrinRpt = EncPrin, @sEncSecRpt = EncSec From dbo.fg_Central_EncabezadoReportes() 
	
	---- Preparar la tabla con la estructura vacia 
	Set @dFecha = getdate() 
	Select space(300) as EncPrincipal, space(300) as EncSecundario, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, FechaCaducidad, FechaRegistro, Existencia, getdate() as FechaDeImpresion  
	Into #tmpReporesExistencias 	
	From SVR_INV_Generar_Existencia_Detallado (NoLock)
	Where 1 = 0 		


	Set @IdEmpresa = IsNull(@IdEmpresa, '') 
	Set @IdEstado = IsNull(@IdEstado, '') 
	Set @IdFarmacia = IsNull(@IdFarmacia, '') 
	Set @IdClaveSSA_Sal = IsNull(@IdClaveSSA_Sal, '') 
	Set @IdProducto = IsNull(@IdProducto, '') 
	Set @CodigoEAN = IsNull(@CodigoEAN, '')
	Set @iTipoRpt = IsNull(@iTipoRpt, '') 			


	-- Mostrar solo las Claves que se soliciten 
	Set @Mostrar = IsNull(@Mostrar, 1) 
	Set @sMostrar = '' --- Mostrar todo  
	if @Mostrar = 1 
	   Set @sMostrar = ' IdFarmacia  <> ' + char(39) + char(39) + ' and '  -- Utilizadas 

	if @Mostrar = 2 
	   Set @sMostrar = ' IdFarmacia  = ' + char(39) + char(39)  + ' and ' -- Utilizadas 

	---- Se elige el tipo de reporte
 			
	set @Condicion = ' ' 			
	if @iTipoRpt = 0 
		set @Condicion = ' '

	if @iTipoRpt = 1
		set @Condicion = ' and Existencia > 0 '

	if @iTipoRpt = 2 
		set @Condicion = ' and Existencia = 0 '


	---- Preparar el Select 
	Set @sWhere = @sMostrar + 
				  ' ' + char(13) +
	              '    IdEmpresa like ' + char(39) + '%' + @IdEmpresa + '%' + char(39) + ' and ' + char(13) + 
				  '	IdEstado like ' + char(39) + '%' + @IdEstado + '%' + char(39) + ' and ' + char(13) + 
				  '	IdFarmacia like ' + char(39) + '%' + @IdFarmacia + '%' + char(39) + ' and ' + char(13) + 
				  '	ClaveSSA like ' + char(39) + '%' + @IdClaveSSA_Sal + '%' + char(39) + ' and ' + char(13) + 
				  '	IdProducto like ' + char(39) + '%' + @IdProducto + '%' + char(39) + ' and ' + char(13) + 	
				  '	CodigoEAN like ' + char(39) + '%' + @CodigoEAN + '%' + char(39) + @Condicion 

	Set @sSql = 
		' Insert Into #tmpReporesExistencias ( EncPrincipal, EncSecundario, IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, FechaCaducidad, FechaRegistro, Existencia, FechaDeImpresion ) ' + char(13) + 	
		' Select ' + char(13) + 
		'' + char(39) + @sEncPrinRpt + char(39) + ', ' + char(39) + @sEncSecRpt + char(39) + ', ' + 
		'	IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, FechaCaducidad, FechaRegistro, 
			Existencia, getdate() ' + char(13) + 
		' From SVR_INV_Generar_Existencia_Detallado (NoLock) ' + char(13) + 
		' Where ' + @sWhere   
	Exec(@sSql) 
	--print @sSql 
	
	--- Salida final 
	Select EncPrincipal, EncSecundario, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, FechaCaducidad, FechaRegistro, Existencia, FechaDeImpresion  
	From #tmpReporesExistencias (NoLock)	
	
	--    spp_Rpt_Central_ExistenciasSales 
	
End 
Go--#SQL
	