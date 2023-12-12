If Exists ( Select Name From sysobjects (NoLock) Where Name = 'spp_Mtto_CFDI_Emisores_SeriesFolios_Series' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFDI_Emisores_SeriesFolios_Series 
Go--#SQL 

Create Proc spp_Mtto_CFDI_Emisores_SeriesFolios_Series 
( 
	@IdEmpresa varchar(4) = '', 
	@IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(10) = '', 
	@IdTipoDocumento varchar(100) = '', @FolioInicial varchar(10) = '', @FolioFinal varchar(10) = '', @FolioUtilizado varchar(10) = '', 
	@Status varchar(1) = 'A'
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@iTipo int,
	@Mensaje varchar(200) 	

	If @Status = 'A' 
	Begin 
		If Not Exists ( Select * From CFDI_Emisores_SeriesFolios (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
					and Serie = @Serie and IdTipoDocumento = @IdTipoDocumento ) 
			Begin 
				Insert Into CFDI_Emisores_SeriesFolios ( IdEmpresa, IdEstado, IdFarmacia, Serie, IdTipoDocumento, FolioInicial, FolioFinal, FolioUtilizado, Status ) 
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @IdTipoDocumento, @FolioInicial, @FolioFinal, @FolioUtilizado, @Status 					
			End 
		Else 
			Begin 
				Update C Set Status = 'A' 
				From CFDI_Emisores_SeriesFolios C (NoLock)  
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
					and Serie = @Serie and IdTipoDocumento = @IdTipoDocumento 
			End 
			
	End 

	If @Status = 'C' 
	Begin 
		Update C Set Status = 'C' 
		From CFDI_Emisores_SeriesFolios C (NoLock)  
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and Serie = @Serie and IdTipoDocumento = @IdTipoDocumento 
	End 
	
		
End 
Go--#SQL 
   
--		sp_listacolumnas CFDI_Emisores_SeriesFolios 

--		sp_listacolumnas__Stores spp_Mtto_CFDI_Emisores_SeriesFolios_Series , 1 
 
	