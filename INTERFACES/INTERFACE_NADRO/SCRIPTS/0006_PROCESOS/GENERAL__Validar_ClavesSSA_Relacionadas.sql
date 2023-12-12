Set DateFormat YMD 
Go--#SQL 

Set NoCount Off  
Go--#SQL 

	
Declare 
	@IdEmpresa varchar(3), 
	@IdEstado varchar(2), 
	@IdFarmacia varchar(4),   
	@sSql varchar(max), 
	@bCrearTablaBase int,   
	@bForzar_CrearTablaBase int  		
		
	Set @IdEmpresa = '003'
	Set @IdEstado = '07' 
	Set @IdFarmacia = '0011'

	Set @sSql = '' 
	Set @bCrearTablaBase = 1 
	Set @bForzar_CrearTablaBase = 1 
	

---		truncate table INT_ND__PRCS_Existencias 

--------------------------------------------------------------------------------------------------------------------------- 		
------------------------------------- Validar la creacion de la tabla base del proceso de existencias 
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'tmp_INT_PRCS_Validar_ClavesSSA' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'tmp_INT_PRCS_Validar_ClavesSSA' and xType = 'U' and datediff(dd, crDate, getdate()) > 1
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 
	End 

	If @bForzar_CrearTablaBase = 1 
	   Set @bCrearTablaBase = 1 
	
		
	-- print @bCrearTablaBase 
	If @bCrearTablaBase = 1 
	Begin 	
		If exists ( select * from sysobjects where name like 'tmp_INT_PRCS_Validar_ClavesSSA' and xType = 'U' ) 
		   drop table tmp_INT_PRCS_Validar_ClavesSSA  
		   
		Select 
			0 as Procesado, 
			0 as ExisteEnCV,   
			-- IdFarmacia, '' as Farmacia, 
			IdClaveSSA_Sal, ClaveSSA, ClaveSSA_Base, 
			ClaveSSA as ClaveSSA_ND, 
			DescripcionClave as Descripcion_ClaveSSA, 
			DescripcionClave as DescripcionClave_ND, 
			TipoDeClave, TipoDeClaveDescripcion 
		into tmp_INT_PRCS_Validar_ClavesSSA	
		From VentasDet_Lotes F (NoLock) 
		Inner Join vw_Productos_CodigoEAN P (NoLock) On ( F.IdProducto = P.IdProducto and F.CodigoEAN = P.CodigoEAN ) 
		Where 
			F.IdEmpresa = @IdEmpresa and F.IdEstado = @IdEstado 
			and Not Exists 
				( 
					Select * 
					From INT_ND_SubFarmaciasConsigna C (NoLock) 
					Where F.IdEstado = C.IdEstado and F.IdSubFarmacia = C.IdSubFarmacia  		
				)	
		Group by 
			--IdFarmacia, 
			IdClaveSSA_Sal, ClaveSSA, ClaveSSA_Base, DescripcionClave, TipoDeClave, TipoDeClaveDescripcion  	
	End 
			


-----------------------------------------------------------------------------------------------------------------	
	Update E Set Procesado = 0, ExisteEnCV = 0, ClaveSSA_ND = '', DescripcionClave_ND = ''   
	From tmp_INT_PRCS_Validar_ClavesSSA E 
		
	Update H Set ClaveSSA_ND = E.ClaveSSA_ND, Procesado = H.Procesado + 1  
	From tmp_INT_PRCS_Validar_ClavesSSA H 
	Inner Join INT_ND_CFG_ClavesSSA E (NoLock) On ( H.ClaveSSA = E.ClaveSSA ) 
	
	Update H Set DescripcionClave_ND = E.Descripcion, Procesado = H.Procesado + 1   
	From tmp_INT_PRCS_Validar_ClavesSSA H 
	Inner Join INT_ND_Claves E (NoLock) On ( H.ClaveSSA_ND = E.ClaveSSA_ND ) 		

	Update H Set ExisteEnCV = 1   
	From tmp_INT_PRCS_Validar_ClavesSSA H 
	Inner Join INT_ND_CFG_CB_CuadrosBasicos E (NoLock) On ( H.ClaveSSA_ND = E.ClaveSSA_ND ) 



-------------------------------- RESULTADOS 	
	Select * 
	From tmp_INT_PRCS_Validar_ClavesSSA (NoLock) 
	Where Procesado = 0 
	Order by Descripcion_ClaveSSA 
	
	

	Select * 
	From tmp_INT_PRCS_Validar_ClavesSSA (NoLock) 
	Where ExisteEnCV = 0 
	Order by Procesado, Descripcion_ClaveSSA 
	
		