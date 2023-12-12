
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_Proceso_FACT_Fuentes_De_Financiamiento__CargaMasiva_001_Integrar' and xType = 'P' ) 
   Drop Proc sp_Proceso_FACT_Fuentes_De_Financiamiento__CargaMasiva_001_Integrar 
Go--#SQL 

Create Proc sp_Proceso_FACT_Fuentes_De_Financiamiento__CargaMasiva_001_Integrar  
( 
	@Tipo int = 1
) 
With Encryption 
As 
Begin 
Set NoCount On 
 Set Ansi_Warnings Off  --- Especial, peligroso 


	If (@Tipo = 1) --Insumo
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_1_Insumo
	End

	If (@Tipo = 2)--Servicio
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_2_Servicio
	End

	If (@Tipo = 3)--Documentos
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_3_Documentos
	End

	If (@Tipo = 4)--Insumo_Clave_Farmacia
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_4_Insumo_Clave_Farmacia
	End

	If (@Tipo = 5)--Servicio_Clave_Farmacia
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_5_Servicio_Clave_Farmacia
	End

	If (@Tipo = 6)--Insumo_Clave_Jurisdiccion
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_6_Clave_Jurisdiccion
	End

	If (@Tipo = 7)--Servicio_Clave_Jurisdiccion
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_7_Servicio_Clave_Jurisdiccion
	End

	If (@Tipo = 8)--ExcepcionPrecios_Insumos
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_8_ExcepcionPrecios_Insumos
	End

	If (@Tipo = 9)--ExcepcionPrecios_Servicio
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_9_ExcepcionPrecios_Servicio
	End

	If (@Tipo = 10)--BeneficiariosJurisdiccion
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_10_BeneficiariosJurisdiccion
	End

	If (@Tipo = 11)--BeneficiariosRelacionados_Jurisdiccion
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_11_BeneficiariosRelacionados_Jurisdiccion
	End

	If (@Tipo = 12)--BeneficiariosRelacionados_Jurisdiccion
	Begin
		Exec sp_Proceso_FACT_FFinanciamiento__CargaMasiva_001_Integrar_12_Grupos_De_Remisiones
	End

End 
Go--#SQL