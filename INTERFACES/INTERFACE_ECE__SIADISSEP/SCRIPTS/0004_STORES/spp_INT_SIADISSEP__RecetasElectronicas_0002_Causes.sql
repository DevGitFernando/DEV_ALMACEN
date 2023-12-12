-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SIADISSEP__RecetasElectronicas_0002_Causes' and xType = 'P' ) 
   Drop Proc spp_INT_SIADISSEP__RecetasElectronicas_0002_Causes
Go--#SQL 

Create Proc spp_INT_SIADISSEP__RecetasElectronicas_0002_Causes 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@Folio varchar(12) = '', 
	
	@NoIntervencionCause varchar(10) = '' 
		
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


	If Not Exists ( Select * From INT_SIADISSEP__RecetasElectronicas_0002_Causes (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 
						and NoIntervencionCause = @NoIntervencionCause ) 
	Begin 
		Insert Into INT_SIADISSEP__RecetasElectronicas_0002_Causes 
		( 
			IdEmpresa, IdEstado, IdFarmacia, Folio, NoIntervencionCause 
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @NoIntervencionCause 
	End 
	
End 
Go--#SQL 
	