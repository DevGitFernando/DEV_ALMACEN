If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'vw_Clientes_Programas_Asignados_Unidad' and xType = 'V' ) 
   Drop View vw_Clientes_Programas_Asignados_Unidad
Go--#SQL 	
    

Create View vw_Clientes_Programas_Asignados_Unidad 
As 
	Select C.IdEstado, F.Estado, C.IdFarmacia, F.Farmacia, 
	     C.IdCliente, Cs.NombreCliente, C.IdSubCliente, Cs.NombreSubCliente, 
	     C.IdPrograma, P.Programa, C.IdSubPrograma, P.SubPrograma, C.Status as StatusAsignacion      
	From CFG_EstadosFarmaciasProgramasSubProgramas C (NoLock) 
	Inner Join vw_Farmacias F (NoLock) On ( C.IdEstado = F.IdEstado and C.IdFarmacia = F.IdFarmacia ) 
	Inner Join vw_Clientes_SubClientes Cs (NoLock) On ( C.IdCliente = Cs.IdCliente and C.IdSubCliente = Cs.IdSubCliente )
	Inner Join vw_Programas_SubProgramas P (NoLock) On ( C.IdPrograma = P.IdPrograma and C.IdSubPrograma = P.IdSubPrograma )
	
Go--#SQL	
 	