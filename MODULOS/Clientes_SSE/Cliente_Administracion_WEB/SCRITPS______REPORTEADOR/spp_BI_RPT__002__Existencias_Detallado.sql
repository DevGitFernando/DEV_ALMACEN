 ------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__002__Existencias_Detallado' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__002__Existencias_Detallado 
Go--#SQL 

Create Proc spp_BI_RPT__002__Existencias_Detallado  
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '', @ClaveSSA varchar(20) = '5165', @FuenteDeFinanciamiento varchar(200) = '',
	@Fecha  varchar(10) = '2020-05-31', @TipoDeProceso int = 1  
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 


	If ( CONVERT(varchar(10), getdate(), 120) <> @Fecha ) 
	Begin 
		Set @TipoDeProceso = 2 
	End 


	--Set @TipoDeProceso = 1 
	If @TipoDeProceso = 1 
		Begin 
			Exec spp_BI_RPT__002__Existencias_Detallado__001___General @IdEmpresa, @IdEstado, @IdMunicipio, @IdJurisdiccion, @IdFarmacia, @ClaveSSA, @FuenteDeFinanciamiento, @Fecha  
		End 
	Else 
		Begin 
			Exec spp_BI_RPT__002__Existencias_Detallado__002___UnaFecha @IdEmpresa, @IdEstado, @IdMunicipio, @IdJurisdiccion, @IdFarmacia,  @ClaveSSA, @FuenteDeFinanciamiento, @Fecha  
		End 


End 
Go--#SQL 


