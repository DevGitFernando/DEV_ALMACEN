
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Usuarios' and xType = 'V' ) 
   Drop View vw_Usuarios 
Go--#SQL

Create View vw_Usuarios 
With Encryption 
As 


	----Select  P.IdEstado, F.Estado, P.IdFarmacia, F.Farmacia, P.IdPersonal, P.CURP, 
	----		(P.ApPaterno + ' ' + P.ApMaterno + ' ' + P.Nombre) as NombrePersonal, 
	----		IsNull(U.LoginUser, '') as LoginUser, IsNull(U.Password, '') as Password, 
	----		P.Status 
	----From CatPersonal P (noLock) 
	----Inner Join vw_Farmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia ) 
	----Left Join Net_Usuarios U (NoLock) On ( P.IdPersonal = U.IdPersonal ) 



	Select  P.IdPersonal, P.CURP, P.Nombre, P.ApPaterno, P.ApMaterno, 
			( IsNull(P.ApPaterno, '') + ' ' + IsNull(P.ApMaterno, '') + ' ' + P.Nombre ) as NombreCompleto,  
			( IsNull(P.ApPaterno, '') + ' ' + IsNull(P.ApMaterno, '') + ' ' + P.Nombre ) as NombrePersonal,  
			IsNull(US.LoginUser, '') as LoginUser, IsNull(US.Password, '') as Password,  
			P.FechaNacimiento, ( dbo.fg_CalcularEdad_Personal(Convert( varchar(10), P.FechaNacimiento, 120) ) )as Edad, 
			P.IdEstado_Domicilio, A.Nombre as Estado_Domicilio, P.IdMunicipio_Domicilio, M.Descripcion as Municipio_Domicilio, 
			P.IdColonia_Domicilio, N.Descripcion as Colonia_Domicilio, P.Calle_Domicilio, P.Numero_Domicilio, P.CodigoPostal_Domicilio, 
			P.IdEstado, F.Estado, P.IdFarmacia, F.Farmacia, 
			P.IdPuesto, U.Descripcion as Puesto, P.IdDepartamento, D.Descripcion as Departamento, 
			P.Sexo, ( Case When P.Sexo = 'M' Then 'MASCULINO' Else 'FEMENINO' End ) as SexoDesc,
			P.IdEscolaridad, E.Descripcion as Escolaridad, 
			P.IdTipoContrato, C.Descripcion as TipoContrato, 
			P.FechaIngreso, 
			( dbo.fg_CalcularAntiguedad_Personal(Convert( varchar(10), P.FechaIngreso, 120), Convert( varchar(10), GetDate(), 120) ) )as Antiguedad, 
			P.EMail, -- P.Password, 
			P.IdGrupoSanguineo, S.Descripcion as GrupoSanguineo, P.Alergias,
			P.NombreFotoPersonal, P.FotoPersonal, P.IdJefe, ( IsNull(CP.ApPaterno, '') + ' ' + IsNull(CP.ApMaterno, '') + ' ' + CP.Nombre ) as NombreJefe,
			P.Status, P.DiasDeAguinaldo 
	From CatPersonal P (noLock) 
	Inner Join vw_Farmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia ) 
	Inner Join CatPuestos U (NoLock) On ( P.IdPuesto = U.IdPuesto ) 
	Inner Join CatDepartamentos D(NoLock) On ( P.IdDepartamento = D.IdDepartamento ) 
	Inner Join CatEscolaridades E(NoLock) On ( P.IdEscolaridad = E.IdEscolaridad )  
	Inner Join CatTipoContrato C(NoLock) On ( P.IdTipoContrato = C.IdTipoContrato ) 
	Inner Join CatEstados A(NoLock) On ( P.IdEstado_Domicilio = A.IdEstado ) 
	Inner Join CatMunicipios M(NoLock) On ( P.IdEstado_Domicilio = M.IdEstado And P.IdMunicipio_Domicilio = M.IdMunicipio ) 
	Inner Join CatColonias N(NoLock) On ( P.IdEstado_Domicilio = N.IdEstado And P.IdMunicipio_Domicilio = N.IdMunicipio And P.IdColonia_Domicilio = N.IdColonia ) 
	Inner Join CatGruposSanguineos S(NoLock) On ( P.IdGrupoSanguineo = S.IdGrupoSanguineo )	
	Left Join CatPersonal CP (NoLock) On (CP.IdPersonal = P.IdJefe) 
	Left Join Net_Usuarios US (NoLock) On ( P.IdPersonal = US.IdPersonal ) 



Go--#SQL 	 	
	
--		sp_listacolumnas Net_Usuarios 	

--	select * from vw_Usuarios 
