If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_CostoProductos_Ubicaciones' And xType = 'P' )
	Drop Proc spp_Rpt_CostoProductos_Ubicaciones
Go--#SQL 

-- Exec spp_Rpt_CostoProductos_Ubicaciones '001', '11', '0003', 1, 100

Create Procedure spp_Rpt_CostoProductos_Ubicaciones 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@CostoMin numeric(14, 4) = 0, @CostoMax numeric(14, 4) = 0
) 
With Encryption 	
As
Begin 
Declare @sSql varchar(8000),   
		@sWhere varchar(8000)	 


    Set @sSql = '' 
    Set @sWhere = ''     


	Select 'Producto' = U.IdProducto, 'Codigo EAN' = U.CodigoEAN, 'Clave SSA' = U.ClaveSSA, 
	'Descripción' = U.DescripcionProducto, 'Lote' = U.ClaveLote, 'Fecha Caducidad' = Convert(varchar(10),U.FechaCaducidad, 120),
	U.MesesParaCaducar, 'Costo' = F.UltimoCosto,
	'Rack' = U.IdPasillo, 'Nivel' = U.IdEstante, 'Entrepaño' = U.IdEntrepaño, 
	cast(U.Existencia as int) as Existencia
	From vw_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (Nolock)
	Inner Join FarmaciaProductos F (Nolock)	
		On ( U.IdEmpresa = F.IdEmpresa AND U.IdEstado = F.IdEstado AND U.IdFarmacia = F.IdFarmacia AND U.IdProducto = F.IdProducto )
	Where U.IdEmpresa = @IdEmpresa AND U.IdEstado = @IdEstado AND U.IdFarmacia = @IdFarmacia 
	AND F.UltimoCosto Between @CostoMin AND @CostoMax
	Order By U.DescripcionClave
	
    
	   	   
End
Go--#SQL 
