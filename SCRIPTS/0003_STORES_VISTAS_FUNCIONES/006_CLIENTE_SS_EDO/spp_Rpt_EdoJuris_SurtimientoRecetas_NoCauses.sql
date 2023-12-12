If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_EdoJuris_SurtimientoRecetas_NoCauses' and xType = 'P') 
    Drop Proc spp_Rpt_EdoJuris_SurtimientoRecetas_NoCauses 
Go--#SQL 
  
--		Exec spp_Rpt_EdoJuris_SurtimientoRecetas_NoCauses '21', '*', '0002', '2012-03-15', '2012-03-18'  
  
  
Create Proc spp_Rpt_EdoJuris_SurtimientoRecetas_NoCauses  
(   
    @IdEstado varchar(2) = '21', @IdJurisdiccion varchar(4) = '001', @IdFarmacia varchar(4) = '0101', @IdCliente varchar(4) = '0002', 
    @FechaInicial varchar(10) = '2012-08-24', @FechaFinal varchar(10) = '2012-08-24' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 	 

Declare 
	@NumPiezas numeric(14,4), 
	@MostrarUltimoSurtimiento bit 	
	
	Set @NumPiezas = 0 	 
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
	Where F.Status = 'A' and F.IdTipoUnidad <> '006' -- Excluir almacenes 

	If @IdFarmacia <> '*'
	  Begin
		Delete From #tmpFarmacias Where IdEstado = @IdEstado And IdFarmacia <> @IdFarmacia
	  End

	------------------------------------
	-- Se obtienen las Fechas Maximas -- 
	------------------------------------
	Select IdEstado, IdJurisdiccion, IdFarmacia, max(FechaRegistro) as FechaMaxima 
	Into #tmpFechas
	From Historial_EdoJuris_NoCauses_Surtimiento_Recetas
	Where FechaRegistro <= @FechaFinal
	Group By IdEstado, IdJurisdiccion, IdFarmacia			
		
		
------------	Tomar los datos iniciales 	
--- En caso de encontrar en la fecha seleccionada 	
	Select H.* 
	Into #Historial_EdoJuris_NoCauses_Surtimiento_Recetas_Aux 
	From Historial_EdoJuris_NoCauses_Surtimiento_Recetas H(NoLock)
	Inner Join #tmpFechas S(NoLock) On ( H.IdEstado = S.IdEstado And H.IdJurisdiccion = S.IdJurisdiccion And H.IdFarmacia = S.IdFarmacia And H.FechaRegistro = S.FechaMaxima )
	Inner Join #tmpFarmacias F(NoLock) On ( H.IdEstado = F.IdEstado And H.IdJurisdiccion = F.IdJurisdiccion And H.IdFarmacia = F.IdFarmacia )


--- Datos del periodo seleccionado 
	Select H.* 
	into #Historial_EdoJuris_NoCauses_Surtimiento_Recetas
	From Historial_EdoJuris_NoCauses_Surtimiento_Recetas H (NoLock) 
	Inner Join #tmpFarmacias F (NoLock) On ( H.IdEstado = F.IdEstado And H.IdJurisdiccion = F.IdJurisdiccion And H.IdFarmacia = F.IdFarmacia )
	Where FechaRegistro between @FechaInicial and @FechaFinal 

--- Asegurar que se muestren datos 		
	Insert Into #Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
	Select * 
	From #Historial_EdoJuris_NoCauses_Surtimiento_Recetas_Aux A 
	Where Not Exists 
		( 
			Select * From #Historial_EdoJuris_NoCauses_Surtimiento_Recetas H 
			Where A.IdEstado = H.IdEstado and A.IdJurisdiccion = H.IdJurisdiccion and A.IdFarmacia = H.IdFarmacia and A.FechaRegistro = H.FechaRegistro
		) 	
		
--		Exec spp_Rpt_EdoJuris_SurtimientoRecetas_NoCauses '21', '*', '0002', '2012-03-20', '2012-03-20'  
	
	
-- select top 1 * from Historial_EdoJuris_NoCauses_Surtimiento_Recetas 

----------------------- AJUSTAR INFORMACION 
--- Jesús Díaz 2K120824.1315 
	Select 
		H.IdEstado, H.IdJurisdiccion, H.Jurisdiccion, H.IdFarmacia, H.Farmacia, 
		H.IdTipoInsumo, H.DescTipoDeInsumo, 
		sum(cast(H.CantidadSurtida as float)) as CantidadSurtida, 
		sum(cast(H.CantidadNoSurtida as float)) as CantidadNoSurtida, 
		sum(cast(H.TotalPiezas as float)) as TotalPiezas, 
		0 as Piezas, cast(0 as numeric(14,4)) as Porcentaje, 
		-- , H.PorcentajeSurtido, H.PorcentajeNoSurtido 
		0 as TipoProceso 
	Into #ResumenInformacion 	
	From #Historial_EdoJuris_NoCauses_Surtimiento_Recetas H (NoLock) 
	Inner Join #tmpFarmacias F (NoLock) On ( H.IdEstado = F.IdEstado And H.IdJurisdiccion = F.IdJurisdiccion And H.IdFarmacia = F.IdFarmacia ) 
	Group by 
		H.IdEstado, H.IdJurisdiccion, H.Jurisdiccion, H.IdFarmacia, H.Farmacia, H.IdTipoInsumo, H.DescTipoDeInsumo 


--- Calcular Ajustes 
	Select @NumPiezas = sum(CantidadSurtida) From #ResumenInformacion  

	Update R Set Piezas = @NumPiezas, Porcentaje = ((CantidadSurtida / @NumPiezas)*100)
	From #ResumenInformacion R 

	Update R Set TipoProceso = 1 
	From #ResumenInformacion R 
	Where CantidadNoSurtida = 0 and Porcentaje >= 2.0 
			
	Update R Set Piezas = @NumPiezas, Porcentaje = ((CantidadSurtida / @NumPiezas)*.075)
	From #ResumenInformacion R 		
	where TipoProceso = 1 
	
	Update R Set CantidadNoSurtida = ceiling(CantidadSurtida * Porcentaje)
	From #ResumenInformacion R 
	Where TipoProceso = 1 	
		
--		spp_Rpt_EdoJuris_SurtimientoRecetas_NoCauses 
		
	Update R Set TotalPiezas = CantidadSurtida + CantidadNoSurtida 
	From #ResumenInformacion R 
			
--	select * 	From #ResumenInformacion R   
--- Calcular Ajustes 			
			
----------------------- AJUSTAR INFORMACION 
				
 
----- Salida Final 		
		Select 
			'Núm. Juris' = T.IdJurisdiccion, T.Jurisdiccion, 'Núm. Unidad' = T.IdFarmacia, 'Unidad' = T.Farmacia, 
			-- 'Folio de Venta' = T.FolioVenta, 
			'Tipo de Insumo' = upper(T.DescTipoDeInsumo), 
			'Piezas surtidas' = cast(CantidadSurtida as int), 
			'Piezas no surtidas' = cast(CantidadNoSurtida as int), 
			'Total piezas' = cast(TotalPiezas as int), 
			'Porcentaje surtido' = (case when TotalPiezas > 0 Then cast(((CantidadSurtida / TotalPiezas)*100) as numeric(14,4)) Else 0 End), 
			'Porcentaje no surtido' = (case when TotalPiezas > 0 Then cast(((CantidadNoSurtida / TotalPiezas)*100) as numeric(14,4)) Else 0 End) 
		From 
		( 
			Select 
				H.IdEstado, H.IdJurisdiccion, H.Jurisdiccion, H.IdFarmacia, H.Farmacia, 
				-- H.FolioVenta, 
				H.IdTipoInsumo, (case when H.IdTipoInsumo = 1 Then 'MATERIAL DE CURACION' Else H.DescTipoDeInsumo End) as DescTipoDeInsumo, 
				-- H.DescTipoDeInsumo, 
				sum(cast(H.CantidadSurtida as float)) as CantidadSurtida, 
				sum(cast(H.CantidadNoSurtida as float)) as CantidadNoSurtida, 
				sum(cast(H.TotalPiezas as float)) as TotalPiezas
				-- , H.PorcentajeSurtido, H.PorcentajeNoSurtido 
			-- From #Historial_EdoJuris_NoCauses_Surtimiento_Recetas H (NoLock) 
			From #ResumenInformacion H (NoLock) 
			Inner Join #tmpFarmacias F (NoLock) On ( H.IdEstado = F.IdEstado And H.IdJurisdiccion = F.IdJurisdiccion And H.IdFarmacia = F.IdFarmacia ) 
			Group by 
				H.IdEstado, H.IdJurisdiccion, H.Jurisdiccion, H.IdFarmacia, H.Farmacia, H.IdTipoInsumo, H.DescTipoDeInsumo
		) as T 		
		-- Where TotalPiezas > 0 
		Order by T.IdJurisdiccion, T.IdFarmacia  
 				

		--		 spp_Rpt_EdoJuris_SurtimientoRecetas_NoCauses 

--	sp_listacolumnas Historial_EdoJuris_NoCauses_Surtimiento_Recetas 

End 
Go--#SQL    



