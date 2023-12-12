
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_COM_OCEN_Vales_Por_Mes' And xType = 'P' )
	Drop Proc spp_Rpt_COM_OCEN_Vales_Por_Mes
Go--#SQL

Create Procedure spp_Rpt_COM_OCEN_Vales_Por_Mes 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @iAño int = 2012, @iMes int = 10 
)
With Encryption
As
Begin 
	Declare @sNombreMes varchar(50)

	Set DateFormat YMD
	Set NoCount On
	Set @sNombreMes = ''
	
	----------------------------------
	-- Se obtiene el nombre del Mes -- 
	----------------------------------
	Exec @sNombreMes = dbo.fg_NombresDeMesNumero @iMes
	
	-------------------------------------
	-- Se obtienen los Vales Emitidos  --
	-------------------------------------
	Select	IdEmpresa, space(100) as Empresa, 
			IdEstado, space(50) as Estado,
			space(3) as IdJurisdiccion, space(50) as Jurisdiccion, 
			IdFarmacia, space(100) as Farmacia, 
			FolioVale, FolioVenta, space(20) as NumReceta,
			GetDate() as FechaReceta, 
			Cast( 0 as Numeric(14,4) )as Cantidad, 
			space(8) as IdBeneficiario, space(200) as Beneficiario,  
			space(20) as Poliza, 
			Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro, 
			Convert( varchar(10), FechaCanje, 120) as FechaCanje, 
			IdCliente, space(100) as Cliente, 
			IdSubCliente, space(100) as SubCliente, 
			IdPrograma, space(100) as Programa, 
			IdSubPrograma, space(100) as SubPrograma, 
			( Case When Status = 'A' Then 'ACTIVO' Else 'REGISTRADO' End ) as Status 
	Into #tmpEmitidos
	From Vales_EmisionEnc(NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado --And IdFarmacia = @IdFarmacia 
		And Year( FechaRegistro ) = @iAño And Month( FechaRegistro ) = @iMes
	Order By IdFarmacia, FolioVale 
 
	-- Se obtienen los IdBeneficiarios de cada vale
	Update E
	Set IdBeneficiario = P.IdBeneficiario, NumReceta = P.NumReceta, FechaReceta = P.FechaReceta
	From #tmpEmitidos E 
	Inner Join Vales_Emision_InformacionAdicional P(NoLock) 
		On ( E.IdEmpresa = P.IdEmpresa And E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia And E.FolioVale = P.FolioVale ) 

	-- Se obtienen los Beneficiarios de cada vale
	Update E
	Set Beneficiario = ( P.Nombre + ' ' + IsNull(P.ApPaterno, '') + ' ' + IsNull(P.ApMaterno, '') ),  
		Poliza = P.FolioReferencia
	From #tmpEmitidos E 
	Inner Join CatBeneficiarios P(NoLock) 
		On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia 
			And E.IdCliente = P.IdCliente And E.SubCliente = E.SubCliente And E.IdBeneficiario = P.IdBeneficiario ) 

	-- Se obtienen la cantidad emitida de cada vale por clave ssa
	Select IdEmpresa, IdEstado, IdFarmacia, FolioVale, 
	IdClaveSSA_Sal, space(50) as ClaveSSA, space(7500) as DescripcionSal,
	Sum(Cantidad) as Cantidad 
	Into #tmpCantidadesEmitidas
	From Vales_EmisionDet D(NoLock)	
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado --And IdFarmacia = @IdFarmacia 
		And FolioVale In ( Select FolioVale From #tmpEmitidos(NoLock) )
	Group By IdEmpresa, IdEstado, IdFarmacia, FolioVale, IdClaveSSA_Sal
	Order By FolioVale 

--	Update E
--	Set Cantidad = P.Cantidad
--	From #tmpEmitidos E 
--	Inner Join #tmpCantidadesEmitidas P(NoLock) 
--		On ( E.IdEmpresa = P.IdEmpresa And E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia And E.FolioVale = P.FolioVale ) 

	------- SE ACTUALIZA LA CLAVE SSA Y LA DESCRIPCION DE LA CLAVE  -----------------------
	Update E Set E.ClaveSSA = C.ClaveSSA, E.DescripcionSal = C.DescripcionCortaClave
	From #tmpCantidadesEmitidas  E (NoLock)
	Inner Join CatClavesSSA_Sales C (Nolock) On (E.IdClaveSSA_Sal = C.IdClaveSSA_Sal)
---------------------------------------------------------------------------------------

 
	----------------------------------------------------------------------------
	-- Se obtienen los nombres de Cliente, SubCliente, Programa y SubPrograma --
	----------------------------------------------------------------------------

	Update E
	Set Cliente = Nombre 
	From #tmpEmitidos E 
	Inner Join CatClientes P(NoLock) On ( E.IdCliente = P.IdCliente ) 

	Update E
	Set SubCliente = Nombre 
	From #tmpEmitidos E 
	Inner Join CatSubClientes P(NoLock) On ( E.IdCliente = P.IdCliente And E.IdSubCliente = P.IdSubCliente ) 

	Update E
	Set Programa = Descripcion 
	From #tmpEmitidos E 
	Inner Join CatProgramas P(NoLock) On ( E.IdPrograma = P.IdPrograma ) 

	Update E
	Set SubPrograma = Descripcion 
	From #tmpEmitidos E 
	Inner Join CatSubProgramas P(NoLock) On ( E.IdPrograma = P.IdPrograma And E.IdSubPrograma = P.IdSubPrograma ) 
 
	---------------------------------------
	-- Se obtienen los Vales Registrados --
	---------------------------------------
	Select	IdEmpresa, space(100) as Empresa, 
			IdEstado, space(50) as Estado,
			space(3) as IdJurisdiccion, space(50) as Jurisdiccion, 
			IdFarmacia, space(100) as Farmacia,  
			Folio, FolioVale, FolioVentaGenerado, Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro, 
			SubTotal, Iva, Total, 
			IdProveedor, space(150) as Proveedor			
	Into #tmpRegistrados
	From ValesEnc (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado -- And IdFarmacia = @IdFarmacia 
		And Cast( IdEmpresa + IdEstado + IdFarmacia + FolioVale as varchar ) In
		( Select Cast( IdEmpresa + IdEstado + IdFarmacia + FolioVale as varchar ) as Folio From #tmpEmitidos ) 		
	Order By IdFarmacia, FolioVale

	-- Se obtiene el nombre del Proveedor
	Update E
	Set Proveedor = Nombre 
	From #tmpRegistrados E 
	Inner Join CatProveedores P(NoLock) On ( E.IdProveedor = P.IdProveedor )

	-------------------------------------------------------
	-- Se obtienen los detalles de los vales registrados --
	------------------------------------------------------- 
	Select	E.IdEmpresa, space(100) as Empresa, E.IdEstado, space(50) as Estado, space(3) as IdJurisdiccion, space(50) as Jurisdiccion, 
			E.IdFarmacia, space(100) as Farmacia, 
			E.Folio, E.FolioVale, E.FolioVentaGenerado, Convert( varchar(10), E.FechaRegistro, 120 ) as FechaRegistro, 
			E.SubTotal, E.Iva, E.Total, 
			E.IdProveedor, space(100) as Proveedor,
			space(4) as IdClaveSSA_Sal, space(50) as ClaveSSA,space(7500) as DescripcionSal, 
			D.IdProducto, D.CodigoEAN, space(200) as DescripcionProducto, 
			L.IdSubFarmacia, space(50) as SubFarmacia, L.ClaveLote, L.CantidadRecibida as CantidadLote, 
			( L.CantidadRecibida * D.CostoUnitario ) as SubTotalLote, 
			D.CantidadRecibida as Cantidad, 
			D.CostoUnitario, D.SubTotal as SubTotal_Producto, D.ImpteIva as Iva_Producto, D.Importe as Importe_Producto,
			@iAño as Año, @sNombreMes as Mes
	Into #tmpRegistrados_Detallado
	From ValesEnc E(NoLock)
	Inner Join ValesDet D(NoLock) On( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio )
	Inner Join ValesDet_Lotes L(NoLock) On ( D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.Folio = L.Folio 
		And D.IdProducto = L.IdProducto And D.CodigoEAN = L.CodigoEAN And D.Renglon = L.Renglon )
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado --	And E.IdFarmacia = @IdFarmacia 
		And Cast( E.IdEmpresa + E.IdEstado + E.IdFarmacia + E.FolioVale as varchar ) In
		( Select Cast( IdEmpresa + IdEstado + IdFarmacia + FolioVale as varchar ) as Folio From #tmpEmitidos ) 		
	Order By E.IdFarmacia, E.FolioVale, D.Renglon	

	---------------------------------------------------------------------
	-- Se obtienen los nombres de Empresa,Estado, Farmacia y Proveedor --
	---------------------------------------------------------------------
	-- Se obtiene el nombre de la empresa.
	Update E
	Set Empresa = Nombre 
	From #tmpEmitidos E 
	Inner Join CatEmpresas P(NoLock) On ( E.IdEmpresa = P.IdEmpresa )

	Update E
	Set Empresa = Nombre 
	From #tmpRegistrados E 
	Inner Join CatEmpresas P(NoLock) On ( E.IdEmpresa = P.IdEmpresa )

	Update E
	Set Empresa = Nombre 
	From #tmpRegistrados_Detallado E 
	Inner Join CatEmpresas P(NoLock) On ( E.IdEmpresa = P.IdEmpresa )

	-- Se obtiene el nombre del estado
	Update E
	Set Estado = Nombre
	From #tmpEmitidos E 
	Inner Join CatEstados P(NoLock) On ( E.IdEstado = P.IdEstado )

	Update E
	Set Estado = Nombre
	From #tmpRegistrados E 
	Inner Join CatEstados P(NoLock) On ( E.IdEstado = P.IdEstado )

	Update E
	Set Estado = Nombre
	From #tmpRegistrados_Detallado E 
	Inner Join CatEstados P(NoLock) On ( E.IdEstado = P.IdEstado )

	-- Se obtiene el IdJurisdiccion, Jurisdiccion y nombre de la farmacia
	Update E
	Set E.IdJurisdiccion = P.IdJurisdiccion, E.Jurisdiccion = P.Jurisdiccion, E.Farmacia = P.Farmacia 
	From #tmpEmitidos E 
	Inner Join vw_Farmacias P(NoLock) On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia )

	Update E
	Set E.IdJurisdiccion = P.IdJurisdiccion, E.Jurisdiccion = P.Jurisdiccion, E.Farmacia = P.Farmacia
	From #tmpRegistrados E 
	Inner Join vw_Farmacias P(NoLock) On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia )

	Update E
	Set E.IdJurisdiccion = P.IdJurisdiccion, E.Jurisdiccion = P.Jurisdiccion, E.Farmacia = P.Farmacia 
	From #tmpRegistrados_Detallado E 
	Inner Join vw_Farmacias P(NoLock) On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia )

	-- Se obtiene el nombre del Proveedor
	Update E
	Set Proveedor = Nombre 
	From #tmpRegistrados_Detallado E 
	Inner Join CatProveedores P(NoLock) On ( E.IdProveedor = P.IdProveedor )

	-- Se obtiene los datos del producto
	Update E
	Set IdClaveSSA_Sal = P.IdClaveSSA_Sal, ClaveSSA = P.ClaveSSA, DescripcionSal = P.DescripcionSal, DescripcionProducto = P.Descripcion
	From #tmpRegistrados_Detallado E 
	Inner Join vw_Productos_CodigoEAN P(NoLock) On ( E.IdProducto = P.IdProducto And E.CodigoEAN = P.CodigoEAN )

	-- Se obtiene la subfarmacia
	Update E
	Set SubFarmacia = Descripcion 
	From #tmpRegistrados_Detallado E 
	Inner Join CatFarmacias_SubFarmacias P(NoLock) On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia And E.IdSubFarmacia = P.IdSubFarmacia )

	------------------------------------------------------------------------------------------------------------------------
	-- Se devuelve el resultado de los 3 Reportes... 1.- Emitidos, 2.- Registrados, 3.- Detallado --
	------------------------------------------------------------------------------------------------------------------------
     
	Select	E.IdEmpresa, E.IdEstado, E.IdJurisdiccion, E.IdFarmacia, E.FolioVale as 'Folio Vale', E.FolioVenta as 'Folio Venta', E.NumReceta as 'Numero Receta', 
			Convert(varchar(10), E.FechaReceta, 120) as 'Fecha Receta', P.ClaveSSA, P.DescripcionSal  as 'Descripcion Clave SSA',
			P.Cantidad, E.IdBeneficiario, E.Beneficiario, E.Poliza, E.FechaRegistro as 'Fecha Registro', E.FechaCanje as 'Fecha Canje',
			E.IdCliente, E.Cliente, E.IdSubCliente, E.SubCliente, E.IdPrograma, E.Programa, E.IdSubPrograma, E.SubPrograma, E.Status  
	From #tmpEmitidos E (NoLock)
	Inner Join #tmpCantidadesEmitidas P(NoLock) 
		On ( E.IdEmpresa = P.IdEmpresa And E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia And E.FolioVale = P.FolioVale )  
	Order By E.IdJurisdiccion, E.IdFarmacia, E.FolioVale 

	Select	IdEmpresa, IdEstado, IdJurisdiccion, IdFarmacia, Folio, FolioVale as 'Folio Vale', 
			FolioVentaGenerado as 'Folio Venta Generado', Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro, 
			SubTotal, Iva, Total, IdProveedor, Proveedor				
	From #tmpRegistrados(NoLock) 
	Order By IdJurisdiccion, IdFarmacia, FolioVale 

	Select * From #tmpRegistrados_Detallado(NoLock) Order By IdJurisdiccion, IdFarmacia, FolioVale, IdProducto, CodigoEAN
	   	 
	 

	-- spp_Rpt_COM_OCEN_Vales_Por_Mes
End
Go--#SQL

