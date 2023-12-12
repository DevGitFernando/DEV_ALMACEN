---------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) where Name = 'spp_INV_Excedentes' and xType = 'P' ) 
   Drop Proc spp_INV_Excedentes 
Go--#SQL 
   
Create Proc spp_INV_Excedentes 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0224', 
	@MesesAnalisis int = 1000, @MesesStock int = 1000,   
	@ClaveSSA varchar(20) = '101', @Excedente int = 1000 
) 
With Encryption 
As 
Begin 
Set NoCount On 

	Insert Into INV_Distribucion_Excedentes ( IdEmpresa, IdEstado, IdFarmacia, MesesAnalisis, MesesStock, ClaveSSA, Excedente ) 
	Select @IdEmpresa, @IdEstado, @IdFarmacia, @MesesAnalisis, @MesesStock, @ClaveSSA, @Excedente 

End 
Go--#SQL 
