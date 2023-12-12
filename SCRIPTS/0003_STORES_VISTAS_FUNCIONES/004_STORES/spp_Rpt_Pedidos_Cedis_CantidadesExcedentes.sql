If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Pedidos_Cedis_CantidadesExcedentes' and xType = 'P' ) 
   Drop Proc spp_Rpt_Pedidos_Cedis_CantidadesExcedentes
Go--#SQL   

Create Proc spp_Rpt_Pedidos_Cedis_CantidadesExcedentes
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005', 
	@IdFarmaciaPedido varchar(4) = '0012', @FolioPedido varchar(6) = '000004'
) 
With Encryption 
As 
Begin 
Set NoCount On

	Select
		IdEmpresa, IdEstado, IdFarmacia,
		P.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, P.Cantidad As CantidadSolicitada, P.Existencia, ExistenciaSugerida, 
		(Case
			When Existencia > ExistenciaSugerida Then 0
			Else (P.ExistenciaSugerida - P.Existencia) 
		 End) as CantidadSugerida, 
		 0 as CantidadSurtida
	Into #Temp_Detalle
	From Pedidos_Cedis_Det P (NoLock) 
	Inner Join 	vw_ClavesSSA_Sales C (NoLock) On ( P.IdClaveSSA = C.IdClaveSSA_Sal )
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmaciaPedido and P.FolioPedido = @FolioPedido


	Select D.Status, IdClaveSSA, sum(CantidadAsignada) as CantidadSurtida 
	Into #tmpDetalle_Surtido_Detallado  	
	From Pedidos_Cedis_Enc_Surtido P (NoLock) 
	Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
		On ( P.IdEmpresa = D.IdEmpresa and P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia and P.FolioSurtido = D.FolioSurtido ) 
	Where P.IdEmpresa = @IdEmpresa and P.IdEstado = @IdEstado and P.IdFarmacia = @IdFarmacia 
		  and P.IdFarmaciaPedido = @IdFarmaciaPedido and P.FolioPedido = @FolioPedido 	
	Group by D.Status, D.IdClaveSSA
	
	
	--Select *
	Update D Set D.CantidadSurtida = S.CantidadSurtida
	From #Temp_Detalle D
	Inner Join #tmpDetalle_Surtido_Detallado S (NoLock) On (D.IdClaveSSA = S.IdClaveSSA)
	
	Select
		@FolioPedido As FolioPedido, D.IdEmpresa, E.Nombre As Empresa, F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave,
		CantidadSugerida, CantidadSolicitada, CantidadSurtida
	From #Temp_Detalle D
	Inner Join CatEmpresas E (NoLock) On (D.IdEmpresa = E.IdEmpresa)
	Inner Join vw_Farmacias F (NoLock) On (D.IdEstado = F.IdEstado And D.IdFarmacia = F.IdFarmacia)
	Where CantidadSugerida < CantidadSolicitada
	
End 
Go--#SQL 