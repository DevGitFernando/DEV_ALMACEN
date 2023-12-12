------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Ventas' and xType = 'V' ) 
	Drop View vw_Impresion_Ventas  
Go--#SQL  	

Create View vw_Impresion_Ventas 
With Encryption 
As 	
	Select 
		M.IdEmpresa, M.Empresa, 
		M.EmpDomicilio, M.EmpColonia, M.EmpCodigoPostal, M.EmpEdoCiudad, 		
		M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.CLUES, M.NombrePropio_UMedica, M.EsAlmacen, M.EsUnidosis, 
		M.Folio, -- M.FolioMovtoInv, 
		M.FechaSistema, M.FechaRegistro, M.IdCaja, 
		M.IdPersonal, M.NombrePersonal,  
		M.IdCliente, M.NombreCliente, -- IsNull(C.NombreCliente, '') as NombreCliente, 
		M.IdSubCliente, M.NombreSubCliente, -- IsNull(C.NombreSubCliente, '') as NombreSubCliente,
		M.IdPrograma, M.Programa, --IsNull(Pr.Programa, '') as Programa, 
		M.IdSubPrograma, M.SubPrograma, --IsNull(Pr.SubPrograma, '') as SubPrograma, 
		-- M.IdPaciente, M.FolioDerechoHabiencia, M.FolioReceta, 
		M.SubTotal, M.Descuento, M.Iva, M.Total, 
		M.TipoDeVenta, M.NombreTipoDeVenta, -- (case when M.TipoDeVenta = 1 Then 'Venta Publico General' Else 'Venta Credito' End) as NombreTipoDeVenta, 
		M.Status as StatusVenta, 
		P.IdClaveSSA_Sal, P.ClaveSSA_Base, 
		
		-- P.ClaveSSA, 
		dbo.fg_ClaveSSABase_ClaveLicitada(M.IdEstado, M.IdCliente, M.IdSubCliente, D.IdProducto, '') as ClaveSSA, 
		P.ClaveSSA_Aux, 
		
		--P.DescripcionSal,  
		P.DescripcionSal as DescripcionSal_Base, 
		dbo.fg_DescripcionClave_ClaveSSARelacionada(M.IdEstado, M.IdCliente, M.IdSubCliente, D.IdProducto, '') as DescripcionSal, 		
		
		P.DescripcionClave, 
		P.DescripcionClave as DescripcionClave_Base, 
		--dbo.fg_DescripcionClave_ClaveSSARelacionada(M.IdEstado, M.IdCliente, M.IdSubCliente, D.IdProducto) as DescripcionClave, 
		
		--P.DescripcionCortaClave, 
		P.DescripcionCortaClave as DescripcionCortaClave_Base, 
		dbo.fg_DescripcionClave_ClaveSSARelacionada(M.IdEstado, M.IdCliente, M.IdSubCliente, D.IdProducto, '') as DescripcionCortaClave, 


		D.Renglon, D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, P.DescripcionCorta, P.Presentacion, 
		
		-- cast(P.ContenidoPaquete_ClaveSSA as int) as ContenidoPaquete, 
		cast(dbo.fg_ContenidoPaquete_ClaveLicitada(M.IdEstado, M.IdCliente, M.IdSubCliente, D.IdProducto, 'SinLote') as int) as ContenidoPaquete, 		
		D.UnidadDeSalida, D.TasaIva,
		(( D.Cant_Entregada - D.Cant_Devuelta ) / dbo.fg_Multiplo_ClaveSSARelacionada(M.IdEstado, M.IdCliente, M.IdSubCliente, D.IdProducto, '')) as Cantidad, 
		(
			(
				( D.Cant_Entregada - D.Cant_Devuelta ) / dbo.fg_Multiplo_ClaveSSARelacionada(M.IdEstado, M.IdCliente, M.IdSubCliente, D.IdProducto, '')		
			)
			/
			cast(dbo.fg_ContenidoPaquete_ClaveLicitada(M.IdEstado, M.IdCliente, M.IdSubCliente, D.IdProducto, 'SinLote') as numeric(14,4))
		) as CantidadCajas, 
		M.EsCorte, D.CostoUnitario as Costo, D.PrecioUnitario as Importe,  --, D.Keyx as KeyxDetalle  
		D.PrecioLicitacion, 
	    M.IdMunicipio, M.Municipio, M.IdColonia, M.Colonia, M.Domicilio,
	    P.IdSegmento, P.Segmento,
	    P.EsAntibiotico, P.EsControlado,
		   Case When ( P.EsAntibiotico = 0 and P.EsControlado = 1 ) Then 1
				When ( P.EsAntibiotico = 1 and P.EsControlado = 0 ) Then 2 
				When ( P.EsAntibiotico = 0 and P.EsControlado = 0 ) Then 3 end 
		   as SegmentoTipoMed	
	From vw_VentasEnc M (NoLock) 
	Inner Join VentasDet D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioVenta ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 


	-- Left Join vw_Clientes_SubClientes C (NoLock) On ( M.IdCliente = C.IdCliente and M.IdSubCliente = C.IdSubCliente )
	-- Left Join vw_Programas_SubProgramas Pr (NoLock) On ( M.IdPrograma = Pr.IdPrograma and M.IdSubPrograma = Pr.IdSubPrograma ) 	

Go--#SQL
 

------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Ventas_Lotes' and xType = 'V' ) 
	Drop View vw_Impresion_Ventas_Lotes   
Go--#SQL 

Create View vw_Impresion_Ventas_Lotes 
With Encryption 
As 
	Select 
		v.IdEmpresa, v.Empresa, 
		v.EmpDomicilio, v.EmpColonia, v.EmpCodigoPostal, v.EmpEdoCiudad, 				
		v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.Farmacia, v.CLUES, v.NombrePropio_UMedica, v.EsAlmacen, v.EsUnidosis, 
		L.IdSubFarmacia, L.SubFarmacia, 
		v.Folio, v.FechaSistema, v.FechaRegistro, 
		v.IdCaja, v.IdPersonal, v.NombrePersonal, v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, 
		v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		-- v.IdPaciente, v.FolioDerechoHabiencia, v.FolioReceta, 
		v.EsCorte, v.StatusVenta, v.SubTotal, v.Descuento, v.Iva, v.Total, v.TipoDeVenta, v.NombreTipoDeVenta, 
		v.IdClaveSSA_Sal, v.ClaveSSA_Base,
		--v.ClaveSSA, 
		dbo.fg_ClaveSSABase_ClaveLicitada(L.IdEstado, V.IdCliente, V.IdSubCliente, L.IdProducto, L.ClaveLote) as ClaveSSA, 
		v.ClaveSSA_Aux, v.DescripcionSal,
		--v.DescripcionClave, 
		dbo.fg_DescripcionClave_ClaveSSARelacionada(L.IdEstado, V.IdCliente, V.IdSubCliente, L.IdProducto, L.ClaveLote) as DescripcionClave,
		--v.DescripcionCortaClave, 
		dbo.fg_DescripcionClave_ClaveSSARelacionada(L.IdEstado, V.IdCliente, V.IdSubCliente, L.IdProducto, L.ClaveLote) as DescripcionCortaClave,
		v.Renglon, v.IdProducto, v.CodigoEAN, v.DescProducto, v.DescripcionCorta, v.Presentacion, v.ContenidoPaquete, 
		v.UnidadDeSalida, v.TasaIva, 
		v.Cantidad, L.ClaveLote, cast(L.EsConsignacion as int) as EsConsignacion, 
		--L.Cantidad as CantidadLote, L.CantidadCajas as CantidadCajasLote,
		CEILING(Cast(L.Cantidad As Numeric(14,4)) / dbo.fg_Multiplo_ClaveSSARelacionada(L.IdEstado, v.IdCliente, v.IdSubCliente, L.IdProducto, L.ClaveLote)) as CantidadLote,
		(
			cast(
				CEILING(Cast(L.Cantidad As Numeric(14,4)) / dbo.fg_Multiplo_ClaveSSARelacionada(L.IdEstado, v.IdCliente, v.IdSubCliente, L.IdProducto, L.ClaveLote)) 
				/ 
				dbo.fg_ContenidoPaquete_ClaveLicitada(L.IdEstado, v.IdCliente, v.IdSubCliente, L.IdProducto, L.ClaveLote) 
			as numeric(14,4)) 
		) as CantidadCajasLote, 
		v.Costo, v.Importe, 
		v.PrecioLicitacion,  
		L.FechaCad,
		v.IdMunicipio, v.Municipio, v.IdColonia, v.Colonia, v.Domicilio,
		v.IdSegmento, v.Segmento,
		v.EsAntibiotico, v.EsControlado, v.SegmentoTipoMed	 
	From vw_Impresion_Ventas v (NoLock) 
	Inner Join vw_VentasDet_CodigosEAN_Lotes L (NoLock) 
		On ( V.IdEmpresa = L.IdEmpresa and V.IdEstado = L.IdEstado and V.IdFarmacia = L.IdFarmacia and V.Folio = L.Folio 
		     and V.IdProducto = L.IdProducto and V.CodigoEAN = L.CodigoEAN ) 

Go--#SQL 

 	
------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Ventas_Contado' and xType = 'V' ) 
	Drop View vw_Impresion_Ventas_Contado   
Go--#SQL
 
Create View vw_Impresion_Ventas_Contado 
With Encryption 
As 
	Select 
		v.IdEmpresa, v.Empresa, 
		v.EmpDomicilio, v.EmpColonia, v.EmpCodigoPostal, v.EmpEdoCiudad, 
		v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.Farmacia, v.CLUES, v.NombrePropio_UMedica, v.EsAlmacen, v.EsUnidosis, v.Folio, v.FechaSistema, v.FechaRegistro, 
		v.IdCaja, v.IdPersonal, v.NombrePersonal, v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, 
		v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		-- v.IdPaciente, v.FolioDerechoHabiencia, v.FolioReceta, 
		v.EsCorte, v.StatusVenta, v.SubTotal, v.Descuento, v.Iva, v.Total, v.TipoDeVenta, v.NombreTipoDeVenta, 
		v.IdClaveSSA_Sal, v.ClaveSSA_Base, v.ClaveSSA, v.ClaveSSA_Aux, v.DescripcionSal, v.DescripcionClave, v.DescripcionCortaClave, 
		v.Renglon, v.IdProducto, v.CodigoEAN, v.DescProducto, v.DescripcionCorta, v.Presentacion, 
		v.UnidadDeSalida, v.TasaIva, v.Cantidad, v.Costo, v.Importe, 
		v.IdMunicipio, v.Municipio, v.IdColonia, v.Colonia, v.Domicilio	 
	From vw_Impresion_Ventas v (NoLock) 
	Where TipoDeVenta = 1 

Go--#SQL
 	


------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Ventas_Credito' and xType = 'V' ) 
	Drop View vw_Impresion_Ventas_Credito 
Go--#SQL  

Create View vw_Impresion_Ventas_Credito 
With Encryption 
As 

	Select 
		v.IdEmpresa, v.Empresa, 
		v.EmpDomicilio, v.EmpColonia, v.EmpCodigoPostal, v.EmpEdoCiudad, 		
		v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.Farmacia, v.CLUES, v.NombrePropio_UMedica, v.EsAlmacen, v.EsUnidosis, v.Folio, v.FechaSistema, v.FechaRegistro, 
		v.IdCaja, v.IdPersonal, v.NombrePersonal, v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, 
		v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		-- v.IdPaciente, v.FolioDerechoHabiencia, v.FolioReceta, 
		B.IdBeneficiario, B.NombreCompleto as Beneficiario, B.CURP, 
		B.FechaNacimiento, 
		B.Domicilio as DomicilioEntrega, 
		B.FolioReferencia, 
		
		I.IdTipoDerechoHabiencia, I.DerechoHabiencia, 
		I.IdEstadoResidencia, I.EstadoDeResidencia, I.ClaveRENAPO__EstadoDeResidencia, 
		
		I.NumReceta, I.FechaReceta, 
		I.IdMedico, I.Medico, I.Cedula,
		I.IdServicio, I.Servicio, I.IdArea, I.Area_Servicio, 
		I.NumeroDeCama, I.NumeroDeHabitacion,
		v.EsCorte, v.StatusVenta, v.SubTotal, v.Descuento, v.Iva, v.Total, v.TipoDeVenta, v.NombreTipoDeVenta, 
		v.IdClaveSSA_Sal, v.ClaveSSA_Base, v.ClaveSSA, v.ClaveSSA_Aux, v.DescripcionSal, v.DescripcionClave, v.DescripcionCortaClave, 
		v.Renglon, v.IdProducto, v.CodigoEAN, v.DescProducto, v.DescripcionCorta, v.Presentacion, v.ContenidoPaquete, 
		v.UnidadDeSalida, v.TasaIva, v.Cantidad, 
		-- v.CantidadCajas, 
		(dbo.fg_CalcularFactorLicitacion( V.IdEstado, V.IdCliente, V.IdSubCliente, V.IdProducto)  * v.CantidadCajas ) as CantidadCajas, 		
		v.Costo, v.Importe, v.PrecioLicitacion, 
		v.IdMunicipio, v.Municipio, v.IdColonia, v.Colonia, v.Domicilio 
		-- , I.Sexo, I.SexoAux, I.IdMedico, I.Medico, I.IdDiagnostico, I.Diagnostico, 		I.IdServicio, I.Servicio, I.IdArea, I.Area_Servicio as Area  
	From vw_Impresion_Ventas v (NoLock) 
	Inner Join vw_VentasDispensacion_InformacionAdicional I (NoLock) 
		On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.Folio ) 
	Left Join vw_Beneficiarios B (NoLock) 
		On ( I.IdEstado = B.IdEstado and I.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente 
			 and I.IdBeneficiario = B.IdBeneficiario ) 	
	Where TipoDeVenta = 2 

Go--#SQL



------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Ventas_Credito_Lotes' and xType = 'V' ) 
	Drop View vw_Impresion_Ventas_Credito_Lotes 
Go--#SQL  

Create View vw_Impresion_Ventas_Credito_Lotes 
With Encryption 
As 

	Select 
		v.IdEmpresa, v.Empresa, 
		v.EmpDomicilio, v.EmpColonia, v.EmpCodigoPostal, v.EmpEdoCiudad, 		
		v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.Farmacia, v.CLUES, v.NombrePropio_UMedica, v.EsAlmacen, v.EsUnidosis, 
		v.Folio, 
		v.IdSubFarmacia, v.SubFarmacia, 
		v.FechaSistema, v.FechaRegistro, 
		v.IdCaja, v.IdPersonal, v.NombrePersonal, v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, 
		v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		-- v.IdPaciente, v.FolioDerechoHabiencia, v.FolioReceta, 
		B.IdBeneficiario, B.NombreCompleto as Beneficiario, B.CURP, B.FechaNacimiento, B.Domicilio as DomicilioEntrega, 
		B.FolioReferencia, 
		
		I.IdTipoDerechoHabiencia, I.DerechoHabiencia, 
		I.IdEstadoResidencia, I.EstadoDeResidencia, I.ClaveRENAPO__EstadoDeResidencia, 
		
		I.NumReceta, I.FechaReceta, 
		I.IdMedico, I.Medico, I.Cedula,
		I.IdServicio, I.Servicio, I.IdArea, I.Area_Servicio, 
		I.NumeroDeCama, I.NumeroDeHabitacion, 
		v.EsCorte, v.StatusVenta, v.SubTotal, v.Descuento, v.Iva, v.Total, v.TipoDeVenta, v.NombreTipoDeVenta, 
		v.IdClaveSSA_Sal, v.ClaveSSA_Base, v.ClaveSSA, v.ClaveSSA_Aux, v.DescripcionSal, v.DescripcionClave, v.DescripcionCortaClave, 
		v.Renglon, v.IdProducto, v.CodigoEAN, v.DescProducto, v.DescripcionCorta, v.Presentacion, v.ContenidoPaquete, 
		v.ClaveLote, 
		--( Case When ClaveLote Like '%*%' Then 1 Else 0 End ) as EsConsignacion, 
		(case when v.EsConsignacion = 1 then 1 else 0 end) as EsConsignacion, 


		v.CantidadLote, v.CantidadLote as Cantidad, 
		-- (dbo.fg_CalcularFactorLicitacion( V.IdEstado, V.IdCliente, V.IdSubCliente, V.IdProducto)  * v.CantidadLote) as CantidadLote, 						
		-- v.CantidadCajasLote as CantidadCajasLote, 		
		(dbo.fg_CalcularFactorLicitacion( V.IdEstado, V.IdCliente, V.IdSubCliente, V.IdProducto)  * v.CantidadCajasLote ) as CantidadCajasLote, 
		--v.CantidadCajasLote, 
		
		v.FechaCad, 		
		v.UnidadDeSalida, v.TasaIva, --v.Cantidad, 
		v.Costo, v.Importe, v.PrecioLicitacion, 
		v.IdMunicipio, v.Municipio, v.IdColonia, v.Colonia, v.Domicilio,
		v.IdSegmento, v.Segmento,
		v.EsAntibiotico, v.EsControlado, v.SegmentoTipoMed, I.RefObservaciones
		-- , I.Sexo, I.SexoAux, I.IdMedico, I.Medico, I.IdDiagnostico, I.Diagnostico, 		I.IdServicio, I.Servicio, I.IdArea, I.Area_Servicio as Area  
	From vw_Impresion_Ventas_Lotes v (NoLock) 
	Inner Join vw_VentasDispensacion_InformacionAdicional I (NoLock) 
		On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.Folio ) 
	Left Join vw_Beneficiarios B (NoLock) 
		On ( I.IdEstado = B.IdEstado and I.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente 
			 and I.IdBeneficiario = B.IdBeneficiario ) 	
	Where TipoDeVenta = 2 

Go--#SQL



-------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Ventas_Contado_Lotes' and xType = 'V' ) 
	Drop View vw_Impresion_Ventas_Contado_Lotes   
Go--#SQL	 

Create View vw_Impresion_Ventas_Contado_Lotes 
With Encryption 
As 
	Select
		v.IdEmpresa, v.Empresa, 
		v.EmpDomicilio, v.EmpColonia, v.EmpCodigoPostal, v.EmpEdoCiudad, 		
		v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.Farmacia, v.CLUES, v.NombrePropio_UMedica, v.EsAlmacen, v.EsUnidosis, 
		v.Folio, 
		v.IdSubFarmacia, v.SubFarmacia, 
		v.FechaSistema, v.FechaRegistro, 
		v.IdCaja, v.IdPersonal, v.NombrePersonal, v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, 
		v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		-- v.IdPaciente, v.FolioDerechoHabiencia, v.FolioReceta, 
		v.EsCorte, v.StatusVenta, v.SubTotal, v.Descuento, v.Iva, v.Total, v.TipoDeVenta, v.NombreTipoDeVenta, 
		v.IdClaveSSA_Sal, v.ClaveSSA_Base, v.ClaveSSA, v.ClaveSSA_Aux, v.DescripcionSal, v.DescripcionClave, v.DescripcionCortaClave, 
		v.Renglon, v.IdProducto, v.CodigoEAN, v.DescProducto, v.DescripcionCorta, v.Presentacion, 
		v.ClaveLote, (case when v.EsConsignacion = 1 then 1 else 0 end) as EsConsignacion, 
		v.CantidadLote as CantidadLote, v.CantidadCajasLote as CantidadCajasLote, v.FechaCad,
		v.UnidadDeSalida, v.TasaIva, --v.Cantidad, 
		v.Costo, v.Importe, v.PrecioLicitacion, 
		v.IdMunicipio, v.Municipio, v.IdColonia, v.Colonia, v.Domicilio	 
	From vw_Impresion_Ventas_Lotes v (NoLock) 
	Where TipoDeVenta = 1 
	
Go--#SQL

 
 
------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Ventas_ClavesSolicitadas' and xType = 'V' ) 
	Drop View vw_Impresion_Ventas_ClavesSolicitadas  
Go--#SQL  

Create View vw_Impresion_Ventas_ClavesSolicitadas 
With Encryption 
As 

	Select V.IdEmpresa, E.Nombre as Empresa, V.IdEstado, F.Estado, V.IdFarmacia, F.Farmacia, M.CLUES, M.NombrePropio_UMedica, F.EsAlmacen, F.EsUnidosis, 
		V.FolioVenta as Folio, M.FechaRegistro,  
		V.EsCapturada, 
		dbo.fg_EsClave_CuadroBasico( V.IdEstado, V.IdFarmacia, C.ClaveSSA ) as Clave_CB, 
		C.ClaveSSA, V.IdClaveSSA, C.DescripcionClave, C.DescripcionSal, C.DescripcionCortaClave, 
		C.IdPresentacion, C.Presentacion, 
		V.CantidadRequerida, V.Observaciones 
	From VentasEstadisticaClavesDispensadas V (NoLock) 
	Inner Join vw_VentasEnc M (NoLock) 
		On ( M.IdEmpresa = V.IdEmpresa and M.IdEstado = V.IdEstado and M.IdFarmacia = V.IdFarmacia and M.Folio = V.FolioVenta )	
	Inner Join vw_Farmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
	Inner Join CatEmpresas E (NoLock) On ( V.IdEmpresa = E.IdEmpresa ) 
	Inner Join vw_ClavesSSA_Sales C (NoLock) On( V.IdClaveSSA = C.IdClaveSSA_Sal ) 
	--Where -- EsCapturada = 1 and 
	--FolioVenta = 42581 

Go--#SQL 
