--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_FACT_CFG__FarmaciasRemisionado' and xType = 'P' ) 
   Drop Proc spp_Mtto_FACT_CFG__FarmaciasRemisionado 
Go--#SQL  

Create Proc spp_Mtto_FACT_CFG__FarmaciasRemisionado
( 
	@IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0003', @Prioridad int = 0, @Status varchar(1) = 'A'
) 
with Encryption 
As 
Begin
Set NoCount On


	Set @IdEstado = RIGHT('0000' + @IdEstado, 2)
	Set @IdFarmacia = RIGHT('0000' + @IdFarmacia, 4) 

	If Not Exists ( Select * From FACT_CFG__FarmaciasRemisionado (NoLock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia) 
		Begin 
			Insert Into FACT_CFG__FarmaciasRemisionado ( IdEstado, IdFarmacia, Prioridad,Status) 
			Select @IdEstado, @IdFarmacia, @Prioridad, @Status
		End 			    
	Else 
	Begin 
		Update A Set Prioridad = @Prioridad, Status = @Status
		From FACT_CFG__FarmaciasRemisionado A (NoLock) 
		Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
	End 

End 
Go--#SQL 
