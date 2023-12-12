
----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_ValidaCantidadClavesSubPerfil' and xType = 'P')
    Drop Proc spp_ValidaCantidadClavesSubPerfil
Go--#SQL

--		Exec spp_ValidaCantidadClavesSubPerfil '003', '16', '0011', '0002', '0002', '0001', 'tmptest'
  
Create Proc spp_ValidaCantidadClavesSubPerfil 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@IdPrograma varchar(4) = '', @IdSubPrograma varchar(4) = '', 
	@Tabla varchar(100) = '', @TipoProceso int = 0, @TipoDeRecetaAtendida varchar(4) = ''   
)
With Encryption 
As
Begin 
Set NoCount On

	If @TipoProceso = 1 
		Exec spp_ValidaCantidadClavesSubPerfil____Farmacia @IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdSubCliente, @TipoDeRecetaAtendida, @Tabla 
	Else 
		Exec spp_ValidaCantidadClavesSubPerfil___ProgramaAtencion @IdEmpresa, @IdEstado, @IdFarmacia, @IdCliente, @IdPrograma, @IdSubPrograma, @TipoDeRecetaAtendida, @Tabla 	
	
End
Go--#SQL	


