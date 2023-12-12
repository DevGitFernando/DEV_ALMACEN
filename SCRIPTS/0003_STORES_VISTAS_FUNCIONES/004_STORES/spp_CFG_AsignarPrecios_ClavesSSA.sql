If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFG_AsignarPrecios_ClavesSSA' and xType = 'P' ) 
   Drop Proc spp_CFG_AsignarPrecios_ClavesSSA
Go--#SQL	
 

Create Proc spp_CFG_AsignarPrecios_ClavesSSA 
(
	@IdEstado varchar(2), @IdCliente varchar(4), @IdSubCliente varchar(4), @IdClaveSSA_Sal varchar(4), @Precio numeric(14, 4), 
	@IdEstadoPersonal varchar(2), @IdFarmaciaPersonal varchar(4), @IdPersonal varchar(4), @iOpcion tinyint, 
	@Factor numeric(14, 4) = 1, @ContenidoPaquete_Licitado Int = 0, @IdPresentacion_Licitado varchar(3) = '000', @Dispensacion_CajasCompletas bit = 0,
	@SAT_ClaveDeProducto_Servicio varchar(20), @SAT_UnidadDeMedida varchar(5)
) 
With Encryption  
As 
Begin 
Set NoCount On 
Declare @sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/
	Set @sStatus = 'A'
	Set @iActualizado = 0 
	
	
	if @iOpcion = 1 
	   Begin 
	       If Not Exists ( Select * From CFG_ClavesSSA_Precios (NoLock) 
							Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
							and IdClaveSSA_Sal = @IdClaveSSA_Sal 
						 )  
	          Begin 
				 Insert Into CFG_ClavesSSA_Precios ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, Precio,
													 ContenidoPaquete_Licitado, IdPresentacion_Licitado, Dispensacion_CajasCompletas, Factor,
													 SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida, Status, Actualizado ) 
				 Select @IdEstado, @IdCliente, @IdSubCliente, @IdClaveSSA_Sal, @Precio, @ContenidoPaquete_Licitado,
						@IdPresentacion_Licitado, @Dispensacion_CajasCompletas, @Factor,
						@SAT_ClaveDeProducto_Servicio, @SAT_UnidadDeMedida, @sStatus, @iActualizado 
	          End 
	       Else 
	          Begin 
				  Update CFG_ClavesSSA_Precios
				  Set Precio = @Precio, Factor = @Factor, ContenidoPaquete_Licitado = @ContenidoPaquete_Licitado,
				  IdPresentacion_Licitado = @IdPresentacion_Licitado, Dispensacion_CajasCompletas = @Dispensacion_CajasCompletas,
				  SAT_ClaveDeProducto_Servicio = @SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida = @SAT_UnidadDeMedida, 
				  Status = @sStatus, Actualizado = @iActualizado 
				  Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal 
	          End    
	   End 
	Else 
	   Begin 
	      Set @sStatus = 'C' 
	      Update CFG_ClavesSSA_Precios
		  Set Status = @sStatus, Actualizado = @iActualizado, IdPresentacion_Licitado = @IdPresentacion_Licitado, Dispensacion_CajasCompletas = @Dispensacion_CajasCompletas,
		  SAT_ClaveDeProducto_Servicio = @SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida = @SAT_UnidadDeMedida
	      Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal
	   End 
	      
	
	---- Insertar la información en el historico 
	Insert Into CFG_ClavesSSA_Precios_Historico 
		( IdEstado, IdCliente, IdSubCliente, IdClaveSSA_Sal, FechaUpdate, Precio,
		  ContenidoPaquete_Licitado, IdPresentacion_Licitado, Dispensacion_CajasCompletas, Factor,
		  SAT_ClaveDeProducto_Servicio, SAT_UnidadDeMedida, 
		  IdEstadoPersonal, IdFarmaciaPersonal, IdPersonal, Status, Actualizado ) 
	Select @IdEstado, @IdCliente, @IdSubCliente, @IdClaveSSA_Sal, getdate() as FechaRegistro, Precio,
		   @ContenidoPaquete_Licitado, @IdPresentacion_Licitado, @Dispensacion_CajasCompletas, @Factor,
		   @SAT_ClaveDeProducto_Servicio, @SAT_UnidadDeMedida,
		   @IdEstadoPersonal, @IdFarmaciaPersonal, @IdPersonal, Status, Actualizado 
    From CFG_ClavesSSA_Precios (NoLock) 
    Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and IdClaveSSA_Sal = @IdClaveSSA_Sal 
	
End 
Go--#SQL 	
 

