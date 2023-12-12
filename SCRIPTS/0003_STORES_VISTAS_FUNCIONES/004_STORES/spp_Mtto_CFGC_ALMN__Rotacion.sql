
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFGC_ALMN__Rotacion' and xType = 'P')
    Drop Proc spp_Mtto_CFGC_ALMN__Rotacion
Go--#SQL
  
Create Proc spp_Mtto_CFGC_ALMN__Rotacion ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdRotacion varchar(3), 
										@Nombre varchar(500), @Orden int, @iOpcion smallint )
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


	If @IdRotacion = '*' 
	   Select @IdRotacion = cast( (max(IdRotacion) + 1) as varchar)  From CFGC_ALMN__Rotacion (NoLock) 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdRotacion = IsNull(@IdRotacion, '1')
	Set @IdRotacion = right(replicate('0', 3) + @IdRotacion, 3)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CFGC_ALMN__Rotacion (NoLock) 
							Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRotacion = @IdRotacion ) 
			  Begin 
				 Insert Into CFGC_ALMN__Rotacion ( IdEmpresa, IdEstado, IdFarmacia, IdRotacion, NombreRotacion, Orden, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdRotacion, @Nombre, @Orden, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFGC_ALMN__Rotacion Set NombreRotacion = @Nombre, Orden = @Orden, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRotacion = @IdRotacion 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdRotacion 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFGC_ALMN__Rotacion Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRotacion = @IdRotacion 
		   Set @sMensaje = 'La información de la rotación ' + @IdRotacion + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdRotacion as Clave, @sMensaje as Mensaje 
End
Go--#SQL
