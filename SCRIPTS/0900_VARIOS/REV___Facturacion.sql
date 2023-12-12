

Select 
--StatusDocto, StatusDoctoAux, StatusDocumento, 
 E.IdEmpresa, E.IdEstado, E.IdFarmacia, FechaRegistro, IdTipoDocumento, E.Serie, E.Folio, Importe, RFC, NombreReceptor,
 --IdPersonalCancela, FechaCancelacion, MotivoCancelacion, 
 E.SubTotal, E.Iva, E.Total, 
 Clave, --DescripcionConcepto, 
 UnidadDeMedida, Cantidad, PrecioUnitario, TasaIva, 
 ( (D.cantidad * D.PrecioUnitario) - D.SubTotal ) as Diferencia, 
 (D.cantidad * D.PrecioUnitario) as SubTotal_Detalle_Calculado, 
 D.SubTotal as SubTotal_Detalle, D.Iva as IVA_Detalle, D.Total as Total_Detalle  
From vw_FACT_CFD_DocumentosElectronicos E (NoLock)
Inner Join FACT_CFD_Documentos_Generados_Detalles D (NoLock)
     On (E.IdEmpresa = D.IdEmpresa And E.IdEstado = D.idestado And E.Idfarmacia = D.idfarmacia And E.Serie = D.serie And E.Folio = D.Folio)
Where -- YEAR(E.fechaRegistro) = 2014 and 
	D.IdEstado = 11  And Cantidad > 0 And StatusDocto = 'A'
     And 
     (
         ((cantidad * PrecioUnitario) - D.SubTotal) > 1  
         Or 
		 ((cantidad * PrecioUnitario) - D.SubTotal) < -1
	 )
	 
  --   And 
  --   (
  --       ((cantidad * PrecioUnitario) - D.SubTotal) > 0  
  --       Or 
		-- ((cantidad * PrecioUnitario) - D.SubTotal) < 0
	 --)	 
	 
Order By abs( (D.cantidad * D.PrecioUnitario) - D.SubTotal ) desc ,  FechaRegistro 

 
--Order By cantidad * PrecioUnitario - D.SubTotal Desc
