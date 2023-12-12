------------------------------------------------------------------------------------------------------------
------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_EstadosClientes' and xType = 'P' )
   Drop Proc spp_Mtto_CFG_EstadosClientes 
Go--#SQL	
 

Create Proc spp_Mtto_CFG_EstadosClientes ( @IdEstado varchar(2), @IdCliente varchar(4), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFG_EstadosClientes (NoLock) Where IdEstado = @IdEstado and IdCliente = @IdCliente ) 
	   Insert Into CFG_EstadosClientes ( IdEstado, IdCliente, Status, Actualizado ) 
	   Select @IdEstado, @IdCliente, @Status, 0 as Actualizado 
	Else 
	   Update CFG_EstadosClientes Set Status = @Status, Actualizado = 0 Where IdEstado = @IdEstado and IdCliente = @IdCliente 

End 
Go--#SQL

------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_EstadosFarmaciasClientes' and xType = 'P' )
   Drop Proc spp_Mtto_CFG_EstadosFarmaciasClientes 
Go--#SQL

Create Proc spp_Mtto_CFG_EstadosFarmaciasClientes ( @IdEstado varchar(2), @IdFarmacia varchar(4), @IdCliente varchar(4), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFG_EstadosFarmaciasClientes (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente ) 
	   Insert Into CFG_EstadosFarmaciasClientes ( IdEstado, IdFarmacia, IdCliente, Status, Actualizado ) 
	   Select @IdEstado, @IdFarmacia, @IdCliente, @Status, 0 as Actualizado 
	Else 
	   Update CFG_EstadosFarmaciasClientes Set Status = @Status, Actualizado = 0 
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente 

End 
Go--#SQL
------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_EstadosFarmaciasClientesSubClientes' and xType = 'P' )
   Drop Proc spp_Mtto_CFG_EstadosFarmaciasClientesSubClientes 
Go--#SQL

Create Proc spp_Mtto_CFG_EstadosFarmaciasClientesSubClientes ( @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@IdCliente varchar(4), @IdSubCliente varchar(4), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFG_EstadosFarmaciasClientesSubClientes (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and @IdSubCliente = IdSubCliente ) 
	   Insert Into CFG_EstadosFarmaciasClientesSubClientes ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Status, Actualizado ) 
	   Select @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @Status, 0 as Actualizado 
	Else 
	   Update CFG_EstadosFarmaciasClientesSubClientes Set Status = @Status, Actualizado = 0 
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and @IdSubCliente = IdSubCliente 

End 
Go--#SQL


------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_EmpresasFarmacias' and xType = 'P' )
   Drop Proc spp_Mtto_CFG_EmpresasFarmacias 
Go--#SQL

Create Proc spp_Mtto_CFG_EmpresasFarmacias ( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFG_EmpresasFarmacias (NoLock) 
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia ) 
	   Insert Into CFG_EmpresasFarmacias ( IdEmpresa, IdEstado, IdFarmacia, Status, Actualizado ) 
	   Select @IdEmpresa, @IdEstado, @IdFarmacia, @Status, 0 as Actualizado 
	Else 
	   Update CFG_EmpresasFarmacias Set Status = @Status, Actualizado = 0 
	   Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

End 
Go--#SQL


------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_EstadosFarmaciasProgramasSubProgramas' and xType = 'P' )
   Drop Proc spp_Mtto_CFG_EstadosFarmaciasProgramasSubProgramas 
Go--#SQL

Create Proc spp_Mtto_CFG_EstadosFarmaciasProgramasSubProgramas ( @IdEstado varchar(2), @IdFarmacia varchar(4), 
	@IdCliente varchar(4), @IdSubCliente varchar(4), 
	@IdPrograma varchar(4), @IdSubPrograma varchar(4), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFG_EstadosFarmaciasProgramasSubProgramas (NoLock) 
					Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
						and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
						and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma ) 
	   Insert Into CFG_EstadosFarmaciasProgramasSubProgramas ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, IdPrograma, IdSubPrograma, Status, Actualizado ) 
	   Select @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @IdPrograma, @IdSubPrograma, @Status, 0 as Actualizado 
	Else 
	   Update CFG_EstadosFarmaciasProgramasSubProgramas Set Status = @Status, Actualizado = 0 
	   Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
			and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
			and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma  

End 
Go--#SQL