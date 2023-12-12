----------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_PRCS_ValidarCURP' and xType = 'P' )
    Drop Proc spp_PRCS_ValidarCURP
Go--#SQL 
  
Create Proc spp_PRCS_ValidarCURP 
( 
	@IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '3224', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0005',  
	@IdBeneficiario varchar(10) = '00000010', 
	@CURP varchar(18) = 'PECM300608MTLXRR04'
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	@sMensaje varchar(1000), 
	@sStatus varchar(1), @iActualizado smallint  

	--------------------- Buscar la CURP de Forma General 
	Select 0 as Pertenece_a_Beneficiario, * 
	Into #tmp__CatBeneficiarios 
	From CatBeneficiarios (NoLock) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and CURP = @CURP 


	--------- validar si el curp pertenece al beneficiario 
	Update B Set Pertenece_a_Beneficiario = 1 
	From #tmp__CatBeneficiarios B 
	Where IdBeneficiario = @IdBeneficiario 


	--		spp_PRCS_ValidarCURP  



	------------------- SALIDA FINAL 
	Select IdBeneficiario, CURP, (ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as NombreBeneficiario  
	From #tmp__CatBeneficiarios 
	Where Pertenece_a_Beneficiario = 0 

	-- select * From #tmp__CatBeneficiarios 
	    
End
Go--#SQL  


