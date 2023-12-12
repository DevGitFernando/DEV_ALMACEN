
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatPersonalCEDIS' and xType = 'P')
    Drop Proc spp_Mtto_CatPersonalCEDIS
Go--#SQL
  
Create Proc spp_Mtto_CatPersonalCEDIS ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdPersonal varchar(4), 
										@Nombre varchar(102), @IdPuesto varchar(2), @iOpcion smallint )
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


	If @IdPersonal = '*' 
	   Select @IdPersonal = cast( (max(IdPersonal) + 1) as varchar)  From CatPersonalCEDIS (NoLock) 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdPersonal = IsNull(@IdPersonal, '1')
	Set @IdPersonal = right(replicate('0', 4) + @IdPersonal, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatPersonalCEDIS (NoLock) 
							Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal ) 
			  Begin 
				 Insert Into CatPersonalCEDIS ( IdEmpresa, IdEstado, IdFarmacia, IdPersonal, Nombre, IdPuesto, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @Nombre, @IdPuesto, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatPersonalCEDIS Set Nombre = @Nombre, IdPuesto = @IdPuesto, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdPersonal 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatPersonalCEDIS Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal 
		   Set @sMensaje = 'La información del Personal ' + @IdPersonal + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdPersonal as Clave, @sMensaje as Mensaje 
End
Go--#SQL
