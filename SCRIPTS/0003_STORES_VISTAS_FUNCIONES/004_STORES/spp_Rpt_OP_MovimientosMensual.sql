
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_OP_MovimientosMensual' and xType = 'P' ) 
   Drop Proc spp_Rpt_OP_MovimientosMensual
Go--#SQL 

Create Proc spp_Rpt_OP_MovimientosMensual
( 
	@IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', @Año varchar(4) = '2013', @Mes Varchar(2) = '07', @TipoReporte Int = 0
) 
With Encryption 
As 
Begin 
Set NoCount On


--- Crear Base
	Select Distinct(E.IdEstado), M.Idfarmacia, ClaveSSA, DescripcionCortaClave, Presentacion_ClaveSSA As Presentacion, ContenidoPaquete_ClaveSSA As ContenidoPaquete
	Into #Base1
	From MovtosInv_Enc E (NoLock)
	Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
		On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
	Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
	Where M.IdEstado = @IdEstado And M.IdFarmacia = @IdFarmacia And DATEPART(yyyy, FechaRegistro) = @Año And DATEPART(MM, FechaRegistro) = @Mes And 
			IdTipoMovto_Inv In ('II', 'IIC', 'EPC', 'EOC', 'EC', 'SV', 'EAI', 'SAI', 'EE', 'SE')
			
	Insert Into #Base1	
	Select Distinct(E.IdEstado), M.Idfarmacia, ClaveSSA, DescripcionCortaClave, Presentacion_ClaveSSA As Presentacion, ContenidoPaquete_ClaveSSA As ContenidoPaquete
	From TransferenciasEnc E (NoLock)
	Inner Join TransferenciasDet M (NoLock)
		On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioTransferencia = M.FolioTransferencia)
	Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
	Where E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And
		DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
			
	Select Distinct(E.IdEstado), E.Idfarmacia, ClaveSSA, DescripcionCortaClave As Descripcion, Presentacion, ContenidoPaquete,
				Cast(0 As int) As II, Cast(0 As int) As IIC, Cast(0 As int) As EPC, Cast(0 As int) As EOC, Cast(0 As int) As EC,
				Cast(0 As int) As TT, Cast(0 As int) As TA, Cast(0 As int) As TE, Cast(0 As int) As SV, Cast(0 As int) As EAI, Cast(0 As int) As SAI,
				Cast(0 As int) As EE, Cast(0 As int) As SE
	Into #Base
	From #Base1 E

	--Borrar Las no deseadas
	If @TipoReporte <> 0
		Begin
			If @TipoReporte = 1
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



	--- LLenar las II
	Update B
	Set II = IsNull((Select Sum(Cantidad)
				From MovtosInv_Enc E (NoLock)
				Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
					On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
				Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
				Where MovtoAplicado = 's' And E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
					 And E.IdTipoMovto_Inv = 'II' And P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B

	--- LLenar las IIC
	Update B
	Set IIC = IsNull((Select Sum(Cantidad)
				From MovtosInv_Enc E (NoLock)
				Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
					On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
				Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
				Where MovtoAplicado = 'S' And E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
					 And E.IdTipoMovto_Inv = 'IIC' And P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B

	--- LLenar las EPC
	Update B
	Set EPC = IsNull((Select Sum(Cantidad)
				From MovtosInv_Enc E (NoLock)
				Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
					On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
				Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
				Where MovtoAplicado = 'S' And E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
					 And E.IdTipoMovto_Inv = 'EPC' And P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B

	--- LLenar las OC
	Update B
	Set EOC = IsNull((Select Sum(Cantidad)
				From MovtosInv_Enc E (NoLock)
				Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
					On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
				Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
				Where MovtoAplicado = 'S' And E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
					And E.IdTipoMovto_Inv = 'EOC' And P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B

	--- LLenar las EC
	Update B
	Set EC = IsNull((Select Sum(Cantidad)
				From MovtosInv_Enc E (NoLock)
				Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
					On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
				Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
				Where MovtoAplicado = 'S' And E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
					And E.IdTipoMovto_Inv = 'EC' And P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B

	--- LLenar las SV
	Update B
	Set SV = IsNull((Select Sum(Cantidad)
				From MovtosInv_Enc E (NoLock)
				Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
					On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
				Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
				Where MovtoAplicado = 'S' And E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
					And E.IdTipoMovto_Inv = 'SV' And P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B

	--- LLenar las EAI
	Update B
	Set EAI = IsNull((Select Sum(Cantidad)
				From MovtosInv_Enc E (NoLock)
				Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
					On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
				Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
				Where MovtoAplicado = 'S' And E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
					And E.IdTipoMovto_Inv = 'EAI' And P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B

	--- LLenar las SAI
	Update B
	Set SAI = IsNull((Select Sum(Cantidad)
				From MovtosInv_Enc E (NoLock)
				Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
					On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
				Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
				Where MovtoAplicado = 'S' And E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
					And E.IdTipoMovto_Inv = 'SAI' And P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B

	--- LLenar las EE
	Update B
	Set EE = IsNull((Select Sum(Cantidad)
				From MovtosInv_Enc E (NoLock)
				Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
					On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
				Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
				Where MovtoAplicado = 'S' And E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
					And E.IdTipoMovto_Inv = 'EE' And P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B

	--- LLenar las EE
	Update B
	Set SE = IsNull((Select Sum(Cantidad)
				From MovtosInv_Enc E (NoLock)
				Inner Join MovtosInv_Det_CodigosEAN M (NoLock)
					On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioMovtoInv = M.FolioMovtoInv)
				Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
				Where MovtoAplicado = 'S' And E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes
					And E.IdTipoMovto_Inv = 'SE' And P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B


	-- LLENAR TT (Transferecias en Transito)
	Update B
	Set TT = IsNull((Select Sum(M.CantidadEnviada)
					From TransferenciasEnc E (NoLock)
					Inner Join TransferenciasDet M (NoLock)
						On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioTransferencia = M.FolioTransferencia)
					Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
					Where E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And E.TipoTransferencia = 'TS' And
						DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes And
						P.ClaveSSA = B.ClaveSSA And E.TransferenciaAplicada = 0), 0)
	From #Base B

	-- LLENAR TA (Transferecias Aplicadas)
	Update B
	Set TA = IsNull((Select Sum(M.CantidadEnviada)
					From TransferenciasEnc E (NoLock)
					Inner Join TransferenciasDet M (NoLock)
						On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioTransferencia = M.FolioTransferencia)
					Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
					Where E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And E.TipoTransferencia = 'TS' And
						DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes And
						P.ClaveSSA = B.ClaveSSA And E.TransferenciaAplicada = 1), 0)
	From #Base B

	-- LLENAR TA (Transferecias Aplicadas)
	Update B
	Set TE = IsNull((Select Sum(M.CantidadEnviada)
					From TransferenciasEnc E (NoLock)
					Inner Join TransferenciasDet M (NoLock)
						On (E.IdEmpresa = M.IdEmpresa And E.IdEstado = M.IdEstado And E.IdFarmacia = M.IdFarmacia And E.FolioTransferencia = M.FolioTransferencia)
					Inner Join vw_Productos_CodigoEAN P (Nolock) On (M.CodigoEAN = P.CodigoEAN)
					Where E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And E.TipoTransferencia = 'TE' And
						DATEPART(yyyy, E.FechaRegistro) = @Año And DATEPART(MM, E.FechaRegistro) = @Mes And
						P.ClaveSSA = B.ClaveSSA ), 0)
	From #Base B

	-- Salida Final
	Select * From #Base

End 
Go--#SQL 