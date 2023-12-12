If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Ctl_CierresGeneral' and xType = 'P' ) 
    Drop Proc spp_Mtto_Ctl_CierresGeneral 
Go--#SQL

---     Exec spp_Mtto_Ctl_CierresGeneral '001', '21', '1113', '', '0001', '21', '1113', '0001'

Create Proc spp_Mtto_Ctl_CierresGeneral 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0018', 
	@FolioCierre varchar(8) = '', @IdPersonal varchar(4) = '0001', 
	@IdEstadoRegistra varchar(2) = '09', @IdFarmaciaRegistra varchar(4) = '0018',  @IdPersonalRegistra varchar(4) = '0001'  
)
with Encryption 
As 
Begin 
--Set NoCount On 


Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint,
		@FechaInicial datetime, @FechaFinal datetime  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set DateFormat YMD	

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @FechaInicial = getdate()
	Set @FechaFinal = getdate()

	----- se genera tabla temporal de productos--codigoean   -------------------
	Select * Into #tmp_vw_Productos_CodigoEAN From vw_Productos_CodigoEAN (Nolock)
	----------------------------------------------------------------------------
	
	
	------------ se genera la tabla temporal de la claves distintas  -------------------------		    
 --   Select Distinct P.IdClaveSSA_Sal
 --   Into #tmpClavesSSA
	--From MovtosInv_Enc E (Nolock)
	--Inner Join MovtosInv_Det_CodigosEAN D (Nolock)
	--	On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )
	--Inner Join #tmp_vw_Productos_CodigoEAN P (Nolock) On ( P.IdProducto = D.IdProducto and P.CodigoEAN = D.CodigoEAN )			
	--Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and E.FolioCierre = 0	
	--------------------------------------------------------------------------------------------------------------------------------------
	
	------------ se genera la tabla temporal de la claves y movtos involucrados  -------------------------		    
    Select E.IdTipoMovto_Inv as IdTipoMovto, P.IdClaveSSA_Sal, Sum(D.Cantidad) as Piezas, SUM(Importe) as ImporteInv
    Into #tmpMovtos_ClaveSSA
	From MovtosInv_Enc E (Nolock)
	Inner Join MovtosInv_Det_CodigosEAN D (Nolock)
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioMovtoInv = D.FolioMovtoInv )
	Inner Join #tmp_vw_Productos_CodigoEAN P (Nolock) On ( P.IdProducto = D.IdProducto and P.CodigoEAN = D.CodigoEAN )			
	Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and E.FolioCierre = 0
	Group By E.IdTipoMovto_Inv, P.IdClaveSSA_Sal
	
	If Exists ( Select * From #tmpMovtos_ClaveSSA (Nolock) )
    Begin
		If @FolioCierre = ''         		 
		Begin 
			Select @FolioCierre = cast( (max(FolioCierre) + 1) as varchar)  From Ctl_CierresGeneral (NoLock)
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		--End 

		-- Asegurar que FolioCierre sea valido 
		Set @FolioCierre = IsNull(@FolioCierre, '1')
		Set @FolioCierre = right('00000000000000' + @FolioCierre, 8)  	 

	    
	---     select @FolioCierre as Folio  

		If Not Exists ( Select * From Ctl_CierresGeneral (NoLock) 
						Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = @FolioCierre ) 
			Begin
				Select @FechaInicial = Min(FechaRegistro), @FechaFinal = Max(FechaRegistro) 
				From MovtosInv_Enc (Nolock)
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = 0					    
			    
				Insert Into Ctl_CierresGeneral 
				(   
					IdEmpresa, IdEstado, IdFarmacia, FolioCierre, IdPersonal, IdEstadoRegistra, IdFarmaciaRegistra, 
					IdPersonalRegistra, FechaRegistro, FechaInicial, FechaFinal, Status, Actualizado, FechaControl
				) 
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioCierre, @IdPersonal, @IdEstadoRegistra, @IdFarmaciaRegistra, @IdPersonalRegistra,
				getdate(), @FechaInicial, @FechaFinal, @sStatus, @iActualizado, GETDATE()
			    
				--------  se actualiza el folio cierre de la tabla de movtos  -----------------------------------
				Update MovtosInv_Enc Set FolioCierre = @FolioCierre, Cierre = 1
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = 0 		    	
				---------------------------------------------------------------------------------------------------------	    
				
				------------------------------  se actualizan las claves en caso de que tengan alguna clave relacionada  -----------
				--Update T Set T.IdClaveSSA_Sal = P.IdClaveSSA
				--From #tmpMovtos_ClaveSSA T (Nolock)
				--Inner Join vw_Relacion_ClavesSSA_Claves P (Nolock) On ( P.IdClaveSSA_Relacionada = T.IdClaveSSA_Sal and P.IdEstado = @IdEstado )
				--------------------------------------------------------------------------------------------------------------------------------
				
				-----------  se genera la tabla final para llenar el detalle del cierre  ------------------------------------------------
				Select T.IdTipoMovto, T.IdClaveSSA_Sal, T.Piezas, T.ImporteInv, Cast(IsNull((AVG(C.PrecioUnitario)), 0 ) As Numeric(14,4)) as Precio
				Into #tmpMovtos_ClaveSSA_Final
				From #tmpMovtos_ClaveSSA T (Nolock)
				Left Join vw_Claves_Precios_Asignados C (Nolock) On ( C.IdClaveSSA = T.IdClaveSSA_Sal and C.IdEstado = @IdEstado )
				Group By T.IdTipoMovto, T.IdClaveSSA_Sal, T.Piezas, T.ImporteInv
			    
				--------------------------------------------------------------------------------------------------------------------------
			    
				-------------  insertamos el detalle del cierre -------------------------------------------------------------------------
				Insert Into Ctl_CierresGeneralDetalles
				(   
					IdEmpresa, IdEstado, IdFarmacia, FolioCierre, IdTipoMovto_Inv, Claves, Piezas, Importe_Licitacion, Importe_Inventario
				)
				Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @FolioCierre as FolioCierre, IdTipoMovto, 
				Count(IdClaveSSA_Sal) as Claves, sum(Piezas) as Piezas, sum((Piezas * Precio)) as ImporteLicitacion,
				SUM(ImporteInv) AS ImporteInventario
				From  #tmpMovtos_ClaveSSA_Final
				Group By IdTipoMovto
				--------------------------------------------------------------------------------------------------------------------------
			    
			    -----------------  se inicia el proceso para llenar la tabla del detalle de existencia del folio cierre  -----------------
			    
			    ---------------- Claves Venta  ------------------------------------------------------------------
			    Select P.IdClaveSSA_Sal, P.TipoDeClave, 1 as TipoInsumo, Sum(Existencia) as Existencia_Venta, 0 as Existencia_Consigna,
			    Sum((E.Existencia * E.UltimoCosto)) as Importe_Venta, cast( 0 as Numeric(14, 4)) as Importe_Consigna,			    
			    cast( 0 as Numeric(14, 4)) as Precio,
			    0 As TipoDispensacion
				Into #tmpClavesSSA_Existencia
				From FarmaciaProductos_CodigoEAN_Lotes E (Nolock)
				Inner Join #tmp_vw_Productos_CodigoEAN P (Nolock) On ( P.IdProducto = E.IdProducto and P.CodigoEAN = E.CodigoEAN )
				Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and P.TipoDeClave in ('01', '02') 
				and ClaveLote Not Like '%*%' 
				Group By P.IdClaveSSA_Sal, P.TipoDeClave
				Order By P.TipoDeClave, P.IdClaveSSA_Sal				
				
				---------------- Claves Consignacion  ------------------------------------------------------------------
				Insert Into #tmpClavesSSA_Existencia
				Select P.IdClaveSSA_Sal, P.TipoDeClave, 1 as TipoInsumo, 0 as Existencia_Venta, Sum(Existencia) as Existencia_Consigna,
				cast( 0 as Numeric(14, 4)) as Importe_Venta,  Sum((E.Existencia * E.UltimoCosto)) as Importe_Consigna,
				cast( 0 as Numeric(14, 4)) as Precio, 1				  
				From FarmaciaProductos_CodigoEAN_Lotes E (Nolock)
				Inner Join #tmp_vw_Productos_CodigoEAN P (Nolock) On ( P.IdProducto = E.IdProducto and P.CodigoEAN = E.CodigoEAN )
				Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and P.TipoDeClave in ('01', '02') 
				and ClaveLote Like '%*%' 
				Group By P.IdClaveSSA_Sal, P.TipoDeClave
				Order By P.TipoDeClave, P.IdClaveSSA_Sal	
				
				--------  SE ACTUALIZA EL PRECIO DE LA CLAVE   ---------------------------------------------------------------
				Update T Set T.Precio = ( Select IsNull((AVG(C.PrecioUnitario)), 0 ) From vw_Claves_Precios_Asignados C (Nolock) 
											Where C.IdClaveSSA = T.IdClaveSSA_Sal and C.IdEstado = @IdEstado ) 
				From #tmpClavesSSA_Existencia T (Nolock)		
				
				---- Se marca el medicamento que NO es Seguro Popular  ----------------------------------------------------------------------------------
				Update T Set T.TipoInsumo = 2
				From #tmpClavesSSA_Existencia T (Nolock)
				Where T.TipoDeClave = '02' and Not Exists ( Select C.IdClaveSSA From CatClavesSSA_SeguroPopular C Where C.IdClaveSSA = T.IdClaveSSA_Sal )
				
				---- Se marca el Material de Curación ---------------------------------------------------------------------------------------------------
				Update T Set T.TipoInsumo = 3
				From #tmpClavesSSA_Existencia T (Nolock)
				Where T.TipoDeClave = '01'	
				
												
				---------  Se genera la Tabla Final de la Existencia  -----------------------------------------------------------------------------------
				Select T.TipoInsumo, 
				( Select count(IdClaveSSA_Sal) From #tmpClavesSSA_Existencia E (Nolock) Where E.TipoInsumo = T.TipoInsumo And E.TipoDispensacion = 0 ) As Claves_Venta,
				( Select count(IdClaveSSA_Sal) From #tmpClavesSSA_Existencia E (Nolock) Where E.TipoInsumo = T.TipoInsumo And E.TipoDispensacion = 1 ) As Claves_Consigna,
				Sum(Existencia_Venta) as Existencia_Venta, Sum(Existencia_Consigna) as Existencia_Consigna,
				Sum(Importe_Venta) as Importe_Venta, Sum(Importe_Consigna) as Importe_Consigna,
				Sum(((Existencia_Venta + Existencia_Consigna) * Precio)) as Importe_Licitacion,
				Sum((Importe_Venta + Importe_Consigna) ) as Importe_Inventario
				Into #tmpExistenciaFinal
				From #tmpClavesSSA_Existencia T (Nolock)
				Group By TipoInsumo
				
				---- Se Inserta en la Tabla de detalles de existencia del folio cierre ---------------------------------------------------
			    Insert Into Ctl_CierresGeneralDet_Existencia
				(   
					IdEmpresa, IdEstado, IdFarmacia, FolioCierre, TipoInsumo, 
					Claves_Venta, Claves_Consigna,
					Existencia_Venta, Existencia_Consigna,
					Importe_Venta, Importe_Consigna, Importe_Licitacion, Importe_Inventario
				)
				Select @IdEmpresa as IdEmpresa, @IdEstado as IdEstado, @IdFarmacia as IdFarmacia, @FolioCierre as FolioCierre, TipoInsumo,
				Claves_Venta, Claves_Consigna, 
				Existencia_Venta, Existencia_Consigna,
				Importe_Venta, Importe_Consigna, Importe_Licitacion, Importe_Inventario
				From  #tmpExistenciaFinal
			    --------------------------------------------------------------------------------------------------------------------------
				
				-- Set @sMensaje = 'La información del Folio se Genero Satisfactoriamente ' 
				Set @sMensaje = 'El cierre General se generó satisfactoriamente con el folio << ' + @FolioCierre + ' >> ' 
			End	    


		End
	End
    -- Regresar el Folio Generado
    Select @FolioCierre as Folio, @sMensaje as Mensaje 
	
	
	-----			spp_Mtto_Ctl_CierresGeneral
	
End 
Go--#SQL
   
