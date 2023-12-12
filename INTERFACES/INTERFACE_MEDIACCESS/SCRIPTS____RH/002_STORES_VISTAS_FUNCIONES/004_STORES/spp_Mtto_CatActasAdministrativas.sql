

	If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatActasAdministrativas' and xType = 'P')
    Drop Proc spp_Mtto_CatActasAdministrativas
Go--#SQL
  
Create Proc spp_Mtto_CatActasAdministrativas ( @IdActa varchar(8), @IdEstado varchar(2), @IdFarmacia varchar(4), 
		@IdPersonal_Acta varchar(8), @IdPersonal_Representante varchar(8), @IdPersonal_Testigo_01 varchar(8), 
		@IdPersonal_Testigo_02 varchar(8), @FechaActa varchar(10), @Hechos varchar(1000), @iOpcion smallint )
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


	If @IdActa = '*' 
	   Select @IdActa = cast( (max(IdActa) + 1) as varchar)  From CatActasAdministrativas (NoLock) 

	-- Asegurar que IdActa sea valido y formatear la cadena 
	Set @IdActa = IsNull(@IdActa, '1')
	Set @IdActa = right(replicate('0', 8) + @IdActa, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatActasAdministrativas (NoLock) Where IdActa = @IdActa ) 
			  Begin 
				 Insert Into CatActasAdministrativas ( IdActa, IdEstado, IdFarmacia, IdPersonal_Acta, IdPersonal_Representante, 
														IdPersonal_Testigo_01, IdPersonal_Testigo_02, 
														FechaActa, FechaRegistro, Hechos, Status, Actualizado ) 
				 Select @IdActa, @IdEstado, @IdFarmacia, @IdPersonal_Acta, @IdPersonal_Representante, 
				 @IdPersonal_Testigo_01, @IdPersonal_Testigo_02, @FechaActa, GetDate(), @Hechos, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatActasAdministrativas Set Hechos = @Hechos, Status = @sStatus, Actualizado = @iActualizado
				 Where IdActa = @IdActa  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdActa 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatActasAdministrativas Set Status = @sStatus, Actualizado = @iActualizado Where IdActa = @IdActa 
		   Set @sMensaje = 'La información del Acta Administrativa ' + @IdActa + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdActa as Folio, @sMensaje as Mensaje 
End
Go--#SQL
