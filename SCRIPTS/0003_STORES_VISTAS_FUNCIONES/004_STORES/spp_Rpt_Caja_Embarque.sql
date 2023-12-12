
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Caja_Embarque' and xType = 'P' ) 
   Drop Proc spp_Rpt_Caja_Embarque 
Go--#SQL   

Create Proc spp_Rpt_Caja_Embarque
( 
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0005', @Folioreferencia varchar(20) = 'TS00026697'
)  
With Encryption 
As 
Begin 
Set NoCount On

	--Declare
	--	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11',
	--	@IdFarmacia varchar(4) = '0005', @Folioreferencia varchar(20) = 'TS00026697'


	Select E.IdEmpresa, M.Nombre As Empresa, E.IdEstado, F.Estado, E.IdFarmacia, F.Farmacia, 
		   D.CodigoEAN, D.ClaveSSA, D.ClaveLote, D.IdCaja, D.CantidadAsignada
	From Pedidos_Cedis_Enc_Surtido E (NoLock)
	Inner Join Pedidos_Cedis_Det_Surtido_Distribucion D (NoLock)
		On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioSurtido = D.FolioSurtido)
	Inner Join CatEmpresas M (NoLock) On (M.IdEmpresa = E.IdEmpresa)
	Inner Join vw_Farmacias F (NoLock) On (E.IdEstado = F.IdEstado And E.IdFarmacia = F.IdFarmacia)
	Where D.CantidadAsignada > 0
		And E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia
		And FolioTransferenciaReferencia = @Folioreferencia
	Order By D.ClaveSSA, D.CodigoEAN, D.ClaveLote, D.IdCaja 
    
End 
Go--#SQL