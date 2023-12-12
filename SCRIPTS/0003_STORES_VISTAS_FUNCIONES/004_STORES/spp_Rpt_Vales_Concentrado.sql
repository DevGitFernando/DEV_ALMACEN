---------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_Vales_Concentrado' And xType = 'P' )
	Drop Proc spp_Rpt_Vales_Concentrado
Go--#SQL

Create Procedure spp_Rpt_Vales_Concentrado 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	--@IdFarmacia varchar(4) = '2224', 	
	--@iAño int = 2016, @iMes int = 1,  
	@FechaInicial varchar(10) = '2015-01-01', @FechaFinal varchar(10) = '2016-01-31', 
	@iMostrarResultado smallint = 0 
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
	--Exec @sNombreMes = dbo.fg_NombresDeMesNumero @iMes
	
	-------------------------------------
	-- Se obtienen los Vales Emitidos  --
	-------------------------------------
	Select	IdEmpresa, space(100) as Empresa, 
			IdEstado, space(50) as Estado, 
			IdFarmacia, space(100) as Farmacia, 
			FolioVale, FolioVenta, space(20) as NumReceta, 
			Cast( 0 as Numeric(14,4) )as Cantidad, 
			space(8) as IdBeneficiario, space(200) as Beneficiario,  
			space(20) as Poliza, 
			Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro, 
			Convert( varchar(10), FechaCanje, 120) as FechaCanje, 
			IdCliente, space(100) as Cliente, 
			IdSubCliente, space(100) as SubCliente, 
			IdPrograma, space(100) as Programa, 
			IdSubPrograma, space(100) as SubPrograma, 
			( Case When Status = 'A' Then 'ACTIVO' Else ( Case When Status = 'C' Then 'CANCELADO' Else 'REGISTRADO' End ) End ) as Status 
	Into #tmpEmitidos
	From Vales_EmisionEnc(NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado -- And IdFarmacia = @IdFarmacia 
		-- And Year( FechaRegistro ) = @iAño And Month( FechaRegistro ) = @iMes 
		And convert(varchar(10), FechaRegistro, 120) Between @FechaInicial  and @FechaFinal  
	Order By FolioVale 
 
 
	-- Se obtienen los IdBeneficiarios de cada vale
	Update E
	Set IdBeneficiario = P.IdBeneficiario, NumReceta = P.NumReceta
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


	-- Se obtienen la cantidad emitida de cada vale
	Select IdEmpresa, IdEstado, IdFarmacia, FolioVale, Sum(Cantidad) as Cantidad 
	Into #tmpCantidadesEmitidas
	From Vales_EmisionDet D(NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado -- And IdFarmacia = @IdFarmacia 
		And FolioVale In ( Select FolioVale From #tmpEmitidos(NoLock) )
	Group By IdEmpresa, IdEstado, IdFarmacia, FolioVale
	Order By FolioVale 

	Update E
	Set Cantidad = P.Cantidad
	From #tmpEmitidos E 
	Inner Join #tmpCantidadesEmitidas P(NoLock) 
		On ( E.IdEmpresa = P.IdEmpresa And E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia And E.FolioVale = P.FolioVale ) 
 
 
 
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
			IdFarmacia, space(100) as Farmacia,  
			Folio, FolioVale, FolioVentaGenerado, Convert( varchar(10), FechaRegistro, 120 ) as FechaRegistro, 
			SubTotal, Iva, Total, 
			IdProveedor, space(150) as Proveedor			
	Into #tmpRegistrados
	From ValesEnc (NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado -- And IdFarmacia = @IdFarmacia 
		And Cast( IdEmpresa + IdEstado + IdFarmacia + FolioVale as varchar ) In
		( Select Cast( IdEmpresa + IdEstado + IdFarmacia + FolioVale as varchar ) as Folio From #tmpEmitidos ) 		
	Order By FolioVale

	-- Se obtiene el nombre del Proveedor
	Update E
	Set Proveedor = Nombre 
	From #tmpRegistrados E 
	Inner Join CatProveedores P(NoLock) On ( E.IdProveedor = P.IdProveedor )

	-------------------------------------------------------
	-- Se obtienen los detalles de los vales registrados --
	------------------------------------------------------- 
	Select	E.IdEmpresa, space(100) as Empresa, E.IdEstado, space(50) as Estado, 
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
			year(E.FechaRegistro) as Año, 
			dbo.fg_NombresDeMesNumero(month(E.FechaRegistro)) as Mes
	Into #tmpRegistrados_Detallado
	From ValesEnc E(NoLock)
	Inner Join ValesDet D(NoLock) On( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio )
	Inner Join ValesDet_Lotes L(NoLock) On ( D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.Folio = L.Folio 
		And D.IdProducto = L.IdProducto And D.CodigoEAN = L.CodigoEAN And D.Renglon = L.Renglon )
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado -- And E.IdFarmacia = @IdFarmacia 
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

	-- Se obtiene el nombre de la farmacia
	Update E
	Set Farmacia = NombreFarmacia 
	From #tmpEmitidos E 
	Inner Join CatFarmacias P(NoLock) On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia )

	Update E
	Set Farmacia = NombreFarmacia 
	From #tmpRegistrados E 
	Inner Join CatFarmacias P(NoLock) On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia )

	Update E
	Set Farmacia = NombreFarmacia 
	From #tmpRegistrados_Detallado E 
	Inner Join CatFarmacias P(NoLock) On ( E.IdEstado = P.IdEstado And E.IdFarmacia = P.IdFarmacia )

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
	-- Se devuelve el resultado solicitado 0.- Ambos( Mostrar en Pantalla ), 1.- Emitidos, 2.- Registrados, 3.- Detallado --
	------------------------------------------------------------------------------------------------------------------------ 	
	Select * From #tmpEmitidos(NoLock) Order By IdEstado, IdFarmacia, FolioVale 
	Select * From #tmpRegistrados(NoLock) Order By IdEstado, IdFarmacia, FolioVale       
	Select * From #tmpRegistrados_Detallado(NoLock) Order By IdEstado, IdFarmacia, FolioVale, IdProducto, CodigoEAN 



	-- spp_Rpt_Vales_Concentrado
End
Go--#SQL

