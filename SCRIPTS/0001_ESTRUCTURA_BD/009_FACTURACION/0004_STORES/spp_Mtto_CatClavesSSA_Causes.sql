If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatClavesSSA_Causes' and xType = 'P' ) 
   Drop Proc spp_Mtto_CatClavesSSA_Causes
Go--#SQL	


Create Proc spp_Mtto_CatClavesSSA_Causes 
(
	@ClaveSSA varchar(30), @Descripcion varchar(7500), @IdPresentacion varchar(3), @ContenidoPaquete int,
	@EsControlado tinyint, @EsAntibiotico tinyint, @Año int, @PrecioBase numeric(14, 4), @Porcentaje numeric(14, 4), 
	@PrecioAdmon numeric(14, 4), @PrecioNeto numeric(14, 4), @iOpcion tinyint    
) 
With Encryption  
As 
Begin 
Set NoCount On 
Declare @sStatus varchar(1)  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/
	Set @sStatus = 'A'
		

	
	if @iOpcion = 1 
	   Begin 
	       If Not Exists ( Select * From CatClavesSSA_Causes (NoLock) Where ClaveSSA = @ClaveSSA and Año = @Año )  
	          Begin 
				 Insert Into CatClavesSSA_Causes ( ClaveSSA, Descripcion, IdPresentacion, ContenidoPaquete, EsSeguroPopular, EsControlado,  
						EsAntibiotico, Año, PrecioBase, Porcentaje, PrecioAdmon, PrecioNeto, EsDollar, Status ) 
				 Select @ClaveSSA, @Descripcion, @IdPresentacion, @ContenidoPaquete, 1, @EsControlado, @EsAntibiotico, @Año, @PrecioBase, 
						@Porcentaje, @PrecioAdmon, @PrecioNeto, 0, @sStatus 
	          End 
	       Else 
	          Begin 
				  Update CatClavesSSA_Causes Set PrecioBase = @PrecioBase, Porcentaje = @Porcentaje, PrecioAdmon = @PrecioAdmon, 
						PrecioNeto = @PrecioNeto, Status = @sStatus, IdPresentacion = @IdPresentacion 
				  Where ClaveSSA = @ClaveSSA and Año = @Año 
	          End    
	   End 
	Else 
	   Begin 
	      Set @sStatus = 'C' 
	      Update CatClavesSSA_Causes Set Status = @sStatus
	      Where ClaveSSA = @ClaveSSA and Año = @Año
	   End	
	
End 
Go--#SQL 	
 

