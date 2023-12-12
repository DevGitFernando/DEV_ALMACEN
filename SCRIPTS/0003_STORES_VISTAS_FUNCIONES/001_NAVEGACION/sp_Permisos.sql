If Exists ( Select * From Sysobjects (NoLock) Where Name = 'sp_Permisos' and xType = 'P' )
   Drop proc sp_Permisos 
Go--#SQL 

Create Proc sp_Permisos (  @sIdEstado varchar(2) = '25', @sIdEntidad varchar(4) = '0011', 
	@sArbol varchar(4) = 'PFAR', @sUserLogin varchar(20) = 'JOSEL' ) 
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

	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisos' and xType = 'U' )
       Drop Table tmpPermisos 

	Create Table tmpPermisos
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
    
	Insert Into tmpPermisos ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
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
	Select @iPermisos = count(*) From tmpPermisos (NoLock) 
	-- select * from tmpPermisos 

    Declare tmp cursor Local For Select Rama From tmpPermisos Order by IdOrden 
    OPEN tmp 
    FETCH NEXT FROM tmp INTO @iRama 
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN
			print cast(@iRama as varchar) 
			Exec sp_PermisosDetalles @sArbol, @iRama, @@NESTLEVEL   
   
            FETCH NEXT FROM tmp INTO @iRama	
        END 
    CLOSE tmp 
    DEALLOCATE tmp 

    --- Agregar la raiz del arbol 
	Insert Into tmpPermisos ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
	Select Distinct N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta    
	From Net_Navegacion N (nolock) 
	Where N.Arbol = @sArbol and @iRaiz = N.Rama

 
    --- Agregar todas las ramas padre 
	While ( (Select count(*) From tmpPermisos T 
				Where Not Exists ( Select top 1 * From tmpPermisos T2 where T2.Rama = T.Padre ) and T.Padre <> -1  ) <> 0 ) 
	Begin 

		-- Select @iVueltas, count(*) From tmpPermisos T 				Where Not Exists ( Select top 1 * From tmpPermisos T2 where T2.rama = T.Padre ) and T.Padre <> -1 and T.Rama <> 1

		Insert Into tmpPermisos ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
		Select Distinct N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta    
		From Net_Navegacion N (nolock) 
		Inner Join tmpPermisos T (NoLock) On ( N.Arbol = @sArbol and N.Rama = T.Padre ) 
		Where -- T.Arbol = @sArbol and 
		    -- N.Status = 'A' and 
			Not Exists ( Select * From tmpPermisos T2 (NoLock) Where T.Arbol = T2.Arbol and T.Padre = T2.Rama )
	End 

	-- 	Select * from tmpPermisos 

 
	-- Select * From tmpPermisos -- Where Arbol <> @sArbol 
	-- Quitar las ramas que no pertencen al grupo 	
	--Delete From tmpPermisos Where Arbol <> @sArbol 
	--Select * From tmpPermisos Where Arbol = @sArbol 

/*
	--- Agregar todas las ramas padre 
	Insert Into tmpPermisos ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
	Select Distinct N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta    
	From Net_Navegacion N (nolock) 
	Inner Join tmpPermisos T (NoLock) On ( N.Rama = T.Padre )
*/


    --- Regresar el arbol ordenado 
	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisosFinal' and xType = 'U' )
       Drop Table tmpPermisosFinal

	Select Top 1 
	    -- Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, identity(int, 1, 1) as Keyx  
	    P.Arbol, P.Rama, P.Nombre, P.Padre, P.FormaLoad, P.GrupoOpciones, P.IdOrden, P.TipoRama, P.RutaCompleta, identity(int, 1, 1) as Keyx    
	Into tmpPermisosFinal  
	From tmpPermisos P (NoLock) 
	-- Inner Join Net_Navegacion N (NoLock) On ( P.Arbol = N.Arbol and P.Rama = N.Rama and N.Status = 'A' ) 	
	Where P.Arbol = @sArbol  
	Group by P.Arbol, P.Rama, P.Nombre, P.Padre, P.FormaLoad, P.GrupoOpciones, P.IdOrden, P.TipoRama, P.RutaCompleta  
	
	-- Ordernar el arbol 
	Exec sp_PermisosDetallesOrdenar @sArbol, @iRaiz, @@NESTLEVEL 


	--- Determinar el tipo de rama 
	Update tmpPermisosFinal Set TipoRama = '1' Where Keyx = 1  -- Raiz  
	Update tmpPermisosFinal Set TipoRama = '2' Where Keyx > 1  -- Nodos
	

    Update T Set TipoRama = '3' -- Terminales 
	From tmpPermisosFinal T (NoLock) 
	Where Keyx > 1 and len(FormaLoad) > 0 and 
		Not Exists ( Select Padre From tmpPermisosFinal R (NoLock) Where T.Rama = R.Padre )


	Select P.Arbol, P.Rama, P.Nombre, P.Padre, P.FormaLoad, P.GrupoOpciones, P.IdOrden, P.TipoRama, P.RutaCompleta, P.Keyx 
	Into #tmpSalida 
	From tmpPermisosFinal P  
    Inner Join Net_Navegacion N (NoLock) On ( P.Arbol = N.Arbol and P.Rama = N.Rama and N.Status = 'A' ) 		
	Order by TipoRama, Keyx 


	-- Borrar las tablas 
	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisos' and xType = 'U' )
       Drop Table tmpPermisos 

	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisosFinal' and xType = 'U' )
       Drop Table tmpPermisosFinal 


--- Verificacion Final de Permisos 
	if @iPermisos = 0 
	   Delete From #tmpSalida 

	Select distinct Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, 'A' as Status, Keyx 
	From #tmpSalida  
	Order by TipoRama, Keyx 


/*
	-- sp_Permisos 
	Select * 
	From tmpPermisos (NoLock) 
	Order by Rama, Padre, IdOrden 
*/
End 
Go--#SQL 


