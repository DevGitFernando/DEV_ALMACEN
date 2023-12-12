---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFDI_NotasDeCredito_DoctosRelacionados' and xType = 'P' )
   Drop Proc spp_Mtto_FACT_CFDI_NotasDeCredito_DoctosRelacionados 
Go--#SQL 

--	select * from FACT_CFD_Documentos_Generados

Create Proc spp_Mtto_FACT_CFDI_NotasDeCredito_DoctosRelacionados
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(10) = '', @Folio int = 0,
	@Serie_Relacionada varchar(10) = '', 
	@Folio_Relacionado int = 0, 	
	@UUID_Relacionado varchar(50) = '', 

	@Total_Base numeric(14,4) = 0,	---- Importe Original del CFDI  

	@SubTotal numeric(14,4) = 0, 
	@IVA numeric(14,4) = 0, 
	@Total numeric(14,4) = 0 
) 
As 
Begin 
Set NoCount On 

	
	If Not Exists 
	( 
		Select * 
		From FACT_CFDI_NotasDeCredito_DoctosRelacionados (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			  and Serie = @Serie and Folio = @Folio and Serie_Relacionada = @Serie_Relacionada and Folio_Relacionado = @Folio_Relacionado 
	) 
	Begin 
		Insert Into FACT_CFDI_NotasDeCredito_DoctosRelacionados
				( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Serie_Relacionada, Folio_Relacionado, UUID_Relacionado, Total_Base, SubTotal, IVA, Total ) 
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @Serie_Relacionada, @Folio_Relacionado, @UUID_Relacionado, @Total_Base, @SubTotal, @IVA, @Total 
	End 
	Else 
	Begin 
		Update D Set SubTotal = SubTotal + @SubTotal, IVA = IVA + @IVA, Total = Total + @Total 
		From FACT_CFDI_NotasDeCredito_DoctosRelacionados D (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			  and Serie = @Serie and Folio = @Folio and Serie_Relacionada = @Serie_Relacionada and Folio_Relacionado = @Folio_Relacionado 
	End 


End 
Go--#SQL

---------------------------------------------------------------------------------------------------------------------------
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP' and xType = 'P' )
   Drop Proc spp_Mtto_FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP 
Go--#SQL 

--	select * from FACT_CFD_Documentos_Generados

Create Proc spp_Mtto_FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@Serie varchar(10) = '', @Folio int = 0,
	@Serie_Relacionada varchar(10) = '', 
	@Folio_Relacionado int = 0, 	
	@UUID_Relacionado varchar(50) = '', 

	@Total_Base numeric(14,4) = 0,	---- Importe Original del CFDI  

	@SubTotal numeric(14,4) = 0, 
	@IVA numeric(14,4) = 0, 
	@Total numeric(14,4) = 0 
) 
As 
Begin 
Set NoCount On 
	
	If Not Exists 
	( 
		Select * 
		From FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			  and Serie = @Serie and Folio = @Folio and Serie_Relacionada = @Serie_Relacionada and Folio_Relacionado = @Folio_Relacionado 
	) 
	Begin 
		Insert Into FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP
				( IdEmpresa, IdEstado, IdFarmacia, Serie, Folio, Serie_Relacionada, Folio_Relacionado, UUID_Relacionado, Total_Base, SubTotal, IVA, Total ) 
		Select @IdEmpresa, @IdEstado, @IdFarmacia, @Serie, @Folio, @Serie_Relacionada, @Folio_Relacionado, @UUID_Relacionado, @Total_Base, @SubTotal, @IVA, @Total 
	End 
	Else 
	Begin 
		Update D Set SubTotal = SubTotal + @SubTotal, IVA = IVA + @IVA, Total = Total + @Total 
		From FACT_CFDI_NotasDeCredito_DoctosRelacionados_VP D (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			  and Serie = @Serie and Folio = @Folio and Serie_Relacionada = @Serie_Relacionada and Folio_Relacionado = @Folio_Relacionado 
	End 


End 
Go--#SQL