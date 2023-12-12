Set DateFormat YMD 
Go--#SQL 

Declare 
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4) 
	
	
	Set @IdEmpresa = '001'
	Set @IdEstado = '21' 
	Set @IdFarmacia = '2116' 
	Set @FolioTransferencia = 'TS00000076' 
	 
	 
	Update T Set FolioTransferencia = 'TS00000080', IdFarmaciaRecibe = '2117' 
	From TransferenciasEnc T (NoLock)  	
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioTransferencia = @FolioTransferencia 
	
	
Go--#SQL 
