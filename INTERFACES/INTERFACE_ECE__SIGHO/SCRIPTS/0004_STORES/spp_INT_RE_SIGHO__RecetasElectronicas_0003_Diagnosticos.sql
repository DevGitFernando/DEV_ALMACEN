-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_SIGHO__RecetasElectronicas_0003_Diagnosticos' and xType = 'P' ) 
   Drop Proc spp_INT_RE_SIGHO__RecetasElectronicas_0003_Diagnosticos
Go--#SQL 

Create Proc spp_INT_RE_SIGHO__RecetasElectronicas_0003_Diagnosticos 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@FolioReceta varchar(12) = '', 
	
	@CIE10 varchar(10) = '', 
	@DescripcionDiagnostico varchar(3000) = '',
	@Clues_Emisor Varchar(20) = ''			
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)
	
	
	Select @sFolio = Folio 
	From INT_RE_SIGHO__RecetasElectronicas_XML (NoLock)
	Where @IdEmpresa = IdEmpresa And @IdEstado = IdEstado And @IdFarmacia = IdFarmacia And Folio_SIADISSEP = @FolioReceta And  uMedica = @Clues_Emisor


	If Not Exists ( Select * From INT_RE_SIGHO__RecetasElectronicas_0003_Diagnosticos (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @sFolio 
						and CIE10 = @CIE10 ) 
	Begin 
		Insert Into INT_RE_SIGHO__RecetasElectronicas_0003_Diagnosticos 
		( 
			IdEmpresa, IdEstado, IdFarmacia, Folio, CIE10, DescripcionDiagnostico 
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @sFolio, @CIE10, @DescripcionDiagnostico 
	End 
	
End 
Go--#SQL 
	