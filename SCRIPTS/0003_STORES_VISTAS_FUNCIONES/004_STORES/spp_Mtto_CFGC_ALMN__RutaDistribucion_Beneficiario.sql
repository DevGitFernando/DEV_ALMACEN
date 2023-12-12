--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFGC_ALMN__RutaDistribucion_Beneficiario' and xType = 'P')
    Drop Proc spp_Mtto_CFGC_ALMN__RutaDistribucion_Beneficiario
Go--#SQL
  
Create Proc spp_Mtto_CFGC_ALMN__RutaDistribucion_Beneficiario
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdRuta varchar(4), @IdCliente varchar(4), @IdSubCliente varchar(4), @IdBeneficiario varchar(8), @IdPersonal varchar(4), 
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
		   If Not Exists ( Select * From CFGC_ALMN__RutaDistribucion_Beneficiario (NoLock) 
							Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdBeneficiario = @IdBeneficiario ) 
			  Begin 
				 Insert Into CFGC_ALMN__RutaDistribucion_Beneficiario ( IdEstado, IdFarmacia, IdRuta, IdCliente, IdSubCliente, IdBeneficiario, Status, Actualizado ) 
				 Select @IdEstado, @IdFarmacia, @IdRuta, @IdCliente, @IdSubCliente, @IdBeneficiario, @sStatus, @iActualizado 
              End 
		   Else 
			  Begin 
			     Update CFGC_ALMN__RutaDistribucion_Beneficiario Set IdRuta = @IdRuta, Status = @sStatus, Actualizado = @iActualizado
				 Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdBeneficiario = @IdBeneficiario
              End 
		   Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdRuta 
	   End 
    Else 
       Begin 
           Set @sStatus = 'C' 
	       Update CFGC_ALMN__RutaDistribucion_Beneficiario Set Status = @sStatus, Actualizado = @iActualizado 
		   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdRuta = @IdRuta And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdBeneficiario = @IdBeneficiario
		   Set @sMensaje = 'La información de la ruta ' + @IdRuta + ' ha sido cancelada satisfactoriamente.' 
	   End 


	---------------- LOG 
	Insert Into CFGC_ALMN__RutaDistribucion__Beneficiario_Historico (  IdEstado, IdFarmacia, IdRuta, IdCliente, IdSubCliente, IdBeneficiario, IdPersonal, FechaRegistro, Status, Actualizado ) 
	Select IdEstado, IdFarmacia, IdRuta, IdCliente, IdSubCliente, IdBeneficiario, @IdPersonal as IdPersonal, getdate() as FechaRegistro, Status, Actualizado 
	From CFGC_ALMN__RutaDistribucion_Beneficiario (NoLock) 
	Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdCliente = @IdCliente And IdSubCliente = @IdSubCliente And IdBeneficiario = @IdBeneficiario


	-- Regresar la Clave Generada
    Select @IdRuta as Clave, @sMensaje as Mensaje 

End
Go--#SQL

---	sp_Listacolumnas  CFGC_ALMN__RutaDistribucion__Beneficiario_Historico 

