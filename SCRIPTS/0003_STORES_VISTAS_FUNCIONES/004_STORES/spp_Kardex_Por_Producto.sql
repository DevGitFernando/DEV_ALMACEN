------If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpKardex_Antibioticos_Farmacia' and xType = 'U' ) 
------   Drop Table tmpKardex_Antibioticos_Farmacia 
----------Go--#xxxSQL  


-------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Kardex_Por_Producto' and xType = 'P' ) 
   Drop Proc spp_Kardex_Por_Producto 
Go--#SQL  

Create Proc	 spp_Kardex_Por_Producto 
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2182',
	@CodigoEAN varchar(20) = '7501537102135',  
	@FechaInicial varchar(10) = '2015-01-01', @FechaFinal varchar(10) = '2015-12-31', 
	@TipoResultado int = 1 
)    
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD 
Declare @sNA varchar(10)  

Declare 
	@bCrearTablaBase int 

	Set @bCrearTablaBase = 1   
	Set @sNA = ' N/A ' 
	Set @sNA = '' 

---------------------------------- Catalogos 		
----	------------------------------------------ Generar tablas de catalogos     
----	If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__PRCS' and xType = 'U' )  
----	Begin 
----		Set @bCrearTablaBase = 0 
----		Select @bCrearTablaBase = 1 
----		From sysobjects (NoLock) 
----		Where Name = 'vw_Productos_CodigoEAN__PRCS' and xType = 'U' and datediff(Hh, crDate, getdate()) > 60 
----		Set @bCrearTablaBase = IsNull(@bCrearTablaBase, 1) 	
----	End 

----	If @bCrearTablaBase = 1 
----	Begin 	
----		------------------------------------------------------------------------------------------------------------------------------------ 	
----		If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_Productos_CodigoEAN__PRCS' and xType = 'U' ) 
----		   Drop Table vw_Productos_CodigoEAN__PRCS 
		   		   
----		Select * --  IdProducto, CodigoEAN, ClaveSSA, DescripcionClave  
----		Into vw_Productos_CodigoEAN__PRCS
----		From vw_Productos_CodigoEAN (Nolock)
----		--- Where 1 = 0 		
----	End 


----	Select * 
----	Into #tmpFarmacias 
----	From vw_Farmacias (NoLock) 
----	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 

---------------------------------- Catalogos 		
			


------------------------ Se llena tabla temporal para hacer mas rapido el llenado base. 
Declare  
	@Bloquedado int  
	Select @Bloquedado = (case when dbo.fg_INV_GetStatusProducto('{0}', '{1}', '{2}', '{4}') In ( 'I', 'S' ) Then 1 else 0 end)  

	Select 
		IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
		Convert(varchar(10), FechaSistema, 120) as Fecha, 
		FechaSistema, FechaRegistro, Folio, TipoMovto, DescMovimiento, 
		Referencia, IdProducto, CodigoEAN, DescProducto, IdClaveSSA_Sal, ClaveSSA_Base, ClaveSSA, ClaveSSA_Aux, DescripcionSal, 
		
		(case when @Bloquedado = 1 Then 0 else Entrada End) As Entrada,  
		(case when @Bloquedado = 1 Then 0 else Salida End) As Salida,  
		(case when @Bloquedado = 1 Then 0 else Existencia End) As Existencia, 
		(case when @Bloquedado = 1 Then 0 else Costo End) As Costo, 
		TasaIva, 
		(case when @Bloquedado = 1 Then 0 else Importe End) As Importe, 
		@Bloquedado as Bloqueado, 
		Keyx  
		
	Into #tmp__Kardex 
	From vw_Kardex_Producto (NoLock) 
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and CodigoEAN = @CodigoEAN 
		and Convert(varchar(10), FechaSistema, 120) Between @FechaInicial And @FechaFinal
	Order By FechaRegistro, Keyx  
                
	
----------------------------------------- SALIDA FINAL 		
	If @TipoResultado = 1 
		Begin 
			Select * 
			From #tmp__Kardex			
			Order By FechaRegistro, Keyx   
		End 
	Else 	
		Begin 
			Select 
				Convert(varchar(10), FechaSistema, 120) as Fecha, Folio, DescMovimiento, 
				Entrada, Salida, Existencia, Costo, Importe, Bloqueado 
			From #tmp__Kardex			
			Order By FechaRegistro, Keyx   
		End 


End 
Go--#SQL  
