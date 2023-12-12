
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_COM_OCEN_Proveedores_Sancionados_Activos' and xType = 'P')
    Drop Proc spp_Mtto_COM_OCEN_Proveedores_Sancionados_Activos
Go--#SQL
  
Create Proc spp_Mtto_COM_OCEN_Proveedores_Sancionados_Activos ( @IdProveedor varchar(4) = '0001', @IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '0001', @IdPersonal varchar(4) = '0001', @Motivo varchar(500) = 'NUEVO ACUERDO' )
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000) 

	Set DateFormat YMD 
	Set @sMensaje = ''

	-- Se inserta en la tabla de Activaciones
	If Not Exists ( Select * From COM_OCEN_Proveedores_Sancionados_Activos(NoLock) Where IdProveedor = @IdProveedor And FechaActivacion = GetDate() ) 
	  Begin 
		 Insert Into COM_OCEN_Proveedores_Sancionados_Activos ( IdProveedor, IdEstado, IdFarmacia, IdPersonal, Motivo, FechaActivacion ) 
		 Select @IdProveedor, @IdEstado, @IdFarmacia, @IdPersonal, @Motivo, GetDate() 
	  End 

	-- Se guarda la informacion en el Historico.
	Insert Into COM_OCEN_Proveedores_Sancionados_Historico ( IdProveedor, IdEstado, IdFarmacia, IdPersonal, Motivo, FechaSancion ) 
	Select Top 1 IdProveedor, IdEstado, IdFarmacia, IdPersonal, Motivo, FechaSancion 
	From COM_OCEN_Proveedores_Sancionados(NoLock) Where IdProveedor = @IdProveedor 
	And IdProveedor Not In 
		( Select IdProveedor From COM_OCEN_Proveedores_Sancionados_Historico(NoLock) 
		  Where IdProveedor = @IdProveedor And FechaSancion = GetDate() )
	Order By FechaSancion Desc
	  

	-- Se elimina de la tabla de Sanciones
	Delete From COM_OCEN_Proveedores_Sancionados Where IdProveedor = @IdProveedor

	Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdProveedor 	   

	-- Regresar la Clave Generada
	Select @IdProveedor as Clave, @sMensaje as Mensaje 
End
Go--#SQL
