



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Ctl_CierresDePeriodos_Farmacia' and xType = 'P' ) 
Drop Proc spp_Rpt_Ctl_CierresDePeriodos_Farmacia
 
Go--#SQL

Create Proc spp_Rpt_Ctl_CierresDePeriodos_Farmacia 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0008', 
	@FolioCierre int = 1
)
with Encryption 
As 
Begin
Set NoCount On 
	

		Select V.IdEmpresa, E.Nombre as Empresa, V.IdEstado, F.Estado as Estado, V.IdFarmacia, F.Farmacia as Farmacia, 
		V.FolioVenta, V.FolioCierre, C.FechaInicial, C.FechaFinal, GetDate() As FechaImpresion, V.FechaRegistro As FechaTicket,
		C.IdPersonal, vP.NombreCompleto as NombrePersonal 
		From VentasEnc V (Nolock)
		Inner Join Ctl_CierresDePeriodos C (Nolock)
			On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioCierre = C.FolioCierre )
		Inner Join CatEmpresas E (NoLock) 
			On ( V.IdEmpresa = E.IdEmpresa )	
		Inner Join vw_Farmacias F (NoLock) 
			On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia )
		Inner Join vw_Personal vP (NoLock) 
			On ( C.IdEstado = vP.IdEstado and C.IdFarmacia = vP.IdFarmacia and C.IdPersonal = vP.IdPersonal )		
		Where V.IdEmpresa = @IdEmpresa and V.IdEstado = @IdEstado and V.IdFarmacia = @IdFarmacia and V.FolioCierre = @FolioCierre
		Order By V.FolioVenta
		

End 
Go--#SQL
   
