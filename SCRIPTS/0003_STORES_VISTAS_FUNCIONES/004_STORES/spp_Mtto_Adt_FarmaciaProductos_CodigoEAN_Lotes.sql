If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Adt_FarmaciaProductos_CodigoEAN_Lotes' and xType = 'P')
    Drop Proc spp_Mtto_Adt_FarmaciaProductos_CodigoEAN_Lotes
Go--#SQL
  
Create Proc spp_Mtto_Adt_FarmaciaProductos_CodigoEAN_Lotes 
(	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdSubFarmacia varchar(2), @IdProducto varchar(8), 
	@CodigoEAN varchar(30), @ClaveLote varchar(30), @FechaCaducidad datetime, @FechaCaducidad_Anterior datetime, 
	@IdPersonal varchar(6)
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado tinyint,
		@FolioMovto varchar(30)

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0

	-- Se obtiene el Movimiento
	Select @FolioMovto = cast( (max(FolioMovto) + 1) as varchar)  
	From Adt_FarmaciaProductos_CodigoEAN_Lotes (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia 
	And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioMovto = IsNull(@FolioMovto, '1')
	Set @FolioMovto = right(replicate('0', 8) + @FolioMovto, 8)

	If Exists ( Select * From FarmaciaProductos_CodigoEAN_Lotes (NoLock) 
				Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia
				And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 
			  )  
	  Begin 

		Insert Into Adt_FarmaciaProductos_CodigoEAN_Lotes 
			( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, FolioMovto, FechaCaducidad, FechaCaducidad_Anterior, FechaRegistro, IdPersonal, Status, Actualizado ) 
		Select	IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, @FolioMovto as FolioMovto, 
			@FechaCaducidad as FechaCaducidad, FechaCaducidad as FechaCaducidad_Anterior, getdate() as FechaRegistro, @IdPersonal as IdPersonal, Status, 0 as Actualizado 
			-- @IdEmpresa, @IdEstado, @IdFarmacia, @IdProducto, @CodigoEAN, @ClaveLote, @FolioMovto, @FechaCaducidad, 
			-- @FechaCaducidad_Anterior, GetDate(), @IdPersonal, @sStatus, @iActualizado 
		From FarmaciaProductos_CodigoEAN_Lotes (NoLock) 	
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia
			And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote
					
		-- Se actualiza la informacion en FarmaciaProductos_CodigoEAN_Lotes
		Update FarmaciaProductos_CodigoEAN_Lotes Set FechaCaducidad = @FechaCaducidad, Actualizado = @iActualizado
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdSubFarmacia = @IdSubFarmacia
		And IdProducto = @IdProducto And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote 


----		-- Se inserta la informacion en Adt_FarmaciaProductos_CodigoEAN_Lotes
----		If Not Exists ( Select * From Adt_FarmaciaProductos_CodigoEAN_Lotes (NoLock) 
----						Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdProducto = @IdProducto 
----						And CodigoEAN = @CodigoEAN And ClaveLote = @ClaveLote And FolioMovto = @FolioMovto )  
----		  Begin
----			Insert Into Adt_FarmaciaProductos_CodigoEAN_Lotes 
----			Select	@IdEmpresa, @IdEstado, @IdFarmacia, @IdProducto, @CodigoEAN, @ClaveLote, @FolioMovto, @FechaCaducidad, 
----					@FechaCaducidad_Anterior, GetDate(), @IdPersonal, @sStatus, @iActualizado 
----		  End
		
      End 
	Set @sMensaje = 'La información del Lote ' + @ClaveLote + ' del Producto ' + @IdProducto + ' se actualizo satisfactoriamente '     

	-- Regresar la Clave Generada
    Select @IdProducto as Clave, @sMensaje as Mensaje 
End
Go--#SQL
