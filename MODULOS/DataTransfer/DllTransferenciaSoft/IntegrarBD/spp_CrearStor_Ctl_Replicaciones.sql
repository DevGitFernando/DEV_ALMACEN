--Drop Table #TempFar
--Drop Table #VentasAdicional
--Drop Table #VentasLotes

If Exists ( Select Name From Sysobjects Where Name = 'spp_CrearStor_Ctl_Replicaciones' and xType = 'P' )
	Drop Proc spp_CrearStor_Ctl_Replicaciones
Go--#SQL

Create Proc spp_CrearStor_Ctl_Replicaciones 
(
    @BaseDeDatosOrigen varchar(100) = 'SII_09_0002_CEDIS_2k20171020', 
    @BaseDeDatosDestino varchar(100) = 'SII_Regional__Tamaulipas',
	@FechaInicio Varchar(10) = '2017-11-01', @FechaFin Varchar(10) = '2018-01-20',
	@VersionExe Varchar(20) = '15.01.01.01', @SalidaFinal varchar(8000) = '' output
) 
with Encryption 
As
Begin 
Set NoCount On

	Declare @sSQl Varchar(max)


	Select '000' As IdEmpresa, IdEstado, IdFarmacia, 
		Cast(0 As Varchar(100)) As RegistrosVentasAdicional, Cast(0 As Varchar(100)) As RegistrosVentasLotes, CAST('' As Varchar(100)) As VersionBD
	Into #TempFar
	From CatFarmacias
	Where IdEstado = '99'

	Select '000' As IdEmpresa, IdEstado, IdFarmacia, cast(0 As Varchar(100)) As Registros
	Into #VentasAdicional
	From CatFarmacias
	Where IdEstado = '99'

	Select '000' As IdEmpresa, IdEstado, IdFarmacia, cast(0 As Varchar(100)) As Registros
	Into #VentasLotes
		From CatFarmacias
	Where IdEstado = '99'

	Set @sSQl = '

	Insert Into #TempFar
	Select
		Distinct IdEmpresa, IdEstado, IdFarmacia, 
		Cast(0 As Varchar(100)) As RegistrosVentasAdicional, Cast(0 As Varchar(100)) As RegistrosVentasLotes, CAST(' + Char(39) + Char(39) + ' As Varchar(100)) As VersionBD
	From ' + @BaseDeDatosOrigen + '.dbo.MovtosInv_Det_CodigosEAN (NoLock)

	Insert Into #VentasAdicional
	Select IdEmpresa, IdEstado, IdFarmacia, cast(COUNT(*) As Varchar(100)) As Registros
	From  ' + @BaseDeDatosOrigen + '.dbo.VentasInformacionAdicional A
	Group By IdEmpresa, IdEstado, IdFarmacia

	Update T Set RegistrosVentasAdicional = Registros
	From #TempFar T
	Inner Join #VentasAdicional A On (T.IdEmpresa = A.IdEmpresa And T.IdEstado = A.IdEstado And T.IdFarmacia = A.IdFarmacia)

	Insert Into #VentasLotes
	Select IdEmpresa, IdEstado, IdFarmacia, cast(COUNT(*) As Varchar(100)) As Registros
	From  '+ @BaseDeDatosOrigen + '.dbo.VentasDet_Lotes A
	Group By IdEmpresa, IdEstado, IdFarmacia

	Update T Set RegistrosVentasLotes = Registros
	From #TempFar T
	Inner Join #VentasLotes A On (T.IdEmpresa = A.IdEmpresa And T.IdEstado = A.IdEstado And T.IdFarmacia = A.IdFarmacia)
	
	Update #TempFar Set VersionBD = (Select Max(dbo.fg_FormatoVersion(Version)) From Net_Versiones Where Tipo = 1)

	'

	Exec(@sSql)
	
	--Select @SalidaFinal = 'Exec ' + @BaseDeDatosDestino + '.dbo.spp_Ctl_Replicaciones @IdEmpresa  = ' + CHAR(39) + IdEmpresa  + CHAR(39) + ', @IdEstado = ' + CHAR(39) + IdEstado  + CHAR(39) +
	--	', @IdFarmacia = ' + CHAR(39) + IdFarmacia + CHAR(39) + ', @FechaIncial = '  + CHAR(39) + @FechaInicio + CHAR(39) +  
	--	', @FechaFinal = '  + CHAR(39) + @FechaFin + CHAR(39) +  ', @BaseDeDatos = '  + CHAR(39) + DB_NAME() + CHAR(39) + 
	--	', @Host = '  + CHAR(39) + @@SERVERNAME + CHAR(39) +
	--	', @RegistrosVentasAdicional = ' + RegistrosVentasAdicional +
	--	', @RegistrosVentasLotes = ' + RegistrosVentasLotes + 
	--	', @VersionBD = ' + Char(39) + VersionBD  + Char(39) +  ', @VersionExe = '+ Char(39) +  @VersionExe   + Char(39)
	----Select *
	--From #TempFar


	Declare @IdEmpresa Varchar(3),
			@IdEstado Varchar(2),
			@IdFarmacia Varchar(4),
			@RegistrosVentasAdicional Varchar(20),
			@RegistrosVentasLotes  Varchar(20),
			@VersionBD  Varchar(20),
			@sSQL2 Varchar(max)

	Set @sSQL2 = ''

	Declare #cursor
	Cursor For 
		Select *
		From #TempFar
	Open #cursor 
	FETCH NEXT FROM #cursor Into @IdEmpresa,  @IdEstado, @IdFarmacia, @RegistrosVentasAdicional, @RegistrosVentasLotes, @VersionBD
		WHILE @@FETCH_STATUS = 0 
		BEGIN

		Set @sSQL2 = @sSQL2 + 'Exec spp_Ctl_Replicaciones @IdEmpresa  = ' + CHAR(39) + @IdEmpresa  + CHAR(39) + ', @IdEstado = ' + CHAR(39) + @IdEstado  + CHAR(39) +
		', @IdFarmacia = ' + CHAR(39) + @IdFarmacia + CHAR(39) + ', @FechaIncial = '  + CHAR(39) + @FechaInicio + CHAR(39) +  
		', @FechaFinal = '  + CHAR(39) + @FechaFin + CHAR(39) +  ', @BaseDeDatos = '  + CHAR(39) + DB_NAME() + CHAR(39) + 
		', @Host = '  + CHAR(39) + @@SERVERNAME + CHAR(39) +
		', @RegistrosVentasAdicional = ' + @RegistrosVentasAdicional +
		', @RegistrosVentasLotes = ' + @RegistrosVentasLotes + 
		', @VersionBD = ' + Char(39) + @VersionBD  + Char(39) +  ', @VersionExe = '+ Char(39) +  @VersionExe   + Char(39) + '    ' --+ Char(13)

			FETCH NEXT FROM #cursor Into @IdEmpresa,  @IdEstado, @IdFarmacia, @RegistrosVentasAdicional, @RegistrosVentasLotes, @VersionBD
		END	 
	Close #cursor 
	Deallocate #cursor

	Set @SalidaFinal = @sSQL2

	--Print(@sSQL2)

End
Go--#SQL