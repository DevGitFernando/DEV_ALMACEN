If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_CteReg_EdoJuris_Proximos_Caducar' and xType = 'P' ) 
   Drop Proc spp_Rpt_CteReg_EdoJuris_Proximos_Caducar 
Go--#SQL

--		Exec spp_Rpt_CteReg_EdoJuris_Proximos_Caducar '21', '*', '2012-01-01', '2012-03-31', 0, 0, 0  

--		Exec spp_Rpt_CteReg_EdoJuris_Proximos_Caducar '21', '*', '*', '2012-05-21', '2012-09-21', '0', '0', '0'  

Create Proc spp_Rpt_CteReg_EdoJuris_Proximos_Caducar 
( 
	@IdEstado varchar(2) = '21', @IdJurisdiccion varchar(4) = '007', @IdFarmacia varchar(6) = '0226',
	@FechaInicial varchar(10) = '2012-01-01' , @FechaFinal varchar(10) = '2012-03-31', @bEsGeneral int = 0, 
	@TipoInsumo tinyint = 0, @TipoDispensacion tinyint = 0 
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
-- Set NoCount On 
/* 
	Los valores default de los parametros no deben ser modificados, este Store se implementa a varios grupos de datos. 
	Se utiliza de forma dinamica. 
*/ 

Declare @sSql varchar(8000),   
		@sWhere varchar(8000), @sMostrar varchar(50), 
		@dFecha datetime 	 

Declare @sEncPrinRpt varchar(200), 
		@sEncSecRpt varchar(200),
		@Status varchar(2)

		Set @Status = 'A'


---------- Obtener lista de Farmacias a Procesas  
	Select IdEstado, @IdEstado as Estado, IdJurisdiccion, Descripcion as Jurisdiccion 
	Into #tmpJuris 
	From CatJurisdicciones 
	Where IdEstado = @IdEstado and IdJurisdiccion = @IdJurisdiccion 

	if @IdJurisdiccion = '*' 
		Begin 
			Insert Into #tmpJuris 
			Select IdEstado, @IdEstado as Estado, IdJurisdiccion as IdJuris, Descripcion as Jurisdiccion 
			From CatJurisdicciones 
			Where IdEstado = @IdEstado -- and IdJurisdiccion = @IdJurisdiccion 	   
	    End 
		

	Select IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia 
	Into #tmpFar 
	From vw_Farmacias (NoLock) 
	Where IdEstado = @IdEstado and IdJurisdiccion In ( Select IdJurisdiccion From #tmpJuris (Nolock) Where IdEstado = @IdEstado )
	and IdFarmacia = @IdFarmacia and Status = @Status

	if @IdFarmacia = '*'
		Begin
			Insert Into #tmpFar 
			Select IdEstado, Estado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia 
			From vw_Farmacias (NoLock) 
			Where IdEstado = @IdEstado and IdJurisdiccion In ( Select IdJurisdiccion From #tmpJuris (Nolock) Where IdEstado = @IdEstado )
			and Status = @Status
		End
		

	
	Select F.IdEstado, F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia 
	Into #tmpFarmacias 
	From vw_Farmacias F (NoLock) 
	Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion ) 
	Inner Join #tmpFar TF (Nolock) On ( F.IdEstado = TF.IdEstado and F.IdJurisdiccion = TF.IdJurisdiccion and F.IdFarmacia = TF.IdFarmacia )
---------- Obtener lista de Farmacias a Procesas  



------	-- Se obtiene el Encabezado Principal y el Encabezado Secundario
------	-- Select @sEncPrinRpt = EncabezadoPrincipal, @sEncSecRpt = EncabezadoSecundario From fg_Regional_EncabezadoReportesClientesSSA() 
	Select @sEncPrinRpt = EncabezadoPrincipal, @sEncSecRpt = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 
    Set @sEncPrinRpt = IsNull(@sEncPrinRpt, 'SISTEMA INTEGRAL DE INFORMACIÓN') 
    Set @sEncSecRpt = IsNull(@sEncSecRpt, 'ADMINISTRACION UNIDAD')     	
	
	---- Preparar la tabla con la estructura vacia 
	Set @dFecha = getdate() 
	Select 
		-- space(300) as EncPrincipal, space(300) as EncSecundario, 
		-- IdEmpresa, Empresa, 
		L.IdEstado, L.Estado, F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia,
		L.ClaveSSA, L.DescripcionSal as DescripcionClave,
		L.IdPresentacion_ClaveSSA, L.Presentacion_ClaveSSA, L.TasaIva, L.ClaveLote, 
		convert(varchar(7), L.FechaCaducidad, 120) as FechaCaducidad, 
		cast(sum(L.Existencia) as int) as Existencia,
		@FechaInicial as FechaInicial , @FechaFinal as FechaFinal 
	Into #tmpProximosCaducar  	
	From SVR_INV_Generar_Existencia_Detallado L (NoLock) 
	Inner Join #tmpFarmacias F (NoLock) On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia ) 
	Where 
		convert(varchar(10), FechaCaducidad, 120)  Between @FechaInicial And @FechaFinal  
	Group by 
		L.IdEstado, L.Estado, F.IdJurisdiccion, F.Jurisdiccion, F.IdFarmacia, F.Farmacia,
		L.ClaveSSA, L.DescripcionSal, 
		L.IdPresentacion_ClaveSSA, L.Presentacion_ClaveSSA, L.TasaIva, L.ClaveLote, 
		convert(varchar(7), L.FechaCaducidad, 120) 
	Having sum(L.Existencia) > 0 
	
	

	--- Remover los Lotes segun sea el caso 
	If @TipoDispensacion <> 0 
		Begin 
			If @TipoDispensacion = 1 
				Delete From #tmpProximosCaducar Where ClaveLote Not Like '%*%'  -- Consignacion 

			If @TipoDispensacion = 2
				Delete From #tmpProximosCaducar Where ClaveLote Like '%*%'  -- Venta 
	End

	If @TipoInsumo <> 0 
		Begin 
			If @TipoInsumo = 1 
				Delete From #tmpProximosCaducar Where TasaIva <> 0  --- Medicamentos 

			If @TipoInsumo = 2
				Delete From #tmpProximosCaducar Where TasaIva = 0  --- Otros (Material de Curacion, Varios)  
	End
			
		
		
----	-- Se ponen los precios de licitacion por IdClaveSSA
----	Select C.IdClaveSSA, Case When P.Precio = 0 Then 1 Else P.Precio End As Precio
----	Into #tmpPreciosLicitacionIdClaveSSA
----	From vw_CB_CuadroBasico_Farmacias C (Nolock)
----	Inner Join vw_Claves_Precios_Asignados P (Nolock)
----		On( C.IdEstado = P.IdEstado and C.IdClaveSSA = P.IdClaveSSA )
----	Where C.IdEstado = @IdEstado -- and C.IdFarmacia = @IdFarmacia 


--- Salida Final 
	Select IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
		ClaveSSA, DescripcionClave,
		Presentacion_ClaveSSA, ClaveLote, 
		FechaCaducidad, Existencia
	From #tmpProximosCaducar 
	Order By IdJurisdiccion, IdFarmacia, DescripcionClave, ClaveLote 	
		
End 
Go--#SQL


	