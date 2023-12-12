



		/*
		

		Select * From TransferenciasEnc  
		Where IdEmpresa = '001' And IdEstado = '21' And IdFarmacia = '2182' And FolioTransferencia = 'TS00012808' 

		Select * From TransferenciasEnvioEnc  
		Where IdEmpresa = '001' And IdEstadoEnvia = '21' And IdFarmaciaEnvia = '2182' And FolioTransferencia = 'TS00012808' 
		 
	
		*/




Begin Tran 


     -- Rollback tran		--------- Ejecutar si el script termina CON errores  

---		Commit tran			--------- Ejecutar si el script termina SIN errores 



Declare 
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4), 
	@FolioTransferencia varchar(20)  


	Set @IdEmpresa = '001'
	 set @IdEstado = '21' 
	 set @IdFarmacia = '2182' 
	 set @FolioTransferencia = 'TS00015554' 


-------------------- Transitos 
	Exec spp_INV_AplicaDesaplicaExistenciaTransito @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTransferencia, 2 


-------------------- Transferencias 
	Update TransferenciasEnc Set SubTotal = 0, Iva = 0, Total = 0, Status = 'C' 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioTransferencia = @FolioTransferencia  

	Update TransferenciasDet Set Cant_Devuelta = Cant_Enviada, CantidadEnviada = 0, 
		SubTotal = 0, ImpteIva = 0, Importe = 0, Status = 'C' 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioTransferencia = @FolioTransferencia  

	Update TransferenciasDet_Lotes Set CantidadEnviada = 0, Status = 'C' 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioTransferencia = @FolioTransferencia  

	Update TransferenciasDet_Lotes_Ubicaciones Set CantidadEnviada = 0, Status = 'C' 
	Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And FolioTransferencia = @FolioTransferencia  
                    

-------------------- Envio 
	Delete From TransferenciasEnvioDet_LotesRegistrar  
	Where IdEmpresa = @IdEmpresa And IdEstadoEnvia = @IdEstado And IdFarmaciaEnvia = @IdFarmacia And FolioTransferencia = @FolioTransferencia  	

	Delete From TransferenciasEnvioDet_Lotes  
	Where IdEmpresa = @IdEmpresa And IdEstadoEnvia = @IdEstado And IdFarmaciaEnvia = @IdFarmacia And FolioTransferencia = @FolioTransferencia  	

	Delete From TransferenciasEnvioDet  
	Where IdEmpresa = @IdEmpresa And IdEstadoEnvia = @IdEstado And IdFarmaciaEnvia = @IdFarmacia And FolioTransferencia = @FolioTransferencia  	

	Delete From TransferenciasEnvioEnc  
	Where IdEmpresa = @IdEmpresa And IdEstadoEnvia = @IdEstado And IdFarmaciaEnvia = @IdFarmacia And FolioTransferencia = @FolioTransferencia  	
	 




