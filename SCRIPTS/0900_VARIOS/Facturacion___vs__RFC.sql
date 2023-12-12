
Set dateformat YMD 
Go--#SQL 

	If Exists ( Select * from sysobjects (nolock) Where Name = 'vw_FACT_CFD_DocumentosElectronicos___REV_20201210_1045' and xType = 'U' ) Drop Table vw_FACT_CFD_DocumentosElectronicos___REV_20201210_1045 
Go--#SQL 

	Select 
		identity(int, 1, 1) as Secuencial, 
		--3.3 as Version, 
		E.IdEmpresa, E.Empresa, 
		E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, 
		FechaRegistro, IdTipoDocumento, 
		E.Serie, E.Folio, 
		
		E.UUID, 

		E.Importe, E.RFC, E.NombreReceptor,

		E.CanceladoSAT, 
		E.IdPersonalCancela, E.FechaCancelacion, E.MotivoCancelacion, 
		E.StatusDocto, E.StatusDoctoAux, E.StatusDocumento, E.Observaciones_01, E.Observaciones_02, E.Observaciones_03, 


		E.SubTotal, E.Iva, E.Total  

	Into vw_FACT_CFD_DocumentosElectronicos___REV_20201210_1045
	From vw_FACT_CFD_DocumentosElectronicos E (NoLock) 
	Where E.RFC like '%IME970211V92%'
	Order by E.FechaRegistro 
	 
Go--#SQL 


	If Exists ( Select * from sysobjects (nolock) Where Name = 'vw_FACT_CFD_DocumentosElectronicos_Detalles___REV_20201210_1045' and xType = 'U' ) Drop Table vw_FACT_CFD_DocumentosElectronicos_Detalles___REV_20201210_1045 
Go--#SQL 


	Select 
		E.Secuencial, 
		--E.Version, 
		E.IdEmpresa, E.Empresa, 
		E.IdEstado, E.Estado, E.IdFarmacia, E.Farmacia, 
		FechaRegistro, IdTipoDocumento, 
		E.Serie, E.Folio, 
		
		E.UUID, 

		E.Importe, E.RFC, E.NombreReceptor,

		E.CanceladoSAT, 
		E.IdPersonalCancela, E.FechaCancelacion, E.MotivoCancelacion, 
		E.StatusDocto, E.StatusDoctoAux, E.StatusDocumento, E.Observaciones_01, E.Observaciones_02, E.Observaciones_03, 


		E.SubTotal, E.Iva, E.Total, 
		Clave, DescripcionConcepto, 
		UnidadDeMedida, Cantidad, PrecioUnitario, TasaIva, 
		( (D.cantidad * D.PrecioUnitario) - D.SubTotal ) as Diferencia, 
		(D.cantidad * D.PrecioUnitario) as SubTotal_Detalle_Calculado, 
		D.SubTotal as SubTotal_Detalle, D.Iva as IVA_Detalle, D.Total as Total_Detalle  
	Into vw_FACT_CFD_DocumentosElectronicos_Detalles___REV_20201210_1045 
	From vw_FACT_CFD_DocumentosElectronicos___REV_20201210_1045 E (NoLock)
	Inner Join FACT_CFD_Documentos_Generados_Detalles D (NoLock)
		On ( E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.idestado And E.Idfarmacia = D.idfarmacia And E.Serie = D.serie And E.Folio = D.Folio )  


Go--#SQL 


	Exec sp_FormatearTabla vw_FACT_CFD_DocumentosElectronicos___REV_20201210_1045  


	Exec sp_FormatearTabla vw_FACT_CFD_DocumentosElectronicos_Detalles___REV_20201210_1045  

--Go--#SQL 


	select * 
	from vw_FACT_CFD_DocumentosElectronicos___REV_20201210_1045 (NoLock) 


	select * 
	from vw_FACT_CFD_DocumentosElectronicos_Detalles___REV_20201210_1045 (NoLock)  

--Go--#SQL 


