------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_INT_ND_GenerarExistencias__001_Ejecucion' and xType = 'P') 
    Drop Proc spp_INT_ND_GenerarExistencias__001_Ejecucion 
Go--#SQL 
  
--  ExCB spp_INT_ND_GenerarExistencias__001_Ejecucion '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_INT_ND_GenerarExistencias__001_Ejecucion 
(   
    @IdEmpresa varchar(3) = '003', @IdEstado varchar(2) = '16', 
    @IdFarmacia varchar(4) = '13', 
    @CodigoCliente varchar(20) = '2179067', 
    @FechaDeProceso varchar(10) = '2015-05-31', 
    @MostrarResultado int = 1     
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set 
DateFormat YMD  

Declare 
	@Folio varchar(8), 
	@sFCBha varchar(8), 
	@sConsCButivo varchar(3), 
	@sMensaje varchar(1000), 
	@IdSubFarmacia varchar(2), 
	@bCrearTablaBase int   

Declare 
	@GUID varchar(max) 
	
	Set @GUID = cast(NEWID() as varchar(max)) 	
	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	Set @IdFarmacia = right('0000' + @IdFarmacia, 4) 
	Set @IdSubFarmacia = '05' 
	Set @bCrearTablaBase = 1 

---------------------- Farmacias a procesar   
	Select * 
	Into #tmpClientes 
	From vw_INT_ND_Clientes F 
	Where F.IdEstado = @IdEstado and CodigoCliente = @CodigoCliente and IdFarmacia = @IdFarmacia 

	Select top 1 @IdFarmacia = IdFarmacia From #tmpClientes 


	----If @CodigoCliente = '*' or @CodigoCliente = ''
	----Begin 
	----	Delete From #tmpClientes 
		
	----	Insert Into #tmpClientes  
	----	Select * 
	----	From vw_INT_ND_Clientes F 
	----	Where F.IdEstado = @IdEstado 
				
	----End 
---------------------- Farmacias a procesar   


---------------------- Base de existencias 
/* 
	Select * 
	Into vw_ExistenciaPorCodigoEAN_Lotes__PRCS  
	From vw_ExistenciaPorCodigoEAN_Lotes 
	Where IdEmpresa <> '' 
*/ 

------------------------------------- Validar la creacion de la tabla base del proceso de existencias 
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_ExistenciaPorCodigoEAN_Lotes__PRCS' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'vw_ExistenciaPorCodigoEAN_Lotes__PRCS' and xType = 'U' and datediff(dd, crDate, getdate()) > 1
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 
	End 
		

	If @bCrearTablaBase = 1 
	Begin 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_ExistenciaPorCodigoEAN_Lotes__PRCS' and xType = 'U' ) 
		   Drop Table vw_ExistenciaPorCodigoEAN_Lotes__PRCS 
	
		Select * 
		Into vw_ExistenciaPorCodigoEAN_Lotes__PRCS  
		From vw_ExistenciaPorCodigoEAN_Lotes 
		Where IdEmpresa <> '' 	
	End 

------------------------------------- Validar la creacion de la tabla base del proceso de existencias 


	Select 
		IdEmpresa, Empresa, 
		IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
		IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, 
		cast('' as varchar(20)) as ClaveSSA_ND, 
		cast('' as varchar(20)) as ClaveSSA_ND_Aux, 
		DescripcionClave as Descripcion_ClaveSSA, 
		cast('' as varchar(7000)) as DescripcionClave, 		
		cast('' as varchar(7000)) as DescripcionComercial, 
		TipoDeClave, TipoDeClaveDescripcion, 
		IdProducto, 
		cast('' as varchar(30)) as CodigoEAN_ND, 
		CodigoEAN, 
		CodigoEAN as CodigoEAN_Base, 
		DescripcionProducto, ContenidoPaquete, 
		ClaveLote, 

/* 		
		cast(Existencia as int) as PiezasTotales, 
		floor((Existencia / (ContenidoPaquete*1.0))) as CajasCompletas,  
		(Existencia - (floor((Existencia / (ContenidoPaquete*1.0))) * ContenidoPaquete)) as PiezasSueltas,  		
*/

		cast(0 as int) as PiezasTotales, 
		floor((0 / (ContenidoPaquete*1.0))) as CajasCompletas,  
		(0 - (floor((0 / (ContenidoPaquete*1.0))) * ContenidoPaquete)) as PiezasSueltas,  
		
		FechaCaducidad, MesesParaCaducar, 
		0 as MovtoDia, 
		0 as CodigoRelacionado  	
	Into #tmpExistencia 
	From vw_ExistenciaPorCodigoEAN_Lotes__PRCS F (NoLock) 
	Where F.IdEmpresa = @IdEmpresa -- and F.IdSubFarmacia = @IdSubFarmacia   
		-- and F.ClaveSSA = 'SC-MC-285' 
		--and CodigoEAN = '7501075718546' 
		--and CodigoEAN = '759684471155' 
		--and CodigoEAN = '382903883172' 
		-- and CodigoEAN = '7501082224269' and ClaveLote = '*C293421D' 
		and exists (	Select *  From #tmpClientes C (NoLock) 
						Where  F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia  ) 
		and Not Exists 
			( 
				Select * 
				From INT_ND_SubFarmaciasConsigna C (NoLock) 
				Where F.IdEstado = C.IdEstado and F.IdSubFarmacia = C.IdSubFarmacia  		
			) 	


--	select * from #tmpExistencia 

	Exec spp_INT_ND_GenerarExistencias__001_Ejecucion_Detalle @IdEmpresa, @IdEstado, @IdFarmacia, @IdSubFarmacia, @FechaDeProceso, 1 

--	select * from #tmpExistencia 

	Delete From #tmpExistencia where MovtoDia = 0 

---------------------- Base de existencias 




	----If exists ( Select * from sysobjects (nolock) where name = 'tmpExistencia_ND' and xType = 'u' ) 
	----	drop table tmpExistencia_ND 
		
	----Select * 
	----into tmpExistencia_ND 
	----From #tmpExistencia 	

	

	
-------------------------------------------- Asignar , Clave SSA y Descripciones  	
	Update E Set ClaveSSA_ND = '', DescripcionClave = '', DescripcionComercial = '' 
	From #tmpExistencia E 
	
------------ AJUSTES 			
--		spp_INT_ND_GenerarExistencias__001_Ejecucion 

	----------Update E Set ClaveSSA_ND_Aux = C.ClaveSSA_ND 
	----------From #tmpExistencia E 
	----------Inner Join INT_ND_CFG_ClavesSSA C On ( E.ClaveSSA = C.ClaveSSA ) 	
	
	----------Update E Set ClaveSSA_ND = H.ClaveSSA_ND, CodigoEAN_ND = H.CodigoEAN_ND  
	----------From #tmpExistencia E 
	----------Inner Join INT_ND_Productos H (NoLock) 
	----------	On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) )   		
		
	
	----------Update E Set CodigoEAN_ND = H.CodigoEAN_ND, CodigoRelacionado = 1  
	----------From #tmpExistencia E 
	----------Inner Join INT_ND_CFG_CodigosEAN H (NoLock) 
	----------	On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN, 30) )   				
		
		
	----------Update E Set ClaveSSA_ND = H.ClaveSSA_ND, DescripcionComercial = H.Descripcion 
	----------From #tmpExistencia E 
	----------Inner Join INT_ND_Productos H (NoLock) 
	----------	-- On ( E.CodigoEAN_ND = H.CodigoEAN_ND )  		
	----------	On ( right(replicate('0', 30) + E.CodigoEAN_ND, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) )
		
		
	----------Update E Set DescripcionClave = H.Descripcion 
	----------From #tmpExistencia E 		
	----------Inner Join INT_ND_Claves H (NoLock) On ( E.ClaveSSA_ND = H.ClaveSSA_ND ) 	



	Update E Set CodigoEAN_ND = H.CodigoEAN_ND, CodigoRelacionado = 1 
	From #tmpExistencia E 
	Inner Join INT_ND_Productos H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) ) 
		
	Update E Set CodigoEAN_ND = H.CodigoEAN_ND, CodigoRelacionado = 2 
	From #tmpExistencia E 
	Inner Join INT_ND_CFG_CodigosEAN H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN, 30) )   	
			
	Update E Set ClaveSSA_ND = H.ClaveSSA_ND, CodigoEAN_ND = H.CodigoEAN_ND, DescripcionComercial = H.Descripcion -- , CodigoNadro = H.Codigo 
	From #tmpExistencia E 
	Inner Join INT_ND_Productos H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN_ND, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) ) 	
	
	Update E Set DescripcionClave = L.Descripcion --, Procesado = E.Procesado + 1 
	From #tmpExistencia E 
	inner Join INT_ND_Claves L (NoLock) On ( E.ClaveSSA_ND = L.ClaveSSA_ND ) 	
		
------------ AJUSTES 			


--	spp_INT_ND_GenerarExistencias__001_Ejecucion 
		
-------------------------------------------- Asignar , Clave SSA y Descripciones  	





---------------------- Intermedio 					
------------------------------------- TABLA FINAL 	
	Select * 
	Into #tmpPerfil  
	From vw_CB_CuadroBasico_Farmacias  F (NoLock) 
	Where exists (	Select *  From #tmpClientes C (NoLock) 
					Where  F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia  )
------------------------------------- TABLA FINAL 	




--		Drop table INT_ND__PRCS_Existencias		
 
 
 
--------------------------------------------------- CONCENTRAR INFORMACION    
	If @GUID <> '' 
	Begin 
		Print @GUID 
		If Not Exists ( Select * From Sysobjects (NoLock) Where Name = 'INT_ND__PRCS_Existencias' and xType = 'U' ) 
		Begin 
			Select 
				identity(int, 1, 1) as Keyx, 
				@GUID as GUID, 
				F.MovtoDia,  
				C.CodigoCliente, 
				C.IdEstado, 	
				C.IdFarmacia, C.Farmacia, 
				'01' as Modulo, 
				F.ClaveSSA_Base, F.ClaveSSA, 
				F.ClaveSSA_ND, F.ClaveSSA_ND_Aux, 
				F.DescripcionClave, F.DescripcionComercial, 
				F.TipoDeClave, F.TipoDeClaveDescripcion, 
				F.CodigoEAN_ND, 
				F.CodigoEAN, F.CodigoEAN_Base, 
				F.Descripcion_ClaveSSA, 
				cast(F.CajasCompletas as int) as Existencia, 
				cast(F.CajasCompletas as int) as CajasCompletas, 				
				F.ClaveLote, 
				F.CodigoRelacionado,
				replace(convert(varchar(10), F.FechaCaducidad, 120), '-', '') as Caducidad,  
				-- replace(convert(varchar(10), getdate(), 120), '-', '') as FechaGeneracion  	
				convert(varchar(10), @FechaDeProceso, 120) as FechaGeneracion,  
				convert(varchar(10), getdate(), 120) as FechaEjecucion, 
				getdate() as FechaHoraEjecucion  		
			Into INT_ND__PRCS_Existencias 	
			-- From #tmpPerfil CB (NoLock) 
			From #tmpExistencia F (NoLock) 
			--Inner Join #tmpExistencia F (NoLock) -- On ( CB.IdEstado = F.IdEstado and CB.IdFarmacia = F.IdFarmacia and CB.ClaveSSA = F.ClaveSSA )  
			Inner Join #tmpClientes C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia )  
			Where 1 =  0 
			-- Order by CB.IdEstado, CB.IdFarmacia, CB.DescripcionClave 	
		End 
	
		
		----Delete From INT_ND__PRCS_Existencias Where convert(varchar(10), FechaEjecucion, 120) < convert(varchar(10), getdate(), 120)	
		Delete From INT_ND__PRCS_Existencias 
		Where IdFarmacia = @IdFarmacia 
			and convert(varchar(10), FechaGeneracion, 120) = convert(varchar(10), @FechaDeProceso, 120) 
		
		
		Insert Into INT_ND__PRCS_Existencias  
		( 
			GUID, MovtoDia, CodigoCliente, IdEstado, IdFarmacia, Farmacia, Modulo, ClaveSSA_Base, ClaveSSA, ClaveSSA_ND, ClaveSSA_ND_Aux, DescripcionClave, DescripcionComercial, 
			TipoDeClave, TipoDeClaveDescripcion, 
			CodigoEAN_ND, CodigoEAN, CodigoEAN_Base, Descripcion_ClaveSSA, Existencia, CajasCompletas, ClaveLote, CodigoRelacionado, 
			Caducidad, FechaGeneracion, FechaEjecucion, FechaHoraEjecucion		
		) 
		Select 
			@GUID as GUID, 
			F.MovtoDia,  
			C.CodigoCliente, 
			C.IdEstado, 
			C.IdFarmacia, C.Farmacia, 
			'01' as Modulo, 
			F.ClaveSSA_Base, F.ClaveSSA, 
			F.ClaveSSA_ND, F.ClaveSSA_ND_Aux, 
			F.DescripcionClave, F.DescripcionComercial, 
			F.TipoDeClave, F.TipoDeClaveDescripcion, 
			F.CodigoEAN_ND, 
			F.CodigoEAN, F.CodigoEAN_Base, F.Descripcion_ClaveSSA, 
			cast(F.CajasCompletas as int) as Existencia, 
			cast(F.CajasCompletas as int) as CajasCompletas, 
			F.ClaveLote, F.CodigoRelacionado, 
			replace(convert(varchar(10), F.FechaCaducidad, 120), '-', '') as Caducidad,  
			-- replace(convert(varchar(10), getdate(), 120), '-', '') as FechaGeneracion  	
			convert(varchar(10), @FechaDeProceso, 120) as FechaGeneracion,  
			convert(varchar(10), getdate(), 120) as FechaEjecucion, 
			getdate() as FechaHoraEjecucion   		
		-- From #tmpPerfil CB (NoLock) 
		From #tmpExistencia F (NoLock) 
		--Inner Join #tmpExistencia F (NoLock) -- On ( CB.IdEstado = F.IdEstado and CB.IdFarmacia = F.IdFarmacia and CB.ClaveSSA = F.ClaveSSA )  
		Inner Join #tmpClientes C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia )  
		Order by F.IdEstado, F.IdFarmacia, F.DescripcionClave 	
		

		
-------------------------- Insertar los Productos que no se movieron el dia solicitado 
------		Select 
------			max(Keyx) as Keyx_Anterior,  
------			@GUID as GUID, 0 as MovtoDia, CodigoCliente, IdEstado, IdFarmacia, Farmacia, Modulo, 
------			ClaveSSA_Base, ClaveSSA, ClaveSSA_ND, ClaveSSA_ND_Aux, DescripcionClave, DescripcionComercial, 
------			TipoDeClave, TipoDeClaveDescripcion, 
------			CodigoEAN_ND, CodigoEAN, CodigoEAN_Base, Descripcion_ClaveSSA, 
------			0 as Existencia, 0 as CajasCompletas, 
------			ClaveLote, CodigoRelacionado, 
------			Caducidad, 
------			convert(varchar(10), @FechaDeProceso, 120) as FechaGeneracion,  
------			convert(varchar(10), getdate(), 120) as FechaEjecucion, 
------			getdate() as FechaHoraEjecucion 
------		Into #tmpExistencia__DiasAnteriores 	
------		From INT_ND__PRCS_Existencias H (NoLock) 
------		Where Not Exists 
------		( 
------			Select * 
------			From #tmpExistencia E (NoLock) 
------			Where 
------				H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia 
------				and H.CodigoEAN = E.CodigoEAN and H.ClaveLote = E.ClaveLote 
------		) 
------		Group by CodigoCliente, IdEstado, IdFarmacia, Farmacia, Modulo, 
------			ClaveSSA_Base, ClaveSSA, ClaveSSA_ND, ClaveSSA_ND_Aux, DescripcionClave, DescripcionComercial, 
------			TipoDeClave, TipoDeClaveDescripcion, 
------			CodigoEAN_ND, CodigoEAN, CodigoEAN_Base, Descripcion_ClaveSSA, ClaveLote, CodigoRelacionado, 
------			Caducidad 
			
			
------		Update A Set Existencia = H.Existencia, CajasCompletas = H. CajasCompletas 
------		From #tmpExistencia__DiasAnteriores A (NoLock) 
------		Inner Join INT_ND__PRCS_Existencias H (NoLock) On ( A.Keyx_Anterior = H.Keyx ) 
		
------		-- select * from #tmpExistencia__DiasAnteriores 
			
	
		
------		--------------- Agregar la informacion restante 
------		Insert Into INT_ND__PRCS_Existencias  
------		( 
------			GUID, MovtoDia, CodigoCliente, IdEstado, IdFarmacia, Farmacia, Modulo, ClaveSSA_Base, ClaveSSA, ClaveSSA_ND, ClaveSSA_ND_Aux, DescripcionClave, DescripcionComercial, 
------			TipoDeClave, TipoDeClaveDescripcion, 
------			CodigoEAN_ND, CodigoEAN, CodigoEAN_Base, Descripcion_ClaveSSA, Existencia, CajasCompletas, ClaveLote, CodigoRelacionado, 
------			Caducidad, FechaGeneracion, FechaEjecucion, FechaHoraEjecucion		
------		) 
------		Select 
------			distinct 
------			@GUID as GUID, 0 as MovtoDia, CodigoCliente, IdEstado, IdFarmacia, Farmacia, Modulo, ClaveSSA_Base, ClaveSSA, ClaveSSA_ND, ClaveSSA_ND_Aux, DescripcionClave, DescripcionComercial, 
------			TipoDeClave, TipoDeClaveDescripcion, 
------			CodigoEAN_ND, CodigoEAN, CodigoEAN_Base, Descripcion_ClaveSSA, Existencia, CajasCompletas, ClaveLote, CodigoRelacionado, 
------			Caducidad, 
------			convert(varchar(10), @FechaDeProceso, 120) as FechaGeneracion,  
------			convert(varchar(10), getdate(), 120) as FechaEjecucion, 
------			getdate() as FechaHoraEjecucion 
------		From #tmpExistencia__DiasAnteriores H (NoLock) 
------		Where Not Exists 
------		( 
------			Select * 
------			From  INT_ND__PRCS_Existencias E (NoLock) 
------			Where 
------				H.IdEstado = E.IdEstado and H.IdFarmacia = E.IdFarmacia 
------				and H.CodigoEAN = E.CodigoEAN and H.ClaveLote = E.ClaveLote 
------				and H.FechaGeneracion = E.FechaGeneracion
------		) 		
		
	
	End 
-------------------------- Insertar los Productos que no se movieron el dia solicitado 

	
--------------------------------------------------- CONCENTRAR INFORMACION    
	 
--	select * from #tmpExistencia 
	
	
----------------------------------- SALIDA FINAL 	
	If @MostrarResultado = 1 
	Begin 
		Select 
			F.MovtoDia,  
			C.CodigoCliente, '01' as Modulo, 
			F.ClaveSSA_Base, F.ClaveSSA, 
			F.ClaveSSA_ND, 
			F.DescripcionClave, F.DescripcionComercial, 
			F.CodigoEAN_ND as CodigoEAN, 
			-- F.CodigoEAN, 
			F.CodigoEAN_Base, cast(F.CajasCompletas as int) as Existencia, F.ClaveLote, F.CodigoRelacionado, 
			replace(convert(varchar(10), F.FechaCaducidad, 120), '-', '') as Caducidad,  
			-- replace(convert(varchar(10), getdate(), 120), '-', '') as FechaGeneracion  	
			replace(convert(varchar(10), @FechaDeProceso, 120), '-', '') as FechaGeneracion 		
		From #tmpExistencia F (NoLock) -- On ( CB.IdEstado = F.IdEstado and CB.IdFarmacia = F.IdFarmacia and CB.ClaveSSA = F.ClaveSSA )  
		left Join #tmpClientes C (NoLock) On ( F.IdEstado = C.IdEstado and F.IdFarmacia = C.IdFarmacia ) 
		---Where F.ClaveSSA_ND = '' 
		Order by F.IdEstado, F.IdFarmacia, F.DescripcionClave 	
	End  
----------------------------------- SALIDA FINAL 	
	
	
---		spp_INT_ND_GenerarExistencias__001_Ejecucion  	

	
End  
Go--#SQL 

