
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_3_Documentos' and xType = 'P' ) 
   Drop Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_3_Documentos 
Go--#SQL 

Create Proc sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_3_Documentos
With Encryption 
As 
Begin 
Set NoCount On 
 Set Ansi_Warnings Off  --- Especial, peligroso 


	Select distinct IdFuenteFinanciamiento, IdFinanciamiento
	Into #Financiamientos
	From FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva

	--Cancelar Las Anteriores
	Update T Set Status = 'C'
	From FACT_Fuentes_De_Financiamiento_Detalles_Documentos T
	Inner Join #Financiamientos F (NoLock) On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento)

	--Actulizar las existentes
	Update T
	Set 
		NombreDocumento = F.NombreDocumento, IdFuenteFinanciamiento_Relacionado = F.IdFuenteFinanciamiento_Relacionado,
		IdFinanciamiento_Relacionado = F.IdFinanciamiento_Relacionado, IdDocumento_Relacionado = F.IdDocumento_Relacionado,
		EsRelacionado = F.EsRelacionado, OrigenDeInsumo = F.OrigenDeInsumo, TipoDeDocumento = F.TipoDeDocumento, TipoDeInsumo = F.TipoDeInsumo,
		ValorNominal = F.ValorNominal, ImporteAplicado = F.ImporteAplicado, ImporteAplicado_Aux = F.ImporteAplicado_Aux,
		ImporteRestante = F.ImporteRestante, AplicaFarmacia = F.AplicaFarmacia, AplicaAlmacen = F.AplicaAlmacen,
		EsProgramaEspecial = F.EsProgramaEspecial, TipoDeBeneficiario = F.TipoDeBeneficiario, Status = F.Status
	From FACT_Fuentes_De_Financiamiento_Detalles_Documentos T
	Inner Join FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva F (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento And T.IdDocumento = F.IdDocumento)

	--Insertar las faltantes
	Insert Into FACT_Fuentes_De_Financiamiento_Detalles_Documentos
		(IdFuenteFinanciamiento, IdFinanciamiento, IdDocumento, NombreDocumento,
			IdFuenteFinanciamiento_Relacionado, IdFinanciamiento_Relacionado, IdDocumento_Relacionado,
			EsRelacionado, OrigenDeInsumo, TipoDeDocumento, TipoDeInsumo, ValorNominal, ImporteAplicado,
			ImporteAplicado_Aux, ImporteRestante, AplicaFarmacia, AplicaAlmacen, EsProgramaEspecial, TipoDeBeneficiario, Status)
	Select
		F.IdFuenteFinanciamiento, F.IdFinanciamiento, F.IdDocumento, F.NombreDocumento,
		F.IdFuenteFinanciamiento_Relacionado, F.IdFinanciamiento_Relacionado, F.IdDocumento_Relacionado,
		F.EsRelacionado, F.OrigenDeInsumo, F.TipoDeDocumento, F.TipoDeInsumo, F.ValorNominal, F.ImporteAplicado,
		F.ImporteAplicado_Aux, F.ImporteRestante, F.AplicaFarmacia, F.AplicaAlmacen, F.EsProgramaEspecial, F.TipoDeBeneficiario, F.Status
	From FACT_Fuentes_De_Financiamiento_Detalles_Documentos__CargaMasiva F
	Left Join FACT_Fuentes_De_Financiamiento_Detalles_Documentos T (NoLock)
		On (T.IdFuenteFinanciamiento = F.IdFuenteFinanciamiento And T.IdFinanciamiento = F.IdFinanciamiento And T.IdDocumento = F.IdDocumento) 
	Where T.IdFuenteFinanciamiento Is Null

End 
Go--#SQL