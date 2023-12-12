----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( select Name From Sysobjects (NoLock) Where Name = 'tmpProductos' and xType = 'U' ) 
   Drop Table tmpProductos 
Go--#SQL 	   

----------------------------------------------------------------------------------------------------------------------------------------- 
If Exists (Select Name From SysObjects(NoLock) Where Name = 'spp_ValidaCantidadClavesSubPerfil___ProgramaAtencion' and xType = 'P')
    Drop Proc spp_ValidaCantidadClavesSubPerfil___ProgramaAtencion
Go--#SQL

--		Exec spp_ValidaCantidadClavesSubPerfil___ProgramaAtencion '003', '16', '0011', '0002', '0002', '0001', 'tmptest'
  
Create Proc spp_ValidaCantidadClavesSubPerfil___ProgramaAtencion 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', 
	@IdCliente varchar(4) = '', @IdPrograma varchar(4) = '', @IdSubPrograma varchar(4) = '', 
	@TipoDeRecetaAtendida varchar(4) = '', 
	@Tabla varchar(100) = 'tmpCantClavesSubPerfil_94DE8051B7DC_20170407_112921' 
)
With Encryption 
As 
Begin
Set NoCount On

Declare @iResultado tinyint, @sSql varchar(7500), @sTablaRegistro varchar(100) 

	Set @sSql = ''	
	Set @iResultado = 0	
	
	--------- SE ACTUALIZA EL IDCLAVESSA DE LOS PRODUCTOS  ---------------------------------------------------------------------
	Set @sSql = ' Update T Set T.IdClaveSSA = P.IdClaveSSA_Sal, T.ClaveSSA = P.ClaveSSA, T.Descripcion = P.DescripcionCortaClave ' + 
	'From ' + @Tabla + ' T (Nolock) ' + 
	'Inner Join vw_Productos_CodigoEAN P (Nolock) On ( T.IdProducto = P.IdProducto and T.CodigoEAN = P.CodigoEAN ) ' 
	Exec(@sSql)	
	
	
	
	---------  SE CREA UNA TEMPORAL DE LOS PRODUCTOS  -------------------------------------------------------------------------
	If Exists ( select Name From Sysobjects (NoLock) Where Name = 'tmpProductos' and xType = 'U' ) 
	   Drop Table tmpProductos 
	
	Set @sSql = ' Select * Into tmpProductos From ' + @Tabla 
	Exec(@sSql)		
	
	Select IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, Cast(ClaveSSA As Varchar(30)) as ClaveSSA, cast(Descripcion as varchar(max)) as Descripcion,
		Sum(Cantidad) as Cant_A_Surtir, 0 as Cant_Surtida, 0 as Stock, 0 as CantidadTotal, 0 as CantExcedida, 0 as EsExcedida 
	Into #tmpClaves From tmpProductos (NOLOCK)
	Group By IdEmpresa, IdEstado, IdFarmacia, IdClaveSSA, ClaveSSA, Descripcion	
	
	
	
	-------------------  VALIDAMOS QUE EXISTA EL SUB PERFIL  --------------------------------------------------------------------------------------------
	If Exists ( Select * From CFG_CB_Sub_CuadroBasico_Claves (Nolock) 
			Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdFarmacia = @IdFarmacia 
			and IdPrograma = @IdPrograma and IdSubPrograma = @IdSubPrograma )
		Begin
		 
			----------------------------------------- SE OBTIENE LO SURTIDO DEL MES EN CURSO  -------------------------------------------------	
			Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, Sum(CantidadVendida) as Cantidad 
			Into #tmpClavesSurtidasMes
			From VentasEnc E (Nolock)
			Inner Join VentasDet D (Nolock)
				On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta )
			Inner Join vw_Productos_CodigoEAN P (Nolock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )
			Where E.IdEmpresa = @IdEmpresa and E.IdEstado = @IdEstado and E.IdFarmacia = @IdFarmacia and E.IdCliente = @IdCliente
				and E.IdPrograma = @IdPrograma and E.IdSubPrograma = @IdSubPrograma 
				and datepart(yy, E.FechaRegistro) = datepart(yy, getdate()) and datepart(mm, E.FechaRegistro) = datepart(mm, getdate())
				and P.IdClaveSSA_Sal In ( Select IdClaveSSA From #tmpClaves (Nolock) )
			Group By E.IdEmpresa, E.IdEstado, E.IdFarmacia, P.IdClaveSSA_Sal, P.ClaveSSA
			 
			 
			-------------------  SE ACTUALIZA LA CANTIDAD SURTIDA Y LA CANTIDAD TOTAL  ---------------------------------- 
			Update T Set T.Cant_Surtida = S.Cantidad, T.CantidadTotal = (T.Cant_A_Surtir + S.Cantidad)
			From #tmpClaves T (Nolock)
			Inner Join #tmpClavesSurtidasMes S (Nolock) On ( T.IdClaveSSA = S.IdClaveSSA )	
			---------------------------------------------------------------------------------------------------------------
			
			-------------- SE OBTIENEN LAS CLAVES DEL SUBPERFIL EN CASO DE QUE EXISTAN  ---------------------------------------------------
			Select T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdClaveSSA, C.Cantidad as Stock, 1 as EsExcedida 
			Into #tmpClavesSubPerfil
			From #tmpClaves T (Nolock)
			Inner Join CFG_CB_Sub_CuadroBasico_Claves C (Nolock)
				On ( T.IdEstado = C.IdEstado and T.IdFarmacia = C.IdFarmacia and C.IdCliente = @IdCliente and C.IdPrograma = @IdPrograma 
					and C.IdSubPrograma = @IdSubPrograma and T.IdClaveSSA = C.IdClaveSSA )
			Group By T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdClaveSSA, C.Cantidad, T.CantidadTotal
			Having T.CantidadTotal > C.Cantidad 
			--------------------------------------------------------------------------------------------------------------------------------
			
			-------------------  SE ACTUALIZA EL STOCK, CANTIDAD EXCEDIDA ---------------------------------------------------------
			Update T Set T.Stock = S.Stock, T.CantExcedida = ( T.CantidadTotal - S.Stock ), T.EsExcedida = S.EsExcedida
			From #tmpClaves T (Nolock)
			Inner Join #tmpClavesSubPerfil S (Nolock) On ( T.IdClaveSSA = S.IdClaveSSA )
			Where S.EsExcedida = 1
			------------------------------------------------------------------------------------------------------------------------
			
			-----  SE REGRESA LA CONSULTA EN CASO DE QUE HAYA CLAVES EXCEDIDAS QUE PERTENECEN AL SUBPERFIL   ---------------
			Select 
				'Clave SSA' = ClaveSSA, 'Descripción Clave' = Descripcion, Stock, 
				'Surtido previo' = Cant_Surtida, 'Surtido actual' = Cant_A_Surtir, 'Cantidad Excedida' = CantExcedida 
			From #tmpClaves (Nolock)  
			Where EsExcedida = 1
			-----------------------------------------------------------------------------------------------------------------
		End
	Else
		Begin
			Select * 
			From #tmpClaves (Nolock)
			Where EsExcedida = 1
		End
	
	
	
End
Go--#SQL	


