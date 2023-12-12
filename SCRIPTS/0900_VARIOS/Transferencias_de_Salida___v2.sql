
Set dateformat YMD 
Go--#SQL 




/* 
drop table TransferenciasDet_Lotes_____20201210 

drop table vw_Productos_CodigoEAN___20201210 


select * 
into vw_Productos_CodigoEAN___20201210 
from vw_Productos_CodigoEAN 


	select D.*, P.ClaveSSA, P.DescripcionClave  
	Into TransferenciasDet_Lotes_____20201210 
	from TransferenciasDet_Lotes D 
	Inner Join vw_Productos_CodigoEAN___20201210 P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )  
	where IdEstado = 11 and IdFarmacia in ( 5, 1005 ) 
		and 
		( 
			P.ClaveSSA like '%0615%' 
			or 
			P.ClaveSSA like '%4148%' 
		) 

*/ 



	If Exists ( Select * from sysobjects (nolock) Where Name = 'RPT__Transferencias__20201210' and xType = 'U' ) Drop Table RPT__Transferencias__20201210 
Go--#SQL 

	If Exists ( Select * from sysobjects (nolock) Where Name = 'RPT__Transferencias_Detalles_20201210' and xType = 'U' ) Drop Table RPT__Transferencias_Detalles_20201210 
Go--#SQL 


	select * 
	Into RPT__Transferencias__20201210 
	from vw_TransferenciasEnc  
	where IdEstado = 11 and IdFarmacia in ( 5, 1005 ) 
		and convert(varchar(10), FechaTransferencia, 120) between '2019-01-01' and '2019-12-31' 
		and TipoTransferencia = 'TS' 

Go--#SQL 

--	sp_listacolumnas RPT__Transferencias__20201210 



	If Exists ( Select * from sysobjects (nolock) Where Name = 'RPT__Transferencias_Detalles_20201210' and xType = 'U' ) Drop Table RPT__Transferencias_Detalles_20201210 
Go--#SQL 

	select 
		--E.*, 
		E.IdEmpresa, E.Empresa, E.IdEstado, E.Estado, E.ClaveRenapo, E.IdFarmacia, E.Farmacia, E.IdJurisdiccion, E.Jurisdiccion, 
		E.EsAlmacen, E.EsTransferenciaAlmacen, 
		E.Folio, E.FolioTransferenciaRef, E.FolioMovtoInv, E.TipoTransferencia, E.DestinoEsAlmacen, E.FechaTransferencia, E.FechaReg, 
		E.IdPersonal, E.NombrePersonal, E.Observaciones, E.SubTotal, E.Iva, E.Total, E.Status, 
		E.IdEstadoRecibe, E.EstadoRecibe, E.ClaveRenapoRecibe, E.IdFarmaciaRecibe, E.FarmaciaRecibe, E.IdJurisdiccionRecibe, E.JurisdiccionRecibe, E.EsAlmacenRecibe, 
		E.StatusTransferencia, E.TransferenciaAplicada, E.IdPersonalRegistra, E.NombrePersonalRegistra, E.FechaAplicada, 
		
		D.IdSubFarmaciaEnvia, 
		D.IdProducto, D.CodigoEAN, P.ClaveSSA, D.ClaveLote, D.CantidadEnviada  
		--0 as Procesar 
		
	Into RPT__Transferencias_Detalles_20201210 
	--from RPT__Transferencias__20201210 E (NoLock) 
	from RPT__Transferencias__20201210 E (NoLock) 
	--Inner Join TransferenciasDet_Lotes_____20201210 D (NoLock) On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioTransferencia = D.FolioTransferencia )  
	Inner Join TransferenciasDet_Lotes_____20201210 D (NoLock) On ( E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.FolioTransferencia )  
	Inner Join vw_Productos_CodigoEAN P (NoLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )  
	Where E.TipoTransferencia ='TS'  
	and 
	( 
		( 
			P.ClaveSSA like '%010.000.0615%' 
			and 
			ClaveLote in 
			( 
				'DB01118', 'DB00119', 'C19E371', 'DB00319', 'DB00419', 'C19Y470', 'DB01119', 'C19U517', 'C18U219', 'C17T746', '113426' 
			) 
		) 
		or 
		(
			P.ClaveSSA like '%010.000.4148%' 
			and 
			ClaveLote in 
			( 
				'861023', '861023', '861023', '954325', '954325', '855755', '859622', '956653', '956653', '960695', 
				'859622', '953134', '953134', '957838', '954325', '957838', '953134', '660254', '761629', '858102', 
				'858345', '857188', '854165', '859622', '759285', '760357', '858345', '857304' 
			) 	 
		) 
	)

Go--#SQL 


	exec sp_FormatearTabla RPT__Transferencias_Detalles_20201210 

Go--#SQL 


	select * from RPT__Transferencias_Detalles_20201210 
	order by FechaTransferencia 

