If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Adt_ComprasEnc' and xType = 'P')
    Drop Proc spp_Mtto_Adt_ComprasEnc
Go--#SQL
  
Create Proc spp_Mtto_Adt_ComprasEnc 
(	@IdEmpresa varchar(5), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioCompra varchar(32), 
	@IdPersonal varchar(6), @IdProveedor varchar(6), @ReferenciaDocto varchar(22), @FechaDocto datetime
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
	From Adt_ComprasEnc (NoLock) 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @FolioMovto = IsNull(@FolioMovto, '1')
	Set @FolioMovto = right(replicate('0', 8) + @FolioMovto, 8)

	If Exists ( Select * From ComprasEnc (NoLock) 
				   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra )  
	  Begin 
		Insert Into Adt_ComprasEnc 
			( IdEmpresa, IdEstado, IdFarmacia, FolioCompra, FolioMovto, IdPersonal, IdProveedor, ReferenciaDocto, FechaDocto, FechaRegistro, Status, Actualizado ) 
		Select IdEmpresa, IdEstado, IdFarmacia, FolioCompra, @FolioMovto as FolioMovto, @IdPersonal as IdPersonal, IdProveedor, ReferenciaDocto, FechaDocto, getdate() as FechaRegistro, Status, 0 as Actualizado
		From ComprasEnc (NoLock) 
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra 		
----		@IdEmpresa, @IdEstado, @IdFarmacia, @FolioCompra, @FolioMovto, @IdPersonal, @IdProveedor, @ReferenciaDocto, 
----			   @FechaDocto, GetDate(), @sStatus, @iActualizado 
					
		-- Se actualiza la informacion en ComprasEnc
		Update ComprasEnc Set 
			IdProveedor = @IdProveedor, ReferenciaDocto = @ReferenciaDocto, FechaDocto = @FechaDocto, Actualizado = @iActualizado
		Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra 

----		-- Se inserta la informacion en Adt_ComprasEnc
----		If Not Exists ( Select * From Adt_ComprasEnc (NoLock) 
----						Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioCompra = @FolioCompra 
----						And FolioMovto = @FolioMovto )  
----		  Begin
----			Insert Into Adt_ComprasEnc 
----			Select	@IdEmpresa, @IdEstado, @IdFarmacia, @FolioCompra, @FolioMovto, @IdPersonal, @IdProveedor, @ReferenciaDocto, 
----					@FechaDocto, GetDate(), @sStatus, @iActualizado 
----		  End
		
      End 
	Set @sMensaje = 'La información del Folio ' + @FolioCompra + ' se actualizo satisfactoriamente '     

	-- Regresar la Clave Generada
    Select @FolioCompra as Clave, @sMensaje as Mensaje 
End
Go--#SQL 