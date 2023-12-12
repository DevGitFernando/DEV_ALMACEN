If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_IGPI_CFGC_Clientes_Terminales' And xType = 'V' )
	Drop view vw_IGPI_CFGC_Clientes_Terminales
Go--#SQL  

Create view vw_IGPI_CFGC_Clientes_Terminales
As
		
	Select CT.IdCliente, T.IdTerminal, T.Nombre, T.MAC_Address, T.EsDeSistema, CT.Asignada, CT.Activa, CT.PuertoDispensacion, CT.Status
	From IGPI_CFGC_Clientes_Terminales CT (NoLock)
	Inner Join IGPI_CFGC_Terminales T(NoLock) On ( CT.IdTerminal = T.IdTerminal )
Go--#SQL  
