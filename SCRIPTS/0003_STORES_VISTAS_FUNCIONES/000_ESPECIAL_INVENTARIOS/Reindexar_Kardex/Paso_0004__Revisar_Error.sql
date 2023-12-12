
Select * 
From 
( 
	SELECT   M.IdSubFarmacia, M.IdProducto, M.CodigoEAN, M.ClaveLote, M.EsConsignacion, M.Cantidad, M.Existencia, cast(F.Existencia as int ) as Exis_F   
	FROM         MovtosInv_Det_CodigosEAN_Lotes M (NoLock) 
	inner Join FarmaciaProductos_CodigoEAN_Lotes F (NoLock) 
		On 
		( 
			M.IdEmpresa = F.IdEmpresa and M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia 
			and M.IdSubFarmacia = F.IdSubFarmacia and M.IdProducto = F.IdProducto and M.CodigoEAN = F.CodigoEAN 
			and M.ClaveLote = F.ClaveLote 
		)
	WHERE FolioMovtoInv = 'SV00000721' 
) as T 
Where Exis_F - Cantidad < 0 

--		IdProducto = '00010571' and CodigoEAN = '7501493861718' and ClaveLote = '*IC01613'  
