--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_CFD_DocumentosElectronicos' and xType = 'V' ) 
	Drop View vw_FACT_CFD_DocumentosElectronicos 
Go--#SQL 
 	
Create View vw_FACT_CFD_DocumentosElectronicos 
With Encryption 
As 

	Select  
		Distinct 
		E.Keyx as Identificador, 
		E.IdEmpresa, EM.Nombre as Empresa, 
		E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia, 
		E.FechaRegistro, E.IdTipoDocumento, T.NombreDocumento as TipoDeDocumento, 
		E.Serie, E.Folio, 
		
		--D.SubTotal, D.Iva, D.Importe, D.Importe as Total, 

		( case when E.IdTipoDocumento = '007' then E.SubTotal else D.SubTotal end) as SubTotal, 
		( case when E.IdTipoDocumento = '007' then E.Iva else D.Iva end) as Iva, 
		( case when E.IdTipoDocumento = '007' then E.Importe else D.Importe end) as Importe, 
		( case when E.IdTipoDocumento = '007' then E.Importe else D.Importe end) as Total, 


		E.TienePagoRelacionado,
		CF.IdCliente, 
		E.RFC, E.NombreReceptor, 
		E.Status as StatusDocto,  
		(case when E.Status = 'A' Then 1 Else 0 End) as StatusDoctoAux, 
		(case when E.Status = 'A' Then 'Activo' Else 'Cancelado' End) as StatusDocumento, 	
		E.Observaciones_01, E.Observaciones_02, E.Observaciones_03, E.Referencia, 
		
		E.TipoDocumento, 
		(
			case When E.TipoDocumento = 1 Then 'Venta' 
				 When E.TipoDocumento = 2 Then 'Administración' 
			else '' 
			End 
		) as TipoDocumentoDescripcion, 
		E.TipoInsumo, 
		(
			case When E.TipoInsumo = 1 Then 'Material de curación' 
				 When E.TipoInsumo = 2 Then 'Medicamento' 
			else '' 
			End 
		) as TipoInsumoDescripcion,
		'' as RubroFinanciamento, 		 		
		'' as RubroFinanciamiento, 
		'' as Financiamiento, 
		IdPersonalEmite, 
		IsNull((Select NombreCompleto  
				From vw_Personal P (NoLock) 
				where P.IdEstado = E.IdEstado and P.IdFarmacia = E.IdFarmacia and P.IdPersonal = E.IdPersonalEmite ),'') as PersonalEmite, 				
		IdPersonalCancela, 
		IsNull((Select NombreCompleto  
				From vw_Personal P (NoLock) 
				where P.IdEstado = E.IdEstado and P.IdFarmacia = E.IdFarmacia and P.IdPersonal = E.IdPersonalCancela ),'') as PersonalCancela, 
		(case when E.Status = 'A' Then '' Else convert(varchar(30), FechaCancelacion, 120) End) as FechaCancelacion, 
		MotivoCancelacion, 
		IsNull(X.IdPAC, '001') as IdPAC, IsNull(P.NombrePAC, '') as NombrePAC, 
		IsNull(X.uf_FolioSAT, '') as UUID   
		
	From CFDI_Documentos E (NoLock) 
	Inner Join CFDI_Clientes CF (NoLock) On ( E.RFC = CF.RFC and CF.Status = 'A' ) 
	Inner Join CFDI_TiposDeDocumentos T (NoLock) On ( E.IdTipoDocumento = T.IdTipoDocumento ) 
	Inner Join CatEmpresas EM (NoLock) On ( E.IdEmpresa = EM.IdEmpresa ) 
	Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
	Left Join CFDI_XML X (NoLock) 
		On ( E.IdEmpresa = X.IdEmpresa and E.IdEstado = X.IdEstado and E.IdFarmacia = X.IdFarmacia and E.Serie = X.Serie And E.Folio = X.Folio )  
	Left Join CFDI_PACs P (NoLock) On ( X.IdPAC = P.IdPAC)	
	Left Join 
	( 
		Select IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, 
			cast(round(sum(SubTotal), 2) as numeric(14,2)) as SubTotal, 
			cast(round(sum(Iva), 2) as numeric(14,2)) as Iva, 
			cast(round(Sum(Importe), 2) as numeric(14,2)) as Importe 
		From CFDI_Documentos_Conceptos DC (NoLock) 
		Group by IdEmpresa, IdEstado, IdFarmacia, Serie, Folio 
	) D On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Serie = D.Serie And E.Folio = D.Folio )


Go--#SQL 



--		select * from  vw_FACT_CFD_DocumentosElectronicos 

--		update FACT_CFD_Documentos_Generados Set Importe = 154.00 * 1.15 where Folio = 7 

--		select * from FACT_CFD_TiposDeDocumentos 

--	sp_listacolumnas vw_FACT_CFD_Sucursales_Series 
	