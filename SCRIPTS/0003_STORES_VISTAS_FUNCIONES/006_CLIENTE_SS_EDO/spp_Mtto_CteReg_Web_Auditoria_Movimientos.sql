If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CteReg_Web_Auditoria_Movimientos' and xType = 'P')
    Drop Proc spp_Mtto_CteReg_Web_Auditoria_Movimientos
Go--#SQL

Create Proc spp_Mtto_CteReg_Web_Auditoria_Movimientos 
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), @IdUsuario varchar(4), @IdSesion varchar(8), @IdMovto varchar(8), @IP_Address varchar(20), @Modulo varchar(50), 
	@Pantalla varchar(100), @Instruccion varchar(7500), @Url_Farmacia varchar(200), @GeneraReporte bit = 0
)
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), 
		@iActualizado smallint  

	-- Se inicializan las variables.
	Set @sMensaje = ''
	Set @iActualizado = 0

	If @IdMovto = '*' 
	   Select @IdMovto = cast( (max(IdMovto) + 1) as varchar)  From CteReg_Web_Auditoria_Movimientos (NoLock)
	   Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdUsuario = @IdUsuario And IdSesion = @IdSesion

	-- Asegurar que IdFamlia sea valido y formatear la cadena 
	Set @IdMovto = IsNull(@IdMovto, '1')
	Set @IdMovto = right(replicate('0', 8) + @IdMovto, 8)

	-- Si la sesion no existe, se inserta.
	If Not Exists ( Select * From CteReg_Web_Auditoria_Movimientos (NoLock) Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And IdUsuario = @IdUsuario And IdSesion = @IdSesion And IdMovto = @IdMovto  ) 
	  Begin 
		 Insert Into CteReg_Web_Auditoria_Movimientos ( IdEstado, IdFarmacia, IdUsuario, IdSesion, IdMovto, FechaRegistro, IP_Address, Modulo, Pantalla, Instruccion, Url_Farmacia, Actualizado, GeneraReporte ) 
		 Select @IdEstado, @IdFarmacia, @IdUsuario, @IdSesion, @IdMovto, GetDate(), @IP_Address, @Modulo, @Pantalla, @Instruccion, @Url_Farmacia, @iActualizado, @GeneraReporte 
	  End 
	Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdSesion

	-- Regresar la Clave Generada
    Select @IdSesion as Clave, @sMensaje as Mensaje 
End
Go--#SQL
