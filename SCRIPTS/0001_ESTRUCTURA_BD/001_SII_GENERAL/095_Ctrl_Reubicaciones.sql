-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctrl_Reubicaciones' and xType = 'U' ) 
Begin 
	Create Table Ctrl_Reubicaciones 
	(
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		Folio_Inv varchar(30) Not Null,
		FolioMovto_Referencia varchar(30) Not Null,
		Status varchar(1) Not Null Default 'P',
		IdPersonal varchar(4) Not Null Default '',
		IdPersonal_Asignado varchar(4) Not Null Default '',
		IdPersonal_Firma varchar(8) Not Null Default '',
		IdPersonal_Autoriza_Extraordinario varchar(8) Not Null Default '',
		FechaConfirmacion datetime Not Null Default GetDate(),
		FechaRegistro datetime Not Null Default GetDate(),
		FechaControl DateTime Not Null Default getdate(),
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table Ctrl_Reubicaciones Add Constraint PK_Ctrl_Reubicaciones Primary Key ( IdEmpresa, IdEstado, IdFarmacia, Folio_Inv ) 

	Alter Table Ctrl_Reubicaciones Add Constraint FK_Ctrl_Reubicaciones_CatEmpresas
		Foreign Key ( IdEmpresa )  References CatEmpresas

	Alter Table Ctrl_Reubicaciones Add Constraint FK_Ctrl_Reubicaciones_CatFarmacias
		Foreign Key ( IdEstado, IdFarmacia )  References CatFarmacias

	Alter Table Ctrl_Reubicaciones Add Constraint FK_Ctrl_Reubicaciones_CatPersonal
		Foreign Key ( IdEstado, IdFarmacia, IdPersonal )  References CatPersonal

	Alter Table Ctrl_Reubicaciones Add Constraint FK_Ctrl_Reubicaciones_CatPersonalCEDIS
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdPersonal_Asignado )  References CatPersonalCEDIS

End 
Go--#SQL 
-----------------------


-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_Ctrl_Reubicaciones' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_Ctrl_Reubicaciones 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Ctrl_Reubicaciones' and xType = 'U' ) 
Begin Declare @sSql varchar(max) 
Set @sSql = ''Set @sSql = 
     '
     Create Trigger TR_OnUpdate_Ctrl_Reubicaciones 
     On Ctrl_Reubicaciones 
     With Encryption 
     For Update  
     As 
     Begin 
                     If Update(FechaControl) 
             Begin 
                --- Deshacer la actualización de datos  
                Rollback 
                              --- Enviar el mensaje de error 
                Raiserror (''Esta acción no esta permitida para FechaControl'', 1, 1) 
             End 
          Else 
             Begin 
                Update U Set FechaControl = getdate() 
                From Ctrl_Reubicaciones U 
                Inner Join Inserted I 
             		On ( I.IdEmpresa = U.IdEmpresa And I.IdEstado = U.IdEstado and I.IdFarmacia = U.IdFarmacia and I.Folio_Inv = U.Folio_Inv  ) 
             End 
     End 
     '
Exec ( @sSql ) 
End 
Go--#SQL 



--Select * From Inserted