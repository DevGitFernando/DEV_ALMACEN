--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFGC_ALMN__RutaDistribucion' and xType = 'P')
    Drop Proc spp_Mtto_CFGC_ALMN__RutaDistribucion
Go--#SQL
  
Create Proc spp_Mtto_CFGC_ALMN__RutaDistribucion 
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdRuta varchar(4), @Descripcion varchar(500), @iOpcion smallint 
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


	If @IdRuta = '*' 
	   Select @IdRuta = cast( (max(IdRuta) + 1) as varchar)  From CFGC_ALMN__RutaDistribucion (NoLock) 
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia

	-- Asegurar que clave sea valido y formatear la cadena 
	Set @IdRuta = IsNull(@IdRuta, '1')
	Set @IdRuta = right(replicate('0', 4) + @IdRuta, 4)


	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select * From CFGC_ALMN__RutaDistribucion (NoLock) 
							Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRuta = @IdRuta ) 
			  Begin 
				 Insert Into CFGC_ALMN__RutaDistribucion ( IdEstado, IdFarmacia, IdRuta, Descripcion, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdRuta, @Descripcion, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFGC_ALMN__RutaDistribucion Set Descripcion = @Descripcion, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRuta = @IdRuta 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdRuta 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFGC_ALMN__RutaDistribucion Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRuta = @IdRuta 
		   Set @sMensaje = 'La información de la ruta ' + @IdRuta + ' ha sido cancelada satisfactoriamente.' 
	   End 


	Update CFGC_ALMN__RutaDistribucion_Beneficiario Set Status = 'C' Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRuta = @IdRuta 
	Update CFGC_ALMN__RutaDistribucion_Transferencia Set Status = 'C' Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRuta = @IdRuta 

	-- Regresar la Clave Generada
    Select @IdRuta as Clave, @sMensaje as Mensaje 

End
Go--#SQL
