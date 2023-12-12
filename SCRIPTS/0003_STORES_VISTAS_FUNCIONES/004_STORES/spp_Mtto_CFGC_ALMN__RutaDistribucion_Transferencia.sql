--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFGC_ALMN__RutaDistribucion_Transferencia' and xType = 'P')
    Drop Proc spp_Mtto_CFGC_ALMN__RutaDistribucion_Transferencia
Go--#SQL
  
Create Proc spp_Mtto_CFGC_ALMN__RutaDistribucion_Transferencia
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdRuta varchar(4), @IdEstadoEnvia varchar(2), @IdFarmaciaEnvia varchar(4), @IdPersonal varchar(4), 
	@iOpcion smallint 
)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), @sStatus varchar(1), @iActualizado smallint

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0 

	If @iOpcion = 1
       Begin
		   If Not Exists ( Select * From CFGC_ALMN__RutaDistribucion_Transferencia (NoLock) 
							Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And IdEstadoEnvia = @IdEstadoEnvia And IdFarmaciaEnvia = @IdFarmaciaEnvia ) 
			  Begin 
				 Insert Into CFGC_ALMN__RutaDistribucion_Transferencia ( IdEstado, IdFarmacia, IdRuta, IdEstadoEnvia, IdFarmaciaEnvia, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdRuta, @IdEstadoEnvia, @IdFarmaciaEnvia, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFGC_ALMN__RutaDistribucion_Transferencia Set IdRuta = @IdRuta, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdEstadoEnvia = @IdEstadoEnvia And IdFarmaciaEnvia = @IdFarmaciaEnvia
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdRuta 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFGC_ALMN__RutaDistribucion_Transferencia Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRuta = @IdRuta And IdEstadoEnvia = @IdEstadoEnvia And IdFarmaciaEnvia = @IdFarmaciaEnvia
		   Set @sMensaje = 'La información de la ruta ' + @IdRuta + ' ha sido cancelada satisfactoriamente.' 
	   End 


	---------------- LOG 
	Insert Into CFGC_ALMN__RutaDistribucion_Transferencia__Beneficiario_Historico (  IdEstado, IdFarmacia, IdRuta, IdEstadoEnvia, IdFarmaciaEnvia, IdPersonal, FechaRegistro, Status, Actualizado ) 
	Select IdEstado, IdFarmacia, IdRuta, IdEstadoEnvia, IdFarmaciaEnvia, @IdPersonal as IdPersonal, getdate() as FechaRegistro, Status, Actualizado 
	From CFGC_ALMN__RutaDistribucion_Transferencia (NoLock) 
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdEstadoEnvia = @IdEstadoEnvia And IdFarmaciaEnvia = @IdFarmaciaEnvia  
	

	-- Regresar la Clave Generada
    Select @IdRuta as Clave, @sMensaje as Mensaje 

End
Go--#SQL
