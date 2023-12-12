If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_PtoVta_ProductosCaducados' and xType = 'P' ) 
   Drop Proc spp_Rpt_PtoVta_ProductosCaducados 
Go--#SQL

-- Exec spp_Rpt_PtoVta_ProductosCaducados '001', '25', '0008'

Create Proc spp_Rpt_PtoVta_ProductosCaducados ( @IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '' ) 
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
		@dFecha datetime 	 

Declare @sEncPrinRpt varchar(300), 
		@sEncSecRpt varchar(300)

	Select @sEncPrinRpt = EncPrin, @sEncSecRpt = EncSec From dbo.fg_Central_EncabezadoReportes() 
	
	---- Preparar la tabla con la estructura vacia 
	Set @dFecha = getdate() 
	Select space(300) as EncPrincipal, space(300) as EncSecundario, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, FechaCaducidad, FechaRegistro, Existencia, getdate() as FechaDeImpresion  
	Into #tmpReportesCaducados 	
	From vw_ExistenciaPorCodigoEAN_Lotes (NoLock)
	Where 1 = 0 		


	Set @IdEmpresa = IsNull(@IdEmpresa, '') 
	Set @IdEstado = IsNull(@IdEstado, '') 
	Set @IdFarmacia = IsNull(@IdFarmacia, '') 


	Set @sSql = 
		' Insert Into #tmpReportesCaducados ( EncPrincipal, EncSecundario, IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, FechaCaducidad, FechaRegistro, Existencia, FechaDeImpresion ) ' + char(13) + 	
		' Select ' + char(13) + 
		'' + char(39) + @sEncPrinRpt + char(39) + ', ' + char(39) + @sEncSecRpt + char(39) + ', ' + 
		'	IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, 
			IdProducto, CodigoEAN, DescripcionProducto, ClaveLote, FechaCaducidad, FechaRegistro, 
			Existencia, getdate() ' + char(13) + 
		' From vw_ExistenciaPorCodigoEAN_Lotes (NoLock) ' + char(13) + 
		' Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' And IdEstado = ' + char(39) + @IdEstado + char(39) + 
		' And IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + ' And Existencia > 0 And MesesParaCaducar <= 0 '
	Exec(@sSql) 
	--print @sSql 
	
	--- Salida final 
	Select EncPrincipal, EncSecundario, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA_Sal, ClaveSSA, DescripcionSal, IdProducto, CodigoEAN, 
		DescripcionProducto, ClaveLote, Convert( varchar(10), FechaCaducidad, 120 ) as FechaCaducidad, 
		FechaRegistro, Existencia, FechaDeImpresion  
	From #tmpReportesCaducados (NoLock)	
	Order by IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, DescripcionSal 
		
	--    spp_Rpt_PtoVta_ProductosCaducados 
	
End 
Go--#SQL

	