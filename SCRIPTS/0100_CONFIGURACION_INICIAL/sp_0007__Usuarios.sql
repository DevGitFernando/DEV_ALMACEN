----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * from sysobjects (nolock) where Name = 'spp_CFG_OP__07__Usuarios' and XTYPE = 'P' ) 
	Drop Proc spp_CFG_OP__07__Usuarios 
Go--#SQL 

Create Proc spp_CFG_OP__07__Usuarios   
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
	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdSucursal, IdPersonal, LoginUser, '' As Password, Status, Actualizado
	Into #tmp_Usuarios  
	From Net_Usuarios P (NoLock) 
	Where IdEstado = @IdEstadoBase and IdSucursal = @IdFarmaciaBase  
	

	Insert Into Net_Usuarios (  IdEstado, IdSucursal, IdPersonal, LoginUser, Password, Status, Actualizado ) 
	Select  IdEstado, IdSucursal, IdPersonal, LoginUser, Password, Status, Actualizado 
	from #tmp_Usuarios P 
	Where Not Exists 
		( 
			Select * From Net_Usuarios C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdSucursal = C.IdSucursal and P.IdPersonal = C.IdPersonal 
		) 



------------------------------------------------------------------------------------------------------------------------------ 
--	Select @IdEstadoDestino as IdEstado, @IdFarmaciaDestino as IdSucursal, IdGrupo, NombreGrupo, Status, Actualizado
--	Into #tmp_Grupos  
--	From Net_Usuarios P (NoLock) 
--	Where IdEstado = @IdEstadoBase and IdFarmacia = @IdFarmaciaBase  
	

--	Insert Into Net_Usuarios (  IdEstado, IdSucursal, IdGrupo, NombreGrupo, Status, Actualizado ) 
--	Select  IdEstado, IdSucursal, IdGrupo, NombreGrupo, Status, Actualizado
--	from #tmp_Grupos P 
--	Where Not Exists 
--		( 
--			Select * From Net_Usuarios C (NoLock) 
--			Where P.IdEstado = C.IdEstado and P.IdSucursal = C.IdSucursal and P.IdGrupo = C.IdGrupo 
--		) 



End 
Go--#SQL 
	


--------------------------------------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------------------------------------- 	
--	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpGruposUsuarios%' and XTYPE = 'U' ) 
--	   Drop table tempdb..#tmpGruposUsuarios 
	   	
--	select distinct 
--		-- P.IdEstado, 
--		'07' as IdEstado, 
--		right('0000' + cast(cast(X.IdFarmacia as int) + 0 as varchar), 4) as IdFarmacia, 
--		P.IdGrupo, P.IdPersonal, P.LoginUser, 'A' as Status, 0 as Actualizado, GETDATE() as FechaUpdate
--	Into  #tmpGruposUsuarios  
--	From Net_Grupos_Usuarios_Miembros P 
--	Inner Join 
--	( 
--		Select F.IdEstado, F.IdFarmacia
--		From CatFarmacias F 
--		Where IdEstado = 16  and IdFarmacia >= 11
--	) as X 	On ( P.IdEstado = x.IdEstado  )
--	where P.IdEstado = 16  and P.IdSucursal >= 101  
--		-- And ( LoginUser like '%encarga%' or LoginUser like '%dispe%' ) 
	
--	--		select top 1 * from Net_Grupos_Usuarios_Miembros 
	
--	--	sp_listacolumnas Net_Grupos_Usuarios_Miembros 
	
--	Insert Into Net_Grupos_Usuarios_Miembros (  IdEstado, IdSucursal, IdGrupo, IdPersonal, LoginUser, Status, FechaUpdate, Actualizado ) 
--	Select  IdEstado, IdFarmacia, IdGrupo, IdPersonal, LoginUser, Status, FechaUpdate, Actualizado 
--	from #tmpGruposUsuarios P 
--	Where Not Exists 
--		( 
--			Select * From Net_Grupos_Usuarios_Miembros C (NoLock) 
--			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdSucursal and P.IdGrupo = C.IdGrupo and P.IdPersonal = C.IdPersonal 
--		) 					
		
		
		
--------------------------------------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------------------------------------- 	
--	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpPrivilegios%' and XTYPE = 'U' ) 
--	   Drop table tempdb..#tmpPrivilegios 
	   	
--	select distinct P.IdEstado, X.IdFarmacia, P.IdGrupo, P.Arbol, P.Rama, P.Ruta, P.TipoRama, P.RutaCompleta, 'A' as Status, 0 as Actualizado, GETDATE() as Fecha
--	Into  #tmpPrivilegios  
--	From Net_Privilegios_Grupo P 
--	Inner Join 
--	( 
--		Select F.IdEstado, F.IdFarmacia
--		From CatFarmacias F 
--		Where IdEstado = 16  and IdFarmacia >= 11
--	) as X 	On ( P.IdEstado = x.IdEstado  )
--	where P.IdEstado = 16  and P.IdSucursal >= 11 and P.Arbol = 'ISNF'
--		-- And IdGrupo in ( 2, 3 ) 
	
--	--		select top 1 * from Net_Privilegios_Grupo 
	
--	Insert Into Net_Privilegios_Grupo 
--	Select * 
--	from #tmpPrivilegios P 
--	Where Not Exists 
--		( 
--			Select * From Net_Privilegios_Grupo C (NoLock) 
--			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdSucursal and P.IdGrupo = C.IdGrupo 
--				and P.Arbol = C.Arbol and P.Rama = C.Rama
--		) 							
		