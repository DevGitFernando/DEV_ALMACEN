
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_OP_EntradasConsignacion' and xType = 'P' ) 
   Drop Proc spp_Rpt_OP_EntradasConsignacion
Go--#SQL 

Create Proc spp_Rpt_OP_EntradasConsignacion
( 
	@IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003',
	@FechaInicial varchar(10) = '2013-01-01', @FechaFinal varchar(10) = '2014-12-31', @TipoClave Int = 1, @TipoReporte Int = 2
) 
With Encryption 
As 
Begin
Set dateFormat YMD
Set NoCount On


	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioEntrada, E.FechaRegistro, E.ReferenciaPedido, E.Observaciones,
		   D.IdProducto, D.CodigoEAN, P.DescripcionCorta, P.ClaveSSA, DescripcionSal, L.ClaveLote,  L.Cant_Recibida, L.Cant_Devuelta, L.CantidadRecibida
	Into #Base
	From EntradasEnc_Consignacion E (NoLock)
	Inner Join EntradasDet_Consignacion D (NoLock)
		On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioEntrada = D.FolioEntrada)
	Inner Join EntradasDet_Consignacion_Lotes L (NoLock)
		On (D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioEntrada = L.FolioEntrada And D.CodigoEAN = L.CodigoEAN)
	Inner Join vw_Productos_CodigoEAN P (NoLock) On (D.IdProducto = P.IdProducto And D.CodigoEAN = P.CodigoEAN)
	Where E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.FechaRegistro Between @FechaInicial And @FechaFinal -- And Cant_Devuelta > 0

	--Borrar Las no deseadas
	If @TipoClave <> 0
		Begin
			If @TipoClave = 1
				Begin
					Delete B
					From #Base B
					Where ClaveSSA Not in ( Select ClaveSSA From vw_Claves_Precios_Asignados A (NoLock) Where B.Idestado = A.IdEstado And A.Precio > 0)
				End
			Else
				Begin
					Delete B
					From #Base B
					Where ClaveSSA in ( Select ClaveSSA From vw_Claves_Precios_Asignados A (NoLock) Where B.Idestado = A.IdEstado And A.Precio > 0)
				End
		End
		
			Select Top 0 Convert(Varchar(10), FechaRegistro, 120) As Fecha, FolioEntrada As 'Folio Entrada', ReferenciaPedido As Referencia, Observaciones,
				   CodigoEAN, DescripcionCorta As Descripción, ClaveSSA As 'Clave SSA', DescripcionSal As 'Descripción Sal', ClaveLote As Lote, CantidadRecibida As Cantidad
			Into #Detalle
			From #Base
	
	If @TipoReporte = 1
		Begin
			Delete B
			From #Base B
			Where CantidadRecibida = 0
			
			Insert Into #Detalle
			Select Convert(Varchar(10), FechaRegistro, 120) As Fecha, FolioEntrada As 'Folio Entrada', ReferenciaPedido As Referencia, Observaciones,
				   CodigoEAN, DescripcionCorta As Descripción, ClaveSSA As 'Clave SSA', DescripcionSal As 'Descripción Sal', ClaveLote As Lote, CantidadRecibida As Cantidad
			From #Base
			
		End
	Else
		Begin
			Delete B
			From #Base B
			Where Cant_Devuelta = 0
			
			Insert Into #Detalle
			Select Convert(Varchar(10), FechaRegistro, 120) As Fecha, FolioEntrada, ReferenciaPedido, Observaciones,
				   CodigoEAN, DescripcionCorta As Descripción, ClaveSSA As 'Clave SSA', DescripcionSal As 'Descripción Sal', ClaveLote As Lote, Cant_Devuelta As Cantidad
			From #Base			
		End
	
	Select Fecha, [Folio Entrada] , Referencia, Observaciones, Cast(SUM(Cantidad) As Int) As Piezas
	From #Detalle
	Group By Fecha, [Folio Entrada], Referencia, Observaciones
	
	Select * From #Detalle

End 
Go--#SQL 