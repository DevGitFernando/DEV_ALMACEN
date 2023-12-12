----------------------------------------------------------------------------------------------------------------------------------------------------------------  
----------------------------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_CFG_GET_CB_CuadroBasico_Claves_Programacion' and xType = 'P' ) 
	Drop Proc spp_CFG_GET_CB_CuadroBasico_Claves_Programacion
Go--#SQL 

Create Proc spp_CFG_GET_CB_CuadroBasico_Claves_Programacion 
( 
	@IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '24', 
	@IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '6', 
	@Año int = 2016, @Mes int = 12, @ClaveSSA varchar(100) = '4158'  
) 
With Encryption 
As 
Begin 
Set NoCount On 
Declare 
	@sFiltro varchar(max), 
	@sSQL varchar(max) 


	Set @sSQL = '' 
	Set @sFiltro = '' 
	Set @IdEstado = right('00000' + @IdEstado, 2) 
	Set @IdFarmacia = right('00000' + @IdFarmacia, 4) 
	Set @IdCliente = right('00000' + @IdCliente, 4) 
	Set @IdSubCliente = right('00000' + @IdSubCliente, 4) 



	Select  IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, 0 as Ampliacion 	
	Into #tmp__CFG_CB_CuadroBasico_Claves_Programacion 
	From CFG_CB_CuadroBasico_Claves_Programacion (NoLock) -- ( Programación normal ) 
	Where 1 = 0  



	Insert Into #tmp__CFG_CB_CuadroBasico_Claves_Programacion ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, Ampliacion )  
	Select IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, 0 as Ampliacion
	From CFG_CB_CuadroBasico_Claves_Programacion (NoLock) -- ( Programación normal ) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
		  And Año = @Año and Mes = @Mes 
	Order by FechaRegistro 


	Insert Into #tmp__CFG_CB_CuadroBasico_Claves_Programacion ( IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, Ampliacion )  
	Select IdEstado, IdFarmacia, IdCliente, IdSubCliente, Año, Mes, IdClaveSSA, Cantidad, FechaRegistro, Status, 1 as Ampliacion
	From CFG_CB_CuadroBasico_Claves_Programacion_Excepciones (NoLock) -- ( Ampliaciones ) 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 
		  And Año = @Año and Mes = @Mes 
	Order by FechaRegistro 



------------------	SALIDA FINAL 
	Select 
		C.IdEstado, C.IdFarmacia, C.IdCliente, C.IdSubCliente, C.Año, C.Mes, 
		C.IdClaveSSA, C.Cantidad, 
		P.ClaveSSA, P.Descripcion, 
		C.FechaRegistro, C.Status, C.Ampliacion 	
	From #tmp__CFG_CB_CuadroBasico_Claves_Programacion C (NoLock) 
	Inner Join CatClavesSSA_Sales P (NoLock) On ( C.IdClaveSSA = P.IdClaveSSA_Sal ) 
	Where ClaveSSA like '%' + @ClaveSSA + '%' 
	Order By P.Descripcion, C.Ampliacion 


End 
Go--#SQL 
