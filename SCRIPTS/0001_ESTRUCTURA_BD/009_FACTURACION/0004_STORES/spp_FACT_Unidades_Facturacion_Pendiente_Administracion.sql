If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Unidades_Facturacion_Pendiente_Administracion' and xType = 'P' ) 
   Drop Proc spp_FACT_Unidades_Facturacion_Pendiente_Administracion  
Go--#SQL 
  
Create Proc spp_FACT_Unidades_Facturacion_Pendiente_Administracion 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', 	
	@IdFuenteFinanciamiento varchar(4) = '0001', @IdFinanciamiento varchar(4) = '0001', 
	@IdPrograma varchar(4) = '*', @IdSubPrograma varchar(4) = '*',  
	@Criterio_ProgramasAtencion varchar(max) = '', 
	@ClaveSSA varchar(20) = '*', 
	@TipoDeUnidades varchar(3) = '*', @IdJurisdiccion varchar(3) = '*', @IdFarmacia varchar(4) = '*', 
	@FechaInicial varchar(10) = '2013-06-01', @FechaFinal varchar(10) = '2013-07-15', 
	@TipoDispensacion int = 0   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(8000), 
	@sIdCliente varchar(4), @sIdSubCliente varchar(4) 


--------------------- Formatear parametros 
	Set @sSql = '' 
	Set @ClaveSSA = IsNull(@ClaveSSA, '') 
	Set @ClaveSSA = ltrim(rtrim(@ClaveSSA)) 
	
	Select @IdEmpresa = dbo.fg_FormatearCadena(@IdEmpresa, '0', 3) 
	Select @IdEstado = dbo.fg_FormatearCadena(@IdEstado, '0', 2) 
	Select @IdFuenteFinanciamiento = dbo.fg_FormatearCadena(@IdFuenteFinanciamiento, '0', 4) 
	Select @IdFinanciamiento = dbo.fg_FormatearCadena(@IdFinanciamiento, '0', 4) 
	Select @IdPrograma = dbo.fg_FormatearCadena(@IdPrograma, '0', 4) 
	Select @IdSubPrograma = dbo.fg_FormatearCadena(@IdSubPrograma, '0', 4) 		   
	Select @TipoDeUnidades = dbo.fg_FormatearCadena(@TipoDeUnidades, '0', 3) 
	Select @IdJurisdiccion = dbo.fg_FormatearCadena(@IdJurisdiccion, '0', 3) 
	Select @IdFarmacia = dbo.fg_FormatearCadena(@IdFarmacia, '0', 4) 
--------------------- Formatear parametros 


--------------------- Obtener Informacion de Cliente 
	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 


	Select @sIdCliente = IdCliente, @sIdSubCliente = IdSubCliente 
	From FACT_Fuentes_De_Financiamiento (NoLock) 
	Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento 
	
	Select 
		F.IdFuenteFinanciamiento as Id_FDF, @IdFinanciamiento as Id_F, 
		F.IdCliente, F.IdSubCliente, F.ClaveSSA, F.DescripcionClaveSSA as DescripcionClave, 
		P.IdProducto, P.CodigoEAN 		 
	Into #tmpClaves 	
	From vw_FACT_FuentesDeFinanciamiento_Admon_Claves  F (NoLock) 
	Inner Join #vw_Productos_CodigoEAN P On ( F.ClaveSSA = P.ClaveSSA ) 
	Where IdFuenteFinanciamiento = @IdFuenteFinanciamiento and IdFinanciamiento = @IdFinanciamiento 		

	-- Select @ClaveSSA as ClaveSSA 
	If @ClaveSSA <> '*' and @ClaveSSA <> '' 
	   Delete From #tmpClaves Where ClaveSSA <> @ClaveSSA 
	   	 	 
--	spp_FACT_Unidades_Facturacion_Pendiente_Insumos	
--	select top 1 * 	from vw_Productos_CodigoEAN 		  

--------------------- Obtener Informacion de Cliente  


--------------------- Obtener los programas y sub-programas 	
	Select 
		ROW_NUMBER() OVER (order by IdPrograma, IdSubPrograma)as Renglon, 
		IdPrograma, Programa, IdSubPrograma, SubPrograma, 
		(IdPrograma + IdSubPrograma) as IdAtencion, 1 as Procesar   
	Into #tmpProgramasDeAtencion 
	From vw_Clientes_Programas_Asignados_Unidad 
	Where IdEstado = @IdEstado and IdCliente = @sIdCliente and IdSubCliente = @sIdSubCliente 
	Group by IdPrograma, Programa, IdSubPrograma, SubPrograma  	
	
	If @Criterio_ProgramasAtencion <> '' 
	Begin 
	   Update #tmpProgramasDeAtencion Set Procesar = 0 
	   
	   Set @sSql = '
		   Update P Set Procesar = 1 	   	   
		   From #tmpProgramasDeAtencion P 
		   Where IdAtencion in ( ' + @Criterio_ProgramasAtencion + ' ) ' 
	   Exec(@sSql) 
	   Print @sSql 
	End 

	Delete From #tmpProgramasDeAtencion Where Procesar = 0  	
--------------------- Obtener los programas y sub-programas 


--------------------- Obtener Informacion de control 
	Select @IdEmpresa as IdEmpresa, F.IdEstado, F.Estado, F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia, F.IdTipoUnidad, F.TipoDeUnidad 
	Into #tmpFarmacias 
	From vw_Farmacias F (NoLock) 
	Where IdEstado = @IdEstado and Status = 'A'
	Order By IdEstado, IdJurisdiccion, IdFarmacia 
	
	If @TipoDeUnidades <> '*' 
	   Delete From #tmpFarmacias Where IdTipoUnidad <> @TipoDeUnidades 

	If @IdJurisdiccion <> '*' 
	   Delete From #tmpFarmacias Where IdJurisdiccion <> @IdJurisdiccion 

	If @IdFarmacia <> '*' 
	   Delete From #tmpFarmacias Where IdFarmacia <> @IdFarmacia 
--------------------- Obtener Informacion de control 


--------------------- Obtener Informacion de Cierres de Periodos  
	Select F.IdEmpresa, F.IdEstado, F.Estado, F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia, F.IdTipoUnidad, F.TipoDeUnidad, 
	     @sIdCliente as IdCliente, @sIdSubCliente as IdSubCliente, 
	     @IdPrograma as IdPrograma, @IdSubPrograma as IdSubPrograma, 
	     C.FechaRegistro, C.FolioCierre as Folio, C.FechaCorte, C.FechaInicial, C.FechaFinal  
	Into #tmpFarmacias_con_cierres 
	From #tmpFarmacias F (NoLock) 
	Inner Join Ctl_CierresDePeriodos C (NoLock) 
		On ( F.IdEmpresa = C.IdEmpresa and F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) 
	where convert(varchar(10), C.FechaCorte, 120) between @FechaInicial and @FechaFinal
	Order By F.IdEstado, F.IdJurisdiccion, F.IdFarmacia, C.FolioCierre   
--------------------- Obtener Informacion de Cierres de Periodos  

--------------------- Obtener Informacion de Folios de venta involucrados  
	Select 
		V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioCierre as Folio, 
		V.IdPrograma, V.IdSubPrograma, 0 as Procesar  
	Into #tmpFolios 
	From #tmpFarmacias_con_cierres F 
	Inner Join VentasEnc V (NoLock)  
		On ( F.IdEmpresa = V.IdEmpresa and F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia and F.Folio = V.FolioCierre 
			 and F.IdCliente = V.IdCliente and F.IdSubCliente = V.IdSubCliente )    
	Where Exists 
		( 
			Select * 
			From VentasDet_Lotes L (NoLock) 
			Inner Join #tmpClaves P (NoLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) 
			Where V.IdEmpresa = L.IdEmpresa and V.IdEstado = L.IdEstado and V.IdFarmacia = L.IdFarmacia and L.FolioVenta = V.FolioVenta 
			      -- and L.ClaveLote Not Like '%*%' 
			      and (case when L.ClaveLote Like '%*%' then 1 else 0 end) = @TipoDispensacion 
			      and L.EnRemision_Admon = 0 and L.RemisionFinalizada = 0
		) 
	Group By V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioCierre, V.IdPrograma, V.IdSubPrograma 
	Order By V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioCierre 
	
	----- Validar los programas seleccionados 
	Update F Set Procesar = 1 
	From #tmpFolios F 
	Inner Join #tmpProgramasDeAtencion P On ( F.IdPrograma = P.IdPrograma and F.IdSubPrograma = P.IdSubPrograma ) 
	
	Delete From #tmpFolios Where Procesar = 0 		
--------------------- Obtener Informacion de Folios de venta involucrados  


--		select top 1 * 	from Ctl_CierresDePeriodos 

------	spp_FACT_Unidades_Facturacion_Pendiente_Insumos 


--	Select * 	From #tmpFarmacias 
--	Select * 	From #tmpFarmacias_con_cierres 
--	Select *	From #tmpFolios 
--	Select *	From #tmpClaves 

-------------------------	Salida Final 
	Select	distinct 
		C.IdJurisdiccion, C.Jurisdiccion, C.IdFarmacia, C.Farmacia, Convert(varchar(10), C.FechaRegistro, 120) as FechaRegistro, 
		C.Folio, Convert( varchar(10), C.FechaCorte, 120 ) as FechaCorte, Convert( varchar(10), C.FechaInicial, 120) as FechaInicial, 
		Convert( varchar(10), C.FechaFinal, 120) as FechaFinal, 0 as Procesar   
	From #tmpFarmacias_con_cierres C 
	Inner Join #tmpFolios F (NoLock) 
		On ( C.IdEmpresa = F.IdEmpresa and C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia and C.Folio = F.Folio ) 
-------------------------	Salida Final 


End 
Go--#SQL    