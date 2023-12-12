----------------------------------------------------------------------------------------------------------------------------------------
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_Adt_Rechazos_Det' and xType = 'U' )
----	Drop Table  COM_Adt_Rechazos_Det
----Go--#xxxSQL

----------------------------------------------------------------------------------------------------------------------------------------
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_Adt_Rechazos_Enc' and xType = 'U' )
----	Drop Table  COM_Adt_Rechazos_Enc
----Go--#xxxSQL	

----------------------------------------------------------------------------------------------------------------------------------------

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_Cat_Rechazos' and xType = 'U' )
Begin  
	Create Table COM_Cat_Rechazos 
	(
		IdRechazo varchar(3) Not Null,	
		Descripcion varchar(200) Not Null,

		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table COM_Cat_Rechazos Add Constraint PK_COM_Cat_Rechazos Primary Key ( IdRechazo )  
	
End 	
Go--#SQL 

	
If Not Exists ( Select * From COM_Cat_Rechazos Where IdRechazo = '000' )  Insert Into COM_Cat_Rechazos (  IdRechazo, Descripcion, Status, Actualizado )  Values ( '000', 'SIN ESPECIFICAR', 'A', 0 )    Else Update COM_Cat_Rechazos Set Descripcion = 'SIN ESPECIFICAR', Status = 'A', Actualizado = 0 Where IdRechazo = '000'
If Not Exists ( Select * From COM_Cat_Rechazos Where IdRechazo = '001' )  Insert Into COM_Cat_Rechazos (  IdRechazo, Descripcion, Status, Actualizado )  Values ( '001', 'DAÑOS EN PRODUCTO', 'A', 0 )    Else Update COM_Cat_Rechazos Set Descripcion = 'DAÑOS EN PRODUCTO', Status = 'A', Actualizado = 0 Where IdRechazo = '001' 
If Not Exists ( Select * From COM_Cat_Rechazos Where IdRechazo = '002' )  Insert Into COM_Cat_Rechazos (  IdRechazo, Descripcion, Status, Actualizado )  Values ( '002', 'PRODUCTO NO SOLICITADO', 'A', 0 )   Else Update COM_Cat_Rechazos Set Descripcion = 'PRODUCTO NO SOLICITADO', Status = 'A', Actualizado = 0 Where IdRechazo = '002'
If Not Exists ( Select * From COM_Cat_Rechazos Where IdRechazo = '003' )  Insert Into COM_Cat_Rechazos (  IdRechazo, Descripcion, Status, Actualizado )  Values ( '003', 'PRODUCTO PROXIMO A CADUCAR', 'A', 0 )    Else Update COM_Cat_Rechazos Set Descripcion = 'PRODUCTO PROXIMO A CADUCAR', Status = 'A', Actualizado = 0 Where IdRechazo = '003'
Go--#SQL	


---------------------------------------------------------------------------------------------------------------------------------------

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_Adt_Rechazos_Enc' and xType = 'U' ) 
Begin  
	Create Table COM_Adt_Rechazos_Enc
	(
		IdEmpresa varchar(3) Not Null,
		IdEstado varchar(2) Not Null,
		IdFarmacia varchar(4) Not Null,
		FolioRechazo varchar(30) Not Null,
		FolioOrden varchar(30) Not Null,		
		IdPersonal varchar(4) Not Null,
		FechaRegistro datetime Not Null Default getdate(),
		NombreRecibeRechazo varchar(100) Not Null Default '',
		Observaciones varchar(200) Not Null Default '',
		TipoProceso int Not Null Default 0, --- se refiere si es 1 = resurtido ó 2 = nota de credito
		FechaResurtido datetime Not Null Default getdate(),
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table COM_Adt_Rechazos_Enc Add Constraint PK_COM_Adt_Rechazos_Enc
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRechazo )	

End 	
Go--#SQL


---------------------------------------------------------------------------------------------------------------------------------------

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_Adt_Rechazos_Det' and xType = 'U' )
Begin  
	Create Table COM_Adt_Rechazos_Det
	(
		IdEmpresa varchar(3) Not Null,
		IdEstado varchar(2) Not Null,
		IdFarmacia varchar(4) Not Null,
		FolioRechazo varchar(30) Not Null,
		IdRechazo varchar(3) Not Null, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table COM_Adt_Rechazos_Det Add Constraint PK_COM_Adt_Rechazos_Det
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRechazo, IdRechazo )	

	Alter Table COM_Adt_Rechazos_Det Add Constraint FK_COM_Adt_Rechazos_Det_COM_Adt_Rechazos_Enc
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioRechazo ) References COM_Adt_Rechazos_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioRechazo ) 

	Alter Table COM_Adt_Rechazos_Det Add Constraint FK_COM_Adt_Rechazos_Det_COM_Cat_Rechazos
		Foreign Key ( IdRechazo ) References COM_Cat_Rechazos ( IdRechazo ) 
	
End 	
Go--#SQL 
	
	



