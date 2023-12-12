
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_Proveedores_Sancionados' and xType = 'P')
    Drop Proc spp_Mtto_COM_OCEN_Proveedores_Sancionados
Go--#SQL
  
Create Proc spp_Mtto_COM_OCEN_Proveedores_Sancionados ( @IdProveedor varchar(4) = '0001', @IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '0001', @IdPersonal varchar(4) = '0001', @Motivo varchar(500) = 'NO CUMPLE' )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000) 

	Set DateFormat YMD 
	Set @sMensaje = ''

	-- Se guarda la informacion en los Proveedores Sancionados.
	If Not Exists ( Select * From COM_OCEN_Proveedores_Sancionados(NoLock) Where IdProveedor = @IdProveedor And FechaSancion = GetDate() ) 
	  Begin 
		 Insert Into COM_OCEN_Proveedores_Sancionados ( IdProveedor, IdEstado, IdFarmacia, IdPersonal, Motivo, FechaSancion ) 
		 Select @IdProveedor, @IdEstado, @IdFarmacia, @IdPersonal, @Motivo, GetDate() 
	  End 

	Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdProveedor 
	   

	-- Regresar la Clave Generada
    Select @IdProveedor as Clave, @sMensaje as Mensaje 
End
Go--#SQL
