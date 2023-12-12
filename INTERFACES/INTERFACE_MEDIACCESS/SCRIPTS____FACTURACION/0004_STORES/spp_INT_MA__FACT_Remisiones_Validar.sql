------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_INT_MA__FACT_Remisiones_Validar' and xType = 'P' )
    Drop Proc spp_INT_MA__FACT_Remisiones_Validar
Go--#SQL
  
Create Proc spp_INT_MA__FACT_Remisiones_Validar 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', 
	@IdFarmaciaGenera varchar(4) = '0001',  @FolioRemision varchar(10) = '163' 
)
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@IdFuenteFinanciamiento varchar(8), 
	@IdFinanciamiento varchar(8), 
	@iLargoMenor int, 
	@iLargoMayor int  	
	
	
	Set @IdEmpresa = right('0000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000' + @IdEstado, 2)
	Set @IdFarmaciaGenera = right('0000000000' + @IdFarmaciaGenera, 4)
	Set @FolioRemision = right('0000000000' + @FolioRemision, 10)				
	
--	select * from FACT_Remisiones 

	---------------------------------------
	-- Se obtienen los datos principales -- 
	---------------------------------------
	------Select	
	------		E.IdEmpresa, Cast( '' as varchar(100) ) as Empresa, E.IdEstado, Cast( '' as varchar(50) ) as Estado, 
	------		E.IdFarmaciaGenera, Cast( '' as varchar(200) ) as FarmaciaGenera, 
	------		E.IdFarmacia, Cast( '' as varchar(200) ) as Farmacia, 
	------		E.FolioRemision, 
	------		0 as ValidarPolizaBeneficiario,  
	------		--- '' as IdFuenteFinanciamiento, '' as IdFinanciamiento, Cast( '' as varchar(100) ) as Financiamiento, 
	------		E.IdCliente, Cast( '' as varchar(100) ) as Cliente, 
	------		E.IdSubCliente, Cast( '' as varchar(100) ) as SubCliente, 
	------		E.IdPrograma, Cast( '' as varchar(100) ) as Programa, 
	------		E.IdSubPrograma, Cast( '' as varchar(100) ) as SubPrograma, 
	------		E.TipoDeRemision, ( Case When TipoDeRemision = 1 Then 'Insumo' Else 'Administración' End ) as TipoDeRemision_Descripcion,
	------		E.FechaRemision, E.FechaValidacion, E.EsFacturable as EsFacturada, E.EsExcedente, 
	------		E.IdPersonalRemision, Cast( '' as varchar(150) ) as PersonalRemision, 
	------		E.IdPersonalValida, Cast( '' as varchar(150) ) as PersonalValida, 
	------		cast(E.SubTotalSinGrabar as numeric(14,2)) as SubTotalSinGrabar_Remision, 
	------		cast(E.SubTotalGrabado as numeric(14,2)) as SubTotalGrabado_Remision, 
	------		cast(E.Iva as numeric(14,2)) as Iva_Remision, 
	------		cast(E.Total as numeric(14,2)) as Total_Remision, 
	------		E.Status, GetDate() as FechaImpresion, 
	------		'' as ClaveSSA, Cast( '' as varchar(7500) ) as ClaveSSA_Descripcion, 
	------		cast(0 as numeric(14,2)) as PrecioLicitado, 
	------		cast(0 as numeric(14,4)) as Cantidad, 
	------		cast(0 as numeric(14,2)) as TasaIva, 
	------		cast(IsNull(E.SubTotalSinGrabar, 0) as numeric(14,2)) as SubTotalSinGrabar, 
	------		cast(IsNull(E.SubTotalGrabado, 0) as numeric(14,2)) as SubTotalGrabado, 
	------		cast(IsNull(E.Iva, 0) as numeric(14,2)) as Iva, 
	------		cast(IsNull(E.Total, 0) as numeric(14,2)) as Importe 
	------Into #tmpCodigos 
	------From FACT_Remisiones E (NoLock) 
	------Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision
	
	
------------------- INFORMACION BASE 
	Select	
		identity(int, 1, 1) as Keyx, 
			E.IdEmpresa, Cast( '' as varchar(100) ) as Empresa, E.IdEstado, Cast( '' as varchar(50) ) as Estado, 
			E.IdFarmaciaGenera, Cast( '' as varchar(200) ) as FarmaciaGenera, 
			E.IdFarmacia, Cast( '' as varchar(200) ) as Farmacia, 
			E.FolioRemision, 
			0 as ValidarPolizaBeneficiario,  
			--- '' as IdFuenteFinanciamiento, '' as IdFinanciamiento, Cast( '' as varchar(100) ) as Financiamiento, 
			E.IdCliente, Cast( '' as varchar(100) ) as Cliente, 
			E.IdSubCliente, Cast( '' as varchar(100) ) as SubCliente, 
			E.IdPrograma, Cast( '' as varchar(100) ) as Programa, 
			E.IdSubPrograma, Cast( '' as varchar(100) ) as SubPrograma, 
			E.TipoDeRemision, ( Case When TipoDeRemision = 1 Then 'Insumo' Else 'Administración' End ) as TipoDeRemision_Descripcion,
			E.FechaRemision, E.FechaValidacion, E.EsFacturable as EsFacturada, E.EsExcedente, 
			E.IdPersonalRemision, Cast( '' as varchar(150) ) as PersonalRemision, 
			E.IdPersonalValida, Cast( '' as varchar(150) ) as PersonalValida, 
			cast(E.SubTotalSinGrabar as numeric(14,2)) as SubTotalSinGrabar_Remision, 
			cast(E.SubTotalGrabado as numeric(14,2)) as SubTotalGrabado_Remision, 
			cast(E.Iva as numeric(14,2)) as Iva_Remision, 
			cast(E.Total as numeric(14,2)) as Total_Remision, 			
			D.FolioVenta, D.IdProducto, D.CodigoEAN, 
			P.ClaveSSA, P.Descripcion as DescripcionComercial, 		
			cast((D.PrecioUnitario) as numeric(14,2)) as PrecioUnitario, 
			cast((D.PrecioUnitario) as numeric(14,2)) as PrecioUnitario_Aseguradora, 		
			
			cast(I.Porcentaje_02 as numeric(14, 2)) as Porcenjate, 
			(I.Porcentaje_02 / 100.00) as Porcenjate_Aplicar, 
			
			cast((D.CantidadVendida) as numeric(14,4)) as Cantidad, 
			cast((D.TasaIva) as numeric(14,2)) as TasaIva, 	
			cast(0 as numeric(14,2)) as SubTotal, 
			cast(0 as numeric(14,2)) as Iva, 
			cast(0 as numeric(14,2)) as Importe,  
			cast('' as varchar(100)) as UnidadDeMedida 
	Into #tmpCodigos 
	From FACT_Remisiones E (NoLock)	
	Inner Join VentasDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioRemision = D.FolioRemision ) 
	Inner Join INT_MA_Ventas_Importes I (NoLock) 
		On ( D.IdEmpresa = I.IdEmpresa And D.IdEstado = I.IdEstado And D.IdFarmacia = I.IdFarmacia And D.FolioVenta = I.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )	
	Where E.IdEmpresa = @IdEmpresa And E.IdEstadoGenera = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision			
	
--	select * from #tmpCodigos 
	
	
	Update P Set PrecioUnitario_Aseguradora = (PrecioUnitario * Porcenjate_Aplicar) 
	From #tmpCodigos P (NoLock) 
	
	Update P Set 
		SubTotal = (PrecioUnitario_Aseguradora * Cantidad),  
		Iva = ((PrecioUnitario_Aseguradora * Cantidad) * ( TasaIva / 100.00 )),  		
		Importe = ((PrecioUnitario_Aseguradora * Cantidad) * ( 1 + ( TasaIva / 100.00 ))) 
	From #tmpCodigos P (NoLock) 
	
--	select * from #tmpCodigos 
			
------------------- INFORMACION BASE  		
	
	
	
	--------Update R Set ValidarPolizaBeneficiario = D.ValidarPolizaBeneficiario 
	--------From #tmpCodigos R 
	--------Inner Join FACT_Fuentes_De_Financiamiento_Detalles  D (NoLock) 
	--------	On ( R.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and R.IdFinanciamiento = D.IdFinanciamiento ) 
	
	
--		spp_INT_MA__FACT_Remisiones_Validar 	
	
	------------------------------------------------------------------------------
	-------- Se obtiene el IdCliente y SubCliente de la Clave de Financiamiento --
	------------------------------------------------------------------------------
	------Update C Set IdCliente = F.IdCliente, IdSubCliente = F.IdSubCliente
	------From #tmpCodigos C(NoLock) 
	------Inner Join FACT_Fuentes_De_Financiamiento F(NoLock) On( C.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento ) 



	-----------------------------------
	-- Se obtienen las Descripciones --
	-----------------------------------
	-- Se obtienen el nombre de la Empresa
	Update C Set Empresa = F.Nombre
	From #tmpCodigos C(NoLock)
	Inner Join CatEmpresas F(NoLock) On( C.IdEmpresa = F.IdEmpresa ) 

	-- Se obtienen el nombre del Estado
	Update C Set Estado = F.Nombre
	From #tmpCodigos C(NoLock)
	Inner Join CatEstados F(NoLock) On( C.IdEstado = F.IdEstado ) 

	-- Se obtienen el nombre de Farmacia Genera
	Update C Set FarmaciaGenera = F.NombreFarmacia
	From #tmpCodigos C(NoLock)
	Inner Join CatFarmacias F(NoLock) On( C.IdEstado = F.IdEstado And C.IdFarmaciaGenera = F.IdFarmacia ) 

	-- Se obtiene el nombre de Farmacia
	Update C Set Farmacia = F.NombreFarmacia
	From #tmpCodigos C(NoLock)
	Inner Join CatFarmacias F(NoLock) On( C.IdEstado = F.IdEstado And C.IdFarmacia = F.IdFarmacia ) 

	-- Se obtienen la descripcion del Cliente
	Update C Set Cliente = F.Nombre
	From #tmpCodigos C(NoLock)
	Inner Join CatClientes F(NoLock) On( C.IdCliente = F.IdCliente ) 

	-- Se obtienen la descripcion del SubCliente
	Update C Set SubCliente = F.Nombre
	From #tmpCodigos C(NoLock)
	Inner Join CatSubClientes F(NoLock) On( C.IdCliente = F.IdCliente And C.IdSubCliente = F.IdSubCliente ) 

	-- Se obtienen la descripcion del Programa
	Update C Set Programa = F.Descripcion
	From #tmpCodigos C(NoLock)
	Inner Join CatProgramas F(NoLock) On( C.IdPrograma = F.IdPrograma ) 

	-- Se obtienen la descripcion del SubPrograma
	Update C Set SubPrograma = F.Descripcion
	From #tmpCodigos C(NoLock)
	Inner Join CatSubProgramas F(NoLock) On( C.IdPrograma = F.IdPrograma And C.IdSubPrograma = F.IdSubPrograma ) 

	------ Se obtienen la descripcion de la Clave SSA
	----Update C Set ClaveSSA_Descripcion = F.Descripcion
	----From #tmpCodigos C(NoLock)
	----Inner Join CatClavesSSA_Sales F(NoLock) On( C.ClaveSSA = F.ClaveSSA ) 

	-- Se obtienen la descripcion del Personal Remision
	Update C Set PersonalRemision = ( F.Nombre + ' ' + IsNull(F.ApPaterno, '') + ' ' + IsNull(F.ApMaterno, '') )   
	From #tmpCodigos C(NoLock)
	Inner Join CatPersonal F(NoLock) On( C.IdEstado = F.IdEstado And C.IdFarmaciaGenera = F.IdFarmacia And C.IdPersonalRemision = F.IdPersonal ) 

	-- Se obtienen la descripcion del Personal Valida
	Update C Set PersonalValida = ( F.Nombre + ' ' + IsNull(F.ApPaterno, '') + ' ' + IsNull(F.ApMaterno, '') )   
	From #tmpCodigos C(NoLock)
	Inner Join CatPersonal F(NoLock) On( C.IdEstado = F.IdEstado And C.IdFarmaciaGenera = F.IdFarmacia And C.IdPersonalValida = F.IdPersonal ) 

	-------- Se obtiene la descripcion del Financiamiento
	------Update C Set Financiamiento = F.Descripcion
	------From #tmpCodigos C(NoLock) 
	------Inner Join FACT_Fuentes_De_Financiamiento_Detalles F (NoLock) On( C.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And C.IdFinanciamiento = F.IdFinanciamiento ) 


	---------------------------------------------------------------
	-- Se obtienen los detalles que se mostraran en el List View -- 
	---------------------------------------------------------------
	Select	'Id Farmacia' = IdFarmacia, Farmacia, 
			'Código EAN' = CodigoEAN, 
			'Descripción comercial' = DescripcionComercial, 
			'Precio' = PrecioUnitario_Aseguradora, 
			'Cantidad' = cast(sum(Cantidad) as int), 
			'% Iva' = sum(TasaIva), 
			'Sub-Total' = sum(SubTotal), 
			'Iva' = sum(Iva), 
			'Importe' = sum(Importe)
	Into #tmpDetalles
	From #tmpCodigos 
	Where Cantidad > 0   -- Status <> 'C' -- and Cantidad > 0 
	Group by IdFarmacia, Farmacia, CodigoEAN, DescripcionComercial, PrecioUnitario_Aseguradora 
	Order By IdFarmacia, DescripcionComercial -- IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, ClaveSSA

--		spp_INT_MA__FACT_Remisiones_Validar 	


 
-------------------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------ 
---------------------------- Se devuelven los datos 
	Select 
		E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, 
		E.IdFarmaciaGenera, E.FarmaciaGenera, 
		E.IdFarmacia, E.Farmacia, 
		E.FolioRemision, 
		ValidarPolizaBeneficiario,  
		E.IdCliente, E.Cliente, 
		E.IdSubCliente, E.SubCliente, 
		E.IdPrograma, E.Programa, 
		E.IdSubPrograma, E.SubPrograma, 
		E.TipoDeRemision, E.TipoDeRemision_Descripcion, 
		max(SubTotalSinGrabar_Remision) as SubTotalSinGrabar_Remision, 
		max(SubTotalGrabado_Remision) as SubTotalGrabado_Remision, 
		max(Iva_Remision) as Iva_Remision, 
		max(Total_Remision) as Total_Remision  				
	From #tmpCodigos E (NoLock) 
	Group by 
		E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, 
		E.IdFarmaciaGenera, E.FarmaciaGenera, 
		E.IdFarmacia, E.Farmacia, 
		E.FolioRemision, 
		ValidarPolizaBeneficiario,  
		E.IdCliente, E.Cliente, 
		E.IdSubCliente, E.SubCliente, 
		E.IdPrograma, E.Programa, 
		E.IdSubPrograma, E.SubPrograma, 
		E.TipoDeRemision, E.TipoDeRemision_Descripcion 
	Order By IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma 

---	Select * From #tmpDetalles (NoLock) Order By IdFarmacia, IdPrograma, IdSubPrograma, ClaveSSA  
	Select * From #tmpDetalles (NoLock) Order By 'Id Farmacia', 'Descripción comercial'   	
	
------------------------------------------------------------------------------------ 	
 	
	
	
End
Go--#SQL

