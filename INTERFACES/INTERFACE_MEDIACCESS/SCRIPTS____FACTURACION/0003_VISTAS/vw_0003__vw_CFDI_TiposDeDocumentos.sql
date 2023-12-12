--------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_CFDI_TiposDeDocumentos' and xType = 'V' ) 
	Drop View vw_CFDI_TiposDeDocumentos 
Go--#SQL 
 	
Create View vw_CFDI_TiposDeDocumentos
With Encryption 
As 

	Select D.IdTipoDocumento, upper(D.NombreDocumento) as NombreDocumento, D.Alias, D.Status, 
		D.TipoDeComprobante, 
		( 
		case when D.TipoDeComprobante = 0 Then 'Sin espeficicar' 
		     when D.TipoDeComprobante = 1 Then 'Ingreso' 
		     when D.TipoDeComprobante = 2 Then 'Egreso' 
		     when D.TipoDeComprobante = 3 Then 'Traslado' 	     
		end
		) as TipoDeComprobanteDescripcion 
	From CFDI_TiposDeDocumentos D (NoLock) 
	
	
Go--#SQL 
select  * from vw_CFDI_TiposDeDocumentos 
 
