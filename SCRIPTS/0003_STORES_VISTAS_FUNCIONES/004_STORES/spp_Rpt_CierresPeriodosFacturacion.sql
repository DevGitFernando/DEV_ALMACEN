If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_CierresPeriodosFacturacion' and xType = 'P' ) 
   Drop Proc spp_Rpt_CierresPeriodosFacturacion
Go--#SQL 

/*
	Exec   spp_Rpt_CierresPeriodosFacturacion  '21', '0188', 1  

	Select * From Rpt_CierresPeriodosFacturacion (Nolock)	

	Select * From Rpt_CierresPeriodosFacturacion (Nolock)	

*/ 

Create Proc spp_Rpt_CierresPeriodosFacturacion 
( 
	@IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0188', @FolioCierre int = 1, @EsVistaPrevia bit = 0  
) 	
With Encryption 
As 
Begin 
Set NoCount On  
Set Dateformat YMD 
Declare 
	@iEnero int, @iFebrero int, @iMarzo int, @iAbril int, @iMayo int, @iJunio int, 
	@iJulio int, @iAgosto int, @iSeptiembre int, @iOctubre int, @iNoviembre int, @iDiciembre int,
	@sWhereSubFarmacias varchar(200) 											

Declare 
	
	@EncPrincipal varchar(500), 
	@EncSecundario varchar(500),
	@FechaInicial varchar(10), 	
	@FechaFinal varchar(10)

	Set @FechaInicial = ''
	Set @FechaFinal = ''

	Select @iEnero = 1, @iFebrero = 2, @iMarzo = 3, @iAbril = 4, @iMayo = 5, @iJunio = 6  
	Select @iJulio = 7, @iAgosto = 8, @iSeptiembre = 9, @iOctubre = 10, @iNoviembre = 11, @iDiciembre = 12 
	
	Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario 
	from dbo.fg_Unidad_EncabezadoReportesClientesSSA()


--- Obtener los Datos Principales 
		
    Select E.IdEmpresa, E.IdEstado, E.IdFarmacia, L.IdSubFarmacia, E.FolioVenta, 
           E.IdCliente, space(100) as Cliente, E.IdSubCliente, space(100) as SubCliente, 
           E.IdPrograma, space(100) as Programa, E.IdSubPrograma, space(100) as SubPrograma, 
	       datepart(yy, E.FechaRegistro) as AñoRegistro, datepart(mm, E.FechaRegistro) as MesRegistro,
	       dbo.fg_NombresDeMesNumero(datepart(mm, E.FechaRegistro)) as NombreMesRegistro, 
	       datepart(yy, VA.FechaReceta) as AñoReceta, datepart(mm, VA.FechaReceta) as MesReceta,
	       dbo.fg_NombresDeMesNumero(datepart(mm, VA.FechaReceta)) as NombreMesReceta, 
	       0 As Tickets, P.IdClaveSSA_Sal as IdClaveSSA, P.ClaveSSA, P.DescripcionClave, D.TasaIva,	cast(0 as numeric(14,4)) as PrecioLicitacion,   	
	       -- L.ClaveLote, EsConsignacion As EsDeConsignacion, sum(L.CantidadVendida) as CantidadVendida, cast(0 as numeric(14,4)) as Importe		
	       L.ClaveLote, 
	       (case when L.ClaveLote like '%*%' Then 2 Else 1 End) As EsDeConsignacion, 
	       sum(L.CantidadVendida) as CantidadVendida, cast(0 as numeric(14,4)) as Importe			       	   
    Into #tmpCierresPeriodos					    
    From VentasEnc E (NoLock) 
    Inner Join VentasDet D (NoLock) 
	    On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioVenta = D.FolioVenta ) 
    Inner Join VentasDet_Lotes L (NoLock) 
	    On ( D.IdEmpresa = L.IdEmpresa and D.IdEstado = L.IdEstado and D.IdFarmacia = L.IdFarmacia and D.FolioVenta = L.FolioVenta 
		     and D.IdProducto = L.IdProducto and D.CodigoEAN = L.CodigoEAN )
    Inner Join VentasInformacionAdicional VA (Nolock)
	    On ( E.IdEmpresa = VA.IdEmpresa and E.IdEstado = VA.IdEstado and E.IdFarmacia = VA.IdFarmacia and E.FolioVenta = VA.FolioVenta )			
    Inner Join vw_Productos_CodigoEAN P (NOLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )				
    Where  E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.FolioCierre = @FolioCierre
    --And convert(varchar(10), E.FechaRegistro, 120) Between @FechaInicial and @FechaFinal  	
    Group by  E.IdEmpresa, E.IdEstado, E.IdFarmacia, L.IdSubFarmacia, E.FolioVenta, 
           E.IdCliente, E.IdSubCliente, E.IdPrograma, E.IdSubPrograma, 
	       datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro), datepart(yy, VA.FechaReceta), datepart(mm, VA.FechaReceta),			    
	       P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, D.TasaIva, L.ClaveLote, 
	       -- EsConsignacion 
	       (case when L.ClaveLote like '%*%' Then 1 Else 0 End) 
    Order By E.FolioVenta, datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro), datepart(yy,  VA.FechaReceta), datepart(mm, VA.FechaReceta)


---- Asignar información de Clientes y Programas 
    Update B Set Cliente = NombreCliente, SubCliente = NombreSubCliente 
	From #tmpCierresPeriodos B (NoLock) 
    Inner Join  vw_Clientes_SubClientes C On ( B.IdCliente = C.IdCliente and B.IdSubCliente = C.IdSubCliente ) 

    Update B Set Programa = B.Programa, SubPrograma = B.SubPrograma 
	From #tmpCierresPeriodos B (NoLock) 
    Inner Join  vw_Programas_SubProgramas C On ( B.IdPrograma = C.IdPrograma and B.IdSubPrograma = C.IdSubPrograma ) 
    

------- Reemplazo de Claves 
	Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionClave = C.Descripcion 
	From #tmpCierresPeriodos B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' ) 	
------- Reemplazo de Claves 

----------- Asignacion de Precios 
	Update B Set PrecioLicitacion = IsNull(PC.PrecioUnitario, 0) 
	From #tmpCierresPeriodos B (NoLock) 
	Left Join vw_Claves_Precios_Asignados PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
		On ( B.IdEstado = PC.IdEstado and B.IdClaveSSA = PC.IdClaveSSA and PC.Status = 'A'  ) 
----------- Asignacion de Precios 

-------------- Calcular el importe 
	Update B Set B.Importe = (CantidadVendida * PrecioLicitacion) 
	From #tmpCierresPeriodos B (NoLock) 

------Sacar el concentrado 

-- Drop Table tmpCierresConcentradoPrueba

	Select 
	    IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
        IdCliente, Cliente, IdSubCliente, SubCliente, 
        IdPrograma, Programa, IdSubPrograma, SubPrograma, 	    
	    AñoRegistro, MesRegistro, NombreMesRegistro, 
	    AñoReceta, MesReceta, NombreMesReceta, Tickets, cast(Sum(CantidadVendida) as numeric(14,4))  As CantidadPiezas,
	    cast(Sum(Importe) as numeric(14,4)) As ImporteTotal, 0 As ValesEmitidos, 0 As ValesRegistrados,
	    cast(0 as numeric(14,4)) as Efectividad, cast(0 as numeric(14,4)) as Perdida,
	    -- 0 As TipoInventario  
	    EsDeConsignacion As TipoInventario  	    
	Into #tmpCierresPeriodosConcentrado 
	From #tmpCierresPeriodos (Nolock)	
	Group By 
	    IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, 
        IdCliente, Cliente, IdSubCliente, SubCliente, 
        IdPrograma, Programa, IdSubPrograma, SubPrograma, 	    	    
	    AñoRegistro, MesRegistro, NombreMesRegistro, 
	    AñoReceta, MesReceta, NombreMesReceta, Tickets, EsDeConsignacion 
	Order By AñoRegistro, MesRegistro, AñoReceta, MesReceta

-- Actualizar el numero de tickets 
	
	Update C Set C.Tickets = ( Select Count(Distinct E.FolioVenta) From #tmpCierresPeriodos E (Nolock)
								Where E.IdEmpresa = C.IdEmpresa and E.IdEstado = C.IdEstado and E.IdFarmacia = C.IdFarmacia 
								and E.IdSubFarmacia = C.IdSubFarmacia 
								and E.IdCliente = C.IdCliente and E.IdSubCliente = C.IdSubCliente 
								and E.IdPrograma = C.IdPrograma and E.IdSubPrograma = C.IdSubPrograma 
								and E.AñoRegistro = C.AñoRegistro 
								and E.MesRegistro = C.MesRegistro and E.AñoReceta = C.AñoReceta and E.MesReceta = C.MesReceta )
	From #tmpCierresPeriodosConcentrado C (Nolock) 
	
------------------------------------------------------------------------------------------------------------------------
--------------------------   SECCION DE VALES  ------------------------------------------------------------------------- 
	Select 
	    E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.Folio, 
	    datepart(yy, E.FechaRegistro) as AñoRegistro, datepart(mm, E.FechaRegistro) as MesRegistro,
        dbo.fg_NombresDeMesNumero(datepart(mm, E.FechaRegistro)) as NombreMesRegistro, 	
	    0 As ValesRegistrados, P.IdClaveSSA_Sal as IdClaveSSA, 
	    P.ClaveSSA, P.DescripcionClave, cast(0 as numeric(14,4)) as PrecioLicitacion, D.CostoUnitario as PrecioProveedor, 
	    L.ClaveLote, sum(L.CantidadRecibida) as CantidadRecibida, cast(0 as numeric(14,4)) as Importe, cast(0 as numeric(14,4)) as ImportePerdida
	Into #tmpCierresValesRegistrados			    
	From ValesEnc E (NoLock)	 
	Inner Join ValesDet D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.Folio = D.Folio )		
	Inner Join ValesDet_Lotes L (Nolock)
		On ( E.IdEmpresa = L.IdEmpresa and E.IdEstado = L.IdEstado and E.IdFarmacia = L.IdFarmacia and E.Folio = L.Folio )
	Inner Join vw_Productos_CodigoEAN P (NOLock) On ( D.IdProducto = P.IdProducto and D.CodigoEAN = P.CodigoEAN )						
	Where  E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.FolioCierre = @FolioCierre 	
	Group by  E.IdEmpresa, E.IdEstado, E.IdFarmacia, E.Folio, 
	    datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro),
	    P.IdClaveSSA_Sal, P.ClaveSSA, P.DescripcionClave, D.CostoUnitario, L.ClaveLote
	Order By datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro)

--------------------------   SECCION DE VALES EMITIDOS ------------------------------------------------------------------------- 
	Select E.IdEmpresa, E.IdEstado, E.IdFarmacia,  datepart(yy, E.FechaRegistro) as AñoRegistro, datepart(mm, E.FechaRegistro) as MesRegistro, 
           dbo.fg_NombresDeMesNumero(datepart(mm, E.FechaRegistro)) as NombreMesRegistro, 			
		   Count(E.Foliovale) As ValesEmitidos, 0 As ValesRegistrados, cast(0 as numeric(14,4)) as Efectividad,
	cast(0 as numeric(14,4)) as Perdida 
	Into #tmpCierresValesEmitidos			    
	From Vales_EmisionEnc E (NoLock)							
	Where E.IdEstado = @IdEstado And E.IdFarmacia = @IdFarmacia And E.FolioCierre = @FolioCierre			
	Group by  E.IdEmpresa, E.IdEstado, E.IdFarmacia, datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro)
	Order By datepart(yy, E.FechaRegistro), datepart(mm, E.FechaRegistro)
--------------------------------------------------------------------------------------------------------------------------------------------------------
	------- Reemplazo de Claves
	Update B Set IdClaveSSA = C.IdClaveSSA, ClaveSSA = C.ClaveSSA, DescripcionClave = C.Descripcion 
	From #tmpCierresValesRegistrados B (NoLock) 
	Inner Join vw_Relacion_ClavesSSA_Claves C (NoLock) On ( B.IdEstado = C.IdEstado and B.IdClaveSSA = C.IdClaveSSA_Relacionada and C.Status = 'A' )	
------- Reemplazo de Claves 

----------- Asignacion de Precios 
	Update B Set PrecioLicitacion = IsNull(PC.Precio, 0) 
	From #tmpCierresValesRegistrados B (NoLock) 
	Left Join vw_Claves_Precios_Asignados PC (NoLock)  --- Asignacion de precios de acuerdo a la Clave Licitada 
		On ( B.IdEstado = PC.IdEstado and B.IdClaveSSA = PC.IdClaveSSA  )
----------- Asignacion de Precios

-------------- Calcular el importe 
	Update B Set B.Importe = (CantidadRecibida * PrecioLicitacion) 
	From #tmpCierresValesRegistrados B (NoLock) 

-------------- Calcular el ImportePerdida 
	Update B Set B.ImportePerdida = (CantidadRecibida * (PrecioProveedor-PrecioLicitacion)) 
	From #tmpCierresValesRegistrados B (NoLock) 

------Sacar el concentrado
	Select 
	    IdEmpresa, IdEstado, IdFarmacia, AñoRegistro, MesRegistro, NombreMesRegistro,
	    0 As ValesEmitidos, ValesRegistrados, cast(0 as numeric(14,4)) as Efectividad, 
	    Sum(CantidadRecibida) As CantidadRecibida, Sum(Importe) As Importe, Sum(ImportePerdida) As ImportePerdida
	Into #tmpCierresConcentradoValesReg
	From #tmpCierresValesRegistrados (Nolock)	
	Group By IdEmpresa, IdEstado, IdFarmacia, AñoRegistro, MesRegistro, NombreMesRegistro, ValesRegistrados

-- Actualizar el numero de ValesRegistrados	
	Update C Set C.ValesRegistrados = ( Select Count(Distinct E.Folio) From #tmpCierresValesRegistrados E (Nolock)
								Where E.IdEmpresa = C.IdEmpresa and E.IdEstado = C.IdEstado and E.IdFarmacia = C.IdFarmacia
								And E.AñoRegistro = C.AñoRegistro and E.MesRegistro = C.MesRegistro )
	From #tmpCierresConcentradoValesReg C (Nolock)
	
-- Concentrar Vales Emitidos
	Select 
	    IdEmpresa, IdEstado, IdFarmacia, AñoRegistro, MesRegistro, NombreMesRegistro,
    	ValesEmitidos, ValesRegistrados, Efectividad, Perdida
	Into #tmpCierresConcentradoVales
	From #tmpCierresValesEmitidos (Nolock)
	Order By IdEmpresa, IdEstado, IdFarmacia, AñoRegistro, MesRegistro

--	Actualizar la Informacion de Vales Registrados
	Update C Set C.ValesRegistrados = R.ValesRegistrados, C.Perdida = R.ImportePerdida
	From #tmpCierresConcentradoVales C (Nolock) 
	Inner Join #tmpCierresConcentradoValesReg R (Nolock)
		On ( C.IdEmpresa = R.IdEmpresa and C.IdEstado = R.IdEstado and C.IdFarmacia = R.IdFarmacia 
			And C.AñoRegistro = R.AñoRegistro and C.MesRegistro = R.MesRegistro )

-- Actualizar la Efectividad	
	Update C Set C.Efectividad = (( cast(C.ValesRegistrados as numeric(14,4))/cast(C.ValesEmitidos as numeric(14,4))) * 100)
	From #tmpCierresConcentradoVales C (Nolock) 
	Where C.ValesEmitidos > 0 and C.ValesRegistrados > 0

-- Actualizar la Tabla de Concetrado Final 
	Update C Set C.ValesEmitidos = R.ValesEmitidos, C.ValesRegistrados = R.ValesRegistrados, C.Efectividad = R.Efectividad, C.Perdida = R.Perdida
	From #tmpCierresPeriodosConcentrado C (Nolock)
	Inner Join #tmpCierresConcentradoVales R (Nolock)
		On ( C.IdEmpresa = R.IdEmpresa and C.IdEstado = R.IdEstado and C.IdFarmacia = R.IdFarmacia 
			And C.AñoRegistro = R.AñoRegistro and C.MesRegistro = R.MesRegistro )


------------------------------------------------------------------------------------------------------------------------------------
------------------------- Actualizar el tipo de Inventario -------------------------------------------------------------------------
----	Revisar 
----    Quito... Jesús Díaz 2K120510.2010 
----	Update C Set C.TipoInventario = ( Case When S.EsConsignacion = 0 Then 1 Else 2 End  ) 
----	From #tmpCierresPeriodosConcentrado C (Nolock)
----	Inner Join vw_Farmacias_SubFarmacias S (Nolock) On ( S.IdEstado = C.IdEstado and S.IdFarmacia = C.IdFarmacia and S.IdSubFarmacia = C.IdSubFarmacia ) 

----------	Update C Set C.TipoInventario = ( Case When S.EsConsignacion = 0 Then 1 Else 2 End  ) 
----------	From #tmpCierresPeriodosConcentrado C (Nolock) 
-------------------------------------------------------------------------------------------------------------------------------------
-------------------------------------------------------------------------------------------------------------------------------------


    If @EsVistaPrevia = 1  
    Begin        
        Insert Into Ctl_CierresPeriodosDetalles_VP  
	    Select		
		    T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdSubFarmacia, @FolioCierre  as FolioCierre, 
            T.IdCliente, T.Cliente, T.IdSubCliente, T.SubCliente, 
		    T.IdPrograma, T.Programa, T.IdSubPrograma, T.SubPrograma, 
		    T.AñoRegistro, T.MesRegistro,
		    T.AñoReceta, T.MesReceta, T.Tickets, T.CantidadPiezas As Piezas, T.ImporteTotal As Monto, 
		    T.ValesEmitidos, T.ValesRegistrados, T.Efectividad, T.Perdida, getdate(), 
		    T.TipoInventario, 'A' As Status, 0 As Actualizado		   
	    From #tmpCierresPeriodosConcentrado T (Nolock)	  
	    Order By T.IdSubFarmacia, T.AñoRegistro, T.MesRegistro, T.AñoReceta, T.MesReceta        
        
    End 
    Else 
    Begin 
	    Insert Into Ctl_CierresPeriodosDetalles_Historico 
	    Select		
		    T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdSubFarmacia, @FolioCierre as FolioCierre, 
		    T.IdCliente, T.Cliente, T.IdSubCliente, T.SubCliente, 
		    T.IdPrograma, T.Programa, T.IdSubPrograma, T.SubPrograma, 
		    T.AñoRegistro, T.MesRegistro,
		    T.AñoReceta, T.MesReceta, T.Tickets, T.CantidadPiezas As Piezas, T.ImporteTotal As Monto, 
		    T.ValesEmitidos, T.ValesRegistrados, T.Efectividad, T.Perdida, getdate() , getdate() ,
		    T.TipoInventario, 'A' As Status, 0 As Actualizado		   
	    From #tmpCierresPeriodosConcentrado T (Nolock)	  
	    Order By T.IdSubFarmacia, T.AñoRegistro, T.MesRegistro, T.AñoReceta, T.MesReceta
    	 
	    Delete From  Ctl_CierresPeriodosDetalles Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioCierre = @FolioCierre


	    Insert Into Ctl_CierresPeriodosDetalles 
							( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioCierre, IdCliente, Cliente,
							  IdSubCliente, SubCliente, IdPrograma, Programa, IdSubPrograma, SubPrograma, AñoRegistro,
							  MesRegistro, AñoReceta, MesReceta, Tickets, Piezas, Monto, ValesEmitidos, ValesRegistrados,
							  Efectividad, Perdida, FechaRegistro, TipoInventario, Status, Actualizado, FechaControl )
	    Select		
		    T.IdEmpresa, T.IdEstado, T.IdFarmacia, T.IdSubFarmacia, @FolioCierre as FolioCierre, 
            T.IdCliente, T.Cliente, T.IdSubCliente, T.SubCliente, 
		    T.IdPrograma, T.Programa, T.IdSubPrograma, T.SubPrograma, 
		    T.AñoRegistro, T.MesRegistro,
		    T.AñoReceta, T.MesReceta, T.Tickets, T.CantidadPiezas As Piezas, T.ImporteTotal As Monto, 
		    T.ValesEmitidos, T.ValesRegistrados, T.Efectividad, T.Perdida, getdate() , 
		    T.TipoInventario, 'A' As Status, 0 As Actualizado, GetDate()		   
	    From #tmpCierresPeriodosConcentrado T (Nolock)	  
	    Order By T.IdSubFarmacia, T.AñoRegistro, T.MesRegistro, T.AñoReceta, T.MesReceta
    End 

/*
	Exec   spp_Rpt_CierresPeriodosFacturacion  '21', '0188', 1  

	Select * From Ctl_CierresPeriodosDetalles (Nolock)

	Select * From Ctl_CierresPeriodosDetalles_Historico (Nolock)
*/


End	
Go--#SQL 
	