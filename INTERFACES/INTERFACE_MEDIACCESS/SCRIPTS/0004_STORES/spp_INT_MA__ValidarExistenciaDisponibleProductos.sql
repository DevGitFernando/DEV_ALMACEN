-------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_INT_MA__ValidarExistenciaDisponibleProductos' and xType = 'P' ) 
   Drop Proc spp_INT_MA__ValidarExistenciaDisponibleProductos
Go--#SQL 

/* 
	
<datos_receta><producto><id>00006834</id><cantidad>1</cantidad></producto></datos_receta>	
	
	Exec spp_INT_MA__ValidarExistenciaDisponibleProductos @Tipo = 1, @Consulta = 'parace 500' 

	Exec spp_INT_MA__ValidarExistenciaDisponibleProductos @IdFarmacia = '61204', @Productos = '9859*10|12322*50'  	
		
	Exec spp_INT_MA__ValidarExistenciaDisponibleProductos @IdFarmacia = '61204', @Productos = '9859*10|'  			
*/ 

Create Proc spp_INT_MA__ValidarExistenciaDisponibleProductos 
( 
	@IdFarmacia varchar(15) = '61207', @Productos varchar(max) = '00006834*1' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFolio varchar(20), 
	@sMensaje varchar(200), 
	@sFiltroProducto varchar(200),   	
	@IdFarmacia_Interno varchar(15), 
	@iEstatus int 

	Set @iEstatus = 0 
	Set @sFiltroProducto = '' 
	Select @IdFarmacia_Interno = (IdEmpresa + IdEstado + IdFarmacia)
	From INT_MA__CFG_FarmaciasClinicas (NoLock) 
	Where Referencia_MA = @IdFarmacia 
	
	----If right(@Productos, 1) = '|'  
	----Begin 
	----	Set @Productos = left(@Productos, len(@Productos) - 1) 
	----End 	
	
	----print @Productos 

	Select 
		cast('' as varchar(3)) as IdEmpresa, 
		cast('' as varchar(2)) as IdEstado, 
		cast('' as varchar(4)) as IdFarmacia, 
		IdProducto, Cantidad as CantidadRequerida, 
		cast(0 as int) as Existencia, 
		cast(0 as int) as ExistenciaReservada,  
		cast(0 as int) as ExistenciaDisponible, 
		cast(0 as int) as CantidadFaltante, 
		0 as Error 	 
	Into #tmp_Resultado 	
	From dbo.fg_SplitProductos(@Productos) 
		
	
	Update R Set IdEmpresa = F.IdEmpresa, IdEstado = F.IdEstado, IdFarmacia = F.IdFarmacia, 
		IdProducto = F.IdProducto, 
		Existencia = F.Existencia 
	From #tmp_Resultado R 
	Inner Join FarmaciaProductos_CodigoEAN F (NoLock) On ( cast(R.IdProducto as bigint) = cast(F.IdProducto as bigint) ) 
	Where (F.IdEmpresa + F.IdEstado + F.IdFarmacia) = @IdFarmacia_Interno 
	
	Update R Set ExistenciaReservada = F.CantidadReservada 
	From #tmp_Resultado R 
	Inner Join INT_MA__RecetasElectronicas_003_ReservaExistencia F (NoLock) 
		On ( R.IdEmpresa = F.IdEmpresa and R.IdEstado = F.IdEstado and R.IdFarmacia = F.IdFarmacia and R.IdProducto = F.IdProducto ) 	
	
	Update R Set ExistenciaDisponible = (Existencia - ExistenciaReservada) -----  - CantidadRequerida 
	From #tmp_Resultado R 	
	
	Update R Set ExistenciaDisponible = 0 
	From #tmp_Resultado R 		
	Where ExistenciaDisponible <= 0 

	Update R Set CantidadFaltante = CantidadRequerida -  ExistenciaDisponible 
	From #tmp_Resultado R 		

	Update R Set CantidadFaltante = 0 
	From #tmp_Resultado R 		
	Where CantidadFaltante <= 0 

	Update R Set Error = 1 
	From #tmp_Resultado R 
	Where ExistenciaDisponible < CantidadRequerida 

	Select @iEstatus = (case when sum(Error) = 0 then 0 else 1 end) From #tmp_Resultado 
	

----		spp_INT_MA__ValidarExistenciaDisponibleProductos 


	Select IdEmpresa, 
		IdEstado, 
		IdFarmacia, 				
		IdProducto, CantidadRequerida, 
		Existencia, 
		ExistenciaReservada,  
		ExistenciaDisponible, 
		CantidadFaltante,  
		@iEstatus as Estatus 
	From #tmp_Resultado 

End 
Go--#SQL 
