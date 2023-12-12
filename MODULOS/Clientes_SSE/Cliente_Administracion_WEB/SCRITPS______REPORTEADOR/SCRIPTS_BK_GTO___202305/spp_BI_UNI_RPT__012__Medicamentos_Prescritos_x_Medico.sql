-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__012__Medicamentos_Prescritos_x_Medico' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__012__Medicamentos_Prescritos_x_Medico
Go--#SQL 

Create Proc spp_BI_UNI_RPT__012__Medicamentos_Prescritos_x_Medico
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '3512',  
	@ClaveSSA varchar(20) = '', 
	@NombreMedico varchar(200) = '', @Servicio varchar(200) = '', 	
	@FechaInicial varchar(10) = '2018-01-01', @FechaFinal varchar(10) = '2028-12-31', 
	@Turno Varchar(20) = '', @Area Varchar(200) = ''
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


------------------------------------------ Generar tablas de catalogos     	   	
--	Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos
	Select Descripcion, HoraEntrada, HoraSalida
	Into #BI_RPT__DTS__TurnosDispensacion 
	From BI_RPT__DTS__TurnosDispensacion 
	Where 1 = 0 

	Set @sSql = 'Insert Into #BI_RPT__DTS__TurnosDispensacion ' +
				'Select Descripcion, HoraEntrada, HoraSalida From BI_RPT__DTS__TurnosDispensacion Where Descripcion like ' + Char(39) + '%' + @Turno + '%' + Char(39)

	Exec(@sSql)  

----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia 
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into #vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, IdFarmacia ' + char(13) + char(10) + 
				'From vw_Farmacias ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 1 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
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
		cast(E.NumReceta as varchar(100)) as NumReceta, 
		convert(varchar(10), E.FechaReceta, 120) as FechaReceta, 
		T.Descripcion As Turno,
		E.IdMedico, 
		cast(E.Medico as varchar(500)) as NombreMedico, 		
		-- cast(E.NumCedula as varchar(100)) as CedulaMedico, 
		cast('' as varchar(100)) as CedulaMedico, 
		cast('' as varchar(100)) as DomicilioMedico, 		
		
		E.IdDiagnostico, 
		cast(E.ClaveDiagnostico as varchar(100)) CIE10, 
		cast(E.Diagnostico as varchar(100)) Diagnostico, 
		
		E.ClaveSSA, E.DescripcionSal as DescripcionClave, -- P.Presentacion, P.Laboratorio, 
		-- L.ClaveLote, 
		--convert(varchar(7), EL.FechaCaducidad, 120) as FechaCaducidad, 
		cast(sum((E.Cantidad) + 0) as int) as CantidadRecetada,  
		cast(sum(E.Cantidad) as int) as CantidadDispensada,  
		0 as CantidadNoDispensada, 
		cast(max(PrecioLicitacion) as numeric(14,4)) as PrecioUnitario, 
		cast(sum(TotalLicitacion) as numeric(14,4)) as CostoTotal 
	Into #tmp_Dispensacion_x_Medico 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	----From VentasEnc E (NoLock) 
	----Inner Join VentasDet D (NoLock) 
	----	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 		
	----Inner Join VentasInformacionAdicional I (NoLock) 
	----	On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	----Inner Join vw_Productos_CodigoEAN__PRCS P (NoLock) 
	----	On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and P.ClaveSSA like '%' + @ClaveSSA + '%%' ) 		
	----Inner Join CatMedicos M (NoLock) 
	----	On ( E.IdEstado = M.IdEstado and E.IdFarmacia = M.IdFarmacia and I.IdMedico = M.IdMedico ) 			
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Inner Join #BI_RPT__DTS__TurnosDispensacion T On (DatePart(HOUR, E.FechaRegistro) Between HoraEntrada And HoraSalida )
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal
		And	E.IdEmpresa = @IdEmpresa 
		And E.Medico like '%' + replace(@NombreMedico, ' ', '%') + '%' 
		And E.ClaveSSA like '%' + @ClaveSSA + '%%'
		And E.Area like '%' +  @Area + '%%'
		--And E.ClaveDiagnostico like '%' + @Diagnostico + '%%'
		-- 1105298594 
		-- ALEMAN LUNA VALENTIN 
		-- and B.FolioReferencia like '%' + ltrim(rtrim(@NumeroDePoliza)) + '%' 
		-- and (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) like '%' + replace(@NombreBeneficiario, ' ', '%') + '%' 
		-- and (M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) like '%' + replace(@NombreMedico, ' ', '%') + '%'  
	Group by 
		E.IdEstado, E.IdFarmacia, 
		convert(varchar(10), E.FechaRegistro, 120), --- E.IdCliente, E.IdSubCliente,
		T.Descripcion,
		E.IdMedico, E.Medico, -- E.NumCedula, 
		cast(E.Diagnostico as varchar(500)), 
		E.IdDiagnostico, E.ClaveDiagnostico, E.Diagnostico,  
		E.NumReceta, convert(varchar(10), E.FechaReceta, 120), 
		E.ClaveSSA, E.DescripcionSal  
	Order By FechaRegistro 

---	Select * from #tmp_Dispensacion_x_Medico 
	


	----Update E Set CIE10 = M.ClaveDiagnostico, Diagnostico = Descripcion  
	----From #tmp_Dispensacion_x_Medico E 
	----Inner Join CatCIE10_Diagnosticos M (NoLock) On ( E.IdDiagnostico = M.ClaveDiagnostico ) 

		

	----Update E Set PrecioUnitario = P.PrecioUnitario, CostoTotal = (P.PrecioUnitario * E.CantidadDispensada) 
	----From #tmp_Dispensacion_x_Medico E 
	----Inner Join vw_Claves_Precios_Asignados__PRCS P (NoLock) On ( E.ClaveSSA = P.ClaveSSA ) 

---------------------		spp_BI_UNI_RPT__012__Medicamentos_Prescritos_x_Medico 



----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Dispensacion_x_Medico 

	Select 
		-- IdFarmacia, 
		'Fecha de dispensación' = FechaRegistro,
		Turno,
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
	From #tmp_Dispensacion_x_Medico M
	Where CantidadDispensada > 0 
	----Group by -- IdFarmacia, 
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA, DescripcionClave    
	----Order By   
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA  



End 
Go--#SQL 


