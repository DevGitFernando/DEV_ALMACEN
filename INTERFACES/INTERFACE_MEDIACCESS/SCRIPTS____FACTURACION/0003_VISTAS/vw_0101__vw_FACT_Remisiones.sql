----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_FACT_Remisiones' and xType = 'V' ) 
	Drop View vw_FACT_Remisiones 
Go--#SQL 

Create View vw_FACT_Remisiones 
With Encryption 
As 

	Select 
		F.IdEmpresa, F.IdEstadoGenera as IdEstado, F.IdFarmaciaGenera as IdFarmacia, F.FolioRemision, 
		F.IdCliente, C.NombreCliente As Cliente, F.IdSubCliente, C.NombreSubCliente As SubCliente,
		F.IdPrograma, P.Programa, F.IdSubPrograma, P.SubPrograma, 
		F.IdEstado as IdEstado_Remision, L.Estado as Estado_Remision, 
		F.IdFarmacia as IdFarmacia_Remision, L.Farmacia as Farmacia_Remision, 
		F.FechaRemision as FechaRemision, 
		F.EsFacturable, (case when F.EsFacturable = 1 Then 'SI' else 'NO' end) as FacturableDescripcion, 
		F.TipoDeRemision, 
		(
			Upper(
			case when F.TipoDeRemision = 0 then 'Sin especificar' 
				else 
					case when F.TipoDeRemision = 1 then 'Insumos' else 'Administracion' end 
			end) 
		) as TipoDeRemisionDesc, 
		 
		F.TipoInsumo, 
		(
			Upper(
			case when F.TipoDeRemision = 0 then 'Sin especificar' 
				else 
					case when F.TipoInsumo = 1 then 'Material de curación' else 'Medicamento' end 
			end) 
		) as TipoDeInsumoDesc, 		
		F.IdPersonalRemision, F.SubTotalSinGrabar, F.SubTotalGrabado, F.Iva, F.Total, 
		F.Observaciones, F.Observaciones as ObservacionesRemision,
		F.Status,
		(
			Upper(
			case when F.Status = 'A' then 'Activa' 
				else 
					case when F.Status = 'C' then 'Cancelada' else 'Otro' end 
			end) 
		) as StatusDesc
	From FACT_Remisiones F (NoLock) 
	Inner Join vw_Farmacias L (NoLock) On ( F.Idestado = L.IdEstado and F.IdFarmacia = L.IdFarmacia )
	inner Join vw_Clientes_SubClientes C (NoLock) On ( F.IdEstadoGenera = C.IdEstado And F.IdCliente = C.IdCliente And F.IdSubCliente = C.IdSubCliente)
	Inner Join vw_Programas_SubProgramas P (NoLock) On ( F.IdPrograma = P.IdPrograma and F.IdSubPrograma = P.IdSubPrograma )  


Go--#SQL 

Select *  From vw_FACT_Remisiones (NoLock)  Where IdEmpresa = '002' and IdEstado = '09' and IdFarmacia = '0001' and FolioRemision = '0000000163'  

select * from vw_FACT_Facturas_Remisiones 

--		select * 	from vw_FACT_Remisiones 
	
--	select * from FACT_Remisiones 

	
	