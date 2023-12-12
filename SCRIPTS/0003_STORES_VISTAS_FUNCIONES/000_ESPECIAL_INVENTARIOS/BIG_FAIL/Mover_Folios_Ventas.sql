
begin tran 
	
--	Select * 
/* 
	select top 17 * 
	From VentasEnc X (NoLock) 
	order by FolioVenta desc 
*/ 	

	Update x Set FolioVenta = right('0000000000' + cast( (cast(FolioVenta as int) + 1) as varchar), 8) 
	From VentasEnc X (NoLock) 
	Where FolioVenta >= 46  



--	Select top 10 IdTipoMovto_inv +	right('0000000000' + cast( (cast( right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_inv)  ) as int) + 1) as varchar), 8), * 
	Update x Set FolioMovtoInv = IdTipoMovto_inv +	right('0000000000' + cast( (cast( right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_inv)  ) as int) + 1) as varchar), 8) 
	from MovtosInv_Enc x (NoLock) 
	where IdTipoMovto_inv = 'SV' 
	      and FolioMovtoInv >= 'SV00000046' 
--	order by FolioMovtoInv desc	

	
	
-----  Reasignar 	
	Update x Set FolioVenta = right('0000000000' + '46', 8) 
	From VentasEnc X (NoLock) 
	Where FolioVenta = 68 	
	
	
	Update x Set FolioMovtoInv = IdTipoMovto_inv +	right('0000000000' + '46', 8)  	
	from MovtosInv_Enc x (NoLock) 
	where IdTipoMovto_inv = 'SV' 
	      and FolioMovtoInv = 'SV00000068'
	      	
	      		
----- Revisar hueco 	
	select top 10 * 
	from VentasEnc (nolock) 
	Where FolioVenta >= 46 
	
	select * 
	from MovtosInv_Enc (nolock) 
	where IdTipoMovto_inv = 'EC' 
	      and right(FolioMovtoInv, 8) >= 654 



---    rollback tran 

---    commit tran 

--------------------------------------------- 
begin tran 

	Update x Set FolioVenta = right('0000000000' + cast( (cast(FolioVenta as int) - 5) as varchar), 8) 
	From VentasEnc X (NoLock) 
	Where FolioVenta >= 69  


--	Select top 10 IdTipoMovto_inv +	right('0000000000' + cast( (cast( right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_inv)  ) as int) + 1) as varchar), 8), * 
	Update x Set FolioMovtoInv = IdTipoMovto_inv +	right('0000000000' + cast( (cast( right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_inv)  ) as int) - 5) as varchar), 8) 
	from MovtosInv_Enc x (NoLock) 
	where IdTipoMovto_inv = 'SV' 
	      and FolioMovtoInv >= 'SV00000069' 

---    rollback tran 

---    commit tran 

--	sp_truncatelog 	1

--	sp_backupdb 1, '', 'Almacen_Ventas'

begin tran 
	
--	select V.FolioVenta, V.FechaSistema, V.FechaRegistro, I.FolioMovtoInv, I.FechaSistema, I.FechaRegistro  
    update I Set FechaSistema = V.FechaRegistro, FechaRegistro = V.FechaRegistro
	from MovtosInv_Enc I (nolock) 
	inner join 	VentasEnc V (nolock) On ( V.FolioVenta = right(I.FolioMovtoInv, 8) ) 
	where I.IdTipoMovto_Inv = 'SV' 	
--	order by 3 
	
	
--	select top 1 I.FolioMovtoInv, I.FechaSistema, I.FechaRegistro, D.*  
    update D Set FechaSistema = I.FechaRegistro
	from MovtosInv_Det_CodigosEAN D (nolock) 
	inner join MovtosInv_Enc I (nolock) On ( I.FolioMovtoInv = D.FolioMovtoInv ) 
	where I.IdTipoMovto_Inv = 'SV' 		

---    rollback tran 

---    commit tran 


	select top 1 I.FolioMovtoInv, I.FechaSistema, I.FechaRegistro, D.*  
	from MovtosInv_Enc I (nolock) 
	inner join 	MovtosInv_Det_CodigosEAN D (nolock) On ( I.FolioMovtoInv = D.FolioMovtoInv ) 
	where I.IdTipoMovto_Inv = 'SV' 	
	