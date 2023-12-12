If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatEstantes' and xType = 'P')
    Drop Proc spp_Mtto_CatEstantes
Go--#SQL
  
Create Proc spp_Mtto_CatEstantes ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@IdEstante varchar(8), @Descripcion varchar(50), @Entrepanos int, @iOpcion smallint )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint,
		@iIdEstante int

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @iIdEstante = 0

    
	If @IdEstante = '*' 
		 Begin
			Select @iIdEstante = ( max(IdEstante) + 1 )  
			From CatEstantes (NoLock) 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
		 End 
	Else
		Begin 
			Set @iIdEstante = Cast( @IdEstante As Int )
		End 
	
	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @iIdEstante = IsNull(@iIdEstante, 1)

	If @iOpcion = 1 
       Begin 
		
		   If Not Exists (  Select * From CatEstantes (NoLock) 
							Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								  And IdEstante = @iIdEstante ) 
			  Begin 
				 Insert Into CatEstantes ( IdEmpresa, IdEstado, IdFarmacia, IdEstante, Descripcion, Entrepanos,  
													Status, Actualizado )
				 Select @IdEmpresa, @IdEstado ,@IdFarmacia, @iIdEstante, @Descripcion, @Entrepanos, @sStatus, @iActualizado 
			  End

			Else 
				Begin 
					Update CatEstantes Set Descripcion = @Descripcion, Entrepanos = @Entrepanos, 
					Status = @sStatus, Actualizado = @iActualizado
					Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
								  And IdEstante = @iIdEstante 
				End 
		    
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + Cast( @iIdEstante As varchar) 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatEstantes Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
					And IdEstante = @iIdEstante

		   Set @sMensaje = 'La información del Estante ' + Cast( @iIdEstante As varchar)  + ' ha sido cancelada satisfactoriamente.' 
	   End 	
	
	-- Regresar la Clave Generada
    Select @iIdEstante as Clave, @sMensaje as Mensaje 
End
Go--#SQL
