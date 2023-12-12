-----------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Pedidos_Cedis' and xType = 'V' ) 
   Drop View vw_Impresion_Pedidos_Cedis 
Go--#SQL

Create View vw_Impresion_Pedidos_Cedis 
With Encryption 
As 

    Select
		E.IdEmpresa, Em.Nombre as Empresa,  
        F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia,
        FD.IdJurisdiccion As IdJurisdiccionSolicita, FD.Jurisdiccion As JurisdiccionSolicita, 
		E.IdEstadoSolicita, FD.Estado as EstadoSolicita, 
        IdFarmaciaSolicita, Fd.Farmacia As FarmaciaSolicita,
        E.FolioPedido as Folio, E.FechaRegistro, E.Observaciones, E.ReferenciaInterna as ReferenciaInternaPedido, E.Estransferencia,
        E.IdCliente, IsNull(A.NombreCliente,'') As NombreCliente, E.IdSubCliente, IsNull(A.NombreSubCliente, '') As NombreSubCliente,
        E.IdPrograma, IsNull(A.Programa,'') As Programa, E.IdSubPrograma, IsNull(A.SubPrograma,'') As SubPrograma,
		E.IdBeneficiario, IsNull(B.NombreCompleto,'') As Beneficiario, IsNull(B.FechaFinVigencia,'') As FechaFinVigencia, IsNull(B.FolioReferencia,'') As FolioReferencia,
        E.IdPersonal, P.NombreCompleto as Personal, E.Status, 
        D.IdClaveSSA, C.ClaveSSA, C.ClaveSSA_Aux, C.DescripcionClave, C.Presentacion, C.ContenidoPaquete,
        D.Cantidad, D.CantidadEnCajas, D.Existencia, D.Status as StatusClave, E.TipoDeClavesDePedido,
		FechaEntrega    
    From Pedidos_Cedis_Enc E (NoLock) 
    Inner Join Pedidos_Cedis_Det D (NoLock) On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioPedido = D.FolioPedido )
    Inner Join CatEmpresas Em (NoLock) On ( E.IdEmpresa = Em.IdEmpresa )
    Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia )
    Inner Join vw_Farmacias FD (NoLock) On ( E.IdEstadoSolicita = FD.IdEstado and E.IdFarmaciaSolicita = FD.IdFarmacia ) 
    Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonal = P.IdPersonal ) 
    Inner Join vw_ClavesSSA_Sales C (NoLock) On ( D.IdClaveSSA = C.IdClaveSSA_Sal )
    Left Join vw_Clientes_Programas_Asignados_Unidad A (NoLock)
			On (E.IdEstado = A.IdEstado And E.IdFarmacia = A.IdFarmacia And 
				E.IdCliente = A.IdCliente And E.IdSubCliente = A.IdSubCliente And E.IdPrograma = A.IdPrograma And E.IdSubPrograma = A.IdSubPrograma)
	Left Join vw_Beneficiarios B (NoLock)
		On (E.IdEstado = B.IdEstado And E.IdFarmacia = B.IdFarmacia And E.IdCliente = B.IdCliente And E.IdSubCliente = B.IdSubCliente And E.IdBeneficiario = B.IdBeneficiario)
    
--    Select * From vw_Clientes_Programas_Asignados_Unidad
     
Go--#SQL     


-----------------------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'vw_Impresion_Pedidos_Cedis_Surtido' and xType = 'V' ) 
   Drop View vw_Impresion_Pedidos_Cedis_Surtido 
Go--#SQL

Create View vw_Impresion_Pedidos_Cedis_Surtido 
With Encryption 
As 

    Select E.IdEmpresa, Em.Nombre as Empresa,  
        F.IdEstado, F.Estado, F.IdFarmacia, F.Farmacia, E.FolioSurtido as Folio, 
        E.IdFarmaciaPedido, FX.Farmacia as FarmaciaPedido, E.FolioPedido as FolioPedido, P.FechaRegistro as FechaPedido, 
        E.FechaRegistro, E.Observaciones, 
        E.IdPersonal, P.NombreCompleto as Personal, E.Status, 
        D.IdClaveSSA, C.ClaveSSA, C.DescripcionClave, C.Presentacion, 
        D.CantidadAsignada as Cantidad, 0 as Existencia, D.Status as StatusClave    
    From Pedidos_Cedis_Enc_Surtido E (NoLock) 
    Inner Join Pedidos_Cedis_Det_Surtido D (NoLock) 
		On ( E.IdEmpresa = D.IdEmpresa and E.IdEstado = D.IdEstado and E.IdFarmacia = D.IdFarmacia and E.FolioSurtido = D.FolioSurtido ) 
	Inner Join 	Pedidos_Cedis_Enc PD (NoLock) 
		On ( E.IdEmpresa = PD.IdEmpresa and E.IdEstado = PD.IdEstado and E.IdFarmaciaPedido = PD.IdFarmacia and E.FolioPedido = PD.FolioPedido ) 	
    Inner Join CatEmpresas Em (NoLock) On ( E.IdEmpresa = Em.IdEmpresa ) 
    Inner Join vw_Farmacias F (NoLock) On ( E.IdEstado = F.IdEstado and E.IdFarmacia = F.IdFarmacia ) 
    Inner Join vw_Farmacias FX (NoLock) On ( E.IdEstado = FX.IdEstado and E.IdFarmaciaPedido = FX.IdFarmacia )     
    Inner Join vw_Personal P (NoLock) On ( E.IdEstado = P.IdEstado and E.IdFarmacia = P.IdFarmacia and E.IdPersonal = P.IdPersonal ) 
    Inner Join vw_ClavesSSA_Sales C (NoLock) On ( D.IdClaveSSA = C.IdClaveSSA_Sal ) 
    
--    select IdFarmaciaPedido, *         from Pedidos_Cedis_Enc_Surtido


     
Go--#SQL     


