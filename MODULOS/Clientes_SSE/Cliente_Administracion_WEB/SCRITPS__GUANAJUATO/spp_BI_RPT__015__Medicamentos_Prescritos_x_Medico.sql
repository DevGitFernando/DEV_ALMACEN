If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__015__Medicamentos_Prescritos_x_Medico' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__015__Medicamentos_Prescritos_x_Medico 
Go--#SQL 

Create Proc spp_BI_RPT__015__Medicamentos_Prescritos_x_Medico 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '88', 
	@ClaveSSA varchar(20) = '', 
	@NombreMedico varchar(200) = 'maci feli', 	
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
		E.IdEstado, E.IdFarmacia, 
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		-- E.IdCliente, E.IdSubCliente, 
		cast(I.NumReceta as varchar(100)) as NumReceta, 
		convert(varchar(10), I.FechaReceta, 120) as FechaReceta, 

		I.IdMedico, 
		cast((M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) as varchar(500)) as NombreMedico, 		
		cast(M.NumCedula as varchar(100)) as CedulaMedico, 
		cast('' as varchar(100)) as DomicilioMedico, 		
		
		I.IdDiagnostico, 
		cast('' as varchar(100)) CIE10, 
		cast('' as varchar(300)) Diagnostico, 
		
		P.ClaveSSA, P.DescripcionClave, -- P.Presentacion, P.Laboratorio, 
		-- L.ClaveLote, 
		--convert(varchar(7), EL.FechaCaducidad, 120) as FechaCaducidad, 
		cast(sum((D.CantidadVendida) + 0) as int) as CantidadRecetada,  
		cast(sum(D.CantidadVendida) as int) as CantidadDispensada,  
		0 as CantidadNoDispensada, 
		cast(0 as numeric(14,4)) as PrecioUnitario, 
		cast(0 as numeric(14,4)) as CostoTotal 
	Into #tmp_Dispensacion_x_Medico 
	From VentasEnc E (NoLock) 
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 		
	Inner Join VentasInformacionAdicional I (NoLock) 
		On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN__PRCS P (NoLock) 
		On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and P.ClaveSSA like '%' + @ClaveSSA + '%%' ) 		
	Inner Join CatMedicos M (NoLock) 
		On ( E.IdEstado = M.IdEstado and E.IdFarmacia = M.IdFarmacia and I.IdMedico = M.IdMedico ) 			
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		-- 1105298594 
		-- ALEMAN LUNA VALENTIN 
		-- and B.FolioReferencia like '%' + ltrim(rtrim(@NumeroDePoliza)) + '%' 
		-- and (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) like '%' + replace(@NombreBeneficiario, ' ', '%') + '%' 
		and (M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) like '%' + replace(@NombreMedico, ' ', '%') + '%' 		
	Group by 
		E.IdEstado, E.IdFarmacia, 
		convert(varchar(10), E.FechaRegistro, 120), --- E.IdCliente, E.IdSubCliente, 
		I.IdMedico, M.NumCedula, 
		cast((M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) as varchar(500)), 
		I.IdDiagnostico,  
		I.NumReceta, convert(varchar(10), I.FechaReceta, 120), 
		P.ClaveSSA, P.DescripcionClave 
	Order By FechaRegistro 

---	Select * from #tmp_Dispensacion_x_Medico 
	


	Update E Set CIE10 = M.ClaveDiagnostico, Diagnostico = Descripcion  
	From #tmp_Dispensacion_x_Medico E 
	Inner Join CatCIE10_Diagnosticos M (NoLock) On ( E.IdDiagnostico = M.ClaveDiagnostico ) 

		

	Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	From #tmp_Dispensacion_x_Medico E 
	Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 

---------------------		spp_BI_RPT__015__Medicamentos_Prescritos_x_Medico 



----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Dispensacion_x_Medico 

	Select 
		-- IdFarmacia, 
		'Fecha de dispensación' = FechaRegistro, 
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 
		-- 'Presentación' = Presentacion, 		
		-- 'Lote' = ClaveLote, 
		-- 'Caducidad' = FechaCaducidad, 		
		-- 'Laboratorio' = Laboratorio,  		
		
		----'Nombre de beneficiario' = Beneficiario, 
		----'Número de poliza' = Referencia, 
		'Fecha de receta' = FechaReceta, 
		'Folio de receta' = NumReceta, 		
		'CIE 10' = CIE10, 
		'Diagnóstico' = Diagnostico,   
		
		'Nombre del médico' = NombreMedico, 
		'CedulaMedico' = CedulaMedico, 
		-- 'DomicilioMedico' = DomicilioMedico, 				
		
		
		'Cantidad dispensada' = (CantidadDispensada), 
		'Precio unitario' = (PrecioUnitario), 
		'Costo total' = (CostoTotal)  
	From #tmp_Dispensacion_x_Medico 
	Where CantidadDispensada > 0 
	----Group by -- IdFarmacia, 
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA, DescripcionClave    
	----Order By   
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA  



End 
Go--#SQL 


