
------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Pedidos__Ubicaciones_Excluidas_Surtimiento' and xType = 'V' ) 
	Drop View vw_Pedidos__Ubicaciones_Excluidas_Surtimiento 
Go--#SQL

Create View vw_Pedidos__Ubicaciones_Excluidas_Surtimiento 
With Encryption 
As 
		Select C.*, Excluida
		From Pedidos__Ubicaciones_Excluidas_Surtimiento E (NoLock)
		Inner Join vw_Pasillos_Estantes_Entrepaños C (NoLock)
			On (C.IdEmpresa = E.IdEmpresa And C.IdEstado = E.IdEstado And C.IdFarmacia = E.IdFarmacia
				And C.IdPasillo = E.IdPasillo And C.IdEstante = E.IdEstante And C.IdEntrepaño = E.IdEntrepaño)
		Where Excluida = 1

Go--#SQL