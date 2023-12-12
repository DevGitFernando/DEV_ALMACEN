If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Rpt_Permisos_Personal' and xType = 'P' )
   Drop proc spp_Rpt_Permisos_Personal
Go--#SQL 

Create Proc spp_Rpt_Permisos_Personal (  @IdPersonal Varchar(8) = '00000007', @Nombre Varchar(200) = 'Manuel',
@sWhere Varchar(Max) =  '(IdEstado = 11 And IdSucursal = 0003)' ) 
With Encryption 
As 
Begin 
	Set dateformat YMD 
	Set NoCount On 

Declare @sArbol varchar(4),
		@sUserLogin varchar(20),
		@sSql varchar(Max),
		@IdEstado varchar(2),
		@IdFarmacia varchar(4)
	
		
	--	Select IdEstado, IdFarmacia, NombreFarmacia
	--	Into #Farmacias
	--	From CatFarmacias
	--	Where Status = 'A'
		

	Create Table #tmpPermisos
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
		keyx int
    )

	Create Table #tmpPermisosFarmacia
	(
		IdEstado Varchar(2) Not Null,
		IdFarmacia varchar(4) Not Null,
		LoginUser varchar(20) Not Null,
		Arbol varchar(4) Not Null,
		Rama int Not Null,
		Nombre varchar(255) Not Null,
		Padre int Not Null,
		FormaLoad varchar(100) Null,
		GrupoOpciones varchar(100),
		IdOrden int Not Null,  
		TipoRama varchar(20) null default '', 
		RutaCompleta varchar(50) null default '',
		keyx int
    )
    
    Create Table #Usuarios 
	(
		IdEstado varchar(2) Not Null, 
		IdSucursal varchar(4) Not Null, 
		LoginUser varchar(50) Not Null
	)
 
 	Set @sSql = 'Select IdEstado, IdSucursal, LoginUser From Net_usuarios Where '  + @sWhere
 	
 	Insert Into #Usuarios
	Exec(@sSql)
	print (@sSql)
	
    Declare tmp
    cursor Local For
			--Select R.IdEstado, R.IdFarmacia, LoginUser
			--From  Net_Usuario_De_Personal R (NoLock)
			--Inner Join #Farmacias F (NoLock) On (R.IdEstado = F.IdEstado And R.IdFarmacia = F.IdFarmacia)
			--Inner Join CatPersonal P (NoLock) On (R.IdEstado = P.IdEstado And R.IdFarmacia = P.IdFarmacia And R.IdUsuario = P.IdPersonal)
			--Inner Join Net_Usuarios U (NoLock) On (R.IdEstado = U.IdEstado And R.IdFarmacia = U.IdSucursal And R.IdUsuario = U.IdPersonal)
			--Where R.IdPersonal = @IdPersonal And R.Status = A And U.Status = A And P.status = 'A
	Select * From #Usuarios
	
	OPEN tmp
    FETCH NEXT FROM tmp INTO @IdEstado, @IdFarmacia, @sUserLogin
    WHILE ( @@FETCH_STATUS = 0 )
        BEGIN
    		    Declare tmp2
				cursor Local For
					Select Arbol From Net_Arboles
				OPEN tmp2
				FETCH NEXT FROM tmp2 INTO @sArbol
				WHILE ( @@FETCH_STATUS = 0 )
					BEGIN
						Insert Into #tmpPermisos
						Exec sp_Permisos @IdEstado, @IdFarmacia, @sArbol, @sUserLogin
						Insert Into #tmpPermisosFarmacia
						Select @IdEstado, @IdFarmacia, @sUserLogin, * From #tmpPermisos
						Delete #tmpPermisos
						FETCH NEXT FROM tmp2 INTO  @sArbol
					END
				CLOSE tmp2
				DEALLOCATE tmp2
				
			FETCH NEXT FROM tmp INTO  @IdEstado, @IdFarmacia, @sUserLogin
			
       END  
    CLOSE tmp  
    DEALLOCATE tmp
	
	
	Select @IdPersonal As IdPersonal, @Nombre As NombreCompleto, T.IdEstado, F.Estado, T.IdFarmacia, F.Farmacia, T.LoginUser, T.Arbol, A.Nombre As NombreArbol, T.Nombre
	From #tmpPermisosFarmacia T
	--Inner Join vw_PersonalHuellas H (NoLock) On (H.IdPersonal = @IdPersonal)
	Inner Join Net_Arboles A (NoLock) On (T.Arbol = A.Arbol)
	Inner Join vw_Farmacias F (NoLock) On (T.IdEstado = F.IdEstado And T.IdFarmacia = F.IdFarmacia)
	Where TipoRama = '3'
	Order By T.IdEstado, T.IdFarmacia, T.LoginUser, T.Arbol, T.Padre, T.IdOrden	
	
End 
Go--#SQL 