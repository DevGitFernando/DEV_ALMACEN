---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Mtto_CatFarmacias' and xType = 'P' )
    Drop Proc spp_Mtto_CatFarmacias 
Go--#SQL 
	  
Create Proc spp_Mtto_CatFarmacias 
( 
	@IdEstado varchar(3) = '', @IdFarmacia varchar(5) = '', @Descripcion varchar(152) = '', 
	@EsConsignacion int = 0, @ManVtaPubGral int = 0, @ManejaCon int = 0,
	@IdJurisdiccion varchar(3) = '', @IdRegion varchar(3) = '', @IdSubRegion varchar(3) = '', 
	@EsAlm int = 0, @IdAlm varchar(3) = '',
	@EsFront int = 0, @IdMun varchar(5) = '', @IdCol varchar(5) = '', @Dom varchar(102) = '', @CP varchar(52) = '',
	@Tel varchar(32) = '', @eMail varchar(102) = '', @IdTipoUnidad varchar(3) = '', 
	@iOpcion smallint = 0, @EsUnidosis smallint = 0, 
	@CLUES varchar(20) = '', @NombrePropio_UMedica varchar(200) = '', 
	@Transferencia_RecepcionHabilitada int = 1, 
	@IdNivelDeAtencion int = 0  
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


	If @IdFarmacia = '*' 
	   Select @IdFarmacia = cast( (max(IdFarmacia) + 1) as varchar)  From CatFarmacias (NoLock) 
		WHERE IdEstado=@IdEstado --AND IdRegion=@IdRegion AND IdSubRegion=@IdSubRegion
	-- Asegurar que IdFarmacia sea valido y formatear la cadena 
	Set @IdFarmacia = IsNull(@IdFarmacia, '1')
	Set @IdFarmacia = right(replicate('0', 4) + @IdFarmacia, 4)  


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatFarmacias (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia ) 
			  Begin 
				 Insert Into CatFarmacias ( IdEstado, IdFarmacia, NombreFarmacia, EsDeConsignacion, ManejaVtaPubGral, ManejaControlados, IdJurisdiccion, IdRegion, IdSubRegion,
											EsAlmacen, IdAlmacen, EsFrontera, IdMunicipio, IdColonia,Domicilio, CodigoPostal,Telefonos, eMail, Status,
											Actualizado, IdTipoUnidad, EsUnidosis, CLUES, NombrePropio_UMedica, Transferencia_RecepcionHabilitada, IdNivelDeAtencion ) 
				 Select @IdEstado, @IdFarmacia, @Descripcion, @EsConsignacion, @ManVtaPubGral, @ManejaCon, @IdJurisdiccion, @IdRegion, @IdSubRegion,@EsAlm , @IdAlm,
						@EsFront, @IdMun , @IdCol, @Dom, @CP,@Tel, @eMail, @sStatus, @iActualizado, @IdTipoUnidad, @EsUnidosis, @CLUES, @NombrePropio_UMedica, @Transferencia_RecepcionHabilitada, @IdNivelDeAtencion  				   						
              End 
		   Else 
			  Begin 
			     Update F Set NombreFarmacia = @Descripcion, EsDeConsignacion = @EsConsignacion, 
					Status = @sStatus, Actualizado = @iActualizado,
					EsAlmacen = @EsAlm , IdAlmacen = @IdAlm, EsFrontera = @EsFront, IdMunicipio = @IdMun, IdColonia = @IdCol,
					Domicilio = @Dom, CodigoPostal = @CP,Telefonos = @Tel, IdJurisdiccion = @IdJurisdiccion, 
					IdRegion = @IdRegion, IdSubRegion = @IdSubRegion, 
					ManejaVtaPubGral = @ManVtaPubGral, ManejaControlados = @ManejaCon, eMail = @eMail, 
					IdTipoUnidad = @IdTipoUnidad, EsUnidosis = @EsUnidosis, 
					CLUES = @CLUES,  NombrePropio_UMedica = @NombrePropio_UMedica, Transferencia_RecepcionHabilitada = @Transferencia_RecepcionHabilitada, IdNivelDeAtencion = @IdNivelDeAtencion  
				  From CatFarmacias F (NoLock) 
				   
				 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdFarmacia 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatFarmacias Set Status = @sStatus, Actualizado = @iActualizado 
	       Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
		   Set @sMensaje = 'La información de la Farmacia ' + @IdFarmacia + ' ha sido cancelada satisfactoriamente.' 
	   End 


	-- Agregar los movimientos de inventario 
	Insert Into Movtos_Inv_Tipos_Farmacia ( IdEstado, IdFarmacia, IdTipoMovto_Inv, Consecutivo, Status, Actualizado ) 
	Select @IdEstado, @IdFarmacia, M.IdTipoMovto_Inv, 0, 'A', 0   
	From Movtos_Inv_Tipos M (NoLock) 
	Where Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Mf (NoLock) 
					   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and M.IdTipoMovto_Inv = Mf.IdTipoMovto_Inv  ) 
								   

    If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_FAR_Pedidos_Tipos_Farmacias' and xType = 'U' ) 
    Begin     
	    -- Agregar los Tipos de Pedido 
	    Insert Into COM_FAR_Pedidos_Tipos_Farmacias ( IdEstado, IdFarmacia, IdTipoPedido, Consecutivo, Status, Actualizado ) 
	    Select @IdEstado, @IdFarmacia, M.IdTipoPedido, 0, 'A', 0   
	    From COM_FAR_Pedidos_Tipos M (NoLock) 
	    Where Not Exists ( Select * From COM_FAR_Pedidos_Tipos_Farmacias Mf (NoLock) 
					       Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and M.IdTipoPedido = Mf.IdTipoPedido  ) 
    End 

	
	-- Regresar la Clave Generada
    Select @IdFarmacia as Farmacia, @sMensaje as Mensaje     		
						   
    --- Asignar las SubFarmacias 
    Exec spp_Mtto_CatFarmacias_SubFarmacias @IdEstado, 1  

    								   
	
End
Go--#SQL
