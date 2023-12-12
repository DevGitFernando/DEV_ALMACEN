--------------------------------------------------------------------------------------------------------------------------------------- 
If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_IATP2_CFGC_Clientes' And xType = 'V' )
	Drop view vw_IATP2_CFGC_Clientes
Go--#SQL  

Create View vw_IATP2_CFGC_Clientes 
As
	Select C.IdCliente, C.IdEstado, F.Estado, C.IdFarmacia, F.Farmacia, C.Status, C.Actualizado
	From IATP2_CFGC_Clientes C (NoLock)
	Inner Join vw_Farmacias F(NoLock) On( C.IdEstado = F.IdEstado And C.IdFarmacia = F.IdFarmacia )
Go--#SQL  



