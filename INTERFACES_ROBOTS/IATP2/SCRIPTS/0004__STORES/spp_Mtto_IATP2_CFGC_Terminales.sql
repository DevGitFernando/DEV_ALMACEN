------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_IATP2_CFGC_Terminales' and xType = 'P')
    Drop Proc spp_Mtto_IATP2_CFGC_Terminales
Go--#SQL  
  
Create Proc spp_Mtto_IATP2_CFGC_Terminales 
( 
	@IdTerminal varchar(3), @Nombre varchar(50), @MAC_Address varchar(20), 
	@iEsInterface smallint, @iOpcion smallint 
)
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


	If @IdTerminal = '*' 
	   Select @IdTerminal = cast( (max(IdTerminal) + 1) as varchar)  From IATP2_CFGC_Terminales (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdTerminal = IsNull(@IdTerminal, '1')
	Set @IdTerminal = right(replicate('0', 3) + @IdTerminal, 3)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From IATP2_CFGC_Terminales (NoLock) Where IdTerminal = @IdTerminal ) 
			  Begin 
				 Insert Into IATP2_CFGC_Terminales ( IdTerminal, Nombre, MAC_Address, EsDeSistema, Status, Actualizado ) 
				 Select @IdTerminal, @Nombre, @MAC_Address, @iEsInterface, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update IATP2_CFGC_Terminales 
					Set Nombre = @Nombre, MAC_Address = @MAC_Address, EsDeSistema = @iEsInterface, 
					Status = @sStatus, Actualizado = @iActualizado
				 Where IdTerminal = @IdTerminal  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdTerminal 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update IATP2_CFGC_Terminales Set Status = @sStatus, Actualizado = @iActualizado Where IdTerminal = @IdTerminal 
		   Set @sMensaje = 'La información de la Terminal ' + @IdTerminal + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdTerminal as Clave, @sMensaje as Mensaje 
End
Go--#SQL  

