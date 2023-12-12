------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_ListadoDeClientes' and xType = 'P') 
    Drop Proc spp_INT_ND_ListadoDeClientes
Go--#SQL  
  
Create Proc spp_INT_ND_ListadoDeClientes 
(   
    @IdEstado varchar(2) = '16', @EsDeSurtimiento smallint = 1, @TipoDeCliente smallint = 1, 
    @EsParaReporteador int = 1   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD  

Declare 
	@iEsDeSurtimiento smallint, 
	@iEsAdministrado smallint  	

Declare 
	@iEsTraspaso smallint, 
	@iEsVentaDirecta smallint  	

	Set @iEsDeSurtimiento = 1  
	Set @iEsAdministrado = 0 

	Set @iEsTraspaso = 1  
	Set @iEsVentaDirecta = 0 


	If @EsDeSurtimiento <> 0 
	Begin 
		If @EsDeSurtimiento = 1 
		   Set @iEsDeSurtimiento = 0 

		If @EsDeSurtimiento = 2 
		   Set @iEsAdministrado = 1 
	End 
	

	If @TipoDeCliente <> 0 
	Begin 
		If @TipoDeCliente = 1 
		   Set @iEsVentaDirecta = 1 

		If @TipoDeCliente = 2 
		   Set @iEsTraspaso = 0  
	End 	
	
	
-------------------------------------------------------------------------------------------------------------- 
	If @EsParaReporteador = 0 
		Begin 
			Select 
				'Código Cliente' = CodigoCliente, 'Nombre Cliente' = NombreCliente, 
				'Id Farmacia' = IdFarmacia, Farmacia, 'Tipo de unidad' = TipoDeUnidad 
			From vw_INT_ND_Clientes C (NoLock)   
			Where IdEstado = @IdEstado and EsDeSurtimiento in ( @iEsDeSurtimiento, @iEsAdministrado ) and Status = 'A'  
			Order by IdFarmacia 
		End 		
	Else 
		Begin 
			Select 
				'Id Farmacia' = IdFarmacia, 'Código Cliente' = CodigoCliente, Farmacia, 0 as Procesar, 0 as Procesado 
			From vw_INT_ND_Clientes C (NoLock)   
			Where IdEstado = @IdEstado and EsDeSurtimiento in ( @iEsDeSurtimiento, @iEsAdministrado ) and Status = 'A' 
			Order by IdFarmacia 
		End 		
	
	
End  
Go--#SQL 

