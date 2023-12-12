---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_GetPermisos_Usuarios' and xType = 'P' )
   Drop proc sp_GetPermisos_Usuarios 
Go--#SQL 

Create Proc sp_GetPermisos_Usuarios 
(  
	@sIdEstado varchar(2) = '25', @sIdEntidad varchar(4) = '0001', 
	@sArbol varchar(4) = 'OCEN', @sUserLogin varchar(20) = 'JESUS.DIAZ' ) 
With Encryption 
As 
Begin 
	Set dateformat YMD 
	Set NoCount On 

Declare @iRaiz int, 
		@iRama int, 
		@iPermisos int  

	-- Actualizar todas las rutas de acceso de navegacion 
	Exec sp_Navegacion @sArbol, 0 

	Set @iPermisos = 0 
	Set @iRama = 0 
	Select @iRaiz = Rama From Net_Navegacion (NoLock) Where Arbol = @sArbol and Padre = -1

	If Exists ( Select Name From Sysobjects Where Name = 'tmpGetPermisos' and xType = 'U' )
       Drop Table tmpGetPermisos 

	Create Table tmpGetPermisos
	(
		Arbol varchar(4) Not Null,
		Rama int Not Null,
		Nombre varchar(255) Not Null,
		Padre int Not Null,
		FormaLoad varchar(100) Null,
		GrupoOpciones varchar(100),
		IdOrden int Not Null,  
		TipoRama varchar(20) null default '', 
		RutaCompleta varchar(50) null default '', 
		
		keyx int identity(1,1)
    )
    
	Insert Into tmpGetPermisos ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
	Select -- Distinct 
		N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta    
	From Net_Grupos_de_Usuarios G (nolock)
	Inner Join Net_Grupos_Usuarios_Miembros M (nolock) 
		On ( G.IdEstado = M.IdEstado and G.IdSucursal = M.IdSucursal and G.IdGrupo = M.IdGrupo and M.Status = 'A' ) 
	Inner Join Net_Usuarios U (NoLock) 
		On ( G.IdEstado = U.IdEstado and G.IdSucursal = U.IdSucursal and M.IdPersonal = U.IdPersonal and M.LoginUser = U.LoginUser and U.Status = 'A' )  
	Inner Join CatPersonal PR (NoLock) 
		On ( M.IdEstado = PR.IdEstado and M.IdSucursal = PR.IdFarmacia and M.IdPersonal = PR.IdPersonal and PR.Status = 'A' )  
	Inner Join Net_Privilegios_Grupo P (NoLock) On ( G.IdEstado = P.IdEstado and G.IdSucursal = P.IdSucursal and G.IdGrupo = P.IdGrupo and P.Status = 'A' ) 
	Inner Join Net_Navegacion N (NoLock) On ( P.Arbol = N.Arbol and P.RutaCompleta = N.RutaCompleta and N.Status = 'A' ) 
	Where G.IdEstado = @sIdEstado and G.IdSucursal = @sIdEntidad and N.Arbol = @sArbol and  M.LoginUser = @sUserLogin 
	Group by N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta 
	Order by len(N.RutaCompleta) 
	-- Select @@rowcount 


--- Verificar si el usuario tiene permisos 
	Select @iPermisos = count(*) From tmpGetPermisos (NoLock) 
	-- select * from tmpGetPermisos 

    Declare tmp cursor Local For Select Rama From tmpGetPermisos Order by IdOrden 
    OPEN tmp 
    FETCH NEXT FROM tmp INTO @iRama 
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN
			print cast(@iRama as varchar) 
			Exec sp_GetPermisos_UsuariosDetalles @sArbol, @iRama, @@NESTLEVEL   
   
            FETCH NEXT FROM tmp INTO @iRama	
        END 
    CLOSE tmp 
    DEALLOCATE tmp 

    --- Agregar la raiz del arbol 
	Insert Into tmpGetPermisos ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
	Select Distinct N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta    
	From Net_Navegacion N (nolock) 
	Where N.Arbol = @sArbol and @iRaiz = N.Rama

 
    --- Agregar todas las ramas padre 
	While ( (Select count(*) From tmpGetPermisos T 
				Where Not Exists ( Select top 1 * From tmpGetPermisos T2 where T2.Rama = T.Padre ) and T.Padre <> -1  ) <> 0 ) 
	Begin 

		-- Select @iVueltas, count(*) From tmpGetPermisos T 				Where Not Exists ( Select top 1 * From tmpGetPermisos T2 where T2.rama = T.Padre ) and T.Padre <> -1 and T.Rama <> 1

		Insert Into tmpGetPermisos ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
		Select Distinct N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta    
		From Net_Navegacion N (nolock) 
		Inner Join tmpGetPermisos T (NoLock) On ( N.Arbol = @sArbol and N.Rama = T.Padre ) 
		Where -- T.Arbol = @sArbol and 
		    -- N.Status = 'A' and 
			Not Exists ( Select * From tmpGetPermisos T2 (NoLock) Where T.Arbol = T2.Arbol and T.Padre = T2.Rama )
	End 

	-- 	Select * from tmpGetPermisos 

 
	-- Select * From tmpGetPermisos -- Where Arbol <> @sArbol 
	-- Quitar las ramas que no pertencen al grupo 	
	--Delete From tmpGetPermisos Where Arbol <> @sArbol 
	--Select * From tmpGetPermisos Where Arbol = @sArbol 

/*
	--- Agregar todas las ramas padre 
	Insert Into tmpGetPermisos ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
	Select Distinct N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta    
	From Net_Navegacion N (nolock) 
	Inner Join tmpGetPermisos T (NoLock) On ( N.Rama = T.Padre )
*/


    --- Regresar el arbol ordenado 
	If Exists ( Select Name From Sysobjects Where Name = 'tmpGetPermisosFinal' and xType = 'U' )
       Drop Table tmpGetPermisosFinal

	Select Top 1 
	    -- Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, identity(int, 1, 1) as Keyx  
	    P.Arbol, P.Rama, P.Nombre, P.Padre, P.FormaLoad, P.GrupoOpciones, P.IdOrden, P.TipoRama, P.RutaCompleta, identity(int, 1, 1) as Keyx    
	Into tmpGetPermisosFinal  
	From tmpGetPermisos P (NoLock) 
	-- Inner Join Net_Navegacion N (NoLock) On ( P.Arbol = N.Arbol and P.Rama = N.Rama and N.Status = 'A' ) 	
	Where P.Arbol = @sArbol  
	Group by P.Arbol, P.Rama, P.Nombre, P.Padre, P.FormaLoad, P.GrupoOpciones, P.IdOrden, P.TipoRama, P.RutaCompleta  
	
	-- Ordernar el arbol 
	Exec sp_GetPermisos_UsuariosDetallesOrdenar @sArbol, @iRaiz, @@NESTLEVEL 


	--- Determinar el tipo de rama 
	Update tmpGetPermisosFinal Set TipoRama = '1' Where Keyx = 1  -- Raiz  
	Update tmpGetPermisosFinal Set TipoRama = '2' Where Keyx > 1  -- Nodos
	

    Update T Set TipoRama = '3' -- Terminales 
	From tmpGetPermisosFinal T (NoLock) 
	Where Keyx > 1 and len(FormaLoad) > 0 and 
		Not Exists ( Select Padre From tmpGetPermisosFinal R (NoLock) Where T.Rama = R.Padre )


	Select 
		P.Arbol, P.Rama, P.Nombre, P.Padre, P.FormaLoad, P.GrupoOpciones, P.IdOrden, P.TipoRama, P.RutaCompleta, 
		cast('' as varchar(max)) as RutaCompleta_Permiso, P.Keyx 
	Into #tmpSalida 
	From tmpGetPermisosFinal P  
    Inner Join Net_Navegacion N (NoLock) On ( P.Arbol = N.Arbol and P.Rama = N.Rama and N.Status = 'A' ) 		
	Order by TipoRama, Keyx 







	-- Borrar las tablas 
	If Exists ( Select Name From Sysobjects Where Name = 'tmpGetPermisos' and xType = 'U' )
       Drop Table tmpGetPermisos 

	If Exists ( Select Name From Sysobjects Where Name = 'tmpGetPermisosFinal' and xType = 'U' )
       Drop Table tmpGetPermisosFinal 


--- Verificacion Final de Permisos 
	if @iPermisos = 0 
	   Delete From #tmpSalida 

	Select distinct Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, RutaCompleta_Permiso, Keyx 
	From #tmpSalida  
	Order by TipoRama, Keyx 


/*
	-- sp_GetPermisos_Usuarios 
	Select * 
	From tmpGetPermisos (NoLock) 
	Order by Rama, Padre, IdOrden 
*/
End 
Go--#SQL 

