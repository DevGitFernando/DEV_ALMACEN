--------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProductos_PreAlta' and xType = 'P')
    Drop Proc spp_Mtto_CatProductos_PreAlta
Go--#SQL	

  
Create Proc spp_Mtto_CatProductos_PreAlta 
( 
	@IdProducto_Alta varchar(10), @IdClaveSSA_Sal varchar(6), @Descripcion varchar(202), @DescripcionCorta varchar(102), 
	@IdClasificacion varchar(6), @IdTipoProducto varchar(4), 
	@EsControlado int, @EsAntibiotico int, @EsSectorSalud int, 
	@IdLaboratorio varchar(4), @IdPresentacion varchar(5), @Descomponer int, @ContenidoPaquete int, @ManejaCodigoEAN int, 
	@PrecioMaxPublico numeric(14,4), @iOpcion smallint 
) 
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 


	If @IdProducto_Alta = '*' 
	   Select @IdProducto_Alta = cast( (max(IdProducto_Alta) + 1) as varchar)  From CatProductos_PreAlta (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdProducto_Alta = IsNull(@IdProducto_Alta, '1')
	Set @IdProducto_Alta = right(replicate('0', 8) + @IdProducto_Alta, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatProductos_PreAlta (NoLock) Where IdProducto_Alta = @IdProducto_Alta ) 
			  Begin 
				 Insert Into CatProductos_PreAlta 
					( IdProducto_Alta, IdClaveSSA_Sal, Descripcion, DescripcionCorta, IdClasificacion, IdTipoProducto, 
					  EsControlado, EsAntibiotico, EsSectorSalud, IdLaboratorio, IdPresentacion, Descomponer, 
					  ContenidoPaquete, ManejaCodigoEAN, PrecioMaxPublico, Status ) 
				 Select @IdProducto_Alta, @IdClaveSSA_Sal, @Descripcion, @DescripcionCorta, @IdClasificacion, 
				        @IdTipoProducto, @EsControlado, @EsAntibiotico, @EsSectorSalud, @IdLaboratorio, 
				        @IdPresentacion, @Descomponer, @ContenidoPaquete, @ManejaCodigoEAN, 
				        @PrecioMaxPublico, @sStatus 
              End 
		   Else 
			  Begin 
			     Update CatProductos_PreAlta Set 
					IdClaveSSA_Sal = @IdClaveSSA_Sal, Descripcion = @Descripcion, DescripcionCorta = @DescripcionCorta, 
					IdClasificacion = @IdClasificacion, IdTipoProducto = @IdTipoProducto, 
					EsControlado = @EsControlado, EsAntibiotico = @EsAntibiotico, 
					EsSectorSalud = @EsSectorSalud, 
					IdLaboratorio = @IdLaboratorio, IdPresentacion = @IdPresentacion, Descomponer = @Descomponer, 
					ContenidoPaquete = @ContenidoPaquete, ManejaCodigoEAN = @ManejaCodigoEAN, Status = @sStatus  
				 Where IdProducto_Alta = @IdProducto_Alta  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdProducto_Alta 
	   End 

	-- Regresar la Clave Generada
    Select @IdProducto_Alta as Clave, @sMensaje as Mensaje 
End
Go--#SQL

