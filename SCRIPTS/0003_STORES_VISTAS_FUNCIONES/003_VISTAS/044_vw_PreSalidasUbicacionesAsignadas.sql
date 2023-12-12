If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_PreSalidasUbicacionesAsignadas' and xType = 'V' ) 
	Drop View vw_PreSalidasUbicacionesAsignadas 
Go--#SQL 
 	
Create View vw_PreSalidasUbicacionesAsignadas 
With Encryption 
As 
		Select P.IdEmpresa, E.Nombre As Empresa, P.IdEstado, FS.Estado, 
			P.IdFarmacia, FS.Farmacia, P.IdSubFarmacia, 
			dbo.fg_ObtenerNombreSubFarmacia( P.IdEstado ,P.IdFarmacia, P.IdSubFarmacia ) as SubFarmacia, 
			P.FolioPreSalida As Folio, 
			P.IdClaveSSA, VP.ClaveSSA, 
			VP.DescripcionSal, 
			P.IdPasillo, U.DescripcionPasillo as NombrePasillo, 
			P.IdEstante, U.DescripcionEstante as NombreEstante, 
			P.IdEntrepaño, U.DescripcionEntrepaño as NombreEntrepaño,
			P.IdProducto, VP.DescripcionProducto, P.CodigoEAN, P.ClaveLote, VP.FechaCaducidad, VP.MesesParaCaducar,
			P.Cantidad, D.CantidadRequerida, PE.FechaPreSalida, PE.FechaRegistro, GetDate() as FechaImpresion, 
			PE.IdPersonal, ( CP.Nombre + ' ' + IsNull(CP.ApPaterno, '') + ' ' + IsNull(CP.ApMaterno, '') ) as NombreCompleto, 
			PE.Observaciones, 
			Case When PE.Status = 'A' Then 'ACTIVO' 
				When PE.Status = 'C' Then 'CANCELADO'  
				When PE.Status = 'P' Then 'PROCESADO' 
			Else 'TERMINADO' End As StatusPresalida,
		P.Status	
		From PreSalidasPedidosDet_Lotes_Ubicaciones P (Nolock)
		Inner Join PreSalidasPedidosEnc PE (Nolock)
			On ( P.IdEmpresa = PE.IdEmpresa And P.IdEstado = PE.IdEstado And P.IdFarmacia = PE.IdFarmacia And P.IdSubFarmacia = PE.IdSubFarmacia 
				And P.FolioPreSalida = PE.FolioPreSalida )
		Inner Join PreSalidasPedidosDet D (Nolock)
			On ( P.IdEmpresa = D.IdEmpresa And P.IdEstado = D.IdEstado And P.IdFarmacia = D.IdFarmacia 
				And P.FolioPreSalida = D.FolioPreSalida And P.IdClaveSSA = D.IdClaveSSA )  
		Inner Join vw_ExistenciaPorCodigoEAN_Lotes VP (Nolock)  
			On ( P.IdEmpresa = VP.IdEmpresa And P.IdEstado = VP.IdEstado And P.IdFarmacia = VP.IdFarmacia And P.IdSubFarmacia = VP.IdSubFarmacia
				And P.IdClaveSSA = VP.IdClaveSSA_Sal And P.IdProducto = VP.IdProducto And P.CodigoEAN = VP.CodigoEAN And P.ClaveLote = VP.ClaveLote )
		Inner Join vw_Pasillos_Estantes_Entrepaños U (Nolock)
			On ( P.IdEmpresa = U.IdEmpresa And P.IdEstado = U.IdEstado And P.IdFarmacia = U.IdFarmacia And 
				P.IdPasillo = U.IdPasillo And P.IdEstante = U.IdEstante And P.IdEntrepaño = U.IdEntrepaño )
		-- Inner Join vw_Farmacias_SubFarmacias FS (Nolock)On ( P.IdEstado = FS.IdEstado And P.IdFarmacia = FS.IdFarmacia And P.IdSubFarmacia = FS.IdSubFarmacia ) 
		Inner Join vw_Farmacias FS (Nolock)On ( P.IdEstado = FS.IdEstado And P.IdFarmacia = FS.IdFarmacia ) 
		Inner Join CatEmpresas E (Nolock) On ( P.IdEmpresa = E.IdEmpresa )
		Inner Join CatPersonal CP (Nolock)On (  PE.IdEstado = CP.IdEstado And PE.IdFarmacia = CP.IdFarmacia And PE.IdPersonal = CP.IdPersonal )

	
Go--#SQL	