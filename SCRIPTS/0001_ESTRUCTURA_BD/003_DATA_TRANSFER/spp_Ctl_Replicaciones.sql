If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Ctl_Replicaciones' And xType = 'P' )
	Drop Proc spp_Ctl_Replicaciones 
Go--#SQL


Create Procedure spp_Ctl_Replicaciones 
(
	@IdEmpresa varchar(3) = '001', @IdEstado Varchar(2) = '11', @IdFarmacia Varchar(4)= '0052',
	@FechaIncial Varchar(10)= '01-06-2016', @FechaFinal Varchar(10) = '02-06-2016',
	@BaseDeDatos Varchar(50) = 'SubBase2', @Host Varchar(50) = 'otrapc',
	@RegistrosVentasAdicional int = 0, @RegistrosVentasLotes int = 0, @VersionBD Varchar(100) = '', @VersionExe  Varchar(100) = ''
)
With Encryption 	
As
Begin

	Declare @RegionalVentasAdcional Int,
			@RegionalVentasLotes Int,
			@Cuadrada Bit 
			
    Set @Cuadrada = 0
			
	Select @RegionalVentasAdcional = Count(*)
	From VentasInformacionAdicional (NoLock)
	Where idEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	
	Select @RegionalVentasLotes = Count(*)
	From ventasDet_Lotes (NoLock)
	Where idEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia

	if (@RegistrosVentasAdicional = @RegionalVentasAdcional And @RegistrosVentasLotes = @RegionalVentasLotes)
	Begin
	   Set @Cuadrada = 1
	End



	
	if (@VersionBD <> '')
	Begin
		Set @VersionBD = dbo.fg_FormatoVersionConPunto(@VersionBD)
	End
	
	If Not Exists( Select * From Ctl_Replicaciones Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia)
		Begin
			Insert Into Ctl_Replicaciones (IdEmpresa, IdEstado, IdFarmacia, FechaInicial, FechaFinal, NombreBaseDeDatos, Host,
				RegistrosVentasAdicional, RegistrosVentasLotes, VersionBD, VersionExe, RegionalVentasAdcional, RegionalVentasLotes, Cuadrada)
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FechaIncial, @FechaFinal, @BaseDeDatos, @Host,
				@RegistrosVentasAdicional, @RegistrosVentasLotes, @VersionBD, @VersionExe, @RegionalVentasAdcional, @RegionalVentasLotes, @Cuadrada
		End
	Else
		Begin
			Update Ctl_Replicaciones
			Set FechaInicial = @FechaIncial, FechaFinal = @FechaFinal, NombreBaseDeDatos = @BaseDeDatos, Host = @Host, FechaUpdate = GetDate(),
				RegistrosVentasAdicional = @RegistrosVentasAdicional, RegistrosVentasLotes = @RegistrosVentasLotes,
				RegionalVentasAdcional = @RegionalVentasAdcional, RegionalVentasLotes = @RegionalVentasLotes,
				VersionBD = @VersionBD, VersionExe = @VersionExe, Cuadrada = @Cuadrada
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
		End
		
	Insert Into Ctl_Replicaciones_Historico (IdEmpresa, IdEstado, IdFarmacia, FechaInicial, FechaFinal, NombreBaseDeDatos, Host,
		RegistrosVentasAdicional, RegistrosVentasLotes, VersionBD, VersionExe, RegionalVentasAdcional, RegionalVentasLotes, Cuadrada)
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @FechaIncial, @FechaFinal, @BaseDeDatos, @Host, 
		@RegistrosVentasAdicional, @RegistrosVentasLotes, @VersionBD, @VersionExe, @RegionalVentasAdcional, @RegionalVentasLotes, @Cuadrada
		
	   	   
End 
Go--#SQL