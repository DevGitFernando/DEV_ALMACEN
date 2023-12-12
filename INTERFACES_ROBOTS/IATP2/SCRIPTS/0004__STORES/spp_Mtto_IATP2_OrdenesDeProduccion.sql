-------------------------------------------------------------------------------------------------------------------------------------  
-------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_IATP2_OrdenesDeProduccion' and xType = 'P' ) 
   Drop Proc spp_Mtto_IATP2_OrdenesDeProduccion
Go--#SQL 

Create Proc spp_Mtto_IATP2_OrdenesDeProduccion 
( 
	@FolioSolicitud varchar(10) = '*', 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '3132', 

	@IdCliente varchar(4) = '0002', 
	@IdSubCliente varchar(4) = '0005', 
	@IdPrograma varchar(4) = '0002', 
	@IdSubPrograma varchar(4) = '0001', 

	@NumeroDeDocumento varchar(50) = '', 
	@Observaciones varchar(500) = '', 

	@Status varchar(1) = 'A' 
) 
With Encryption
As 
Begin 
Set DateFormat YMD 

Declare @sMensaje varchar(500)  

	If @FolioSolicitud = '*' 
	Begin 
		Select @FolioSolicitud = cast(max(FolioSolicitud) + 1 as varchar(10)) 
		From IATP2_OrdenesDeProduccion (NoLock) 

	End 

	Set @FolioSolicitud = isnull(@FolioSolicitud, '1') 
	Set @FolioSolicitud = RIGHT('000000000000000' + @FolioSolicitud, 10) 



	If Not Exists ( Select * From IATP2_OrdenesDeProduccion (NoLock) Where FolioSolicitud = @FolioSolicitud ) 
		Begin 
			Insert Into IATP2_OrdenesDeProduccion 
			( 
				FolioSolicitud, IdEmpresa, IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, NumeroDeDocumento, Observaciones, Status 
			) 
			Select  @FolioSolicitud, @IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, @NumeroDeDocumento, @Observaciones, @Status
		End 

	Set @sMensaje = 'Información guardada satisfactoriamente con el folio ' + @FolioSolicitud 


	Select @FolioSolicitud as FolioSolicitud, @sMensaje as Mensaje 

End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------  
-------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_IATP2_OrdenesDeProduccion_InformacionAdicional' and xType = 'P' ) 
   Drop Proc spp_Mtto_IATP2_OrdenesDeProduccion_InformacionAdicional
Go--#SQL 

Create Proc spp_Mtto_IATP2_OrdenesDeProduccion_InformacionAdicional 
( 
	@FolioSolicitud varchar(10) = '*', 
	@Consecutivo int = 0, 
	 
	@IdEmpresa varchar(3) = '001', 
	@IdEstado varchar(2) = '21', 
	@IdFarmacia varchar(4) = '3132', 
	@FolioVenta varchar(30) = '', 

	@IdBeneficiario varchar(8) = '', 
	@IdTipoDeDispensacion varchar(2) = '', 
	@NumeroDeHabitacion varchar(20) = '', 
	@NumeroDeCama varchar(20) = '', 
	@NumReceta varchar(20) = '', 
	@FechaReceta datetime = '', 
	@IdMedico varchar(6) = '', 
	@IdBeneficio varchar(4) = '0000', 
	@IdDiagnostico varchar(6) = '000000', 
    @IdUMedica varchar(6) = '000000', 	
	@IdServicio varchar(3) = '', 
	@IdArea varchar(3) = '',  
	@RefObservaciones varchar(100) = '' , 

	@Status varchar(1) = 'A' 
) 
With Encryption
As 
Begin 
Set DateFormat YMD 

	If Not Exists 
		( 
			Select * From IATP2_OrdenesDeProduccion_InformacionAdicional (NoLock) 
			Where FolioSolicitud = @FolioSolicitud  and Consecutivo = @Consecutivo 
		) 
	Begin 
		Insert Into IATP2_OrdenesDeProduccion_InformacionAdicional 
		( 
			FolioSolicitud, Consecutivo, IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdBeneficiario, IdTipoDeDispensacion, NumeroDeHabitacion, NumeroDeCama, 
			NumReceta, FechaReceta, IdMedico, IdBeneficio, IdDiagnostico, IdUMedica, IdServicio, IdArea, RefObservaciones, Status
		) 
		Select  
			@FolioSolicitud, @Consecutivo, @IdEmpresa, @IdEstado, @IdFarmacia, @FolioVenta, @IdBeneficiario, @IdTipoDeDispensacion, @NumeroDeHabitacion, @NumeroDeCama, 
			@NumReceta, @FechaReceta, @IdMedico, @IdBeneficio, @IdDiagnostico, @IdUMedica, @IdServicio, @IdArea, @RefObservaciones, @Status  
	End 


End 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------------------  
-------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_IATP2_OrdenesDeProduccion_Productos' and xType = 'P' ) 
   Drop Proc spp_Mtto_IATP2_OrdenesDeProduccion_Productos
Go--#SQL 

Create Proc spp_Mtto_IATP2_OrdenesDeProduccion_Productos 
( 
	@FolioSolicitud varchar(10) = '*', 
	@Consecutivo int = 0, 
	
	@Partida int = 0, 
	@IdProducto varchar(8) = '', 
	@CodigoEAN varchar(30) = '', 
	@CantidadSolicitada int = 0, 
	@FechaHora_De_Administracion varchar(20) = '', 
	@Status varchar(1) = 'A' 
) 
With Encryption
As 
Begin 
Set DateFormat YMD 

Declare 
	@dtFechaHora_Administracion datetime 

	Set @dtFechaHora_Administracion = cast(@FechaHora_De_Administracion as datetime) 


	If Not Exists 
		( 
			Select * From IATP2_OrdenesDeProduccion_Productos (NoLock) 
			Where FolioSolicitud = @FolioSolicitud  and Consecutivo = @Consecutivo 
				and IdProducto = @IdProducto and CodigoEAN = @CodigoEAN 
		) 
	Begin 
		Insert Into IATP2_OrdenesDeProduccion_Productos 
		( 
			FolioSolicitud, Consecutivo, Partida, IdProducto, CodigoEAN, CantidadSolicitada, FechaHora_De_Administracion, Status
		) 
		Select  
			@FolioSolicitud, @Consecutivo, @Partida, @IdProducto, @CodigoEAN, @CantidadSolicitada, @FechaHora_De_Administracion, @Status 
	End 



End 
Go--#SQL 