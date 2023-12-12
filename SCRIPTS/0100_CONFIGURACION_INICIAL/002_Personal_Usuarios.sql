

--------------------------------------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------------------------------------- 
--	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpPersonal%' and XTYPE = 'U' ) 
--	   Drop table tempdb..#tmpPersonal 

--	select distinct P.IdEstado, 
--		right('0000' + cast(cast(X.IdFarmacia as int) + 0 as varchar), 4) as IdFarmacia, 
--		P.IdPersonal, P.Nombre, P.ApPaterno, P.ApMaterno, GETDATE() as Fecha, 'A' as Status, 0 as Actualizado, getdate() as FechaRegistro  
--	Into #tmpPersonal 
--	from CatPersonal P 
--	Inner Join 
--	( 
--		Select F.IdEstado, F.IdFarmacia
--		From CatFarmacias F 
--		Where IdEstado = 9  and IdFarmacia >= 11 
--	) as X 	On ( P.IdEstado = x.IdEstado  )
--	where P.IdEstado = 7 and P.IdFarmacia >= 11
--		-- And ( Nombre like '%encarga%' or Nombre like '%dispe%' ) 
	
-----		select * from #tmpPersonal 	
	
	
----	sp_listacolumnas CatPersonal 
	
--	Insert Into CatPersonal ( IdEstado, IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Actualizado, Status ) 
--	Select IdEstado, IdFarmacia, IdPersonal, Nombre, ApPaterno, ApMaterno, FechaRegistro, Actualizado, Status 
--	from #tmpPersonal P 
--	Where Not Exists 
--		( 
--			Select * From CatPersonal C (NoLock) Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdFarmacia and P.IdPersonal = C.IdPersonal 
--		) 
--	order by IdFarmacia, IdPersonal 

------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 	
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpGrupos%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpGrupos 
	   	
	select distinct 
		-- P.IdEstado, 
		'31' as IdEstado, 
		right('0000' + cast(cast(X.IdFarmacia as int) + 0 as varchar), 4) as IdFarmacia, 
		P.IdGrupo, P.NombreGrupo, 'A' as Status, 0 as Actualizado, GETDATE() as FechaUpdate
	Into  #tmpGrupos  
	From Net_Grupos_De_Usuarios P 
	Inner Join 
	( 
		Select F.IdEstado, F.IdFarmacia
		From CatFarmacias F 
		Where IdEstado = 9  and IdFarmacia = 11 
	) as X 	On ( P.IdEstado = x.IdEstado  )
	where P.IdEstado = 9  and P.IdSucursal = 11   
		-- and nombreGrupo not like '%super%' 
	
--	sp_listacolumnas Net_Grupos_De_Usuarios 	
	
---		select * from #tmpGrupos 

	Insert Into Net_Grupos_De_Usuarios ( IdEstado, IdSucursal, IdGrupo, NombreGrupo, Status, FechaUpdate, Actualizado ) 
	Select distinct IdEstado, IdFarmacia as IdSucursal, IdGrupo, NombreGrupo, Status, FechaUpdate, Actualizado
	from #tmpGrupos P 
	Where Not Exists 
		( 
			Select * From Net_Grupos_De_Usuarios C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdSucursal and P.IdGrupo = C.IdGrupo 
		) 	
		
		
------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 	
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpUsuarios%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpUsuarios 
	   	
	select distinct 
		-- P.IdEstado, 
		'07' as IdEstado, 
		right('0000' + cast(cast(X.IdFarmacia as int) + 0 as varchar), 4) as IdFarmacia, 
		P.IdPersonal, P.LoginUser, '' as Password, 'A' as Status, 0 as Actualizado, GETDATE() as FechaUpdate
	Into  #tmpUsuarios  
	From Net_Usuarios P 
	Inner Join 
	( 
		Select F.IdEstado, F.IdFarmacia
		From CatFarmacias F 
		Where IdEstado = 16  and IdFarmacia >= 11 
	) as X 	On ( P.IdEstado = x.IdEstado  )
	where P.IdEstado = 16  and P.IdSucursal >= 101  
		-- And ( LoginUser like '%encarga%' or LoginUser like '%dispe%' ) 
	
	--	select top 1 * from Net_Usuarios 
	
--	sp_listacolumnas 	Net_Usuarios 
	
	Insert Into Net_Usuarios ( IdEstado, IdSucursal, IdPersonal, LoginUser, Password, Status, FechaUpdate, Actualizado ) 
	Select IdEstado, IdFarmacia, IdPersonal, LoginUser, Password, Status, FechaUpdate, Actualizado 
	from #tmpUsuarios P 
	Where Not Exists 
		( 
			Select * From Net_Usuarios C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdSucursal and P.IdPersonal = C.IdPersonal 
		) 			
		
	
------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 	
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpGruposUsuarios%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpGruposUsuarios 
	   	
	select distinct 
		-- P.IdEstado, 
		'07' as IdEstado, 
		right('0000' + cast(cast(X.IdFarmacia as int) + 0 as varchar), 4) as IdFarmacia, 
		P.IdGrupo, P.IdPersonal, P.LoginUser, 'A' as Status, 0 as Actualizado, GETDATE() as FechaUpdate
	Into  #tmpGruposUsuarios  
	From Net_Grupos_Usuarios_Miembros P 
	Inner Join 
	( 
		Select F.IdEstado, F.IdFarmacia
		From CatFarmacias F 
		Where IdEstado = 16  and IdFarmacia >= 11
	) as X 	On ( P.IdEstado = x.IdEstado  )
	where P.IdEstado = 16  and P.IdSucursal >= 101  
		-- And ( LoginUser like '%encarga%' or LoginUser like '%dispe%' ) 
	
	--		select top 1 * from Net_Grupos_Usuarios_Miembros 
	
	--	sp_listacolumnas Net_Grupos_Usuarios_Miembros 
	
	Insert Into Net_Grupos_Usuarios_Miembros (  IdEstado, IdSucursal, IdGrupo, IdPersonal, LoginUser, Status, FechaUpdate, Actualizado ) 
	Select  IdEstado, IdFarmacia, IdGrupo, IdPersonal, LoginUser, Status, FechaUpdate, Actualizado 
	from #tmpGruposUsuarios P 
	Where Not Exists 
		( 
			Select * From Net_Grupos_Usuarios_Miembros C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdSucursal and P.IdGrupo = C.IdGrupo and P.IdPersonal = C.IdPersonal 
		) 					
		
		
		
------------------------------------------------------------------------------------------------------------------------------- 
------------------------------------------------------------------------------------------------------------------------------- 	
	If exists ( select * from tempdb..sysobjects (nolock) where name like '#tmpPrivilegios%' and XTYPE = 'U' ) 
	   Drop table tempdb..#tmpPrivilegios 
	   	
	select distinct P.IdEstado, X.IdFarmacia, P.IdGrupo, P.Arbol, P.Rama, P.Ruta, P.TipoRama, P.RutaCompleta, 'A' as Status, 0 as Actualizado, GETDATE() as Fecha
	Into  #tmpPrivilegios  
	From Net_Privilegios_Grupo P 
	Inner Join 
	( 
		Select F.IdEstado, F.IdFarmacia
		From CatFarmacias F 
		Where IdEstado = 16  and IdFarmacia >= 11
	) as X 	On ( P.IdEstado = x.IdEstado  )
	where P.IdEstado = 16  and P.IdSucursal >= 11 and P.Arbol = 'ISNF'
		-- And IdGrupo in ( 2, 3 ) 
	
	--		select top 1 * from Net_Privilegios_Grupo 
	
	Insert Into Net_Privilegios_Grupo 
	Select * 
	from #tmpPrivilegios P 
	Where Not Exists 
		( 
			Select * From Net_Privilegios_Grupo C (NoLock) 
			Where P.IdEstado = C.IdEstado and P.IdFarmacia = C.IdSucursal and P.IdGrupo = C.IdGrupo 
				and P.Arbol = C.Arbol and P.Rama = C.Rama
		) 							
		