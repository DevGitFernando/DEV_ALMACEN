
	

Begin tran 

	Alter Table ComprasDet_Lotes Drop Constraint FK_ComprasDet_Lotes_VentasDet 
	Alter Table ComprasDet Drop Constraint FK_ComprasDet_ComprasEnc 
	
	

	Alter Table ComprasDet Add Constraint FK_ComprasDet_ComprasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCompra ) References ComprasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioCompra ) 
	On Update Cascade 
	 

	Alter Table ComprasDet_Lotes Add Constraint FK_ComprasDet_Lotes_VentasDet 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioCompra, IdProducto, CodigoEAN, Renglon ) 
		References ComprasDet ( IdEmpresa, IdEstado, IdFarmacia, FolioCompra, IdProducto, CodigoEAN, Renglon ) 
	On Update Cascade 


----------------- 
	Alter Table VentasDet_Lotes Drop Constraint FK_VentasDet_Lotes_VentasDet 
	Alter Table VentasDet Drop Constraint FK_VentasDet_VentasEnc 
	Alter Table VentasInformacionAdicional Drop Constraint FK_VentasInformacionAdicional_VentasEnc 
	
	
	Alter Table VentasDet Add Constraint FK_VentasDet_VentasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
	On Update Cascade 


	Alter Table VentasDet_Lotes Add Constraint FK_VentasDet_Lotes_VentasDet 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon ) 
		References VentasDet ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdProducto, CodigoEAN, Renglon ) 
	On Update Cascade 

	Alter Table VentasInformacionAdicional Add Constraint FK_VentasInformacionAdicional_VentasEnc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
	On Update Cascade 
	
	
----------------- 
	Alter Table MovtosInv_Det_CodigosEAN_Lotes Drop Constraint FK_MovtosInv_Det_CodigosEAN_Lotes_MovtosInv_Det_CodigosEAN 
	Alter Table MovtosInv_Det_CodigosEAN Drop Constraint FK_MovtosInv_Det_CodigosEAN_MovtosInv_Enc 
	

	Alter Table MovtosInv_Det_CodigosEAN Add Constraint FK_MovtosInv_Det_CodigosEAN_MovtosInv_Enc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) References MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 
	On Update Cascade 
	

	Alter Table MovtosInv_Det_CodigosEAN_Lotes Add Constraint FK_MovtosInv_Det_CodigosEAN_Lotes_MovtosInv_Det_CodigosEAN
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN ) 
		References MovtosInv_Det_CodigosEAN ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN ) 
	On Update Cascade 

------------------------------------------------ 
/* 

--	Select * 
	Update x Set FolioCompra = right('0000000000' + cast( (cast(folioCompra as int) + 1) as varchar), 8) 
	From ComprasEnc X (NoLock) 
	Where FolioCompra Between 625 and 625 

	Select * 
	From ComprasEnc X (NoLock) 
	Where FolioCompra Between 624 and 647 


--	Select top 10 IdTipoMovto_inv +	right('0000000000' + cast( (cast( right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_inv)  ) as int) + 1) as varchar), 8), * 
	Update x Set FolioMovtoInv = IdTipoMovto_inv +	right('0000000000' + cast( (cast( right(FolioMovtoInv, len(FolioMovtoInv) - len(IdTipoMovto_inv)  ) as int) + 1) as varchar), 8) 
	from MovtosInv_Enc (NoLock) 
	where IdTipoMovto_inv = 'EC' 
	order by FolioMovtoInv desc	

*/ 

--		rollback tran 

--		commit tran 

