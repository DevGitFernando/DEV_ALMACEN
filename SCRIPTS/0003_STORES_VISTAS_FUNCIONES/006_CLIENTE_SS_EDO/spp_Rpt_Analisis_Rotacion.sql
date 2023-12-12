If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Analisis_Rotacion' and xType = 'P' ) 
   Drop Proc spp_Rpt_Analisis_Rotacion	
Go--#SQL 

---- Exec   spp_Rpt_Analisis_Rotacion  '21', '0188', '*', '2011-07-01', '2011-10-31', '1', '1'  

Create Proc spp_Rpt_Analisis_Rotacion		
( 
	@IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '*',
--	@ClaveSSA varchar(30) = '*',		
--	@FechaInicial varchar(10) = '2011-07-01', @FechaFinal varchar(10) = '2011-10-29', 
	@TipoDispensacion smallint = 0, 
	@TipoInsumo tinyint = 0 
) 	
With Encryption 
As 
Begin 
Set NoCount On  
Set Dateformat YMD 
Declare 
	@iEnero int, @iFebrero int, @iMarzo int, @iAbril int, @iMayo int, @iJunio int, 
	@iJulio int, @iAgosto int, @iSeptiembre int, @iOctubre int, @iNoviembre int, @iDiciembre int,
	@sWhereSubFarmacias varchar(200) 											

Declare 
	@iVenta int, @iConsignacion int,
	@EncPrincipal varchar(500), 
	@EncSecundario varchar(500), 
	@DescTipoDispensacion varchar(50)

--- Asignar valores iniciales 
	--If @ClaveSSA = '' 
	  -- Set @ClaveSSA = '*' 
	
	
	Set @iVenta = 0 
	Set @iConsignacion = 1
	Set @sWhereSubFarmacias = ''
	Set @DescTipoDispensacion = ''  
	
	if @TipoDispensacion = 1  --- Solo Venta 
	   Set @iConsignacion = 0     	
	
	if @TipoDispensacion = 2  --- Solo Consingacion 
	   Set @iVenta = 1     	


	Select @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Select @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
	
	Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario 
	from dbo.fg_Unidad_EncabezadoReportesClientesSSA()


---	Lista de Farmacias 
	Select IdEstado, IdFarmacia 
	into #tmp_Farmacias 
	From CatFarmacias (NoLock) 
	Where 1 = 0 
	
	Insert Into #tmp_Farmacias 
	Select @IdEstado, @IdFarmacia
	
	If @IdFarmacia = '*' 
	   Begin
			Delete From #tmp_Farmacias 
			Insert Into #tmp_Farmacias ( IdEstado, IdFarmacia  ) 
			Select IdEstado, IdFarmacia  
			From CatFarmacias (NoLock) 	
			Where IdEstado = @IdEstado  
	   End 

--		spp_Rpt_Analisis_Rotacion  
	Select L.IdEmpresa, L.IdEstado, L.IdFarmacia,
		   P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave,    
		   L.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmacia, 	 
		   L.IdProducto, L.CodigoEAN, L.ClaveLote, 
		   (case when L.ClaveLote like '%*%' then 1 else 0 end) as EsDeConsignacion,
		   L.Existencia, convert(varchar(10), L.FechaRegistro, 120) as FechaIngreso, 
		   0 as Folios, 
		   convert(varchar(10), getdate(), 120) as PrimerVenta,  		   
		   convert(varchar(10), getdate(), 120) as UltimaVenta,  
		   P.TasaIva   
	Into #tmpLotes 	   
	From FarmaciaProductos_CodigoEAN_Lotes L (NoLock)	 
	Inner Join #tmp_Farmacias F (NoLock) On  ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Productos_CodigoEAN P (NOLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) 		  
----	Group by  L.IdEmpresa, L.IdEstado, L.IdFarmacia,
----		   P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, P.TasaIva, 
----		   L.ClaveLote, L.IdSubFarmacia 			   
	


	Select L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.FolioVenta, 
		   P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave,    
		   L.IdSubFarmacia, 
		   L.IdProducto, L.CodigoEAN, L.ClaveLote, 
		   (case when L.ClaveLote like '%*%' then 1 else 0 end) as EsDeConsignacion,
		   L.CantidadVendida, 0 as Folios, convert(varchar(10), getdate(), 120) as FechaVenta,  
		   P.TasaIva   
	Into #tmpLotes_Ventas  	   
	From VentasDet_Lotes L (NoLock)	 
--	Inner Join #tmp_Farmacias F (NoLock) On  ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Productos_CodigoEAN P (NOLock) On ( L.IdProducto = P.IdProducto and L.CodigoEAN = P.CodigoEAN ) 	

	
	Update L Set FechaVenta = convert(varchar(10), V.FechaRegistro , 120)
	From #tmpLotes_Ventas L 
	Inner Join VentasEnc V (NoLock) 
		On ( L.IdEmpresa = V.IdEmpresa and L.IdEstado = V.IdEstado and L.IdFarmacia = V.IdFarmacia and L.FolioVenta = V.FolioVenta ) 


	Select L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote, 
			count(Distinct L.FolioVenta ) as Folios, min(FechaVenta) as PrimerVenta, max(FechaVenta) as UltimaVenta  
	Into #tmpResumen_Ventas 
	From #tmpLotes_Ventas L 
	Group by L.IdEmpresa, L.IdEstado, L.IdFarmacia, L.IdSubFarmacia, L.IdProducto, L.CodigoEAN, L.ClaveLote		   	
		   	

--	select * from #tmpResumen_Ventas 

	Update L Set Folios = V.Folios, PrimerVenta = V.PrimerVenta,  UltimaVenta = V.UltimaVenta 
--	Select L.*, V.* 
	From #tmpLotes L (NoLock) 
	Inner Join #tmpResumen_Ventas V (NoLock) 
		On ( L.IdEmpresa = V.IdEmpresa and L.IdEstado = V.IdEstado and L.IdFarmacia = V.IdFarmacia and L.IdSubFarmacia = V.IdSubFarmacia
			 and L.IdProducto = V.IdProducto and L.CodigoEAN = V.CodigoEAN and L.ClaveLote = V.ClaveLote ) 

		

--		spp_Rpt_Analisis_Rotacion 

------ Se Borran los tipos de dispensacion que se dieron por Receta Generada por vales
--	Delete From #tmpCaducadosClaves Where IdTipoDeDispensacion = '07'  --- Receta Generada por vales 
	
	
	If @TipoInsumo <> 0 
		   Begin 
			  If @TipoInsumo = 1 
				 Delete From #tmpCaducadosClaves Where TasaIva <> 0  --- Medicamentos 

			  If @TipoInsumo = 2
				 Delete From #tmpCaducadosClaves Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
		   End 
			   
--	select count(*) from #tmpCaducadosClaves 
	
--          spp_Rpt_Analisis_Rotacion 	
	
	
	
	
	
 
	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'Rpt_Resumen_Ventas' and xType = 'U' ) 
	   Drop Table Rpt_Resumen_Ventas  

	Select 
		 T.IdEmpresa, Ex.Nombre as Empresa, T.IdEstado, E.Nombre as Estado, T.IdFarmacia, F.NombreFarmacia as Farmacia,	
		 T.IdSubFarmacia, T.SubFarmacia, 	 
		 T.IdClaveSSA, T.ClaveSSA, T.DescripcionClave, 
		 T.IdProducto, T.CodigoEAN, T.ClaveLote, T.EsDeConsignacion, 
		 T.Existencia, T. FechaIngreso, T.Folios, T.PrimerVenta, T.UltimaVenta  
	Into Rpt_Resumen_Ventas 	   
	From #tmpLotes T	 
	Inner Join CatEmpresas Ex (NoLock) On ( T.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	Inner Join CatFarmacias F (NoLock) On ( T.IdEstado = F.IdEstado and T.IdFarmacia = F.IdFarmacia )  
	Order By T.IdFarmacia, T.ClaveSSA 
 

 
	-- Select * From #tmpCaducadosClaves_Claves_Cruze
	
	
--	spp_Rpt_Analisis_Rotacion 



End	
Go--#SQL 
	