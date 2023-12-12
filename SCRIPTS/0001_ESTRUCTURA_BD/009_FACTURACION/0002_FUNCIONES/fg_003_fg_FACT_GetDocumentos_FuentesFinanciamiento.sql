----------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'fg_FACT_GetDocumentos_FuentesFinanciamiento' and xType = 'TF' )
   Drop Function fg_FACT_GetDocumentos_FuentesFinanciamiento  
Go--#SQL     
      
Create Function dbo.fg_FACT_GetDocumentos_FuentesFinanciamiento
(
	@NoRelacionar int = 0 
--	@IdFuenteFinanciamiento varchar(4) = '', @IdFinanciamiento varchar(4) = '', @IdDocumento varchar(4) = ''  
)  
returns @Tabla Table 
( 
	IdFuenteFinanciamiento varchar(4), 
	IdFinanciamiento varchar(4), 
	IdDocumento varchar(4), 
	NombreDocumento varchar(max), 

	IdFuenteFinanciamiento_Relacionado varchar(4), 
	IdFinanciamiento_Relacionado varchar(4), 
	IdDocumento_Relacionado varchar(4), 
	NombreDocumento_Relacionado varchar(max), 
		
	EsRelacionado int, 
	OrigenDeInsumo int,			-- 0 ==> General | 1 ==> Venta | 2 ==> Consigna 
	TipoDeDocumento int,		-- 1 ==> Producto | 2 ==> Servicio 
	TipoDeInsumo int,			-- 0 ==> General | 1 ==> Medicamento | 2 ==> Material de curacion 
	ValorNominal numeric(14,4),   
	ImporteAplicado numeric(14,4), 
	ImporteRestante numeric(14,4), 
	AplicaFarmacia bit, 
	AplicaAlmacen bit, 
	EsProgramaEspecial bit, 
	TipoDeBeneficiario int,		-- 0 ==> Todos | 1 ==> General <Solo farmacia> | 2 ==> Hospitales <Solo almacén> | 3 ==> Jurisdicciones <Solo almacén> 
	Status varchar(1)   

) 
With Encryption 
As 
Begin 
Declare 
	@sSql varchar(max) 

	Set @sSql = '' 


	Insert Into @Tabla 
	Select 
		IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, NombreDocumento, 
		IdFuenteFinanciamiento_Relacionado, IdFinanciamiento_Relacionado, IdDocumento_Relacionado, '' as NombreDocumento_Relacionado, 

		EsRelacionado, OrigenDeInsumo, TipoDeDocumento, TipoDeInsumo, 	
		ValorNominal, ImporteAplicado, (ValorNominal - ImporteAplicado) as ImporteRestante, AplicaFarmacia, AplicaAlmacen, EsProgramaEspecial, TipoDeBeneficiario, Status  
	From FACT_Fuentes_De_Financiamiento_Detalles_Documentos 


	Update T Set 
		IdFuenteFinanciamiento_Relacionado = IdFuenteFinanciamiento, 
		IdFinanciamiento_Relacionado = IdFinanciamiento, 
		IdDocumento_Relacionado = IdDocumento, NombreDocumento_Relacionado = NombreDocumento 
	From @Tabla T 
	Where EsRelacionado = 0 


	Update DF Set 
		NombreDocumento_Relacionado = GF.NombreDocumento,  
		-- OrigenDeInsumo = GF.OrigenDeInsumo, 
		TipoDeDocumento = GF.TipoDeDocumento, TipoDeInsumo = GF.TipoDeInsumo, 	
		ValorNominal = GF.ValorNominal, ImporteAplicado = GF.ImporteAplicado, ImporteRestante = Gf.ImporteRestante, 
		AplicaFarmacia = GF.AplicaFarmacia, AplicaAlmacen = GF.AplicaAlmacen, EsProgramaEspecial = GF.EsProgramaEspecial, 
		TipoDeBeneficiario = GF.TipoDeBeneficiario, Status = GF.Status 
	From @Tabla DF 
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_Documentos GF (NoLock) 
		On ( DF.IdFuenteFinanciamiento_Relacionado = GF.IdFuenteFinanciamiento and DF.IdFinanciamiento_Relacionado  = GF.IdFinanciamiento and DF.IdDocumento_Relacionado  = GF.IdDocumento )
	Where DF.EsRelacionado = 1  


	If @NoRelacionar <> 0 
	Begin 
		Update DF Set 
			ValorNominal = 0, ImporteAplicado = 0, ImporteRestante = 0  
		From @Tabla DF 
		Inner Join FACT_Fuentes_De_Financiamiento_Detalles_Documentos GF (NoLock) 
			On ( DF.IdFuenteFinanciamiento_Relacionado = GF.IdFuenteFinanciamiento and DF.IdFinanciamiento_Relacionado  = GF.IdFinanciamiento and DF.IdDocumento_Relacionado  = GF.IdDocumento )
		Where DF.EsRelacionado = 1  
	End 

	------------ Regresar el resultado 
    return 
          
End 
Go--#SQL 


