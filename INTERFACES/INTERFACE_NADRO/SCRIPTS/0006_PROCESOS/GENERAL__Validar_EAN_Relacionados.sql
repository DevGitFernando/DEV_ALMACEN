Set DateFormat YMD 
Go--#SQL 

Set NoCount Off  
Go--#SQL 

	
Declare 
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4),   
	@sSql varchar(max), 
	@bCrearTablaBase int 
		
	Set @IdEmpresa = '003'
	Set @IdEstado = '07' 
	Set @IdFarmacia = '0011'

	Set @sSql = '' 
	Set @bCrearTablaBase = 1 

---		truncate table INT_ND__PRCS_Existencias 

--------------------------------------------------------------------------------------------------------------------------- 		
------------------------------------- Validar la creacion de la tabla base del proceso de existencias 
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'tmp_INT_PRCS_Validar_CodigoEAN' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'tmp_INT_PRCS_Validar_CodigoEAN' and xType = 'U' and datediff(dd, crDate, getdate()) > 1
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 
	End 
		
	-- print @bCrearTablaBase 
	If @bCrearTablaBase = 1 
	Begin 	
		If exists ( select * from sysobjects where name like 'tmp_INT_PRCS_Validar_CodigoEAN' and xType = 'U' ) 
		   drop table tmp_INT_PRCS_Validar_CodigoEAN  
		   
		Select 
			0 as Procesado, 
			IdEmpresa, Empresa, 
			IdEstado, Estado, IdFarmacia, Farmacia, IdSubFarmacia, SubFarmacia, 
			IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, 
			space(20) as ClaveSSA_ND, 
			DescripcionClave as Descripcion_ClaveSSA, 
			space(7000) as DescripcionClave_ND, 		
			space(7000) as DescripcionComercial, 
			TipoDeClave, TipoDeClaveDescripcion, 
			IdProducto, 
			space(30) as CodigoEAN_ND, 
			CodigoEAN, 
			CodigoEAN as CodigoEAN_Base, 
			Laboratorio, 
			DescripcionProducto, ContenidoPaquete, ClaveLote, 

			cast(0 as int) as PiezasTotales, 
			floor((0 / (ContenidoPaquete*1.0))) as CajasCompletas,  
			(0 - (floor((0 / (ContenidoPaquete*1.0))) * ContenidoPaquete)) as PiezasSueltas,  
			
			FechaCaducidad, MesesParaCaducar, 
			0 as MovtoDia,
			0 as CodigoRelacionado 
		into tmp_INT_PRCS_Validar_CodigoEAN	
		From vw_ExistenciaPorCodigoEAN_Lotes F (NoLock) 
		Where 
			-- IdProducto = '00001405' and 
			F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado 
			-- and F.IdFarmacia = @IdFarmacia 
			----and Not Exists 
			----	( 
			----		Select * 
			----		From INT_ND_SubFarmaciasConsigna C (NoLock) 
			----		Where F.IdEstado = C.IdEstado and F.IdSubFarmacia = C.IdSubFarmacia  		
			----	) 	
			-- and CodigoEAN = '7503007782038' 
			and Not Exists 
				( 
					Select * 
					From INT_ND_SubFarmaciasConsigna C (NoLock) 
					Where F.IdEstado = C.IdEstado and F.IdSubFarmacia = C.IdSubFarmacia  		
				)			
	End 
			
			
			
-----------------------------------------------------------------------------------------------------------------	
	Update E Set Procesado = 0 
	From tmp_INT_PRCS_Validar_CodigoEAN E 
	
	Update E Set CodigoEAN_ND = H.CodigoEAN_ND, CodigoRelacionado = 1, Procesado = E.Procesado + 1  
	From tmp_INT_PRCS_Validar_CodigoEAN E 
	Inner Join INT_ND_Productos H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) ) 


	Update E Set CodigoEAN_ND = H.CodigoEAN_ND, CodigoRelacionado = 1, Procesado = E.Procesado + 1   
	From tmp_INT_PRCS_Validar_CodigoEAN E 
	Inner Join INT_ND_CFG_CodigosEAN H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN, 30) = right(replicate('0', 30) + H.CodigoEAN, 30) )   	
	
	
	Update E Set ClaveSSA_ND = H.ClaveSSA_ND, CodigoEAN_ND = H.CodigoEAN_ND, DescripcionComercial = H.Descripcion, 
		Procesado = E.Procesado -- + 1   
	From tmp_INT_PRCS_Validar_CodigoEAN E 
	Inner Join INT_ND_Productos H (NoLock) 
		On ( right(replicate('0', 30) + E.CodigoEAN_ND, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) ) 


	
	Update E Set DescripcionClave_ND = L.Descripcion, Procesado = E.Procesado + 1 
	From tmp_INT_PRCS_Validar_CodigoEAN E 
	inner Join INT_ND_Claves L (NoLock) On ( E.ClaveSSA_ND = L.ClaveSSA_ND ) 		

	
		
	------Update E Set DescripcionComercial = H.Descripcion, Procesado = E.Procesado + 1 
	------From tmp_INT_PRCS_Validar_CodigoEAN E 
	------Inner Join INT_ND_Productos H (NoLock) 
	------	-- On ( E.CodigoEAN_ND = H.CodigoEAN_ND )  		
	------	On ( right(replicate('0', 30) + E.CodigoEAN_ND, 30) = right(replicate('0', 30) + H.CodigoEAN_ND, 30) )
		
	 	 

--------------------------------- Consultas  
	Select ClaveSSA, Descripcion_ClaveSSA, ClaveSSA_ND, DescripcionClave_ND, CodigoEAN, CodigoEAN_ND, TipoDeClaveDescripcion  
	from tmp_INT_PRCS_Validar_CodigoEAN 
	where CodigoEAN_ND = '' or DescripcionClave_ND = '' or DescripcionComercial = '' or CodigoEAN_ND = '' 
	group by ClaveSSA, Descripcion_ClaveSSA, ClaveSSA_ND, DescripcionClave_ND, CodigoEAN, CodigoEAN_ND, TipoDeClaveDescripcion  
	order by Descripcion_ClaveSSA, TipoDeClaveDescripcion 


	Select count(*) as Registros, CodigoEAN_ND, CodigoEAN, ClaveSSA, Descripcion_ClaveSSA, TipoDeClaveDescripcion    
	from tmp_INT_PRCS_Validar_CodigoEAN 
	where CodigoEAN_ND = '' or DescripcionClave_ND = '' or DescripcionComercial = '' or CodigoEAN_ND = '' 
	group by CodigoEAN_ND, CodigoEAN, ClaveSSA, Descripcion_ClaveSSA, TipoDeClaveDescripcion   
	order by Descripcion_ClaveSSA, TipoDeClaveDescripcion 
	  

	Select count(*) as Registros, CodigoEAN, CodigoEAN_ND, ClaveSSA, Descripcion_ClaveSSA, Laboratorio, TipoDeClaveDescripcion    
	from tmp_INT_PRCS_Validar_CodigoEAN 
	where CodigoEAN_ND = '' -- or DescripcionClave_ND = '' or DescripcionComercial = '' or CodigoEAN_ND = '' 
	group by CodigoEAN_ND, CodigoEAN, ClaveSSA, Descripcion_ClaveSSA, Laboratorio, TipoDeClaveDescripcion   
	order by Descripcion_ClaveSSA, TipoDeClaveDescripcion 




/* 
	Select 
		count(*) as Registros, CodigoEAN, CodigoEAN_ND, 
		ClaveSSA, 
		
		(case when TipoDeClave = 2 then 
		ClaveSSA_BAse  
		else 
		replace(ClaveSSA, '.', '') 
		end) as ClaveSSA__FormatoND, 
		Descripcion_ClaveSSA, Laboratorio, TipoDeClave, TipoDeClaveDescripcion    
	from tmp_INT_PRCS_Validar_CodigoEAN 
	where CodigoEAN_ND = '' -- or DescripcionClave_ND = '' or DescripcionComercial = '' or CodigoEAN_ND = '' 
		-- and TipoDeClave = 2  
	group by CodigoEAN_ND, CodigoEAN, ClaveSSA, ClaveSSA_Base, Descripcion_ClaveSSA, Laboratorio, TipoDeClave, TipoDeClaveDescripcion   
	order by TipoDeClaveDescripcion, Descripcion_ClaveSSA  
*/ 	
	
/* 

	Select * 
	from tmp_INT_PRCS_Validar_CodigoEAN 
	where CodigoEAN_ND = '' or DescripcionClave = '' or DescripcionComercial = '' or CodigoEAN_ND = '' 

*/ 	
	
