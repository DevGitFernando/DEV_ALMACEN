If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__008__Beneficiario_Atendido__Detalle' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__008__Beneficiario_Atendido__Detalle 
Go--#SQL 

Create Proc spp_BI_UNI_RPT__008__Beneficiario_Atendido__Detalle 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '0002', @IdJurisdiccion varchar(3) = '008', 
	@IdFarmacia varchar(4) = '0501', 
	@NumeroDePoliza varchar(20) = '1104007803-1', @NombreBeneficiario varchar(200) = 'BARROSO LOPEZ MA GUADALUPE', 
	@FechaInicial varchar(10) = '2016-06-27', @FechaFinal varchar(10) = '2016-07-12'	
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


------------------------------------------ Generar tablas de catalogos     	   	
	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  
 	



----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia 
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		-- E.IdEstado, E.IdFarmacia, 
		-- E.FolioVenta, 
		-- E.FechaRegistro, 
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		E.IdCliente, E.IdSubCliente, 
		I.IdBeneficiario, 
		--cast(B.NombreCompleto as varchar(500)) as Beneficiario, 
		cast((B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) as  varchar(500)) as Beneficiario,
		cast(B.FolioReferencia as varchar(100)) as Referencia, 
		cast(I.NumReceta as varchar(100)) as NumReceta,
		P.ClaveSSA, P.DescripcionClave, 
		cast((sum(D.CantidadVendida) + 0) as int) as CantidadRecetada,  
		cast(sum(D.CantidadVendida) as int) as CantidadDispensada,  
		0 as CantidadNoDispensada, 
		cast(0 as numeric(14,4)) as PrecioUnitario, 
		cast(0 as numeric(14,4)) as CostoTotal 
	Into #tmp_Beneficiarios 
	From VentasEnc E (NoLock) 
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 		
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN__PRCS P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 		
	Inner Join CatBeneficiarios B (NoLock) 
		On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente
			and I.IdBeneficiario = B.IdBeneficiario ) 	
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa and 
		Exists 
		( 
			Select * 
			From #vw_Farmacias F 
			Where E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia 
		) 
		-- 1105298594 
		-- ALEMAN LUNA VALENTIN 
		and B.FolioReferencia like '%' + ltrim(rtrim(@NumeroDePoliza)) + '%' 
		--and (B.NombreCompleto) like '%' + replace(@NombreBeneficiario, ' ', '%') + '%' 
		and cast((B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) as  varchar(500)) like '%' + replace(@NombreBeneficiario, ' ', '%') + '%' 
	Group by 	
		convert(varchar(10), E.FechaRegistro, 120), E.IdCliente, E.IdSubCliente, 
		I.IdBeneficiario, 
		--cast(B.NombreCompleto as varchar(500)), 
		cast((B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) as  varchar(500)),
		cast(B.FolioReferencia as varchar(100)), 
		I.NumReceta, P.ClaveSSA, P.DescripcionClave 
	Order By FechaRegistro 


	Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	From #tmp_Beneficiarios E 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 
	
	
---------------------		spp_BI_UNI_RPT__008__Beneficiario_Atendido__Detalle



----------------------------------------------------- SALIDA FINAL 
	Select 
		-- IdFarmacia, 
		'Fecha de Atención' = FechaRegistro, 
		'Nombre de beneficiario' = Beneficiario, 
		'Número de poliza' = Referencia, 
		'Folio de receta' = NumReceta, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Cantidad recetada' = sum(CantidadRecetada), 
		'Cantidad dispensada' = sum(CantidadDispensada), 
		'Cantidad no dispensada' = sum(CantidadNoDispensada), 
		'Precio unitario' = max(PrecioUnitario), 
		'Costo total' = sum(CostoTotal)  
	From #tmp_Beneficiarios 
	Group by -- IdFarmacia, 
		FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA, DescripcionClave    
	Order By   
		FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA  



End 
Go--#SQL 


