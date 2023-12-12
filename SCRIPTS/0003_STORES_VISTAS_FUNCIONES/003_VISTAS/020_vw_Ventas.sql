--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_VentasEnc' and xType = 'V' ) 
	Drop View vw_VentasEnc 
Go--#SQL

Create View vw_VentasEnc 
With Encryption 
As 
	Select 
		M.IdEmpresa, Ex.Empresa,  
		Ex.Domicilio as EmpDomicilio, Ex.Colonia as EmpColonia, Ex.CodigoPostal as EmpCodigoPostal, Ex.EdoCiudad as EmpEdoCiudad, 
		M.IdEstado, F.Estado as Estado, F.ClaveRenapo, M.IdFarmacia, F.Farmacia as Farmacia, F.CLUES, F.NombrePropio_UMedica, F.EsAlmacen, F.EsUnidosis, 
		M.FolioVenta as Folio, M.FolioCierre,     

		M.FolioMovtoInv, 
		----		 ( Select top 1 K.FolioMovtoInv From MovtosInv_Enc K (NoLock) 
		----			Where K.IdEmpresa = M.IdEmpresa and K.IdEstado = M.IdEstado and K.IdFarmacia = M.IdFarmacia 
		----			      and K.IdTipoMovto_Inv = 'SV' and right(K.FolioMovtoInv, len(K.FolioMovtoInv) - len(K.IdTipoMovto_Inv) ) = M.FolioVenta  
		----		 ) as FolioMovtoInv, 		 

		M.FechaSistema, M.FechaRegistro, M.IdCaja, M.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		--		 M.IdCliente, M.IdSubCliente, M.IdPrograma, M.IdSubPrograma, 
		M.IdCliente, IsNull(C.NombreCliente, '') as NombreCliente, 
		M.IdSubCliente, IsNull(C.NombreSubCliente, '') as NombreSubCliente, 
		M.IdPrograma, IsNull(Pr.Programa, '') as Programa, 
		M.IdSubPrograma, IsNull(Pr.SubPrograma, '') as SubPrograma, 		 
		M.Corte as EsCorte, M.SubTotal, M.Descuento, M.Iva, M.Total, 
		M.TipoDeVenta, (case when M.TipoDeVenta = 1 Then 'Venta Publico General' Else 'Venta Credito' End) as NombreTipoDeVenta, 
		M.Status, F.IdMunicipio, F.Municipio, F.IdColonia, F.Colonia, F.Domicilio    
	From VentasEnc M (NoLock) 
	Inner Join vw_Empresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 		
	-- Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )  	
	Left Join vw_Clientes_SubClientes C (NoLock) On ( M.IdCliente = C.IdCliente and M.IdSubCliente = C.IdSubCliente )
	Left Join vw_Programas_SubProgramas Pr (NoLock) On ( M.IdPrograma = Pr.IdPrograma and M.IdSubPrograma = Pr.IdSubPrograma ) 		
	
Go--#SQL


--	select * from vw_Empresas  
--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_VentasDet_CodigosEAN' and xType = 'V' ) 
	Drop View vw_VentasDet_CodigosEAN 
Go--#SQL	
 	
Create View vw_VentasDet_CodigosEAN 
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.CLUES, M.NombrePropio_UMedica, M.EsAlmacen, M.Folio, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, 
		P.ClaveSSA_Base, P.ClaveSSA, P.ClaveSSA_Aux, 
		D.UnidadDeSalida, D.TasaIva, 
		----cast(P.ContenidoPaquete as int) as ContenidoPaquete, 
		-- cast(P.ContenidoPaquete_ClaveSSA as int) as ContenidoPaquete, 		
		cast(dbo.fg_ContenidoPaquete_ClaveLicitada(M.IdEstado, M.IdCliente, M.IdSubCliente, D.IdProducto, '') as int) as ContenidoPaquete, 			
		( D.Cant_Entregada - D.Cant_Devuelta ) as Cantidad, 
		----(( D.Cant_Entregada - D.Cant_Devuelta ) / (P.ContenidoPaquete * 1.0)) as CantidadCajas, 
		(
			( D.Cant_Entregada - D.Cant_Devuelta ) 
			/ 
			(cast(dbo.fg_ContenidoPaquete_ClaveLicitada(M.IdEstado, M.IdCliente, M.IdSubCliente, D.IdProducto, '') as int) * 1.0)
		) as CantidadCajas, 		
		M.EsCorte, D.CostoUnitario as Costo, D.PrecioUnitario as Importe, --, D.Keyx as KeyxDetalle
		D.Renglon 
	From vw_VentasEnc M (NoLock) 
	Inner Join VentasDet D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioVenta ) 
	-- Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 	
	-- Where D.Status = 'A' 
	
Go--#SQL  
 	
 	
--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_VentasDet_CodigosEAN_Lotes' and xType = 'V' ) 
	Drop View vw_VentasDet_CodigosEAN_Lotes 
Go--#SQL	 	

Create View vw_VentasDet_CodigosEAN_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, E.CLUES, E.NombrePropio_UMedica, E.EsAlmacen, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 	
		E.Folio, L.IdProducto, L.CodigoEAN, 
		E.ClaveSSA_Base, E.ClaveSSA, E.ClaveSSA_Aux, 
		E.ContenidoPaquete, D.ClaveLote, 
		(case when D.EsConsignacion = 1 then 1 else 0 end) as EsConsignacion, 
		L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, 
		cast(D.CantidadVendida as int) as Cantidad, --, D.Keyx as KeyxDetalleLote 
		(cast(D.CantidadVendida as int) / (E.ContenidoPaquete * 1.0)) as CantidadCajas 
	From vw_VentasDet_CodigosEAN E (NoLock) 
	Inner Join VentasDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioVenta and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
	-- Where D.Status = 'A' 
	
Go--#SQL  
 	
 	
--------------------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_VentasDispensacion_InformacionAdicional' and xType = 'V' ) 
	Drop View vw_VentasDispensacion_InformacionAdicional  
Go--#SQL 	

Create View vw_VentasDispensacion_InformacionAdicional 
With Encryption 
As 
--- Mostrar solo las Ventas de Credito 
	Select v.IdEmpresa, v.Empresa, v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.CLUES, v.NombrePropio_UMedica, v.Farmacia, v.Folio, 
		v.FechaSistema, v.FechaRegistro, 
		v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		I.IdBeneficiario, B.NombreCompleto as Beneficiario, B.CURP, 
		
		I.IdTipoDerechoHabiencia, DH.Descripcion as DerechoHabiencia, 
		I.IdEstadoResidencia, E.Nombre as EstadoDeResidencia, E.ClaveRENAPO as ClaveRENAPO__EstadoDeResidencia, 
		
		B.Domicilio, 
		B.Sexo, (case when B.Sexo = 'M' Then 'Masculino' Else 'Femenino' End) as SexoAux, 
		B.FechaNacimiento, 
		B.FolioReferencia, B.Edad,
		B.FechaInicioVigencia, B.FechaFinVigencia, B.EsVigente, 
		B.FechaRegistro as FechaRegBen, 
		I.NumReceta, I.FechaReceta, 
		T.IdTipoDeDispensacion, T.Descripcion as TipoDeDispensacion, 
		I.IdUMedica, U.NombreUnidadMedica as NombreUMedica, 
		I.IdMedico, (M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) as Medico, M.NumCedula as Cedula,
		I.IdBeneficio, Bn.ClaveBeneficio, Bn.Descripcion as Beneficio, 
		I.IdDiagnostico, D.Descripcion as Diagnostico, 
		I.IdServicio, S.Servicio, 
		I.IdArea, S.Area_Servicio, 
		I.RefObservaciones,
		I.NumeroDeHabitacion, I.NumeroDeCama
	From vw_VentasEnc V (NoLock) 
	Inner Join VentasInformacionAdicional I (NoLock) On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.FolioVenta ) 
	Inner Join CatTiposDispensacion T (NoLock) On ( I.IdTipoDeDispensacion = T.IdTipoDeDispensacion ) 
	Inner Join vw_Beneficiarios B (NoLock) 
		On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and I.IdBeneficiario = B.IdBeneficiario ) 
	Inner Join CatMedicos M (NoLock) On ( I.IdEstado = M.IdEstado and I.IdFarmacia = M.IdFarmacia and I.IdMedico = M.IdMedico )  
	Inner Join CatCIE10_Diagnosticos D (NoLock) On ( I.IdDiagnostico = D.ClaveDiagnostico ) 
	Inner Join vw_Servicios_Areas S (NoLock) On ( I.IdServicio = S.IdServicio and I.IdArea = S.IdArea ) 
	Inner Join CatBeneficios Bn (NoLock) On ( I.IdBeneficio = Bn.IdBeneficio )  
	Inner Join CatUnidadesMedicas U (NoLock) On ( I.IdUMedica = U.IdUMedica ) 

	Inner Join CatTiposDeDerechohabiencia DH On ( I.IdTipoDerechoHabiencia = DH.IdTipoDerechoHabiencia ) 
	Inner Join CatEstados E (NoLock) On ( I.IdEstadoResidencia = E.IdEstado ) 

	Where v.TipoDeVenta = 2 
	
Go--#SQL 
	
--------------------------------------------------------------------------------------------------------------------------------------- 
--------------------------------- ALMACENES 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_VentasDet_CodigosEAN_Lotes_Ubicaciones' and xType = 'V' ) 
	Drop View vw_VentasDet_CodigosEAN_Lotes_Ubicaciones 
Go--#SQL 

Create View vw_VentasDet_CodigosEAN_Lotes_Ubicaciones  
With Encryption 
As 	

	Select D.IdEmpresa, E.Nombre As Empresa, D.IdEstado, F.Estado, D.IdFarmacia, F.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		D.FolioVenta As Folio, 
		D.IdProducto, D.CodigoEAN, D.ClaveLote, D.IdPasillo, D.IdEstante, D.IdEntrepaño,
		-- P.FechaRegistro as FechaReg, P.FechaCaducidad as FechaCad,		 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		L.Existencia, cast(D.CantidadVendida as int) as Cantidad 
	From VentasDet_Lotes_Ubicaciones D (NoLock)		
	Inner Join FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote and
			 D.IdPasillo = L.IdPasillo and D.IdEstante = L.IdEstante and D.IdEntrepaño = L.IdEntrepaño )
	Inner Join CatEmpresas E (NoLock) On ( D.IdEmpresa = E.IdEmpresa )	 
	Inner Join vw_Farmacias F (NoLock) On ( D.IdEstado = F.IdEstado and D.IdFarmacia = F.IdFarmacia )
----	Inner Join FarmaciaProductos_CodigoEAN_Lotes P (NoLock) 
----		On ( D.IdEmpresa = P.IdEmpresa and D.IdEstado = P.IdEstado and D.IdFarmacia = P.IdFarmacia and D.IdSubFarmacia = P.IdSubFarmacia and  
----			 D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN and D.ClaveLote = P.ClaveLote )

--		Select * From vw_VentasDet_CodigosEAN_Lotes_Ubicaciones (Nolock)

Go--#SQL 
		
		