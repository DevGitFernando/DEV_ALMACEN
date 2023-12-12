------------------------------------ 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFGC_Terminales' and xType = 'P')
    Drop Proc spp_Mtto_CFGC_Terminales
Go--#SQL  
  
Create Proc spp_Mtto_CFGC_Terminales ( @IdTerminal varchar(4), @Nombre varchar(50), @MAC_Address varchar(20), @iOpcion smallint )
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


	If @IdTerminal = '*' 
	   Select @IdTerminal = cast( (max(IdTerminal) + 1) as varchar)  From CFGC_Terminales (NoLock) 

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdTerminal = IsNull(@IdTerminal, '1')
	Set @IdTerminal = right(replicate('0', 4) + @IdTerminal, 4)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CFGC_Terminales (NoLock) Where IdTerminal = @IdTerminal ) 
			  Begin 
				 Insert Into CFGC_Terminales ( IdTerminal, Nombre, MAC_Address, Status, Actualizado ) 
				 Select @IdTerminal, @Nombre, @MAC_Address, @sStatus, @iActualizado 
              End 
--		   Else 
--			  Begin 
--			     Update CFGC_Terminales Set Nombre = @Nombre, MAC_Address = @MAC_Address, Status = @sStatus, Actualizado = @iActualizado
--				 Where IdTerminal = @IdTerminal  
--              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdTerminal 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFGC_Terminales Set Status = @sStatus, Actualizado = @iActualizado Where IdTerminal = @IdTerminal 
		   Set @sMensaje = 'La información de la Terminal ' + @IdTerminal + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdTerminal as Clave, @sMensaje as Mensaje 
End
Go--#SQL  

