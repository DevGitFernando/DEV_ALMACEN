------------------------------------------------------------------------------------------------------------------ 
------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_CFG_CB_CuadroBasico_Claves_Programacion__ValidarDatosDeEntrada' and xType = 'P' ) 
	Drop Proc spp_CFG_CB_CuadroBasico_Claves_Programacion__ValidarDatosDeEntrada
Go--#SQL 

Create Proc spp_CFG_CB_CuadroBasico_Claves_Programacion__ValidarDatosDeEntrada   
( 
	@IdEstado varchar(2) = '13', 
	@IdCliente varchar(4) = '0002', @IdSubCliente varchar(4) = '0010', 
	@IdEstado_Registra varchar(2) = '25', @IdFarmacia_Registra varchar(4) = '0001', @IdPersonal_Registra varchar(4) = '0001' 
) 
With Encryption   
As 
Begin 
Set NoCount On 
Declare 
	@sSql varchar(max), 
	@sFiltro varchar(200), 
	@sCliente varchar(500), 
	@sSubCliente varchar(500) 
	
Declare 
	@IdExcepcion int,  
	@dFecha datetime, 
	@Año int, 
	@Mes int  


	Set @Año = 0 
	Set @Mes = 0 
	Set @IdExcepcion = 0 

	
	Set @IdEstado = right('00' +  @IdEstado, 2) 
	Set @IdCliente = right('000000' +  @IdCliente, 4) 
	Set @IdSubCliente = right('00000' +  @IdSubCliente, 4) 		

	Set @IdEstado_Registra = right('00' +  @IdEstado_Registra, 2) 
	Set @IdFarmacia_Registra = right('000000' +  @IdFarmacia_Registra, 4) 
	Set @IdPersonal_Registra = right('00000' +  @IdPersonal_Registra, 4) 		


---------------------------------- Preparar informacion de Programcion-Excepciones   
	Select top 1 @Año = Año, @Mes = Mes 
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva (NoLock) 
	
	Select @IdExcepcion = max(IdExcepcion) + 1 
	From CFG_CB_CuadroBasico_Claves_Programacion_Excepciones 
	Where IdCliente = @IdCliente and IdSubCliente = @IdSubCliente and Año = @Año and Mes = @Mes 


	Set @IdExcepcion = IsNull(@IdExcepcion, 1) 	
---------------------------------- Preparar informacion de Programcion-Excepciones   


---------------------------------- Preparar informacion 
	Select  @sCliente = NombreCliente, @sSubCliente = NombreSubCliente 
	From vw_Clientes_SubClientes C(NoLock) 
	Where IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 

	Update C Set 
		IdEstado = right('0000' + @IdEstado, 2), IdFarmacia = right('0000' + IdFarmacia, 4), Farmacia = '', 
		IdCliente = @IdCliente, IdSubCliente = @IdSubCliente, 
		Cliente = @sCliente, SubCliente = @sSubCliente, 
		IdEstadoRegistra = @IdEstado_Registra, IdFarmaciaRegistra = @IdFarmacia_Registra, IdPersonalRegistra =  @IdPersonal_Registra
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva  C (NoLock) 

	
	Update C Set ClaveSSA = replace(replace(replace(replace(replace(ltrim(rtrim(ClaveSSA)), ' ', ''), char(39), ''), char(13), ''), char(10), ''), char(160), '')     
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva  C (NoLock) 


	Update C Set ClaveValida = 1, IdClaveSSA = P.IdClaveSSA_Sal, DescripcionClaveSSA = P.Descripcion  
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva  C (NoLock) 
	Inner Join CatClavesSSA_Sales P (NoLock) On ( C.ClaveSSA = P.ClaveSSA )  


	Update C Set EsAmpliacion = 1  
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva  C (NoLock) 
	Inner Join CFG_CB_CuadroBasico_Claves_Programacion E (NoLock) 
		On ( C.IdEstado = E.IdEstado and C.IdFarmacia = E.IdFarmacia and C.IdClaveSSA = E.IdClaveSSA and C.Año = E.Año and C.Mes = E.Mes ) 


	Update C Set Farmacia = F.NombreFarmacia 
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva  C (NoLock) 
	Inner Join CatFarmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia )
---------------------------------- Preparar informacion 


---------------------------------- Preparar informacion de Programacion 
	Update C Set IdExcepcion = IsNull(( Select top 1 0
		From CFG_CB_CuadroBasico_Claves_Programacion E (NoLock) 
		Where C.IdEstado = E.IdEstado and C.IdFarmacia = E.IdFarmacia And C.IdCliente = E.IdCliente and C.IdSubCliente = E.IdSubCliente 
			  and C.IdClaveSSA = E.IdClaveSSA and C.Año = E.Año and C.Mes = E.Mes  ), -1) 
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva  C (NoLock) 


	Update C Set IdExcepcion = @IdExcepcion
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva  C (NoLock) 
	Where IdExcepcion = 0 
---------------------------------- Preparar informacion de Programacion 


---		spp_CFG_CB_CuadroBasico_Claves_Programacion__ValidarDatosDeEntrada 


---------------------------------- Validaciones 
	Select IdFarmacia, Farmacia, 'Clave SSA' = ClaveSSA, 'Descripción Clave' = DescripcionClaveSSA, Cantidad  
	Into #tmpClavesNoRegistradas 
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva 
	Where ClaveValida = 0 


	Select distinct IdFarmacia 
	Into #tmpFarmaciasNoRegistradas 
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva 
	Where Farmacia = ''  
	Order by IdFarmacia 


	Select IdFarmacia, Año, Mes, sum(Cantidad) as Cantidad, count(*) as Repeticiones, 'Clave SSA' = ClaveSSA, 'Descripción Clave' = DescripcionClaveSSA 
	Into #tmpClavesDuplicadas  
	From CFG_CB_CuadroBasico_Claves_Programacion___CargaMasiva 
	Group by IdFarmacia, Año, Mes, ClaveSSA, DescripcionClaveSSA  
	Having count(*) >= 2 
	Order by IdFarmacia, Año, Mes, ClaveSSA 



------------------------- SALIDA FINAL	
	Select top 0 identity(int, 2, 1) as Orden, cast('' as varchar(200)) as NombreTabla Into #tmpResultados 

	Insert Into #tmpResultados ( NombreTabla ) select 'Claves no registradas' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Farmacias no registradas' 
	Insert Into #tmpResultados ( NombreTabla ) select 'Claves duplicadas' 
--	Insert Into #tmpResultados ( NombreTabla ) select 'Error en caducidades' 
--	Insert Into #tmpResultados ( NombreTabla ) select 'EAN multiples caducidades' 
--	Insert Into #tmpResultados ( NombreTabla ) select 'EAN no registrados' 
--	Insert Into #tmpResultados ( NombreTabla ) select 'EAN sin costo' 
--	Insert Into #tmpResultados ( NombreTabla ) select 'EAN multiples costos' 
--	Insert Into #tmpResultados ( NombreTabla ) select 'SubInventarios incorrectos' 	
	            	
	
	Select * From #tmpResultados 	
	Select * From #tmpClavesNoRegistradas 
	Select * From #tmpFarmaciasNoRegistradas  
	Select * From #tmpClavesDuplicadas 

--	Select * From #tmpLotes_Error_Caducidades Where EnInventario = 1  	
--	Select * From #tmpLotes_Multiples_Caducidades Where EnInventario = 1  	-- and 1 = 0 
--	Order By CodigoEAN, ClaveLote, Caducidad 
	
--	Select * From #tmpEAN_No_Encontrados   
 
--	Select * From #tmpProductoSinPrecio 
--	Select * From #tmpLotes_Multiples_Costos Where EnInventario = 1  
--	Order By CodigoEAN, ClaveLote, Costo  
	
--	Select * From #tmpSubFarmaciasIncorrecta  	
----------------------- SALIDA FINAL	


End 
Go--#SQL 
