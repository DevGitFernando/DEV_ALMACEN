

begin tran 
	
--	Select * 
/* 
	select top 17 * 
	From ComprasEnc X (NoLock) 
	order by FolioCompra desc 
*/ 	

--	sp_truncatelog 1 

--  sp_backupdb 1, '', 'Compras_Paso_003_OrdenFolios_Fechas'
	

	Update x Set FolioCompra = right('0000000000' + cast( (cast(folioCompra as int) + 1) as varchar), 8) 
	From ComprasEnc X (NoLock) 
	Where FolioCompra >= 472  



--	Select top 10 IdTipoMovto_inv +	right('0000000000' + cast( (cast( right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_inv)  ) as int) + 1) as varchar), 8), * 
	Update x Set FolioMovtoInv = IdTipoMovto_inv +	right('0000000000' + cast( (cast( right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_inv)  ) as int) + 1) as varchar), 8) 
	from MovtosInv_Enc x (NoLock) 
	where IdTipoMovto_inv = 'EC' 
	      and FolioMovtoInv >= 'EC00000472' 
--	order by FolioMovtoInv desc	
	
	
-----  Reasignar 	
	Update x Set FolioCompra = right('0000000000' + '472', 8) 
	From ComprasEnc X (NoLock) 
	Where FolioCompra = 634 	
	
	
	Update x Set FolioMovtoInv = IdTipoMovto_inv +	right('0000000000' + '472', 8)  	
	from MovtosInv_Enc x (NoLock) 
	where IdTipoMovto_inv = 'EC' 
	      and FolioMovtoInv = 'EC00000634'
	      	

----- Revisar hueco 	
	select * -- FolioCompra = right('0000000000' + cast( (cast(folioCompra as int) - 19) as varchar), 8), * 
	from ComprasEnc (nolock) 
	Where FolioCompra >= 649 
	
	select * 
	from MovtosInv_Enc (nolock) 
	where IdTipoMovto_inv = 'EC' 
	      -- and right(FolioMovtoInv, 8) >= 624 
	order by FolioMovtoInv 



---    rollback tran 
---    commit tran 


---------------------- 
Begin tran 

	Update x Set FolioCompra = right('0000000000' + cast( (cast(folioCompra as int) - 19) as varchar), 8) 
	From ComprasEnc X (NoLock) 
	Where FolioCompra >= 649  



--	Select top 10 IdTipoMovto_inv +	right('0000000000' + cast( (cast( right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_inv)  ) as int) + 1) as varchar), 8), * 
	Update x Set FolioMovtoInv = IdTipoMovto_inv +	right('0000000000' + cast( (cast( right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_inv)  ) as int) - 19) as varchar), 8) 
	from MovtosInv_Enc x (NoLock) 
	where IdTipoMovto_inv = 'EC' 
	      and FolioMovtoInv >= 'EC00000649' 
--	order by FolioMovtoInv desc	


---    rollback tran 
---    commit tran 
---------------------------------------------- 

begin tran 
	
	select V.FolioCompra, V.FechaSistema, V.FechaRegistro, I.FolioMovtoInv, I.FechaSistema, I.FechaRegistro  
--    update I Set FechaSistema = V.FechaRegistro, FechaRegistro = V.FechaRegistro
	from MovtosInv_Enc I (nolock) 
	inner join 	ComprasEnc V (nolock) On ( V.FolioCompra = right(I.FolioMovtoInv, 8) ) 
	where I.IdTipoMovto_Inv = 'EC' 	
	order by V.FolioCompra 
--	order by 3 
	
	
--	select top 1 I.FolioMovtoInv, I.FechaSistema, I.FechaRegistro, D.*  
    update D Set FechaSistema = I.FechaRegistro
	from MovtosInv_Det_CodigosEAN D (nolock) 
	inner join MovtosInv_Enc I (nolock) On ( I.FolioMovtoInv = D.FolioMovtoInv ) 
	where I.IdTipoMovto_Inv = 'EC' 		

---    rollback tran 

---    commit tran 


	select top 1 I.FolioMovtoInv, I.FechaSistema, I.FechaRegistro, D.*  
	from MovtosInv_Enc I (nolock) 
	inner join 	MovtosInv_Det_CodigosEAN D (nolock) On ( I.FolioMovtoInv = D.FolioMovtoInv ) 
	where I.IdTipoMovto_Inv = 'SV' 	
	