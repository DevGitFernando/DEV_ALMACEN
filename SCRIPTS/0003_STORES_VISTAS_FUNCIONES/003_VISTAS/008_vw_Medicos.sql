------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Medicos' and xType = 'V' ) 
   Drop View vw_Medicos 
Go--#SQL 
 
Create View vw_Medicos 
With Encryption 
As 
	Select  P.IdEstado, F.Estado, P.IdFarmacia, F.Farmacia, 
			P.IdMedico, P.Nombre, P.ApPaterno, P.ApMaterno,
			( IsNull(P.Nombre, '') + ' ' + IsNull(P.ApPaterno, '') + ' ' + IsNull(P.ApMaterno, '') ) as NombreCompleto,
			P.NumCedula, P.IdEspecialidad, E.Descripcion as Especialidad, P.Status,
			-- IsNull(D.Direccion, Estado + ' ' + F.Municipio + ' COL: ' + F.Colonia + ' ' + F.Domicilio) As Direccion,
			ISNULL(D.Pais, '') AS PAIS,
			IsNull(G.IdEstado, '') as DireccionIdEstado, IsNull(G.Estado, '') as DireccionEstado,
			IsNull(G.IdMunicipio, '') as DireccionIdMunicipio, IsNull(G.Municipio, '') as DireccionMunicipio,
			IsNull(G.IdColonia, '') as DireccionIdColonia, IsNull(G.Colonia, '') as DireccionColonia,
			ISNULL(D.Calle, '') As Calle, Isnull(D.NumeroExterior, '') As NumeroExterior,
			ISNULL(D.NumeroInterior, '') As NumeroInterior, IsNull(D.CodigoPostal, '') As CodigoPostal,
			UPPER( IsNULL(Calle + ' Num Ext:' + NumeroExterior + ' Int:' + NumeroInterior + ' COL:' + G.Colonia + ' ' +
				   G.Municipio + ' ' + G.Estado + ', ' + G.Estado + ' CP:' + D.CodigoPostal, '')) as Direccion, 
			Isnull(D.Status, 'A') As StatusDireccion 	
	From CatMedicos P (noLock) 
	Inner Join vw_Farmacias F (NoLock) On ( P.IdEstado = F.IdEstado and P.IdFarmacia = F.IdFarmacia ) 
	Inner Join CatEspecialidades E (NoLock) On ( P.IdEspecialidad = E.IdEspecialidad )
	Left Join CatMedicos_Direccion D (NoLock) 
		On ( P.IdEstado = D.IdEstado and P.IdFarmacia = D.IdFarmacia And P.IdMedico = D.IdMedico And D.Status = 'A' )
	Left Join vw_Geograficos G (Nolock) 
		On ( D.IdEstado = G.IdEstado and D.IdMunicipio = G.IdMunicipio and D.IdColonia = G.IdColonia )  
	
Go--#SQL
 	
