 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INV_Rpt_DetalladoTransferencias' And xType = 'P' )
	Drop Proc spp_INV_Rpt_DetalladoTransferencias
Go--#SQL

Create Procedure spp_INV_Rpt_DetalladoTransferencias 
(	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0010',
    @FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2010-10-01', 	
	-- @Anio varchar(4) = '2010', @Mes varchar(2) = '01', 
	@TipoTransf smallint = 1, @TipoInsumo tinyint = 0, @TipoDispensacion tinyint = 0, @ClaveLote varchar(30) = '' )
With Encryption 
As
Begin
Set Dateformat YMD 
Set NoCount On 
Declare 
	@iMedicamento tinyint, 
	@iMatCuracion tinyint, 
	@iDispVenta tinyint, 
	@iDispConsignacion tinyint


	-- Vefificar la información solicitada 
	Set @iMedicamento = 1 
	Set @iMatCuracion = 2 
	
	if @TipoInsumo = 1 
	   Set @iMatCuracion = 1
	
	if @TipoInsumo = 2 
	   Set @iMedicamento = 2 

	--------------------------------------- 	   
	Set @iDispConsignacion = 1 
	Set @iDispVenta = 2 

	If @TipoDispensacion = 1 
	   Set @iDispVenta = 1 	

	If @TipoDispensacion = 2 
	   Set @iDispConsignacion = 2 

	----  Si es tipo = 1 ; significa transferencias de entrada
	if @TipoTransf = 1
		Begin
		---------------------- Script para las transferencias de ENTRADA ------------------------------------------------------
			Select * Into #tmpEntrada
			From
			(
				Select	E.IdEstado, E.Estado, E.IdFarmacia As IdFarmaciaRecibe , E.Farmacia As FarmaciaRecibe, E.Folio, E.FechaReg, E.IdPersonal, 
						E.NombrePersonal, E.IdFarmaciaRecibe As IdFarmaciaEnvia, E.FarmaciaRecibe As FarmaciaEnvia, D.Status, P.ClaveSSA, 
						P.DescripcionSal, D.CodigoEAN,
						(Case When P.IdTipoProducto = '02' Then 1 Else 2 End) as TipoInsumo, 
						(Case When P.IdTipoProducto = '02' Then 'MEDICAMENTO' Else 'MATERIAL DE CURACION' End) as TipoDeInsumo,
 						D.IdProducto,
						(Case When P.EsControlado = 1 Then 'SI' Else 'NO' End ) As EsControlado,  
						P.Descripcion, D.ClaveLote,
						(Case When D.ClaveLote Like '%*%' Then 1 Else 2 End) as EsConsignacion, 
						(Case When D.ClaveLote Like '%*%' Then 'CONSIGNACION' Else 'VENTA' End) as EsConsignacionDesc,
						D.FechaCad, D.Cantidad
				From vw_TransferenciasEnc E (Nolock)
				INNER JOIN vw_TransferenciaDet_CodigosEAN_Lotes D (Nolock)
					ON ( E.IdEmpresa = D.IdEmpresa  And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio )
				INNER JOIN vw_Productos P (Nolock)
					ON ( D.IdProducto = P.IdProducto )	
				Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.TipoTransferencia = 'TE' 
				-- And DatePart(yy, E.FechaReg) = @Anio And DatePart(mm,E.FechaReg) = @Mes 
				and convert(varchar(10), E.FechaReg, 120) Between @FechaInicial and @FechaFinal 
				And P.EsControlado = 1
				--Order By E.Folio
			) As T
			Where TipoInsumo In ( @iMedicamento, @iMatCuracion ) 
			And EsConsignacion In ( @iDispConsignacion, @iDispVenta )
			Order By T.Folio
			
			If @ClaveLote <> '' 
			  Begin
				Select * From #tmpEntrada(NoLock) Where ClaveLote = @ClaveLote Order By Folio
			  End
			Else
			  Begin
				Select * From #tmpEntrada(NoLock) Order By Folio
			  End
		-----------------------------------------------------------------------------------------------------------------------
		End

	----  Si es tipo = 2 ; significa transferencias de salida
	else
		Begin
		---------------------- Script para las transferencias de Salida ------------------------------------------------------
			Select * Into #tmpSalida
			From
			(
				Select	E.IdEstado, E.Estado, E.IdFarmacia As IdFarmaciaEnvia, E.Farmacia As FarmaciaEnvia, E.Folio, E.FechaReg, E.IdPersonal, 
						E.NombrePersonal, E.IdFarmaciaRecibe, E.FarmaciaRecibe, D.Status, P.ClaveSSA, P.DescripcionSal, D.CodigoEAN, 
						(Case When P.IdTipoProducto = '02' Then 1 Else 2 End) as TipoInsumo, 
						(Case When P.IdTipoProducto = '02' Then 'MEDICAMENTO' Else 'MATERIAL DE CURACION' End) as TipoDeInsumo,
 						D.IdProducto, 
						(Case When P.EsControlado = 1 Then 'SI' Else 'NO' End ) As EsControlado, 
						P.Descripcion, D.ClaveLote,
						(Case When D.ClaveLote Like '%*%' Then 1 Else 2 End) as EsConsignacion, 
						(Case When D.ClaveLote Like '%*%' Then 'CONSIGNACION' Else 'VENTA' End) as EsConsignacionDesc,
						D.FechaCad, D.Cantidad
				From vw_TransferenciasEnc E (Nolock)
				INNER JOIN vw_TransferenciaDet_CodigosEAN_Lotes D (Nolock)
					ON ( E.IdEmpresa = D.IdEmpresa  And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.Folio = D.Folio )
				INNER JOIN vw_Productos P (Nolock)
					ON ( D.IdProducto = P.IdProducto )	
				Where E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.TipoTransferencia = 'TS' 
				-- And DatePart(yy,E.FechaReg) = @Anio And DatePart(mm,E.FechaReg) = @Mes 
				and convert(varchar(10), E.FechaReg, 120) Between @FechaInicial and @FechaFinal 			
				And P.EsControlado = 1
				--Order By E.Folio
			) As T
			Where TipoInsumo In ( @iMedicamento, @iMatCuracion ) 
			And EsConsignacion In ( @iDispConsignacion, @iDispVenta )
			Order By T.Folio

			If @ClaveLote <> '' 
			  Begin
				Select * From #tmpSalida(NoLock) Where ClaveLote = @ClaveLote Order By Folio
			  End
			Else
			  Begin
				Select * From #tmpSalida(NoLock) Order By Folio
			  End
	------------------------------------------------------------------------------------------------------------------------------
		End


END
 
Go--#SQL
