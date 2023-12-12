If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_EdoJuris_SurtimientoRecetas' and xType = 'P') 
    Drop Proc spp_Rpt_EdoJuris_SurtimientoRecetas 
Go--#SQL 
  
--		Exec spp_Rpt_EdoJuris_SurtimientoRecetas '21', '*', '0002', '2012-03-15', '2012-03-18'  
  
  
Create Proc spp_Rpt_EdoJuris_SurtimientoRecetas  
(   
    @IdEstado varchar(2) = '21', @IdJurisdiccion varchar(4) = '006', @IdCliente varchar(4) = '0002', 
    @FechaInicial varchar(10) = '2011-09-01', @FechaFinal varchar(10) = '2012-09-30' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 	 
	
Declare 
	@MostrarUltimoSurtimiento bit 	
	 
	Select @MostrarUltimoSurtimiento = dbo.fg_GetParametro_MostrarUltimoSurtimiento()  
	 
	-------------------------------------------------
	-- Se Obtiene la lista de Farmacias a Procesar --
	-------------------------------------------------
	Select IdEstado, @IdEstado as Estado, IdJurisdiccion, Descripcion as Jurisdiccion 
	Into #tmpJuris 
	From CatJurisdicciones 
	Where IdEstado = @IdEstado and IdJurisdiccion = @IdJurisdiccion 

	if @IdJurisdiccion = '*' 
		Begin 
			Insert Into #tmpJuris 
			Select IdEstado, @IdEstado as Estado, IdJurisdiccion as IdJuris, Descripcion as Jurisdiccion 
			From CatJurisdicciones 
			Where IdEstado = @IdEstado 
		End 

	-- Se obtiene la Lista de Farmacias de las Jurisdicciones.	
	Select F.IdEstado, F.IdJurisdiccion, J.Jurisdiccion, F.IdFarmacia 
	Into #tmpFarmacias 
	From CatFarmacias F (NoLock) 
	Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion ) 
	Where F.Status = 'A' 
	
	------------------------------------
	-- Se obtienen las Fechas Maximas -- 
	------------------------------------
	Select IdEstado, IdJurisdiccion, IdFarmacia, max(FechaRegistro) as FechaMaxima 
	Into #tmpFechas
	From Historial_EdoJuris_Surtimiento_Recetas
	Where FechaRegistro <= @FechaFinal
	Group By IdEstado, IdJurisdiccion, IdFarmacia		


---		select * from  CFG_VALES_Emision 
	Select 
		F.IdEstado, F.IdFarmacia,  
		(case when IsNull(V.EmiteVales, 0) = 1 Then 'SI' Else 'NO' end) as EmiteVales, 
		IsNull(V.Observaciones, '') as Observaciones, 
		(case when IsNull(V.EmiteVales, 0) = 1 Then 
			(case when IsNull(V.TipoEmision, 0) = 1 Then 'Automatico' Else 'Manual' End) 
	     Else '' end) as TipoEmision  
	Into #tmpFarmacias_Vales      
	From #tmpFarmacias F  
	Left Join CFG_VALES_Emision V (NoLock) On ( F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia )  


-----------------------------------
------- Se devuelve el resultado --
-----------------------------------
/* 
		Select 
			'Núm. Juris' = H.IdJurisdiccion, H.Jurisdiccion, 'Núm. Unidad' = H.IdFarmacia, 'Unidad' = H.Farmacia, 
			H.Recetas, 'Recetas completas' = H.RecetasCompletas, 
			H.Vales, 'Porcentaje vales' = H.PorcentajeVales, 
			'No surtido' = H.NoSurtido, 'Porcentaje no surtido' = H.PorcentajeNoSurtido
		From Historial_EdoJuris_Surtimiento_Recetas H(NoLock)
		Inner Join #tmpFechas S(NoLock) On ( H.IdEstado = S.IdEstado And H.IdJurisdiccion = S.IdJurisdiccion And H.IdFarmacia = S.IdFarmacia And H.FechaRegistro = S.FechaMaxima )
		Inner Join #tmpFarmacias F(NoLock) On ( H.IdEstado = F.IdEstado And H.IdJurisdiccion = F.IdJurisdiccion And H.IdFarmacia = F.IdFarmacia )
		Order by H.IdJurisdiccion, H.IdFarmacia  
*/ 		
		
		
------------	Tomar los datos iniciales 	
--- En caso de encontrar en la fecha seleccionada 	
	Select H.* 
	Into #Historial_EdoJuris_Surtimiento_Recetas_Aux 
	From Historial_EdoJuris_Surtimiento_Recetas H (NoLock) 
	Inner Join #tmpFechas S(NoLock) On ( H.IdEstado = S.IdEstado And H.IdJurisdiccion = S.IdJurisdiccion And H.IdFarmacia = S.IdFarmacia And H.FechaRegistro = S.FechaMaxima )
	Inner Join #tmpFarmacias F(NoLock) On ( H.IdEstado = F.IdEstado And H.IdJurisdiccion = F.IdJurisdiccion And H.IdFarmacia = F.IdFarmacia )


--- Datos del periodo seleccionado 
	Select H.* 
	into #Historial_EdoJuris_Surtimiento_Recetas
	From Historial_EdoJuris_Surtimiento_Recetas H (NoLock) 
	Inner Join #tmpFarmacias F (NoLock) On ( H.IdEstado = F.IdEstado And H.IdJurisdiccion = F.IdJurisdiccion And H.IdFarmacia = F.IdFarmacia )
	Where FechaRegistro between @FechaInicial and @FechaFinal 

--- Asegurar que se muestren datos 		
	If @MostrarUltimoSurtimiento = 1  -- Jesús Díaz 2K120918.1055 
		Begin 
			Insert Into #Historial_EdoJuris_Surtimiento_Recetas 
			Select * 
			From #Historial_EdoJuris_Surtimiento_Recetas_Aux A 
			Where Not Exists 
				( 
					Select * From #Historial_EdoJuris_Surtimiento_Recetas H 
					Where A.IdEstado = H.IdEstado and A.IdJurisdiccion = H.IdJurisdiccion and A.IdFarmacia = H.IdFarmacia and A.FechaRegistro = H.FechaRegistro
				) 	
				
		--		Exec spp_Rpt_EdoJuris_SurtimientoRecetas '21', '*', '0002', '2012-03-20', '2012-03-20'  
	End 
	

--- Salida Final 		
	Select Row_Number() Over(Order by T.IdJurisdiccion, T.IdFarmacia  ) As Renglon,
		'Núm. Juris' = T.IdJurisdiccion, T.Jurisdiccion, 'Núm. Unidad' = T.IdFarmacia, 'Unidad' = T.Farmacia, 
		T.Recetas , 'Recetas completas' = cast(((  (T.Recetas - (T.Vales + T.NoSurtido)) / (T.Recetas * 1.0)) * 100) as numeric(14,4)), 
		T.Vales, 'Porcentaje vales' = cast((( T.Vales / (T.Recetas * 1.0)) * 100.00) as numeric(14,4)),  
		'No surtido' = T.NoSurtido, 'Porcentaje no surtido' = cast((( T.NoSurtido / (T.Recetas * 1.0)) * 100.00) as numeric(14,4)), 
		'Emite vales' = T.EmiteVales, 'Tipo de emisión' = T.TipoEmision, 'Observaciones' = T.Observaciones  
	From 
	( 				
		Select 
			H.IdJurisdiccion, H.Jurisdiccion, H.IdFarmacia, H.Farmacia, 
			sum(H.Recetas) as Recetas, sum(H.Vales) as Vales, sum(H.NoSurtido) as NoSurtido, 
			IsNull(V.EmiteVales, 'NO') as EmiteVales, V.TipoEmision, V.Observaciones    
		From #Historial_EdoJuris_Surtimiento_Recetas H (NoLock) 
		Inner Join #tmpFarmacias F (NoLock) On ( H.IdEstado = F.IdEstado And H.IdJurisdiccion = F.IdJurisdiccion And H.IdFarmacia = F.IdFarmacia ) 
		Left Join #tmpFarmacias_Vales V (NoLock) On ( F.IdEstado = V.IdEstado and F.IdFarmacia = V.IdFarmacia ) 
		-- Where FechaRegistro between @FechaInicial and @FechaFinal 
		Group by 
			H.IdJurisdiccion, H.Jurisdiccion, H.IdFarmacia, H.Farmacia, 
			V.EmiteVales, V.TipoEmision, V.Observaciones  
	) as T 		
	-- Where 1= 0 
	Order by T.IdJurisdiccion, T.IdFarmacia  
--- Salida Final 		


/* 
		(case when IsNull(V.EmiteVales, 0) = 1 Then 'SI' Else 'NO' end) as EmiteVales, 
		IsNull(V.Observaciones, '') as Observaciones, 
		(case when IsNull(V.EmiteVales, 0) = 1 Then 
			(case when IsNull(V.TipoEmision, 0) = 1 Then 'Automatico' Else 'Manual' End) 
	     Else '' end) as TipoEmision  
*/ 	     

		--		 spp_Rpt_EdoJuris_SurtimientoRecetas

End 
Go--#SQL    



