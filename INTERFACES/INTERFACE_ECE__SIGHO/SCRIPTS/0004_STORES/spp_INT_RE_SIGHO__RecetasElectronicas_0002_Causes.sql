-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_SIGHO__RecetasElectronicas_0002_Causes' and xType = 'P' ) 
   Drop Proc spp_INT_RE_SIGHO__RecetasElectronicas_0002_Causes
Go--#SQL 

Create Proc spp_INT_RE_SIGHO__RecetasElectronicas_0002_Causes 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@FolioReceta varchar(12) = '', 
	
	@NoIntervencionCause varchar(10) = '',
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


	If Not Exists ( Select * From INT_RE_SIGHO__RecetasElectronicas_0002_Causes (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @sFolio 
						and NoIntervencionCause = @NoIntervencionCause ) 
	Begin 
		Insert Into INT_RE_SIGHO__RecetasElectronicas_0002_Causes 
		( 
			IdEmpresa, IdEstado, IdFarmacia, Folio, NoIntervencionCause 
		) 
		Select 
			@IdEmpresa, @IdEstado, @IdFarmacia, @sFolio, @NoIntervencionCause 
	End 
	
End 
Go--#SQL 
	