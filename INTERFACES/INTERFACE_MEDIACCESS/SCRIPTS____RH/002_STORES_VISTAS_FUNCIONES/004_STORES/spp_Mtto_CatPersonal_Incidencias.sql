



If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CatPersonal_Incidencias' and xType = 'P')
    Drop Proc spp_Mtto_CatPersonal_Incidencias
Go--#SQL
  
 ------		 Exec spp_Mtto_CatPersonal_Incidencias '00001270', '01', '2014-05-26', '2014-05-30', 1
  
Create Proc spp_Mtto_CatPersonal_Incidencias 
( 
		@IdPersonal varchar(8), @IdIncidencia varchar(2), @FechaInicio varchar(10), @FechaFin varchar(10), @iOpcion smallint 
)
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


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatPersonal_Incidencias (NoLock) 
							Where IdPersonal = @IdPersonal and IdIncidencia = @IdIncidencia 
							and convert(varchar(10), FechaInicio, 120) = @FechaInicio 
							and convert(varchar(10), FechaFin, 120) = @FechaFin ) 
			  Begin 
				 Insert Into CatPersonal_Incidencias ( IdPersonal, IdIncidencia, FechaInicio, FechaFin, FechaRegistro, Status, Actualizado ) 
				 Select @IdPersonal, @IdIncidencia, convert(datetime,@FechaInicio, 120), 
				 convert(datetime, @FechaFin, 120), GETDATE(), @sStatus, @iActualizado 
              End 
		   
		   Set @sMensaje = 'La información de la Incidencia se guardo satisfactoriamente'
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatPersonal_Incidencias Set Status = @sStatus, Actualizado = @iActualizado 
	       Where IdPersonal = @IdPersonal and IdIncidencia = @IdIncidencia 
			and convert(varchar(10), FechaInicio, 120) = @FechaInicio 
			and convert(varchar(10), FechaFin, 120) = @FechaFin
	       
		   Set @sMensaje = 'La información de la Incidencia ' + @IdIncidencia + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @sMensaje as Mensaje 
End
Go--#SQL
