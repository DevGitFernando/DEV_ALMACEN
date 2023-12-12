If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_EnvioVersiones' and xType = 'U' ) 
   Drop Table CFG_EnvioVersiones 
Go--#SQL   

Create Table CFG_EnvioVersiones 
( 
	IdEnvio int Identity(1,1),
	NombreTabla varchar(100) Not Null Default '',
	IdOrden int Not Null Default ((0)),
	IdGrupo int Not Null Default ((0)),
	Status varchar(1) Null Default 'A',
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL   

Alter Table CFG_EnvioVersiones Add Constraint PK_CFG_EnvioVersiones Primary Key ( NombreTabla ) 
Go--#SQL   

If Not Exists ( Select * From CFG_EnvioVersiones Where NombreTabla = 'Net_Modulos_Tipos' )  Insert Into CFG_EnvioVersiones (  NombreTabla, IdOrden, IdGrupo, Status, Actualizado )  Values ( 'Net_Modulos_Tipos', 101, 0, 'A', 0 )    Else Update CFG_EnvioVersiones Set IdOrden = 101, IdGrupo = 0, Status = 'A', Actualizado = 0 Where NombreTabla = 'Net_Modulos_Tipos'
If Not Exists ( Select * From CFG_EnvioVersiones Where NombreTabla = 'Net_Modulos' )  Insert Into CFG_EnvioVersiones (  NombreTabla, IdOrden, IdGrupo, Status, Actualizado )  Values ( 'Net_Modulos', 102, 0, 'A', 0 )    Else Update CFG_EnvioVersiones Set IdOrden = 102, IdGrupo = 0, Status = 'A', Actualizado = 0 Where NombreTabla = 'Net_Modulos'
If Not Exists ( Select * From CFG_EnvioVersiones Where NombreTabla = 'Net_Modulos_Relacionados' )  Insert Into CFG_EnvioVersiones (  NombreTabla, IdOrden, IdGrupo, Status, Actualizado )  Values ( 'Net_Modulos_Relacionados', 103, 0, 'A', 0 )    Else Update CFG_EnvioVersiones Set IdOrden = 103, IdGrupo = 0, Status = 'A', Actualizado = 0 Where NombreTabla = 'Net_Modulos_Relacionados'
Go--#SQL   
