
---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_ALMN_DisponibleDevolucion_Lotes' and xType = 'FN' )
   Drop Function fg_ALMN_DisponibleDevolucion_Lotes   
Go--#SQL     
      
Create Function dbo.fg_ALMN_DisponibleDevolucion_Lotes 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', @IdSubFarmacia varchar(2) = '01',
	@IdProducto varchar(8) = '00011040', @CodigoEAN varchar(30) = '7501384541514', @ClaveLote varchar(30) = '1750113',  
	@CantidadValidar int = 100  
) 
Returns int 
With Encryption 
As 
Begin 
Declare 
	@CantidadDisponible int 

	Select @CantidadDisponible = (Existencia - (ExistenciaEnTransito + ExistenciaSurtidos))  
	From FarmaciaProductos_CodigoEAN_Lotes 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia 
		  and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 		
		  
	If @CantidadDisponible <> @CantidadValidar 
	Begin 
		If @CantidadValidar > @CantidadDisponible 
			Begin 
				Set @CantidadValidar = @CantidadDisponible 
			End 	
		Else
			Begin 
				If @CantidadDisponible > @CantidadValidar
					Set @CantidadDisponible = @CantidadValidar 
				Else 
					Set @CantidadDisponible = @CantidadDisponible - @CantidadValidar
	        End 
	End 	
		

	If @CantidadDisponible < 0 
	   Set @CantidadDisponible = 0  

	If @CantidadDisponible >= @CantidadValidar 
	   Set @CantidadDisponible = @CantidadValidar  

    return @CantidadDisponible  
          
End 
Go--#SQL 

---------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_ALMN_DisponibleDevolucion_Ubicaciones' and xType = 'FN' )
   Drop Function fg_ALMN_DisponibleDevolucion_Ubicaciones   
Go--#SQL     
      
Create Function dbo.fg_ALMN_DisponibleDevolucion_Ubicaciones 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', @IdSubFarmacia varchar(2) = '01',
	@IdProducto varchar(8) = '00011040', @CodigoEAN varchar(30) = '7501384541514', @ClaveLote varchar(30) = '1750113', 
	@IdPasillo int = 0, @IdEstante int = 0, @IdEntrepaño int = 1, @CantidadValidar int = 100  
)  
Returns int 
With Encryption 
As 
Begin 
Declare 
	@CantidadDisponible int 

	Select @CantidadDisponible = (Existencia - (ExistenciaEnTransito + ExistenciaSurtidos))  
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia 
		  and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 
		  and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño 

	If @CantidadDisponible <> @CantidadValidar 
	Begin 
		If @CantidadValidar > @CantidadDisponible 
			Begin 
				Set @CantidadValidar = @CantidadDisponible 
			End 	
		Else
			Begin 
				If @CantidadDisponible > @CantidadValidar
					Set @CantidadDisponible = @CantidadValidar 
				Else 
					Set @CantidadDisponible = @CantidadDisponible - @CantidadValidar
	        End 
	End 

	If @CantidadDisponible < 0 
	   Set @CantidadDisponible = 0  

	If @CantidadDisponible >= @CantidadValidar 
	   Set @CantidadDisponible = @CantidadValidar  

    return @CantidadDisponible  
          
End 
Go--#SQL 



---------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ALMN_DisponibleDevolucion_Ubicaciones' and xType = 'P' )
   Drop Proc spp_ALMN_DisponibleDevolucion_Ubicaciones   
Go--#SxQL     
      
--		Exec spp_ALMN_DisponibleDevolucion_Ubicaciones	'001', '11', '0003', '01', '00013149', '4042809287547', 244107, 0, 0, 1, 470                   
      
Create Proc dbo.spp_ALMN_DisponibleDevolucion_Ubicaciones 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', @IdSubFarmacia varchar(2) = '02',
	@IdProducto varchar(8) = '00001828', @CodigoEAN varchar(30) = '7501299302385', @ClaveLote varchar(30) = 'H05078', 
	@IdPasillo int = 0, @IdEstante int = 0, @IdEntrepaño int = 1, 
	@CantidadValidar_Lote int = 11, @CantidadValidar_Ubicacion int = 11  
) 
With Encryption 
As 
Begin 
Declare 
	@CantidadDisponible_Lote int,  
	@CantidadDisponible_Ubicacion int 

----------------------- LOTES 

--		spp_ALMN_DisponibleDevolucion_Ubicaciones	

	Select @CantidadDisponible_Lote = (Existencia - (ExistenciaEnTransito + ExistenciaSurtidos)) 
	From FarmaciaProductos_CodigoEAN_Lotes 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia 
		  and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 	   
	
	If @CantidadDisponible_Lote <> @CantidadValidar_Lote 
	Begin 
		If @CantidadValidar_Lote > @CantidadDisponible_Lote 
			Begin 
				Set @CantidadValidar_Lote = @CantidadDisponible_Lote 
			End 	
		Else
			Begin 
				If @CantidadDisponible_Lote > @CantidadValidar_Lote
					Set @CantidadDisponible_Lote = @CantidadValidar_Lote 
				Else 
					Set @CantidadDisponible_Lote = @CantidadDisponible_Lote - @CantidadValidar_Lote
	        End 
	End 

	If @CantidadDisponible_Lote < 0 
	   Set @CantidadDisponible_Lote = 0 
	   
	If @CantidadDisponible_Lote >= @CantidadValidar_Lote 
	   Set @CantidadDisponible_Lote = @CantidadValidar_Lote 	   
	   
--		spp_ALMN_DisponibleDevolucion_Ubicaciones	
	   
----------------------- LOTES 


----------------------- UBICACIONES   
	Select @CantidadDisponible_Ubicacion = (Existencia - (ExistenciaEnTransito + ExistenciaSurtidos)) 
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia 
		  and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 
		  and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño 


	If @CantidadDisponible_Ubicacion <> @CantidadValidar_Ubicacion 
	Begin 
		If @CantidadValidar_Ubicacion > @CantidadDisponible_Ubicacion 
			Begin 
				Set @CantidadValidar_Ubicacion = @CantidadDisponible_Ubicacion 
			End 	
		Else
			Begin 
				If @CantidadDisponible_Ubicacion > @CantidadValidar_Ubicacion
					Set @CantidadDisponible_Ubicacion = @CantidadValidar_Ubicacion 
				Else 
					Set @CantidadDisponible_Ubicacion = @CantidadDisponible_Ubicacion - @CantidadValidar_Ubicacion
	        End 
	End


	If @CantidadDisponible_Ubicacion <> @CantidadValidar_Ubicacion 
	Begin 
		If @CantidadValidar_Ubicacion > @CantidadDisponible_Ubicacion 
			Set @CantidadValidar_Ubicacion = @CantidadDisponible_Ubicacion 
		Else 
	        Set @CantidadDisponible_Ubicacion = @CantidadDisponible_Ubicacion - @CantidadValidar_Ubicacion 
	End 		
	   
	   
	If @CantidadDisponible_Ubicacion < 0 
	   Set @CantidadDisponible_Ubicacion = 0 
	   
	If @CantidadDisponible_Ubicacion >= @CantidadValidar_Ubicacion 
	   Set @CantidadDisponible_Ubicacion = @CantidadValidar_Ubicacion 
		   
----------------------- UBICACIONES   
 


--	Select @CantidadDisponible_Lote, @CantidadDisponible_Ubicacion  
-- @CantidadValidar_Lote int = 100, @CantidadValidar_Ubicacion int = 2  

	Select @CantidadValidar_Lote, @CantidadDisponible_Lote, *  
	From FarmaciaProductos_CodigoEAN_Lotes 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia 
		  and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 

 
	Select @CantidadValidar_Ubicacion, @CantidadDisponible_Ubicacion, *  
	From FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia 
		  and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN and ClaveLote = @ClaveLote 
		  and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepaño 

 

--		Exec spp_ALMN_DisponibleDevolucion_Ubicaciones	'001', '11', '0003', '02', '00011040', '7501384541514', 1750113, 4, 49, 20, 2  

          
--		Exec spp_ALMN_DisponibleDevolucion_Ubicaciones	'001', '11', '0003', '01', '00013149', '4042809287547', 244107, 0, 0, 1, 470             
          
          
End 
Go--#SxQL 

/* 

	select * 
	from FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones 
	where idProducto = 11040 
	
*/ 	

