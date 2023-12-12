
------------------------------------------------------------------------------------------------------------------------------- 
	Select 'Drop Table ' + name as DeleteColum, 'select count(*) as ' + name + ' from  ' + name + ' (nolock) ' as CountColumn ,   *  
	From sysobjects (NoLock) 
	Where Name in 
	( 
		'vw_ClavesSSA___PRCS',  
		'vw_Productos_CodigoEAN__PRCS',  
		'vw_Productos_CodigoEAN__ANTIBIOTICOS_PRCS',  
		'vw_Productos_CodigoEAN__CONTROLADOS_PRCS',  
		'vw_CB_CuadroBasico_Claves__PRCS', 
		'vw_Claves_Precios_Asignados__PRCS', 
		'vw_Beneficiarios__PRCS',
		'vw_Farmacias__PRCS'
	) 	
	and xType = 'U' 
Go--#SQL 

--		Exec spp_BI_RPT__000__Preparar_Catalogos @ForzarActualizacion = 1 

---		Exec spp_BI_RPT__000__Preparar_Catalogos @IdEmpresa = '001', @IdEstado = '13', @IdCliente = '2', @IdSubCliente = '10', @ForzarActualizacion = 1 

------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_BI_RPT__000__Preparar_Catalogos' and xType = 'P' ) 
   Drop Proc spp_BI_RPT__000__Preparar_Catalogos 
Go--#SQL 

Create Proc spp_BI_RPT__000__Preparar_Catalogos 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@ForzarActualizacion int = 0   
) 
As 
Begin 
Set DateFormat YMD 
Set NoCount On 

Declare 
	@sSql varchar(max) 

	Set @sSql = '' 

Declare 
	@bCrearTablaBase int 

	Set @bCrearTablaBase = 1   

	------------------------ Tomar los parámetros configurados 
	Select Top 1 @IdEmpresa = IdEmpresa, @IdEstado = IdEstado, @IdCliente = IdCliente, @IdSubCliente = IdSubCliente 
	From BI_RPT__DTS__Configuracion_Operacion (NoLock) 


	Set @IdEmpresa = right('00000000' + @IdEmpresa, 3 ) 
	Set @IdEstado = right('00000000' + @IdEstado, 2 ) 
	Set @IdCliente = right('00000000' + @IdCliente, 4 ) 
	Set @IdSubCliente = right('00000000' + @IdSubCliente, 4 ) 
			
			

	------------------------------------------ Generar tablas de catalogos     
	---- vw_ClavesSSA___PRCS			
	---- vw_Productos_CodigoEAN__PRCS 
	---- vw_Productos_CodigoEAN__ANTIBIOTICOS_PRCS 
	---- vw_Productos_CodigoEAN__CONTROLADOS_PRCS  
	---- vw_CB_CuadroBasico_Claves__PRCS 
	---- vw_Claves_Precios_Asignados__PRCS  
	---- vw_Beneficiarios__PRCS  

	If @ForzarActualizacion = 1 
	Begin 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_ClavesSSA___PRCS' and xType = 'U' ) Drop Table vw_ClavesSSA___PRCS 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__PRCS' and xType = 'U' ) Drop Table vw_Productos_CodigoEAN__PRCS 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__ANTIBIOTICOS_PRCS' and xType = 'U' ) Drop Table vw_Productos_CodigoEAN__ANTIBIOTICOS_PRCS 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__CONTROLADOS_PRCS' and xType = 'U' ) Drop Table vw_Productos_CodigoEAN__CONTROLADOS_PRCS 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_CB_CuadroBasico_Claves__PRCS' and xType = 'U' ) Drop Table vw_CB_CuadroBasico_Claves__PRCS 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Claves_Precios_Asignados__PRCS' and xType = 'U' ) Drop Table vw_Claves_Precios_Asignados__PRCS 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Beneficiarios__PRCS' and xType = 'U' ) Drop Table vw_Beneficiarios__PRCS
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Farmacias__PRCS' and xType = 'U' ) Drop Table vw_Beneficiarios__PRCS 
	End 


	------------------------------------------ Generar tablas de catalogos     
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_ClavesSSA___PRCS' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'vw_ClavesSSA___PRCS' and xType = 'U' and datediff(dd, crDate, getdate()) > 3 
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 	
	End 
	
	
	
	
	If @bCrearTablaBase = 1 
	Begin 
	
		------------------------------------------------------------------------------------------------------------------------------------ 	
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_ClavesSSA___PRCS' and xType = 'U' ) 
		   Drop Table vw_ClavesSSA___PRCS 
		   		   
		Select * 
		Into vw_ClavesSSA___PRCS  
		From vw_ClavesSSA_Sales 
		
			
		------------------------------------------------------------------------------------------------------------------------------------ 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__PRCS' and xType = 'U' ) 
		   Drop Table vw_Productos_CodigoEAN__PRCS 
		   		   
		Select * 
		Into vw_Productos_CodigoEAN__PRCS  
		From vw_Productos_CodigoEAN 
			

		------------------------------------------------------------------------------------------------------------------------------------ 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__ANTIBIOTICOS_PRCS' and xType = 'U' ) 
		   Drop Table vw_Productos_CodigoEAN__ANTIBIOTICOS_PRCS 
		   		   
		Select * 
		Into vw_Productos_CodigoEAN__ANTIBIOTICOS_PRCS  
		From vw_Productos_CodigoEAN 
		Where EsAntibiotico = 1  


		------------------------------------------------------------------------------------------------------------------------------------ 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__CONTROLADOS_PRCS' and xType = 'U' ) 
		   Drop Table vw_Productos_CodigoEAN__CONTROLADOS_PRCS 
		   		   
		Select * 
		Into vw_Productos_CodigoEAN__CONTROLADOS_PRCS  
		From vw_Productos_CodigoEAN 
		Where EsControlado = 1  
		
		

		------------------------------------------------------------------------------------------------------------------------------------ 			
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_CB_CuadroBasico_Claves__PRCS' and xType = 'U' ) 
		   Drop Table vw_CB_CuadroBasico_Claves__PRCS 

		Select 
			IdEstado, Estado, IdCliente, Cliente, IdClaveSSA, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionClave, DescripcionCortaClave, 
			1 as EsDeCuadroBasico, 
			EsAntibiotico, EsControlado, IdTipoDeClave, TipoDeClaveDescripcion, IdPresentacion, Presentacion, StatusNivel, StatusMiembro, StatusClave 
		Into vw_CB_CuadroBasico_Claves__PRCS 
		From vw_CB_CuadroBasico_Claves 
		Where IdEstado = @IdEstado and StatusClave = 'A' and StatusNivel = 'A' and StatusMiembro = 'A'  
		Group by 
			IdEstado, Estado, IdCliente, Cliente, IdClaveSSA, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionClave, DescripcionCortaClave, 
			EsAntibiotico, EsControlado, IdTipoDeClave, TipoDeClaveDescripcion, IdPresentacion, Presentacion, StatusNivel, StatusMiembro, StatusClave  
		
		

		Insert Into vw_CB_CuadroBasico_Claves__PRCS 
		( 
			IdEstado, Estado, IdCliente, Cliente, IdClaveSSA, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionClave, DescripcionCortaClave, 
			EsDeCuadroBasico, 
			EsAntibiotico, EsControlado, IdTipoDeClave, TipoDeClaveDescripcion, IdPresentacion, Presentacion, StatusNivel, StatusMiembro, StatusClave )
		Select 
			@IdEstado as IdEstado, '' as Estado, @IdCliente as IdCliente, '' as Cliente, IdClaveSSA_Sal as IdClaveSSA, 
			ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionClave, DescripcionCortaClave, 
			0 as EsDeCuadroBasico, 
			EsAntibiotico, EsControlado, 
			TipoDeClave as IdTipoDeClave, TipoDeClaveDescripcion, 
			IdPresentacion, Presentacion, 'A' as StatusNivel, 'A' as StatusMiembro, 'A' as StatusClave 
		From vw_ClavesSSA_Sales C (NoLock) 
		Where Not Exists 
		( 
			Select * 
			From vw_CB_CuadroBasico_Claves__PRCS CB (NoLock)
			Where C.ClaveSSA = CB.ClaveSSA 
		) 



		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Claves_Precios_Asignados__PRCS' and xType = 'U' ) 
		   Drop Table vw_Claves_Precios_Asignados__PRCS 
		   		   
		Select * 
		Into vw_Claves_Precios_Asignados__PRCS 
		From vw_Claves_Precios_Asignados 		
		Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and Status = 'A' 
					
				
		------------------------------------------------------------------------------------------------------------------------------------ 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Beneficiarios__PRCS' and xType = 'U' ) 
		   Drop Table vw_Beneficiarios__PRCS 
		   		   
		Select *, (cast((ApPaterno + ' ' + ApMaterno + ' ' + Nombre) as varchar(500))) as NombreCompleto  
		Into vw_Beneficiarios__PRCS
		From CatBeneficiarios 		
		Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente -- and Status = 'A'
		
		------------------------------------------------------------------------------------------------------------------------------------ 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Farmacias__PRCS' and xType = 'U' ) 
		   Drop Table vw_Farmacias__PRCS 
		   		   
		Select F.*
		Into vw_Farmacias__PRCS
		From vw_Farmacias F
		Inner Join BI_RPT__CatFarmacias_A_Procesar P On (F.IdEstado = P.IdEstado And F.IdFarmacia = P.IdFarmacia)	
		
		
		
			
						
	End
------------------------------------------ Generar tablas de catalogos  





End 
Go--#SQL 


