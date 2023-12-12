If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Bitacora_Movimientos' and xType = 'P' ) 
   Drop Proc spp_Bitacora_Movimientos 
Go--#SQL

Create Proc spp_Bitacora_Movimientos ( @Tipo int = 1 ) 
With Encryption 
As 
Begin 

	Select 
		IdEstado, space(100) as Estado, 
		IdFarmacia, space(200) as Farmacia, 
		IdTipoMovto_Inv as TipoMovimiento, space(100) as Movimiento, (case when TipoES = 'E' then 'Entrada' else 'Salida' end) as Tipo, 
		count(*) as Folios, min(FechaRegistro) as FechaMenor,  max(FechaRegistro) as FechaMayor 
	Into #tmpFolios 	
	From MovtosInv_Enc E (NoLock)  
	Group By IdEstado, IdFarmacia, IdTipoMovto_Inv, TipoES  
	Order by IdEstado, IdFarmacia, TipoES   
	
	
	Update M Set Estado = F.Estado, Farmacia = F.Farmacia 
	From #tmpFolios M 
	Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado =  F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 
	
	Update M Set Movimiento = T.Descripcion 
	From #tmpFolios M 
	Inner Join Movtos_Inv_Tipos T (NoLock) On ( M.TipoMovimiento = T.IdTipoMovto_Inv ) 
	
------		spp_Bitacora_Movimientos 	
	

---------------	Salida Final 	
	If @Tipo = 1 
	Begin 
		If Exists ( select * from sysobjects (nolock) where Name = 'Ctl_Bitacora_Movimientos'  and xType = 'U' ) 
		   Drop Table Ctl_Bitacora_Movimientos 
	
		Select 
			IdFarmacia, Farmacia, 
			sum(Folios) as Folios, min(FechaMenor) as FechaMenor,  max(FechaMayor) as FechaMayor
		Into Ctl_Bitacora_Movimientos 	
		From #tmpFolios 
		Group by IdEstado, Estado, IdFarmacia, Farmacia 
		
		
		Select * 
		From Ctl_Bitacora_Movimientos 
		
	End 	

	If @Tipo = 2 
		Begin 
		Select 
			IdFarmacia, Farmacia, Tipo, 
			sum(Folios) as Folios, min(FechaMenor) as FechaMenor,  max(FechaMayor) as FechaMayor
		From #tmpFolios 
		Group by IdEstado, Estado, IdFarmacia, Farmacia, Tipo  

		Select IdFarmacia, Farmacia, TipoMovimiento, Tipo, 
			Folios, FechaMenor, FechaMayor 
		From #tmpFolios 
	End 
---------------	Salida Final 


End 
Go--#SQL 
