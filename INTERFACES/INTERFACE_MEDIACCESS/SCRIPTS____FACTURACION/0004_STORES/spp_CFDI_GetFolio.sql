If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_CFDI_GetFolio' and xType = 'P' )
    Drop Proc spp_CFDI_GetFolio
Go--#SQL
  
Create Proc spp_CFDI_GetFolio 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0001', @Serie varchar(10) = 'Z' 
)
With Encryption 
As
Begin
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@iFolioGenerado int, 
	@iFolioFinal int   


--	Select * From FACT_CFD_SeriesFolios (NoLock) 
--	Select * From FACT_CFD_Sucursales_Series (NoLock) 
	Set @iFolioGenerado = 0 
	Set @iFolioFinal = 0 
	Set @sFolio = '' 
	
	If Exists 
		( 
			Select * 
			From CFDI_Emisores_SeriesFolios F (NoLock) 
			Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.Serie = @Serie 
		) 
	Begin 
		Update F Set FolioUtilizado = F.FolioUtilizado + 1
		From CFDI_Emisores_SeriesFolios F (NoLock) 
		Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.Serie = @Serie 
		
		Select @iFolioGenerado = F.FolioUtilizado, @iFolioFinal = F.FolioFinal 
		From CFDI_Emisores_SeriesFolios F (NoLock) 
		Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.Serie = @Serie 
	End 

--			spp_CFDI_GetFolio 

--		select * from FACT_CFD_SeriesFolios 	
	
--		sp_listacolumnas__stores spp_CFDI_GetFolio, 1


	Select cast(@iFolioGenerado as varchar) as FolioGenerado, cast(@iFolioFinal  as varchar) as FolioFinal 
	
End
Go--#SQL
