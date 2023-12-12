--------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProductos_Historico' and xType = 'P')
    Drop Proc spp_Mtto_CatProductos_Historico
Go--#SQL
  
Create Proc spp_Mtto_CatProductos_Historico ( @IdProducto varchar(10), @IdPersonal varchar(4) ) 
With Encryption 
As 
Begin 

	Insert Into CatProductos_Historico ( IdProducto, IdClaveSSA_Sal, Descripcion, DescripcionCorta, IdClasificacion, IdTipoProducto, 
		EsMedicamentoControlado, IdFamilia, IdSubFamilia, IdSegmento, IdLaboratorio, IdPresentacion, Descomponer, 
		ContenidoPaquete, ManejaCodigoEAN, UtilidadProducto, PrecioMaxPublico, DescuentoGral, EsSectorSalud, FechaRegistro, 
		IdPersonal, Status, Actualizado )
	Select IdProducto, IdClaveSSA_Sal, Descripcion, DescripcionCorta, IdClasificacion, IdTipoProducto, 
		EsMedicamentoControlado, IdFamilia, IdSubFamilia, IdSegmento, IdLaboratorio, IdPresentacion, Descomponer, 
		ContenidoPaquete, ManejaCodigoEAN, UtilidadProducto, PrecioMaxPublico, DescuentoGral, EsSectorSalud, getdate(), 
		@IdPersonal, Status, Actualizado   
	From CatProductos (NoLock) 
	Where IdProducto = @IdProducto 

End 
Go--#SQL	


--------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatProductos' and xType = 'P')
    Drop Proc spp_Mtto_CatProductos
Go--#SQL	

  
Create Proc spp_Mtto_CatProductos ( 
	@IdProducto varchar(10), @IdClaveSSA_Sal varchar(6), @Descripcion varchar(202), @DescripcionCorta varchar(102), 
	@IdClasificacion varchar(6), @IdTipoProducto varchar(4), @EsMedicamentoControlado int, @EsSectorSalud int, 
	@IdFamilia varchar(4), @IdSubFamilia varchar(4), @IdSegmento varchar(2), 
	@IdLaboratorio varchar(4), @IdPresentacion varchar(5), @Descomponer int, @ContenidoPaquete int, @ManejaCodigoEAN int, 
	@UtilidadProducto numeric(14,4), @PrecioMaxPublico numeric(14,4), @DescuentoGral numeric(14,4), 
	@IdPersonal varchar(4), @iOpcion smallint )
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


	If @IdProducto = '*' 
	   Select @IdProducto = cast( (max(IdProducto) + 1) as varchar)  From CatProductos (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdProducto = IsNull(@IdProducto, '1')
	Set @IdProducto = right(replicate('0', 8) + @IdProducto, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatProductos (NoLock) Where IdProducto = @IdProducto ) 
			  Begin 
				 Insert Into CatProductos 
					( IdProducto, IdClaveSSA_Sal, Descripcion, DescripcionCorta, IdClasificacion, IdTipoProducto, 
					  EsMedicamentoControlado, EsSectorSalud, IdFamilia, IdSubFamilia, IdSegmento, IdLaboratorio, IdPresentacion, Descomponer, 
					  ContenidoPaquete, ManejaCodigoEAN, UtilidadProducto, PrecioMaxPublico, DescuentoGral, Status, Actualizado ) 
				 Select @IdProducto, @IdClaveSSA_Sal, @Descripcion, @DescripcionCorta, @IdClasificacion, 
				        @IdTipoProducto, @EsMedicamentoControlado, @EsSectorSalud, @IdFamilia, @IdSubFamilia, @IdSegmento, @IdLaboratorio, 
				        @IdPresentacion, @Descomponer, @ContenidoPaquete, @ManejaCodigoEAN, 
				        @UtilidadProducto, @PrecioMaxPublico, @DescuentoGral, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatProductos Set 
					IdClaveSSA_Sal = @IdClaveSSA_Sal, Descripcion = @Descripcion, DescripcionCorta = @DescripcionCorta, 
					IdClasificacion = @IdClasificacion, IdTipoProducto = @IdTipoProducto, 
					EsMedicamentoControlado = @EsMedicamentoControlado, 
					EsSectorSalud = @EsSectorSalud, 
					IdFamilia = @IdFamilia, IdSubFamilia = @IdSubFamilia, IdSegmento = @IdSegmento,  
					IdLaboratorio = @IdLaboratorio, IdPresentacion = @IdPresentacion, Descomponer = @Descomponer, 
					ContenidoPaquete = @ContenidoPaquete, ManejaCodigoEAN = @ManejaCodigoEAN, Status = @sStatus, 
					Actualizado = @iActualizado
				 Where IdProducto = @IdProducto  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdProducto 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatProductos Set Status = @sStatus, Actualizado = @iActualizado Where IdProducto = @IdProducto 
		   Set @sMensaje = 'La información del Producto ' + @IdProducto + ' ha sido cancelada satisfactoriamente.' 
	   End 


    --- Generar Log 
    Exec spp_Mtto_CatProductos_Historico @IdProducto, @IdPersonal 
    
	-- Regresar la Clave Generada
    Select @IdProducto as Clave, @sMensaje as Mensaje 
End
Go--#SQL

