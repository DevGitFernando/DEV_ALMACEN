
If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CortesParciales_Datos' And xType = 'P' ) 
	Drop Proc spp_Mtto_CortesParciales_Datos
Go--#SQL

Create Procedure dbo.spp_Mtto_CortesParciales_Datos   
( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @FechaSistema datetime, @IdPersonal varchar(4) )  
With Encryption 
As  
Begin  
 Declare   
	@DotacionInicial float,  
	@VentaDiaContado float,  
	@VentaDiaCredito float,  
	@DevVentaDiaContado float,  
	@DevVentaDiaCredito float,  
	@DevVentaDiaAntContado float,  
	@DevVentaDiaAntCredito float,
	@VentaTiempoAireContado int,
	@VentaTiempoAireCredito int,
	@TotalCorteParcial float,
	@TotalTiempoAire int,
	@TotalContado float,  
	@TotalCredito float,
	@TotalGeneral float,
	@TotalEfectivo float
  
	Set @DotacionInicial = 0  
	Set @VentaDiaContado = 0  
	Set @VentaDiaCredito = 0  
	Set @DevVentaDiaContado = 0  
	Set @DevVentaDiaCredito = 0  
	Set @DevVentaDiaAntContado = 0  
	Set @DevVentaDiaAntCredito = 0
	Set @VentaTiempoAireContado = 0
	Set @VentaTiempoAireCredito = 0
	Set @TotalCorteParcial = 0
	Set @TotalTiempoAire = 0
	Set @TotalContado = 0
	Set @TotalCredito = 0
	Set @TotalGeneral = 0
	Set @TotalEfectivo = 0
  
	-------------------------  
	-- Se obtiene el Monto --  
	-------------------------  
	Select @DotacionInicial = DotacionInicial 
	From CtlCortesParciales(NoLock)
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
	And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And IdPersonal = @IdPersonal    
  
	------------------------------------  
	-- Se obtienen las Ventas del Dia --  
	------------------------------------

	-- Se obtiene la venta de Contado  
	Select @VentaDiaContado = IsNull( Sum( Total ), 0 )  
	From VentasEnc (NoLock)   
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
	And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And IdPersonal = @IdPersonal  
	And TipoDeVenta = 1 And Corte = 0

	-- Se obtiene la venta a Credito  
	Select @VentaDiaCredito = IsNull( Sum( Total ),0 )  
	From VentasEnc (NoLock)   
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
	And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema And IdPersonal = @IdPersonal  
	And TipoDeVenta = 2 And Corte = 0
   
   
	------------------------------------------  
	-- Se obtienen las Devoluciones del Dia --  
	------------------------------------------

	-- Se obtiene la Devolucion de Contado  
	Select @DevVentaDiaContado = IsNull( Sum( Total ), 0 )  
	From DevolucionesEnc D (NoLock)   
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado And D.IdFarmacia = @IdFarmacia   
	And Convert( varchar(10), D.FechaSistema, 120 ) = @FechaSistema 
	And Convert( varchar(10), D.FechaSistema, 120 ) = Convert( varchar(10), D.FechaSistemaDevol, 120 )
	And D.IdPersonal = @IdPersonal  
	And D.TipoDeDevolucion = 1 And Corte = 0
	And Exists  ( Select * From VentasEnc V (NoLock) 
				Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado And V.IdFarmacia = @IdFarmacia 
					  and V.FolioVenta = D.Referencia and V.TipoDeVenta = 1  ) 	 
  
	-- Se obtiene la Devolucion a Credito  
	Select @DevVentaDiaCredito = IsNull( Sum( Total ), 0 )  
	From DevolucionesEnc D (NoLock)   
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado And D.IdFarmacia = @IdFarmacia   
	And Convert( varchar(10), D.FechaSistema, 120 ) = @FechaSistema 
	And Convert( varchar(10), D.FechaSistema, 120 ) = Convert( varchar(10), D.FechaSistemaDevol, 120 )
	And D.IdPersonal = @IdPersonal  
	And D.TipoDeDevolucion = 1 And Corte = 0
	And Exists  ( Select * From VentasEnc V (NoLock) 
				Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado And V.IdFarmacia = @IdFarmacia 
					  and V.FolioVenta = D.Referencia and V.TipoDeVenta = 2  ) 	   
  
	-----------------------------------------------------  
	-- Se obtienen las Devoluciones de dias anteriores --  
	-----------------------------------------------------

	-- Se obtiene la Devolucion de Contado de dias anteriores  
	Select @DevVentaDiaAntContado = IsNull( Sum( Total ), 0 )  
	From DevolucionesEnc D (NoLock)   
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado And D.IdFarmacia = @IdFarmacia   
	And Convert( varchar(10), D.FechaSistema, 120 ) = @FechaSistema 
	And Convert( varchar(10), D.FechaSistema, 120 ) <> Convert( varchar(10), D.FechaSistemaDevol, 120 )
	And D.IdPersonal = @IdPersonal  
	And D.TipoDeDevolucion = 1 And D.Corte = 0 
	And Exists  ( Select * From VentasEnc V (NoLock) 
				Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado And V.IdFarmacia = @IdFarmacia 
					  and V.FolioVenta = D.Referencia and V.TipoDeVenta = 1  ) 
  
	-- Se obtiene la Devolucion a Credito de dias anteriores  
	Select @DevVentaDiaAntCredito = IsNull( Sum( Total ), 0 )  
	From DevolucionesEnc D (NoLock)   
	Where D.IdEmpresa = @IdEmpresa And D.IdEstado = @IdEstado And D.IdFarmacia = @IdFarmacia   
	And Convert( varchar(10), D.FechaSistema, 120 ) = @FechaSistema 
	And Convert( varchar(10), D.FechaSistema, 120 ) <> Convert( varchar(10), D.FechaSistemaDevol, 120 )
	And D.IdPersonal = @IdPersonal  
	And D.TipoDeDevolucion = 1 And D.Corte = 0
	And Exists ( Select * From VentasEnc V (NoLock) 
				Where V.IdEmpresa = @IdEmpresa And V.IdEstado = @IdEstado And V.IdFarmacia = @IdFarmacia 
					  and V.FolioVenta = D.Referencia and V.TipoDeVenta = 2  ) 	 

	-------------------------------------------  
	-- Se obtienen las Ventas de Tiempo Aire --  
	-------------------------------------------

	-- Se obtiene la Venta de Tiempo Aire de Contado
	Select @VentaTiempoAireContado = IsNull( Sum( Monto ), 0 )  
	From Ventas_TiempoAire (NoLock)   
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
	And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema 
	And IdPersonal = @IdPersonal 
	And TipoTA = 1 And Corte = 0

	-- Se obtiene la Venta de Tiempo Aire a Credito
	Select @VentaTiempoAireCredito = IsNull( Sum( Monto ), 0 )  
	From Ventas_TiempoAire (NoLock)   
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia   
	And Convert( varchar(10), FechaSistema, 120 ) = @FechaSistema 
	And IdPersonal = @IdPersonal 
	And TipoTA = 2 And Corte = 0

	-----------------------------------------------
	-- Se obtienen los Totales del Corte Parcial --  
	----------------------------------------------- 
 
	Set @TotalCorteParcial = @VentaDiaContado - ( @DevVentaDiaContado + @DevVentaDiaAntContado )
	Set @TotalTiempoAire = @VentaTiempoAireContado + @VentaTiempoAireCredito
	Set @TotalContado = ( ( @VentaDiaContado + @TotalTiempoAire ) - ( @DevVentaDiaContado + @DevVentaDiaAntContado ) ) 
	Set @TotalCredito = ( @VentaDiaCredito - ( @DevVentaDiaCredito + @DevVentaDiaAntCredito ) )
	Set @TotalGeneral = ( @TotalContado + @TotalCredito )
	Set @TotalEfectivo =( ( @DotacionInicial + @TotalContado ) - @VentaTiempoAireCredito )

	----------------------------  
	-- Se devuelven los datos --  
	----------------------------    
	Select	@DotacionInicial as DotacionInicial, @VentaDiaContado as VentaDiaContado, @VentaDiaCredito as VentaDiaCredito,  
			@DevVentaDiaContado as DevVentaDiaContado, @DevVentaDiaCredito as DevVentaDiaCredito,   
			@DevVentaDiaAntContado as DevVentaDiaAntContado, @DevVentaDiaAntCredito as DevVentaDiaAntCredito, 
			@VentaTiempoAireContado as VentaTiempoAireContado, @VentaTiempoAireCredito as VentaTiempoAireCredito,
			@TotalCorteParcial as TotalCorteParcial, @TotalTiempoAire as TotalTiempoAire, @TotalContado as TotalContado, 
			@TotalCredito as TotalCredito, @TotalGeneral as TotalGeneral, @TotalEfectivo as TotalEfectivo
  
End
Go--#SQL	
