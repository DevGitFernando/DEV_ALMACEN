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
	@iRama int, 
	@iPermisos int,   
	@sIdEntidad varchar(100), 
	@sUserLogin varchar(100) 


	-- Actualizar todas las rutas de acceso de navegacion 
	Exec sp_Navegacion @sArbol, 0  

	Set @iPermisos = 0 
	Set @iRama = 0 
	Set @sIdEntidad = '' 
	Set @sUserLogin = '' 
	Select @iRaiz = Rama From Net_Navegacion (NoLock) Where Arbol = @sArbol and Padre = -1

	-------------------------------------------- BASE DE PERMISOS  
	If Exists ( Select Name From Sysobjects Where Name = '#tmpPermisos_Exportar' and xType = 'U' )
       Drop Table #tmpPermisos_Exportar 

	Create Table #tmpPermisos_Exportar
	(
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdUsuario varchar(4) Not Null, 
		LoginUser varchar(50) Not Null, 
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
		and G.IdFarmacia = '0011' and  M.LoginUser = 'HGDOLORES' 
	Group by PR.IdEstado, PR.IdFarmacia, M.IdUsuario, M.LoginUser, N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta 
	Order by len(N.RutaCompleta)  
	-- Select @@rowcount 


	select cast('' as varchar(50)) as GUID, Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta, RutaCompleta_Descripcion  
	Into tmpPermisos_ExportarDetalles 
	From tmpPermisos_Exportar 
	Where 1 = 0 



	Insert Into #tmpPermisos_Exportar ( IdEstado, IdFarmacia, IdUsuario, LoginUser, Arbol ) 
	Select IdEstado, IdFarmacia, IdUsuario, LoginUser, Arbol 
	From tmpPermisos_Exportar  
	Group by IdEstado, IdFarmacia, IdUsuario, LoginUser, Arbol 

	select * from #tmpPermisos_Exportar 
	Select * From tmpPermisos_Exportar	


----		 sp_CTE_Exportar___Permisos   

	Set @sIdEntidad = '' 
	Set @sUserLogin = '' 


	-------------------------------- PERMISOS 
    Declare #tmp cursor Local For 
		Select IdFarmacia, LoginUser, Arbol 
		From #tmpPermisos_Exportar 
		Order by IdFarmacia, IdUsuario 
    OPEN #tmp 
    FETCH NEXT FROM #tmp INTO @sIdEntidad, @sUserLogin, @sArbol  
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN 
			Print @sIdEntidad + '  ' + @sArbol + '  ' + @sUserLogin 

			Set @sGUID = cast(newid() as varchar(50)) 

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
				Select Rama 
				From tmpPermisos_ExportarDetalles  
				Where GUID = @sGUID 
			OPEN #tmp_Permisos
			FETCH NEXT FROM #tmp_Permisos INTO @iRama 
			WHILE ( @@FETCH_STATUS = 0 ) 
				BEGIN 
					print cast(@iRama as varchar) 
					Exec sp_CTE_Exportar___PermisosDetalles @sGUID, @sArbol, @iRama, @@NESTLEVEL   
   
					FETCH NEXT FROM #tmp_Permisos INTO @iRama	
				END 
			CLOSE #tmp_Permisos 
			DEALLOCATE #tmp_Permisos  
 			-------------------------------- PERMISOS ESPECIFICOS 

			  
            FETCH NEXT FROM #tmp INTO @sIdEntidad, @sUserLogin, @sArbol 
        END 
    CLOSE #tmp 
    DEALLOCATE #tmp 
	-------------------------------- PERMISOS 


----		 sp_CTE_Exportar___Permisos   


	Select * 
	from tmpPermisos_ExportarDetalles 



/* 
--- Verificar si el usuario tiene permisos 
	Select @iPermisos = count(*) From tmpPermisos_Exportar (NoLock) 


    Declare tmp cursor Local For Select Rama From tmpPermisos_Exportar Order by IdOrden 
    OPEN tmp 
    FETCH NEXT FROM tmp INTO @iRama 
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN
			print cast(@iRama as varchar) 
			Exec sp_CTE_Exportar___PermisosDetalles @sArbol, @iRama, @@NESTLEVEL   
   
            FETCH NEXT FROM tmp INTO @iRama	
        END 
    CLOSE tmp 
    DEALLOCATE tmp 

    --- Agregar la raiz del arbol 
	Insert Into tmpPermisos_Exportar ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
	Select Distinct N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta    
	From Net_Navegacion N (nolock) 
	Where N.Arbol = @sArbol and @iRaiz = N.Rama

 
    --- Agregar todas las ramas padre 
	While ( (Select count(*) From tmpPermisos_Exportar T 
				Where Not Exists ( Select top 1 * From tmpPermisos_Exportar T2 where T2.Rama = T.Padre ) and T.Padre <> -1  ) <> 0 ) 
	Begin 

		-- Select @iVueltas, count(*) From tmpPermisos_Exportar T 				Where Not Exists ( Select top 1 * From tmpPermisos_Exportar T2 where T2.rama = T.Padre ) and T.Padre <> -1 and T.Rama <> 1

		Insert Into tmpPermisos_Exportar ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
		Select Distinct N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta    
		From Net_Navegacion N (nolock) 
		Inner Join tmpPermisos_Exportar T (NoLock) On ( N.Arbol = @sArbol and N.Rama = T.Padre ) 
		Where -- T.Arbol = @sArbol and 
		    -- N.Status = 'A' and 
			Not Exists ( Select * From tmpPermisos_Exportar T2 (NoLock) Where T.Arbol = T2.Arbol and T.Padre = T2.Rama )
	End 

	-- 	Select * from tmpPermisos_Exportar 



    --- Regresar el arbol ordenado 
	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisos_ExportarFinal' and xType = 'U' )
       Drop Table tmpPermisos_ExportarFinal

	Select Top 1 
	    -- Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, identity(int, 1, 1) as Keyx  
	    P.Arbol, P.Rama, P.Nombre, P.Padre, P.FormaLoad, P.GrupoOpciones, P.IdOrden, P.TipoRama, P.RutaCompleta, identity(int, 1, 1) as Keyx    
	Into tmpPermisos_ExportarFinal  
	From tmpPermisos_Exportar P (NoLock) 
	-- Inner Join Net_Navegacion N (NoLock) On ( P.Arbol = N.Arbol and P.Rama = N.Rama and N.Status = 'A' ) 	
	Where P.Arbol = @sArbol  
	Group by P.Arbol, P.Rama, P.Nombre, P.Padre, P.FormaLoad, P.GrupoOpciones, P.IdOrden, P.TipoRama, P.RutaCompleta  
	
	-- Ordernar el arbol 
	Exec sp_CTE_Exportar___PermisosDetallesOrdenar @sArbol, @iRaiz, @@NESTLEVEL 


	--- Determinar el tipo de rama 
	Update tmpPermisos_ExportarFinal Set TipoRama = '1' Where Keyx = 1  -- Raiz  
	Update tmpPermisos_ExportarFinal Set TipoRama = '2' Where Keyx > 1  -- Nodos
	

    Update T Set TipoRama = '3' -- Terminales 
	From tmpPermisos_ExportarFinal T (NoLock) 
	Where Keyx > 1 and len(FormaLoad) > 0 and 
		Not Exists ( Select Padre From tmpPermisos_ExportarFinal R (NoLock) Where T.Rama = R.Padre )


	Select P.Arbol, P.Rama, P.Nombre, P.Padre, P.FormaLoad, P.GrupoOpciones, P.IdOrden, P.TipoRama, P.RutaCompleta, P.Keyx 
	Into #tmpSalida 
	From tmpPermisos_ExportarFinal P  
    Inner Join Net_Navegacion N (NoLock) On ( P.Arbol = N.Arbol and P.Rama = N.Rama and N.Status = 'A' ) 		
	Order by TipoRama, Keyx 


	-- Borrar las tablas 
	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisos_Exportar' and xType = 'U' )
       Drop Table tmpPermisos_Exportar 

	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisos_ExportarFinal' and xType = 'U' )
       Drop Table tmpPermisos_ExportarFinal 


--- Verificacion Final de Permisos 
	if @iPermisos = 0 
	   Delete From #tmpSalida 

	Select distinct Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, Keyx 
	From #tmpSalida  
	Order by TipoRama, Keyx 


*/ 

End 
Go--#SQL 


