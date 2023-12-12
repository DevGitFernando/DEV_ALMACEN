-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SIADISSEP__RecetasElectronicas_0003_Diagnosticos' and xType = 'P' ) 
   Drop Proc spp_INT_SIADISSEP__RecetasElectronicas_0003_Diagnosticos
Go--#SQL 

Create Proc spp_INT_SIADISSEP__RecetasElectronicas_0003_Diagnosticos 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@Folio varchar(12) = '', 
	
	@CIE10 varchar(10) = '', 
	@DescripcionDiagnostico varchar(3000) = '' 
		
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


	If Not Exists ( Select * From INT_SIADISSEP__RecetasElectronicas_0003_Diagnosticos (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 
						and CIE10 = @CIE10 ) 
	Begin 
		Insert Into INT_SIADISSEP__RecetasElectronicas_0003_Diagnosticos 
		( 
			IdEmpresa, IdEstado, IdFarmacia, Folio, CIE10, DescripcionDiagnostico 
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @CIE10, @DescripcionDiagnostico 
	End 
	
End 
Go--#SQL 
	