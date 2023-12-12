If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Vales_EmisionEnc' and xType = 'P' )
    Drop Proc spp_Mtto_Vales_EmisionEnc
Go--#SQL
  
Create Proc spp_Mtto_Vales_EmisionEnc 
(	
	@IdEmpresa varchar(3) = '', @IdEstado varchar(4) = '', @IdFarmacia varchar(6) = '', 
	@FolioVale varchar(32) = '', @FolioVenta varchar(32) = '', 
	@IdPersonal varchar(6) = '', @IdCliente varchar(6) = '', @IdSubCliente varchar(4) = '', 
	@IdPrograma varchar(4) = '', @IdSubPrograma varchar(4) = '', @iOpcion smallint = '', @FolioTimbre int = 0, 
	@FechaSistema varchar(10) 
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

	If @FolioVale = '*' 
	   Select @FolioVale = cast( (max(FolioVale) + 1) as varchar)  From Vales_EmisionEnc (NoLock) 
			  Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

	-- Asegurar que FolioVale sea valido y formatear la cadena 
	Set @FolioVale = IsNull(@FolioVale, '1')
	Set @FolioVale = right(replicate('0', 8) + @FolioVale, 8) 

	If @iOpcion = 1 
       Begin 

		   If Not Exists ( Select * From Vales_EmisionEnc (NoLock) 
						   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale ) 
			  Begin 
				 Insert Into Vales_EmisionEnc 
					 ( IdEmpresa, IdEstado, IdFarmacia, FolioVale, FolioVenta, FechaCanje, 
					   IdPersonal, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, 
					   Status, Actualizado, FolioTimbre, FechaSistema 
					 ) 
				 Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVale, @FolioVenta,@FechaCanje, 
					   @IdPersonal, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, 
					   @sStatus, @iActualizado, @FolioTimbre, @FechaSistema 
              End 
		   Else 
			  Begin 
				Set @sStatus = 'R'
				Update Vales_EmisionEnc Set FechaCanje = @FechaCanje, Status = @sStatus, Actualizado = @iActualizado
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale 
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con el Folio ' + @FolioVale 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update Vales_EmisionEnc Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale
 
		   Set @sMensaje = 'La información del Folio ' + @FolioVale + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @FolioVale as Clave, @sMensaje as Mensaje 
End
Go--#SQL	
