If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Adt_VentasInformacionAdicional' and xType = 'P' ) 
   Drop Proc spp_Mtto_Adt_VentasInformacionAdicional 
Go--#SQL

Create Proc spp_Mtto_Adt_VentasInformacionAdicional 
( 
	@IdEmpresa varchar(3) = '', 
	@IdEstado varchar(4) = '', @IdFarmacia varchar(6) = '', @FolioVenta varchar(32) = '', 
	@IdBeneficiario varchar(8) = '', @NumReceta varchar(20) = '', @FechaReceta datetime, 
	@IdMedico varchar(6) = '', @IdDiagnostico varchar(6) = '', 
	@IdServicio varchar(3) = '', @IdArea varchar(3) = '', @RefObservaciones varchar(100) = '', 
	@iOpcion smallint = 1, @FolioMovto varchar(8) = '', @IdPersonal varchar(4) = '', 
	@IdTipoDeDispensacion varchar(2) = '', @IdUMedica varchar(6)  = ''  
)
With Encryption 
As 
Begin 
Set NoCount On 

Declare @sMensaje varchar(1000), 
		@sStatus varchar(1), 
		@iActualizado smallint,
		@IdBeneficio varchar(4) 

	/*Opciones
	Opcion 1.- Insercion / Actualizacion
	Opcion 2.- Cancelar ----Eliminar.
	*/

	Set @sMensaje = ''
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @IdBeneficio = '0000'	


	If Exists ( Select * From VentasInformacionAdicional (NoLock) 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta ) 
	    Begin 

		    Insert Into Adt_VentasInformacionAdicional 
			     ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioMovto, IdPersonal, IdBeneficiario, 
					NumReceta, FechaReceta, IdTipoDeDispensacion, IdUMedica, IdMedico, IdBeneficio, IdDiagnostico, 
					IdServicio, IdArea, RefObservaciones, Status, Actualizado ) 
		    Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta, @FolioMovto, @IdPersonal as IdPersonal, IdBeneficiario, 
					NumReceta, FechaReceta, IdTipoDeDispensacion, IdUMedica, IdMedico, IdBeneficio, IdDiagnostico, 
					IdServicio, IdArea, RefObservaciones, Status, Actualizado 
		    From VentasInformacionAdicional 
		    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 

		    Update VentasInformacionAdicional 
				Set IdBeneficiario = @IdBeneficiario, NumReceta = @NumReceta, FechaReceta = @FechaReceta, 
				IdTipoDeDispensacion = @IdTipoDeDispensacion, IdUMedica = @IdUMedica, IdMedico = @IdMedico, 
			    IdDiagnostico = @IdDiagnostico, IdServicio = @IdServicio, IdArea = @IdArea, RefObservaciones = @RefObservaciones, 
			    Status = @sStatus, Actualizado = @iActualizado
		    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta
	    End 
	Else 
	    Begin 
           Insert Into VentasInformacionAdicional ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, 
       		      IdBeneficiario, NumReceta, FechaReceta, IdTipoDeDispensacion, IdUMedica, 
       		      IdMedico, IdDiagnostico, IdServicio, IdArea, RefObservaciones,  
			      Status, Actualizado ) 
           Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, 
			      @IdBeneficiario, @NumReceta, @FechaReceta, @IdTipoDeDispensacion, @IdUMedica, 
			      @IdMedico, @IdDiagnostico, @IdServicio, @IdArea, @RefObservaciones,  
			      @sStatus, @iActualizado 
    	
		    Insert Into Adt_VentasInformacionAdicional 
			     ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, FolioMovto, IdPersonal, IdBeneficiario, 
			     NumReceta, FechaReceta, IdTipoDeDispensacion, IdUMedica, IdMedico, IdBeneficio, 
			     IdDiagnostico, IdServicio, IdArea, RefObservaciones, Status, Actualizado ) 
		    Select IdEmpresa, IdEstado, IdFarmacia, FolioVenta, @FolioMovto, @IdPersonal as IdPersonal, IdBeneficiario, 
				NumReceta, FechaReceta, IdTipoDeDispensacion, IdUMedica, IdMedico, IdBeneficio, 
				IdDiagnostico, IdServicio, IdArea, RefObservaciones, Status, Actualizado 
		    From VentasInformacionAdicional 
		    Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta 	
	    End 

----	If Not Exists ( Select * From Adt_VentasInformacionAdicional ( Nolock )
----		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioVenta = @FolioVenta And FolioMovto = @FolioMovto )	
----	Begin
----		Insert Into Adt_VentasInformacionAdicional 
----		Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, @FolioMovto, @IdPersonal, @IdBeneficiario, @NumReceta, @FechaReceta,
----		@IdMedico, @IdBeneficio, @IdDiagnostico, @IdServicio, @IdArea, @RefObservaciones, @sStatus, @iActualizado
----	End
	 
	       
End 
Go--#SQL 