-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_ToExcel' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_ToExcel 
Go--#SQL 

/* 
	Exec spp_Rpt_Administrativos_ToExcel 0, 0, 0 

	Exec spp_Rpt_Administrativos_ToExcel 0, 0, 1  

	Exec spp_Rpt_Administrativos_ToExcel 1	
*/ 

Create Proc spp_Rpt_Administrativos_ToExcel 
(
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '3224', 
	@MostrarAgrupado tinyint = 0, @MostrarPrecios tinyint = 1
) 
As 
Begin 
Set NoCount On 
	
	Set @IdEmpresa = right('00000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('00000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('00000000' + @IdFarmacia, 4) 


	Select 
		IdPerfilAtencion, IdSubPerfilAtencion, PerfilDeAtencion, IdGrupoPrecios, DescripcionGrupoPrecios, EsSeguroPopular, TituloSeguroPopular, 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		IdSubFarmacia, SubFarmacia, Folio, FechaRegistro, 
		IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		IdBeneficiario, Beneficiario, FolioReferencia, EsRecetaForanea, 
		IdUMedica, CLUES_UMedica, Nombre_UMedica, NumReceta, FechaReceta, 
		StatusVenta, SubTotal, Descuento, Iva, Total, 
		IdClaveSSA_Sal, ClaveSSA, DescripcionSal, ContenidoPaquete_ClaveSSA, ContenidoPaquete_ClaveSSA_Licitado, 
		IdGrupoTerapeutico, GrupoTerapeutico, 
		IdProducto, CodigoEAN, ClaveLote, FechaCaducidad, EsConsignacion, 
		ContenidoPaquete, EsControlado, Renglon, DescProducto, DescripcionCorta, UnidadDeSalida, TasaIva, Factor, 

		-- Cantidad, 
		(case when @MostrarAgrupado = 1 Then Agrupacion Else Cantidad End) as Cantidad, 
		
		Agrupacion, 
		Cantidad_A_Cobro, Multiplo, PiezasTotales, AgrupadoMenor, AgrupadoMayor, 
		PiezasSueltas, Agrupacion_Comercial, AgrupadoMenor_Comercial, AgrupadoMayor_Comercial, PiezasSueltas_Comercial, 
		ProgramaSubPrograma_ForzaCajas, ProgramaSubPrograma_ForzaCajas_Habilitado, 
		PrecioUnitario, PrecioLicitacionUnitario, 
		
		-- PrecioLicitacion, 
		cast( round( (case when @MostrarAgrupado = 1 Then PrecioLicitacion Else PrecioLicitacionUnitario End), 2) as numeric(14,2)) as PrecioLicitacion, 		

		ImporteEAN, ImporteEAN_Licitado, 
		SubTotalLicitacion_0, SubTotalLicitacion, IvaLicitacion, TotalLicitacion, 
		Cantidad__Negado, PrecioLicitacion__Negado, ImporteEAN__Negado, SubTotalLicitacion_0__Negado, SubTotalLicitacion__Negado, IvaLicitacion__Negado, TotalLicitacion__Negado, 
		TipoInsumo, TipoDeInsumo, IdMedico, Medico, IdGrupo, GrupoClaves, DescripcionGrupo, SubGrupo, ClaveSubGrupo, DescripcionSubGrupo, 
		IdDiagnostico, ClaveDiagnostico, Diagnostico, IdServicio, Servicio, IdArea, Area, FechaInicial, FechaFinal, 

		PrecioLicitacion as PrecioLicitacion_Base, PrecioLicitacionUnitario as PrecioLicitacionUnitario_Base, 
		Agrupacion as Agrupacion_Base, Cantidad as Cantidad_Base  

	Into #RptAdmonDispensacion 
	From RptAdmonDispensacion_Detallado 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	Order by DescripcionSal 



	Update R Set 
		PrecioLicitacion = (case when ProgramaSubPrograma_ForzaCajas = 1 Then PrecioLicitacion_Base else PrecioLicitacionUnitario_Base end), 
		Cantidad = (case when ProgramaSubPrograma_ForzaCajas = 1 Then Agrupacion_Base else Cantidad_Base end)
	From #RptAdmonDispensacion R 
	Where ProgramaSubPrograma_ForzaCajas_Habilitado = 1 -- and ProgramaSubPrograma_ForzaCajas = 1  


	Update R Set PrecioLicitacion = 0, SubTotalLicitacion_0 = 0, SubTotalLicitacion = 0, IvaLicitacion = 0, TotalLicitacion = 0  
	From #RptAdmonDispensacion R 
	Where @MostrarPrecios = 0 


---		spp_Rpt_Administrativos_ToExcel 


---------------------- Encabezado  
	Select 
		Empresa, (Farmacia) as Farmacia, 
		( 
			cast('Reporte de dispensación de medicamentos y material de curación ' as varchar(500)) + 
			' del ' + convert(varchar(10), FechaInicial, 120) + ' al ' + convert(varchar(10), FechaFinal, 120) 	
		)
		as TituloReporte 
		-- convert(varchar(10), FechaInicial, 120) as FechaInicial, 
		-- convert(varchar(10), FechaFinal, 120) as FechaFinal    
	From #RptAdmonDispensacion 
	Group by Empresa, Farmacia, convert(varchar(10), FechaInicial, 120), convert(varchar(10), FechaFinal, 120)   

	
---------------------- Encabezado  
	Select 
		'Cliente' = NombreCliente, 
		'Sub-Cliente' = NombreSubCliente, 
		'Programa' = Programa, 
		'Sub-Programa' = SubPrograma, 
		'Folio de venta' = Folio, 
		'Fecha de dispensación' = convert(varchar(10), FechaRegistro, 120),
		'Número de receta' = NumReceta, 
		'Fecha de Receta' = FechaReceta, 		
		'Número de poliza' = FolioReferencia, 
		'Nombre del beneficiario' = Beneficiario, 

		'Clave SSA' = ClaveSSA, 		
		'Descripción ClaveSSA ' = DescripcionSal, 
		'Código EAN' = CodigoEAN, 
		'Descripción comercial' = DescProducto, 
		'Clave de Lote' = ClaveLote, 
		'Cantidad' = Cantidad, 
		'Precio' = PrecioLicitacion, 
		'Importe' =  TotalLicitacion 

	From #RptAdmonDispensacion 
	Order by Folio, ClaveSSA 

End 
Go--#SQL 


