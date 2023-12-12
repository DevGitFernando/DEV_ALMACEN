If Exists( Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_Registro_Ubicaciones' And xType = 'P' )
	Drop Proc spp_Mtto_Registro_Ubicaciones
Go--#SQL

Create Proc spp_Mtto_Registro_Ubicaciones   
(  
	@IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@IdPasillo int, @IdEstante int, @IdEntrepano int 
)  
With Encryption 
As   
Begin   
Set NoCount On   
Set DateFormat YMD   
Declare @EsConsignacion bit 

--- Pasillos     
	If Not Exists ( Select * From CatPasillos (NoLock)   
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPasillo = @IdPasillo )   
		Begin   
			Insert Into CatPasillos (  IdEmpresa, IdEstado, IdFarmacia, IdPasillo, DescripcionPasillo, Status, Actualizado )  
		    Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, 'PASILLO #' + cast(@IdPasillo as varchar), 'A', '1'
	End   
	
	
--- Estantes 
	If Not Exists ( Select * From CatPasillos_Estantes (NoLock)   
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and IdPasillo = @IdPasillo and IdEstante = @IdEstante )   
		Begin   
			Insert Into CatPasillos_Estantes (  IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, DescripcionEstante, Status, Actualizado )  
		    Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, @IdEstante, 'ESTANTE #' + cast(@IdEstante as varchar), 'A', '1'
	End   	


--- Entrepaños 
	If Not Exists ( Select * From CatPasillos_Estantes_Entrepaños (NoLock)   
			Where IdEmpresa = @IdEmpresa And IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and IdPasillo = @IdPasillo and IdEstante = @IdEstante and IdEntrepaño = @IdEntrepano )   
		Begin   
			Insert Into CatPasillos_Estantes_Entrepaños (  IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño, DescripcionEntrepaño, Status, Actualizado )  
		    Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPasillo, @IdEstante, @IdEntrepano, 'ENTREPAÑO #' + cast(@IdEntrepano as varchar), 'A', '1'
	End   
	
	
  
End  
Go--#SQL
