---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_CFG_EX_Validacion_Titulos' and xType = 'V' ) 
   Drop View vw_CFG_EX_Validacion_Titulos 
Go--#SQL    

Create View vw_CFG_EX_Validacion_Titulos 
As 

	Select 
		T.IdEstado, 
		T.IdCliente, C.NombreCliente as Cliente, T.Cliente as ClienteTitulo, 
		T.IdSubCliente, C.NombreSubCliente as SubCliente, T.SubCliente as SubClienteTitulo, 
		T.IdPrograma, P.Programa, T.Programa as ProgramaTitulo, T.IdSubPrograma, P.SubPrograma, T.SubPrograma as SubProgramaTitulo, 
		(case when T.Status = 'A' Then 'SI' Else 'NO' End) as Status, 
		cast((case when T.Status = 'A' Then 1 else 0 end) as bit) as Activo
	From CFG_EX_Validacion_Titulos T 
	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	Inner Join vw_Clientes_SubClientes C On ( T.IdCliente = C.IdCliente and T.IdSubCliente = C.IdSubCliente ) 
	Inner Join vw_Programas_SubProgramas P On ( T.IdPrograma = P.IdPrograma and T.IdSubPrograma = P.IdSubPrograma ) 
	
--	sp_listacolumnas CFG_EX_Validacion_Titulos 
	
Go--#SQL 	


---------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From sysobjects (NoLock) Where Name = 'vw_CFG_EX_Validacion_Titulos_Beneficiarios' and xType = 'V' ) 
   Drop View vw_CFG_EX_Validacion_Titulos_Beneficiarios 
Go--#SQL 

Create View vw_CFG_EX_Validacion_Titulos_Beneficiarios 
As 

	Select 
		T.IdEstado, 
		T.IdCliente, C.NombreCliente as Cliente, 
		T.IdSubCliente, C.NombreSubCliente as SubCliente, 
		T.IdPrograma, P.Programa, T.IdSubPrograma, P.SubPrograma,  
		T.FolioReferencia, 
		(case when T.ReemplazarFolioReferencia = 1 Then 'SI' Else 'NO' End) as ReemplazarFolioReferencia, 
		T.Beneficiario, 
		(case when T.ReemplazarBeneficiario = 1 Then 'SI' Else 'NO' End) as ReemplazarBeneficiario, 		
		(case when T.Status = 'A' Then 'SI' Else 'NO' End) as Status, 
		cast((case when T.Status = 'A' Then 1 else 0 end) as bit) as Activo
	From CFG_EX_Validacion_Titulos_Beneficiarios T 
	Inner Join CatEstados E (NoLock) On ( T.IdEstado = E.IdEstado ) 
	Inner Join vw_Clientes_SubClientes C On ( T.IdCliente = C.IdCliente and T.IdSubCliente = C.IdSubCliente ) 
	Inner Join vw_Programas_SubProgramas P On ( T.IdPrograma = P.IdPrograma and T.IdSubPrograma = P.IdSubPrograma ) 
	
--	sp_listacolumnas CFG_EX_Validacion_Titulos 
	
Go--#SQL 	

