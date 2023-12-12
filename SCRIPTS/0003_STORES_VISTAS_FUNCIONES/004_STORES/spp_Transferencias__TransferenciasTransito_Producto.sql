If Exists ( select * from sysobjects (NoLock) Where name = 'spp_Transferencias__TransferenciasTransito_Producto' and xType = 'P' ) 
   Drop Proc spp_Transferencias__TransferenciasTransito_Producto 
Go--#SQL 

Create Proc spp_Transferencias__TransferenciasTransito_Producto 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003', 
	@IdProducto varchar(8) = '00009875', @FechaInicial varchar(10) = '2012-07-02', @FechaFinal varchar(10) = '2013-07-02'  
) 
As 
Begin 
Set NoCount On 

	----Select T.Folio, Convert(Varchar(10), T.FechaTransferencia, 120) As 'Fecha', 
	----'Farmacia recibe' = (T.IdFarmaciaRecibe + ' - ' + T.FarmaciaRecibe), Cast(Sum(T.Cantidad) As Int) As Piezas 
	----From vw_TransferenciasDet_CodigosEAN T (NoLock) 
	----Where Left(T.Folio,2) = 'TS' And T.TransferenciaAplicada = 0 And T.IdProducto = '00009875' 
	----And Convert(Varchar(10), T.FechaTransferencia, 120) between '2012-07-02' And '2013-07-02' 
	----Group By T.Folio, Convert(Varchar(10), T.FechaTransferencia, 120), T.IdFarmaciaRecibe , T.FarmaciaRecibe


	Select 'Folio' = T.FolioTransferencia, Convert(Varchar(10), T.FechaTransferencia, 120) As 'Fecha', 
	     T.IdEstadoRecibe, T.IdFarmaciaRecibe, space(200) as FarmaciaRecibe, Cast(Sum(D.CantidadEnviada) As Int) As Piezas 
	Into #tmpTransferencias      
	From TransferenciasEnc T (NoLock) 
	Inner Join TransferenciasDet D (NoLock) 
		On ( T.IdEmpresa = D.IdEmpresa and T.IdEstado = D.IdEstado and T.IdFarmacia = D.IdFarmacia 
			and T.FolioTransferencia = D.FolioTransferencia ) 
	Where 
		T.TipoTransferencia = 'TS' And T.TransferenciaAplicada = 0 
		And T.IdEmpresa = @IdEmpresa And T.IdEstado = @IdEstado And T.IdFarmacia = @IdFarmacia 
		And Convert(Varchar(10), T.FechaTransferencia, 120) between @FechaInicial And @FechaFinal
		And D.IdProducto = @IdProducto
	Group by 
		T.FolioTransferencia, Convert(Varchar(10), T.FechaTransferencia, 120), T.IdEstadoRecibe, T.IdFarmaciaRecibe  

---		spp_Transferencias__TransferenciasTransito_Producto 
	
	Update T Set FarmaciaRecibe = F.Farmacia 
	From #tmpTransferencias T 
	Inner Join vw_Farmacias F (NoLock) On ( T.IdEstadoRecibe = F.IdEstado and T.IdFarmaciaRecibe = F.IdFarmacia ) 
	
	
---------------------- SALIDA FINAL 	
	Select Folio, Fecha, 'Farmacia recibe' = (IdFarmaciaRecibe + ' - ' + FarmaciaRecibe), Piezas 
	From #tmpTransferencias 
	Order by Folio 

End 
Go--#SQL 

	