If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CtlCortesDiarios' and xType = 'P')
    Drop Proc spp_Mtto_CtlCortesDiarios
Go--#SQL

  
Create Proc spp_Mtto_CtlCortesDiarios 
( 
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @IdPersonal varchar(4), 
	@FechaSistema datetime, @FechaNuevaSistema varchar(10), @iSoloConsulta tinyint = 0, @Observaciones varchar(200) = '',
	@ArbolModulo varchar(10) = '' 
) 
With Encryption 
As
Begin
Set NoCount On

Declare @sMensaje varchar(1000), @IdCorte varchar(2), @TotalCorte numeric(14,4), @TotalVentaTA int,
		@sStatus varchar(1), @iActualizado smallint

	/*Opciones
	Opcion 1.- Insercion. Esta se lleva a cabo cada vez que el usuario entra a la pantalla de Corte Diario.
	Opcion 2.- Busqueda. Se utiliza para saber si el usuario ya efectuo el corte del dia.
	*/

	Set dateformat YMD 
	Set @sMensaje = ''
	Set @IdCorte = ''
	Set @iActualizado = 0 
	Set @TotalCorte = 0
	Set @TotalVentaTA = 0


	-------------------------------------------------
	-- Se obtiene el TotalCorte y el TotalVentasTA --
	-------------------------------------------------
	
	----- En el Corte Diario se consideran las DevDiaAnterior en caso de que la Unidad no devuelva efectivo. 
	Select  -- @TotalCorte = Sum( VentaDiaContado ) - (  Sum( DevVentaDiaContado ) + Sum( DevVentaDiaAntContado )  ),
			@TotalCorte = Sum(VentaDiaContado) - Sum(DevVentaDiaAntContado),	
			@TotalVentaTA = Sum( VentaTAContado ) + Sum(VentaTACredito)
	From CtlCortesParciales(NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Status = 'C'
	And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema 

	Set @TotalCorte = IsNull( @TotalCorte, 0 )
	Set @TotalVentaTA = IsNull( @TotalVentaTA, 0 )


	---------------------------------------
	-- Se ejecuta la opcion seleccionada --
	---------------------------------------
	If @iSoloConsulta = 0 
	   Begin 
			Select @TotalCorte as SaldoCorte  
	   End 
	Else    
	   Begin  
			------------------------------------
			-- Se obtiene el nuevo el IdCorte --
			------------------------------------
			Select @IdCorte = cast( (max(IdCorte) + 1) as varchar)  
			From CtlCortesDiarios (NoLock) 
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia 
				  And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema

			-- Asegurar que IdCorte sea valido y formatear la cadena 
			Set @IdCorte = IsNull(@IdCorte, '1')
			Set @IdCorte = right(replicate('0', 2) + @IdCorte, 2) 

			-- Se inserta en CtlCortesDiarios
			If Not Exists ( Select * From CtlCortesDiarios (NoLock) 
							Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia  
							And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And Status = 'A' And IdCorte = @IdCorte ) 
			  Begin 
				Insert Into CtlCortesDiarios ( IdEmpresa, IdEstado, IdFarmacia,IdPersonal, IdCorte, FechaSistema, FechaRegistro, TotalCorte, TotalVentaTA, Comentario, Status, Actualizado ) 
				Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPersonal, @IdCorte, @FechaSistema, GetDate(), @TotalCorte, @TotalVentaTA, @Observaciones, 'A', @iActualizado 

				Set @sMensaje = 'La información se guardo satisfactoriamente con la clave ' + @IdCorte
			  End 

			---------------------------------------
			-- Se Actualiza la Fecha del Sistema --
			--------------------------------------- 
			Update Net_CFGC_Parametros
			Set Valor = @FechaNuevaSistema, Actualizado = 0
			Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And ArbolModulo = @ArbolModulo And NombreParametro = 'FechaOperacionSistema' 
			---------------------------------------
			-- Se devuelve el mensaje al usuario --
			---------------------------------------
				   
			-- Regresar la Clave Generada
			Select @IdCorte as Clave, @sMensaje as Mensaje 
	   End 
End
Go--#SQL
