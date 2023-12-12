-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__016__Diagosticos_y_Padecimientos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__016__Diagosticos_y_Padecimientos 
Go--#SQL 

Create Proc spp_BI_RPT__016__Diagosticos_y_Padecimientos  
( 
	@IdEmpresa varchar(3) = '004', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '5533', 
	@ClaveSSA varchar(20) = '', 
	@CIE10 varchar(200) = '', @Diagnostico varchar(200) = '', 	
	@FechaInicial varchar(10) = '2018-04-01', @FechaFinal varchar(10) = '2023-04-05',
	@Servicio Varchar(200) = ''	
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max), 
	@sFiltro varchar(max) 

	Set @sSql = '' 
	Set @sFiltro = '' 


------------------------------------------ Generar tablas de catalogos     	   	
	-- Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa, @IdEstado, '2', '6' 
------------------------------------------ Generar tablas de catalogos  


----------------------------------------------------- DATOS FILTRO 
	Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia
	Into #vw_Farmacias 
	From vw_Farmacias 
	Where 1 = 0 

	Set @sSql = 'Insert Into SII_REPORTEADOR..#vw_Farmacias ( IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ) ' + char(13) + char(10) + 
				'Select IdEstado, IdMunicipio, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia ' + char(13) + char(10) + 
				'From vw_Farmacias__PRCS ' + char(13) + char(10)	
				
	Set @sSql = @sSql + 'Where EsUnidosis = 0 and IdEstado = ' + char(39) + @IdEstado + char(39) + char(13) + char(10)
	
	If @IdMunicipio <> '' and @IdMunicipio <> '*' 
	   Set @sSql = @sSql + ' and IdMunicipio = ' + char(39) + right('0000' + @IdMunicipio, 4) + char(39) + char(13) + char(10)

	If @IdJurisdiccion <> '' and @IdJurisdiccion <> '*' 
	   Set @sSql = @sSql + ' and IdJurisdiccion = ' + char(39) + right('0000' + @IdJurisdiccion, 3) + char(39) + char(13) + char(10)
	   
	If @IdFarmacia <> '' and @IdFarmacia <> '*' 
	   Set @sSql = @sSql + ' and IdFarmacia = ' + char(39) + right('0000' + @IdFarmacia, 4) + char(39) + char(13) + char(10) 	   

	Exec(@sSql)  

----	select top 1 * from RptAdmonDispensacion_Detallado 

----------------------------------------------------- OBTENCION DE DATOS  
	Select 
		E.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia,
		convert(varchar(10), E.FechaRegistro, 120) as FechaRegistro, 
		-- E.IdCliente, E.IdSubCliente, 
		cast(E.NumReceta as varchar(100)) as NumReceta, 
		convert(varchar(10), E.FechaReceta, 120) as FechaReceta, 
		
		E.IdDiagnostico, 
		cast(E.ClaveDiagnostico as varchar(100)) CIE10, 
		cast(E.Diagnostico as varchar(100)) Diagnostico, 
	
		E.IdClaveSSA_Sal as IdClaveSSA, 
		E.ClaveSSA, E.ClaveSSA as ClaveSSA_Fisica, 0 as EsRelacionada, 		
		cast(E.ClaveSSA as varchar(5000)) as NombreGenerico, E.DescripcionSal as DescripcionClave, cast('' as varchar(100)) as Presentacion, -- P.Laboratorio, 
		cast('' as varchar(100)) as FuenteDeFinanciamiento, 
		-- L.ClaveLote, 
		--convert(varchar(7), EL.FechaCaducidad, 120) as FechaCaducidad, 
		cast(sum((E.Cantidad) + 0) as int) as CantidadRecetada,  
		cast(sum(E.Cantidad) as int) as CantidadDispensada,  
		0 as CantidadNoDispensada, 
		cast(max(PrecioLicitacion) as numeric(14,4)) as PrecioUnitario, 
		cast(sum(TotalLicitacion) as numeric(14,4)) as CostoTotal 
	Into #tmp_Dispensacion_x_Medico 
	From SII_REPORTEADOR..RptAdmonDispensacion_Detallado E (NoLock) 
	--From VentasEnc E (NoLock) 
	--Inner Join VentasDet D (NoLock) 
	--	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 		
	--Inner Join VentasInformacionAdicional I (NoLock) 
	--	On ( E.IdEmpresa = I.IdEmpresa and E.IdEstado = I.IdEstado and E.IdFarmacia = I.IdFarmacia and E.FolioVenta = I.FolioVenta ) 
	--Inner Join vw_Productos_CodigoEAN__PRCS P (NoLock) 
	--	On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and P.ClaveSSA like '%' + @ClaveSSA + '%%' ) 		
	--Inner Join CatCIE10_Diagnosticos M (NoLock) On ( I.IdDiagnostico = M.ClaveDiagnostico ) 	
	Inner Join #vw_Farmacias F (Nolock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Where convert(varchar(10), E.FechaRegistro, 120) between @FechaInicial and @FechaFinal and 
		E.IdEmpresa = @IdEmpresa 
		and ( E.ClaveSSA like '%' + @ClaveSSA + '%%' ) 
		and E.Servicio like '%' + @Servicio + '%' 
		and E.Diagnostico like '%' + @Diagnostico + '%' 
		-- 1105298594 
		-- ALEMAN LUNA VALENTIN 
		-- and B.FolioReferencia like '%' + ltrim(rtrim(@NumeroDePoliza)) + '%' 
		-- and (B.ApPaterno + ' ' + B.ApMaterno + ' ' + B.Nombre) like '%' + replace(@NombreBeneficiario, ' ', '%') + '%' 
		-- and (M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) like '%' + replace(@NombreMedico, ' ', '%') + '%' 
		
	Group by 
		E.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, E.IdFarmacia, F.Farmacia,
		convert(varchar(10), E.FechaRegistro, 120), --- E.IdCliente, E.IdSubCliente, 
		E.IdDiagnostico,  E.ClaveDiagnostico, E.Diagnostico,   
		E.NumReceta, convert(varchar(10), E.FechaReceta, 120), 
		E.IdClaveSSA_Sal, E.ClaveSSA, E.DescripcionSal 
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

	---		  spp_BI_RPT__016__Diagosticos_y_Padecimientos  

	Update C Set NombreGenerico = S.DescripcionClave, DescripcionClave = S.DescripcionClave 
	From #tmp_Dispensacion_x_Medico C (NoLock) 
	Inner Join vw_ClavesSSA_Sales S (NoLock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where EsRelacionada =  1 

	--Select * 
	--From #tmp_Dispensacion_x_Medico  
	--Where EsRelacionada =  1 
	-------------------------- Reemplazo de claves 
	-------------------------------------------------------------------------------------------------------- 

---------------------		spp_BI_RPT__016__Diagosticos_y_Padecimientos 


--------------------------------------------- GENERAR LA TABLA FINAL 
	Select top  0 * 
	Into #tmp_Dispensacion_x_Medico___Final 
	From #tmp_Dispensacion_x_Medico 

	-- @CIE10 varchar(200) = '', @Diagnostico varchar(200) = ''  

	Set @sFiltro = '' 

	If @CIE10 <> ''  and @Diagnostico <> '' 
		Begin 
			Set @sFiltro = @sFiltro + ' ( CIE10 like ' + char(39) + '%' + char(39) + ' + ' + char(39) + @CIE10 + char(39) + ' + ' + char(39) + '%' + char(39) + ' ) ' 	  
			Set @sFiltro = @sFiltro + ' and ( Diagnostico like ' + char(39) + '%' + char(39) + ' + ' + char(39) + @Diagnostico + char(39) + ' + ' + char(39) + '%' + char(39) + ' ) '  
		End 
	Else 
		Begin 
			If @CIE10 <> ''  and @Diagnostico = ''
				Set @sFiltro = @sFiltro + ' ( CIE10 like ' + char(39) + '%' + char(39) + ' + ' + char(39) + @CIE10 + char(39) + ' + ' + char(39) + '%' + char(39) + ' ) ' 
		  
			If @Diagnostico <> '' and @CIE10 = '' 
				Set @sFiltro = @sFiltro + ' ( Diagnostico like ' + char(39) + '%' + char(39) + ' + ' + char(39) + @Diagnostico + char(39) + ' + ' + char(39) + '%' + char(39) + ' ) '  
	End 



	If @sFiltro <> '' 
	   Set @sFiltro = 'Where ' + @sFiltro


	Set @sSql = 
		'Insert Into #tmp_Dispensacion_x_Medico___Final ' + char(13) + 
		'Select * ' + char(13) + 
		'From #tmp_Dispensacion_x_Medico ' + char(13) + @sFiltro 
	Exec(@sSql) 
	Print @sSql 


	---------------------- ASIGNAR INFORMACION SEGUN REQUERIMIENTOS DEL CLIENTE 
	If @IdEstado = 13 
	Begin 

		--Update D Set  Procedencia = (case when ClaveLote like '%*%' then 'CONSIGNACIÓN' else 'INTERMED' end) 
		--From #tmp_Caducidades D 

		Update D Set FuenteDeFinanciamiento = C.Financiamiento  
		From #tmp_Dispensacion_x_Medico___Final D 
		Inner Join vw_FACT_FuentesDeFinanciamiento_Claves_PRCS C (NoLock) On ( D.ClaveSSA = C.ClaveSSA )
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

		Update D Set NombreGenerico = C.NombreGenerico, Presentacion = C.Presentacion 
		From #tmp_Dispensacion_x_Medico___Final D 
		Inner Join BI_RPT__DTS__ClavesSSA__CB C (NoLock) On ( D.ClaveSSA = C.ClaveSSA ) 
		Inner Join vw_CB_CuadroBasico_Claves__PRCS CB (NoLock) 
			On ( C.IdEstado = CB.IdEstado and C.IdCliente = CB.IdCliente -- and C.IdSubCliente = CB.IdSubCliente 
				 and C.ClaveSSA = CB.ClaveSSA )

	End 


----------------------------------------------------- SALIDA FINAL 
--	Select * From #tmp_Dispensacion_x_Medico 

	Select
		IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia,
		'Fecha de receta' = FechaReceta, 	
		-- IdFarmacia, 
		-- 'Fecha de dispensación' = FechaRegistro, 
		'Clave SSA' = ClaveSSA, 
		'Nombre genérico' = NombreGenerico, 
		'Descripción Clave SSA' = DescripcionClave, 
		'Presentación' = Presentacion, 		
		-- 'Lote' = ClaveLote, 
		-- 'Caducidad' = FechaCaducidad, 		
		-- 'Laboratorio' = Laboratorio,  		
		
		----'Nombre de beneficiario' = Beneficiario, 
		----'Número de poliza' = Referencia, 
		-- 'Folio de receta' = NumReceta, 		
		'CIE 10' = CIE10, 
		'Diagnóstico' = Diagnostico,   
		
		-- 'Nombre del médico' = NombreMedico, 
		-- 'CedulaMedico' = CedulaMedico, 
		-- 'DomicilioMedico' = DomicilioMedico, 				
		
		
		'Cantidad dispensada' = (CantidadDispensada), 
		'Precio unitario' = (PrecioUnitario), 
		'Costo total' = (CostoTotal)  
	From #tmp_Dispensacion_x_Medico___Final  
	Where CantidadDispensada > 0 
	----Group by -- IdFarmacia, 
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA, DescripcionClave    
	----Order By   
	----	FechaRegistro, Beneficiario, Referencia, NumReceta, ClaveSSA  



End 
Go--#SQL 


