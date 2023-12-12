If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Operaciones_Supervisadas' and xType = 'P' ) 
   Drop Proc spp_Net_Operaciones_Supervisadas 
Go--#SQL 

Create Proc spp_Net_Operaciones_Supervisadas 
( 
	@IdOperacion varchar(4), @Nombre varchar(50), @Descripcion varchar(200), @iOpcion tinyint  
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

	If @IdOperacion = '*' 
	   Begin 
	       Select @IdOperacion = cast( max(IdOperacion) + 1 as varchar) 
	       From Net_Operaciones_Supervisadas (NoLock) 
	   End 

	Set @IdOperacion = IsNull(@IdOperacion, '1') 
	Set @IdOperacion = right(replicate('0', 4) + @IdOperacion, 4)

	If @iOpcion = 1 
	   Begin 
	      If Not Exists ( Select * From Net_Operaciones_Supervisadas (NoLock) Where IdOperacion = @IdOperacion ) 
	         Insert Into Net_Operaciones_Supervisadas ( IdOperacion, Nombre, Descripcion, Status, Actualizado ) 
	         Select @IdOperacion, @Nombre, @Descripcion, @sStatus, @iActualizado  
	      Else 
	         Update Net_Operaciones_Supervisadas 
					Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado  
	         Where IdOperacion = @IdOperacion    
	   End 
	Else 
	   Begin 
		  Set @sStatus = 'C'
	      Update Net_Operaciones_Supervisadas Set Status = @sStatus, Actualizado = @iActualizado  
	      Where IdOperacion = @IdOperacion    	   
	   End    

	Select @IdOperacion as IdOperacion, @sMensaje as Mensaje 

End 
Go--#SQL     

-------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Permisos_Operaciones_Supervisadas' and xType = 'P' ) 
   Drop Proc spp_Net_Permisos_Operaciones_Supervisadas 
Go--#SQL 

Create Proc spp_Net_Permisos_Operaciones_Supervisadas 
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdPersonal varchar(4), @IdOperacion varchar(4), @iOpcion tinyint  
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
	-- IdEstado, IdFarmacia, IdPersonal, IdOperacion

	If @iOpcion = 2 
	   Set @sStatus = 'C' 
	
	If Not Exists ( Select * From Net_Permisos_Operaciones_Supervisadas (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal and IdOperacion = @IdOperacion )
	   Insert Into Net_Permisos_Operaciones_Supervisadas ( IdEstado, IdFarmacia, IdPersonal, IdOperacion, Status, Actualizado ) 
	   Select @IdEstado, @IdFarmacia, @IdPersonal, @IdOperacion, @sStatus, @iActualizado 
	Else 
	   Update Net_Permisos_Operaciones_Supervisadas Set FechaUpdate = getdate(), Status = @sStatus, Actualizado = @iActualizado
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPersonal = @IdPersonal and IdOperacion = @IdOperacion 	
End 
Go--#SQL 

