If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Net_Proveedores_Mtto' and xType = 'P' )
   Drop Proc spp_Net_Proveedores_Mtto
Go--#SQL 

Create Proc spp_Net_Proveedores_Mtto ( @IdProveedor varchar(4), @LoginProv varchar(20), @Password varchar(500), @Status varchar(1) ) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From Net_Proveedores (NoLock) Where IdProveedor = @IdProveedor ) 
	   Insert Into Net_Proveedores ( IdProveedor, LoginProv, Password, Status, Actualizado ) Select @IdProveedor, @LoginProv, @Password, @Status, 0 
	else 
	   Update Net_Proveedores Set Password = @Password, Status = @Status, Actualizado = 0 Where IdProveedor = @IdProveedor 

End 
Go--#SQL


If Exists ( Select Name From Sysobjects (nolock) Where Name = 'vw_Net_Proveedores' and xType = 'V' ) 
   Drop View vw_Net_Proveedores
Go--#SQL

Create View vw_Net_Proveedores 
With Encryption 
As 
	Select P.IdProveedor, N.LoginProv as Login, P.Nombre, P.AliasNombre, P.RFC, 
		 P.IdEstado, G.Estado, P.IdMunicipio, G.Municipio, P.IdColonia, G.Colonia, 
		 P.Domicilio, P.CodigoPostal, P.Telefonos, 
		 P.TieneLimiteDeCredito, P.LimiteDeCredito, P.CreditoSuspendido, 
		 P.SaldoActual, P.CtaMay, P.SubCta, P.SSbCta, P.SSSCta,
		 P.Status, (case when P.Status = 'A' Then 'ACTIVO' Else 'CANCELADO' end) as StatusAux 
	From CatProveedores P (nolock) 
    Inner Join Net_Proveedores N (NoLock) On ( N.IdProveedor = P.IdProveedor ) 
	Inner Join vw_Geograficos G (NoLock) 
		On ( P.IdEstado = G.IdEstado and P.IdMunicipio = G.IdMunicipio and P.IdColonia = G.IdColonia  )
Go--#SQL