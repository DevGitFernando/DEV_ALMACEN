
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_INT_MA__RecetasElectronicas' and xType = 'V' ) 
   Drop View vw_INT_MA__RecetasElectronicas
Go--#SQL

Create View vw_INT_MA__RecetasElectronicas
With Encryption 
As 

	Select 
		E.Folio, E.FechaRegistro, E.Surtido, E.FechaSurtido, E.IdEmpresaSurtido, E.IdEstadoSurtido, E.IdFarmaciaSurtido, E.IdPersonalSurtido, 
		E.Folio_MA,
		E.IdFarmacia, E.NombrePaciente, E.NombreMedico, E.Especialidad, E.Copago, E.PlanBeneficiario, E.FechaEmision, E.Elegibilidad,
		E.CIE_01, dbo.fg_Nombre_CIE10_Diagnosticos(E.CIE_01) As NombreCIE_01, E.CIE_02, dbo.fg_Nombre_CIE10_Diagnosticos(E.CIE_02) As NombreCIE_02,
		E.CIE_03, dbo.fg_Nombre_CIE10_Diagnosticos(E.CIE_03) As NombreCIE_03, E.CIE_04, dbo.fg_Nombre_CIE10_Diagnosticos(E.CIE_04) As NombreCIE_04,
		E.EsRecetaManual,
		R.Partida, P.IdProducto, P.CodigoEAN, P.DescripcionCorta, P.ClaveSSA, P.DescripcionCortaClave,
		P.TasaIva, R.CantidadSolicitada, R.CantidadSurtida, (R.CantidadSolicitada - R.CantidadSurtida) As Cantidad
	From INT_MA__RecetasElectronicas_001_Encabezado E (NoLock)
	Inner Join INT_MA__RecetasElectronicas_002_Productos R (NoLock) On ( E.Folio_MA = R.Folio_MA )
	Inner Join vw_Productos_CodigoEAN P On ( R.CodigoEAN = P.IdProducto )

	
Go--#SQL 