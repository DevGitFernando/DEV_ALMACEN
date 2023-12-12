If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CatEmpresasEstados' and xType = 'P' ) 
   Drop Proc spp_Mtto_CatEmpresasEstados 
Go--#SQL

Create Proc spp_Mtto_CatEmpresasEstados ( @IdEmpresaEdo varchar(4), @IdEmpresa varchar(3), @IdEstado varchar(2), @iOpcion smallint )  
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
	
	If @IdEmpresaEdo = '*' 
	   Select @IdEmpresaEdo = cast( (max(IdEmpresaEdo) + 1) as varchar)  From CatEmpresasEstados (NoLock) 

	-- Asegurar que IdEstado sea valido y formatear la cadena 
	Set @IdEmpresaEdo = IsNull(@IdEmpresaEdo, '1')
	Set @IdEmpresaEdo = right(replicate('0', 4) + @IdEmpresaEdo, 4)


	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From CatEmpresasEstados (NoLock) Where IdEmpresaEdo = @IdEmpresaEdo ) 
			  Begin 
				 Insert Into CatEmpresasEstados ( IdEmpresaEdo, IdEmpresa, IdEstado, Status, Actualizado ) 
				 Select @IdEmpresaEdo, @IdEmpresa, @IdEstado, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CatEmpresasEstados Set Status = @sStatus, Actualizado = @iActualizado
				 Where IdEmpresaEdo = @IdEmpresaEdo  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Id ' + @IdEmpresa 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatEmpresasEstados Set Status = @sStatus, Actualizado = @iActualizado Where IdEmpresaEdo = @IdEmpresaEdo 
		   Set @sMensaje = 'La información de la Relacion Empresa-Estado ' + @IdEmpresa + ' ha sido cancelado satisfactoriamente.' 
	   End 

	-- Regresar el Id Generado
    Select @IdEmpresa as Estado, @sMensaje as Mensaje 


End 
Go--#SQL	
 