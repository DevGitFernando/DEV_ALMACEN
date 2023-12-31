------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_VentasInformacionAdicional' and xType = 'P' ) 
   Drop Proc spp_Mtto_VentasInformacionAdicional 
Go--#SQL

Create Proc spp_Mtto_VentasInformacionAdicional 
( 
    @IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioVenta varchar(32), 
	@IdBeneficiario varchar(8), @NumReceta varchar(50), @FechaReceta datetime, 
	@IdTipoDeDispensacion varchar(2), @IdUnidadMedica varchar(6), 
	@IdMedico varchar(6), @IdBeneficioSP varchar(4), @IdDiagnostico varchar(6), 
	@IdServicio varchar(3), @IdArea varchar(3), @RefObservaciones varchar(100), 
	@iOpcion smallint = 1, @NumeroDeHabitacion varchar(20) = '', @NumeroDeCama varchar(20) = '', 
	@IdEstadoResidencia varchar(2) = '',  @IdTipoDerechoHabiencia varchar(3) = ''  
)
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


	If Not Exists ( Select * From VentasInformacionAdicional (NoLock) Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta ) 
		Begin 
			Insert Into VentasInformacionAdicional 
			( 
				IdEmpresa, IdEstado, IdFarmacia, FolioVenta, 
				IdBeneficiario, NumReceta, FechaReceta, IdTipoDeDispensacion, 
				IdMedico, IdBeneficio, IdDiagnostico, IdUMedica, IdServicio, IdArea, RefObservaciones,  
				Status, Actualizado, NumeroDeHabitacion,  NumeroDeCama, IdEstadoResidencia, IdTipoDerechoHabiencia 
			) 
			Select 
				@IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, 
				@IdBeneficiario, @NumReceta, @FechaReceta, @IdTipoDeDispensacion, 
				@IdMedico, @IdBeneficioSP, @IdDiagnostico, @IdUnidadMedica, @IdServicio, @IdArea, @RefObservaciones,  
				@sStatus, @iActualizado, @NumeroDeHabitacion,  @NumeroDeCama, @IdEstadoResidencia, @IdTipoDerechoHabiencia 


			Update B Set Status = B.Status 
			From CatBeneficiarios B  
			inner join VentasEnc V (NoLock) On ( V.IdEmpresa = @IdEmpresa and B.IdEstado = V.IdEstado and B.IdFarmacia = V.IdFarmacia and V.FolioVenta = @FolioVenta and B.IdCliente = V.IdCliente and B.IdSubCliente = V.IdSubCliente )  
			Where B.IdEstado = @IdEstado and B.IdFarmacia = @IdFarmacia and B.IdBeneficiario = @IdBeneficiario 

			Update B Set Status = B.Status 
			From CatMedicos B  
			Where B.IdEstado = @IdEstado and B.IdFarmacia = @IdFarmacia and B.IdMedico = @IdMedico 

		End    
	       
End 
Go--#SQL
