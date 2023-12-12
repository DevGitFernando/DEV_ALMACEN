Set NoCount On 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
	Drop Table MovtosInv_Det_CodigosEAN_Lotes 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
	Drop Table MovtosInv_Det_CodigosEAN_Lotes 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN' and xType = 'U' ) 
	Drop Table MovtosInv_Det_CodigosEAN 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det' and xType = 'U' ) 
	Drop Table MovtosInv_Det 
Go--#SQL  

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Enc' and xType = 'U' ) 
	Drop Table MovtosInv_Enc 
Go--#SQL 

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Movtos_Inv_Tipos_Farmacia' and xType = 'U' ) 
	Drop Table Movtos_Inv_Tipos_Farmacia 
Go--#SQL  


--------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Movtos_Inv_Tipos' and xType = 'U' ) 
	Drop Table Movtos_Inv_Tipos 
Go--#SQL  

Create Table Movtos_Inv_Tipos 
(
--	IdTipoMovimiento varchar(2) Not Null,  
	IdTipoMovto_Inv varchar(6) Not Null, 
	Descripcion varchar(50) Not Null, 
	Efecto_Movto varchar(1) Not Null Default '', 
	IdTipoMovto_Inv_ContraMovto varchar(6) Not Null Default '',	
	Efecto_ContraMovto varchar(1) Not Null Default '', 
	EsMovtoGral Bit Not Null Default 'false', 
	PermiteCaducados Bit Not Null Default 'false', 	
	Keyx int identity(1,1), 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table Movtos_Inv_Tipos Add Constraint PK_Movtos_Inv_Tipos Primary Key ( IdTipoMovto_Inv ) 
Go--#SQL  

/* 
If Not Exists ( Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = 'II' )  Insert Into Movtos_Inv_Tipos Values ( 'II', 'Inventario Inicial', 'E', 'IC', 'S', 'A', 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Inventario Inicial', Efecto_Movto = 'E', IdTipoMovto_Inv_ContraMovto = 'IC', Efecto_ContraMovto = 'S', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'II' 
If Not Exists ( Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = 'IC' )  Insert Into Movtos_Inv_Tipos Values ( 'IC', 'Cancelacion de Inventario Inicial', 'S', '', '', 'A', 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Cancelacion de Inventario Inicial', Efecto_Movto = 'S', IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'IC' 
If Not Exists ( Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = 'SV' )  Insert Into Movtos_Inv_Tipos Values ( 'SV', 'Salida por dispensación', 'S', 'ED', 'E', 'A', 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Salida por dispensación', Efecto_Movto = 'S', IdTipoMovto_Inv_ContraMovto = 'ED', Efecto_ContraMovto = 'E', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'SV' 
If Not Exists ( Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = 'ED' )  Insert Into Movtos_Inv_Tipos Values ( 'ED', 'Entrada por devolución de dispensación', 'E', '', '', 'A', 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Entrada por devolución de dispensación', Efecto_Movto = 'E', IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'ED' 
If Not Exists ( Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = 'EC' )  Insert Into Movtos_Inv_Tipos Values ( 'EC', 'Entrada por compra', 'E', 'CC', 'S', 'A', 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Entrada por compra', Efecto_Movto = 'E', IdTipoMovto_Inv_ContraMovto = 'CC', Efecto_ContraMovto = 'S', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'EC' 
If Not Exists ( Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = 'CC' )  Insert Into Movtos_Inv_Tipos Values ( 'CC', 'Cancelación de compra', 'S', '', '', 'A', 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Cancelación de compra', Efecto_Movto = 'S', IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'CC' 

If Not Exists ( Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = 'TS' )  Insert Into Movtos_Inv_Tipos Values ( 'TS', 'Salida por transferencia', 'S', '', '', 'A', 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Salida por transferencia', Efecto_Movto = 'S', IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'TS' 
If Not Exists ( Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = 'TSC' )  Insert Into Movtos_Inv_Tipos Values ( 'TSC', 'Cancelacion de salida por transferencia', 'E', '', '', 'A', 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Cancelacion de salida por transferencia', Efecto_Movto = 'E', IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'TSC' 
If Not Exists ( Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = 'TE' )  Insert Into Movtos_Inv_Tipos Values ( 'TE', 'Entrada por transferencia', 'E', '', '', 'A', 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Entrada por transferencia', Efecto_Movto = 'E', IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'TE' 
If Not Exists ( Select * From Movtos_Inv_Tipos Where IdTipoMovto_Inv = 'TEC' )  Insert Into Movtos_Inv_Tipos Values ( 'TEC', 'Cancelacion de entrada por transferencia', 'S', '', '', 'A', 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Cancelacion de entrada por transferencia', Efecto_Movto = 'S', IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'TEC' 
*/

/* 
---- 2K101219-1427 
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'CC' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'CC', 'Cancelación de compra', 'S', 0, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Cancelación de compra', Efecto_Movto = 'S', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'CC'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'EAI' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'EAI', 'Entrada por Ajuste de Inventario', 'E', 0, 'SAI', 'S', 'A', 0, 1 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Entrada por Ajuste de Inventario', Efecto_Movto = 'E', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = 'SAI', Efecto_ContraMovto = 'S', Status = 'A', Actualizado = 0, PermiteCaducados = 1 Where IdTipoMovto_Inv = 'EAI'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'EC' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'EC', 'Entrada por compra', 'E', 0, 'CC', 'S', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Entrada por compra', Efecto_Movto = 'E', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = 'CC', Efecto_ContraMovto = 'S', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'EC'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'ED' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'ED', 'Entrada por devolución de dispensación', 'E', 0, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Entrada por devolución de dispensación', Efecto_Movto = 'E', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'ED'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'EE' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'EE', 'Entrada por Error', 'E', 1, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Entrada por Error', Efecto_Movto = 'E', EsMovtoGral = 1, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'EE'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'EPC' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'EPC', 'Entrada por Consignación', 'E', 1, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Entrada por Consignación', Efecto_Movto = 'E', EsMovtoGral = 1, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'EPC'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'IC' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'IC', 'Cancelacion de Inventario Inicial', 'S', 0, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Cancelacion de Inventario Inicial', Efecto_Movto = 'S', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'IC'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'II' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'II', 'Inventario Inicial', 'E', 0, 'IC', 'S', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Inventario Inicial', Efecto_Movto = 'E', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = 'IC', Efecto_ContraMovto = 'S', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'II'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'SAI' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'SAI', 'Salida por Ajuste de Inventario', 'S', 0, 'EAI', 'E', 'A', 0, 1 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Salida por Ajuste de Inventario', Efecto_Movto = 'S', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = 'EAI', Efecto_ContraMovto = 'E', Status = 'A', Actualizado = 0, PermiteCaducados = 1 Where IdTipoMovto_Inv = 'SAI'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'SC' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'SC', 'Salida por Caducado', 'S', 1, '', '', 'A', 0, 1 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Salida por Caducado', Efecto_Movto = 'S', EsMovtoGral = 1, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 1 Where IdTipoMovto_Inv = 'SC'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'SCI' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'SCI', 'Salida por Cierre de Inventario', 'S', 1, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Salida por Cierre de Inventario', Efecto_Movto = 'S', EsMovtoGral = 1, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'SCI'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'SE' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'SE', 'Salida por Error', 'S', 1, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Salida por Error', Efecto_Movto = 'S', EsMovtoGral = 1, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'SE'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'SM' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'SM', 'Salida por Merma', 'S', 1, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Salida por Merma', Efecto_Movto = 'S', EsMovtoGral = 1, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'SM'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'SV' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'SV', 'Salida por dispensación', 'S', 0, 'ED', 'E', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Salida por dispensación', Efecto_Movto = 'S', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = 'ED', Efecto_ContraMovto = 'E', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'SV'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'TE' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'TE', 'Entrada por transferencia', 'E', 0, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Entrada por transferencia', Efecto_Movto = 'E', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'TE'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'TEC' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'TEC', 'Cancelacion de entrada por transferencia', 'S', 0, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Cancelacion de entrada por transferencia', Efecto_Movto = 'S', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'TEC'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'TS' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'TS', 'Salida por transferencia', 'S', 0, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Salida por transferencia', Efecto_Movto = 'S', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'TS'
If Not Exists ( Select * From Movtos_Inv_Tipos (NoLock)  Where IdTipoMovto_Inv = 'TSC' )  Insert Into Movtos_Inv_Tipos ( IdTipoMovto_Inv, Descripcion, Efecto_Movto, EsMovtoGral, IdTipoMovto_Inv_ContraMovto, Efecto_ContraMovto, Status, Actualizado, PermiteCaducados )  Values ( 'TSC', 'Cancelacion de salida por transferencia', 'E', 0, '', '', 'A', 0, 0 )    Else Update Movtos_Inv_Tipos Set Descripcion = 'Cancelacion de salida por transferencia', Efecto_Movto = 'E', EsMovtoGral = 0, IdTipoMovto_Inv_ContraMovto = '', Efecto_ContraMovto = '', Status = 'A', Actualizado = 0, PermiteCaducados = 0 Where IdTipoMovto_Inv = 'TSC'  
*/ 
------------------


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Movtos_Inv_Tipos_Farmacia' and xType = 'U' ) 
	Drop Table Movtos_Inv_Tipos_Farmacia 
Go--#SQL  

Create Table Movtos_Inv_Tipos_Farmacia 
( 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	IdTipoMovto_Inv varchar(6) Not Null, 
	Consecutivo int Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table Movtos_Inv_Tipos_Farmacia 
	Add Constraint PK_Movtos_Inv_Tipos_Farmacia Primary Key ( IdEstado, IdFarmacia, IdTipoMovto_Inv ) 
Go--#SQL  

Alter Table Movtos_Inv_Tipos_Farmacia Add Constraint FK_Movtos_Inv_Tipos_Farmacia_Movtos_Inv_Tipos 
	Foreign Key ( IdTipoMovto_Inv ) References Movtos_Inv_Tipos ( IdTipoMovto_Inv ) 
Go--#SQL  

Alter Table Movtos_Inv_Tipos_Farmacia Add Constraint FK_Movtos_Inv_Tipos_Farmacia_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  

/*
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'CC' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0001', 'CC', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'CC'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'EC' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0001', 'EC', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'EC'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'ED' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0001', 'ED', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'ED'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'IC' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0001', 'IC', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'IC'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'II' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0001', 'II', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'II'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'SV' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0001', 'SV', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'SV'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'TE' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0001', 'TE', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'TE'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'TEC' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0001', 'TEC', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'TEC'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'TS' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0001', 'TS', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'TS'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'TSC' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0001', 'TSC', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0001' and IdTipoMovto_Inv = 'TSC' 

If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'CC' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0002', 'CC', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'CC'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'EC' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0002', 'EC', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'EC'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'ED' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0002', 'ED', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'ED'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'IC' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0002', 'IC', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'IC'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'II' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0002', 'II', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'II'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'SV' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0002', 'SV', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'SV'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'TE' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0002', 'TE', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'TE'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'TEC' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0002', 'TEC', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'TEC'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'TS' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0002', 'TS', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'TS'
If Not Exists ( Select * From Movtos_Inv_Tipos_Farmacia Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'TSC' )  Insert Into Movtos_Inv_Tipos_Farmacia Values ( '25', '0002', 'TSC', 0, 'A', 0 )    Else Update Movtos_Inv_Tipos_Farmacia Set Consecutivo = 0, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0002' and IdTipoMovto_Inv = 'TSC' 
*/


----------------------------------- Movimientos de Inventario 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Enc' and xType = 'U' ) 
	Drop Table MovtosInv_Enc 
Go--#SQL  

Create Table MovtosInv_Enc 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	FolioMovtoInv varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	IdTipoMovto_Inv varchar(6) Not Null, 
	TipoES varchar(1) Not Null, 
	FechaSistema datetime Default GetDate(),     --- Fecha de Sistema en que se realizo el movimiento  	
	-- FechaVenta datetime Default GetDate(),     --- Fecha contable del movimiento 
	FechaRegistro datetime Default GetDate(),  --- Fecha en que se registro movimiento
	Referencia varchar(30) Not Null Default '', 
	MovtoAplicado varchar(1) Not Null Default 'N',  
	IdPersonalRegistra varchar(6) Not Null Default '', 
	Observaciones varchar(500) Not Null Default '', 
	SubTotal Numeric(14,4) Not Null Default 0, 
	-- Descuento Numeric(14,4) Not Null Default 0, 
	Iva Numeric(14,4) Not Null Default 0, 
	Total Numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 

	FolioCierre int Not Null Default 0, 
	Cierre tinyint Not Null Default 0, 
	 
	IdPersonalHuella Varchar(8) DEFAULT ''  Not Null, 

	Keyx int identity(1,1), 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL  

Alter Table MovtosInv_Enc Add Constraint PK_MovtosInv_Enc Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv  )
Go--#SQL  

Alter Table MovtosInv_Enc Add Constraint FK_MovtosInv_Enc_CatEmpresas 
	Foreign Key ( IdEmpresa ) References CatEmpresas ( IdEmpresa ) 
Go--#SQL 

Alter Table MovtosInv_Enc Add Constraint FK_MovtosInv_Enc_Movtos_Inv_Tipos_Farmacia 
	Foreign Key ( IdEstado, IdFarmacia, IdTipoMovto_Inv ) References Movtos_Inv_Tipos_Farmacia ( IdEstado, IdFarmacia, IdTipoMovto_Inv ) 
Go--#SQL  

Alter Table MovtosInv_Enc Add Constraint FK_MovtosInv_Enc_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL 

--If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det' and xType = 'U' ) 
--	Drop Table MovtosInv_Det 
--Go 
--
--Create Table MovtosInv_Det 
--( 
--	IdEstado varchar(2) Not Null, 
--	IdFarmacia varchar(4) Not Null,	
--	FolioMovtoInv varchar(30) Not Null, 
--	IdProducto varchar(8) Not Null, 
--	-- CodigoEAN_Movto varchar(30) Not Null, 
--	-- Renglon int Not Null Default 0, 
--	Cantidad Numeric(14,4) Not Null Default 0, 
--	Costo Numeric(14,4) Not Null Default 0, 
--	Importe Numeric(14,4) Not Null Default 0, 
--	Existencia Int Not Null Default 0, 	
--	Status varchar(1) Not Null Default 'A', 
--	Keyx int identity(1,1),
--	Actualizado tinyint Not Null Default 0 
--)
--Go 
--
--Alter Table MovtosInv_Det Add Constraint PK_MovtosInv_Det Primary Key ( IdEstado, IdFarmacia, FolioMovtoInv, IdProducto ) 
--Go 
--
--Alter Table MovtosInv_Det Add Constraint FK_MovtosInv_Det_MovtosInv_Enc 
--	Foreign Key ( IdEstado, IdFarmacia, FolioMovtoInv ) References MovtosInv_Enc ( IdEstado, IdFarmacia, FolioMovtoInv ) 
--Go 
--
--Alter Table MovtosInv_Det Add Constraint FK_MovtosInv_Det_CatProductos 
--	Foreign Key ( IdProducto ) References CatProductos ( IdProducto ) 
--Go 
 
------------------------- 


If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN' and xType = 'U' ) 
	Drop Table MovtosInv_Det_CodigosEAN 
Go--#SQL  

Create Table MovtosInv_Det_CodigosEAN 
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	FolioMovtoInv varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	-- EsConsignacion tinyint Not Null Default 0,  	
	FechaSistema datetime Default GetDate(),     --- Fecha de Sistema en que se realizo el movimiento  		
	-- Renglon int Not Null Default 0, 
	UnidadDeSalida smallint Not Null Default 1, 	
	TasaIva Numeric(14,4) Not Null Default 0, 
	Cantidad Numeric(14,4) Not Null Default 0, 
	Costo Numeric(14,4) Not Null Default 0, 
	Importe Numeric(14,4) Not Null Default 0, 
	Existencia Int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Keyx int identity(1,1), 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table MovtosInv_Det_CodigosEAN Add Constraint PK_MovtosInv_Det_CodigosEAN 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN ) 
Go--#SQL  

--Alter Table MovtosInv_Det_CodigosEAN Add Constraint FK_MovtosInv_Det_CodigosEAN_MovtosInv_ 
--	Foreign Key ( IdEstado, IdFarmacia, FolioMovtoInv, IdProducto ) References MovtosInv ( IdEstado, IdFarmacia, FolioMovtoInv , IdProducto ) 
--Go
Alter Table MovtosInv_Det_CodigosEAN Add Constraint FK_MovtosInv_Det_CodigosEAN_MovtosInv_Enc 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) References MovtosInv_Enc ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv ) 
Go--#SQL 

Alter Table MovtosInv_Det_CodigosEAN Add Constraint FK_MovtosInv_Det_CodigosEAN_CatProductos_CodigosRelacionados 
	Foreign Key ( IdProducto, CodigoEAN ) References CatProductos_CodigosRelacionados ( IdProducto, CodigoEAN )  
Go--#SQL  

Alter Table MovtosInv_Det_CodigosEAN With NoCheck 
	Add Constraint CK_MovtosInv_Det_CodigosEAN_Cantidad Check Not For Replication (Cantidad > 0)
Go--#SQL 


-------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes' and xType = 'U' ) 
	Drop Table MovtosInv_Det_CodigosEAN_Lotes 
Go--#SQL  

Create Table MovtosInv_Det_CodigosEAN_Lotes 
(
 	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	IdSubFarmacia varchar(2) Not Null,		
	FolioMovtoInv varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	EsConsignacion bit Not Null Default 'false',  	
	-- Renglon int Not Null Default 0, 
	Cantidad Numeric(14,4) Not Null Default 0, 
	Costo Numeric(14,4) Not Null Default 0, 
	Importe Numeric(14,4) Not Null Default 0, 
	Existencia Int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Keyx int identity(1,1), 
	Actualizado tinyint Not Null Default 0 
)
Go--#SQL 

Alter Table MovtosInv_Det_CodigosEAN_Lotes Add Constraint PK_MovtosInv_Det_CodigosEAN_Lotes 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  

Alter Table MovtosInv_Det_CodigosEAN_Lotes Add Constraint FK_MovtosInv_Det_CodigosEAN_Lotes_MovtosInv_Det_CodigosEAN
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN ) 
	References MovtosInv_Det_CodigosEAN ( IdEmpresa, IdEstado, IdFarmacia, FolioMovtoInv, IdProducto, CodigoEAN ) 
Go--#SQL 

Alter Table MovtosInv_Det_CodigosEAN_Lotes Add Constraint FK_MovtosInv_Det_CodigosEAN_Lotes_FarmaciaProductos_CodigoEAN_Lotes 
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
	References FarmaciaProductos_CodigoEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL  

Alter Table MovtosInv_Det_CodigosEAN_Lotes With NoCheck 
	Add Constraint CK_MovtosInv_Det_CodigosEAN_Lotes_Cantidad Check Not For Replication (Cantidad > 0)
Go--#SQL 

------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones' and xType = 'U' ) 
   Drop Table MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
Go--#SQL 

Create Table MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
(
 	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null,	
	IdSubFarmacia varchar(2) Not Null,		
	FolioMovtoInv varchar(30) Not Null, 
	IdProducto varchar(8) Not Null, 
	CodigoEAN varchar(30) Not Null, 
	ClaveLote varchar(30) Not Null, 
	EsConsignacion bit Not Null Default 'false',
	
	IdPasillo int Not Null, 
	IdEstante int Not Null, 
	IdEntrepaño int Not Null,
  	
	-- Renglon int Not Null Default 0, 
	Cantidad Numeric(14,4) Not Null Default 0, 
	--Costo Numeric(14,4) Not Null Default 0, 
	--Importe Numeric(14,4) Not Null Default 0, 
	Existencia Int Not Null Default 0, 	
	Status varchar(1) Not Null Default 'A', 
	Actualizado tinyint Not Null Default 0, 
	Keyx int identity(1,1) 	 
) 
Go--#SQL 

Alter Table MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones Add Constraint PK_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL 

Alter Table MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones Add Constraint FK_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones__MovtosInv_Det_CodigosEAN_Lotes  
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote ) 
	References MovtosInv_Det_CodigosEAN_Lotes ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, FolioMovtoInv, IdProducto, CodigoEAN, ClaveLote ) 
Go--#SQL 

Alter Table MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones Add Constraint FK_MovtosInv_Det_CodigosEAN_Lotes_Ubicaciones_FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones
	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño) 
	References FarmaciaProductos_CodigoEAN_Lotes_Ubicaciones ( IdEmpresa, IdEstado, IdFarmacia, IdSubFarmacia, IdProducto, CodigoEAN, ClaveLote, IdPasillo, IdEstante, IdEntrepaño ) 
Go--#SQL 


