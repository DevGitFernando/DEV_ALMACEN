If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Vehiculos' and xType = 'V' ) 
	Drop View vw_Vehiculos 
Go--#SQL

Create View vw_Vehiculos 
With Encryption 
As

	Select V.IdEstado, F.Estado, V.Idfarmacia, F.Farmacia, V.IdVehiculo, V.Marca, V.Modelo, V.Descripcion, V.NumSerie, V.Placas, V.FechaRegistro, V.Status
	From CatVehiculos V (NoLock)
	Inner Join vw_Farmacias F (NoLock) On (V.IdEstado = F.IdEstado And V.IdFarmacia =  F.IdFarmacia)
	
Go--#SQL