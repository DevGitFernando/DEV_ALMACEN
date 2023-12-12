

If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_RemisionesDist_Vta_Admon_Mensuales' and xType = 'P') 
    Drop Proc spp_Rpt_RemisionesDist_Vta_Admon_Mensuales
Go--#SQL 
  
--  Exec spp_Rpt_RemisionesDist_Vta_Admon_Mensuales '001', '21', '0182', '0001', 0, '2012-05-01', '2012-05-01', 9.49 
  
Create Proc spp_Rpt_RemisionesDist_Vta_Admon_Mensuales 
(   
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0182', 
	@IdDistribuidor varchar(4) = '0001', @EsConsignacion tinyint = 1, 
	@FechaIni varchar(10) = '2012-05-01', @FechaFin varchar(10) = '2012-05-06', @PrecioAdmon numeric(14, 4) = 9.4900
) 
With Encryption 
As 
Begin 
	Declare @SubTotal numeric(14, 4), @Iva numeric(14, 4), @Total numeric(14, 4), @Piezas int, @Cajas int

	Set NoCount On 
	Set DateFormat YMD
	Set @SubTotal = 0 
	Set @Iva = 0
	Set @Total = 0
	Set @Piezas = 0
	Set @Cajas = 0
	
	
	If @EsConsignacion = 0   ---- REMISIONES DE VENTA
		Begin
			---- Concentrado Remisiones
			Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdDistribuidor, Distribuidor,
			Folio, ReferenciaPedido as Remision, CodigoCliente, Cliente, IdFarmaciaRelacionada, FarmaciaRelacionada, FechaDocumento,
			Sum(CantidadRecibida * ContenidoPaquete) As CantidadPiezas, 
			Sum(CantidadRecibida) As CantidadCajas, Sum(CantidadRecibida * Precio) As Importe, 
			Cast(0 as numeric(14, 4)) as SubTotal, Cast(0 as numeric(14, 4)) as Iva, Cast(0 as numeric(14, 4)) as Total
			Into #tmpRemisionesConcentradoVta	
			From vw_Impresion_RemisionesDistribuidor (Nolock)
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor 
			and EsConsignacion = @EsConsignacion and Convert(varchar(10), FechaDocumento, 120) Between @FechaIni and @FechaFin
			and FolioCargaMasiva = 0 and Status <> 'C'
			Group By IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdDistribuidor, Distribuidor,
			Folio, ReferenciaPedido, CodigoCliente, Cliente, IdFarmaciaRelacionada, FarmaciaRelacionada, 
			FechaDocumento	
			Order By Folio, FechaDocumento

			---- Detallado Remisiones
			Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdDistribuidor, Distribuidor,
			Folio, ReferenciaPedido as Remision, CodigoCliente, Cliente, IdFarmaciaRelacionada, FarmaciaRelacionada, 
			FechaDocumento, ClaveSSA, DescripcionSal, (Precio / ContenidoPaquete) as PrecioUnitario,
			(CantidadRecibida * ContenidoPaquete) as CantidadPiezas, CantidadRecibida As CantidadCajas, 
			(CantidadRecibida * Precio) As Importe, 
			Cast(0 as numeric(14, 4)) as SubTotal, Cast(0 as numeric(14, 4)) as Iva, Cast(0 as numeric(14, 4)) as Total
			Into #tmpRemisionesDetalladoVta	
			From vw_Impresion_RemisionesDistribuidor (Nolock)
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor 
			and EsConsignacion = @EsConsignacion and Convert(varchar(10), FechaDocumento, 120) Between @FechaIni and @FechaFin
			and FolioCargaMasiva = 0 and Status <> 'C'		
			Order By Folio, FechaDocumento, ClaveSSA
		End
	Else  ---- REMISIONES DE ADMON
		Begin 
			---- Concentrado Remisiones
			Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdDistribuidor, Distribuidor,
			Folio, ReferenciaPedido as Remision, CodigoCliente, Cliente, IdFarmaciaRelacionada, FarmaciaRelacionada, 
			FechaDocumento, Sum(CantidadRecibida) As Cantidad, Sum(CantidadRecibida * @PrecioAdmon) As Importe, 
			Cast(0 as numeric(14, 4)) as SubTotal, Cast(0 as numeric(14, 4)) as Iva, Cast(0 as numeric(14, 4)) as Total
			Into #tmpRemisionesConcentradoAdmon	
			From vw_Impresion_RemisionesDistribuidor (Nolock)
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor 
			and EsConsignacion = @EsConsignacion and Convert(varchar(10), FechaDocumento, 120) Between @FechaIni and @FechaFin
			and FolioCargaMasiva = 0  and Status <> 'C'
			Group By IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdDistribuidor, Distribuidor,
			Folio, ReferenciaPedido, CodigoCliente, Cliente, IdFarmaciaRelacionada, FarmaciaRelacionada, 
			FechaDocumento	
			Order By Folio, FechaDocumento
			---- Detallado Remisiones
			Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, IdDistribuidor, Distribuidor,
			Folio, ReferenciaPedido as Remision, CodigoCliente, Cliente, IdFarmaciaRelacionada, FarmaciaRelacionada, 
			FechaDocumento, ClaveSSA, DescripcionSal, CantidadRecibida As Cantidad, (CantidadRecibida * @PrecioAdmon) As Importe, 
			Cast(0 as numeric(14, 4)) as SubTotal, Cast(0 as numeric(14, 4)) as Iva, Cast(0 as numeric(14, 4)) as Total
			Into #tmpRemisionesDetalladoAdmon	
			From vw_Impresion_RemisionesDistribuidor (Nolock)
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdDistribuidor = @IdDistribuidor 
			and EsConsignacion = @EsConsignacion and Convert(varchar(10), FechaDocumento, 120) Between @FechaIni and @FechaFin
			and FolioCargaMasiva = 0 and Status <> 'C'	
			Order By Folio, FechaDocumento, ClaveSSA
		End
	
	If @EsConsignacion = 0
		Begin
			Set @Piezas = (Select Sum(CantidadPiezas) From #tmpRemisionesConcentradoVta (Nolock))
			Set @Cajas = (Select Sum(CantidadCajas) From #tmpRemisionesConcentradoVta (Nolock))
			Set @SubTotal = (Select Sum(Importe) From #tmpRemisionesConcentradoVta (Nolock))
			Set @Total = @SubTotal
			Update #tmpRemisionesConcentradoVta Set SubTotal = @SubTotal, Total = @Total
			Update #tmpRemisionesDetalladoVta Set SubTotal = @SubTotal, Total = @Total
		End
	Else
		Begin
			Set @Piezas = (Select Sum(Cantidad) From #tmpRemisionesConcentradoAdmon (Nolock))
			Set @SubTotal = (Select (Sum(Importe)/1.16) From #tmpRemisionesConcentradoAdmon (Nolock))
			Set @Iva = (Select (Sum(Importe) - (Sum(Importe)/1.16)) From #tmpRemisionesConcentradoAdmon (Nolock))
			Set @Total = @SubTotal + @Iva
			Update #tmpRemisionesConcentradoAdmon Set SubTotal = @SubTotal, Iva = @Iva, Total = @Total
			Update #tmpRemisionesDetalladoAdmon Set SubTotal = @SubTotal, Iva = @Iva, Total = @Total
		End	

	If @EsConsignacion = 0
		Begin
			Select *, @Piezas as Piezas, @Cajas as Cajas From #tmpRemisionesConcentradoVta (Nolock)
			Select *, @Piezas as Piezas, @Cajas as Cajas From #tmpRemisionesDetalladoVta (Nolock)
		End
	Else
		Begin
			Select *, @Piezas as Piezas From #tmpRemisionesConcentradoAdmon (Nolock)
			Select *, @Piezas as Piezas From #tmpRemisionesDetalladoAdmon (Nolock)	
		End
End
Go--#SQL 