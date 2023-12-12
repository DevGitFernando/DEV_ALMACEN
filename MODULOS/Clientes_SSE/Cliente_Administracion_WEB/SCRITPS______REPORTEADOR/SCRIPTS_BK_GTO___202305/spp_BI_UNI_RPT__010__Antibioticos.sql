-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__010__Antibioticos' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__010__Antibioticos 
Go--#SQL 

Create Proc spp_BI_UNI_RPT__010__Antibioticos  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '3512', 
	@ClaveSSA varchar(20) = '', @NumeroDePoliza varchar(20) = '', @NombreBeneficiario varchar(200) = '', 
	@NombreMedico varchar(200) = '', @Servicio varchar(200) = '', 	
	@FechaInicial varchar(10) = '2021-01-01', @FechaFinal varchar(10) = '2022-12-31'	
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


------------------------------------------ Generar tablas de catalogos     	   	
	-- Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  





----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia 
	Into SII_REPORTEADOR..#vw_Farmacias 
	From SII_REPORTEADOR..vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into SII_REPORTEADOR..#vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ' + char(13) + char(10) + 
				'From SII_REPORTEADOR..vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 1 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  

	--select * from #vw_Farmacias 


----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEstado, E.IdFarmacia, E.Farmacia, E.IdSubFarmacia,
		-- E.FolioVenta, 
		-- E.FechaRegistro, 
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		E.IdCliente, E.IdSubCliente, 
		E.IdBeneficiario, 
		cast(E.Beneficiario as varchar(500)) as Beneficiario, 
		cast(E.FolioReferencia as varchar(100)) as Referencia, 
		cast(E.NumReceta as varchar(100)) as NumReceta, 
		convert(varchar(10), E.FechaReceta, 120) as FechaReceta, 

		E.IdMedico, 
		cast(E.Medico as varchar(500)) as NombreMedico, 	
		cast('' as varchar(100)) as CedulaMedico, 	

		--cast((M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) as varchar(500)) as NombreMedico, 		
		--cast(M.NumCedula as varchar(100)) as CedulaMedico, 
		cast('' as varchar(100)) as DomicilioMedico, 		

		E.ClaveSSA, E.DescripcionSal as DescripcionClave, 
		P.Presentacion, P.Laboratorio, 
		E.ClaveLote, 
		convert(varchar(7), E.FechaCaducidad, 120) as FechaCaducidad, 
		cast(((E.Cantidad) + 0) as int) as CantidadRecetada,  
		cast((E.Cantidad) as int) as CantidadDispensada,  
		0 as CantidadNoDispensada, 
		cast(PrecioLicitacion as numeric(14,4)) as PrecioUnitario, 
		cast(TotalLicitacion as numeric(14,4)) as CostoTotal 
	Into #tmp_Dispensacion_Controlados 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	Inner Join vw_Productos_CodigoEAN__ANTIBIOTICOS_PRCS P (NoLock) 
		On ( E.IdProducto = P.IdProducto and E.CodigoEAN = P.CodigoEAN and P.ClaveSSA like '%' + @ClaveSSA + '%%' ) 			
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- 1105298594 
		-- ALEMAN LUNA VALENTIN 
		and E.FolioReferencia like '%' + ltrim(rtrim(@NumeroDePoliza)) + '%' 
		and ( E.Beneficiario like '%' + replace(@NombreBeneficiario, ' ', '%') + '%' ) 
		-- and E.EsControlado = 1 
		-- and (M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) like '%' + replace(@NombreMedico, ' ', '%') + '%' 		
	------Group by 	
	------	convert(varchar(10), E.FechaRegistro, 120), E.IdCliente, E.IdSubCliente, 
	------	I.IdBeneficiario, 
	------	cast((B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) as varchar(500)), 
	------	cast(B.FolioReferencia as varchar(100)), 
	------	I.NumReceta, P.ClaveSSA, P.DescripcionClave 
	Order By FechaRegistro 


	Update E Set CedulaMedico = M.NumCedula 
	From #tmp_Dispensacion_Controlados E 
	Inner Join CatMedicos M (NoLock) 
		On ( E.IdEstado = M.IdEstado and E.IdFarmacia = M.IdFarmacia and E.IdMedico = M.IdMedico ) 


	----Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	----From #tmp_Dispensacion_Controlados E 
	----Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 

---------------------		spp_BI_UNI_RPT__010__Antibioticos 



----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Dispensacion_Controlados 

	Select 
		-- IdFarmacia, 
		'Fecha de dispensación' = FechaRegistro, 
		'IdFarmacia' = IdFarmacia, 
		'Farmacia' = Farmacia, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 		
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 		
		'Laboratorio' = Laboratorio,  		
		
		'Nombre de beneficiario' = Beneficiario, 
		'Número de poliza' = Referencia, 
		'Folio de receta' = NumReceta,
		'Fecha de la receta' = '',
		
		'Nombre del médico' = NombreMedico, 
		'CedulaMedico' = CedulaMedico, 
		'DomicilioMedico' = DomicilioMedico, 				
		
		'Cantidad dispensada' = (CantidadDispensada), 
		'Precio unitario' = (PrecioUnitario), 
		'Costo total' = (CostoTotal)  
	From #tmp_Dispensacion_Controlados 
	Where CantidadDispensada > 0 
	----Group by -- IdFarmacia, 
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA, DescripcionClave    
	Order By   
		IdFarmacia, FechaRegistro, ClaveSSA  



End 
Go--#SQL 


