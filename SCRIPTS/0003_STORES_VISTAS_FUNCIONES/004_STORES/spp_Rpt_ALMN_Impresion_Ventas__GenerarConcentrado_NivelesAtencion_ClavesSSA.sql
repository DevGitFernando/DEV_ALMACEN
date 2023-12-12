----------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Rpt_ALMN_Impresion_Ventas__GenerarConcentrado_NivelesAtencion_ClavesSSA' and Type = 'P' ) 
   Drop Proc spp_Rpt_ALMN_Impresion_Ventas__GenerarConcentrado_NivelesAtencion_ClavesSSA
Go--#SQL 

---		Exec spp_Rpt_ALMN_Impresion_Ventas__GenerarConcentrado_NivelesAtencion_ClavesSSA  1, 11, 3, '2014-05-01', '2014-05-01'      
   
Create Proc spp_Rpt_ALMN_Impresion_Ventas__GenerarConcentrado_NivelesAtencion_ClavesSSA 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '3', 
	@FechaInicial varchar(10) = '2014-05-01', @FechaFinal varchar(10) = '2014-05-01'   
)
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@IdCliente varchar(4), 
	@IdSubCliente varchar(4), 
	@IdPrograma varchar(4), 
	@IdSubPrograma varchar(4), 
	@FolioVenta varchar(10), 
	@GenerarConcentrado int, 
	@IdPerfilDeAtencion int, 
	@TieneVenta smallint,  	
	@TieneConsignacion smallint 

---------------	Formatear valores 
	Set @IdEmpresa = right('0000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000000000000' + @IdFarmacia, 4) 
	-- Set @Folio = right('0000000000000' + @Folio, 8) 
	Set @GenerarConcentrado = 1 
	Set @IdPerfilDeAtencion = 0 
	Set @TieneVenta = 0 
	Set @TieneConsignacion = 0 
---------------	Formatear valores 	

	Set @FolioVenta = '' 

	
--- Obtener la informacion de base del Folio de Venta 	
	if exists ( select * from tempdb..sysobjects (nolock) where name like '##tmpFolios%' and xType = 'u' ) 
	   Drop table ##tmpFolios 
	   
	Select identity(int, 1,1) as Keyx, FolioVenta 
	Into ##tmpFolios 
	From VentasEnc (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		And convert(varchar(10), FechaRegistro, 120) Between  @FechaInicial and @FechaFinal 
		-- and FolioVenta = 5817 
	
	
---------------------------------------------------- Estructura base 		
	Select Top 0 0 as IdPerfilAtencion, space(200) as Titulo, 0 as TieneVenta, 0 as TieneConsignacion 
	Into #tmpListaPerfiles 	
	
	----Select 
	----	IdEmpresa, Empresa, EmpDomicilio, EmpColonia, EmpCodigoPostal, EmpEdoCiudad, 
	----	IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, EsAlmacen, Folio, IdSubFarmacia, SubFarmacia, FechaSistema, 
	----	FechaRegistro, IdCaja, IdPersonal, NombrePersonal, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, 
	----	IdSubPrograma, SubPrograma, IdBeneficiario, Beneficiario, DomicilioEntrega, FolioReferencia, NumReceta, FechaReceta, 
	----	IdMedico, Medico, EsCorte, StatusVenta, SubTotal, Descuento, Iva, Total, TipoDeVenta, NombreTipoDeVenta, 
	----	IdClaveSSA_Sal, ClaveSSA, ClaveSSA_Base, ClaveSSA_Aux, DescripcionSal, DescripcionClave, DescripcionCortaClave, Renglon, 
	----	IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, ClaveLote, 
	----	EsConsignacion, CantidadLote, CantidadCajasLote, FechaCad, UnidadDeSalida, TasaIva, Costo, Importe, PrecioLicitacion, 
	----	IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, IdSegmento, Segmento 
	----Into #tmpConcentradoFolios 
	----From vw_Impresion_Ventas_Credito_Lotes 
	----Where 1 = 0 
	
	Select 
		Folio as FolioBase, Folio, 0 as EsConsignacion, getdate() as FechaRegistro, ClaveSSA, CantidadLote, CantidadCajasLote, TasaIva, PrecioLicitacion
	Into #tmpConcentradoFolios 
	From vw_Impresion_Ventas_Credito_Lotes 
	Where 1 = 0 	
	
	Alter Table #tmpConcentradoFolios Alter Column Folio varchar(200) 
	
	
	Select ---- identity(int, 1, 1) as Keyx, 
		IdEmpresa, Empresa, EmpDomicilio, EmpColonia, EmpCodigoPostal, EmpEdoCiudad, 
		IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, EsAlmacen, 
		Folio as FolioBase, Folio, 
		IdSubFarmacia, SubFarmacia, FechaSistema, 
		FechaRegistro, IdCaja, IdPersonal, NombrePersonal, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, 
		IdSubPrograma, SubPrograma, IdBeneficiario, Beneficiario, DomicilioEntrega, FolioReferencia, NumReceta, FechaReceta, 
		IdMedico, Medico, EsCorte, StatusVenta, SubTotal, Descuento, Iva, Total, TipoDeVenta, NombreTipoDeVenta, 
		IdClaveSSA_Sal, ClaveSSA, ClaveSSA_Base, ClaveSSA_Aux, DescripcionSal, DescripcionClave, DescripcionCortaClave, Renglon, 
		IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, ClaveLote, 
		EsConsignacion, CantidadLote, CantidadCajasLote, FechaCad, UnidadDeSalida, TasaIva, Costo, Importe, PrecioLicitacion, 
		IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, IdSegmento, Segmento 
	Into #tmp_Perfiles__vw_Impresion_Ventas_Credito_Lotes  
	From vw_Impresion_Ventas_Credito_Lotes 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		And convert(varchar(10), FechaRegistro, 120) Between  @FechaInicial and @FechaFinal   	
		-- And Folio = 5817 
	Order by Folio 	
	
	
	if exists ( select * from tempdb..sysobjects (nolock) where name like '##tmpResumenFolios%' and xType = 'u' ) 
	   Drop table ##tmpResumenFolios 
	
	Select identity(int, 1,1) as Keyx, FolioBase, FechaRegistro  
	Into ##tmpResumenFolios 
	From #tmp_Perfiles__vw_Impresion_Ventas_Credito_Lotes 
	Group by FolioBase, FechaRegistro  
	Order by FolioBase, FechaRegistro  	
	
---------------------------------------------------- Estructura base 		
	
	
	
	----- Recorrer la lista de folios 
	Declare #cursorFolios  
	Cursor For 
	Select FolioVenta 
	From ##tmpFolios T 
	Order by FolioVenta  
	Open #cursorFolios 
	FETCH NEXT FROM #cursorFolios Into @FolioVenta 
		WHILE @@FETCH_STATUS = 0 
		BEGIN 
			
			Delete From #tmpListaPerfiles  			
			Exec spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA_Proceso 
				 @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, @GenerarConcentrado   
			
			--- Select * From #tmpListaPerfiles 
			---	Exec spp_Rpt_ALMN_Impresion_Ventas__GenerarConcentrado_NivelesAtencion_ClavesSSA  			
						
			
			----- Recorrer la lista de perfiles  
			Declare #cursorPerfiles  
			Cursor For 
			Select IdPerfilAtencion, TieneVenta, TieneConsignacion 
			From #tmpListaPerfiles T 
			Order by IdPerfilAtencion   
			Open #cursorPerfiles 
			FETCH NEXT FROM #cursorPerfiles Into @IdPerfilDeAtencion, @TieneVenta, @TieneConsignacion 
				WHILE @@FETCH_STATUS = 0 
				BEGIN 					
				
					if @TieneVenta = 1 
					Begin 
						-- Print 'Tiene venta'
						Exec spp_Rpt_ALMN_Impresion_Ventas__NivelesAtencion_ClavesSSA_Proceso  
							 @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, 
							 @IdPerfilDeAtencion, 0, @GenerarConcentrado  			
					End 
				
					If @TieneConsignacion = 1 
					Begin 
						-- Print 'Tiene consigna'
						Exec spp_Rpt_ALMN_Impresion_Ventas__NivelesAtencion_ClavesSSA_Proceso  
							 @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, 
							 @IdPerfilDeAtencion, 1, @GenerarConcentrado  
					End 
				
					--Select @FolioVenta as FolioVenta, @IdPerfilDeAtencion, @TieneVenta, @TieneConsignacion  
				
					FETCH NEXT FROM #cursorPerfiles Into @IdPerfilDeAtencion, @TieneVenta, @TieneConsignacion 
				END	 
			Close #cursorPerfiles 
			Deallocate #cursorPerfiles 		
			----- Recorrer la lista de perfiles  	

			FETCH NEXT FROM #cursorFolios Into @FolioVenta   
		END	 
	Close #cursorFolios 
	Deallocate #cursorFolios 		
	
--- Resultado Final 	
	If Exists (  Select * From Sysobjects (NoLock) Where Name = 'PRCS_PerfilesAtencion_Folios' and xType = 'U' ) 
	   Drop Table PRCS_PerfilesAtencion_Folios	
	   
	Select * 
	Into PRCS_PerfilesAtencion_Folios 
	From #tmpConcentradoFolios 

/* 
	Select 
		count(distinct FolioBase) as FoliosBase, count(distinct Folio) as Folios, count(distinct ClaveSSA) as Claves,  
		sum(CantidadLote) as Piezas, sum(CantidadCajasLote) as Cajas, 		
		sum(CantidadLote * PrecioLicitacion) as SubTotal,  		
		sum((CantidadLote * PrecioLicitacion) * ( TasaIva/100 ) ) as Iva, 
		sum((CantidadLote * PrecioLicitacion) * ( 1 + ( TasaIva/100 ) ) ) as Total 
	From #tmpConcentradoFolios 
	--Group By Folio -- , ClaveSSA  
*/ 
	
	Select 
		FolioBase, Folio, EsConsignacion, FechaRegistro, 
		count(distinct ClaveSSA) as Claves,  
		sum(CantidadCajasLote) as Piezas, sum(CantidadCajasLote) as Cajas, 		
		sum(CantidadCajasLote * PrecioLicitacion) as SubTotal,  		
		sum((CantidadCajasLote * PrecioLicitacion) * ( TasaIva/100 ) ) as Iva, 
		sum((CantidadCajasLote * PrecioLicitacion) * ( 1 + ( TasaIva/100 ) ) ) as Total 
	From PRCS_PerfilesAtencion_Folios 
	Group By FolioBase, Folio, EsConsignacion, FechaRegistro   
	Order By FolioBase, FechaRegistro 
	
	
End 
Go--#SQL 

/*

	Select 
		FolioBase, Folio, EsConsignacion, 
		count(distinct ClaveSSA) as Claves,  
		sum(CantidadCajasLote) as Piezas, sum(CantidadCajasLote) as Cajas, 		
		sum(CantidadCajasLote * PrecioLicitacion) as SubTotal,  		
		sum((CantidadCajasLote * PrecioLicitacion) * ( TasaIva/100 ) ) as Iva, 
		sum((CantidadCajasLote * PrecioLicitacion) * ( 1 + ( TasaIva/100 ) ) ) as Total 
	From PRCS_PerfilesAtencion_Folios 
	Group By FolioBase, Folio, EsConsignacion  

*/ 
	