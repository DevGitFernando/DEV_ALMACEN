


If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Mtto_CFGC_ALMN_CB_NivelesAtencion_ClavesSSA' and xType = 'P')
    Drop Proc spp_Mtto_CFGC_ALMN_CB_NivelesAtencion_ClavesSSA
Go--#SQL
  
Create Proc spp_Mtto_CFGC_ALMN_CB_NivelesAtencion_ClavesSSA ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', 
		@IdFarmacia varchar(4) = '0001', @IdPerfilAtencion int = 1, @ClaveSSA varchar(30) = '' )
With Encryption 
As
Begin 
Set NoCount On

	If Not Exists ( Select * From CFGC_ALMN_CB_NivelesAtencion_ClavesSSA (NoLock) 
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPerfilAtencion = @IdPerfilAtencion 
			and ClaveSSA = @ClaveSSA ) 
	   Begin 
		  Insert Into CFGC_ALMN_CB_NivelesAtencion_ClavesSSA ( IdEmpresa, IdEstado, IdFarmacia, IdPerfilAtencion, ClaveSSA ) 
		  Select @IdEmpresa, @IdEstado, @IdFarmacia, @IdPerfilAtencion, @ClaveSSA 
	   End 
	Else 
	   Begin 				   
			update CFGC_ALMN_CB_NivelesAtencion_ClavesSSA Set Status = 'A', Actualizado = 0  
			Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdPerfilAtencion = @IdPerfilAtencion 
			and ClaveSSA = @ClaveSSA 	   
	   End 
	   	   
--	Select @IdNivel as Grupo 
End
Go--#SQL 