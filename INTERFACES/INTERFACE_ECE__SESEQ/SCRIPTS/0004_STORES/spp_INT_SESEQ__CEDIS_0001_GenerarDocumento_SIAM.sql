-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM' and xType = 'P' ) 
   Drop Proc spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM
Go--#SQL 

Create Proc spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM 
( 
	@IdEmpresa varchar(3) = '4', 
	@IdEstado varchar(2) = '22', 
	@IdFarmacia varchar(4) = '104',  
	@Folio varchar(8) = '000191', 
	@TipoDeDocumento int = 1,  
	@TipoDeProceso int = 2  
) 
With Encryption 
As 
Begin 
--Set NoCount On 
Set DateFormat YMD 

	
	If @TipoDeDocumento = 1 
	Begin 
		Exec spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM__01_Ventas 
			@IdEmpresa =  @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @Folio = @Folio, @TipoDeProceso = @TipoDeProceso 
	End 

	If @TipoDeDocumento = 2 
	Begin 
		Exec spp_INT_SESEQ__CEDIS_0001_GenerarDocumento_SIAM__02_Transferencias 
			@IdEmpresa =  @IdEmpresa, @IdEstado = @IdEstado, @IdFarmacia = @IdFarmacia, @Folio = @Folio, @TipoDeProceso = @TipoDeProceso 
	End 

End 
Go--#SQL 

	