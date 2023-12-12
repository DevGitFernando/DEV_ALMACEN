----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__13__CFG_Farmacias_ConvenioVales' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__13__CFG_Farmacias_ConvenioVales 
Go--#SQL 

Create Proc spp_CFG_OP__13__CFG_Farmacias_ConvenioVales 
(
	@IdEstadoBase varchar(2) = '21', @IdFarmaciaBase varchar(4) = '2005', @IdEstadoDestino varchar(2) = '21', @IdFarmaciaDestino varchar(4) = '3005' 
) 
As 
Begin 
Set NoCount On 

	Set @IdEstadoBase = right('0000' + @IdEstadoBase, 2) 
	Set @IdFarmaciaBase = right('0000' + @IdFarmaciaBase, 4) 
	Set @IdEstadoDestino = right('0000' + @IdEstadoDestino, 2) 
	Set @IdFarmaciaDestino = right('0000' + @IdFarmaciaDestino, 4) 
	
---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, IdFarmaciaConvenio, Status, 0 As Actualizado
	Into #tmp_CFG_Farmacias_ConvenioVales
	From CFG_Farmacias_ConvenioVales P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
		and Status = 'A' 	


	Insert Into CFG_Farmacias_ConvenioVales (  IdEstado, IdFarmacia, IdFarmaciaConvenio, Status, Actualizado ) 
	Select  IdEstado, IdFarmacia, IdFarmaciaConvenio, Status, Actualizado 
	from #tmp_CFG_Farmacias_ConvenioVales P 
	Where Not Exists 
		( 
			Select * From CFG_Farmacias_ConvenioVales C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia And P.IdFarmaciaConvenio = C.IdFarmaciaConvenio
		) 

End 
Go--#SQL 			
		