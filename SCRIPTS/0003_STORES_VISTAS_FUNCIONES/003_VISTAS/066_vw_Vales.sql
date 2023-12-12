------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Vales_EmisionEnc' and xType = 'V' ) 
	Drop View vw_Vales_EmisionEnc 
Go--#SQL

Create View vw_Vales_EmisionEnc 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa,  
	    M.IdEstado, F.Estado as Estado, F.ClaveRenapo, M.IdFarmacia, F.Farmacia, F.CLUES, F.NombrePropio_UMedica, 
		M.FolioVale as Folio, M.FolioVenta, 
	    M.EsSegundoVale, IsNull(R.FolioValeReembolso, '') as FolioValeReembolso, 
	    M.FechaSistema, 
		M.FechaRegistro, M.FechaCanje, M.IdPersonal, vP.NombreCompleto as NombrePersonal, 
		M.IdCliente, IsNull(C.NombreCliente, '') as NombreCliente, 
		M.IdSubCliente, IsNull(C.NombreSubCliente, '') as NombreSubCliente, 
		M.IdPrograma, IsNull(Pr.Programa, '') as Programa, 
		M.IdSubPrograma, IsNull(Pr.SubPrograma, '') as SubPrograma, 		
		M.Status, F.IdMunicipio, F.Municipio, F.IdColonia, F.Colonia, F.Domicilio, M.IdPersonaFirma,
		M.FolioTimbre
	From Vales_EmisionEnc M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 		
	Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal ) 
	Left Join Vales_Emision_Reembolso_Enc R 
		On ( M.IdEmpresa = R.IdEmpresa and M.IdEstado = R.IdEstado and M.IdFarmacia = R.IdFarmacia and M.FolioVale = R.FolioVale ) 
	Left Join vw_Clientes_SubClientes C (NoLock) On ( M.IdCliente = C.IdCliente and M.IdSubCliente = C.IdSubCliente )
	Left Join vw_Programas_SubProgramas Pr (NoLock) On ( M.IdPrograma = Pr.IdPrograma and M.IdSubPrograma = Pr.IdSubPrograma ) 		
Go--#SQL


------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Vales_EmisionDet' and xType = 'V' ) 
	Drop View vw_Vales_EmisionDet 
Go--#SQL	 	

Create View vw_Vales_EmisionDet 
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.CLUES, M.NombrePropio_UMedica, M.Folio, M.FolioVenta, 
		M.EsSegundoVale, M.FolioValeReembolso, 
		D.IdClaveSSA_Sal, S.ClaveSSA_Base, 

		-- S.ClaveSSA_Base as ClaveSSA, S.ClaveSSA as ClaveSSA_Aux, 
		S.ClaveSSA, S.ClaveSSA as ClaveSSA_Aux, 		

		S.Descripcion as DescripcionSal, 
		S.Descripcion as DescripcionCortaClave,  
		D.IdPresentacion, P.Descripcion as Presentacion, 
		D.Cantidad, D.Cantidad_2 as CantidadSegundoVale, 
		M.Status as StatusEnc, D.Status as StatusDet
	From vw_Vales_EmisionEnc M (NoLock) 
	Inner Join Vales_EmisionDet D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioVale ) 
	Inner Join CatClavesSSA_SAles S (NoLock) On ( D.IdClaveSSA_Sal = S.IdClaveSSA_Sal ) 
	Inner Join CatPresentaciones P(NoLock) On ( D.IdPresentacion = P.IdPresentacion ) 

Go--#SQL
 
 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Vales_Emision_InformacionAdicional' and xType = 'V' ) 
	Drop View vw_Vales_Emision_InformacionAdicional  
Go--#SQL	

Create View vw_Vales_Emision_InformacionAdicional 
With Encryption 
As 
	Select v.IdEmpresa, v.Empresa, v.IdEstado, v.Estado, v.ClaveRenapo, v.IdFarmacia, v.Farmacia, v.CLUES, v.NombrePropio_UMedica, v.Folio, 
		v.FechaSistema,
		v.FechaRegistro, 
		v.IdCliente, v.NombreCliente, v.IdSubCliente, v.NombreSubCliente, v.IdPrograma, v.Programa, v.IdSubPrograma, v.SubPrograma, 
		I.IdBeneficiario, B.NombreCompleto as Beneficiario, 
		B.Sexo, (case when B.Sexo = 'M' Then 'Masculino' Else 'Femenino' End) as SexoAux, 
		B.FechaNacimiento, 
		B.FolioReferencia, B.Edad,
		B.FechaInicioVigencia, B.FechaFinVigencia, B.EsVigente, 
		B.FechaRegistro as FechaRegBen, 
		I.NumReceta, I.FechaReceta, 
		T.IdTipoDeDispensacion, T.Descripcion as TipoDeDispensacion, 
		I.IdUMedica, U.NombreUnidadMedica as NombreUMedica, 
		I.IdMedico, (M.ApPaterno + ' ' + M.ApMaterno + ' ' + M.Nombre) as Medico, 
		I.IdBeneficio, Bn.ClaveBeneficio, Bn.Descripcion as Beneficio, 
		I.IdDiagnostico, D.Descripcion as Diagnostico, 
		I.IdServicio, S.Servicio, 
		I.IdArea, S.Area_Servicio, 
		I.RefObservaciones 
	From vw_Vales_EmisionEnc V (NoLock) 
	Inner Join Vales_Emision_InformacionAdicional I (NoLock) On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.Folio = I.FolioVale ) 
	Inner Join CatTiposDispensacion T (NoLock) On ( I.IdTipoDeDispensacion = T.IdTipoDeDispensacion )
	Inner Join vw_Beneficiarios B (NoLock) 
		On ( V.IdEstado = B.IdEstado and V.IdFarmacia = B.IdFarmacia and V.IdCliente = B.IdCliente and V.IdSubCliente = B.IdSubCliente and I.IdBeneficiario = B.IdBeneficiario ) 
	Inner Join CatMedicos M (NoLock) On ( I.IdEstado = M.IdEstado and I.IdFarmacia = M.IdFarmacia and I.IdMedico = M.IdMedico )  
	Inner Join CatCIE10_Diagnosticos D (NoLock) On ( I.IdDiagnostico = D.ClaveDiagnostico ) 
	Inner Join vw_Servicios_Areas S (NoLock) On ( I.IdServicio = S.IdServicio and I.IdArea = S.IdArea ) 
	Inner Join CatBeneficios Bn (NoLock) On ( I.IdBeneficio = Bn.IdBeneficio )  
	Inner Join CatUnidadesMedicas U (NoLock) On ( I.IdUMedica = U.IdUMedica ) 
	
Go--#SQL


------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Vales' and xType = 'V' ) 
	Drop View vw_Impresion_Vales 
Go--#SQL

Create View vw_Impresion_Vales 
With Encryption 
As 

	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.CLUES, M.NombrePropio_UMedica, M.Folio, 
		IsNull(M.FolioVenta, '') as FolioVenta,  
		M.EsSegundoVale, M.FolioValeReembolso, 
		iA.IdBeneficiario, B.NombreCompleto as Beneficiario, B.FolioReferencia, iA.NumReceta, iA.FechaReceta, 
		M.FechaSistema, 
		M.FechaRegistro, M.FechaCanje, M.IdPersonal, M.NombrePersonal, 
		M.IdCliente, M.NombreCliente, M.IdSubCliente, M.NombreSubCliente, 
		M.IdPrograma, M.Programa, M.IdSubPrograma, M.SubPrograma, M.Status,
		D.IdClaveSSA_Sal, D.ClaveSSA_Base, D.ClaveSSA, D.DescripcionSal, D.DescripcionCortaClave, 
		D.IdPresentacion, D.Presentacion, 
		D.Cantidad, D.CantidadSegundoVale, 
		M.IdMunicipio, M.Municipio, M.IdColonia, M.Colonia, M.Domicilio	
	From vw_Vales_EmisionEnc M (NoLock) 
	Inner Join vw_Vales_EmisionDet D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.Folio ) 
	Inner Join Vales_Emision_InformacionAdicional iA(NoLock) On ( M.IdEmpresa = iA.IdEmpresa And M.IdEstado = iA.IdEstado and M.IdFarmacia = iA.IdFarmacia And M.Folio = iA.FolioVale ) 
	Inner Join vw_Beneficiarios B(NoLock)  On ( iA.IdEstado = B.IdEstado And iA.IdFarmacia = B.IdFarmacia And M.IdCliente = B.IdCliente And M.IdSubCliente = B.IdSubCliente And iA.IdBeneficiario = B.IdBeneficiario )	

Go--#SQL


------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_ValesEnc' and xType = 'V' ) 
	Drop View vw_ValesEnc 
Go--#SQL 	

Create View vw_ValesEnc 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa, M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
	     M.IdFarmacia, F.Farmacia, F.CLUES, F.NombrePropio_UMedica, M.Folio, M.FolioVale, M.FolioVentaGenerado, 
		 M.IdPersonal, vP.NombreCompleto as NombrePersonal, M.FechaSistema, M.IdProveedor, P.Nombre as Proveedor, 
		 M.ReferenciaDocto, Convert( varchar(10), M.FechaDocto, 120 ) as FechaDocto, 
		 Convert( varchar(10), M.FechaVenceDocto, 120 ) as FechaVenceDocto, M.Observaciones, 
		 M.SubTotal, M.Iva, M.Total, M.FechaRegistro, 
		 M.Status, F.IdMunicipio, F.Municipio, F.IdColonia, F.Colonia, F.Domicilio   
	From ValesEnc M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join CatProveedores P (NoLock) On ( M.IdProveedor = P.IdProveedor ) 
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )	
Go--#SQL	

 
------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_ValesDet' and xType = 'V' ) 
	Drop View vw_ValesDet
Go--#SQL  	

Create View vw_ValesDet
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.CLUES, M.NombrePropio_UMedica, M.Folio, M.FolioVale, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, 
		D.UnidadDeEntrada, D.TasaIva, 
		D.CostoUnitario as Costo, 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida   		
	From vw_ValesEnc M (NoLock) 
	Inner Join ValesDet D (NoLock) On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.Folio) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	-- Where D.Status = 'A' 
Go--#SQL
 	

------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_ValesDet_Lotes' and xType = 'V' ) 
	Drop View vw_ValesDet_Lotes 
Go--#SQL  	

Create View vw_ValesDet_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, E.CLUES, E.NombrePropio_UMedica, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		E.Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, --, D.Keyx as KeyxDetalleLote
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida   
	From vw_ValesDet E (NoLock) 
	Inner Join ValesDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.Folio and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote )
Go--#SQL
 	
 	
------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Vales_Registrados' and xType = 'V' ) 
	Drop View vw_Impresion_Vales_Registrados 
Go--#SQL	

Create View vw_Impresion_Vales_Registrados 
With Encryption 
As 

	Select -- L.*, vP.*  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, E.CLUES, E.NombrePropio_UMedica, 
		   L.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmacia, 		   
		   E.Folio, E.FolioVale, 
		   E.FechaSistema, E.IdPersonal, E.NombrePersonal, E.Observaciones, 
		   E.IdProveedor, E.Proveedor, E.ReferenciaDocto, E.FechaDocto, E.FechaVenceDocto, 
		   E.SubTotal, E.Iva, E.Total, E.Status, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, vP.DescripcionCortaClave, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, vP.DescripcionCorta as DescripcionCortaProducto, 
		   D.CodigoEAN, L.ClaveLote, D.CostoUnitario, L.CantidadRecibida as CantidadLote, 
		   D.TasaIva, (D.CostoUnitario * L.CantidadRecibida) as SubTotalLote, 
		   (D.CostoUnitario * L.CantidadRecibida) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.CostoUnitario * L.CantidadRecibida) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad, F.FechaRegistro, E.IdMunicipio, E.Municipio, E.IdColonia, E.Colonia, E.Domicilio	  
	From vw_ValesEnc E (NoLock) 
	Inner Join ValesDet D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.Folio ) 
	Inner Join ValesDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.Folio = L.Folio 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = D.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos vP On ( D.IdProducto = vP.IdProducto ) 
Go--#SQL


-------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------- 
-------- Farmacias Convenio-Vales  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Vales_Farmacias_Convenio' and xType = 'V' ) 
	Drop View vw_Vales_Farmacias_Convenio  
Go--#SQL	

Create View vw_Vales_Farmacias_Convenio 
With Encryption 
As 

	Select 
		C.IdEstado, IsNull(F.Estado, '') as Estado, C.IdFarmacia, IsNull(F.Farmacia, '') as Farmacia, F.CLUES, F.NombrePropio_UMedica, 
		C.IdFarmaciaConvenio, FC.Nombre as FarmaciaConvenio, FC.Direccion, 
		C.Status 
	From CFG_Farmacias_ConvenioVales C (NoLock) 
	Left Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
	Inner Join CatFarmacias_ConvenioVales FC (NoLock) On ( C.IdEstado = FC.IdEstado and C.IdFarmaciaConvenio = FC.IdFarmaciaConvenio ) 

--	Select * 	from CatFarmacias_ConvenioVales 

Go--#SQL 


--		select * 	from vw_Vales_Farmacias_Convenio where IdFarmacia = 188 

-------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------- 
-------- Farmacias Convenio-Vales  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Vales_ServiciosADomicilio' and xType = 'V' ) 
	Drop View vw_Vales_ServiciosADomicilio
Go--#SQL	

Create View vw_Vales_ServiciosADomicilio 
With Encryption 
As 

	Select 
		V.IdEmpresa, V.Empresa, V.IdEstado, V.Estado, V.ClaveRenapo, V.IdFarmacia, V.Farmacia, V.CLUES, V.NombrePropio_UMedica, 
		V.Folio, V.FolioVenta, S.FolioServicioDomicilio, 
		S.FechaRegistro, S.HoraVisita_Desde, S.HoraVisita_Hasta, 
		convert(varchar(5), S.HoraVisita_Desde, 108) as VisitaDesde, 
		convert(varchar(5), S.HoraVisita_Hasta, 108) as VisitaHasta,  
		V.EsSegundoVale, V.FolioValeReembolso, 
		V.IdBeneficiario, V.Beneficiario, V.FolioReferencia, V.NumReceta, V.FechaReceta, 
		V.FechaRegistro as FechaRegistroVale, V.FechaCanje, V.IdPersonal, V.NombrePersonal, 
		V.IdCliente, V.NombreCliente, V.IdSubCliente, V.NombreSubCliente, 
		V.IdPrograma, V.Programa, V.IdSubPrograma, V.SubPrograma, V.Status,
		V.IdClaveSSA_Sal, V.ClaveSSA_Base, V.ClaveSSA, V.DescripcionSal, V.DescripcionCortaClave, 
		V.IdPresentacion, V.Presentacion, 
		V.Cantidad, V.CantidadSegundoVale, 
		V.IdMunicipio, V.Municipio, V.IdColonia, V.Colonia, V.Domicilio, 
		S.ServicioConfirmado, S.TipoSurtimiento, S.ReferenciaSurtimiento	
	From Vales_Servicio_A_Domicilio S (NoLock) 
	-- Inner Join vw_Beneficiarios_Domicilios D (NoLock) 
	Inner Join vw_Impresion_Vales V (NoLock) 
		On ( S.IdEmpresa = V.IdEmpresa and S.IdEstado = V.IdEstado and S.IdFarmacia = V.IdFarmacia and S.FolioVale = V.Folio ) 
	

Go--#SQL 	


-------------------------------------------------------------------------------------------------------------------------------- 
-------------------------------------------------------------------------------------------------------------------------------- 
-------- Domicilios para control de Servicio a Domicilio ( Vales )  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Beneficiarios_Domicilios' and xType = 'V' ) 
   Drop View vw_Beneficiarios_Domicilios 
Go--#SQL

Create View vw_Beneficiarios_Domicilios 
With Encryption 
As 
	Select B.IdEstado, B.IdFarmacia, B.IdCliente, B.IdSubCliente, B.IdBeneficiario,
		B.IdEstado_D, E.Nombre As Estado_D, B.IdMunicipio_D, M.Descripcion As Municipio_D, B.IdColonia_D, C.Descripcion As Colonia_D,
		B.CodigoPostal, B.Direccion, B.Referencia, B.Telefonos
	From CatBeneficiarios_Domicilios B (NoLock)
	Inner Join CatEstados E (NoLock) On (B.IdEstado_D = E.IdEstado)
	Inner Join CatMunicipios M (NoLock) ON (B.IdEstado_D = M.IdEstado And B.IdMunicipio_D = M.IdMunicipio)
	Inner Join CatColonias C (NoLock) ON (B.IdEstado_D = C.IdEstado And B.IdMunicipio_D = C.IdMunicipio And B.IdColonia_D = C.IdColonia)

Go--#SQL 	 


-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Vales_Servicio_A_Domicilio' and xType = 'V' ) 
	Drop View vw_Vales_Servicio_A_Domicilio 
Go--#SQL 	

---			Select * From Vales_Servicio_A_Domicilio (Nolock)

Create View vw_Vales_Servicio_A_Domicilio 
With Encryption 
As 

	Select M.IdEmpresa, Ex.Nombre as Empresa, M.IdEstado, E.Nombre as Estado, E.ClaveRenapo, 
	     M.IdFarmacia, F.Farmacia, M.FolioServicioDomicilio as Folio, M.FolioVale, M.FolioVentaGenerado,
	     Convert(varchar(10), M.FechaRegistro, 120) as FechaRegistro, 
		 M.IdPersonal, vP.NombreCompleto as Personal, M.HoraVisita_Desde, M.HoraVisita_Hasta,
		 M.ServicioConfirmado, Convert(varchar(10), M.FechaConfirmacion, 120) as FechaConfirmacion, 
		 M.IdPersonalConfirma, PC.NombreCompleto as PersonalConfirma,
		 M.TipoSurtimiento, M.ReferenciaSurtimiento, M.Status 
	From Vales_Servicio_A_Domicilio M (NoLock) 
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 
	Inner Join CatEstados E (NoLock) On ( M.IdEstado = E.IdEstado ) 
	Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstado = vP.IdEstado and M.IdFarmacia = vP.IdFarmacia and M.IdPersonal = vP.IdPersonal )
	Inner Join vw_Personal PC (NoLock) On ( M.IdEstado = PC.IdEstado and M.IdFarmacia = PC.IdFarmacia and M.IdPersonalConfirma = PC.IdPersonal )	

Go--#SQL	
 

-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Vales_Servicio_A_DomicilioDet' and xType = 'V' ) 
	Drop View vw_Vales_Servicio_A_DomicilioDet
Go--#SQL

	----		Select * From Vales_Servicio_A_DomicilioDet (Nolock) 	

Create View vw_Vales_Servicio_A_DomicilioDet
With Encryption 
As 
	Select M.IdEmpresa, M.Empresa, M.IdEstado, M.Estado, M.ClaveRenapo, M.IdFarmacia, M.Farmacia, M.Folio, M.FolioVale, 
		D.IdProducto, D.CodigoEAN, P.Descripcion as DescProducto, 
		D.UnidadDeEntrada, D.TasaIva, D.CostoUnitario as Costo, 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida   		
	From vw_Vales_Servicio_A_Domicilio M (NoLock) 
	Inner Join Vales_Servicio_A_DomicilioDet D (NoLock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.Folio = D.FolioServicioDomicilio) 
	Inner Join CatProductos P (NoLock) On ( D.IdProducto = P.IdProducto ) 
	-- Where D.Status = 'A' 

Go--#SQL
 	

-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Vales_Servicio_A_DomicilioDet_Lotes' and xType = 'V' ) 
	Drop View vw_Vales_Servicio_A_DomicilioDet_Lotes 
Go--#SQL

----		Select * From Vales_Servicio_A_DomicilioDet_Lotes (Nolock) 	

Create View vw_Vales_Servicio_A_DomicilioDet_Lotes  
With Encryption 
As 	

	Select E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia, 
		E.Folio, L.IdProducto, L.CodigoEAN, L.ClaveLote, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad, 
		(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status, 
		cast(L.Existencia as Int) as Existencia, --, D.Keyx as KeyxDetalleLote
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cantidad,		 
		cast( (D.Cant_Recibida - D.Cant_Devuelta) as int) as Cant_Recibida, 
		cast(D.Cant_Devuelta as int) as Cant_Devuelta, 
		cast(D.CantidadRecibida as int) as CantidadRecibida   
	From vw_Vales_Servicio_A_DomicilioDet E (NoLock) 
	Inner Join Vales_Servicio_A_DomicilioDet_Lotes D (noLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioServicioDomicilio and 
			 E.IdProducto = D.IdProducto and E.CodigoEAN = D.CodigoEAN ) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.IdSubFarmacia = L.IdSubFarmacia and  
			 D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN and D.ClaveLote = L.ClaveLote ) 

Go--#SQL
 	

-------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Vales_Registrados_ServicioDom' and xType = 'V' ) 
	Drop View vw_Impresion_Vales_Registrados_ServicioDom 
Go--#SQL	

Create View vw_Impresion_Vales_Registrados_ServicioDom 
With Encryption 
As 

	Select -- L.*, vP.*  
		   E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, 
		   L.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(L.IdEstado, L.IdFarmacia, L.IdSubFarmacia) as SubFarmacia, 		   
		   E.Folio, E.FolioVale, E.IdPersonal, E.Personal, E.ReferenciaSurtimiento, E.FechaConfirmacion, E.Status, 
		   vP.IdClaveSSA_Sal, vP.ClaveSSA, vP.DescripcionSal, vP.DescripcionCortaClave, 
		   D.IdProducto, vP.Descripcion as DescripcionProducto, vP.DescripcionCorta as DescripcionCortaProducto, 
		   D.CodigoEAN, L.ClaveLote, D.CostoUnitario, L.CantidadRecibida as CantidadLote, 
		   D.TasaIva, (D.CostoUnitario * L.CantidadRecibida) as SubTotalLote, 
		   (D.CostoUnitario * L.CantidadRecibida) * (case when D.TasaIva = 0 then 1 else (D.TasaIva / 100.0) end) as ImpteIvaLote, 
		   (D.CostoUnitario * L.CantidadRecibida) * (case when D.TasaIva = 0 then 1 else 1 + (D.TasaIva / 100.0) end) as ImporteLote, 		    
		   F.FechaCaducidad, F.FechaRegistro	  
	From vw_Vales_Servicio_A_Domicilio E (NoLock) 
	Inner Join Vales_Servicio_A_DomicilioDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioServicioDomicilio ) 
	Inner Join Vales_Servicio_A_DomicilioDet_Lotes L (NoLock) 
		On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioServicioDomicilio = L.FolioServicioDomicilio 
			 and D.IdProducto = L.IdProducto and D.CodigoEAN = D.CodigoEAN )  
	Inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On ( F.IdEmpresa = L.IdEmpresa and F.IdEstado = L.IdEstado and F.IdFarmacia = L.IdFarmacia and F.IdSubFarmacia = L.IdSubFarmacia 
			 and F.IdProducto = L.IdProducto and F.CodigoEAN = L.CodigoEAN and F.ClaveLote = L.ClaveLote )  	
	Inner Join vw_Productos vP On ( D.IdProducto = vP.IdProducto )  

Go--#SQL


--------------------------------------------------------------------------------------------------------------------------------	
	