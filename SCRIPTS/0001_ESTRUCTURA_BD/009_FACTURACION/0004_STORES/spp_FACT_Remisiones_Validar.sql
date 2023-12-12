
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_Remisiones_Validar' and xType = 'P' )
    Drop Proc spp_FACT_Remisiones_Validar
Go--#SQL
  
Create Proc spp_FACT_Remisiones_Validar 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '28', 
	@IdFarmaciaGenera varchar(4) = '1',  @FolioRemision varchar(10) = '673' 
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
	
	
	--------------------- FORMATEAR LOS PARAMETROS 
	Set @IdEmpresa = right('0000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('0000000000000000' + @IdEstado, 2)  
	Set @IdFarmaciaGenera = right('0000000000000000' + @IdFarmaciaGenera, 4)  
	Set @FolioRemision = right('0000000000000000' + @FolioRemision, 10)  

	
--	select * from FACT_Remisiones 
	---------------------------------------
	-- Se obtienen los datos principales -- 
	---------------------------------------
	Select	
			E.IdEmpresa, Cast( '' as varchar(100) ) as Empresa, E.IdEstado, Cast( '' as varchar(50) ) as Estado, 
			E.IdFarmaciaGenera, Cast( '' as varchar(100) ) as FarmaciaGenera, 
			D.IdFarmaciaDispensacion, D.FarmaciaDispensacion, 
			E.FolioRemision, 
			0 as ValidarPolizaBeneficiario,  
			E.IdFuenteFinanciamiento, E.IdFinanciamiento, Cast( '' as varchar(100) ) as Financiamiento, 
			Cast( '' as varchar(4) ) as IdCliente, Cast( '' as varchar(100) ) as Cliente, 
			Cast( '' as varchar(4) ) as IdSubCliente, Cast( '' as varchar(100) ) as SubCliente, 
			D.IdPrograma, Cast( '' as varchar(100) ) as Programa, 
			D.IdSubPrograma, Cast( '' as varchar(100) ) as SubPrograma, 
			
			-- E.TipoDeRemision, 
			-- ( Case When TipoDeRemision = 1 Then 'Insumo' Else 'Administración' End ) as TipoDeRemision_Descripcion,

			E.TipoDeRemision, 
			( 
				Upper( 
					case when E.TipoDeRemision = 1 then 'INSUMOS' 
						 when E.TipoDeRemision = 3 then 'INSUMOS INCREMENTO'
						 when E.TipoDeRemision = 2 then 'ADMINISTRACIÓN' 
						 when E.TipoDeRemision = 4 then 'VENTA DIRECTA' 
					else 
						'Sin especificar' end 
					) 
			) as TipoDeRemision_Descripcion, 


			E.FechaRemision, E.FechaValidacion, 
			
			(case when (E.EsRelacionFacturaPrevia = 1 or E.EsRelacionDocumento = 1 ) then 1 else 0 end) as EsRemisionDeComprobacion, 
			(case when (E.EsRelacionFacturaPrevia = 1 or E.EsRelacionDocumento = 1 ) then 1 else E.EsFacturable end) as EsFacturada, 
			
			E.EsExcedente, 
			E.IdPersonalRemision, Cast( '' as varchar(150) ) as PersonalRemision, 
			E.IdPersonalValida, Cast( '' as varchar(150) ) as PersonalValida, 
			cast(E.SubTotalSinGrabar as numeric(14,2)) as SubTotalSinGrabar_Remision, 
			cast(E.SubTotalGrabado as numeric(14,2)) as SubTotalGrabado_Remision, 
			cast(E.Iva as numeric(14,2)) as Iva_Remision, 
			cast(E.Total as numeric(14,2)) as Total_Remision, 
			E.Status, GetDate() as FechaImpresion, 
			D.IdFarmacia, Cast( '' as varchar(100) ) as Farmacia, 
			D.ClaveSSA, Cast( '' as varchar(7500) ) as ClaveSSA_Descripcion, 
			cast(IsNull(D.PrecioLicitado, 0) as numeric(14,2)) as PrecioLicitado, 
			cast(IsNull(D.Cantidad_Agrupada, 0) as numeric(14,4)) as Cantidad, 
			cast(IsNull(D.TasaIva, 0) as numeric(14,2)) as TasaIva, 
			cast(IsNull(D.SubTotalSinGrabar_Clave, 0) as numeric(14,2)) as SubTotalSinGrabar, 
			cast(IsNull(D.SubTotalGrabado_Clave, 0) as numeric(14,2)) as SubTotalGrabado, 
			cast(IsNull(D.Iva_Clave, 0) as numeric(14,2)) as Iva, 
			cast(IsNull(D.Importe_Clave, 0) as numeric(14,2)) as Importe 
	Into #tmpCodigos 
	From FACT_Remisiones E (NoLock) 
	Left Join vw_FACT_Remisiones_ClavesResumen D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmaciaGenera = D.IdFarmacia And E.FolioRemision = D.FolioRemision )
	Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmaciaGenera = @IdFarmaciaGenera And E.FolioRemision = @FolioRemision
	
	
	Update R Set ValidarPolizaBeneficiario = D.ValidarPolizaBeneficiario 
	From #tmpCodigos R 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles  D (NoLock) 
		On ( R.IdFuenteFinanciamiento = D.IdFuenteFinanciamiento and R.IdFinanciamiento = D.IdFinanciamiento ) 
	

	------------------------------------------------------------------------
	-- Se obtiene el IdCliente y SubCliente de la Clave de Financiamiento --
	------------------------------------------------------------------------
	Update C Set IdCliente = F.IdCliente, IdSubCliente = F.IdSubCliente
	From #tmpCodigos C(NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento F(NoLock) On( C.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento ) 


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

	-- Se obtienen la descripcion de la Clave SSA
	Update C Set ClaveSSA_Descripcion = F.Descripcion
	From #tmpCodigos C(NoLock)
	Inner Join CatClavesSSA_Sales F(NoLock) On( C.ClaveSSA = F.ClaveSSA ) 

	-- Se obtienen la descripcion del Personal Remision
	Update C Set PersonalRemision = ( F.Nombre + ' ' + IsNull(F.ApPaterno, '') + ' ' + IsNull(F.ApMaterno, '') )   
	From #tmpCodigos C(NoLock)
	Inner Join CatPersonal F(NoLock) On( C.IdEstado = F.IdEstado And C.IdFarmaciaGenera = F.IdFarmacia And C.IdPersonalRemision = F.IdPersonal ) 

	-- Se obtienen la descripcion del Personal Valida
	Update C Set PersonalValida = ( F.Nombre + ' ' + IsNull(F.ApPaterno, '') + ' ' + IsNull(F.ApMaterno, '') )   
	From #tmpCodigos C(NoLock)
	Inner Join CatPersonal F(NoLock) On( C.IdEstado = F.IdEstado And C.IdFarmaciaGenera = F.IdFarmacia And C.IdPersonalValida = F.IdPersonal ) 

	-- Se obtiene la descripcion del Financiamiento
	Update C Set Financiamiento = F.Descripcion
	From #tmpCodigos C(NoLock) 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles F(NoLock) On( C.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And C.IdFinanciamiento = F.IdFinanciamiento ) 


	---------------------------------------------------------------
	-- Se obtienen los detalles que se mostraran en el List View -- 
	---------------------------------------------------------------
	Select	'Id Farmacia' = IdFarmacia, Farmacia, IdPrograma, Programa, IdSubPrograma, SubPrograma, 
			'Clave SSA' = ClaveSSA, 
			'Descripción clave' = ClaveSSA_Descripcion, 
			'Precio licitado' = PrecioLicitado, 
			'Agrupacion' = Cantidad, 
			'% Iva' = TasaIva, 
			'Sub-Total sin grabar' = SubTotalSinGrabar, 
			'Sub-Total grabado' = SubTotalGrabado, 
			'Iva' = Iva, 
			'Importe' =Importe
	Into #tmpDetalles
	From #tmpCodigos 
	Where Status <> 'C' and Cantidad > 0 
	Order By IdFarmacia, IdFuenteFinanciamiento, IdFinanciamiento, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, ClaveSSA


	---------------------------- 
	-- Se devuelven los datos --
	---------------------------- 
	Select * 
	From #tmpCodigos (NoLock) 
	Order By IdFarmacia, IdFuenteFinanciamiento, IdFinanciamiento, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, ClaveSSA

---	Select * From #tmpDetalles (NoLock) Order By IdFarmacia, IdPrograma, IdSubPrograma, ClaveSSA  
	Select * From #tmpDetalles (NoLock) Order By 'Id Farmacia', IdPrograma, IdSubPrograma, 'Clave SSA'   	
	
	
End
Go--#SQL

