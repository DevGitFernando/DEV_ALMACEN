

	If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vehiculos' and xType = 'P')
    Drop Proc spp_Mtto_Vehiculos
Go--#SQL
  
Create Proc spp_Mtto_Vehiculos ( @IdEstado varchar(2), @IdFarmacia varchar(4), @IdVehiculo varchar(8), @Marca varchar(200),
		@Descripcion varchar(200), @Modelo Varchar(10), @NumSerie varchar(50), @Placas varchar(20),@iOpcion smallint )
With Encryption 
As
Begin
Set DateFormat YMD
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


	If @IdVehiculo = '*' 
	   Select @IdVehiculo = cast( (max(IdVehiculo) + 1) as varchar)  From CatVehiculos (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

	-- Asegurar que el Folio sea valido y formatear la cadena 
	Set @IdVehiculo = IsNull(@IdVehiculo, '1')
	Set @IdVehiculo = right(replicate('0', 8) + @IdVehiculo, 8)


	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From CatVehiculos (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdVehiculo = @IdVehiculo ) 
			  Begin 
				 Insert Into CatVehiculos ( IdEstado, IdFarmacia, IdVehiculo, Marca, Descripcion, Modelo, NumSerie, Placas, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdVehiculo, @Marca, @Descripcion, @Modelo, @NumSerie, @Placas, @sStatus, @iActualizado 
              End  
		   Else 
			  Begin 
			     Update CatVehiculos
			     Set IdEstado = @IdEstado, IdFarmacia = @IdFarmacia, Marca = @Marca, Descripcion = @Descripcion, Modelo = @Modelo, Status = @sStatus,
						NumSerie = @NumSerie, Placas = @Placas, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdVehiculo = @IdVehiculo  
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdVehiculo 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CatVehiculos Set Status = @sStatus, Actualizado = @iActualizado
	       Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdVehiculo = @IdVehiculo 
		   Set @sMensaje = 'La información del vehiculo ' + @IdVehiculo + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdVehiculo As IdVehiculo, @sMensaje as Mensaje 
End
Go--#SQL