If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_PRCS_DeterminarExistencia' and xType = 'P' ) 
   Drop Proc spp_PRCS_DeterminarExistencia 
Go--#SQL 

Create Proc spp_PRCS_DeterminarExistencia 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003' 
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
--Set NoCount On 


------------------ Obtener la información base	
	Select 
		convert(varchar(10), getdate(), 120) as FechaOperacion, 
		IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, 
		EsConsignacion, ( Existencia - ExistenciaEnTransito ) as ExistenciaDisponible, 
		'A' as Status, 0 as Actualizado, getdate() as FechaControl  
	Into #tmpExistencia 	
	From FarmaciaProductos_CodigoEAN_Lotes (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		  and ( Existencia - ExistenciaEnTransito ) > 0 
------------------ Obtener la información base	


------------------ Agregar los productos nuevos 
	Insert Into FarmaciaProductos_CodigoEAN_Lotes__Historico ( FechaOperacion, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, ExistenciaDisponible, Status, Actualizado, FechaControl ) 	
	Select FechaOperacion, IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, EsConsignacion, ExistenciaDisponible, Status, Actualizado, FechaControl 
	From #tmpExistencia E (NoLock) 
	Where Not Exists 
	(
		Select * 
		From FarmaciaProductos_CodigoEAN_Lotes__Historico H (NoLock) 
		Where E.FechaOperacion = H.FechaOperacion and E.IdEmpresa = H.IdEmpresa and E.IdEstado = H.IdEstado and E.IdFarmacia = H.IdFarmacia 
			  and E.IdProducto = H.IdProducto and E.CodigoEAN = H.CodigoEAN and E.ClaveLote = H.ClaveLote  
	) 
------------------ Agregar los productos nuevos 


------------------ Asignar la mejor existencia 
	Update H Set ExistenciaDisponible = E.ExistenciaDisponible, FechaControl = E.FechaControl 
	From FarmaciaProductos_CodigoEAN_Lotes__Historico H (NoLock) 
	Inner Join #tmpExistencia E (NoLock) 
		On ( E.FechaOperacion = H.FechaOperacion and E.IdEmpresa = H.IdEmpresa and E.IdEstado = H.IdEstado and E.IdFarmacia = H.IdFarmacia 
			  and E.IdProducto = H.IdProducto and E.CodigoEAN = H.CodigoEAN and E.ClaveLote = H.ClaveLote  ) 
	Where E.ExistenciaDisponible > H.ExistenciaDisponible 
------------------ Asignar la mejor existencia 
	
	

End 

Go--#SQL 

