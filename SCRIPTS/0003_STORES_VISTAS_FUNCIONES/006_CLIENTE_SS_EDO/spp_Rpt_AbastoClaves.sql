/* 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'CFG_Claves_Excluir_NivelAbasto' and xType = 'U' )
    Drop Table CFG_Claves_Excluir_NivelAbasto 
Go--#xSQL


Create Table CFG_Claves_Excluir_NivelAbasto 
(
    IdEstado varchar(2) Not Null, 
    IdCliente varchar(4) Not Null, 
    IdClaveSSA varchar(4) Not Null, 
    Status varchar(1) Not Null Default 'A', 
    Actualizado tinyint Not Null Default 0 
) 
Go--#xSQL

Alter Table CFG_Claves_Excluir_NivelAbasto 
    Add Constraint PK_CFG_Claves_Excluir_NivelAbasto Primary Key ( IdEstado, IdCliente, IdClaveSSA ) 
Go--#xSQL
*/ 

/* 
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0023' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0023', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0023'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0124' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0124', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0124'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0125' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0125', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0125'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0145' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0145', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0145'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0208' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0208', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0208'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0252' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0252', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0252'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0315' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0315', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0315'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0368' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0368', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0368'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0512' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0512', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0512'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0519' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0519', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0519'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0523' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0523', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0523'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0524' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0524', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0524'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0525' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0525', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0525'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0560' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0560', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0560'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0707' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0707', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0707'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0709' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0709', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0709'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0735' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0735', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0735'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0785' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0785', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0785'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0837' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0837', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0837'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0915' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0915', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0915'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0917' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0917', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0917'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0920' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0920', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0920'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0929' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0929', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0929'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0935' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '0935', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '0935'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1067' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1067', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1067'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1069' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1069', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1069'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1117' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1117', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1117'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1252' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1252', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1252'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1253' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1253', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1253'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1282' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1282', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1282'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1288' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1288', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1288'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1289' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1289', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1289'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1294' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1294', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1294'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1319' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1319', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1319'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1328' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1328', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1328'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1382' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1382', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1382'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1384' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1384', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1384'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1440' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1440', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1440'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1477' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1477', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1477'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1548' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1548', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1548'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1552' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1552', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1552'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1621' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1621', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1621'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1643' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1643', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1643'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1645' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1645', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1645'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1758' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1758', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1758'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1760' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1760', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1760'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1913' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1913', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1913'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1918' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1918', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1918'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1951' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1951', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1951'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1952' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '1952', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '1952'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '2007' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '2007', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '2007'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '2101' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '2101', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '2101'
If Not Exists ( Select * From CFG_Claves_Excluir_NivelAbasto Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '2102' )  Insert Into CFG_Claves_Excluir_NivelAbasto (  IdEstado, IdCliente, IdClaveSSA, Status, Actualizado )  Values ( '21', '0002', '2102', 'A', 1 )    Else Update CFG_Claves_Excluir_NivelAbasto Set Status = 'A', Actualizado = 1 Where IdEstado = '21' and IdCliente = '0002' and IdClaveSSA = '2102' 


*/ 

-------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From SysObjects(NoLock) Where Name = 'spp_Rpt_AbastoClaves_Global' and xType = 'P' )
    Drop Proc spp_Rpt_AbastoClaves_Global 
Go--#SQL
  
Create Proc spp_Rpt_AbastoClaves_Global ( @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '*' )
With Encryption 
As
Begin 
Set NoCount On 

Declare @EncPrincipal varchar(500), 
		@EncSecundario varchar(500), 
		@Claves numeric(14,4), @Abasto numeric(14,4),
		@PorcAbasto numeric(14,4), 
		@Tipo int  
	 
		Set @Tipo = 1 
		Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 	

------- Generar Perifil de Farmacia 
        Select IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave 
        Into #tmpPerfil 
        From vw_CB_CuadroBasico_Farmacias  
        Where IdEstado = @IdEstado --and IdFarmacia = @IdFarmacia 
			  and StatusClave = 'A' 
		
		If @IdFarmacia <> '*'  
		Begin 
		   Set @Tipo = 2 
		   Delete From #tmpPerfil Where IdFarmacia <> @IdFarmacia 
		End 
		   		
--      spp_Rpt_AbastoClaves_Aux 

------------------------------------------- Calcular Abasto 
		If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpRptAbastoClaves' and xType = 'U' )
		    Drop Table #tmpRptAbastoClaves
		   
	   
	    Select  @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
				IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave, sum(Existencia) as Existencia, 
	           (case when sum(Existencia) > 0 then '1' else '0' end) Abasto, Cast(0 As Numeric(14,4)) as PorcAbasto  
        Into #tmpRptAbastoClaves 
	    From 
	    ( 
		    Select  P.IdEstado, P.Estado, P.IdFarmacia, -- Cast( '' As varchar(50)) As Farmacia, 
		            P.Farmacia, 
		            P.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, IsNull(C.Existencia, 0) As Existencia,
		            Case When C.Existencia > 0 Then '1' Else '0' End As Abasto, Cast( 0 As Numeric(14,4)) As PorcAbasto 
		    From #tmpPerfil P (Nolock)
		    Left Join SVR_INV_Generar_Existencia_Concentrado C (Nolock) 
			    On ( P.IdEstado = C.IdEstado And P.IdFarmacia = C.IdFarmacia And P.ClaveSSA = C.ClaveSSA  )
		    -- Where P.IdEstado = @IdEstado 
		) as T 
		group by IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave 
------------------------------------------- Calcular Abasto 


----    Excepciones 
--        select T.* 
--        From #tmpRptAbastoClaves T 
--        Inner Join CFG_Claves_Excluir_NivelAbasto E On ( E.IdEstado = @IdEstado and T.IdClaveSSA = E.IdClaveSSA )
--        Where Abasto = 0 
        
        Update T Set Abasto = 2 
        From #tmpRptAbastoClaves T 
        Inner Join CFG_Claves_Excluir_NivelAbasto E On ( E.IdEstado = @IdEstado and T.IdClaveSSA = E.IdClaveSSA )

	
--------------    Calcular los abastos 		
----------		Set @Claves = (	Select Cast(Count(*) As Numeric(14,4)) From #tmpPerfil (Nolock)
----------						Where IdEstado = @IdEstado ) 
----------		Set @Abasto = ( Select Cast(Count(*) As Numeric(14,4)) From #tmpRptAbastoClaves (Nolock)
----------						Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Abasto in ( 1, 2 ) ) 
----------
----------		-- Set @PorcAbasto = ( Select ((@Abasto/@Claves) * 100) ) 
----------		Set @PorcAbasto = (case when @Claves = 0 then 0 else ((@Abasto/@Claves) * 100) end ) 	
	

----------		Update #tmpRptAbastoClaves Set PorcAbasto = @PorcAbasto 
----------		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	

--- SALIDA FINAL	
	Select IdEstado, Estado, IdFarmacia, Farmacia, '' as Url, '' as Juris, 1 as Procesar, 
		   FechaReporte, 
		   TotalClaves, ConExistencia, SinExistencia, 
		   cast(round((case when ConExistencia = 0 then 0 else (ConExistencia/(TotalClaves*1.0)) * 100 end), 4) as numeric(14,4)) as PorcAbasto 
	Into #tmp_Final 	   
	From 
	( 
		Select IdEstado, Estado, IdFarmacia, Farmacia, getdate() as FechaReporte, 
			Count(*) As TotalClaves, 
			( Select Count(Abasto) From #tmpRptAbastoClaves X (Nolock) 
				Where X.IdEstado = T.IdEstado and X.IdFarmacia = T.IdFarmacia and Abasto in ( 1, 2 ) ) As ConExistencia, 
			( Select Count(Abasto) From #tmpRptAbastoClaves Y (Nolock) 
				Where Y.IdEstado = T.IdEstado and Y.IdFarmacia = T.IdFarmacia and Abasto = 0 ) As SinExistencia, 
			max(PorcAbasto) as PorcAbasto 
		From #tmpRptAbastoClaves T (Nolock) 
		-- Where 
		Group By IdEstado, Estado, IdFarmacia, Farmacia 
	) as D 	
	Order By IdEstado, IdFarmacia 

	If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'tmpRptAbastoClaves' and xType = 'U' )
	    Drop Table tmpRptAbastoClaves

	Select * 
	Into tmpRptAbastoClaves 
	From #tmpRptAbastoClaves 
--- SALIDA FINAL	


	IF @Tipo = 1 
	Begin 
		Select 
			IdEstado, Estado, IdFarmacia, Farmacia, Url, Juris, Procesar, 
			FechaReporte, TotalClaves, ConExistencia, SinExistencia, PorcAbasto 
		From #tmp_Final		
	End 
	Else 
	Begin
		Select TotalClaves, ConExistencia, SinExistencia, PorcAbasto, FechaReporte   
		From #tmp_Final				 
	End 	


End 
Go--#SQL


-------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From SysObjects(NoLock) Where Name = 'spp_Rpt_AbastoClaves' and xType = 'P' )
    Drop Proc spp_Rpt_AbastoClaves
Go--#SQL

Create Proc spp_Rpt_AbastoClaves ( @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0053', @Consulta int = 1 )
With Encryption 
As
Begin 
Set NoCount On 

	Exec spp_Rpt_AbastoClaves_Global @IdEstado, @IdFarmacia 

End 
Go--#SQL 

/*   
Create Proc spp_Rpt_AbastoClaves ( @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '0053', @Consulta int = 1 )
With Encryption 
As
Begin 
Set NoCount On

Declare @EncPrincipal varchar(500), 
		@EncSecundario varchar(500),
		@Claves numeric(14,4), @Abasto numeric(14,4),
		@PorcAbasto numeric(14,4)
	 

		Select @EncPrincipal = EncabezadoPrincipal, @EncSecundario = EncabezadoSecundario From dbo.fg_Unidad_EncabezadoReportesClientesSSA() 	

------- Generar Perifil de Farmacia 
        Select IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave 
        Into #tmpPerfil 
        From vw_CB_CuadroBasico_Farmacias  
        Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and StatusClave = 'A'


------------------------------------------- Calcular Abasto 
		If Exists ( Select Name From Sysobjects (NoLock) Where Name = '#tmpRptAbastoClaves' and xType = 'U' )
		    Drop Table #tmpRptAbastoClaves
		   
	   
	    Select  @EncPrincipal as EncabezadoPrincipal, @EncSecundario as EncabezadoSecundario, 
				IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave, sum(Existencia) as Existencia, 
	           (case when sum(Existencia) > 0 then '1' else '0' end) Abasto, Cast(0 As Numeric(14,4)) as PorcAbasto 
        Into #tmpRptAbastoClaves 
	    From 
	    ( 
		    Select  P.IdEstado, P.Estado, P.IdFarmacia, -- Cast( '' As varchar(50)) As Farmacia, 
		            P.Farmacia, 
		            P.IdClaveSSA, P.ClaveSSA, P.DescripcionClave, IsNull(C.Existencia, 0) As Existencia,
		            Case When C.Existencia > 0 Then '1' Else '0' End As Abasto, Cast( 0 As Numeric(14,4)) As PorcAbasto 
		    From #tmpPerfil P (Nolock)
		    Left Join vw_ExistenciaPorSales C (Nolock) 
			    On ( P.IdEstado = C.IdEstado And P.IdFarmacia = C.IdFarmacia And P.ClaveSSA = C.ClaveSSA  )
		    -- Where P.IdEstado = @IdEstado 
		) as T 
		group by IdEstado, Estado, IdFarmacia, Farmacia, IdClaveSSA, ClaveSSA, DescripcionClave 
------------------------------------------- Calcular Abasto 
	
		
		--Order By IdFarmacia, DescripcionClave, Abasto 

----    Excepciones 
--        select T.* 
--        From #tmpRptAbastoClaves T 
--        Inner Join CFG_Claves_Excluir_NivelAbasto E On ( E.IdEstado = @IdEstado and T.IdClaveSSA = E.IdClaveSSA )
--        Where Abasto = 0 
        
        Update T Set Abasto = 2 
        From #tmpRptAbastoClaves T 
        Inner Join CFG_Claves_Excluir_NivelAbasto E On ( E.IdEstado = @IdEstado and T.IdClaveSSA = E.IdClaveSSA )

	
----    Calcular los abastos 		
		Set @Claves = (	Select Cast(Count(*) As Numeric(14,4)) From #tmpPerfil (Nolock)
						Where IdEstado = @IdEstado ) 
		Set @Abasto = ( Select Cast(Count(*) As Numeric(14,4)) From #tmpRptAbastoClaves (Nolock)
						Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia And Abasto in ( 1, 2 ) ) 

		-- Set @PorcAbasto = ( Select ((@Abasto/@Claves) * 100) ) 
		Set @PorcAbasto = (case when @Claves = 0 then 0 else ((@Abasto/@Claves) * 100) end ) 	
	

		Update #tmpRptAbastoClaves Set PorcAbasto = @PorcAbasto 
		Where IdEstado = @IdEstado And IdFarmacia = @IdFarmacia
	

--- SALIDA FINAL	
	If @Consulta = 0 
		Begin 
			Select top 1 IdEstado, Estado, IdFarmacia, Farmacia, getdate() as FechaReporte, 
				Count(*) As TotalClaves, 
				( Select Count(Abasto) From #tmpRptAbastoClaves (Nolock) Where Abasto in ( 1, 2 ) ) As ConExistencia, 
				( Select Count(Abasto) From #tmpRptAbastoClaves (Nolock) Where Abasto = 0 ) As SinExistencia, 
				PorcAbasto 
			From #tmpRptAbastoClaves (Nolock) 
			Group By IdEstado, Estado, IdFarmacia, Farmacia, PorcAbasto 	
	End 
----	Else 
----		Begin 
----			Select top 1 IdEstado, Estado, IdFarmacia, Farmacia, getdate() as FechaReporte, 
----				Count(*) As TotalClaves, 
----				( Select Count(Abasto) From #tmpRptAbastoClaves (Nolock) Where Abasto in ( 1, 2 ) ) As ConExistencia, 
----				( Select Count(Abasto) From #tmpRptAbastoClaves (Nolock) Where Abasto = 0 ) As SinExistencia, 
----				PorcAbasto 
----			From #tmpRptAbastoClaves (Nolock) 
----			Group By IdEstado, Estado, IdFarmacia, Farmacia, PorcAbasto 	
----		End 		
	
End
Go--#__SQL
*/ 