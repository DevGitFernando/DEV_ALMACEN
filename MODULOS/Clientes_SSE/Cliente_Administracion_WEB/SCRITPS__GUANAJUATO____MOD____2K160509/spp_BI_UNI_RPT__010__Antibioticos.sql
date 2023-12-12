If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__010__Antibioticos' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__010__Antibioticos 
Go--#SQL 

Create Proc spp_BI_UNI_RPT__010__Antibioticos 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '88', 
	@ClaveSSA varchar(20) = '', @NumeroDePoliza varchar(20) = '', @NombreBeneficiario varchar(200) = '', 
	@NombreMedico varchar(200) = '', @Servicio varchar(200) = '', 	
	@FechaInicial varchar(10) = '2015-01-01', @FechaFinal varchar(10) = '2015-01-31'	
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
		E.IdEstado, E.IdFarmacia, L.IdSubFarmacia,
		-- E.FolioVenta, 
		-- E.FechaRegistro, 
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		E.IdCliente, E.IdSubCliente, 
		I.IdBeneficiario, 
		cast((B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) as varchar(500)) as Beneficiario, 
		cast(B.FolioReferencia as varchar(100)) as Referencia, 
		cast(I.NumReceta as varchar(100)) as NumReceta, 
		convert(varchar(10), I.FechaReceta, 120) as FechaReceta, 

		I.IdMedico, 
		cast('' as varchar(500)) as NombreMedico, 	
		cast('' as varchar(100)) as CedulaMedico, 	

		--cast((M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) as varchar(500)) as NombreMedico, 		
		--cast(M.NumCedula as varchar(100)) as CedulaMedico, 
		cast('' as varchar(100)) as DomicilioMedico, 		

		P.ClaveSSA, P.DescripcionClave, P.Presentacion, P.Laboratorio, 
		L.ClaveLote, 
		convert(varchar(7), EL.FechaCaducidad, 120) as FechaCaducidad, 
		cast(((L.CantidadVendida) + 0) as int) as CantidadRecetada,  
		cast((L.CantidadVendida) as int) as CantidadDispensada,  
		0 as CantidadNoDispensada, 
		cast(0 as numeric(14,4)) as PrecioUnitario, 
		cast(0 as numeric(14,4)) as CostoTotal 
	Into #tmp_Dispensacion_Controlados 
	From VentasEnc E (NoLock) 
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 		
	Inner Join VentasDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
			And D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN ) 				
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN__ANTIBIOTICOS_PRCS P (NoLock) 
		On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and P.ClaveSSA like '%' + @ClaveSSA + '%%' ) 		
	Inner Join BI_UNI_RPT__DTS__ClavesSSA	M ON ( P.ClaveSSA = M.ClaveSSA )  		
	Inner Join CatBeneficiarios B (NoLock) 
		On ( E.IdEstado = B.IdEstado and E.IdFarmacia = B.IdFarmacia and E.IdCliente = B.IdCliente and E.IdSubCliente = B.IdSubCliente
			and I.IdBeneficiario = B.IdBeneficiario ) 	
	----Inner Join CatMedicos M (NoLock) 
	----	On ( E.IdEstado = M.IdEstado and E.IdFarmacia = M.IdFarmacia and I.IdMedico = M.IdMedico ) 			
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes EL (NoLock)
		On ( L.IdEmpresa = EL.IdEmpresa and L.IdEstado = EL.IdEstado and L.IdFarmacia = EL.IdFarmacia and L.IdSubFarmacia = EL.IdSubFarmacia
			and L.IdProducto = EL.IdProducto and L.CodigoEAN = EL.CodigoEAN and L.ClaveLote = EL.ClaveLote ) 
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- 1105298594 
		-- ALEMAN LUNA VALENTIN 
		and B.FolioReferencia like '%' + ltrim(rtrim(@NumeroDePoliza)) + '%' 
		and (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) like '%' + replace(@NombreBeneficiario, ' ', '%') + '%' 
		-- and (M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) like '%' + replace(@NombreMedico, ' ', '%') + '%' 		
	------Group by 	
	------	convert(varchar(10), E.FechaRegistro, 120), E.IdCliente, E.IdSubCliente, 
	------	I.IdBeneficiario, 
	------	cast((B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) as varchar(500)), 
	------	cast(B.FolioReferencia as varchar(100)), 
	------	I.NumReceta, P.ClaveSSA, P.DescripcionClave 
	Order By FechaRegistro 


	Update E Set CedulaMedico = M.NumCedula, 
		NombreMedico = ( M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre ) 
	From #tmp_Dispensacion_Controlados E 
	Inner Join CatMedicos M (NoLock) 
		On ( E.IdEstado = M.IdEstado and E.IdFarmacia = M.IdFarmacia and E.IdMedico = M.IdMedico ) 


	Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	From #tmp_Dispensacion_Controlados E 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 

---------------------		spp_BI_UNI_RPT__010__Antibioticos 



----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Dispensacion_Controlados 

	Select 
		-- IdFarmacia, 
		'Fecha de dispensación' = FechaRegistro, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 		
		'Lote' = ClaveLote, 
		'Caducidad' = FechaCaducidad, 		
		'Laboratorio' = Laboratorio,  		
		
		'Nombre de beneficiario' = Beneficiario, 
		'Número de poliza' = Referencia, 
		'Folio de receta' = NumReceta, 		
		
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
	----Order By   
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA  



End 
Go--#SQL 


