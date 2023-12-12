---------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_CTE_Exportar___Permisos' and xType = 'P' )
   Drop proc sp_CTE_Exportar___Permisos  
Go--#SQL 

Create Proc sp_CTE_Exportar___Permisos 
(  
	@sIdEstado varchar(2) = '11', @sArbol varchar(10) = 'CTRW' 
) 
With Encryption 
As 
Begin 
	Set dateformat YMD 
	Set NoCount On 

Declare 
	@sGUID varchar(100), 
	@iRaiz int, 
	@iPadre int, 
	@iRama int, 
	@RutaCompleta varchar(max), 
	@iPermisos int,   
	@sIdEntidad varchar(100), 
	@sUserLogin varchar(100) 


	-- Actualizar todas las rutas de acceso de navegacion 
	Exec sp_Navegacion @sArbol, 0  

	Set @iPermisos = 0 
	Set @iPadre = 0 
	Set @iRama = 0 
	Set @RutaCompleta = '' 
	Set @sIdEntidad = '' 
	Set @sUserLogin = '' 
	Select @iRaiz = Rama From Net_Navegacion (NoLock) Where Arbol = @sArbol and Padre = -1

	-------------------------------------------- BASE DE PERMISOS  
	If Exists ( Select Name From Sysobjects Where Name = '#tmpPermisos_Exportar' and xType = 'U' )
       Drop Table #tmpPermisos_Exportar 

	Create Table #tmpPermisos_Exportar
	(
		GUID varchar(100) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		Farmacia varchar(300) Not Null Default '',  
		IdUsuario varchar(4) Not Null, 
		LoginUser varchar(50) Not Null, 
		NombreUsuario varchar(200) Not Null, 
		Arbol varchar(4) Not Null  
	) 


	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisos_ExportarDetalles' and xType = 'U' )
       Drop Table tmpPermisos_ExportarDetalles   


	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisos_Exportar' and xType = 'U' )
       Drop Table tmpPermisos_Exportar 

	Create Table tmpPermisos_Exportar
	(
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdUsuario varchar(4) Not Null, 
		LoginUser varchar(50) Not Null, 

		Arbol varchar(4) Not Null,
		Rama int Not Null,
		Nombre varchar(255) Not Null,
		Padre int Not Null,
		FormaLoad varchar(100) Null,
		GrupoOpciones varchar(100),
		IdOrden int Not Null,  
		TipoRama varchar(20) null default '', 
		RutaCompleta varchar(50) null default '', 
		RutaCompleta_Descripcion varchar(max) null default '', 
		keyx int identity(1,1)
    ) 
    
	Insert Into tmpPermisos_Exportar ( IdEstado, IdFarmacia, IdUsuario, LoginUser, Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
	Select -- Distinct 
		PR.IdEstado, PR.IdFarmacia, M.IdUsuario, M.LoginUser, N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta    
	From Net_CTE_Grupos_De_Usuarios G (nolock)
	Inner Join Net_CTE_Grupos_Usuarios_Miembros M (nolock) 
		On ( G.IdEstado = M.IdEstado and G.IdFarmacia = M.IdFarmacia and G.IdGrupo = M.IdGrupo and M.Status = 'A' ) 
	Inner Join Net_Regional_Usuarios PR (NoLock) On ( G.IdEstado = PR.IdEstado and G.IdFarmacia = PR.IdFarmacia and PR.Status = 'A' )  
	Inner Join Net_CTE_Privilegios_Grupo P (NoLock) On ( G.IdEstado = P.IdEstado and G.IdFarmacia = P.IdFarmacia and G.IdGrupo = P.IdGrupo and P.Status = 'A' ) 
	Inner Join Net_Navegacion N (NoLock) On ( P.Arbol = N.Arbol and P.RutaCompleta = N.RutaCompleta and N.Status = 'A' ) 
	Where G.IdEstado = @sIdEstado and G.Status = 'A' and N.Arbol = @sArbol  
		-- and G.IdFarmacia = '0011' and  M.LoginUser = 'HGDOLORES' 
	Group by PR.IdEstado, PR.IdFarmacia, M.IdUsuario, M.LoginUser, N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta 
	Order by len(N.RutaCompleta)  
	-- Select @@rowcount 


	select cast('' as varchar(50)) as GUID, Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta, RutaCompleta_Descripcion  
	Into tmpPermisos_ExportarDetalles 
	From tmpPermisos_Exportar 
	Where 1 = 0 



	Insert Into #tmpPermisos_Exportar ( GUID, IdEstado, IdFarmacia, IdUsuario, LoginUser, NombreUsuario, Arbol ) 
	Select cast(newid() as varchar(50)) as GUID, IdEstado, IdFarmacia, IdUsuario, LoginUser, '' as NombreUsuario, Arbol 
	From tmpPermisos_Exportar  
	Group by IdEstado, IdFarmacia, IdUsuario, LoginUser, Arbol 


	Update G Set NombreUsuario = PR.Nombre 
	From #tmpPermisos_Exportar G 
	Inner Join Net_Regional_Usuarios PR  (NoLock) On( G.IdEstado = PR.IdEstado and G.IdFarmacia = PR.IdFarmacia and PR.Status = 'A' )

	Update G Set Farmacia = F.Farmacia 
	From #tmpPermisos_Exportar G 
	Inner Join vw_Farmacias F (NoLock) On ( G.IdEstado = F.IdEstado and G.IdFarmacia = F.IdFarmacia ) 


	--select * from #tmpPermisos_Exportar 
	--Select * From tmpPermisos_Exportar	


----		 sp_CTE_Exportar___Permisos   

	Set @sIdEntidad = '' 
	Set @sUserLogin = '' 


	-------------------------------- PERMISOS 
    Declare #tmp cursor Local For 
		Select GUID, IdFarmacia, LoginUser, Arbol 
		From #tmpPermisos_Exportar 
		Order by IdFarmacia, IdUsuario 
    OPEN #tmp 
    FETCH NEXT FROM #tmp INTO @sGUID, @sIdEntidad, @sUserLogin, @sArbol  
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN 
			Print @sIdEntidad + '  ' + @sArbol + '  ' + @sUserLogin 

			-- Set @sGUID = cast(newid() as varchar(50)) 

			Insert Into tmpPermisos_ExportarDetalles ( GUID, Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta, RutaCompleta_Descripcion  ) 
			Select -- Distinct 
				@sGUID, 
				N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta, '' as RutaCompleta_Descripcion      
			From Net_CTE_Grupos_De_Usuarios G (nolock) 
			Inner Join Net_CTE_Grupos_Usuarios_Miembros M (nolock) 
				On ( G.IdEstado = M.IdEstado and G.IdFarmacia = M.IdFarmacia and G.IdGrupo = M.IdGrupo and M.Status = 'A' ) 
			Inner Join Net_Regional_Usuarios PR (NoLock) On ( G.IdEstado = PR.IdEstado and G.IdFarmacia = PR.IdFarmacia and PR.Status = 'A' )  
			Inner Join Net_CTE_Privilegios_Grupo P (NoLock) On ( G.IdEstado = P.IdEstado and G.IdFarmacia = P.IdFarmacia and G.IdGrupo = P.IdGrupo and P.Status = 'A' ) 
			Inner Join Net_Navegacion N (NoLock) On ( P.Arbol = N.Arbol and P.RutaCompleta = N.RutaCompleta and N.Status = 'A' ) 
			Where G.IdEstado = @sIdEstado and G.Status = 'A' and N.Arbol = @sArbol and G.IdFarmacia = @sIdEntidad and  M.LoginUser = @sUserLogin 
			Group by PR.IdEstado, PR.IdFarmacia, M.IdUsuario, M.LoginUser, N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta 
			Order by len(N.RutaCompleta)  



			-------------------------------- PERMISOS ESPECIFICOS 
			Declare #tmp_Permisos cursor Local For 
				Select Rama, Nombre 
				From tmpPermisos_ExportarDetalles  
				Where GUID = @sGUID 
			OPEN #tmp_Permisos
			FETCH NEXT FROM #tmp_Permisos INTO @iRama, @RutaCompleta  
			WHILE ( @@FETCH_STATUS = 0 ) 
				BEGIN 
					--print cast(@iRama as varchar) 
					Exec sp_CTE_Exportar___PermisosDetalles @sGUID, @sArbol, @iRama, @RutaCompleta, @@NESTLEVEL   
   
					FETCH NEXT FROM #tmp_Permisos INTO @iRama, @RutaCompleta 	
				END 
			CLOSE #tmp_Permisos 
			DEALLOCATE #tmp_Permisos  
 			-------------------------------- PERMISOS ESPECIFICOS 

			--- Agregar la raiz del arbol 
			Insert Into tmpPermisos_ExportarDetalles ( GUID, Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta, RutaCompleta_Descripcion ) 
			Select Distinct @sGUID, N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta, '' as RutaCompleta_Descripcion     
			From Net_Navegacion N (nolock) 
			Where N.Arbol = @sArbol and @iRaiz = N.Rama


			--- Agregar todas las ramas padre 
			While ( (Select count(*) From tmpPermisos_ExportarDetalles T 
						Where GUID = @sGUID  and Not Exists ( Select top 1 * From tmpPermisos_ExportarDetalles T2 where T.GUID = T2.GUID and T2.Rama = T.Padre ) and T.Padre <> -1  ) <> 0 ) 
			Begin 

				Insert Into tmpPermisos_ExportarDetalles ( GUID, Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta, RutaCompleta_Descripcion ) 
				Select Distinct @sGUID, N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta, '' as RutaCompleta_Descripcion     
				From Net_Navegacion N (nolock) 
				Inner Join tmpPermisos_ExportarDetalles T (NoLock) On ( N.Arbol = @sArbol and N.Rama = T.Padre and T.GUID = @sGUID ) 
				Where -- T.Arbol = @sArbol and 
					-- N.Status = 'A' and 
					Not Exists ( Select * From tmpPermisos_ExportarDetalles T2 (NoLock) Where T.Arbol = T2.Arbol and T.Padre = T2.Rama )
			End 

			
			
			
			  
            FETCH NEXT FROM #tmp INTO @sGUID, @sIdEntidad, @sUserLogin, @sArbol 
        END 
    CLOSE #tmp 
    DEALLOCATE #tmp 
	-------------------------------- PERMISOS 



	-------------------------------- NOMBRE DE PERMISOS ESPECIFICOS 
	Update P Set RutaCompleta_Descripcion = Nombre 
	From tmpPermisos_ExportarDetalles P 
	Where Padre = -1 

----		 sp_CTE_Exportar___Permisos   

    Declare #tmp cursor Local For 
		Select GUID -- , IdFarmacia, LoginUser, Arbol 
		From #tmpPermisos_Exportar 
		Order by IdFarmacia, IdUsuario 
    OPEN #tmp 
    FETCH NEXT FROM #tmp INTO @sGUID 
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN 

			Declare #tmp_Permisos cursor Local For 
				Select Padre, Rama   
				From tmpPermisos_ExportarDetalles 
				Where GUID = @sGUID and Padre <> -1  
				order by GUID, Padre, Rama 
			OPEN #tmp_Permisos
			FETCH NEXT FROM #tmp_Permisos INTO @iPadre, @iRama 
			WHILE ( @@FETCH_STATUS = 0 ) 
				BEGIN 
					----Select *, 						( Select Top 1 P.RutaCompleta_Descripcion From tmpPermisos_ExportarDetalles P Where Padre = @iPadre )  
					----From tmpPermisos_ExportarDetalles D 
					----Where Rama = @iRama 

----		 sp_CTE_Exportar___Permisos   

					--Print cast(@iPadre as varchar(20)) + ' ' + cast(@iRama as varchar(20))  
			
					--Select * 
					--From tmpPermisos_ExportarDetalles D 
					--Where Rama = @iPadre 

					--Select * 
					--From tmpPermisos_ExportarDetalles D 
					--Where Rama = @iRama 

					Update D Set RutaCompleta_Descripcion = D.RutaCompleta_Descripcion + 
						IsNull(( Select Top 1 P.RutaCompleta_Descripcion From tmpPermisos_ExportarDetalles P Where Rama = @iPadre ), '0') + '\' + Nombre 
					From tmpPermisos_ExportarDetalles D 
					Where Rama = @iRama 

					FETCH NEXT FROM #tmp_Permisos INTO @iPadre, @iRama  	
				END 
			CLOSE #tmp_Permisos 
			DEALLOCATE #tmp_Permisos  


            FETCH NEXT FROM #tmp INTO @sGUID 
        END 
    CLOSE #tmp 
    DEALLOCATE #tmp 
	-------------------------------- NOMBRE DE PERMISOS ESPECIFICOS 

----		 sp_CTE_Exportar___Permisos   


------------------------------------ SALIDA FINAL 
	Select P.GUID, U.Farmacia, U.LoginUser, U.NombreUsuario, P.Arbol, P.Nombre, P.RutaCompleta_Descripcion   
	from tmpPermisos_ExportarDetalles P 
	inner join #tmpPermisos_Exportar U (NoLock) On ( P.GUID = U.GUID ) 
	order by P.GUID, P.Padre, P.Rama 



End 
Go--#SQL 


