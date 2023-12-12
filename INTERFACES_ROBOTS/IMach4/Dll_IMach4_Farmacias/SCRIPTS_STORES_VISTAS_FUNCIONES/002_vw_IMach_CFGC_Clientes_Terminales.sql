
If Exists( Select Name From SysObjects(NoLock) Where Name = 'vw_IMach_CFGC_Clientes_Terminales' And xType = 'V' )
	Drop view vw_IMach_CFGC_Clientes_Terminales
Go

Create view vw_IMach_CFGC_Clientes_Terminales
As
		
	Select CT.IdCliente, T.IdTerminal, T.Nombre, T.MAC_Address, CT.Asignada, CT.Activa, CT.PuertoDispensacion, CT.Status
	From IMach_CFGC_Clientes_Terminales CT (NoLock)
	Inner Join IMach_CFGC_Terminales T(NoLock) On ( CT.IdTerminal = T.IdTerminal )
Go