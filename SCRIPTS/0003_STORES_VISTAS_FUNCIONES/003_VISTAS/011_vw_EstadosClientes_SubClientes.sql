---------------------------------------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Estados_Clientes' and xType = 'V' ) 
   Drop View vw_Estados_Clientes 
Go--#SQL

Create View vw_Estados_Clientes 
With Encryption 
As 

	Select Distinct F.IdEstado, E.Nombre, F.IdCliente, C.NombreCliente, F.Status as StatusRelacion, C.Status as StatusCliente  
	From CFG_EstadosClientes F (NoLock)  
	Inner Join CatEstados E (NoLock) On ( F.IdEstado = E.IdEstado ) 
	Inner Join vw_Clientes_SubClientes C On ( F.IdCliente = C.IdCliente ) 
Go--#SQL

	
---------------------------------------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Farmacias_Clientes_SubClientes' and xType = 'V' ) 
   Drop View vw_Farmacias_Clientes_SubClientes 
Go--#SQL	


Create View vw_Farmacias_Clientes_SubClientes 
With Encryption 
As 

	Select C.IdEstado, F.Estado, C.IdFarmacia, F.Farmacia, F.ManejaVtaPubGral, 
		C.IdCliente, S.NombreCliente, C.IdSubCliente, S.NombreSubCliente, 
		S.PorcUtilidad as Descuento, 
		S.PermitirImportaBeneficiarios, S.PermitirCapturaBeneficiarios, 
		C.Status as StatusRelacion, S.StatusSubCliente   
	From CFG_EstadosFarmaciasClientesSubClientes C (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Clientes_SubClientes S (NoLock) On ( C.IdCliente = S.IdCliente and C.IdSubCliente = S.IdSubCliente ) 

Go--#SQL	
  	

---------------------------------------------------------------------------------------------------------------------------------------------------------- 	
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Farmacias_Programas_SubPrograma_Clientes' and xType = 'V' ) 
   Drop View vw_Farmacias_Programas_SubPrograma_Clientes 
Go--#SQL
 

Create View vw_Farmacias_Programas_SubPrograma_Clientes  
With Encryption 
As 
	Select C.IdEstado, F.Estado, C.IdFarmacia, F.Farmacia, F.ManejaVtaPubGral, 
		C.IdCliente, S.NombreCliente, C.IdSubCliente, S.NombreSubCliente, C.IdPrograma, P.Programa, C.IdSubPrograma, P.SubPrograma, 
		C.Dispensacion_CajasCompletas, 
		C.Status as StatusRelacion, S.StatusSubCliente, P.StatusSubPrograma    
	From CFG_EstadosFarmaciasProgramasSubProgramas C (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Clientes_SubClientes S (NoLock) On ( C.IdCliente = S.IdCliente and C.IdSubCliente = S.IdSubCliente ) 
	Inner Join vw_Programas_SubProgramas P (NoLock) On ( C.IdPrograma = P.IdPrograma and C.IdSubPrograma = P.IdSubPrograma )  		
Go--#SQL

	