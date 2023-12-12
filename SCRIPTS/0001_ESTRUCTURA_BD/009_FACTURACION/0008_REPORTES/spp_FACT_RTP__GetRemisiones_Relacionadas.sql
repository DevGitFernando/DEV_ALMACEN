-------------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_FACT_RTP__GetRemisiones_Relacionadas' and xType = 'P' )
    Drop Proc spp_FACT_RTP__GetRemisiones_Relacionadas
Go--#SQL
  
---			Delete from  FACT_Remisiones_Relacionadas 


--		Exec spp_FACT_RTP__GetRemisiones_Relacionadas @FolioRemision = '0000021668' 

--		Exec spp_FACT_RTP__GetRemisiones_Relacionadas @IdEmpresa = '001', @IdEstado = '28', @IdFarmaciaGenera = '0001', @FolioRemision = '0000021565' 


Create Proc spp_FACT_RTP__GetRemisiones_Relacionadas 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '28', 
	@IdFarmaciaGenera varchar(4) = '0001',  
	@FolioRemision varchar(10) = '0000021897' 
)  
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@bEsComplemento int 




	--------------------- FORMATEAR LOS PARAMETROS 
	Set @IdEmpresa = right('0000000000000000' + @IdEmpresa, 3)  
	Set @IdEstado = right('0000000000000000' + @IdEstado, 2)  
	Set @IdFarmaciaGenera = right('0000000000000000' + @IdFarmaciaGenera, 4)  
	Set @FolioRemision = right('0000000000000000' + @FolioRemision, 10)  

	Set @bEsComplemento = 0 
	Select @bEsComplemento = 1 
	From FACT_Remisiones D (NoLock) 
	where 
		D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmaciaGenera = @IdFarmaciaGenera 
		and D.FolioRemision = @FolioRemision 
		and D.TipoDeRemision in ( 3, 5 )  

	Set @bEsComplemento = IsNull(@bEsComplemento, 0) 
	--------------------- FORMATEAR LOS PARAMETROS 


	--	 spp_FACT_RTP__GetRemisiones_Relacionadas 

	------------------------- Obtener datos 
	Select 
		D.IdEmpresa, D.IdEstado, D.IdFarmaciaGenera, D.FolioRemision, D.IdFarmacia, D.IdSubFarmacia, D.FolioVenta, D.ClaveSSA, D.IdProducto, D.CodigoEAN, D.ClaveLote
		, 0 as TipoDeRemision, 0 as TipoDeRemision_Relacionada    
	Into #tmp_Remisiones_Base 
	From FACT_Remisiones_Detalles D (NoLock) 
	--Inner Join FACT_Remisiones_Detalles D (NoLock) On ( R.IdEmpresa = D.IdEmpresa and R.IdEstado = D.IdEstado and R.IdFarmaciaGenera = D.IdFarmaciaGenera and R.FolioRemision = D.FolioRemision ) 
	where 
		D.IdEmpresa = @IdEmpresa and D.IdEstado = @IdEstado and D.IdFarmaciaGenera = @IdFarmaciaGenera 
		and D.FolioRemision = @FolioRemision 
		and @bEsComplemento = 1 
	Group by 
		D.IdEmpresa, D.IdEstado, D.IdFarmaciaGenera, D.FolioRemision, D.IdFarmacia, D.IdSubFarmacia, D.FolioVenta, D.ClaveSSA, D.IdProducto, D.CodigoEAN, D.ClaveLote -- , R.TipoDeRemision 


	Update L Set TipoDeRemision = R.TipoDeRemision 
	From #tmp_Remisiones_Base L (NoLock)    
	Inner Join FACT_Remisiones R (NoLock) On ( L.FolioRemision = R.FolioRemision ) 
	Where @bEsComplemento = 1 

	Update L Set TipoDeRemision_Relacionada = R.TipoDeRemision_Relacionada 
	From #tmp_Remisiones_Base L (NoLock)    
	Inner Join FACT_TiposDeRemisiones R (NoLock) On ( L.TipoDeRemision = R.TipoDeRemision ) 
	Where @bEsComplemento = 1 
	------------------------- Obtener datos 
	
	
	------------------ Informacion relacionada 
	Select 
		D.IdEmpresa, D.IdEstado, D.IdFarmaciaGenera, 
		D.FolioRemision, 

		cast(@FolioRemision as varchar(10)) as FolioRemision_Base, 
		D.FolioRemision as FolioRemision_Relacionado, 

		0 as TipoDeRemision, 0 as TipoDeRemision_Relacionada, 0 as Complementaria   
	Into #tmp_Remisiones_Relacionadas 
	From FACT_Remisiones_Detalles D (NoLock) 
	Inner Join #tmp_Remisiones_Base (Nolock) 
	X On 
		( 
			D.IdEmpresa = X.IdEmpresa and D.IdEstado = X.IdEstado and D.IdFarmacia = X.IdFarmacia and D.IdSubFarmacia = X.IdSubFarmacia 
			and D.FolioVenta = X.FolioVenta 
			and D.IdProducto = X.IdProducto 
			and D.CodigoEAN = X.CodigoEAN and D.ClaveLote = X.ClaveLote 
		) 
	Where @bEsComplemento = 1 
	Group by 
		D.IdEmpresa, D.IdEstado, D.IdFarmaciaGenera, D.FolioRemision  
	


	Update L Set TipoDeRemision = R.TipoDeRemision  
	From #tmp_Remisiones_Relacionadas L (NoLock)    
	Inner Join FACT_Remisiones R (NoLock) On ( L.FolioRemision = R.FolioRemision ) 
	

	Update L Set TipoDeRemision_Relacionada = R.TipoDeRemision_Relacionada 
	From #tmp_Remisiones_Relacionadas L (NoLock)    
	Inner Join FACT_TiposDeRemisiones R (NoLock) On ( L.TipoDeRemision = R.TipoDeRemision ) 
	Where @bEsComplemento = 1 

	Update R Set Complementaria = 1 
	from #tmp_Remisiones_Relacionadas R 
	inner join #tmp_Remisiones_Base L (nolock) on ( R.TipoDeRemision = L.TipoDeRemision_Relacionada ) 
	Where @bEsComplemento = 1 
	------------------ Informacion relacionada 




	----			spp_FACT_RTP__GetRemisiones_Relacionadas  
	
	--select FolioRemision, TipoDeRemision, TipoDeRemision_Relacionada 
	--from #tmp_Remisiones_Base 
	--group by FolioRemision, TipoDeRemision, TipoDeRemision_Relacionada  


	--select distinct R.* 
	--from #tmp_Remisiones_Relacionadas R 
	--inner join tmp_Remisiones_Base L (nolock) on ( R.TipoDeRemision = L.TipoDeRemision_Relacionada ) 


	--select * 
	--from #tmp_Remisiones_Relacionadas R 
	----Where R.FolioRemision = 0000019081 



	Insert Into FACT_Remisiones_Relacionadas ( IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision, FolioRemision_Relacionado ) 
	Select  IdEmpresa, IdEstado, IdFarmaciaGenera, FolioRemision_Base, FolioRemision_Relacionado 
	From #tmp_Remisiones_Relacionadas R 
	Where Complementaria = 1 
		and Not Exists 
		( 
			Select * 
			From FACT_Remisiones_Relacionadas H (NoLock) 
			Where R.IdEmpresa = H.IdEmpresa and R.IdEstado = H.IdEstado and R.IdFarmaciaGenera = H.IdFarmaciaGenera 
				and R.FolioRemision_Base = H.FolioRemision and R.FolioRemision_Relacionado = H.FolioRemision_Relacionado 
		) 
		and @bEsComplemento = 1 

End 
Go--#SQL

