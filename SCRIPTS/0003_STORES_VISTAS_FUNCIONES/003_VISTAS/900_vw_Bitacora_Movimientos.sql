If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Bitacora_Movimientos' and xType = 'V' ) 
   Drop View vw_Bitacora_Movimientos 
Go--#SQL

Create View vw_Bitacora_Movimientos 
With Encryption 
As 	
	Select IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		count(*) as NumMovimientos, min(FechaReg) as FechaRegMenor,  max(FechaReg) as FechaRegMayor 
	From vw_MovtosInv_Enc 
	Group by IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia 
Go--#SQL 
