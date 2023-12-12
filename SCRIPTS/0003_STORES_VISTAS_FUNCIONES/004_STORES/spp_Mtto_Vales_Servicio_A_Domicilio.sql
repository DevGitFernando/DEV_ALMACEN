If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vales_Servicio_A_Domicilio' and xType = 'P' )
    Drop Proc spp_Mtto_Vales_Servicio_A_Domicilio
Go--#SQL 
  
Create Proc spp_Mtto_Vales_Servicio_A_Domicilio 
(	
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(4) = '21', @IdFarmacia varchar(6) = '1021', 
	@FolioServicioDomicilio varchar(32) = '*', @FolioVale varchar(32) = '00000001', 
	@HoraVisita_Desde varchar(10) = '10:00:00', @HoraVisita_Hasta varchar(10) = '15:00:00',   
	@IdPersonal varchar(6) = '0001', @iOpcion smallint = 1, @iOrigenDeServicio smallint = 1
 )
With Encryption  
As
Begin
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@FechaCanje varchar(10)		

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	
	Select @FechaCanje =  Convert(varchar(10), GetDate(), 120 )

	If @FolioServicioDomicilio = '*' 
	Begin 
		Select @FolioServicioDomicilio = cast( (max(FolioServicioDomicilio) + 1) as varchar)  
		From Vales_Servicio_A_Domicilio (NoLock) 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
	End 

	----- Asegurar que FolioVale sea valido y formatear la cadena 
	Set @FolioServicioDomicilio = IsNull(@FolioServicioDomicilio, '1') 
	Set @FolioServicioDomicilio = right(replicate('0', 8) + @FolioServicioDomicilio, 8)  
	

	If @iOpcion = 1 
       Begin 
		   If Not Exists ( Select * From Vales_Servicio_A_Domicilio (NoLock) 
						   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
							And FolioServicioDomicilio = @FolioServicioDomicilio ) 
			  Begin 
				 Insert Into Vales_Servicio_A_Domicilio 
					 ( 
						IdEmpresa, IdEstado, IdFarmacia, FolioServicioDomicilio, FolioVale, FechaRegistro, 
						HoraVisita_Desde, HoraVisita_Hasta,  
					   IdPersonal, IdPersonalConfirma, Status, Actualizado, OrigenDeServicio
					 ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioServicioDomicilio, @FolioVale, getdate() as FechaRegistro, 
					@HoraVisita_Desde, @HoraVisita_Hasta, 
					@IdPersonal, @IdPersonal, @sStatus, @iActualizado, @iOrigenDeServicio  
              End 
              
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioServicioDomicilio 

	   End 


	-- Regresar la Clave Generada
    Select @FolioServicioDomicilio as Clave, @sMensaje as Mensaje 
    
End 
Go--#SQL	
