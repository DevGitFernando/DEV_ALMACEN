If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_PedidosCEDIS_CantidadSugerida' and xType = 'P' )
   Drop proc spp_PedidosCEDIS_CantidadSugerida
Go--#SQL 

Create Proc spp_PedidosCEDIS_CantidadSugerida (  @IdEmpresa  Varchar(3)= '001', @Idestado Varchar(2)= '11', @IdFarmacia Varchar(4) = '0012', @Dias int = 180 ) 
With Encryption 
As 
Begin 
	Set dateformat YMD
	Set NoCount On
	
	Declare @TotalTiket Numeric(14, 8),
	@Acumulado Numeric(14, 8),
	@ClaveSSA varchar(200),
	@Porcentaje numeric(14, 8)


	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, ClaveSSA, COUNT(Distinct(D.FolioVenta)) As Folios, Cast(0 As numeric(14, 8)) as PorcentajeFolios, Cast(0 As numeric(14, 8)) As AcumuladoFolios,
		Sum(CantidadVendida) As Piezas, Cast(0 As numeric(15, 12)) as PiezasDiarias
	Into #Tmp_Folios
	From VentasDet D (NoLock)
	inner Join VentasEnc E (NoLock) On (D.IdEmpresa = E.IdEmpresa And D.IdEstado = E.IdEstado And D.IdFarmacia = E.IdFarmacia And D.FolioVenta = E.FolioVenta)
	Inner Join vw_Productos_CodigoEAN P(NoLock) On (D.CodigoEAN = P.CodigoEAN)
	Where  D.IdEmpresa = @IdEmpresa And D.Idestado = @Idestado And D.IdFarmacia = @IdFarmacia And
			CantidadVendida > 0 And	DATEDIFF(DD, E.FechaRegistro, GETDATE()) <= @Dias
	Group BY E.IdEmpresa, E.IdEstado, E.IdFarmacia, ClaveSSA
	Order BY Folios Desc
	
	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, ClaveSSA, COUNT(Distinct(E.FolioVenta)) As FoliosNoEntregados, Sum(CantidadRequerida - CantidadEntregada) As CantidadNoEntregada
	Into #TempNoEntregado
	From VentasEnc E (NoLock)
	Inner Join VentasEstadisticaClavesDispensadas V (NoLock)
		On (E.IdEmpresa = V.IdEmpresa And E.IdEstado = V.IdEstado And E.IdFarmacia = V.IdFarmacia And E.FolioVenta = V.FolioVenta)
	Inner Join vw_ClavesSSA_Sales S (NoLock) On (V.IdClaveSSA = S.IdClaveSSA_sal)
		Where  V.IdEmpresa = @IdEmpresa And V.Idestado = @Idestado And V.IdFarmacia = @IdFarmacia And
			(CantidadRequerida - CantidadEntregada) > 0 And	DATEDIFF(DD, E.FechaRegistro, GETDATE()) <= @Dias			 
	Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia, ClaveSSA
	
	Insert #Tmp_Folios
	Select N.IdEmpresa, N.IdEstado, N.IdFarmacia, N.ClaveSSA, 0 As Folios, 0 as PorcentajeFolios, 0 As AcumuladoFolios, 0  As Piezas, 0 as PiezasDiarias
	From #Tmp_Folios D
	Right Join #TempNoEntregado N  On (D.ClaveSSA = N.ClaveSSA)
	Where D.ClaveSSA is Null
	
	Update D
	Set Folios = Folios + N.FoliosNoEntregados, D.Piezas = D.Piezas + N.CantidadNoEntregada
	From #Tmp_Folios D
	Inner Join #TempNoEntregado N  On (D.ClaveSSA = N.ClaveSSA)
	
	Select @TotalTiket = SUM(Folios) From #Tmp_Folios	
	
	Update #Tmp_Folios Set PorcentajeFolios = (Folios * 100) / @TotalTiket
	Update #Tmp_Folios Set PiezasDiarias = Piezas / @Dias
	
	Set @Acumulado = 0
	
	Declare tmp cursor Local For 
		Select ClaveSSA, PorcentajeFolios From #Tmp_Folios Order By PorcentajeFolios
	OPEN tmp 
		FETCH NEXT FROM tmp INTO @ClaveSSA, @Porcentaje
		WHILE ( @@FETCH_STATUS = 0 ) 
			BEGIN
				Set @Acumulado = @Acumulado + @Porcentaje
				Update #Tmp_Folios Set AcumuladoFolios = @Acumulado Where ClaveSSA = @ClaveSSA
				FETCH NEXT FROM tmp INTO @ClaveSSA, @Porcentaje
			End
	CLOSE tmp 
    DEALLOCATE tmp
    
	If Exists(Select * From Sysobjects Where name = 'PedidosCEDIS_CantidadSugerida' And xtype = 'U')
		Drop Table PedidosCEDIS_CantidadSugerida

    Select *,
    CEILING((
			Case
				When AcumuladoFolios <= 20 Then PiezasDiarias * 15
				When (AcumuladoFolios > 20 And AcumuladoFolios <= 60) Then PiezasDiarias * 30
				When AcumuladoFolios > 60 Then PiezasDiarias * 60
			End
			)) As CantidadRecomendada,
	GETDATE() As FechaRegistro
	Into PedidosCEDIS_CantidadSugerida
    From #Tmp_Folios Order By AcumuladoFolios
    
End 
Go--#SQL 