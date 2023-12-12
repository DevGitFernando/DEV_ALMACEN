-------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_CFD_MetodosPago' and xType = 'P')
    Drop Proc spp_Mtto_FACT_CFD_MetodosPago
Go--#SQL

  
Create Proc spp_Mtto_FACT_CFD_MetodosPago 
(	
	@IdMetodoPago varchar(2), @Descripcion varchar(100), @iOpcion smallint 
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint
		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0


	If @IdMetodoPago = '*' 
	  Begin 
		Select @IdMetodoPago = cast( (max(IdMetodoPago) + 1) as varchar)  
		From FACT_CFD_MetodosPago (NoLock)		
	  End 

	-- Asegurar que FolioContrarecibo sea valido y formatear la cadena 
	Set @IdMetodoPago = IsNull(@IdMetodoPago, '1')
	Set @IdMetodoPago = right(replicate('0', 2) + @IdMetodoPago, 2)


	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From FACT_CFD_MetodosPago (NoLock) 
						   Where IdMetodoPago = @IdMetodoPago ) 
			  Begin 
				 Insert Into FACT_CFD_MetodosPago ( IdMetodoPago, Descripcion, Status, Actualizado ) 
				 Select @IdMetodoPago, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update FACT_CFD_MetodosPago Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				Where IdMetodoPago = @IdMetodoPago
              End
 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @IdMetodoPago 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update FACT_CFD_MetodosPago Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdMetodoPago = @IdMetodoPago
			 
		    Set @sMensaje = 'La información del Folio ' + @IdMetodoPago + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada 
    Select @IdMetodoPago as Folio, @sMensaje as Mensaje 
End
Go--#SQL 	


-------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_FACT_CFD_FormasDePago' and xType = 'P')
    Drop Proc spp_Mtto_FACT_CFD_FormasDePago
Go--#SQL
  
Create Proc spp_Mtto_FACT_CFD_FormasDePago 
(	
	@IdCondicionPago varchar(2), @Descripcion varchar(100), @iOpcion smallint 
 )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint
		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0


	If @IdCondicionPago = '*' 
	  Begin 
		Select @IdCondicionPago = cast( (max(IdCondicionPago) + 1) as varchar)  
		From FACT_CFD_FormasDePago (NoLock)		
	  End 

	-- Asegurar que FolioContrarecibo sea valido y formatear la cadena 
	Set @IdCondicionPago = IsNull(@IdCondicionPago, '1')
	Set @IdCondicionPago = right(replicate('0', 2) + @IdCondicionPago, 2)


	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From FACT_CFD_FormasDePago (NoLock) 
						   Where IdCondicionPago = @IdCondicionPago ) 
			  Begin 
				 Insert Into FACT_CFD_FormasDePago ( IdCondicionPago, Descripcion, Status, Actualizado ) 
				 Select @IdCondicionPago, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
				Update FACT_CFD_FormasDePago Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				Where IdCondicionPago = @IdCondicionPago
              End
 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @IdCondicionPago 
	   End 
    Else 
       Begin 
			Set @sStatus = 'C' 
			Update FACT_CFD_FormasDePago Set Status = @sStatus, Actualizado = @iActualizado 
			Where IdCondicionPago = @IdCondicionPago
			 
		    Set @sMensaje = 'La información del Folio ' + @IdCondicionPago + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdCondicionPago as Folio, @sMensaje as Mensaje 
End
Go--#SQL 