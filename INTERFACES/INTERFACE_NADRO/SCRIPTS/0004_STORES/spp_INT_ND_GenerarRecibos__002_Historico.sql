------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_GenerarRecibos__002_Historico' and xType = 'P') 
    Drop Proc spp_INT_ND_GenerarRecibos__002_Historico
Go--#SQL 
  
--		ExCB spp_INT_ND_GenerarRecibos__002_Historico '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_GenerarRecibos__002_Historico 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', @IdFarmacia varchar(4) = '0011', 
    @CodigoCliente varchar(20) = '2181002', 
    @FechaInicial varchar(10) = '2015-01-02', @FechaFinal varchar(10) = '2015-01-02', 
    -- @FechaDeProceso varchar(10) = '2014-09-19', 
    @GUID varchar(100) = '', 
    @MostrarResultado int = 1    
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  


	Select 
		IdFarmacia, -- Farmacia, 
		TipoRegistro, CodigoCliente, Modulo, 
		Ticket, Origen, Folio, 
		-- FechaDeRecibo, 
		-- FechaDeDocumento, 
		
		replace(convert(varchar(10), FechaDeRecibo, 120), '-', '') as FechaDeRecibo, 
		replace(convert(varchar(10), FechaDeDocumento, 120), '-', '') as FechaDeDocumento, 		
		
		IdHospitalOrigen, IdModuloOrigen, Motivo, NombreTercero, Motivo2, Estatus, 
		ClaveSSA_ND, ClaveSSA, ClaveSSA_Base, DescripcionClave, DescripcionClaveComercial, --  as DescripcionComercial, 
		CodigoEAN, CodigoNadro, 
		CantidadPedida, CantidadRecibida, ClaveLote, 
		replace(convert(varchar(10), Caducidad, 120), '-', '') as Caducidad, 
		EstatusArticulo, 
		replace(convert(varchar(10), FechaDeRecibo, 120), '-', '') as FechaGeneracion  	
	From INT_ND__PRCS_Recibos  
	Where IdFarmacia =  @IdFarmacia and CodigoCliente = @CodigoCliente 
		and replace(convert(varchar(10), FechaDeRecibo, 120), '-', '') 
			between replace(convert(varchar(10), @FechaInicial, 120), '-', '') 
					and 
					replace(convert(varchar(10), @FechaFinal, 120), '-', '')
	Order By TipoRegistro, Ticket 	
	
	
--		select top 10 * from INT_ND__PRCS_Recibos 
	
	
End  
Go--#SQL 

