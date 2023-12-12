-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_ExportarExcel' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_ExportarExcel
Go--#SQL 

/* 
	Exec spp_Rpt_Administrativos_ExportarExcel 0, 0, 0 

	Exec spp_Rpt_Administrativos_ExportarExcel 0, 0, 1  

	Exec spp_Rpt_Administrativos_ExportarExcel 1	
*/ 

Create Proc spp_Rpt_Administrativos_ExportarExcel 
(   
	@MostrarAgrupado tinyint = 0, @IdPerfilAtencion int = 0, @Concentrado int = 1, @IdSubPerfilAtencion int = 0 
)     
As 
Begin 
Set NoCount On 
	
	----select * 
	----from RptAdmonDispensacion_Detallado 
	----Where ClaveSSA = '010.000.0624.00'

	Select *, Cast('' As Varchar(200)) As SAT_UnidadDeMedida, Cast('' As Varchar(200)) As SAT_ClaveDeProducto_Servicio
	Into #RptAdmonDispensacion_Detallado
	From RptAdmonDispensacion_Detallado


	Update F Set F.SAT_UnidadDeMedida = R.SAT_UnidadDeMedida, F.SAT_ClaveDeProducto_Servicio = R.SAT_ClaveDeProducto_Servicio
	From #RptAdmonDispensacion_Detallado F
	Inner Join CFG_ClavesSSA_Precios R (NoLock)
		On (F.IdEstado = R.IdEstado And F.IdCliente = R.IdCliente And F.IdSubCliente = R.IdSubCliente And F.IdClaveSSA_Sal = R.IdClaveSSA_Sal)
	Where R.Status = 'A' And R.SAT_UnidadDeMedida <> ''

	
	Select 
		IdPerfilAtencion, IdSubPerfilAtencion, 
		IdGrupoPrecios, 
		identity(int, 1, 1) as Identificador, 
		ClaveSSA, DescripcionSal as DescripcionClave, space(200) as Presentacion, 
		
		max(PrecioLicitacion) as PrecioLicitacion_Base, 
		max(PrecioLicitacionUnitario) as PrecioLicitacionUnitario_Base, 
		sum(Agrupacion) as Agrupacion_Base, sum(Cantidad) as Cantidad_Base, 

		cast( round( max((case when @MostrarAgrupado = 1 Then PrecioLicitacion Else PrecioLicitacionUnitario End)), 2) as numeric(14,2)) as PrecioUnitario, 
		sum((case when @MostrarAgrupado = 1 Then Agrupacion Else Cantidad End)) as Cantidad, 

		--cast( round( max((case when ProgramaSubPrograma_ForzaCajas = 1 Then PrecioLicitacion Else PrecioLicitacionUnitario End)), 2) as numeric(14,2)) as PrecioUnitario, 
		--sum((case when ProgramaSubPrograma_ForzaCajas = 1 Then Agrupacion Else Cantidad End)) as Cantidad, 

		cast(max(TasaIva) as int) as TasaIva,  
		cast( round( sum(SubTotalLicitacion_0 + SubTotalLicitacion), 2 ) as numeric(14,2)) as SubTotal, 
		cast( round( sum(IvaLicitacion), 2 ) as numeric(14,2)) as Iva, 
		cast( round( sum(TotalLicitacion), 2 ) as numeric(14,2)) as Total, 
		'PIEZA' as UnidadDeMedida, 'IVA' as Impuesto, 
		ClaveLote, FechaCaducidad as Caducidad 
		--'' as ClaveLote, '' Caducidad 	
		, ProgramaSubPrograma_ForzaCajas, ProgramaSubPrograma_ForzaCajas_Habilitado,
		SAT_UnidadDeMedida, SAT_ClaveDeProducto_Servicio
	Into #RptAdmonDispensacion 
	From #RptAdmonDispensacion_Detallado 
	Where 
		-- ClaveSSA = '010.000.2188.00' and 
		IdPerfilAtencion = @IdPerfilAtencion And IdSubPerfilAtencion = @IdSubPerfilAtencion 
	Group by 
		IdPerfilAtencion, IdSubPerfilAtencion, 
		IdGrupoPrecios, 
		ClaveSSA, DescripcionSal, ClaveLote, FechaCaducidad,
		ProgramaSubPrograma_ForzaCajas, ProgramaSubPrograma_ForzaCajas_Habilitado,
		SAT_UnidadDeMedida, SAT_ClaveDeProducto_Servicio
	Order by DescripcionSal 
	
	
	Update R Set Presentacion = S.Presentacion  
	From #RptAdmonDispensacion R 
	Inner Join vw_ClavesSSA_Sales S On ( R.ClaveSSA = S.ClaveSSA ) 




--		spp_Rpt_Administrativos_ExportarExcel 

	-- select * from #RptAdmonDispensacion 

	Update R Set 
		PrecioUnitario = (case when ProgramaSubPrograma_ForzaCajas = 1 Then PrecioLicitacion_Base else PrecioLicitacionUnitario_Base end), 
		Cantidad = (case when ProgramaSubPrograma_ForzaCajas = 1 Then Agrupacion_Base else Cantidad_Base end)
	From #RptAdmonDispensacion R 
	Where ProgramaSubPrograma_ForzaCajas_Habilitado = 1 -- and ProgramaSubPrograma_ForzaCajas = 1  

	-- select * from #RptAdmonDispensacion 


	if @Concentrado = 1  
	Begin 
		print 'xxx' 
		Update R Set ClaveLote = '', Caducidad = '' 
		From #RptAdmonDispensacion R 
	End 
	
--		spp_Rpt_Administrativos_ExportarExcel 

	
---------------------- Salida Final 	
	Select 
		IdPerfilAtencion, IdSubPerfilAtencion, 
		IdGrupoPrecios, 
		Identity(int, 1, 1) as Identificador, 
		'Clave SSA Base' = ClaveSSA, 
		'Clave SSA' = ClaveSSA, 
		--(ClaveSSA + ' - ' + DescripcionClave) as DescripcionClave, 
		'Descripcion' = DescripcionClave, 
		Presentacion, 
		
		round(PrecioUnitario, 2) as PrecioUnitario, 
		sum(Cantidad) as Cantidad, 
				
		TasaIva, 
		sum(round(SubTotal, 2)) as SubTotal, 
		sum(round(Iva, 2)) as Iva, 
		sum(round(Total, 2)) as Total, 
		UnidadDeMedida, Impuesto, 
		
		(case when ClaveLote <> '' then (char(39) + ClaveLote + char(39)) else '' end) as ClaveLote, 
		
		Caducidad,
		SAT_UnidadDeMedida As 'SAT Unidad de Medida', SAT_ClaveDeProducto_Servicio As 'SAT Producto Servicio'
		--, ProgramaSubPrograma_ForzaCajas
	Into #RptAdmonDispensacion_Salida  	
	From #RptAdmonDispensacion  	
	Group by 
		IdPerfilAtencion, IdSubPerfilAtencion,  
		IdGrupoPrecios, 
		-- Identificador, 
		ClaveSSA, DescripcionClave, Presentacion, PrecioUnitario, TasaIva, 
		UnidadDeMedida, Impuesto, ClaveLote, Caducidad,
		SAT_UnidadDeMedida, SAT_ClaveDeProducto_Servicio
		--, ProgramaSubPrograma_ForzaCajas 
	Having sum(Cantidad) > 0 
	Order by DescripcionClave   

--		spp_Rpt_Administrativos_ExportarExcel 


	------------------------------- SALIDA FINAL   
	Select * 
	From #RptAdmonDispensacion_Salida   
	Where Cantidad > 0 


	
End 
Go--#SQL 

