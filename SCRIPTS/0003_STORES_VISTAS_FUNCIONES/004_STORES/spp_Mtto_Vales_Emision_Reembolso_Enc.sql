If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vales_Emision_Reembolso_Enc' and xType = 'P' )
    Drop Proc spp_Mtto_Vales_Emision_Reembolso_Enc
Go--#SQL
  
Create Proc spp_Mtto_Vales_Emision_Reembolso_Enc 
(	@IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioValeReembolso varchar(32), @FolioVale varchar(32), 
	@IdPersonal varchar(6), @iOpcion smallint 
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
	

	If @FolioValeReembolso = '*' 
	   Select @FolioValeReembolso = cast( (max(FolioValeReembolso) + 1) as varchar)  From Vales_Emision_Reembolso_Enc (NoLock) 
			  Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	-- Asegurar que FolioValeReembolso sea valido y formatear la cadena 
	Set @FolioValeReembolso = IsNull(@FolioValeReembolso, '1')
	Set @FolioValeReembolso = right(replicate('0', 8) + @FolioValeReembolso, 8) 

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From Vales_Emision_Reembolso_Enc (NoLock) 
						   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioValeReembolso = @FolioValeReembolso ) 
			  Begin 
				 Insert Into Vales_Emision_Reembolso_Enc 
					 ( IdEmpresa, IdEstado, IdFarmacia, FolioValeReembolso, FolioVale,  
					   FechaRegistro, IdPersonal, Status, Actualizado
					 ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioValeReembolso, @FolioVale, 
					   GetDate(), @IdPersonal, @sStatus, @iActualizado 

				Update Vales_EmisionEnc Set EsSegundoVale = 1, Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And FolioVale = @FolioVale

              End 
		   
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioValeReembolso
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update Vales_Emision_Reembolso_Enc Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioValeReembolso = @FolioValeReembolso
 
		   Set @sMensaje = 'La información del Folio ' + @FolioValeReembolso + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioValeReembolso as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
