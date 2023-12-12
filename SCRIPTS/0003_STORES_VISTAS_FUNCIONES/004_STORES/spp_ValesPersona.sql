------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_ValesPersona' and xType = 'P' ) 
   Drop Proc spp_ValesPersona
Go--#SQL 

Create Proc spp_ValesPersona
( 
	@IdEstado varchar(2) = '20', @IdFarmacia Varchar(4) = '0130',
	@IdPersonaFirma varchar(8) = '*', @GUID varchar(100) = '', 
	@Nombre varchar(50) = 'aa', @ApPaterno varchar(50) = 'bb', @ApMaterno varchar(50) = 'dd',
	@Parentesco Varchar(2), @EsPersonalFarmacia Bit = 1, @iOpcion smallint = 1
) 
As 
Begin 
Set NoCount On 
 
	Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), @iActualizado smallint  

	Set @sMensaje = '' 
	Set @sStatus = 'A'
	Set @iActualizado = 0
	
	If @IdPersonaFirma = '*'
	Begin 
		Select @IdPersonaFirma = max(IdPersonaFirma) + 1 
		From Vales_Huellas (NoLock) 
	    Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   		
	End
	
	Set @IdPersonaFirma = IsNull(@IdPersonaFirma, '1') 
	Set @IdPersonaFirma = right(replicate('0', 8) + @IdPersonaFirma, 8) 


	If @iOpcion = 1 
       Begin
		   If Not Exists ( Select *
						   From Vales_Huellas (NoLock)
						   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdPersonaFirma = @IdPersonaFirma) 
			  Begin 
				 Insert Into Vales_Huellas ( IdEstado, IdFarmacia, IdPersonaFirma, GUID, Nombre, ApPaterno, ApMaterno, EsPersonalFarmacia, Parentesco, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdPersonaFirma, @GUID, @Nombre, @ApPaterno, @ApMaterno, @EsPersonalFarmacia, @Parentesco, @sStatus, @iActualizado
              End 
		   Else 
			  Begin 
			     Update Vales_Huellas Set Nombre = @Nombre, ApPaterno = @ApPaterno, ApMaterno = @ApMaterno, Parentesco = @Parentesco, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdPersonaFirma = @IdPersonaFirma
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdPersonaFirma 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update Vales_Huellas Set Status = @sStatus, Actualizado = @iActualizado Where IdPersonaFirma = @IdPersonaFirma 
		   Set @sMensaje = 'La información de la persona ' + @IdPersonaFirma + ' ha sido cancelada satisfactoriamente.' 
	   End 

	-- Regresar la Clave Generada
    Select @IdPersonaFirma as Clave, @sMensaje as Mensaje 


End 
Go--#SQL 