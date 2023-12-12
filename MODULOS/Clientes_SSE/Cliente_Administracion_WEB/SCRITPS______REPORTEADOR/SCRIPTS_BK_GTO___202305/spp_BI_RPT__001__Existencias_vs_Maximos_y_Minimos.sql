------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__001__Existencias_vs_Maximos_y_Minimos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__001__Existencias_vs_Maximos_y_Minimos 
Go--#SQL  

Create Proc spp_BI_RPT__001__Existencias_vs_Maximos_y_Minimos 
( 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '11', @IdMunicipio varchar(4) = '*', @IdJurisdiccion varchar(3) = '*', 
	@IdFarmacia varchar(4) = '88', 
	@Fecha  varchar(10) = '2015-12-05', @TipoDeProceso int = 1  
) 
As 
Begin 
Set DateFormat YMD  
Set NoCount On 


	If @TipoDeProceso = 1 
		Begin 
			Exec spp_BI_RPT__001__Existencias_vs_Maximos_y_Minimos__001___General 
				@IdEmpresa, @IdEstado, @IdMunicipio, @IdJurisdiccion, @IdFarmacia, @Fecha  
		End 
	Else 
		Begin 
			Exec spp_BI_RPT__001__Existencias_vs_Maximos_y_Minimos__002___UnaFecha 
				@IdEmpresa, @IdEstado, @IdMunicipio, @IdJurisdiccion, @IdFarmacia, @Fecha 
		End 




End 
Go--#SQL 


