-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_RE_SIGHO__RecetasElectronicas_0004_Insumos' and xType = 'P' ) 
   Drop Proc spp_INT_RE_SIGHO__RecetasElectronicas_0004_Insumos
Go--#SQL 

Create Proc spp_INT_RE_SIGHO__RecetasElectronicas_0004_Insumos 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@FolioReceta varchar(12) = '', 
	
	@ClaveSSA varchar(50) = '', 
	@CantidadRequerida int = 0,
	@CantidadEntregada int = 0,
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
	
	Select @ClaveSSA = ClaveSSA From CatClavesSSA_Sales Where IdClaveSSA_Sal = @ClaveSSA


	If Not Exists ( Select * From INT_RE_SIGHO__RecetasElectronicas_0004_Insumos (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and  Folio = @sFolio and ClaveSSA = @ClaveSSA ) 
		Begin 
			Insert Into INT_RE_SIGHO__RecetasElectronicas_0004_Insumos 
			( 
				IdEmpresa, IdEstado, IdFarmacia, Folio, ClaveSSA, CantidadRequerida, CantidadEntregada, EmitioVale, CantidadVale 
			) 
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, @sFolio, @ClaveSSA, @CantidadRequerida, 
				@CantidadEntregada, 0 as EmitioVale, 0 as CantidadVale 
		End
	Else
		Begin
			Update INT_RE_SIGHO__RecetasElectronicas_0004_Insumos Set CantidadEntregada = @CantidadEntregada
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and  Folio = @sFolio and ClaveSSA = @ClaveSSA
		End 
	
End 
Go--#SQL 
	