
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_AjustesInv_Codigos_Sin_Ajuste' and xType = 'P' ) 
   Drop Proc spp_Rpt_AjustesInv_Codigos_Sin_Ajuste 
Go--#SQL

Create Proc spp_Rpt_AjustesInv_Codigos_Sin_Ajuste (  @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0012', 
	@PolizaInicio varchar(8) = '00000001', @PolizaFinal varchar(8) = '00000002'
) 
With Encryption 
As 
Begin 
Set NoCount On 

	-- Se borra la tabla en caso de existir. 
	If Exists ( Select Name From SysObjects(NoLock) Where Name = 'tmpAjustesInventarioCodigos' And xType = 'U' )
	  Begin
		Drop Table tmpAjustesInventarioCodigos
	  End	
	
	-- Se insertan los datos en la tabla
	Select	L.IdEmpresa, E.NombreEmpresa as Empresa, L.IdEstado, E.Estado, L.IdFarmacia, E.Farmacia, 
			@PolizaInicio as PolizaInicio, @PolizaFinal as PolizaFinal,
			P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionSal, F.IdSubFarmacia, F.Descripcion as SubFarmacia, 
			L.IdProducto, P.Descripcion, L.CodigoEAN, L.ClaveLote, 
			Datediff(mm, getdate(), L.FechaCaducidad) as MesesCad, L.FechaRegistro as FechaReg, L.FechaCaducidad as FechaCad,
			(Case When L.Status = 'A' Then 'Activo' Else 'Cancelado' End) as Status,
			cast(L.Existencia as Int) as Existencia, 0 as Cantidad, 
			Convert(varchar(16), GetDate(), 120 ) as FechaImpresion
	Into tmpAjustesInventarioCodigos
	From FarmaciaProductos_CodigoEAN_Lotes L(NoLock)  
	Inner Join vw_Productos_CodigoEAN P(NoLock) On( L.IdProducto = P.IdProducto And L.CodigoEAN = P.CodigoEAN )  
	Inner Join vw_EmpresasFarmacias E(NoLock) On (L.IdEmpresa = E.IdEmpresa And L.IdEstado = E.IdEstado And L.IdFarmacia = E.IdFarmacia ) 
	Inner join CatFarmacias_SubFarmacias F (NoLock) On ( L.IdEstado = F.IdEstado and L.IdFarmacia = F.IdFarmacia and L.IdSubFarmacia = F.IdSubFarmacia )  
	Where L.IdEmpresa = @IdEmpresa And L.IdEstado = @IdEstado And L.IdFarmacia = @IdFarmacia And L.Status = 'A' -- And L.Existencia > 0 
	And L.CodigoEAN Not In  
	(	Select CodigoEAN From vw_AjustesInv_Det(NoLock)  	
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  	
		And Poliza Between @PolizaInicio And @PolizaFinal  	And MovtoAplicado = 'S'  
	)  
	Order By Descripcion
	
	-- Se devuelven los datos del reporte 
	--Select * From tmpAjustesInventarioCodigos(NoLock) Order By Descripcion, IdProducto, ClaveLote
End 
Go--#SQL


