------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_IFarmatel___SetServiciosADomicilio_Enviado' and xType = 'P') 
    Drop Proc spp_INT_IFarmatel___SetServiciosADomicilio_Enviado 
Go--#SQL 
  
--  ExCB spp_INT_IFarmatel___SetServiciosADomicilio_Enviado '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_IFarmatel___SetServiciosADomicilio_Enviado 
(   
    @IdEmpresa varchar(3) = '002', 
    @IdEstado varchar(2) = '09', 
    @IdFarmacia varchar(4) = '0011', 
    @FolioServicioDomicilio varchar(8) = '1'  
) 
With Encryption 
As 
Begin 
Set NoCount On  
Set 
DateFormat YMD  


	Set @IdEmpresa = right('000000000000' + @IdEmpresa, 3) 
	Set @IdEstado = right('000000000000' + @IdEstado, 2) 
	Set @IdFarmacia = right('000000000000' + @IdFarmacia, 4) 		
	Set @FolioServicioDomicilio = right('000000000000' + @FolioServicioDomicilio, 8)


---------------------------------------------- Obtener los folios de servicios a domicilio 
	Update D Set PedidoEnviado = 1, FechaEnvioPedido = getdate() 
	From Vales_Servicio_A_Domicilio D (NoLock)  
	Inner Join Vales_EmisionEnc I (NoLock) 
		On ( D.IdEmpresa = I.IdEmpresa and D.IdEstado = I.IdEstado and D.IdFarmacia = I.IdFarmacia and D.FolioVale = I.FolioVale ) 	
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia 
		and D.PedidoEnviado = 0 and FolioServicioDomicilio = @FolioServicioDomicilio and OrigenDeServicio = 1 
		

	Update D Set PedidoEnviado = 1, FechaEnvioPedido = getdate() 
	From Vales_Servicio_A_Domicilio D (NoLock)  
	Inner Join VentasEnc I (NoLock) 
		On ( D.IdEmpresa = I.IdEmpresa and D.IdEstado = I.IdEstado and D.IdFarmacia = I.IdFarmacia and D.FolioVale = I.FolioVenta ) 	
	Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia 
		and D.PedidoEnviado = 0 and FolioServicioDomicilio = @FolioServicioDomicilio and OrigenDeServicio = 2 
		
		
		
	----Select 
	----	(D.IdEmpresa + D.IdEstado + D.IdFarmacia + D.FolioServicioDomicilio) as UUID, 	
	----	D.IdEmpresa, D.IdEstado, D.IdFarmacia, D.FolioServicioDomicilio, 
	----	D.FolioVale, D.FolioVentaGenerado, 
	----	D.FechaRegistro, D.IdPersonal, D.HoraVisita_Desde, D.HoraVisita_Hasta, D.ServicioConfirmado, 
	----	D.FechaConfirmacion, D.IdPersonalConfirma, D.TipoSurtimiento, D.ReferenciaSurtimiento, 
	----	D.Status, D.Actualizado, D.FechaControl, 
	----	D.PedidoEnviado, D.FechaEnvioPedido, 		
	----	I.FolioVenta, 
	----	I.IdCliente, 
	----	I.IdSubCliente  
	----From Vales_Servicio_A_Domicilio D (NoLock)  
	----Inner Join Vales_EmisionEnc I (NoLock) 
	----	On ( D.IdEmpresa = I.IdEmpresa and D.IdEstado = I.IdEstado and D.IdFarmacia = I.IdFarmacia and D.FolioVale = I.FolioVale ) 	
	----Where D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmacia = @IdFarmacia 
	----	and  FolioServicioDomicilio = @FolioServicioDomicilio   

	

End  
Go--#SQL 

