

	----select EsEntrada, sum(cantidad)  
	----from MovtosInv_Enc____Secuencia_EAN 
	----group by EsEntrada 


-- Begin Tran 

---		rollback tran   

---		commit tran   





---	7501565902196 

---	'7501565902196'

	--select * 
	--from MovtosInv_Enc____Secuencia_EAN E (NoLock)  
	----Where CodigoEAN = '7501565902196' 

Declare 
	@sCodigoEAN varchar(30) 

	Set @sCodigoEAN = '' 

	Declare #cursorTriggers  
	Cursor For 
		Select Top 10000 CodigoEAN 
		From Existencia__Secuencia__02_EAN T 
		Order by IdProducto  
	Open #cursorTriggers 
	FETCH NEXT FROM #cursorTriggers Into @sCodigoEAN  
		WHILE @@FETCH_STATUS = 0 
		BEGIN 

			------------------------------------------------------ PROCESAMIENTO A NIVEL PRODUCTO 
			Update E Set AcumuladoEntradas = 
				IsNull( 
					(
						Select sum(Cantidad) 
						From MovtosInv_Enc____Secuencia_EAN H (NoLock) 
						Where H.EsEntrada = 1 
							and H.IdEmpresa = E.IdEmpresa and H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia 
							And H.IdProducto = E.IdProducto and H.CodigoEAN = E.CodigoEAN and H.Keyx <= E.Keyx
					)
				, 0) 
			from MovtosInv_Enc____Secuencia_EAN E (NoLock) 
			Where CodigoEAN = @sCodigoEAN 


			Update E Set AcumuladoSalidas = 
				IsNull( 
					(
						Select sum(Cantidad) 
						From MovtosInv_Enc____Secuencia_EAN H (NoLock) 
						Where H.EsEntrada =  0 
							and H.IdEmpresa = E.IdEmpresa and H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia 
							And H.IdProducto = E.IdProducto and H.CodigoEAN = E.CodigoEAN and H.Keyx <= E.Keyx
					)
				, 0) 
			from MovtosInv_Enc____Secuencia_EAN E (NoLock) 
			Where CodigoEAN = @sCodigoEAN 


			Update E Set ExistenciaAcumulada = (AcumuladoEntradas - AcumuladoSalidas)
			from MovtosInv_Enc____Secuencia_EAN E (NoLock) 
			Where CodigoEAN = @sCodigoEAN


			Update E Set ExistenciaFinal = 
				IsNull( 
					(
						Select top 1 ExistenciaAcumulada  
						From MovtosInv_Enc____Secuencia_EAN H (NoLock) 
						Where 
							H.IdEmpresa = E.IdEmpresa and H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia 
							and H.IdProducto = E.IdProducto and H.CodigoEAN = E.CodigoEAN 
						order by Keyx desc 
					)
				, 0) 
			from MovtosInv_Enc____Secuencia_EAN E (NoLock) 
			Where CodigoEAN = @sCodigoEAN 


			Update H Set Procesado = 1, Existencia = E.ExistenciaFinal 
			From Existencia__Secuencia__02_EAN H (NoLock) 
			Inner Join  MovtosInv_Enc____Secuencia_EAN E (NoLock) 
				On ( H.IdEmpresa = E.IdEmpresa and H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia and H.IdProducto = E.IdProducto and H.CodigoEAN = E.CodigoEAN )
			Where H.CodigoEAN = @sCodigoEAN

			--select * 
			--from Existencia__Secuencia__02_EAN H (NoLock) 
			--Where H.CodigoEAN = '7501565902196' 
			------------------------------------------------------ PROCESAMIENTO A NIVEL PRODUCTO 


			--select * 
			--from MovtosInv_Enc____Secuencia_EAN E (NoLock)  
			----Where CodigoEAN = '7501565902196' 



			------------------------------------------------------ PROCESAMIENTO A NIVEL LOTES  
			Update E Set AcumuladoEntradas = 
				IsNull( 
					(
						Select sum(Cantidad) 
						From MovtosInv_Enc____Secuencia_EAN_Lotes H (NoLock) 
						Where H.EsEntrada = 1 
							and H.IdEmpresa = E.IdEmpresa and H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia 
							And H.IdProducto = E.IdProducto and H.CodigoEAN = E.CodigoEAN 
							and H.ClaveLote = E.ClaveLote and H.IdSubFarmacia = E.IdSubFarmacia 
							and H.Keyx <= E.Keyx
					)
				, 0) 
			from MovtosInv_Enc____Secuencia_EAN_Lotes E (NoLock) 
			Where CodigoEAN = @sCodigoEAN 

			Update E Set AcumuladoSalidas = 
				IsNull( 
					(
						Select sum(Cantidad) 
						From MovtosInv_Enc____Secuencia_EAN_Lotes H (NoLock) 
						Where H.EsEntrada = 0  
							and H.IdEmpresa = E.IdEmpresa and H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia 
							And H.IdProducto = E.IdProducto and H.CodigoEAN = E.CodigoEAN 
							and H.ClaveLote = E.ClaveLote and H.IdSubFarmacia = E.IdSubFarmacia 
							and H.Keyx <= E.Keyx
					)
				, 0) 
			from MovtosInv_Enc____Secuencia_EAN_Lotes E (NoLock) 
			Where CodigoEAN = @sCodigoEAN

			Update E Set ExistenciaAcumulada = (AcumuladoEntradas - AcumuladoSalidas)
			from MovtosInv_Enc____Secuencia_EAN_Lotes E (NoLock) 
			Where CodigoEAN = @sCodigoEAN 


			Update E Set ExistenciaFinal = 
				IsNull( 
					(
						Select top 1 ExistenciaAcumulada  
						From MovtosInv_Enc____Secuencia_EAN_Lotes H (NoLock) 
						Where 
							H.IdEmpresa = E.IdEmpresa and H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia 
							and H.IdProducto = E.IdProducto and H.CodigoEAN = E.CodigoEAN 
							and H.ClaveLote = E.ClaveLote and H.IdSubFarmacia = E.IdSubFarmacia 
						order by Keyx desc 
					)
				, 0) 
			from MovtosInv_Enc____Secuencia_EAN_Lotes E (NoLock) 
			Where CodigoEAN = @sCodigoEAN 


			Update H Set Procesado = 1, Existencia = E.ExistenciaFinal 
			From Existencia__Secuencia__03_Lotes H (NoLock) 
			Inner Join  MovtosInv_Enc____Secuencia_EAN_Lotes E (NoLock) 
				On ( H.IdEmpresa = E.IdEmpresa and H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia and H.IdFarmacia = E.IdFarmacia 
					and H.IdProducto = E.IdProducto and H.CodigoEAN = E.CodigoEAN and H.ClaveLote = E.ClaveLote )
			Where H.CodigoEAN = @sCodigoEAN

			------------------------------------------------------ PROCESAMIENTO A NIVEL LOTES  

			FETCH NEXT FROM #cursorTriggers Into @sCodigoEAN    
		END	 
	Close #cursorTriggers 
	Deallocate #cursorTriggers 	



	--select * 
	--from MovtosInv_Enc____Secuencia_EAN_Lotes E (NoLock)  
	----Where CodigoEAN = '7501565902196' 



	--select * 
	--from Existencia__Secuencia__02_EAN H (NoLock) 
	--Where H.CodigoEAN = '7501565902196' 
	
	--select * 
	--from MovtosInv_Enc____Secuencia_EAN_Lotes H (NoLock) 
	--Where H.CodigoEAN = '7501565902196' 
	
		 