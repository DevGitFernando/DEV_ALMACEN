If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_PermisosDetalles' and xType = 'P' )
   Drop proc sp_PermisosDetalles 
GO 

Create Proc sp_PermisosDetalles ( @sArbol varchar(4) = 'VEN', @iRamaPadre int, @iNivel int = 1 output ) 
With Encryption 
As 
Begin 
	Set DateFormat YMD 

declare @iRama int 
	Set @iRama = 0 

	-- Buscar todos los hijos de la rama padre, primer nivel 
	Select * 
	Into #tmpHijosPadre 
	From Net_Navegacion (NoLock) 
	Where Arbol = @sArbol and Padre = @iRamaPadre and Status = 'A' 
	Order by IdOrden 
	
	-- Select * from #tmpHijosPadre  
	--Select @@rowcount 
	-- Select * From #tmpHijosPadre 

    Declare tmp cursor Local For Select Rama From #tmpHijosPadre Order by IdOrden 
    OPEN tmp 
    FETCH NEXT FROM tmp INTO @iRama 
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN
            
            If @@NESTLEVEL < 32
            Begin
			   Insert Into tmpPermisos ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta ) 
			   Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, RutaCompleta  
			   From #tmpHijosPadre 
			   Where Arbol = @sArbol and Padre = @iRamaPadre and Rama = @iRama 	
				
               Set @iNivel = @iNivel + 1
               Exec sp_PermisosDetalles @sArbol, @iRama, @iNivel out 
               Set @iNivel = @iNivel - 1
            End
   
            FETCH NEXT FROM tmp INTO @iRama	
        END 
    CLOSE tmp 
    DEALLOCATE tmp 


End 
Go 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_PermisosDetallesOrdenar' and xType = 'P' )
   Drop proc sp_PermisosDetallesOrdenar 
GO 

Create Proc sp_PermisosDetallesOrdenar ( @sArbol varchar(4) = 'COM', @iRamaPadre int, @iNivel int = 1 output ) 
with Encryption 
As 
Begin 
	Set DateFormat YMD 

declare @iRama int 
	Set @iRama = 0 

	-- Buscar todos los hijos de la rama padre, primer nivel 
	Select Distinct Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta  
	Into #tmpHijosPadre 
	From tmpPermisos (NoLock) 
	Where Arbol = @sArbol and Padre = @iRamaPadre 
	Order by IdOrden 

	-- Select * From #tmpHijosPadre 

    Declare tmp cursor Local For Select Rama From #tmpHijosPadre Order by IdOrden 
    OPEN tmp 
    FETCH NEXT FROM tmp INTO @iRama 
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN
            
            If @@NESTLEVEL < 32
            Begin
			   Insert Into tmpPermisosFinal ( Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta ) 
			   Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta  
			   From #tmpHijosPadre 
			   Where Arbol = @sArbol and Padre = @iRamaPadre and Rama = @iRama 	

               Set @iNivel = @iNivel + 1
               Exec sp_PermisosDetallesOrdenar @sArbol, @iRama, @iNivel out 
               Set @iNivel = @iNivel - 1
            End
   
            FETCH NEXT FROM tmp INTO @iRama	
        END 
    CLOSE tmp 
    DEALLOCATE tmp 


End 
Go 



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_NavegacionDetalles' and xType = 'P' )
   Drop proc sp_NavegacionDetalles 
GO 

Create Proc sp_NavegacionDetalles ( @sArbol varchar(4), @iRamaPadre int output, @sRutaCompleta varchar(20) out, @iNivel int = 1 output ) 
With Encryption 
As 
Begin 
	Set DateFormat YMD 

declare @iRama int 
	Set @iRama = 0 

	-- Buscar todos los hijos de la rama padre, primer nivel 
	Select *, space(20) as TipoRama 
	Into #tmpHijosPadre 
	From Net_Navegacion (NoLock) 
	Where Arbol = @sArbol and Padre = @iRamaPadre 
	Order by IdOrden 

	-- Select * From #tmpHijosPadre 

    Declare tmp cursor Local For Select Rama From #tmpHijosPadre Order by IdOrden 
    OPEN tmp 
    FETCH NEXT FROM tmp INTO @iRama 
    WHILE ( @@FETCH_STATUS = 0 ) 
        BEGIN
            
            If @@NESTLEVEL < 32
            Begin

			   Set @sRutaCompleta = replace(@sRutaCompleta, '||', '|') 
			   Set @sRutaCompleta = ltrim(rtrim(@sRutaCompleta)) + '|' + cast(@iRama as varchar)	

			   Insert Into tmpNavegacion 
			   Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, @sRutaCompleta, Status   
			   From #tmpHijosPadre 
			   Where Arbol = @sArbol and Padre = @iRamaPadre and Rama = @iRama 	

               Set @iNivel = @iNivel + 1
               Exec sp_NavegacionDetalles @sArbol, @iRama, @sRutaCompleta, @iNivel out 
			   Set @sRutaCompleta = ltrim(rtrim(@sRutaCompleta))
			   --Print @sRutaCompleta 	
			   Set @sRutaCompleta = left(@sRutaCompleta, len(@sRutaCompleta) - (len(cast(@iRama as varchar)) ))
			   Set @sRutaCompleta = replace(@sRutaCompleta, '||', '|')
			   --Print @sRutaCompleta 
               Set @iNivel = @iNivel - 1
            End
   
            FETCH NEXT FROM tmp INTO @iRama	
        END 
    CLOSE tmp 
    DEALLOCATE tmp 


End 
Go 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Navegacion' and xType = 'P' )
   Drop proc sp_Navegacion 
GO 

Create Proc sp_Navegacion ( @sArbol varchar(4), @iMostrarResultado int = 1 )
With Encryption 
As 
Begin 
	Set dateformat YMD 

Declare @iRaiz int, 
		@sRaiz varchar(3) 

	Select @iRaiz = Rama From Net_Navegacion (NoLock) Where Arbol = @sArbol and Padre = -1 
	Set @sRaiz = cast(@iRaiz as varchar) 
	-- Select @iRaiz as Raiz_01

	If Exists ( Select Name From Sysobjects Where Name = 'tmpNavegacion' and xType = 'U' )
       Drop Table tmpNavegacion


	Create Table tmpNavegacion
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
		Status varchar(1) null default 'C', 
		keyx int identity(1,1)
    )

	--- Insertar la rama del arbol 
	Insert Into tmpNavegacion 
	Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, '', @sRaiz, Status  
	From Net_Navegacion (NoLock) Where Arbol = @sArbol and Padre = -1 

	Exec sp_NavegacionDetalles @sArbol, @iRaiz, @sRaiz, @@NESTLEVEL 


	--- Actualizar la ruta completa 
	Update tmpNavegacion Set RutaCompleta = Replace(RutaCompleta, '||', '|')

	--- Determinar el tipo de rama 
	Update tmpNavegacion Set TipoRama = '1' Where Keyx = 1  -- Raiz  
	Update tmpNavegacion Set TipoRama = '2' Where Keyx > 1  -- Nodos
	

    Update T Set TipoRama = '3' -- Terminales 
	From tmpNavegacion T (NoLock) 
	Where Keyx > 1 and len(FormaLoad) > 0 and 
		Not Exists ( Select Padre From tmpNavegacion R (NoLock) Where T.Rama = R.Padre )


	-- Actualizar la ruta de navegacion 
	Update N Set RutaCompleta = T.RutaCompleta
	From Net_Navegacion N (nolock) 
	Inner Join tmpNavegacion T (nolock) On ( N.Arbol = T.Arbol and N.Rama = T.Rama )


	If @iMostrarResultado = 1 
	   Begin 
		  Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, Status, Keyx 
		  Into #tmpSalida 
		  From tmpNavegacion (NoLock) 
		  Order by TipoRama, Keyx 		
	   
		  Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, Status, Keyx 
		  From #tmpSalida 
		  Order by TipoRama, Keyx 
	   End 

	-- Borrar la tabla 
	If Exists ( Select Name From Sysobjects Where Name = 'tmpNavegacion' and xType = 'U' )
       Drop Table tmpNavegacion
       
	   
End 
Go


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'sp_Permisos' and xType = 'P' )
   Drop proc sp_Permisos 
GO 

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
	From Net_grupos_de_usuarios G (nolock)
	Inner Join Net_grupos_usuarios_miembros M (nolock) 
		On ( G.IdEstado = M.IdEstado and G.IdSucursal = M.IdSucursal and G.IdGrupo = M.IdGrupo and M.Status = 'A' ) 
	Inner Join CatPersonal PR (NoLock) On ( G.IdEstado = PR.IdEstado and G.IdSucursal = PR.IdFarmacia and PR.Status = 'A' )  
	Inner Join Net_Privilegios_grupo P (NoLock) On ( G.IdEstado = P.IdEstado and G.IdSucursal = P.IdSucursal and G.IdGrupo = P.IdGrupo and P.Status = 'A' ) 
	Inner Join Net_Navegacion N (NoLock) On ( P.Arbol = N.Arbol and P.RutaCompleta = N.RutaCompleta and N.Status = 'A' ) 
	Where G.IdEstado = @sIdEstado and G.IdSucursal = @sIdEntidad and N.Arbol = @sArbol and  M.LoginUser = @sUserLogin 
	Group by N.Arbol, N.Rama, N.Nombre, N.Padre, N.FormaLoad, N.GrupoOpciones, N.IdOrden, N.RutaCompleta 
	Order by len(N.RutaCompleta) 
	-- Select @@rowcount 


--- Verificar si el usuario tiene permisos 
	Select @iPermisos = count(*) From tmpPermisos (NoLock) 


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

	Select Top 1 Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, identity(int, 1, 1) as Keyx  
	Into tmpPermisosFinal  
	From tmpPermisos (NoLock) 
	Where Arbol = @sArbol  
	Group by Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta  
	
	-- Ordernar el arbol 
	Exec sp_PermisosDetallesOrdenar @sArbol, @iRaiz, @@NESTLEVEL 


	--- Determinar el tipo de rama 
	Update tmpPermisosFinal Set TipoRama = '1' Where Keyx = 1  -- Raiz  
	Update tmpPermisosFinal Set TipoRama = '2' Where Keyx > 1  -- Nodos
	

    Update T Set TipoRama = '3' -- Terminales 
	From tmpPermisosFinal T (NoLock) 
	Where Keyx > 1 and len(FormaLoad) > 0 and 
		Not Exists ( Select Padre From tmpPermisosFinal R (NoLock) Where T.Rama = R.Padre )


	Select Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, Keyx 
	Into #tmpSalida 
	From tmpPermisosFinal  
	Order by TipoRama, Keyx 


	-- Borrar las tablas 
	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisos' and xType = 'U' )
       Drop Table tmpPermisos 

	If Exists ( Select Name From Sysobjects Where Name = 'tmpPermisosFinal' and xType = 'U' )
       Drop Table tmpPermisosFinal 


--- Verificacion Final de Permisos 
	if @iPermisos = 0 
	   Delete From #tmpSalida 

	Select distinct Arbol, Rama, Nombre, Padre, FormaLoad, GrupoOpciones, IdOrden, TipoRama, RutaCompleta, Keyx 
	From #tmpSalida  
	Order by TipoRama, Keyx 


/*
	-- sp_Permisos 
	Select * 
	From tmpPermisos (NoLock) 
	Order by Rama, Padre, IdOrden 
*/
End 
Go

