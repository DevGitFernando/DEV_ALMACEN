
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Vales_Emision_InformacionAdicional' and xType = 'P' ) 
   Drop Proc spp_Mtto_Vales_Emision_InformacionAdicional 
Go--#SQL

Create Proc spp_Mtto_Vales_Emision_InformacionAdicional ( 
    @IdEmpresa varchar(3), @IdEstado varchar(4), @IdFarmacia varchar(6), @FolioVale varchar(32), 
	@IdBeneficiario varchar(8), @NumReceta varchar(20), @FechaReceta datetime, 
	@IdTipoDeDispensacion varchar(2), @IdUnidadMedica varchar(6), 
	@IdMedico varchar(6), @IdBeneficioSP varchar(4), @IdDiagnostico varchar(6), 
	@IdServicio varchar(3), @IdArea varchar(3), @RefObservaciones varchar(100), 
	@iOpcion smallint = 1 )
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


	If Not Exists ( Select * From Vales_Emision_InformacionAdicional (NoLock) 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVale = @FolioVale ) 
	   Begin 
	       Insert Into Vales_Emision_InformacionAdicional ( IdEmpresa, IdEstado, IdFarmacia, FolioVale, 
	       		  IdBeneficiario, NumReceta, FechaReceta, IdTipoDeDispensacion, 
	       		  IdMedico, IdBeneficio, IdDiagnostico, IdUMedica, IdServicio, IdArea, RefObservaciones,  
				  Status, Actualizado ) 
	       Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVale, 
				  @IdBeneficiario, @NumReceta, @FechaReceta, @IdTipoDeDispensacion, 
				  @IdMedico, @IdBeneficioSP, @IdDiagnostico, @IdUnidadMedica, @IdServicio, @IdArea, @RefObservaciones,  
				  @sStatus, @iActualizado 
       End    
	       
End 
Go--#SQL
