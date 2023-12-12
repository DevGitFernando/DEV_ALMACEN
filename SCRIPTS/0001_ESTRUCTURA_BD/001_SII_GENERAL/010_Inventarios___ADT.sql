----------------------------------- Movimientos de Inventario 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_ADT' and xType = 'U' ) 
Begin 
--	Drop Table MovtosInv_ADT 
	Create Table MovtosInv_ADT 
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null,	
		FolioMovtoInv varchar(30) Not Null, 
		FechaRegistro datetime Not Null Default getdate(), 
		HostName varchar(100) Not Null default host_name()  
	)

	Alter Table MovtosInv_ADT Add Constraint PK_MovtosInv_ADT Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv  )

	Alter Table MovtosInv_ADT Add Constraint FK_MovtosInv_ADT___MovtosInv_Enc 
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 
		References MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 

End 
Go--#SQL

	Insert Into MovtosInv_ADT (  IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 
	Select IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv 
	From MovtosInv_Enc M (NoLock) 
	Where Not Exists 
	( 
		Select * 
		From MovtosInv_ADT A (NoLock) 
		Where M.IdEmpresa = A.IdEmpresa and M.IdEstado = A.IdEstado and M.IdFarmacia = A.IdFarmacia and M.FolioMovtoInv = A.FolioMovtoInv 
	) 
Go--#SQL 


-------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'TR_OnUpdate_MovtosInv_ADT' and xType = 'TR' ) 
   Drop Trigger TR_OnUpdate_MovtosInv_ADT 
Go--#S#QL 

Create Trigger TR_OnUpdate_MovtosInv_ADT 
On MovtosInv_ADT 
-- With Encryption 
For Update 
As 
Begin 
	--- Deshacer la actualización de datos  
	Rollback 	--- Enviar el mensaje de error  
	Raiserror ( 'Esta acción no esta permitida.', 1, 1 )  
End  
Go--#S#QL 
