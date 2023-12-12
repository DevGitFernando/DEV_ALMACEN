If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_FACT_Rtp_DetalladoFactura' and xType = 'P' ) 
   Drop Proc spp_FACT_Rtp_DetalladoFactura
Go--#SQL 

Create Proc spp_FACT_Rtp_DetalladoFactura 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0001', 
	@FolioFactura varchar(10) = '0000000001'
) 
As 
Begin 
Set NoCount On 

Declare 
	@sSql varchar(7000), @FolioRemision varchar(10), 
	@sFiltro varchar(500),
	@sIdFarmacia varchar(4),
	@sClaveSSA varchar(30),
	@sReceta varchar(20), 
	@sFoliosReceta varchar(6000),  	 
	@sFolio varchar(20),  	 
	@sFoliosTickets varchar(6000)  

	Set @sSql = '' 
	Set @sFiltro = ''
	Set @FolioRemision = ''
	Set @sIdFarmacia = ''
	Set @sClaveSSA = ''
	Set @sReceta = '' 
	Set @sFoliosReceta = '' 
	Set @sFolio = ''
	Set @sFoliosTickets = ''
	
	Set @FolioRemision = ( Select FolioRemision From FACT_Facturas (nolock)
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioFactura = @FolioFactura )
	
	Select 
	0 as Año, space(30) as Mes, C.IdFarmacia, space(100) as Farmacia, F.FolioFacturaElectronica, 
 	C.IdPrograma, C.IdSubPrograma, space(100) as SubPrograma, space(50) as Contrato,
	R.TipoInsumo, case when R.TipoInsumo = '02' Then 'MEDICAMENTO' Else 'MAT. CURACION' End As TipoDeInsumo,
	C.ClaveSSA, space(7500) as DescripcionClave, space(100) as Presentacion, 0 as ContenidoPaquete,
	Sum(C.Cantidad) as Cantidad, C.PrecioLicitado as PrecioUnitario, 
	Case When C.Iva > 0 Then Sum(C.SubTotalGrabado) Else Sum(C.Importe) End as SubTotal,
	Sum(C.Iva) as Iva, Sum(C.Importe) as Importe,
	@sFoliosReceta as FoliosRecetas, @sFoliosTickets as FoliosTickects, 
	case when R.TipoDeRemision = 1 then 'Producto' else 'Administración' end as ConceptoFactura	
	Into #tmpConcentrado
	From FACT_Facturas F (nolock)
	Inner Join FACT_Remisiones R (nolock)
		On ( F.IdEmpresa = R.IdEmpresa and F.IdEstado = R.IdEstado and F.IdFarmacia = R.IdFarmaciaGenera and F.FolioRemision = R.FolioRemision ) 
	Inner Join FACT_Remisiones_Concentrado C (nolock)
		On ( C.IdEmpresa = R.IdEmpresa and C.IdEstado = R.IdEstado and C.IdFarmaciaGenera = R.IdFarmaciaGenera and C.FolioRemision = R.FolioRemision )
	Where F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and F.FolioFactura = @FolioFactura
	Group By C.IdFarmacia, F.FolioFacturaElectronica, C.IdPrograma, C.IdSubPrograma, 
	R.TipoInsumo, C.ClaveSSA, C.PrecioLicitado, C.Iva, R.TipoDeRemision
	Order By C.IdFarmacia, C.ClaveSSA	
	
	
	----- se actualiza la descripcion, presentación y contenido paquete de la claveSSA
	Update T Set T.DescripcionClave = S.DescripcionSal, T.Presentacion = S.Presentacion, T.ContenidoPaquete = S.ContenidoPaquete
	From #tmpConcentrado T (Nolock) 
	Inner Join vw_ClavesSSA_Sales S (Nolock) On ( T.ClaveSSA = S.ClaveSSA )
	
	----- SE ACTUALIZA EL NOMBRE DE LA FARMACIA
	Update T Set T.Farmacia = F.NombreFarmacia
	From #tmpConcentrado T (Nolock)
	Inner Join CatFarmacias F (Nolock)
		On ( F.IdEstado = @IdEstado and F.IdFarmacia = T.IdFarmacia )
	-------------------------------------------------------------------------------------------------------------
	
	----- SE ACTUALIZA EL NOMBRE DEL SUBPROGRAMA
	Update T Set T.SubPrograma = F.Descripcion
	From #tmpConcentrado T (Nolock)
	Inner Join CatSubProgramas F (Nolock)
		On ( F.IdPrograma = T.IdPrograma and F.IdSubPrograma = T.IdSubPrograma )
	-------------------------------------------------------------------------------------------------------------
	
	------- Se obtiene el año y el mes de validacion para la facturacion
	Select V.IdFarmacia, DatePart(yyyy, V.FechaRegistro) as Año, dbo.fg_NombresDeMesNumero(DatePart(mm, V.FechaRegistro)) as Mes
	Into #tmpAnioMes
	From VentasEnc V (Nolock)
	Inner Join FACT_Remisiones_Detalles R (nolock)
		On ( V.IdEmpresa = R.IdEmpresa and V.IdEstado = R.IdEstado and V.IdFarmacia = R.IdFarmacia and V.FolioVenta = R.FolioVenta )
	Where R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmaciaGenera = @IdFarmacia and R.FolioRemision = @FolioRemision 
	Group By V.IdFarmacia, R.FolioVenta, V.FechaRegistro
	
	------ Se actualiza el año y mes --------------------------------
	Update C Set C.Año = T.Año, C.Mes = T.Mes
	From #tmpConcentrado C (Nolock)
	Inner Join #tmpAnioMes T (Nolock) On ( C.IdFarmacia = T.IdFarmacia )
	-----------------------------------------------------------------------------------------------------------------------
	
	------   Se obtienen los folios de venta y los folios de receta ---------------------------------------------------------------------
	Select R.IdFarmacia, V.FolioVenta as FolioTicket, I.NumReceta as FolioReceta, R.ClaveSSA
	Into #tmpFoliosVentas_Recetas
	From FACT_Remisiones_Detalles R (nolock)
	Inner Join VentasDet V (Nolock)
		On ( V.IdEmpresa = R.IdEmpresa and V.IdEstado = R.IdEstado and V.IdFarmacia = R.IdFarmacia and V.FolioVenta = R.FolioVenta
			and V.IdProducto = R.IdProducto and V.CodigoEAN = R.CodigoEAN )
	Inner Join VentasInformacionAdicional I (Nolock)
		On ( V.IdEmpresa = I.IdEmpresa and V.IdEstado = I.IdEstado and V.IdFarmacia = I.IdFarmacia and V.FolioVenta = I.FolioVenta )
	Where R.IdEmpresa = @IdEmpresa and R.IdEstado = @IdEstado and R.IdFarmaciaGenera = @IdFarmacia and R.FolioRemision = @FolioRemision
	Group By R.IdFarmacia, V.FolioVenta, I.NumReceta, R.ClaveSSA
	--------------------------------------------------------------------------------------------------------------------------------------
	
	----------------------------   Proceso para asignar los folio de recetas y tickets a las claves  ---------------------------------------------------
	
	Declare #cursor_001 
    Cursor For 
		Select Distinct C.IdFarmacia, C.ClaveSSA 
		From #tmpConcentrado C (NoLock)  		
		Where Exists 
			 (
				Select 1 
				From #tmpFoliosVentas_Recetas D (NoLock) 
				Where C.IDFARMACIA = D.IdFarmacia and C.CLAVESSA = D.ClaveSSA 
			 ) 
			 and C.FoliosRecetas = '' 
		Order By IdFarmacia, ClaveSSA
		
    Open #cursor_001
    FETCH NEXT FROM #cursor_001 Into @sIdFarmacia, @sClaveSSA  
        WHILE @@FETCH_STATUS = 0 
        BEGIN 		

			-- Print @sIdFarmacia + '   ' + @sClaveSSA 
			Set @sFoliosReceta = '' 
			Set @sFoliosTickets = '' 
			
			--------- Concatenar Folios de Receta 	
			Declare #cursor_002 
			Cursor For 
				Select FolioReceta, FolioTicket  
				From #tmpFoliosVentas_Recetas (NoLock) 
				Where IdFarmacia = @sIdFarmacia and ClaveSSA = @sClaveSSA  
				Order By IdFarmacia, ClaveSSA 
				
			Open #cursor_002
			FETCH NEXT FROM #cursor_002 Into @sReceta, @sFolio  
				WHILE @@FETCH_STATUS = 0 
				BEGIN 		
					Set @sFoliosReceta = @sFoliosReceta + @sReceta + ', ' 
					Set @sFoliosTickets = @sFoliosTickets + @sFolio + ', ' 
					FETCH NEXT FROM #cursor_002 Into  @sReceta, @sFolio  
				END
			Close #cursor_002 
			Deallocate #cursor_002 		
			--------- Concatenar Folios de Receta 		

			Set @sFoliosReceta = LTRIM(RTRIM(@sFoliosReceta))
			Set @sFoliosTickets = LTRIM(RTRIM(@sFoliosTickets))
	
			---- Asingar los Folios al Encabezado 
			If len(@sFoliosReceta) > 0 
				Set @sFoliosReceta = LEFT(@sFoliosReceta, len(@sFoliosReceta) - 1) 
				
			If len(@sFoliosTickets) > 0 				
				Set @sFoliosTickets = LEFT(@sFoliosTickets, len(@sFoliosTickets) - 1) 				

			-- print @sFoliosReceta 
			Update C Set FoliosRecetas = ISNULL(@sFoliosReceta, '0'), FoliosTickects = ISNULL(@sFoliosTickets, '0')				
			From #tmpConcentrado C (NoLock) 
			Where IdFarmacia = @sIdFarmacia and ltrim(rtrim(ClaveSSA)) = @sClaveSSA   
	
			FETCH NEXT FROM #cursor_001 Into  @sIdFarmacia, @sClaveSSA 
        END
    Close #cursor_001 
    Deallocate #cursor_001
	
	
	----------------------------------------------------------------------------------------------------------------------------------------------------
	
	--------------  se devuelve el concentrado  -----------------------------------------------
	Select * From #tmpConcentrado (Nolock) 
	Order By ClaveSSA
	-------------------------------------------------------------------------------------------
End 
Go--#SQL 

