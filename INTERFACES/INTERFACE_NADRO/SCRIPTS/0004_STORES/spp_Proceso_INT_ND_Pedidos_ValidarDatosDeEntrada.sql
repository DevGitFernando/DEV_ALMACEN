------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_Proceso_INT_ND_Pedidos_ValidarDatosDeEntrada' and xType = 'P') 
    Drop Proc spp_Proceso_INT_ND_Pedidos_ValidarDatosDeEntrada
Go--#SQL 
  
--  Exec spp_Proceso_INT_ND_Pedidos_ValidarDatosDeEntrada '001', '11', '0003', '0001', '0001', 1, '2014-08-20'
  
Create Proc spp_Proceso_INT_ND_Pedidos_ValidarDatosDeEntrada 
(   
    @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0003' 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare 
	@FolioPedido varchar(8), @sMensaje varchar(1000),			
	@sStatus varchar(1), @iActualizado smallint, 
	@bErorres int 

	Set @sMensaje = '' 	
	Set @sStatus = 'A'
	Set @iActualizado = 0
	Set @FolioPedido = '0'	
	Set @bErorres = 0 
	
	Select * Into #tmp_vw_Productos_CodigoEAN From vw_Productos_CodigoEAN (Nolock)
	update INT_ND_Pedidos_CargaMasiva Set CodigoEAN_Existe = 0 

------------------------------------------------------------ Verificar informacion 
--------------------------- Validar Clientes 
	If Exists ( select Name From Sysobjects (NoLock) Where Name = '#tmp__Rpt_INT_ND_Pedidos_CodigoClientes' and xType = 'U' ) 
	   Drop Table #tmp__Rpt_INT_ND_Pedidos_CodigoClientes 

	Select Distinct C.CodigoCliente, C.ReferenciaPedido 
	Into #tmp__Rpt_INT_ND_Pedidos_CodigoClientes 
	From INT_ND_Pedidos_CargaMasiva C (nolock)
	Where Not Exists 
		( Select P.* From INT_ND_Clientes P (nolock) Where P.CodigoCliente  = C.CodigoCliente  )


--------------------------- Validar Codigo EAN 
	If Exists ( select Name From Sysobjects (NoLock) Where Name = '#tmp__Rpt_INT_ND_Pedidos_CodigoEAN' and xType = 'U' ) 
	   Drop Table #tmp__Rpt_INT_ND_Pedidos_CodigoEAN 

	Select Distinct C.CodigoCliente, C.ReferenciaPedido, C.CodigoProducto, C.CodigoEAN 
	Into #tmp__Rpt_INT_ND_Pedidos_CodigoEAN 
	From INT_ND_Pedidos_CargaMasiva C (nolock)
	Where Not Exists 
		( Select P.* From #tmp_vw_Productos_CodigoEAN P (nolock) 
		  Where right(replicate('0', 20) + P.CodigoEAN, 20)  = right(replicate('0', 20) + C.CodigoEAN, 20)  )  
	
	
	Update C Set CodigoEAN_Existe = 1  
	From INT_ND_Pedidos_CargaMasiva C (nolock)
	Where Exists 
		( Select P.* From #tmp_vw_Productos_CodigoEAN P (nolock) 
		  Where right(replicate('0', 20) + P.CodigoEAN, 20)  = right(replicate('0', 20) + C.CodigoEAN, 20)  )  


		  	
--------------------------- Validar Costos 	
	If Exists ( select Name From Sysobjects (NoLock) Where Name = '#tmp__CostoCero' and xType = 'U' ) 
	   Drop Table #tmp__CostoCero  

	Select Distinct C.CodigoCliente, C.ReferenciaPedido, C.CodigoProducto, C.CodigoEAN 
	Into #tmp__CostoCero 
	From INT_ND_Pedidos_CargaMasiva C (nolock)	
	Where Precio <= 0 
------------------------------------------------------------ Verificar informacion 

	
---		select top 1 * From INT_ND_Pedidos_CargaMasiva 	
	
/*
	select *  
	From INT_ND_Pedidos_CargaMasiva C (nolock)	
	Where Precio <= 0  
	
*/ 		
	
---------------------------------------------- Verificar informacion 	


	----Select @bErorres = count(*) From #tmp__Rpt_INT_ND_Pedidos_CodigoEAN (Nolock) 
	----Select @bErorres = @bErorres + count(*) Select * From #tmp__Rpt_INT_ND_Pedidos_CodigoClientes (Nolock) 
	
			
			
	---------------------------------------------------------- RESULTADOS 	
	Select * From #tmp__Rpt_INT_ND_Pedidos_CodigoClientes (Nolock)
	Select * From #tmp__Rpt_INT_ND_Pedidos_CodigoEAN (Nolock)   
	Select * From #tmp__CostoCero (NoLock) 
	---------------------------------------------------------- RESULTADOS 		
	
----	spp_Proceso_INT_ND_Pedidos_ValidarDatosDeEntrada  	
	
End
Go--#SQL 