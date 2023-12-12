


If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_Contrarecibos_Detalles' and xType = 'P')
    Drop Proc spp_Mtto_FACT_Contrarecibos_Detalles
Go--#SQL

  
Create Proc spp_Mtto_FACT_Contrarecibos_Detalles
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioContrarecibo varchar(12), @FolioFactura varchar(12) 
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sStatus varchar(1), 
		@iActualizado smallint	

	Set @sStatus = 'A'
	Set @iActualizado = 0	

	If Not Exists ( Select * From FACT_Contrarecibos_Detalles (NoLock) 
				   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
					And FolioContrarecibo = @FolioContrarecibo and FolioFactura = @FolioFactura ) 
	Begin 
		Insert Into FACT_Contrarecibos_Detalles ( IdEmpresa, IdEstado, IdFarmacia, FolioContrarecibo, FolioFactura, Status, Actualizado ) 
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioContrarecibo, @FolioFactura, @sStatus, @iActualizado 
	End     

	Update FACT_Facturas Set EstaEnCobro = 1
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia and FolioFactura = @FolioFactura
	
End
Go--#SQL 	
