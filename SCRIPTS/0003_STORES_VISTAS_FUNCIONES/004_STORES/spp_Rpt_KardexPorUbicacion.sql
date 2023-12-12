If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_KardexPorUbicacion' and xType = 'P')
    Drop Proc spp_Rpt_KardexPorUbicacion
Go--#SQL

Create Proc spp_Rpt_KardexPorUbicacion
( 
	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia Varchar(4) = '0005',
	@IdPasillo int = 0, @IdEstante int = 0, @IdEntrepaño int = 0, @FechaInicial Varchar(10) = '2015-06-01', @FechaFinal Varchar(10) = '2015-07-01'
)
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

	
	Select
		E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.FolioMovtoInv, E.IdTipoMovto_Inv, 
		(Case When E.TipoES = 'E' Then 'ENTRADA' Else 'SALIDA' End) As TipoES, E.FechaRegistro,
		E.IdPersonalRegistra, L.CodigoEAN, L.IdSubFarmacia, L.ClaveLote, L.Cantidad, L.Existencia
	Into #tmpMovtos
	From MovtosInv_Enc E (NoLock)
	Inner Join MovtosInv_Det_CodigosEAN D (NoLock)
		On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.IdEstado And E.IdFarmacia = D.IdFarmacia And E.FolioMovtoInv = D.FolioMovtoInv)
	Inner Join MovtosInv_Det_CodigosEAN_Lotes L (NoLock)
		On (D.IdEmpresa = L.IdEmpresa And D.IdEstado = L.IdEstado And D.IdFarmacia = L.IdFarmacia And D.FolioMovtoInv = L.FolioMovtoInv And
			D.CodigoEAN = L.CodigoEAN)
	Inner Join MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones U (NoLock)
		On (L.IdEmpresa = U.IdEmpresa And L.IdEstado = U.IdEstado And L.IdFarmacia = U.IdFarmacia And L.FolioMovtoInv = U.FolioMovtoInv And
			L.CodigoEAN = U.CodigoEAN And L.IdSubFarmacia = U.IdSubFarmacia And L.ClaveLote = U.ClaveLote)
	Where MovtoAplicado = 'S' And E.Status = 'A' And
		  E.IdEmpresa = @IdEmpresa And E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And
		  Convert(Varchar(10), E.FechaRegistro, 120) between @FechaInicial And @FechaFinal And
		  U.IdPasillo = @IdPasillo And U.IdEstante = @IdEstante And U.IdEntrepaño = @IdEntrepaño
		
			
			
	Select
		T.IdEmpresa, E.Nombre As Empresa, T.IdEstado, F.Estado, T.IdFarmacia, F.Farmacia,
		T.FolioMovtoInv, T.IdTipoMovto_Inv, M.Descripcion As  TipoMovto,T.TipoES, T.FechaRegistro,
		T.IdPersonalRegistra, R.NombreCompleto As Personal,
		T.CodigoEAN, P.Descripcion, P.ClaveSSA, P.DescripcionSal, T.IdSubFarmacia, T.ClaveLote, T.Cantidad, T.Existencia
	From #tmpMovtos T
	Inner Join CatEmpresas E (NoLock) On (T.IdEmpresa = E.IdEmpresa)
	Inner Join vw_Farmacias F (NoLock) On (T.IdEstado = F.IdEstado And T.IdFarmacia = F.IdFarmacia)
	Inner Join vw_Personal R (NoLock) On (T.IdEstado = R.IdEstado And T.IdFarmacia = R.IdFarmacia And T.IdPersonalRegistra = R.IdPersonal)
	Inner Join Movtos_Inv_Tipos M (NoLock) On (T.IdTipoMovto_Inv = M.IdTipoMovto_Inv)
	inner Join vw_Productos_CodigoEAN P (NoLock) On (T.CodigoEAN = P.CodigoEAN)
	Order By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.FechaRegistro
	
End
Go--#SQL
	