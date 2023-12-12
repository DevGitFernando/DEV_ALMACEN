------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_GenerarExistencias__002_Historico' and xType = 'P') 
    Drop Proc spp_INT_ND_GenerarExistencias__002_Historico 
Go--#SQL 
  
--  ExCB spp_INT_ND_GenerarExistencias__002_Historico '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_GenerarExistencias__002_Historico 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', 
    @IdFarmacia varchar(4) = '13', 
    @CodigoCliente varchar(20) = '2179067', 
    @FechaDeProceso varchar(10) = '2015-05-31', 
    @MostrarResultado int = 1     
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  

---	drop table INT_ND__PRCS_Existencias 

	Select 
		F.MovtoDia,  
		F.CodigoCliente, '01' as Modulo, 
		F.ClaveSSA_Base, F.ClaveSSA, 
		F.ClaveSSA_ND, 
		F.DescripcionClave, F.DescripcionComercial, 
		F.CodigoEAN_ND as CodigoEAN, 
		-- F.CodigoEAN, 
		F.CodigoEAN_Base, cast(F.Existencia as int) as Existencia, F.ClaveLote, F.CodigoRelacionado, 
		replace(convert(varchar(10), F.Caducidad, 120), '-', '') as Caducidad,  
		-- replace(convert(varchar(10), getdate(), 120), '-', '') as FechaGeneracion  	
		replace(convert(varchar(10), @FechaDeProceso, 120), '-', '') as FechaGeneracion 		
	From INT_ND__PRCS_Existencias F (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and CodigoCliente = @CodigoCliente 
		and FechaGeneracion = convert(varchar(10), @FechaDeProceso, 120) 
	Order by F.IdEstado, F.IdFarmacia, F.DescripcionClave 	



------------------------- Salida Final 		


	
--		select top 10 * from INT_ND__PRCS_Existencias 	
	
---		spp_INT_ND_GenerarExistencias__002_Historico  	

	
End  
Go--#SQL 

