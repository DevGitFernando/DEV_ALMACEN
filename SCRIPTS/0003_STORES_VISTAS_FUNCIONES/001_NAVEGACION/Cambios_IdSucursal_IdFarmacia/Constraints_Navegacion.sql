
--     Begin tran 

---------------------------------- Eliminar constraints 
If Exists ( Select Name From sysobjects Where Name = 'FK_NetGruposDeUsuarios_Net_Privilegios_Grupo' and xType = 'F' ) 
Alter Table Net_Privilegios_Grupo Drop Constraint FK_NetGruposDeUsuarios_Net_Privilegios_Grupo 
Go--#SQL  

If Exists ( Select Name From sysobjects Where Name = 'PK_Net_Privilegios_Grupo' and xType = 'PK' ) 
Alter Table Net_Privilegios_Grupo Drop Constraint PK_Net_Privilegios_Grupo 
Go--#SQL  



If Exists ( Select Name From sysobjects Where Name = 'FK_NetGruposDeUsuarios_NetGruposUsuariosMiembros' and xType = 'F' ) 
Alter Table Net_Grupos_Usuarios_Miembros Drop Constraint FK_NetGruposDeUsuarios_NetGruposUsuariosMiembros 
Go--#SQL  

If Exists ( Select Name From sysobjects Where Name = 'FK_Net_Usuarios_NetGruposUsuariosMiembros' and xType = 'F' ) 
Alter Table Net_Grupos_Usuarios_Miembros Drop Constraint FK_Net_Usuarios_NetGruposUsuariosMiembros 
Go--#SQL  

If Exists ( Select Name From sysobjects Where Name = 'PK_Net_Grupos_Usuarios_Miembros' and xType = 'PK' ) 
Alter Table Net_Grupos_Usuarios_Miembros Drop Constraint PK_Net_Grupos_Usuarios_Miembros 
Go--#SQL 	



If Exists ( Select Name From sysobjects Where Name = 'FK_NetGruposDeUsuarios_NetGruposUsuariosMiembros' and xType = 'F' ) 
Alter Table Net_Privilegios_Grupo Drop Constraint FK_NetGruposDeUsuarios_Net_Privilegios_Grupo 
Go--#SQL  

If Exists ( Select Name From sysobjects Where Name = 'PK_Net_Privilegios_Grupo' and xType = 'PK' ) 
Alter Table Net_Privilegios_Grupo Drop Constraint PK_Net_Privilegios_Grupo 
Go--#SQL 


If Exists ( Select Name From sysobjects Where Name = 'FK_CatFarmacias_NetGruposDeUsuarios' and xType = 'F' ) 
Alter Table Net_Grupos_De_Usuarios Drop Constraint FK_CatFarmacias_NetGruposDeUsuarios 
Go--#SQL  


If Exists ( Select Name From sysobjects Where Name = 'PK_Net_Grupos_De_Usuarios' and xType = 'PK' ) 
Alter Table Net_Grupos_De_Usuarios Drop Constraint PK_Net_Grupos_De_Usuarios 
Go--#SQL 


If Exists ( Select Name From sysobjects Where Name = 'FK_CatPersonal_Net_Usuarios' and xType = 'F' ) 	
Alter Table Net_Usuarios Drop Constraint FK_CatPersonal_Net_Usuarios 
Go--#SQL  

If Exists ( Select Name From sysobjects Where Name = 'Pk_Net_Usuarios' and xType = 'PK' ) 
Alter Table Net_Usuarios Drop Constraint Pk_Net_Usuarios 
Go--#SQL 

-------------------------------------------- Renombrar Campos 
    Exec sp_rename 'dbo.Net_Usuarios.IdSucursal', 'IdFarmacia', 'COLUMN'  
    Exec sp_rename 'dbo.Net_Grupos_De_Usuarios.IdSucursal', 'IdFarmacia', 'COLUMN'  
    Exec sp_rename 'dbo.Net_Grupos_Usuarios_Miembros.IdSucursal', 'IdFarmacia', 'COLUMN'  
    Exec sp_rename 'dbo.Net_Privilegios_Grupo.IdSucursal', 'IdFarmacia', 'COLUMN'    
-------------------------------------------- Renombrar Campos 



---------------------------------- Crear constraints 
---------------------------------- Crear constraints 
If Not Exists ( Select Name From sysobjects Where Name = 'Pk_Net_Usuarios' and xType = 'PK' ) 
Alter Table Net_Usuarios Add Constraint Pk_Net_Usuarios 
    Primary Key ( IdEstado, IdFarmacia, IdPersonal )	
Go--#SQL 

If Not Exists ( Select Name From sysobjects Where Name = 'FK_CatPersonal_Net_Usuarios' and xType = 'F' ) 	
Alter Table Net_Usuarios Add Constraint FK_CatPersonal_Net_Usuarios 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References CatPersonal ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  



If Not Exists ( Select Name From sysobjects Where Name = 'PK_Net_Grupos_De_Usuarios' and xType = 'PK' ) 
Alter Table Net_Grupos_De_Usuarios Add Constraint PK_Net_Grupos_De_Usuarios 
    Primary Key ( IdEstado, IdFarmacia, IdGrupo )
Go--#SQL 

If Not Exists ( Select Name From sysobjects Where Name = 'FK_CatFarmacias_NetGruposDeUsuarios' and xType = 'F' ) 
Alter Table Net_Grupos_De_Usuarios Add Constraint FK_CatFarmacias_NetGruposDeUsuarios 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  



If Not Exists ( Select Name From sysobjects Where Name = 'PK_Net_Grupos_Usuarios_Miembros' and xType = 'PK' ) 
Alter Table Net_Grupos_Usuarios_Miembros Add Constraint PK_Net_Grupos_Usuarios_Miembros 
    Primary Key ( IdEstado, IdFarmacia, IdGrupo, IdPersonal )  
Go--#SQL 	

If Not Exists ( Select Name From sysobjects Where Name = 'FK_Net_Usuarios_NetGruposUsuariosMiembros' and xType = 'F' ) 
Alter Table Net_Grupos_Usuarios_Miembros Add Constraint FK_Net_Usuarios_NetGruposUsuariosMiembros 
	Foreign Key ( IdEstado, IdFarmacia, IdPersonal ) References Net_Usuarios ( IdEstado, IdFarmacia, IdPersonal ) 
Go--#SQL  

If Not Exists ( Select Name From sysobjects Where Name = 'FK_NetGruposDeUsuarios_NetGruposUsuariosMiembros' and xType = 'F' ) 
Alter Table Net_Grupos_Usuarios_Miembros Add Constraint FK_NetGruposDeUsuarios_NetGruposUsuariosMiembros 
	Foreign Key ( IdEstado, IdFarmacia, IdGrupo ) References Net_Grupos_De_Usuarios ( IdEstado, IdFarmacia, IdGrupo ) 
Go--#SQL  


If Not Exists ( Select Name From sysobjects Where Name = 'PK_Net_Privilegios_Grupo' and xType = 'PK' ) 
Alter Table Net_Privilegios_Grupo Add Constraint PK_Net_Privilegios_Grupo 
    Primary Key ( IdEstado, IdFarmacia, IdGrupo, Arbol, Rama )	
Go--#SQL  

If Not Exists ( Select Name From sysobjects Where Name = 'FK_NetGruposDeUsuarios_NetGruposUsuariosMiembros' and xType = 'F' ) 
Alter Table Net_Privilegios_Grupo Add Constraint FK_NetGruposDeUsuarios_Net_Privilegios_Grupo 
	Foreign Key ( IdEstado, IdFarmacia, IdGrupo ) References Net_Grupos_De_Usuarios ( IdEstado, IdFarmacia, IdGrupo ) 
Go--#SQL  


---     rollback tran 


---     commit tran 
