------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_BancosCuentas_Receptor' and xType = 'V' ) 
	Drop View vw_BancosCuentas_Receptor 
Go--#SQL 

Create View vw_BancosCuentas_Receptor 
With Encryption 
As 

	Select 
		 CB.IdEmpresa, CB.IdEstado, F.Estado, CB.IdFarmacia, F.Farmacia, 
		 CB.RFC_Banco, 
		 CB.NumeroDeCuenta, 
		 --B.RFC, 
		 B.NombreCorto, B.NombreRazonSocial, 	 
		 CB.Status 
	From CFDI_BancosCuentas__Receptor CB (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( F.IdEstado = CB.IdEstado and F.IdFarmacia = CB.IdFarmacia ) 
	Inner Join CFDI__Bancos B (NoLock) On ( CB.RFC_Banco = B.RFC ) 
 

Go--#SQL 


------------------------------------------------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_BancosCuentas_Emisor' and xType = 'V' ) 
	Drop View vw_BancosCuentas_Emisor 
Go--#SQL 

Create View vw_BancosCuentas_Emisor 
With Encryption 
As 

--	sp_listacolumnas FACT_CFDI_BancosCuentas__Receptor 

--	sp_listacolumnas FACT_CFDI__Bancos 

	Select 
		 CB.IdEmpresa, CB.IdEstado, F.Estado, CB.IdFarmacia, F.Farmacia, 
		 CB.RFC_Emisor, C.Nombre, 
		 CB.RFC_Banco, 
		 CB.NumeroDeCuenta, 
		 --B.RFC, 
		 B.NombreCorto, B.NombreRazonSocial, 	 
		 CB.Status 
	From CFDI_BancosCuentas__Emisor CB (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( F.IdEstado = CB.IdEstado and F.IdFarmacia = CB.IdFarmacia ) 
	Inner Join CFDI_Clientes C (NoLock) On ( CB.RFC_Emisor = C.RFC ) 
	Inner Join CFDI__Bancos B (NoLock) On ( CB.RFC_Banco = B.RFC ) 
 

Go--#SQL 

--	select * from vw_FACT_BancosCuentas_Receptor 

--	select * from vw_FACT_BancosCuentas_Emisor  


