-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_AMPM__RecetasElectronicas_0003_Diagnosticos' and xType = 'P' ) 
   Drop Proc spp_INT_AMPM__RecetasElectronicas_0003_Diagnosticos
Go--#SQL 

Create Proc spp_INT_AMPM__RecetasElectronicas_0003_Diagnosticos
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
	--@sFolio varchar(20), 
	@sMensaje varchar(200)
	
	--Select @sFolio = Folio 
	--From INT_RE_INTERMED__RecetasElectronicas_XML (NoLock)
	--Where @IdEmpresa = IdEmpresa And @IdEstado = IdEstado And @IdFarmacia = IdFarmacia And Folio_SIADISSEP = @FolioReceta And  uMedica = @Clues_Emisor


	If Not Exists ( Select * From INT_AMPM__RecetasElectronicas_0003_Diagnosticos (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio And CIE10 = @CIE10) 
	Begin 
		Insert Into INT_AMPM__RecetasElectronicas_0003_Diagnosticos
		( 
			IdEmpresa, IdEstado, IdFarmacia, Folio, CIE10, DescripcionDiagnostico		
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @CIE10, @DescripcionDiagnostico	
	End 
	
End 
Go--#SQL 
	