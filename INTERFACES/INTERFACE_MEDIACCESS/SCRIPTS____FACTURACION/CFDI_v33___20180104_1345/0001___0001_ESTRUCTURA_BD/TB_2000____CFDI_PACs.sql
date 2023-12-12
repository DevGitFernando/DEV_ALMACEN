
---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select So.Name, Sc.Name From sysobjects So (NoLock) Inner Join Syscolumns Sc (NoLock) On ( So.Id = Sc.Id ) 
	Where So.Name = 'CFDI_PACs' and Sc.Name = 'Status' ) 
Begin 	
	Alter Table CFDI_PACs Add Status varchar(1)  Not Null Default 'A' 
End 
Go--#SQL  	

If Not Exists ( Select * From CFDI_PACs Where IdPAC = '002' )  Insert Into CFDI_PACs (  IdPAC, NombrePAC, UrlProduccion, UrlPruebas )  Values ( '002', 'PAX Facturación', 'https://www.paxfacturacion.com.mx:453', 'https://test.paxfacturacion.com.mx:453' ) 
 Else Update CFDI_PACs Set NombrePAC = 'PAX Facturación', UrlProduccion = 'https://www.paxfacturacion.com.mx:453', UrlPruebas = 'https://test.paxfacturacion.com.mx:453' Where IdPAC = '002'  
If Not Exists ( Select * From CFDI_PACs Where IdPAC = '003' )  Insert Into CFDI_PACs (  IdPAC, NombrePAC, UrlProduccion, UrlPruebas )  Values ( '003', 'Folios Digitales', 'https://www.foliosdigitalespac.com/WSTimbrado33/WSCFDI33.svc', 'https://www.foliosdigitalespac.com/WSTimbrado33Test/WSCFDI33.svc' ) 
 Else Update CFDI_PACs Set NombrePAC = 'Folios Digitales', UrlProduccion = 'https://www.foliosdigitalespac.com/WSTimbrado33/WSCFDI33.svc', UrlPruebas = 'https://www.foliosdigitalespac.com/WSTimbrado33Test/WSCFDI33.svc' Where IdPAC = '003'  
If Not Exists ( Select * From CFDI_PACs Where IdPAC = '004' )  Insert Into CFDI_PACs (  IdPAC, NombrePAC, UrlProduccion, UrlPruebas )  Values ( '004', 'VirtualSoft', 'http://facturacion.virtualsoft.com.mx:7512/timbradoXMLDos/Service.svc', 'http://facturacion.virtualsoft.com.mx:7512/timbradoXMLDemoDos/Service.svc' ) 
 Else Update CFDI_PACs Set NombrePAC = 'VirtualSoft', UrlProduccion = 'http://facturacion.virtualsoft.com.mx:7512/timbradoXMLDos/Service.svc', UrlPruebas = 'http://facturacion.virtualsoft.com.mx:7512/timbradoXMLDemoDos/Service.svc' Where IdPAC = '004'  

Go--#SQL 

