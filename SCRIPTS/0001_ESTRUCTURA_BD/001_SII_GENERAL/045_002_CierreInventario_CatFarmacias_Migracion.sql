If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatFarmacias_Migracion' and xType = 'U' )
	Drop Table CatFarmacias_Migracion 
Go--#SQL 

Create Table CatFarmacias_Migracion 
(
	IdEmpresa varchar(3) Not Null,
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,
	IdEmpresaNueva varchar(3) Not Null,
	IdFarmaciaNueva varchar(4) Not Null, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table CatFarmacias_Migracion Add Constraint PK_CatFarmacias_Migracion Primary Key ( IdEmpresa, IdEstado, IdFarmacia )
Go--#SQL 

Alter Table CatFarmacias_Migracion Add Constraint FK_CatFarmacias_Migracion_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0043' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0043', '001', '0043', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0043', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0043'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0044' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0044', '001', '0044', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0044', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0044'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0103' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0103', '001', '0103', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0103', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0103'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0104' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0104', '001', '0104', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0104', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0104'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0105' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0105', '001', '0105', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0105', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0105'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0106' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0106', '001', '0106', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0106', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0106'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0107' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0107', '001', '0107', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0107', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0107'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0108' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0108', '001', '0108', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0108', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0108'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0109' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0109', '001', '0109', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0109', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0109'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0110' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0110', '001', '0110', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0110', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0110'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0111' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0111', '001', '0111', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0111', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0111'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0112' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0112', '001', '0112', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0112', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0112'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0113' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0113', '001', '0113', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0113', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0113'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0114' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0114', '001', '0114', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0114', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0114'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0115' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0115', '001', '0115', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0115', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0115'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0116' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0116', '001', '0116', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0116', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0116'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0117' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0117', '001', '0117', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0117', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0117'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0118' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0118', '001', '0118', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0118', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0118'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0119' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0119', '001', '0119', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0119', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0119'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0120' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0120', '001', '0120', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0120', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0120'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0121' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0121', '001', '0121', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0121', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0121'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0122' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0122', '001', '0122', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0122', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0122'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0123' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0123', '001', '0123', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0123', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0123'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0124' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0124', '001', '0124', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0124', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0124'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0125' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0125', '001', '0125', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0125', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0125'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0126' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0126', '001', '0126', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0126', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0126'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0127' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0127', '001', '0127', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0127', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0127'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0128' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0128', '001', '0128', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0128', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0128'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0129' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0129', '001', '0129', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0129', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0129'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0130' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0130', '001', '0130', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0130', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0130'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0131' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0131', '001', '0131', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0131', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0131'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0132' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0132', '001', '0132', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0132', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0132'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0133' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0133', '001', '0133', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0133', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0133'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0134' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0134', '001', '0134', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0134', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0134'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0135' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0135', '001', '0135', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0135', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0135'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0136' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0136', '001', '0136', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0136', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0136'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0137' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0137', '001', '0137', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0137', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0137'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0138' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0138', '001', '0138', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0138', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0138'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0139' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0139', '001', '0139', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0139', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0139'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0140' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0140', '001', '0140', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0140', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0140'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0141' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0141', '001', '0141', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0141', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0141'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0142' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0142', '001', '0142', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0142', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0142'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0143' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0143', '001', '0143', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0143', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0143'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0144' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0144', '001', '0144', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0144', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0144'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0145' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0145', '001', '0145', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0145', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0145'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0146' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0146', '001', '0146', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0146', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0146'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0147' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0147', '001', '0147', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0147', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0147'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0148' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0148', '001', '0148', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0148', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0148'  
If Not Exists ( Select * From CatFarmacias_Migracion Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0149' )  Insert Into CatFarmacias_Migracion (  IdEmpresa, IdEstado, IdFarmacia, IdEmpresaNueva, IdFarmaciaNueva, Status, Actualizado )  Values ( '002', '20', '0149', '001', '0149', 'A', 0 )    Else Update CatFarmacias_Migracion Set IdEmpresaNueva = '001', IdFarmaciaNueva = '0149', Status = 'A', Actualizado = 0 Where IdEmpresa = '002' and IdEstado = '20' and IdFarmacia = '0149'  
Go--#SQL 