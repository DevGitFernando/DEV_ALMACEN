If Exists ( Select Name From Sysobjects (nolock) Where Name = 'vw_Proveedores' and xType = 'V' ) 
   Drop View vw_Proveedores
Go--#SQL

Create View vw_Proveedores 
With Encryption 
As 
	Select P.IdProveedor, P.Nombre, P.AliasNombre, P.RFC, 
		 P.IdEstado, G.Estado, P.IdMunicipio, G.Municipio, P.IdColonia, G.Colonia, 
		 P.Domicilio, P.CodigoPostal, P.Telefonos, 
		 P.TieneLimiteDeCredito, P.LimiteDeCredito, P.CreditoSuspendido, 
		 P.SaldoActual, P.CtaMay, P.SubCta, P.SSbCta, P.SSSCta,
		 P.Status, (case when P.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' end) as StatusAux 
	From CatProveedores P (nolock) 
	Inner Join vw_Geograficos G (NoLock) 
		On ( P.IdEstado = G.IdEstado and P.IdMunicipio = G.IdMunicipio and P.IdColonia = G.IdColonia  )
Go--#SQL