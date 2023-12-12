
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_ADMI_Impresion_Productos_Compras_Principal' And xType = 'P' )
	Drop Proc spp_ADMI_Impresion_Productos_Compras_Principal
Go--#SQL

Create Procedure spp_ADMI_Impresion_Productos_Compras_Principal ( @IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '25', 
	@IdFarmacia varchar(4) = '0012', @FechaInicial varchar(10) = '2010-01-01', @FechaFinal varchar(10) = '2011-01-31' )
With Encryption
As
Begin

	--------------------------------------------------
	-- Se Ejecuta cada uno de los Sps para reporteo --
	--------------------------------------------------	

	Exec spp_ADMI_Impresion_Productos_Compras_Estado @IdEmpresa, @IdEstado, @IdFarmacia, @FechaInicial, @FechaFinal

	Exec spp_ADMI_Impresion_Productos_Porcentajes_Compras

	----------------------------------------------------------------------
	-- El resultado de cada consulta se guarda en las siguientes tablas --
	----------------------------------------------------------------------
	--Select * From tmpADMI_Productos_Movimientos(NoLock)
	--Select * From tmpADMI_Productos_Compras(NoLock)
	--Select * From tmpADMI_Productos_TransferenciaEntrada(NoLock)
	--Select * From tmpADMI_Productos_TransferenciaSalida(NoLock)
	--Select * From tmpADMI_Productos_EntradaError(NoLock)
	--Select * From tmpADMI_Productos_SalidaError(NoLock)
	--Select * From tmpADMI_Productos_SalidaCaducado(NoLock)

End
Go--#SQL


