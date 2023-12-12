If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_DEV_ValidarDevolucion' and xType = 'P' ) 
	Drop Proc spp_DEV_ValidarDevolucion 
Go--#SQL

Create Proc spp_DEV_ValidarDevolucion 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '3', @FolioDevolucion varchar(20) = '269' 
) 
With Encryption 
As 
Set NoCount On 
Declare 
	@sFolioDevolucion varchar(20),  
	@iCantidadesCorrectas bit, 
	@iExistenEANs bit, 	
	@iExistenLotes bit, 
	@iExistenUbicaciones bit, 	
	@ManejaUbicaciones bit, 
	@iError bit  
	
	Set @IdEmpresa = right('00000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('00000000000000000' + @IdEstado, 2)  
	Set @IdFarmacia = right('00000000000000000' + @IdFarmacia, 4)  		
	Set @FolioDevolucion = right('00000000000000000' + @FolioDevolucion, 8) 
	Set @iCantidadesCorrectas = 0 
	Set @iExistenEANs = 0 
	Set @iExistenLotes = 0
	Set @iExistenUbicaciones = 0    
	Set @ManejaUbicaciones = 0 
	Set @iError = 0


--- Verificar si es almacen y maneja ubicaciones    
	Select @ManejaUbicaciones = 1 
	From CatFarmacias F (NoLock) 
	Inner Join Net_CFGC_Parametros P (NoLock) On ( F.IdEstado = P.IdEstado and F.IdFarmacia = P.IdFarmacia ) 
	Where F.IdEstado = @IdEstado and F.IdFarmacia = @IdFarmacia and P.NombreParametro = 'ManejaUbicaciones' 
	Set @ManejaUbicaciones = IsNull(@ManejaUbicaciones, 0) 
--- Verificar si es almacen y maneja ubicaciones    


--------------------------	Obtener datos base 		
	Select D.IdProducto, D.CodigoEAN, P.Descripcion, cast(Cant_Devuelta as int) as Cantidad, 0 as Lotes, 0 as Ubicaciones, 0 as EsCorrecto   
	into #tmpEAN 
	From DevolucionesDet D (NoLock) 
	Inner Join vw_Productos_CodigoEAN P On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioDevolucion = @FolioDevolucion 
		
	Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, sum(cast(Cant_Devuelta as int)) as Cantidad, 0 as Ubicaciones, 0 as EsCorrecto 
	into #tmpLotes 
	From DevolucionesDet_Lotes (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioDevolucion = @FolioDevolucion 
	group by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote 	


	Select IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, sum(cast(Cant_Devuelta as int)) as Cantidad, 0 as Registro
	into #tmpUbicaciones  
	From DevolucionesDet_Lotes_Ubicaciones (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioDevolucion = @FolioDevolucion  
	Group by IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote  
--------------------------	Obtener datos base 		


--	select top 1 * from DevolucionesDet_Lotes  	 
	

---------		spp_DEV_ValidarDevolucion  
----------------------- Revision de cantidades    	
	Update E Set Lotes = IsNull(( select sum(Cantidad) From #tmpLotes L Where L.IdProducto = E.IdProducto and L.CodigoEAN = E.CodigoEAN ), 0) 	
	From #tmpEAN E 
	
	Update E Set Ubicaciones = IsNull(( select sum(Cantidad) From #tmpUbicaciones L Where L.IdProducto = E.IdProducto and L.CodigoEAN = E.CodigoEAN ), 0) 	
	From #tmpEAN E 	
	
	Update E Set Ubicaciones = IsNull(( select sum(Cantidad) From #tmpUbicaciones L 
										Where L.IdSubFarmacia = E.IdSubFarmacia and L.IdProducto = E.IdProducto and L.CodigoEAN = E.CodigoEAN 
											and L.ClaveLote = E.ClaveLote 
										), 0) 	
	From #tmpLotes E 	
	
	Update E Set EsCorrecto = 1 From #tmpEAN E Where Cantidad = Lotes  
	Update E Set EsCorrecto = 1 From #tmpLotes E Where Cantidad = Ubicaciones   	
	Update E Set EsCorrecto = 0 From #tmpEAN E Where Cantidad = 0 or Lotes = 0 or Ubicaciones = 0 
----------------------- Revision de cantidades    	


------------------------------------------------ Revision final			
	------Select @iCantidadesCorrectas = (case when count(*) > 0 then 1 else 0 end) From #tmpEAN E where EsCorrecto = 0  
	------Select @iExistenLotes = (case when count(*) > 0 then 1 else 0 end) From #tmpLotes E where Registro = 0  
	------Select @iExistenUbicaciones = (case when count(*) > 0 then 1 else 0 end) From #tmpUbicaciones E	where Registro = 0   
	

	If Exists ( Select top 1 * From #tmpEAN E ) 
	Begin 
		Set @iCantidadesCorrectas = 1 
		If Exists ( Select top 1 * From #tmpEAN E where EsCorrecto = 0 ) 
		   Set @iCantidadesCorrectas = 0 
	End 

	If Exists ( Select top 1 * From #tmpEAN E  ) 
	   Set @iExistenEANs =  1  

	If Exists ( Select top 1 * From #tmpLotes E ) 
	Begin 
		If Not Exists ( Select top 1 * From #tmpLotes E where EsCorrecto = 0 ) 
		   Set @iExistenLotes = 1 
	End    
	   
	   
	If @ManejaUbicaciones = 1 
		Begin    
			If Exists ( Select top 1 * From #tmpUbicaciones E ) 		
				Set @iExistenUbicaciones = 1     
		End    

----------------- Revision  
	If @ManejaUbicaciones = 0 
		Begin 
		If @iCantidadesCorrectas = 0 OR @iExistenEANs = 0 OR  @iExistenLotes = 0 OR @iExistenUbicaciones = 0 
			Set @iError = 1  		
		End 
	Else 
		Begin 	
		If @iCantidadesCorrectas = 0 OR @iExistenEANs = 0 OR  @iExistenLotes = 0 
			Set @iError = 1 
		End 	
------------------------------------------------ Revision final			



/* 

	Exec spp_DEV_ValidarDevolucion  1, 11, 3, 1 

	Exec spp_DEV_ValidarDevolucion  1, 11, 3, 270  
	
	Exec spp_DEV_ValidarDevolucion  1, 11, 3, 2700 
	
*/ 	 

------	Habilitar posteriormente 
--	Set @iError = 0   

	Select 
		@iCantidadesCorrectas as CantidadesCorrectas, 
		@iExistenEANs as ExistenEANs, 
		@iExistenLotes as ExistenTodosLosLotes, 
		@iExistenUbicaciones as ExistenTodasLasUbicaciones, 
		@iError as ExisteError  
	
	Select * from #tmpEAN Where EsCorrecto = 0  
	Select * from #tmpLotes 		
	Select * from #tmpUbicaciones 


Go--#SQL 
	