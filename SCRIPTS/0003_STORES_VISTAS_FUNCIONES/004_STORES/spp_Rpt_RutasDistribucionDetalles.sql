If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_RutasDistribucionDetalles' and xType = 'P')
    Drop Proc spp_Rpt_RutasDistribucionDetalles
Go--#SQL
  
Create Proc spp_Rpt_RutasDistribucionDetalles 
( 
	@IdEmpresa Varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2182', 
	@Tipo int = 0, @DiasRevision int = 15, @OrigenDatos int = 2  
) 
With Encryption 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare  
	@FechaMinima varchar(10),  
	@Pedidos int, 
	@Movtos int 
	
	Set @FechaMinima = convert(varchar(10), dateadd(dd, -1 * @DiasRevision, getdate()), 120)  
	Set @Pedidos = 1  
	Set @Movtos = 1  

	If @OrigenDatos = 1 
	Begin  
	   Set @Pedidos = 1   
	   Set @Movtos = 0  	   
	End    

	If @OrigenDatos = 2 
	Begin  
	   Set @Pedidos = 0   
	   Set @Movtos = 1   	   
	End    


--	Select  @FechaMinima as Fecha, @Pedidos as Pedidos, @Movtos as Movtos 
	If (@Tipo = 0)
		Begin 
			Select * 
			Into #tmp_Transferencias 
			From 
			( 
					Select 
						T.IdEstadoRecibe, T.IdFarmaciaRecibe, 
						RIGHT(T.FolioTransferencia,8) As Folio , Convert(Varchar(10), T.FechaTransferencia, 120) As 'Fecha',
						(T.IdFarmaciaRecibe + ' - ' + cast('' as varchar(200))) As FarmaciaRecibe, 
						Cast(Sum(D.CantidadEnviada) As Int) As Piezas, 1 as Origen 
					From TransferenciasEnc T (NoLock) 
					Inner Join TransferenciasDet D (NoLock)
						On ( T.IdEmpresa = D.IdEmpresa And T.IdEstado = D.IdEstado And T.IdFarmacia = D.IdFarmacia And
							T.FolioTransferencia = D.FolioTransferencia ) 
					Inner Join Pedidos_Cedis_Enc_Surtido S (NoLock)
						On (T.IdEmpresa = S.IdEmpresa And T.IdEstado = S.IdEstado And T.IdFarmacia = S.IdFarmacia And
							T.FolioTransferencia = S.FolioTransferenciaReferencia And S.Status = 'E' )
					Where 
						@Pedidos = 1 and  
						T.TransferenciaAplicada = 'False'  And T.IdEmpresa = @IdEmpresa  And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia And
						LEFT(T.FolioTransferencia, 2) = 'TS' And Convert(Varchar(10), T.FechaTransferencia, 120) >= @FechaMinima And D.CantidadEnviada > 0
						--And RIGHT(T.FolioTransferencia, 8) Not In (Select R.FolioTransferenciaVenta
						--										   From vw_RutasDistribucionDetTrans R (NoLock)
						--										   Where T.IdEmpresa = R.IdEmpresa And T.IdEstado = R.IdEstado And T.IdFarmacia = R.IdFarmacia )
					Group  By T.IdEstadoRecibe, T.FolioTransferencia, T.FechaTransferencia, T.IdFarmaciaRecibe -- , T.FarmaciaRecibe
					-- Order By RIGHT(T.Folio,8) 
				UNION 
					Select 
						T.IdEstadoRecibe, T.IdFarmaciaRecibe, 
						RIGHT(T.FolioTransferencia,8) As Folio , Convert(Varchar(10), T.FechaTransferencia, 120) As 'Fecha',
						(T.IdFarmaciaRecibe + ' - ' + cast('' as varchar(200))) As FarmaciaRecibe, 
						Cast(Sum(D.CantidadEnviada) As Int) As Piezas, 2 as Origen 
					From TransferenciasEnc T (NoLock) 
					Inner Join TransferenciasDet D (NoLock)
						On ( T.IdEmpresa = D.IdEmpresa And T.IdEstado = D.IdEstado And T.IdFarmacia = D.IdFarmacia And
							T.FolioTransferencia = D.FolioTransferencia ) 
					Where 
						@Movtos = 1 and 
						T.TransferenciaAplicada = 'False'  And T.IdEmpresa = @IdEmpresa  And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia And
						LEFT(T.FolioTransferencia, 2) = 'TS' and Convert(Varchar(10), T.FechaTransferencia, 120) >= @FechaMinima  And D.CantidadEnviada > 0
						--And RIGHT(T.FolioTransferencia, 8) Not In (Select R.FolioTransferenciaVenta
						--	            						   From vw_RutasDistribucionDetTrans R (NoLock)
						--										   Where T.IdEmpresa = R.IdEmpresa And T.IdEstado = R.IdEstado And T.IdFarmacia = R.IdFarmacia )
					Group  By T.IdEstadoRecibe, T.FolioTransferencia, T.FechaTransferencia, T.IdFarmaciaRecibe -- , T.FarmaciaRecibe
			) as L 
			Order By Fecha desc, Folio desc, FarmaciaRecibe, Piezas
			
			Delete EV
			From #tmp_Transferencias EV
			Inner Join RutasDistribucionDet R On (EV.Folio = R.FolioTransferenciaVenta)
			Where R.IdEmpresa = @IdEmpresa And R.IdEstado = @IdEstado And R.IdFarmacia = @IdFarmacia And
				Tipo = 'T' And FolioDevuelto = 0 And Status = 'A'
			
			
			Update T Set FarmaciaRecibe = (F.IdFarmacia + ' - ' + F.Farmacia)
			From #tmp_Transferencias T 
			Inner Join vw_Farmacias F (NoLock) On ( T.IdEstadoRecibe = F.IdEstado and T.IdFarmaciaRecibe = F.IdFarmacia ) 
			
			Select Distinct Folio, Fecha, FarmaciaRecibe, Piezas--, Origen 
			From #tmp_Transferencias 
			
			--		spp_Rpt_RutasDistribucionDetalles 
			
		End
	Else
		Begin 
			Select *  
			Into #tmp_Ventas 
			From 
			( 
					Select Folio, Convert(Varchar(10), EV.FechaRegistro, 120) As Fecha, Beneficiario, Sum(CantidadVendida) As Piezas, 3 as Origen 
					From VentasEnc EV (NoLock)
					Inner Join VentasDet DV (NoLock)
						On (EV.IdEmpresa = DV.IdEmpresa And EV.IdEstado = DV.IdEstado And EV.IdFarmacia = DV.IdFarmacia And EV.FolioVenta = DV.FolioVenta)
					Inner Join vw_VentasDispensacion_InformacionAdicional A (NoLock)
						On (EV.IdEmpresa = A.IdEmpresa And EV.IdEstado = A.IdEstado And EV.IdFarmacia = A.IdFarmacia And
							EV.FolioVenta = A.Folio And EV.IdCliente = A.IdCliente And EV.IdSubCliente = A.IdSubCliente) 
					Inner Join Pedidos_Cedis_Enc_Surtido S (NoLock)
						On (EV.IdEmpresa = S.IdEmpresa And EV.IdEstado = S.IdEstado And EV.IdFarmacia = S.IdFarmacia And
							'SV' + EV.FolioVenta = S.FolioTransferenciaReferencia And S.Status = 'E')
					Where @Pedidos = 1 and 
						EV.IdEmpresa = @IdEmpresa And EV.IdEstado = @IdEstado And EV.IdFarmacia = @IdFarmacia And CantidadVendida > 0 And 
						Convert(Varchar(10), EV.FechaRegistro, 120) >= @FechaMinima
						--And Folio Not In (Select R.FolioTransferenciaVenta
						--				  From vw_RutasDistribucionDetVentas R (NoLock)
						--				  Where EV.IdEmpresa = R.IdEmpresa And EV.IdEstado = R.IdEstado And EV.IdFarmacia = R.IdFarmacia )
					Group By Folio, EV.FechaRegistro, Beneficiario 
				UNION 
					Select Folio, Convert(Varchar(10), EV.FechaRegistro, 120) As Fecha, Beneficiario, Sum(CantidadVendida) As Piezas, 4 as Origen 
					From VentasEnc EV (NoLock)
					Inner Join VentasDet DV (NoLock)
						On (EV.IdEmpresa = DV.IdEmpresa And EV.IdEstado = DV.IdEstado And EV.IdFarmacia = DV.IdFarmacia And EV.FolioVenta = DV.FolioVenta)
					Inner Join vw_VentasDispensacion_InformacionAdicional A (NoLock)
						On (EV.IdEmpresa = A.IdEmpresa And EV.IdEstado = A.IdEstado And EV.IdFarmacia = A.IdFarmacia And
							EV.FolioVenta = A.Folio And EV.IdCliente = A.IdCliente And EV.IdSubCliente = A.IdSubCliente)
					Where @Movtos  = 1 and 
						EV.IdEmpresa = @IdEmpresa And EV.IdEstado = @IdEstado And EV.IdFarmacia = @IdFarmacia And CantidadVendida > 0  
						and Convert(Varchar(10), EV.FechaRegistro, 120) >= @FechaMinima
						--And Folio Not In (Select R.FolioTransferenciaVenta
						--				  From vw_RutasDistribucionDetVentas R (NoLock)
						--				  Where EV.IdEmpresa = R.IdEmpresa And EV.IdEstado = R.IdEstado And EV.IdFarmacia = R.IdFarmacia )
					Group By Folio, EV.FechaRegistro, Beneficiario 				
			) as L 
			Order by Fecha desc, Folio desc, Beneficiario, Piezas 	
			
			Delete EV
			From #tmp_Ventas EV
			Inner Join RutasDistribucionDet R On (EV.Folio = R.FolioTransferenciaVenta)
			Where R.IdEmpresa = @IdEmpresa And R.IdEstado = @IdEstado And R.IdFarmacia = @IdFarmacia And
				Tipo = 'V' And FolioDevuelto = 0 And Status = 'A' 
			
			
			Select Distinct Folio, Fecha, Beneficiario, Piezas--, Origen 
			From #tmp_Ventas 
			
						
		End  

End 
Go--#SQL