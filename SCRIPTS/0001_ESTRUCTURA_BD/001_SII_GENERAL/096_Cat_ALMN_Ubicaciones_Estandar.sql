
Set Dateformat YMD 

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Cat_ALMN_Ubicaciones_Estandar' and xType = 'U' ) 
Begin  
	Create Table Cat_ALMN_Ubicaciones_Estandar 
	(
		NombrePosicion varchar(100) Not Null,
		Descripcion varchar(500) Not Null,
		FechaRegistro datetime Not Null Default GetDate(), 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table Cat_ALMN_Ubicaciones_Estandar Add Constraint PK_Cat_ALMN_Ubicaciones_Estandar Primary Key ( NombrePosicion ) 

End  
Go--#SQL 


Set Dateformat YMD 

If Not Exists ( Select * From Cat_ALMN_Ubicaciones_Estandar Where NombrePosicion = 'COMPRAS_DIRECTAS' )  Insert Into Cat_ALMN_Ubicaciones_Estandar (  NombrePosicion, Descripcion, FechaRegistro, Status, Actualizado )  Values ( 'COMPRAS_DIRECTAS', 'DETERMINA LA POSICION DONDE SE UBICARAN LAS COMPRAS DIRECTAS', '2015-10-06 11:35:01', 'A', 0 )    Else Update Cat_ALMN_Ubicaciones_Estandar Set Descripcion = 'DETERMINA LA POSICION DONDE SE UBICARAN LAS COMPRAS DIRECTAS', FechaRegistro = '2015-10-06 11:35:01', Status = 'A', Actualizado = 0 Where NombrePosicion = 'COMPRAS_DIRECTAS'
If Not Exists ( Select * From Cat_ALMN_Ubicaciones_Estandar Where NombrePosicion = 'ENTRADAS_CONSIGNA' )  Insert Into Cat_ALMN_Ubicaciones_Estandar (  NombrePosicion, Descripcion, FechaRegistro, Status, Actualizado )  Values ( 'ENTRADAS_CONSIGNA', 'DETERMINA LA POSICION DONDE SE UBICARAN LAS ENTRADAS DE CONSIGNACION.', '2015-10-06 11:36:30', 'A', 0 )    Else Update Cat_ALMN_Ubicaciones_Estandar Set Descripcion = 'DETERMINA LA POSICION DONDE SE UBICARAN LAS ENTRADAS DE CONSIGNACION.', FechaRegistro = '2015-10-06 11:36:30', Status = 'A', Actualizado = 0 Where NombrePosicion = 'ENTRADAS_CONSIGNA'
If Not Exists ( Select * From Cat_ALMN_Ubicaciones_Estandar Where NombrePosicion = 'ORDEN_COMPRAS' )  Insert Into Cat_ALMN_Ubicaciones_Estandar (  NombrePosicion, Descripcion, FechaRegistro, Status, Actualizado )  Values ( 'ORDEN_COMPRAS', 'DETERMINA LA POSICION DONDE SE UBICARAN LAS ORDENES DE COMPRAS RECIBIDAS', '2015-10-06 11:35:38', 'A', 0 )    Else Update Cat_ALMN_Ubicaciones_Estandar Set Descripcion = 'DETERMINA LA POSICION DONDE SE UBICARAN LAS ORDENES DE COMPRAS RECIBIDAS', FechaRegistro = '2015-10-06 11:35:38', Status = 'A', Actualizado = 0 Where NombrePosicion = 'ORDEN_COMPRAS'
If Not Exists ( Select * From Cat_ALMN_Ubicaciones_Estandar Where NombrePosicion = 'TE_INTERESTATALES' )  Insert Into Cat_ALMN_Ubicaciones_Estandar (  NombrePosicion, Descripcion, FechaRegistro, Status, Actualizado )  Values ( 'TE_INTERESTATALES', 'DETERMINA LA POSICION DONDE SERAN UBICADAS LAS TRANSFERENCIAS DE ENTRADA INTERESTATALES.', '2015-10-06 16:13:37', 'A', 0 )    Else Update Cat_ALMN_Ubicaciones_Estandar Set Descripcion = 'DETERMINA LA POSICION DONDE SERAN UBICADAS LAS TRANSFERENCIAS DE ENTRADA INTERESTATALES.', FechaRegistro = '2015-10-06 16:13:37', Status = 'A', Actualizado = 0 Where NombrePosicion = 'TE_INTERESTATALES'
If Not Exists ( Select * From Cat_ALMN_Ubicaciones_Estandar Where NombrePosicion = 'TRANSFERENCIAS_ENTRADA' )  Insert Into Cat_ALMN_Ubicaciones_Estandar (  NombrePosicion, Descripcion, FechaRegistro, Status, Actualizado )  Values ( 'TRANSFERENCIAS_ENTRADA', 'DETERMINA LA POSICION DONDE SE UBICARAN LAS TRANSFERENCIAS DE ENTRADA.', '2015-10-06 11:37:48', 'A', 0 )    Else Update Cat_ALMN_Ubicaciones_Estandar Set Descripcion = 'DETERMINA LA POSICION DONDE SE UBICARAN LAS TRANSFERENCIAS DE ENTRADA.', FechaRegistro = '2015-10-06 11:37:48', Status = 'A', Actualizado = 0 Where NombrePosicion = 'TRANSFERENCIAS_ENTRADA'
If Not Exists ( Select * From Cat_ALMN_Ubicaciones_Estandar Where NombrePosicion = 'TS_DEVOLUCION' )  Insert Into Cat_ALMN_Ubicaciones_Estandar (  NombrePosicion, Descripcion, FechaRegistro, Status, Actualizado )  Values ( 'TS_DEVOLUCION', 'DETERMINA LA POSICION DONDE SE UBICARAN LAS DEVOLUCIONES DE TRANSFERENCIAS', '2015-09-28 12:25:57', 'A', 0 )    Else Update Cat_ALMN_Ubicaciones_Estandar Set Descripcion = 'DETERMINA LA POSICION DONDE SE UBICARAN LAS DEVOLUCIONES DE TRANSFERENCIAS', FechaRegistro = '2015-09-28 12:25:57', Status = 'A', Actualizado = 0 Where NombrePosicion = 'TS_DEVOLUCION'
If Not Exists ( Select * From Cat_ALMN_Ubicaciones_Estandar Where NombrePosicion = 'VTA_DEVOLUCION' )  Insert Into Cat_ALMN_Ubicaciones_Estandar (  NombrePosicion, Descripcion, FechaRegistro, Status, Actualizado )  Values ( 'VTA_DEVOLUCION', 'DETERMINA LA POSICION DONDE SERAN UBICADAS LAS DEVOLUCIONES DE VENTA', '2015-09-28 12:18:38', 'A', 0 )    Else Update Cat_ALMN_Ubicaciones_Estandar Set Descripcion = 'DETERMINA LA POSICION DONDE SERAN UBICADAS LAS DEVOLUCIONES DE VENTA', FechaRegistro = '2015-09-28 12:18:38', Status = 'A', Actualizado = 0 Where NombrePosicion = 'VTA_DEVOLUCION'
Go--#SQL
		
	