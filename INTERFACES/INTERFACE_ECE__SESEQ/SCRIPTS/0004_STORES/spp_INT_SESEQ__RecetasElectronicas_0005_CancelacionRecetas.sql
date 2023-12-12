-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@Folio varchar(12) = '', 
	
	@FolioReceta varchar(50) = '', 
	@FechaReceta varchar(10) = '', 
	@FechaEnvioReceta varchar(10) = '', 
	@Expediente varchar(50) = '' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


	If Not Exists ( Select * From INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 
						and FolioReceta = @FolioReceta ) 
	Begin 
		Insert Into INT_SESEQ__RecetasElectronicas_0005_CancelacionRecetas 
		( 
			IdEmpresa, IdEstado, IdFarmacia, Folio, FolioReceta, FechaReceta, FechaEnvioReceta, Expediente 
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @FolioReceta, @FechaReceta, @FechaEnvioReceta, @Expediente
	End 
	
End 
Go--#SQL 
	