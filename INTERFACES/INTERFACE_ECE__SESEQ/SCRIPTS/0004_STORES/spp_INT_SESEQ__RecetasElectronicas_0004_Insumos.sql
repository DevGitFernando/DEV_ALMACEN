-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__RecetasElectronicas_0004_Insumos' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__RecetasElectronicas_0004_Insumos
Go--#SQL 

Create Proc spp_INT_SESEQ__RecetasElectronicas_0004_Insumos 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(2) = '', 
	@IdFarmacia varchar(4) = '', 	
	@Folio varchar(12) = '', 
	
	@ClaveSSA varchar(50) = '', 
	@CantidadRequerida int = 0, 
	@TipoDeInsumo int = 0, 
	@RecepcionDuplicada int = 0, 
	@ClavePrograma varchar(10) = '0' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@sMensaje varchar(200)  


	If Not Exists ( Select * From INT_SESEQ__RecetasElectronicas_0004_Insumos (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 
						and ClaveSSA = @ClaveSSA and TipoDeInsumo = TipoDeInsumo ) 
		Begin 
			Insert Into INT_SESEQ__RecetasElectronicas_0004_Insumos 
			( 
				IdEmpresa, IdEstado, IdFarmacia, Folio, ClaveSSA, CantidadRequerida, CantidadEntregada, EmitioVale, CantidadVale, TipoDeInsumo, RecepcionDuplicada, ClavePrograma  
			) 
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, @Folio, @ClaveSSA, @CantidadRequerida,  
				0 as CantidadEntregada, 0 as EmitioVale, 0 as CantidadVale, @TipoDeInsumo, @RecepcionDuplicada, @ClavePrograma    
		End 
	Else 
		Begin 
			Update C Set CantidadRequerida = C.CantidadRequerida + @TipoDeInsumo 
			From INT_SESEQ__RecetasElectronicas_0004_Insumos C (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Folio = @Folio 
				and ClaveSSA = @ClaveSSA and TipoDeInsumo = TipoDeInsumo
		End 
	
End 
Go--#SQL 

	