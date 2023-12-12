

----------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (nolock) Where Name = 'spp_Rpt_Administrativos_009' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_009 
Go--#SQL 

---   Exec spp_Rpt_Administrativos_009 @IdEmpresa = '001', @IdEstado = '11', @IdCliente = '2', @IdSubCliente = '6', @ForzarActualizacion = 1 
 
Create Proc spp_Rpt_Administrativos_009 
( 
	@IdEmpresa varchar(3) = '1', @IdEstado varchar(2) = '21', 
	@IdCliente varchar(4) = '2', @IdSubCliente varchar(4) = '5', @ForzarActualizacion int = 1    
)  
With Encryption 
As 
Begin 
Set NoCount On  
Set DateFormat YMD 
Declare 
	@bCrearTablaBase int, 
	@iDias_EAN int, 
	@iDias_CIE10 int, 
	@iDias_Servicios int, 
	@iHoras_Precios int 


	Set @IdEmpresa = right('0000' + @IdEmpresa, 3) 
	Set @IdEstado = right('0000' + @IdEstado, 2) 
	Set @IdCliente = right('0000' + @IdCliente, 4) 
	Set @IdSubCliente = right('0000' + @IdSubCliente, 4) 		

	Set @iDias_EAN = 1 
	Set @iDias_CIE10 = 10 
	Set @iDias_Servicios = 3 
	Set @iHoras_Precios = 2 


--	Select @IdEmpresa, @IdEstado, @IdCliente, @IdSubCliente 


	If @ForzarActualizacion = 1 
	Begin 
		
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Claves_Precios_Asignados__PRCS_VLDCN' and xType = 'U' ) Drop Table vw_Claves_Precios_Asignados__PRCS_VLDCN  
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__PRCS_VLDCN' and xType = 'U' ) Drop Table vw_Productos_CodigoEAN__PRCS_VLDCN 

	End 

			
------------------------------------------------------------------------------------------------------------------------------------------------- 
	Set @bCrearTablaBase = 1   
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_CIE10_Diagnosticos__PRCS_VLDCN' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'vw_CIE10_Diagnosticos__PRCS_VLDCN' and xType = 'U' and datediff(dd, crDate, getdate()) > @iDias_CIE10  
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 	
	End 
		
	If @bCrearTablaBase = 1 
	Begin 
		print 'Creando tabla: vw_CIE10_Diagnosticos__PRCS_VLDCN' 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_CIE10_Diagnosticos__PRCS_VLDCN' and xType = 'U' ) 
		   Drop Table vw_CIE10_Diagnosticos__PRCS_VLDCN 
		   		   
		Select * 
		Into vw_CIE10_Diagnosticos__PRCS_VLDCN  
		From vw_CIE10_Diagnosticos (NoLock) 
	End 

		
------------------------------------------------------------------------------------------------------------------------------------------------- 
	Set @bCrearTablaBase = 1   
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Servicios_Areas__PRCS_VLDCN' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'vw_Servicios_Areas__PRCS_VLDCN' and xType = 'U' and datediff(dd, crDate, getdate()) > @iDias_Servicios  
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 	
	End 
		
	If @bCrearTablaBase = 1 
	Begin 
		print 'Creando tabla: vw_Servicios_Areas__PRCS_VLDCN' 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Servicios_Areas__PRCS_VLDCN' and xType = 'U' ) 
		   Drop Table vw_Servicios_Areas__PRCS_VLDCN 
		   		   
		Select * 
		Into vw_Servicios_Areas__PRCS_VLDCN  
		From vw_Servicios_Areas (NoLock) 
	End 
	
	
------------------------------------------------------------------------------------------------------------------------------------------------- 
	Set @bCrearTablaBase = 1   
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Claves_Precios_Asignados__PRCS_VLDCN' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'vw_Claves_Precios_Asignados__PRCS_VLDCN' and xType = 'U' and datediff(hh, crDate, getdate()) > @iHoras_Precios  
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 	
	End 	
		
	If @bCrearTablaBase = 1 
	Begin 
		print 'Creando tabla: vw_Claves_Precios_Asignados__PRCS_VLDCN' 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Claves_Precios_Asignados__PRCS_VLDCN' and xType = 'U' ) 
		   Drop Table vw_Claves_Precios_Asignados__PRCS_VLDCN 
		   		   
		Select * 
		Into vw_Claves_Precios_Asignados__PRCS_VLDCN  
		From vw_Claves_Precios_Asignados (NoLock) 
		Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente 


		If Not Exists ( Select top 1 * From vw_Claves_Precios_Asignados__PRCS_VLDCN (NoLock) )
		Begin 
			Insert Into vw_Claves_Precios_Asignados__PRCS_VLDCN  
			Select * 
			From vw_Claves_Precios_Asignados (NoLock) 
			Where IdEstado = @IdEstado
		End 

	End 
	
	
------------------------------------------------------------------------------------------------------------------------------------------------- 
	Set @bCrearTablaBase = 1   
	If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__PRCS_VLDCN' and xType = 'U' )  
	Begin 
		Set @bCrearTablaBase = 0 
		Select @bCrearTablaBase = 1 
		From sysobjects (NoLock) 
		Where Name = 'vw_Productos_CodigoEAN__PRCS_VLDCN' and xType = 'U' and datediff(dd, crDate, getdate()) > @iDias_EAN  
		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 	
	End 
	
	If @bCrearTablaBase = 1 
	Begin 
		print 'Creando tabla: vw_Productos_CodigoEAN__PRCS_VLDCN' 
		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__PRCS_VLDCN' and xType = 'U' ) 
		   Drop Table vw_Productos_CodigoEAN__PRCS_VLDCN 
		   		   
		Select * 
		Into vw_Productos_CodigoEAN__PRCS_VLDCN  
		From vw_Productos_CodigoEAN (NoLock) 
	End 
	
End 
Go--#SQL 


	
	