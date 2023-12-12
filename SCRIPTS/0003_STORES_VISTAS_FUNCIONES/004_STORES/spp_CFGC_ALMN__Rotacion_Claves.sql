
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_CFGC_ALMN__Rotacion_Claves' and xType = 'P')
    Drop Proc spp_CFGC_ALMN__Rotacion_Claves
Go--#SQL
  
Create Proc spp_CFGC_ALMN__Rotacion_Claves ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdRotacion varchar(3), 
										@IdClaveSSA varchar(4), @iOpcion smallint = 1 )
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
	
	
	
	Update CFGC_ALMN__Rotacion_Claves
	Set Status = 'C', Actualizado = @iActualizado
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRotacion = @IdRotacion And IdClaveSSA = @IdClaveSSA


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CFGC_ALMN__Rotacion_Claves (NoLock) 
							Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRotacion = @IdRotacion And IdClaveSSA = @IdClaveSSA ) 
			  Begin 
				 Insert Into CFGC_ALMN__Rotacion_Claves ( IdEmpresa, IdEstado, IdFarmacia, IdRotacion, IdClaveSSA, Status, Actualizado ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdRotacion, @IdClaveSSA, @sStatus, @iActualizado 
              End
		   Else
		   Update CFGC_ALMN__Rotacion_Claves
		   Set Status = @sStatus, Actualizado = @iActualizado
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRotacion = @IdRotacion 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdRotacion 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFGC_ALMN__Rotacion_Claves Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRotacion = @IdRotacion 
		   Set @sMensaje = 'La información de la rotación ' + @IdRotacion + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdRotacion as Clave, @sMensaje as Mensaje 
End
Go--#SQL
