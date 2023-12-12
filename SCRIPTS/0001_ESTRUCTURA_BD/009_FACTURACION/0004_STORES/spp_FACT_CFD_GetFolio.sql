
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_CFD_GetFolio' and xType = 'P' )
    Drop Proc spp_FACT_CFD_GetFolio
Go--#SQL
  
Create Proc spp_FACT_CFD_GetFolio 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '0001',  @Serie varchar(10) = 'Z', 
	@Identificador_UUID varchar(500) = '', @Incrementar bit = 1    
)
With Encryption 
As
Begin
Set NoCount On 
Declare 
	@sFolio varchar(20), 
	@iFolioGenerado int, 
	@iFolioFinal int, 
	@iFactor int, 
	@IdentificadorSerie int 


--	Select * From FACT_CFD_SeriesFolios (NoLock) 
--	Select * From FACT_CFD_Sucursales_Series (NoLock) 
	Set @iFolioGenerado = 0 
	Set @iFolioFinal = 0 
	Set @sFolio = '' 
	Set @IdentificadorSerie = 0 
	Set @iFactor = 1 
	if @Incrementar = 0 
	   Set @iFactor = 0 

----------------------------------------------- Generar la tabla para Timbrado Masivo 
	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FACT_CFDI_TM__03__FolioElectronico' and xType = 'U' ) 
	Begin 
		Select 
			S.IdEmpresa, S.IdEstado, S.IdFarmacia, F.Serie, S.IdentificadorSerie, F.FolioUtilizado as Folio, 
			@Identificador_UUID as Identificador_UUID, getdate() as FechaProcesamiento 
		Into FACT_CFDI_TM__03__FolioElectronico 
		From FACT_CFD_SeriesFolios F (NoLock) 
		Inner Join FACT_CFD_Sucursales_Series S (NoLock) On ( F.IdEmpresa = S.IdEmpresa and F.IdentificadorSerie = S.IdentificadorSerie)  
		Where 1 = 0 
		--Where S.IdEmpresa = @IdEmpresa and S.IdEstado = @IdEstado and S.IdFarmacia = @IdFarmacia and F.Serie = @Serie 

	End 
----------------------------------------------- Generar la tabla para Timbrado Masivo 


	If Exists 
		( 
			Select * 
			From FACT_CFD_SeriesFolios F (NoLock) 
			Inner Join FACT_CFD_Sucursales_Series S (NoLock) On ( F.IdEmpresa = S.IdEmpresa and F.IdentificadorSerie = S.IdentificadorSerie)  
			Where S.IdEmpresa = @IdEmpresa and S.IdEstado = @IdEstado and S.IdFarmacia = @IdFarmacia and F.Serie = @Serie 
		) 
	Begin 
		Update F Set FolioUtilizado = F.FolioUtilizado + @iFactor 
		From FACT_CFD_SeriesFolios F (NoLock) 
		Inner Join FACT_CFD_Sucursales_Series S (NoLock) On ( F.IdEmpresa = S.IdEmpresa and F.IdentificadorSerie = S.IdentificadorSerie )  
		Where S.IdEmpresa = @IdEmpresa and S.IdEstado = @IdEstado and S.IdFarmacia = @IdFarmacia and F.Serie = @Serie 				
		
		Select @iFolioGenerado = F.FolioUtilizado, @iFolioFinal = F.FolioFinal, @IdentificadorSerie =  S.IdentificadorSerie  
		From FACT_CFD_SeriesFolios F (NoLock) 
		Inner Join FACT_CFD_Sucursales_Series S (NoLock) On ( F.IdEmpresa = S.IdEmpresa and F.IdentificadorSerie = S.IdentificadorSerie )   
		Where S.IdEmpresa = @IdEmpresa and S.IdEstado = @IdEstado and S.IdFarmacia = @IdFarmacia and F.Serie = @Serie 
		
	End 


--			spp_FACT_CFD_GetFolio 

--		select * from FACT_CFD_SeriesFolios 	
	
--		sp_listacolumnas__stores spp_FACT_CFD_GetFolio, 1

	If @Identificador_UUID = '' 
		Begin 
			Select @iFolioGenerado as FolioGenerado, @iFolioFinal as FolioFinal 
		End 
	Else 
		Begin 
			Delete From FACT_CFDI_TM__03__FolioElectronico Where Identificador_UUID = @Identificador_UUID 

			Insert Into FACT_CFDI_TM__03__FolioElectronico ( IdEmpresa, IdEstado, IdFarmacia, Serie, IdentificadorSerie, Folio, Identificador_UUID, FechaProcesamiento ) 
			Select 
				@IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, 
				@Serie as Serie, @IdentificadorSerie as IdentificadorSerie, @iFolioGenerado as Folio, 
				@Identificador_UUID as Identificador_UUID, getdate() as FechaProcesamiento  
		End 

End 
Go--#SQL

