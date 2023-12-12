
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_CteReg_Impresion_Proximos_Caducar' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_Impresion_Proximos_Caducar 
Go--#SQL

Create Proc spp_Rpt_CteReg_Impresion_Proximos_Caducar ( @IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	 @IdClaveSSA_Sal varchar(4) = '', @IdProducto varchar(8) = '', @CodigoEAN varchar(20) = '', @Mostrar tinyint = 1, 
	 @FechaInicial varchar(10) , @FechaFinal varchar(10) ) 
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

Declare @sEncPrinRpt varchar(200), 
		@sEncSecRpt varchar(200)

	Set DateFormat YMD 

	-- Se elimina la tabla en caso de existir.
	If Exists( Select Name From SysObjects(NoLock) Where Name = 'Rpt_CteReg_Impresion_Proximos_Caducar' And xType = 'U' )
	  Begin
		Drop Table Rpt_CteReg_Impresion_Proximos_Caducar
	  End

	-- Se obtiene el Encabezado Principal y el Encabezado Secundario
	Select @sEncPrinRpt = EncabezadoPrincipal, @sEncSecRpt = EncabezadoSecundario From fg_Regional_EncabezadoReportesClientesSSA()
	
	---- Preparar la tabla con la estructura vacia 
	Set @dFecha = getdate() 
	Select space(300) as EncPrincipal, space(300) as EncSecundario, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
		IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, FechaCaducidad, FechaRegistro, Existencia, getdate() as FechaDeImpresion,
		@FechaInicial as FechaInicial , @FechaFinal as FechaFinal
	Into Rpt_CteReg_Impresion_Proximos_Caducar 	
	From vw_ExistenciaPorCodigoEAN_Lotes (NoLock)
	Where 1 = 0 		


	Set @IdEmpresa = IsNull(@IdEmpresa, '') 
	Set @IdEstado = IsNull(@IdEstado, '') 
	Set @IdFarmacia = IsNull(@IdFarmacia, '') 
	Set @IdClaveSSA_Sal = IsNull(@IdClaveSSA_Sal, '') 
	Set @IdProducto = IsNull(@IdProducto, '') 
	Set @CodigoEAN = IsNull(@CodigoEAN, '') 			
	Set @FechaInicial = IsNull(@FechaInicial, getdate())
	Set @FechaFinal = IsNull(@FechaFinal, getdate())

	-- Mostrar solo las Claves que se soliciten 
	Set @Mostrar = IsNull(@Mostrar, 1) 
 
	if @Mostrar = 1 
	   Set @sMostrar = ' IdFarmacia  <> ' + char(39) + char(39) + ' and ' -- Mostrar todas las farmacias 

	if @Mostrar = 2 
	   Set @sMostrar = ' IdFarmacia  = ' + char(39) + @IdFarmacia + char(39)  + ' and ' -- Mostrar solo la farmacia seleccionada 



	---- Preparar el Select 
	Set @sWhere = @sMostrar + 
				  ' ' + char(13) +
	              '    IdEmpresa like ' + char(39) + '%' + @IdEmpresa + '%' + char(39) + ' and ' + char(13) + 
				  '	IdEstado like ' + char(39) + '%' + @IdEstado + '%' + char(39) + ' and ' + char(13) + 
				  '	IdFarmacia like ' + char(39) + '%' + @IdFarmacia + '%' + char(39) + ' and ' + char(13) + 
				  '	IdClaveSSA_Sal like ' + char(39) + '%' + @IdClaveSSA_Sal + '%' + char(39) + ' and ' + char(13) + 
				  '	IdProducto like ' + char(39) + '%' + @IdProducto + '%' + char(39) + ' and ' + char(13) + 	
				  '	CodigoEAN like ' + char(39) + '%' + @CodigoEAN + '%' + char(39) +
				  ' And Convert( varchar(10), FechaCaducidad, 120 )  Between ' + char(39) + @FechaInicial + char(39) + ' And ' + char(39) + @FechaFinal + char(39) + 
				  ' and Existencia > 0 ' + 
				  ' And ClaveLote Like ' + char(39) + '%*%' + char(39) + ' ' 

	Set @sSql = 
		' Insert Into Rpt_CteReg_Impresion_Proximos_Caducar ( EncPrincipal, EncSecundario, IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, FechaCaducidad, FechaRegistro, Existencia, FechaDeImpresion, FechaInicial, FechaFinal ) ' + char(13) + 	
		' Select ' + char(13) + 
		'' + char(39) + @sEncPrinRpt + char(39) + ', ' + char(39) + @sEncSecRpt + char(39) + ', ' + 
		'	IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, FechaCaducidad, FechaRegistro, 
			Existencia, getdate() ' + ', ' + char(39) + @FechaInicial + char(39) + ', ' + char(39) + @FechaFinal + char(39) + char(13) + 
		' From vw_ExistenciaPorCodigoEAN_Lotes (NoLock) ' + char(13) + 
		' Where ' + @sWhere   
	Exec(@sSql) 
			
End 
Go--#SQL


	