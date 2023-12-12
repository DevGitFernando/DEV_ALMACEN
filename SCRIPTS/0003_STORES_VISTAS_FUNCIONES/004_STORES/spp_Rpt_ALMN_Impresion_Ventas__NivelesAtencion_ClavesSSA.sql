----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA' and Type = 'P' ) 
   Drop Proc spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA
Go--#SQL 

Create Proc spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '3', @Folio varchar(8) = '3620' 
) 
With Encryption 
As 
Begin 
Set NoCount On 

	Exec spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA_Proceso 
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @Folio = @Folio, @GenerarConcentrado = 0 

End 
Go--#SQL  

----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Rpt_ALMN_Impresion_Ventas__NivelesAtencion_ClavesSSA' and Type = 'P' ) 
   Drop Proc spp_Rpt_ALMN_Impresion_Ventas__NivelesAtencion_ClavesSSA
Go--#SQL 
   
Create Proc spp_Rpt_ALMN_Impresion_Ventas__NivelesAtencion_ClavesSSA 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '3', @Folio varchar(8) = '36', 
	@IdPerfilDeAtencion int = 1, @EsConsignacion smallint = 0, @MostrarPrecios int = 1      
) 
With Encryption 
As 
Begin 
Set NoCount On  

	Exec spp_Rpt_ALMN_Impresion_Ventas__NivelesAtencion_ClavesSSA_Proceso 
		@IdEmpresa = @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @Folio = @Folio, 
		@IdPerfilDeAtencion = @IdPerfilDeAtencion, @EsConsignacion = @EsConsignacion, @GenerarConcentrado = 0, @MostrarPrecios = @MostrarPrecios 

End 
Go--#SQL 


----------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA_Proceso' and Type = 'P' ) 
   Drop Proc spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA_Proceso
Go--#SQL 

---		Exec spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA_Proceso  1, 21, 1182, 6383, 4, 0    

Create Proc spp_Rpt_ALMN_Impresion_Ventas__Obtener_NivelesAtencion_ClavesSSA_Proceso 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '3', @Folio varchar(8) = '36', 
	@GenerarConcentrado int = 0   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFiltro_Atencion varchar(1000),   
	@sFiltro varchar(1000), 
	@sTipoReporte varchar(10), 
	@PerfilDeAtencion_Nombre varchar(200)    

Declare 
	@IdCliente varchar(4), 
	@IdSubCliente varchar(4), 
	@IdPrograma varchar(4), 
	@IdSubPrograma varchar(4) 		

---------------	Formatear valores 
	Set @IdEmpresa = right('0000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000000000000' + @IdFarmacia, 4) 
	Set @Folio = right('0000000000000' + @Folio, 8) 
---------------	Formatear valores 	

	
--- Obtener la informacion de base del Folio de Venta 	
	Select @IdCliente = IdCliente, @IdSubCliente = IdSubCliente, @IdPrograma = IdPrograma, @IdSubPrograma = IdSubPrograma
	From VentasEnc (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioVenta = @Folio 

	Select ClaveSSA, TieneVenta, TieneConsignacion 
	Into #tmpClaves  
	From 
	( 
		Select
			dbo.fg_ClaveSSABase_ClaveLicitada(E.IdEstado, E.IdCliente, E.IdSubCliente, D.IdProducto, D.ClaveLote) as ClaveSSA,
			--P.ClaveSSA, 
			(case when D.ClaveLote not like '%*%' then 1 else 0 end) as TieneVenta,  
			(case when D.ClaveLote like '%*%' then 1 else 0 end) as TieneConsignacion  		
		From VentasEnc E (NoLock)
		Inner Join VentasDet_Lotes D (NoLock)  On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioVenta = D.FolioVenta)
		Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
		Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and E.FolioVenta = @Folio 
		Group by dbo.fg_ClaveSSABase_ClaveLicitada(E.IdEstado, E.IdCliente, E.IdSubCliente, D.IdProducto, D.ClaveLote), D.ClaveLote   
	) as C 
	Group by ClaveSSA, TieneVenta, TieneConsignacion 
	
--	select * from #tmpClaves 	
--- Obtener la informacion de base del Folio de Venta 		
		

------------- Determinar los Perfiles de atencion involucrados en el Folio de Venta		
-- 	Select 0 as IdPerfilAtencion, 'Sin Especificar' as Titulo 
	Select Distinct IdPerfilAtencion, PerfilDeAtencion as Titulo, TieneVenta, TieneConsignacion  
	Into #tmpPerfiles 
	From vw_CFGC_ALMN_CB_NivelesAtencion_ClavesSSA P (NoLock) 
	Inner Join #tmpClaves C On ( P.ClaveSSA = C.ClaveSSA ) 
	Where P.IdEmpresa = @IdEmpresa And P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia
	   And P.IdCliente = @IdCliente and P.IdSubCliente = @IdSubCliente and P.IdPrograma = @IdPrograma and P.IdSubPrograma = @IdSubPrograma  
	   and P.StatusClaveSSA = 'A' 


	If Exists ( 		
		Select * 
		From #tmpClaves C (NoLock) 
		Where Not Exists 
		( 
			Select * From vw_CFGC_ALMN_CB_NivelesAtencion_ClavesSSA P 
			Where P.IdEmpresa = @IdEmpresa And P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia
			And P.IdCliente = @IdCliente and P.IdSubCliente = @IdSubCliente and P.IdPrograma = @IdPrograma and P.IdSubPrograma = @IdSubPrograma  
			and P.ClaveSSA = C.ClaveSSA 
		)  )
	   Begin 
			Insert Into #tmpPerfiles 
			Select 0, 'Sin Especificar', 1, 1  
	   End 
------------- Determinar los Perfiles de atencion involucrados en el Folio de Venta		
	
	
-------------------------------------- SALIDA FINAL 
	Select  
		IdPerfilAtencion, Titulo, 
		(case when TieneVenta > 0 then 1 else 0 end) as TieneVenta, 
		(case when TieneConsignacion > 0 then 1 else 0 end) as TieneConsignacion 
	Into #tmpPerfilesFinal 	
	From 
	( 	
		Select  IdPerfilAtencion, Titulo, sum(TieneVenta) as TieneVenta, sum(TieneConsignacion) as TieneConsignacion 
		From #tmpPerfiles 	
		Group by IdPerfilAtencion, Titulo 	
	) as P 
	
	
	If @GenerarConcentrado = 1 
	Begin 
		Insert Into #tmpListaPerfiles ( IdPerfilAtencion, Titulo, TieneVenta, TieneConsignacion ) 
		Select IdPerfilAtencion, Titulo, TieneVenta, TieneConsignacion 
		From #tmpPerfilesFinal 
	End 
	Else 
	Begin 	
		--------- FINAL 
		Select * From #tmpPerfilesFinal  	
	End 
-------------------------------------- SALIDA FINAL 	
	
End  
Go--#SQL 
	


----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Rpt_ALMN_Impresion_Ventas__NivelesAtencion_ClavesSSA_Proceso' and Type = 'P' ) 
   Drop Proc spp_Rpt_ALMN_Impresion_Ventas__NivelesAtencion_ClavesSSA_Proceso  
Go--#SQL 
   
Create Proc spp_Rpt_ALMN_Impresion_Ventas__NivelesAtencion_ClavesSSA_Proceso 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '3182', @Folio varchar(8) = '10007', 
	@IdPerfilDeAtencion int = 0, @EsConsignacion smallint = 0,	@GenerarConcentrado int = 0, 
	@MostrarPrecios int = 1      
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFiltro_Atencion varchar(1000),   
	@sFiltro varchar(1000), 
	@sTipoReporte varchar(10), 
	@PerfilDeAtencion_Nombre varchar(200), 
	@sFolioImpresion varchar(1000),  
	@Mostrar_Descripcion_Perfil bit, 
	@OrigenDeDatos varchar(500)      
	-- @MostrarPrecios int 

---------------	Formatear valores 
	Set @IdEmpresa = right('0000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000000000000' + @IdFarmacia, 4) 
	Set @Folio = right('0000000000000' + @Folio, 8) 	
	Set @sFiltro = '' 
	Set @sFiltro_Atencion = '' 
	Set @sTipoReporte = '01'	
	Set @Mostrar_Descripcion_Perfil = 0 
	Set @sFolioImpresion = '' 
	Set @OrigenDeDatos = '' 
	-- Set @MostrarPrecios = 0

	If @EsConsignacion = 1 
	   Set @sTipoReporte = '02'	 
			
---------------	Preparar tabla 	
	Select 
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
		IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, IdSegmento, Segmento,
		EsAntibiotico, EsControlado, SegmentoTipoMed 
	Into #tmpVenta 
	From vw_Impresion_Ventas_Credito_Lotes 
	Where 1 = 0 
	
	Alter Table #tmpVenta Alter Column Folio varchar(200) 
	
	If @GenerarConcentrado = 1 
		Begin 
			Set @OrigenDeDatos = '#tmp_Perfiles__vw_Impresion_Ventas_Credito_Lotes' 
		End 
	Else 
		Begin 
			Set @OrigenDeDatos = 'vw_Impresion_Ventas_Credito_Lotes' 
		End 	
	
	Select * 
	Into #tmpVentaFiltro 	
	From #tmpVenta 
	

	Set @sFiltro = 'Where IdEmpresa = ' + char(39) + @IdEmpresa + char(39) + ' and IdEstado = ' + char(39) + @IdEstado + char(39) 
	Set @sFiltro = @sFiltro + ' and IdFarmacia = ' + char(39) + @IdFarmacia + char(39) + ' and Folio = ' + char(39) + @Folio + char(39)
	Set @sFiltro = @sFiltro + ' and EsConsignacion = ' + Cast(@EsConsignacion as varchar)   
	Set @sFiltro_Atencion = ' C.IdEmpresa = V.IdEmpresa and C.IdEstado = V.IdEstado and C.IdFarmacia = V.IdFarmacia ' 
	
	
--	spp_Rpt_ALMN_Impresion_Ventas__NivelesAtencion_ClavesSSA	
	
----------------- Crear la tabla base 	
	Set @sSql = 'Insert Into #tmpVenta ' + 
	' Select ' + 
	' IdEmpresa, Empresa, EmpDomicilio, EmpColonia, EmpCodigoPostal, EmpEdoCiudad, ' + 
	' IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, EsAlmacen, Folio, Folio, IdSubFarmacia, SubFarmacia, FechaSistema, ' + 
	' FechaRegistro, IdCaja, IdPersonal, NombrePersonal, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, ' + 
	' IdSubPrograma, SubPrograma, IdBeneficiario, Beneficiario, DomicilioEntrega, FolioReferencia, NumReceta, FechaReceta, ' + 
	' IdMedico, Medico, EsCorte, StatusVenta, SubTotal, Descuento, Iva, Total, TipoDeVenta, NombreTipoDeVenta, ' + 
	' IdClaveSSA_Sal, ClaveSSA, ClaveSSA_Base, ClaveSSA_Aux, DescripcionSal, DescripcionClave, DescripcionCortaClave, Renglon, ' + 
	' IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, ClaveLote, ' + 
	' EsConsignacion, CantidadLote, CantidadCajasLote, FechaCad, UnidadDeSalida, TasaIva, Costo, Importe, PrecioLicitacion, ' + 
	' IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, IdSegmento, Segmento, EsAntibiotico, EsControlado, SegmentoTipoMed ' + 
	' From ' + @OrigenDeDatos + ' V (NoLock) ' + @sFiltro 
	Exec (@sSql) 
	-- Print @sSql 
	
	--	Select * From #tmpVenta 

----	select * from #tmpVenta 

-----------	Obtener la informacion del Perfil de Atencion 
	Select  @PerfilDeAtencion_Nombre = PerfilDeAtencion 
	From vw_CFGC_ALMN_CB_NivelesAtencion  	
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPerfilAtencion = @IdPerfilDeAtencion 

	Set @PerfilDeAtencion_Nombre = IsNull(@PerfilDeAtencion_Nombre, 'Sin especificar') 
	
	
	Select @Mostrar_Descripcion_Perfil = Mostrar_Descripcion_Perfil
	From CFGC_Titulos_Reportes_Detallado_Venta 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia  
	
	Set @Mostrar_Descripcion_Perfil = IsNull(@Mostrar_Descripcion_Perfil, 0) 	
-----------	Obtener la informacion del Perfil de Atencion 
	
	
	
---------	Dar formato al Folio 	
	Set @sFolioImpresion = '-' + cast(@IdPerfilDeAtencion as varchar) + '-' + @sTipoReporte 
	If @Mostrar_Descripcion_Perfil = 1  
	   Set @sFolioImpresion = '-' + cast(@IdPerfilDeAtencion as varchar) + '-' + @PerfilDeAtencion_Nombre + '-' + @sTipoReporte 
	
	Update V Set Folio = Folio + @sFolioImpresion 
	From #tmpVenta V 	
---------	Dar formato al Folio 	
	
	
	If @IdPerfilDeAtencion = 0
	   Begin 
			Set @sFiltro_Atencion = char(13) + 
			'Where Not Exists ' + char(13) + 
			'	(	' + 
			'		Select * From vw_CFGC_ALMN_CB_NivelesAtencion_ClavesSSA C (NoLock) ' + char(13) + 
			'		Where ' + @sFiltro_Atencion + ' And C.ClaveSSA = V.ClaveSSA ' + char(13) + 
			'			And C.IdCliente = V.IdCliente and C.IdSubCliente = V.IdSubCliente ' + 
			'			And C.IdPrograma = V.IdPrograma and C.IdSubPrograma = V.IdSubPrograma ' + 			
			'			And C.ClaveSSA = V.ClaveSSA and C.StatusClaveSSA = ' + char(39) + 'A' + char(39) + char(13) + 			
			'	) ' + char(13) 
			Set @sSql = 'Insert Into #tmpVentaFiltro Select * From #tmpVenta V (NoLock) ' + @sFiltro_Atencion  
	   End 
	Else 
	   Begin 
			Set @sFiltro_Atencion = char(13) + 
			'Where Exists ' + char(13) + 
			'	(	' + 
			'		Select * From vw_CFGC_ALMN_CB_NivelesAtencion_ClavesSSA C (NoLock) ' + char(13) + 
			'		Where ' + @sFiltro_Atencion + ' And IdPerfilAtencion = ' + cast(@IdPerfilDeAtencion as varchar) +  char(13) + 
			'			And C.IdCliente = V.IdCliente and C.IdSubCliente = V.IdSubCliente ' + 
			'			And C.IdPrograma = V.IdPrograma and C.IdSubPrograma = V.IdSubPrograma ' + 			
			'			And C.ClaveSSA = V.ClaveSSA and C.StatusClaveSSA = ' + char(39) + 'A' + char(39) + char(13) + 
			'	) ' + char(13) 
			Set @sSql = 'Insert Into #tmpVentaFiltro Select * From #tmpVenta V (NoLock) ' + @sFiltro_Atencion  
	   End 
	
--- Generar Impresion 
	Exec (@sSql) 
	--Print @sSql 
   

---------------------- Filtro de Precios 
	If @MostrarPrecios = 0  
	Begin  
		Update F Set SubTotal = 0, Descuento = 0, Iva = 0, Total = 0, Costo = 0, Importe = 0, PrecioLicitacion = 0
		From #tmpVentaFiltro F 
	End  
---------------------- Filtro de Precios 



---------------	Salida final 
	If @GenerarConcentrado = 1 
		Begin 
			----Insert Into #tmpConcentradoFolios ( IdEmpresa, Empresa, EmpDomicilio, EmpColonia, EmpCodigoPostal, EmpEdoCiudad, 
			----	IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, EsAlmacen, Folio, IdSubFarmacia, SubFarmacia, FechaSistema, 
			----	FechaRegistro, IdCaja, IdPersonal, NombrePersonal, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, 
			----	IdSubPrograma, SubPrograma, IdBeneficiario, Beneficiario, DomicilioEntrega, FolioReferencia, NumReceta, FechaReceta, 
			----	IdMedico, Medico, EsCorte, StatusVenta, SubTotal, Descuento, Iva, Total, TipoDeVenta, NombreTipoDeVenta, 
			----	IdClaveSSA_Sal, ClaveSSA, ClaveSSA_Base, ClaveSSA_Aux, DescripcionSal, DescripcionClave, DescripcionCortaClave, Renglon, 
			----	IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, ClaveLote, 
			----	EsConsignacion, CantidadLote, CantidadCajasLote, FechaCad, UnidadDeSalida, TasaIva, Costo, Importe, PrecioLicitacion, 
			----	IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, IdSegmento, Segmento  )
			----Select 
			----	IdEmpresa, Empresa, EmpDomicilio, EmpColonia, EmpCodigoPostal, EmpEdoCiudad, 
			----	IdEstado, Estado, ClaveRenapo, IdFarmacia, Farmacia, EsAlmacen, Folio, IdSubFarmacia, SubFarmacia, FechaSistema, 
			----	FechaRegistro, IdCaja, IdPersonal, NombrePersonal, IdCliente, NombreCliente, IdSubCliente, NombreSubCliente, IdPrograma, Programa, 
			----	IdSubPrograma, SubPrograma, IdBeneficiario, Beneficiario, DomicilioEntrega, FolioReferencia, NumReceta, FechaReceta, 
			----	IdMedico, Medico, EsCorte, StatusVenta, SubTotal, Descuento, Iva, Total, TipoDeVenta, NombreTipoDeVenta, 
			----	IdClaveSSA_Sal, ClaveSSA, ClaveSSA_Base, ClaveSSA_Aux, DescripcionSal, DescripcionClave, DescripcionCortaClave, Renglon, 
			----	IdProducto, CodigoEAN, DescProducto, DescripcionCorta, Presentacion, ContenidoPaquete, ClaveLote, 
			----	EsConsignacion, CantidadLote, CantidadCajasLote, FechaCad, UnidadDeSalida, TasaIva, Costo, Importe, PrecioLicitacion, 
			----	IdMunicipio, Municipio, IdColonia, Colonia, Domicilio, IdSegmento, Segmento, EsAntibiotico, EsControlado, SegmentoTipoMed 
			----From #tmpVentaFiltro 
			
			Insert Into #tmpConcentradoFolios ( FolioBase, Folio, EsConsignacion, FechaRegistro, ClaveSSA, CantidadLote, CantidadCajasLote, TasaIva, PrecioLicitacion  )
			Select 
				FolioBase, Folio, @EsConsignacion, FechaRegistro, ClaveSSA, CantidadLote, CantidadCajasLote, TasaIva, 
					(case when @EsConsignacion = 1 then 0 else PrecioLicitacion end)
			From #tmpVentaFiltro 			
		End 
	Else 
		Begin 
			Select * From #tmpVentaFiltro  
		End 
		
End 
Go--#SQL 
