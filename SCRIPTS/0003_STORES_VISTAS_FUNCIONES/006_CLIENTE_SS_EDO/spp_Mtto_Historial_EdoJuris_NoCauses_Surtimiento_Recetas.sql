	    
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Historial_EdoJuris_NoCauses_Surtimiento_Recetas' and xType = 'P') 
    Drop Proc spp_Mtto_Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
Go--#SQL 
  
--  Exec spp_Mtto_Historial_EdoJuris_NoCauses_Surtimiento_Recetas '21', '0188', '0002', '0005', '2011-10-10', '2011-10-10'  
  
Create Proc spp_Mtto_Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
(   
    @IdEstado varchar(2) = '21', @IdJurisdiccion varchar(4) = '006', 
    @IdCliente varchar(4) = '0002', @FechaRegistro varchar(10) = '2013-10-01', 
    @Porcentaje numeric(14,4) = 20 
	-- @FechaInicial varchar(10) = '2011-10-01', @FechaFinal varchar(10) = '2011-10-19' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

Declare 
        @Empresa varchar(500), 
        @EncabezadoReporte varchar(500), 
        @EncabezadoPeriodo varchar(500),         
        @EncPrincipal varchar(500), 
		@EncSecundario varchar(500), 
		@Claves numeric(14,4), @Abasto numeric(14,4), -- @ClavesPerfil int, 
		@PorcAbasto numeric(14,4),  
		@iTickets numeric(14,4), @iVales numeric(14,4), 
		@iEsCapturaCompleta numeric(14,4), @iEsNoSurtido numeric(14,4)  

Declare @sFechaMinima datetime 
	
Declare 
	@PorcentajeAjuste numeric(14,4) 
	 
	Set @PorcentajeAjuste = (@Porcentaje / (100.0)) 

	----------------------------
	-- Encabezado de reportes --
	----------------------------
	Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 	

	------------------------------------------- 
	-- Obtener lista de Farmacias a Procesar --
	------------------------------------------- 
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


	-- Lista de Farmacias 		
	Select F.IdEstado, F.Estado, F.IdJurisdiccion, J.Jurisdiccion, F.IdFarmacia, F.Farmacia  
	Into #tmpFarmacias 
	From vw_Farmacias F (NoLock) 
	Inner Join #tmpJuris J (NoLock) On ( F.IdEstado = J.IdEstado and F.IdJurisdiccion = J.IdJurisdiccion ) 


------------------- CLAVES DE CUADRO BASCO DE PROVEEDORES EXTERNOS  
	Select 
		P.IdProveedor, P.Nombre, 
		CP.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, C.Presentacion 
	into #tmpClaves_ProvExternos   
	From CatProveedores_Externos P (NoLock) 
	Inner Join CatProveedores_Externos_Claves CP On ( P.IdEstado = CP.IdEstado and P.IdProveedor = CP.IdProveedor ) 
	Inner Join vw_ClavesSSA_Sales C On ( C.IdClaveSSA_Sal = CP.IdClaveSSA ) 
	Where P.IdEstado = @IdEstado and P.Status = 'A' and CP.Status = 'A'  
	Order by P.IdProveedor, C.DescripcionClave 
------------------- CLAVES DE CUADRO BASCO DE PROVEEDORES EXTERNOS  


------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------ 
	Select C.IdEstado, C.IdFarmacia, C.FolioVenta, C.IdClaveSSA, S.ClaveSSA, S.TipoDeClave, S.TipoDeClaveDescripcion, 
		C.CantidadRequerida As CantidadNoSurtida, 0 As EsCause, 0 as Actualizado 
	Into #tmpNoSurtido 
	From VentasEnc V (Nolock) 
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 
	Inner Join VentasEstadisticaClavesDispensadas C (Nolock)
		On ( V.IdEmpresa = C.IdEmpresa And V.IdEstado = C.IdEstado And V.IdFarmacia = C.IdFarmacia And V.FolioVenta = C.FolioVenta ) 
	Inner Join vw_ClavesSSA_Sales S (Nolock) On ( C.IdClaveSSA = S.IdClaveSSA_Sal ) 
	Where Convert(varchar(10),FechaRegistro, 120 ) = @FechaRegistro And C.EsCapturada = 1	
	Order By C.FolioVenta		


--	Marcar las claves que son causes
	Update T Set EsCause = 1  
	From #tmpNoSurtido T (Nolock)
	Inner Join vw_CB_CuadroBasico_Farmacias C (Nolock)
		On ( T.IdEstado = C.IdEstado And T.IdFarmacia = C.IdFarmacia And T.ClaveSSA = C.ClaveSSA )

--	Borrar las claves que son causes
	Delete From #tmpNoSurtido Where EsCause = 1
------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------ 


------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------ 
	Select 
		V.IdEstado, V.IdFarmacia, V.FolioVenta, P.IdClaveSSA_Sal As IdClaveSSA, P.ClaveSSA, 
		S.TipoDeClave, S.TipoDeClaveDescripcion, 
		Sum(V.CantidadVendida) As CantidadSurtida, 0 As EsCause, 0 as Actualizado  
	Into #tmpSurtido 
	From VentasDet V (Nolock) 
	Inner Join VentasEnc E (Nolock) 
		On ( E.IdEmpresa = V.IdEmpresa and E.IdEstado = V.IdEstado and E.IdFarmacia = V.IdFarmacia and E.FolioVenta = V.FolioVenta ) 
	Inner Join #tmpFarmacias F (NoLock) On ( V.IdEstado = F.IdEstado and V.IdFarmacia = F.IdFarmacia ) 	
------	Left Join #tmpNoSurtido T (Nolock) 
------		On ( V.IdEstado = T.IdEstado And V.IdFarmacia = T.IdFarmacia And V.FolioVenta = T.FolioVenta )
	Inner Join vw_Productos_CodigoEAN P (Nolock) On ( V.IdProducto = P.IdProducto and V.CodigoEAN = P.CodigoEAN ) 
	Inner Join vw_ClavesSSA_Sales S (Nolock) On ( P.ClaveSSA = S.ClaveSSA ) 	
	Where Convert(varchar(10), E.FechaRegistro, 120 ) = @FechaRegistro  
	Group By V.IdEmpresa, V.IdEstado, V.IdFarmacia, V.FolioVenta, P.IdClaveSSA_Sal, P.ClaveSSA, S.TipoDeClave, S.TipoDeClaveDescripcion  
	Order By V.FolioVenta 

	--	Marcar las claves que son causes
	Update T Set EsCause = 1  
	From #tmpSurtido T (Nolock)
	Inner Join vw_CB_CuadroBasico_Farmacias C (Nolock)
		On ( T.IdEstado = C.IdEstado And T.IdFarmacia = C.IdFarmacia And T.ClaveSSA = C.ClaveSSA )	

--	Borrar las claves que son causes
	Delete From #tmpSurtido Where EsCause = 1 
	
	
------------------- SOLO CONSDERAR CLAVES DE CUADRO BASCO DE PROVEEDORES EXTERNOS  
	Update H Set Actualizado = 5 
	From #tmpSurtido H (NoLock) 
	Where Not Exists ( Select * From #tmpClaves_ProvExternos P Where P.ClaveSSA = H.ClaveSSA )  

	Delete From #tmpSurtido Where Actualizado = 5  		
	

	Update H Set Actualizado = 5 
	From #tmpNoSurtido H (NoLock) 
	Where Not Exists ( Select * From #tmpClaves_ProvExternos P Where P.ClaveSSA = H.ClaveSSA )  

	Delete From #tmpNoSurtido Where Actualizado = 5  			
------------------- SOLO CONSDERAR CLAVES DE CUADRO BASCO DE PROVEEDORES EXTERNOS  	
	
	
	
------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------ 

-----------------	select @PorcentajeAjuste 
-----------------	Jesús Díaz 2K120604.1032
------------	Update S Set CantidadSurtida = ceiling(CantidadSurtida * (1 + @PorcentajeAjuste))  
------------	From #tmpSurtido S 
------------
------------	Update S Set CantidadNoSurtida = ceiling(CantidadNoSurtida * (1 - @PorcentajeAjuste))  
------------	From #tmpNoSurtido S 
	

	--- Borrar la tabla base de los Datos
	If Exists ( select * from sysobjects (nolock) Where Name = '#tmpConcentradoPorcentaje' and xType = 'U' ) 
	   Drop table #tmpConcentradoPorcentaje 

	Select S.IdEstado, F.Estado, S.IdFarmacia, F.Farmacia, F.IdJurisdiccion, F.Jurisdiccion,  
		S.FolioVenta, 
		S.TipoDeClave, S.TipoDeClaveDescripcion,   
		Sum(S.CantidadSurtida) As CantidadSurtida, Sum(isNull(N.CantidadNoSurtida, 0)) As CantidadNoSurtida, 
		(Sum(S.CantidadSurtida) + Sum(isNull(N.CantidadNoSurtida, 0))) As TotalPiezas, 
		Cast(0 as Numeric(14, 4) ) As PorcentajeSurtido, Cast(0 as Numeric(14, 4) ) As PorcentajeNoSurtido
	Into #tmpConcentradoPorcentaje 
	From #tmpSurtido S (Nolock) 
	Inner Join #tmpFarmacias F (NoLock) On ( S.IdEstado = F.IdEstado and S.IdFarmacia = F.IdFarmacia ) 
	Left Join #tmpNoSurtido N (Nolock) On ( S.IdEstado = N.IdEstado And S.IdFarmacia = N.IdFarmacia And S.FolioVenta = N.FolioVenta  )
	Group By S.IdEstado, F.Estado, S.IdFarmacia, F.Farmacia, F.IdJurisdiccion, F.Jurisdiccion, S.FolioVenta, 
		S.TipoDeClave, S.TipoDeClaveDescripcion    
--	Order By N.FolioVenta 

--	Se calculan los porcentajes de lo surtido y No Surtido 
	Update P Set 
		PorcentajeSurtido = Case When CantidadSurtida = 0 Then 0 Else ((CantidadSurtida/TotalPiezas) * 100 ) End , 
		PorcentajeNoSurtido = Case When CantidadNoSurtida = 0 Then 0 Else ((CantidadNoSurtida/TotalPiezas) * 100 ) End 
	From #tmpConcentradoPorcentaje P (Nolock) 


--	select top 1 * from Historial_EdoJuris_NoCauses_Surtimiento_Recetas 


------------------------------------------------ 
--- Se Inserta en el Historial de Surtimiento -- 
------------------------------------------------ 
	Update H Set Actualizado = 5 
	From Historial_EdoJuris_NoCauses_Surtimiento_Recetas H (NoLock) 
	Inner Join #tmpConcentradoPorcentaje D (NoLock) 
		On ( H.IdEstado = D.IdEstado and H.IdFarmacia = D.IdFarmacia 
			 and convert(varchar(10), H.FechaRegistro, 120) = @FechaRegistro )

	Delete From Historial_EdoJuris_NoCauses_Surtimiento_Recetas Where Actualizado = 5  
	
	

	Insert Into Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
	( 
		IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
		FolioVenta, IdTipoInsumo, DescTipoDeInsumo, FechaRegistro, 
		CantidadSurtida, CantidadNoSurtida, TotalPiezas, PorcentajeSurtido, PorcentajeNoSurtido, Status, Actualizado 
	) 
	Select 
		@IdEstado, IdJurisdiccion, Jurisdiccion, IdFarmacia, Farmacia, 
		FolioVenta, TipoDeClave, TipoDeClaveDescripcion, @FechaRegistro, 
		CantidadSurtida, CantidadNoSurtida, TotalPiezas, 
		PorcentajeSurtido, PorcentajeNoSurtido, 'A' as Status, 0 as Actualizado
	From #tmpConcentradoPorcentaje  
	Order by IdJurisdiccion, IdFarmacia 

-- Select * From Historial_EdoJuris_Surtimiento_Recetas

--	sp_listacolumnas Historial_EdoJuris_NoCauses_Surtimiento_Recetas 

End 
Go--#SQL 
