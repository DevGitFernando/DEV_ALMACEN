If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_PedidosInformacionAdicional' and xType = 'P' ) 
   Drop Proc spp_Mtto_PedidosInformacionAdicional 
Go--#SQL

Create Proc spp_Mtto_PedidosInformacionAdicional ( 
    @IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioPedido varchar(32), 
	@IdBeneficiario varchar(8), @NumReceta varchar(20), @FechaReceta datetime, 
	@IdTipoDeDispensacion varchar(2), @IdUnidadMedica varchar(6), 
	@IdMedico varchar(6), @IdBeneficioSP varchar(4), @IdDiagnostico varchar(6), 
	@IdServicio varchar(3), @IdArea varchar(3), @RefObservaciones varchar(100), 
	@iOpcion smallint = 1, @NumeroDeHabitacion varchar(20) = '', @NumeroDeCama varchar(20) = '' )
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint 

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0	


	If Not Exists ( Select * From PedidosInformacionAdicional (NoLock) 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioPedido = @FolioPedido ) 
	   Begin 
	       Insert Into PedidosInformacionAdicional ( IdEmpresa, IdEstado, IdFarmacia, FolioPedido, 
	       		  IdBeneficiario, NumReceta, FechaReceta, IdTipoDeDispensacion, 
	       		  IdMedico, IdBeneficio, IdDiagnostico, IdUMedica, IdServicio, IdArea, RefObservaciones,  
				  Status, Actualizado, NumeroDeHabitacion,  NumeroDeCama ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioPedido, 
				  @IdBeneficiario, @NumReceta, @FechaReceta, @IdTipoDeDispensacion, 
				  @IdMedico, @IdBeneficioSP, @IdDiagnostico, @IdUnidadMedica, @IdServicio, @IdArea, @RefObservaciones,  
				  @sStatus, @iActualizado, @NumeroDeHabitacion,  @NumeroDeCama
       End    
	       
End 
Go--#SQL
