---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_INT_SESEQ__Farmacias_CentrosDeCostos' and xType = 'V' ) 
	Drop view vw_INT_SESEQ__Farmacias_CentrosDeCostos 
Go--#SQL  

Create View vw_INT_SESEQ__Farmacias_CentrosDeCostos 
As 
	select  
		I.IdEmpresa, 
		I.IdEstado, F.Estado, I.IdFarmacia, F.Farmacia, 
		I.Referencia_SESEQ, I.URL_Interface, I.CapturaInformacion, 
		I.Referencia_SESEQ_CentroDeCostos, I.Referencia_SESEQ_CCC, 
		CC.Nombre as NombreDelCentroDeCostos, 
		
		I.URL_InformacionOperacion 
	From INT_SESEQ__CFG_Farmacias_UMedicas I (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( I.IdEstado = F.IdEstado and I.IdFarmacia = F.IdFarmacia ) 
	Inner Join INT_SESEQ__CentrosDeCostos CC (NoLock) On ( I.Referencia_SESEQ_CentroDeCostos = CC.Clave ) 

Go--#SQL 

--	sp_listacolumnas INT_SESEQ__CFG_Farmacias_UMedicas 

--	select * from INT_SESEQ__CentrosDeCostos 

	--select 
	--	Referencia_SESEQ_CentroDeCostos, 
	--	NombreDelCentroDeCostos, 
	--	Referencia_SESEQ_CCC, 
	--	Estado, IdFarmacia, Farmacia 
		
	--from vw_INT_SESEQ__Farmacias_CentrosDeCostos 
	--order by cast(Referencia_SESEQ_CentroDeCostos as int) 

