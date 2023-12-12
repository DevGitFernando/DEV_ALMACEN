If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Impresion_CortesParciales__INT_MA__Ventas_Pagos' And xType = 'P' )
	Drop Proc spp_Impresion_CortesParciales__INT_MA__Ventas_Pagos
Go--#SQL  

Create Procedure spp_Impresion_CortesParciales__INT_MA__Ventas_Pagos
(
	@IdEmpresa varchar(3) = '002', @IdEstado varchar(2) = '09', @IdFarmacia varchar(4) = '0011', 
	@FechaDeSistema varchar(10) = '2016-04-11', @IdPersonal varchar(4) = '0002'
) 
With Encryption   
As
Begin
Set NoCount On 
Set DateFormat YMD 
Declare @FechaSistema datetime, 
		@EsCorte int, 
		@TasaIvaBase int 


	Set @FechaSistema = cast(@FechaDeSistema as datetime) 

	Select 1 As TipoDeVenta, 'CONTADO' As DescripcionTipoDeVenta
	Into #FormasDePago
	
	Insert Into #FormasDePago
	Select 2 As TipoDeVenta, 'MIXTO' As DescripcionTipoDeVenta
	
	
	select E.TipoDeVenta, DescripcionTipoDeVenta, I.IdFormasDePago, I.FormaDePago,  SUM(Importe) As Importe
	From vw_INT_MA__Ventas_Pagos I (NoLock)
	Inner Join ventasEnc E  (NoLock)
		On (I.IdEmpresa = E.IdEmpresa And I.IdEstado = E.IdEstado And I.IdFarmacia = E.IdFarmacia And I.FolioVenta = E.FolioVenta)
	Inner Join #FormasDePago M (NoLock) On (E.TipoDeVenta = M.TipoDeVenta)
	Where I.IdEmpresa = @IdEmpresa And I.IdEstado = @IdEstado And I.IdFarmacia = @IdFarmacia And E.FechaSistema = @FechaSistema
		And IdPersonal = @IdPersonal And I.IdFormasDePago <> '00'
	Group By E.TipoDeVenta, DescripcionTipoDeVenta, I.IdFormasDePago, I.FormaDePago
	
	
End
Go--#SQL