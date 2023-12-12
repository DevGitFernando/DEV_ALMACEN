If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_INV_ActualizarCostoPromedio' and xType = 'P' )
   Drop Proc spp_INV_ActualizarCostoPromedio 
Go--#SQL	
 

Create Proc spp_INV_ActualizarCostoPromedio ( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0001', @FolioMovto varchar(30) = 'II00000002' ) 
With Encryption 
As 
Begin 
Set NoCount On 	
	
---------------- Obtener los movimientos de inventario 	
	-- Productos del movimiento 
	Select Distinct M.IdEmpresa, M.IdEstado, M.IdFarmacia, M.IdProducto, M.CodigoEAN, E.CostoPromedio   
	Into #tmpProductos  
	From MovtosInv_Det_CodigosEAN M (NoLock) 
	Inner Join FarmaciaProductos E (NoLock) 
		On ( M.IdEmpresa = E.IdEmpresa and M.IdEstado = E.IdEstado and M.IdFarmacia = E.IdFarmacia and M.IdProducto = E.IdProducto ) 			
	Inner Join FarmaciaProductos_CodigoEAN F (NoLock) 
		On ( M.IdEmpresa = F.IdEmpresa and M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia and M.IdProducto = F.IdProducto and M.CodigoEAN = F.CodigoEAN ) 		
	Where M.IdEmpresa = @IdEmpresa and M.IdEstado = @IdEstado and M.IdFarmacia = @IdFarmacia and M.FolioMovtoInv = @FolioMovto 
		
	
	-- Actualizar la información Existente 
	Update C Set CostoPromedio = P.CostoPromedio  
	From CFG_CostosPromediosPrecios C (noLock) 
	Inner Join #tmpProductos P (NoLock) On ( C.IdProducto = P.IdProducto and C.CodigoEAN = P.CodigoEAN ) 
		
	-- Insertar la información no registrada 
	Insert Into CFG_CostosPromediosPrecios ( IdProducto, CostoPromedio, CodigoEAN, Status, Actualizado ) 	
	Select IdProducto, CostoPromedio, CodigoEAN, 'A', 1 
	From #tmpProductos P (NoLock) 
	Where Not Exists ( Select IdProducto From CFG_CostosPromediosPrecios C (NoLock) 
					   Where P.IdProducto = C.IdProducto and P.CodigoEAN = C.CodigoEAN )  
	
--  select * from CFG_CostosPromediosPrecios 		

End    
Go--#SQL 	
 


