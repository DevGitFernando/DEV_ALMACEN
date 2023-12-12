
--------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CierresPeriodosDetalles' and xType = 'V' ) 
	Drop View vw_CierresPeriodosDetalles 
Go--#SQL

Create View vw_CierresPeriodosDetalles 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa,    
	    M.IdEstado, F.Estado as Estado, M.IdFarmacia, F.Farmacia, 
	    D.IdCliente, D.Cliente, D.IdSubCliente, D.SubCliente,  
	    D.IdPrograma, P.Programa, D.IdSubPrograma, P.SubPrograma,  
		D.TipoInventario, 
		Case When D.TipoInventario = 1 Then ('INVENTARIO ' + Ex.NombreCorto) Else 'CONSIGNACION' End As NombreTipoInv, 
	    D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia,
	    ( right('00000000' + cast(M.FolioCierre as varchar), 8) ) as Folio, M.IdPersonalRegistra, 
	    vP.NombreCompleto As Personal,
	    -- space(200) as Personal, 
	    D.AñoRegistro, MesRegistro, dbo.fg_NombresDeMesNumero(MesRegistro) as NombreMesRegistro, 
	    AñoReceta, MesReceta, dbo.fg_NombresDeMesNumero(MesReceta) as NombreMesReceta,
	    D.Tickets, D.Piezas, D.Monto, D.ValesEmitidos, D.ValesRegistrados, D.Efectividad, D.Perdida,
	    M.FechaRegistro, M.FechaCorte As FechaCierre, M.Status  	
	From Ctl_CierresDePeriodos M (NoLock)
	Inner Join Ctl_CierresPeriodosDetalles D (Nolock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.FolioCierre = D.FolioCierre )
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 		
	Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join vw_Programas_SubProgramas P (NoLock) On ( D.IdPrograma = P.IdPrograma and D.IdSubPrograma = P.IdSubPrograma )
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstadoRegistra = vP.IdEstado and M.IdFarmaciaRegistra = vP.IdFarmacia and M.IdPersonalRegistra = vP.IdPersonal ) 
Go--#SQL



--------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CierresPeriodosConcentrado' and xType = 'V' ) 
	Drop View vw_CierresPeriodosConcentrado 
Go--#SQL 

Create View vw_CierresPeriodosConcentrado 
With Encryption 
As 

    Select 
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
        TipoInventario, NombreTipoInv, IdSubFarmacia, SubFarmacia, 
        Folio, IdPersonalRegistra, Personal, 
        AñoRegistro, MesRegistro, NombreMesRegistro, AñoReceta, MesReceta, NombreMesReceta, 
        sum(Tickets) as Tickets, sum(Piezas) as Piezas, sum(Monto) as Monto, 
        max(ValesEmitidos) as ValesEmitidos, max(ValesRegistrados) as ValesRegistrados, max(Efectividad) as Efectividad, sum(Perdida) as Perdida, 
        FechaRegistro, FechaCierre, Status 
    From vw_CierresPeriodosDetalles 
    Group by 
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
        TipoInventario, NombreTipoInv, IdSubFarmacia, SubFarmacia, 
        Folio, IdPersonalRegistra, Personal, 
        AñoRegistro, MesRegistro, NombreMesRegistro, AñoReceta, MesReceta, NombreMesReceta, 
        FechaRegistro, FechaCierre, Status  

Go--#SQL 



--------------------------------------------------------------------------------------------------- 
--------------------------------------------------------------------------------------------------- 
--------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CierresPeriodosDetalles_VP' and xType = 'V' ) 
	Drop View vw_CierresPeriodosDetalles_VP 
Go--#SQL

Create View vw_CierresPeriodosDetalles_VP 
With Encryption 
As 
	Select M.IdEmpresa, Ex.Nombre as Empresa,    
	    M.IdEstado, F.Estado as Estado, M.IdFarmacia, F.Farmacia, 
	    D.IdCliente, D.Cliente, D.IdSubCliente, D.SubCliente,  
	    D.IdPrograma, P.Programa, D.IdSubPrograma, P.SubPrograma,  
		D.TipoInventario, 
		Case When D.TipoInventario = 1 Then ('INVENTARIO ' + Ex.NombreCorto) Else 'CONSIGNACION' End As NombreTipoInv, 
	    D.IdSubFarmacia, dbo.fg_ObtenerNombreSubFarmacia(D.IdEstado, D.IdFarmacia, D.IdSubFarmacia) as SubFarmacia,
	    ( right('00000000' + cast(M.FolioCierre as varchar), 8) ) as Folio, M.IdPersonalRegistra, 
	    vP.NombreCompleto As Personal,
	    -- space(200) as Personal, 
	    D.AñoRegistro, MesRegistro, dbo.fg_NombresDeMesNumero(MesRegistro) as NombreMesRegistro, 
	    AñoReceta, MesReceta, dbo.fg_NombresDeMesNumero(MesReceta) as NombreMesReceta,
	    D.Tickets, D.Piezas, D.Monto, D.ValesEmitidos, D.ValesRegistrados, D.Efectividad, D.Perdida,
	    M.FechaRegistro, M.FechaCorte As FechaCierre, M.Status  	
	From Ctl_CierresDePeriodos_VP M (NoLock)
	Inner Join Ctl_CierresPeriodosDetalles_VP D (Nolock) 
		On ( M.IdEmpresa = D.IdEmpresa and M.IdEstado = D.IdEstado and M.IdFarmacia = D.IdFarmacia and M.FolioCierre = D.FolioCierre )
	Inner Join CatEmpresas Ex (NoLock) On ( M.IdEmpresa = Ex.IdEmpresa ) 		
	Inner Join vw_Farmacias F (NoLock) On ( M.IdEstado = F.IdEstado and M.IdFarmacia = F.IdFarmacia ) 	
	Inner Join vw_Programas_SubProgramas P (NoLock) On ( D.IdPrograma = P.IdPrograma and D.IdSubPrograma = P.IdSubPrograma )
	Inner Join vw_Personal vP (NoLock) On ( M.IdEstadoRegistra = vP.IdEstado and M.IdFarmaciaRegistra = vP.IdFarmacia and M.IdPersonalRegistra = vP.IdPersonal ) 
Go--#SQL



--------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CierresPeriodosConcentrado_VP' and xType = 'V' ) 
	Drop View vw_CierresPeriodosConcentrado_VP 
Go--#SQL 

Create View vw_CierresPeriodosConcentrado_VP 
With Encryption 
As 

    Select 
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
        TipoInventario, NombreTipoInv, IdSubFarmacia, SubFarmacia, 
        Folio, IdPersonalRegistra, Personal, 
        AñoRegistro, MesRegistro, NombreMesRegistro, AñoReceta, MesReceta, NombreMesReceta, 
        sum(Tickets) as Tickets, sum(Piezas) as Piezas, sum(Monto) as Monto, 
        max(ValesEmitidos) as ValesEmitidos, max(ValesRegistrados) as ValesRegistrados, max(Efectividad) as Efectividad, sum(Perdida) as Perdida, 
        FechaRegistro, FechaCierre, Status 
    From vw_CierresPeriodosDetalles_VP 
    Group by 
        IdEmpresa, Empresa, IdEstado, Estado, IdFarmacia, Farmacia, 
        TipoInventario, NombreTipoInv, IdSubFarmacia, SubFarmacia, 
        Folio, IdPersonalRegistra, Personal, 
        AñoRegistro, MesRegistro, NombreMesRegistro, AñoReceta, MesReceta, NombreMesReceta, 
        FechaRegistro, FechaCierre, Status  

Go--#SQL 
        