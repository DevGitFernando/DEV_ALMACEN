----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__08__Permisos' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__08__Permisos 
Go--#SQL 

Create Proc spp_CFG_OP__08__Permisos   
(
	@IdEstadoBase varchar(2) = '13', @IdFarmaciaBase varchar(4) = '0011', @IdEstadoDestino varchar(2) = '13', @IdFarmaciaDestino varchar(4) = '0012' 
) 
As 
Begin 
Set NoCount On 

	
	Set @IdEstadoBase = right('0000' + @IdEstadoBase, 2) 
	Set @IdFarmaciaBase = right('0000' + @IdFarmaciaBase, 4) 
	Set @IdEstadoDestino = right('0000' + @IdEstadoDestino, 2) 
	Set @IdFarmaciaDestino = right('0000' + @IdFarmaciaDestino, 4) 



---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdSucursal, IdGrupo, NombreGrupo, Status, Actualizado
	Into #tmp_Grupos  
	From Net_Grupos_De_Usuarios P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdSucursal = @IdFarmaciaBase  
	

	Insert Into Net_Grupos_De_Usuarios (  IdEstado, IdSucursal, IdGrupo, NombreGrupo, Status, Actualizado ) 
	Select  IdEstado, IdSucursal, IdGrupo, NombreGrupo, Status, Actualizado
	from #tmp_Grupos P  
	Where Not Exists 
		( 
			Select * From Net_Grupos_De_Usuarios C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdSucursal = C.IdSucursal and P.IdGrupo = C.IdGrupo 
		) 


---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdSucursal, IdGrupo, IdPersonal, LoginUser, Status, Actualizado 
	Into #tmp_Grupos_Miembros   
	From Net_Grupos_Usuarios_Miembros P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdSucursal = @IdFarmaciaBase  
		and Exists 
		( 
			Select * 
			From Net_Usuarios U (NoLock) 
			Where U.IdEstado = @IdEstadoDestino and U.IdSucursal = @IdFarmaciaDestino and P.IdPersonal = U.IdPersonal 
		) 


	Insert Into Net_Grupos_Usuarios_Miembros (  IdEstado, IdSucursal, IdGrupo, IdPersonal, LoginUser, Status, Actualizado ) 
	Select  IdEstado, IdSucursal, IdGrupo, IdPersonal, LoginUser, Status, Actualizado 
	from #tmp_Grupos_Miembros P 
	Where Not Exists 
		( 
			Select * From Net_Grupos_Usuarios_Miembros C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdSucursal = C.IdSucursal and P.IdGrupo = C.IdGrupo and P.IdPersonal = C.IdPersonal 
		) 


---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdSucursal, IdGrupo, Arbol, Rama, Ruta, TipoRama, RutaCompleta, Status, Actualizado
	Into #tmp_Grupos_Privilegios    
	From Net_Privilegios_Grupo P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdSucursal = @IdFarmaciaBase  
	

	Insert Into Net_Privilegios_Grupo (  IdEstado, IdSucursal, IdGrupo, Arbol, Rama, Ruta, TipoRama, RutaCompleta, Status, Actualizado ) 
	Select  IdEstado, IdSucursal, IdGrupo, Arbol, Rama, Ruta, TipoRama, RutaCompleta, Status, Actualizado 
	from #tmp_Grupos_Privilegios P 
	Where Not Exists 
		( 
			Select * From Net_Privilegios_Grupo C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdSucursal = C.IdSucursal and P.IdGrupo = C.IdGrupo and P.Arbol = C.Arbol and P.Rama = C.Rama 
		)
---------------------------------------------------------------------------------------------------------------------------- 
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdFarmacia, IdPersonal, IdOperacion
	Into #tmp_Operaciones_Supervisadas    
	From Net_Permisos_Operaciones_Supervisadas P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
	

	Insert Into Net_Permisos_Operaciones_Supervisadas (  IdEstado, IdFarmacia, IdPersonal, IdOperacion ) 
	Select  IdEstado, IdFarmacia, IdPersonal, IdOperacion
	from #tmp_Operaciones_Supervisadas P 
	Where Not Exists 
		( 
			Select * From Net_Permisos_Operaciones_Supervisadas C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdPersonal = C.IdPersonal and P.IdOperacion = C.IdOperacion 
		) 


End 
Go--#SQL 
	
