---------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_PRCS_CFDI__ComplementoDePagos_ObtenerDatos' and xType = 'P' ) 
   Drop Proc spp_PRCS_CFDI__ComplementoDePagos_ObtenerDatos 
Go--#SQL 

Create Proc spp_PRCS_CFDI__ComplementoDePagos_ObtenerDatos   
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(4) = '11', @IdFarmacia varchar(4) = '1', @RFC varchar(20) = 'ISP961122JV5' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 

	Set @IdEmpresa = right('00' +  @IdEmpresa, 3) 
	Set @IdEstado = right('000000' +  @IdEstado, 2) 
	Set @IdFarmacia = right('00000' +  @IdFarmacia, 4) 		
	
----------------------- SALIDA FINAL	

	Select  
		Serie, Folio, UUID, NumParcialidad, ValorNominal, Importe_Abonos, Importe_SaldoAnterior, Importe_Pagado, Importe_SaldoInsoluto 
	From CFDI_ComplementoDePagos__CargaMasiva (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and 
		ExisteFactura = 1  and EsFacturaDeRFC = 1 
	Order by Serie, Folio 

----------------------- SALIDA FINAL	


---		spp_PRCS_FACT__ComplementoDePagos_ObtenerDatos


End 
Go--#SQL 



