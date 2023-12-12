-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_UNI_RPT__012__Medicamentos_Prescritos_x_Medico' and xType = 'P' ) 
   Drop Proc spp_BI_UNI_RPT__012__Medicamentos_Prescritos_x_Medico
Go--#SQL 

/*

Exec spp_BI_UNI_RPT__012__Medicamentos_Prescritos_x_Medico 
	@IdEmpresa = '004',  
	@IdEstado = '11', @IdMunicipio = '*', @IdJurisdiccion = '*', 
	@IdFarmacia = '', 
	@ClaveSSA = '',  

	@NombreMedico = '', @Servicio = '', 	
	@FechaInicial = '2023-04-01', @FechaFinal = '2023-04-05',  
	@Turno = '', @Area = ''  

*/ 

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
	@sSql varchar(max), 
	@sFiltro varchar(max),  
	@sEmpresa varchar(20) 
	 

	Set @sSql = '' 
	Set @sFiltro = '' 
	Set @sEmpresa = 'PHARMAJAL' 



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
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, 
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
		
		E.IdClaveSSA_Sal as IdClaveSSA, 
		E.ClaveSSA, E.ClaveSSA as ClaveSSA_Fisica, 0 as EsRelacionada, 
		cast(E.DescripcionSal as varchar(5000)) as NombreGenerico, E.DescripcionSal as DescripcionClave, cast('' as varchar(100)) as Presentacion, -- P.Laboratorio, 

		cast('' as varchar(500)) as FuenteDeFinanciamiento, 
		E.IdSubFarmacia, 
		E.IdProducto, E.CodigoEAN, 
		E.ClaveLote, 
		cast('' as varchar(10)) as Caducidad, 
		cast('' as varchar(500)) as Laboratorio, 
		cast('' as varchar(500)) as Procedencia, 

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
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, 
		convert(varchar(10), E.FechaRegistro, 120), --- E.IdCliente, E.IdSubCliente,
		T.Descripcion,
		E.IdMedico, E.Medico, -- E.NumCedula, 
		cast(E.Diagnostico as varchar(500)), 
		E.IdDiagnostico, E.ClaveDiagnostico, E.Diagnostico,  
		E.NumReceta, convert(varchar(10), E.FechaReceta, 120), 
		E.IdClaveSSA_Sal, 
		E.ClaveSSA, E.DescripcionSal, 
		E.IdSubFarmacia, 
		E.IdProducto, E.CodigoEAN, 
		E.ClaveLote 			
	Order By FechaRegistro 

---	Select * from #tmp_Dispensacion_x_Medico 
	


	-------------------------------------------------------------------------------------------------------- 
	-------------------------- Reemplazo de claves 
	Update L Set EsRelacionada = 1, ClaveSSA = P.ClaveSSA, IdClaveSSA = P.IdClaveSSA 
	From #tmp_Dispensacion_x_Medico L (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves P (NoLock) 
		On ( --L.IdEstado = P.IdEstado and L.IdCliente = P.IdCliente and L.IdSubCliente = P.IdSubCliente and 
			L.IdClaveSSA = P.IdClaveSSA_Relacionada and P.Status = 'A' ) 
	Inner Join BI_RPT__DTS__Configuracion_Operacion OP (NoLock) 
		On ( P.IdEstado = OP.IdEstado and P.IdCliente = OP.IdCliente and P.IdSubCliente = OP.IdSubCliente ) 

	---		  spp_BI_RPT__015__Medicamentos_Prescritos_x_Medico 

	Update C Set NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave 
	From #tmp_Dispensacion_x_Medico C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where EsRelacionada =  1 

	--Select * 
	--From #tmp_Dispensacion_x_Medico  
	--Where EsRelacionada =  1 
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 


	-------------------------------------------------------------------------------------------------------- 
	-------------------------- Completar la información comercial del producto 
	Update D Set Caducidad = convert(varchar(7), F.FechaCaducidad, 120)  
	From #tmp_Dispensacion_x_Medico D 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On 
		( 
			D.IdEmpresa = F.IdEmpresa and D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia and D.IdSubFarmacia = F.IdSubFarmacia 
			and D.IdProducto = F.IdProducto and D.CodigoEAN = F.CodigoEAN and D.ClaveLote = F.ClaveLote
		) 

	Update D Set Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACION' Else @sEmpresa end)
	From #tmp_Dispensacion_x_Medico D 
	Where ClaveLote <> '' 

	Update D Set Laboratorio = P.Laboratorio, Presentacion = P.Presentacion  
	From #tmp_Dispensacion_x_Medico D 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 


	---------------------    FUENTE DE FINANCIAMIENTO 
	If Exists ( Select * From Sysobjects (nolock) Where Name = 'BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento' and xType = 'U' ) 
	Begin 
		Update D Set FuenteDeFinanciamiento = F.FuenteFinanciamiento 
		From #tmp_Dispensacion_x_Medico D 
		Inner Join BI_RPT__DTS__ClavesSSA__FuentesDeFinanciamiento F (NoLock) On ( D.ClaveSSA = F.ClaveSSA ) 
	End 
	-------------------------- Completar la información comercial del producto 	
	-------------------------------------------------------------------------------------------------------- 	


---------------------		spp_BI_UNI_RPT__012__Medicamentos_Prescritos_x_Medico 



----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Dispensacion_x_Medico 

	Select 
		-- IdFarmacia, 
		'Fecha de dispensación' = FechaRegistro,
		Turno,
		'Clave SSA' = ClaveSSA, 
		'Descripción Clave SSA' = DescripcionClave, 

		'Presentación' = Presentacion, 		

		'Lote' = ClaveLote, 
		'Caducidad' = Caducidad, 
		'Fuente de financiamiento' = FuenteDeFinanciamiento, 
		'Laboratorio' = Laboratorio, 
		'Procedencia' = Procedencia, 

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


