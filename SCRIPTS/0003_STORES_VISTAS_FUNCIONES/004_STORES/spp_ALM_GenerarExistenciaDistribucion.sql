If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ALM_GenerarExistenciaDistribucion' and xType = 'P' ) 
   Drop Proc spp_ALM_GenerarExistenciaDistribucion 
Go--#SQL 

--		Exec spp_ALM_GenerarExistenciaDistribucion '100', '99', '9999'

Create Proc spp_ALM_GenerarExistenciaDistribucion 
( 
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0044', @IdPersonal Varchar(4) = '0001', @Equipo Varchar(30) = '', @GUID Varchar(200) = ''
) 
With Encryption 
As 
Begin 
Set NoCount On

	Declare @FechaGeneracion datetime

---------------------- Crear la caja de distribución default 
	If Not Exists ( Select * From CatCajasDistribucion Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCaja = '00000000' )  
	Begin 
		Insert Into CatCajasDistribucion (  IdEstado, IdFarmacia, IdCaja, FechaRegistro, Disponible, Habilitada, Status, Actualizado )  
		Values ( @IdEstado, @IdFarmacia, '00000000', getdate(), 1, 1, 'A', 0 )    
	End 
	


-------------- 
	Select * 
	Into #vw_Productos_CodigoEAN 
	From vw_Productos_CodigoEAN 


	Set @FechaGeneracion = getdate()
	

-------------------------- Obtener informacion base para la distribucion de pedidos 	
	Select @FechaGeneracion as FechaGeneracion, 
		U.IdEmpresa, U.IdEstado, U.IdFarmacia, 
		U.IdSubFarmacia, 
		space(20) as ClaveSSA, convert(varchar(max), '') as DescripcionClave, space(100) as Presentacion, 
		U.IdProducto, U.CodigoEAN, U.ClaveLote, C.FechaCaducidad, datediff(mm, getdate(), C.FechaCaducidad) as MesesCaducar,  
		(case when U.EsConsignacion = 1 then 1 else 2 end) as EsConsignacion, 
		(case when E.EsDePickeo = 1 then 1 else 2 end) as EsDePickeo, 
		U.IdPasillo, U.IdEstante, U.IdEntrepaño, 
		cast((U.Existencia - U.ExistenciaEnTransito) as int) as Existencia, 
		cast((U.Existencia - U.ExistenciaEnTransito) as int) as Existencia_Aux,
		@IdPersonal As IdPersonal, @Equipo As Equipo, @GUID As GUID
	Into #tmpDistribucion 	
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones U (NoLock) 
	Inner Join FarmaciaProductos_CodigoEAN_Lotes C (NoLock) 
		on ( U.IdEmpresa = C.IdEmpresa and U.IdEstado = C.IdEstado and U.IdFarmacia = C.IdFarmacia 
			 and U.IdProducto = C.IdProducto and U.CodigoEAN = C.CodigoEAN and U.ClaveLote = C.ClaveLote )
	Inner Join CatPasillos_Estantes_Entrepaños E (NoLock) 
		On (U.IdEmpresa = E.IdEmpresa and U.IdEstado = E.IdEstado and C.IdFarmacia = E.IdFarmacia
			And U.IdPasillo = E.IdPasillo And U.IdEstante = E.IdEstante And U.IdEntrepaño = E.IdEntrepaño)
	Where U.IdEmpresa = @IdEmpresa and U.IdEstado = @IdEstado and U.IdFarmacia = @IdFarmacia
	Order By U.EsConsignacion Desc, E.EsDePickeo Desc
		  -- and U.CodigoEAN = '7503000897432' 
		  
	
	Update D Set ClaveSSA = P.ClaveSSA, DescripcionClave = P.DescripcionClave, Presentacion = P.Presentacion_ClaveSSA 
	From #tmpDistribucion D 
	Inner Join #vw_Productos_CodigoEAN P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN ) 
-------------------------- Obtener informacion base para la distribucion de pedidos 	
	
	
-------------------------- Generar la tabla base para la distribucion de pedidos 		
	If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'FarmaciaProductos_ALM_Distribucion' and xType = 'U' )
	Begin  
	   -- Drop Table  FarmaciaProductos_ALM_Distribucion
		Select Top 0 * 
		Into FarmaciaProductos_ALM_Distribucion  
		From #tmpDistribucion
		Order By EsConsignacion Desc, EsDePickeo Desc
	End

	If Not Exists ( Select So.Name, Sc.Name
					From sysobjects So (NoLock)
					Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id )
					Where So.Name = 'FarmaciaProductos_ALM_Distribucion' and Sc.Name = 'IdPersonal' )  
	Begin 	
		Alter Table FarmaciaProductos_ALM_Distribucion Add IdPersonal Varchar(4) Not Null Default ''
		Alter Table FarmaciaProductos_ALM_Distribucion Add Equipo Varchar(30) Not Null Default ''
		Alter Table FarmaciaProductos_ALM_Distribucion Add GUID Varchar(200) Not Null Default ''
	End
	
	Delete From FarmaciaProductos_ALM_Distribucion Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	Insert Into FarmaciaProductos_ALM_Distribucion 
	Select * 
	From #tmpDistribucion
	Order By EsConsignacion Desc, EsDePickeo Desc 
-------------------------- Generar la tabla base para la distribucion de pedidos 	
	 
	
	
	Select ClaveSSA, DescripcionClave, Presentacion, cast(sum(Existencia) as int) as Existencia 
	From #tmpDistribucion 
	Group by ClaveSSA, DescripcionClave, Presentacion  
	Order by DescripcionClave 
	
	
End 
Go--#SQL 
	

