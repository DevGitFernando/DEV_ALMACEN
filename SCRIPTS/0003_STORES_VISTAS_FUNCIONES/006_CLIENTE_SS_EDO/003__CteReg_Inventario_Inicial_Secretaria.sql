
If Exists ( Select Name From Sysobjects Where Name = 'CteReg_Inventario_Inicial_Secretaria' and xType = 'U' )
      Drop Table CteReg_Inventario_Inicial_Secretaria
Go--#SQL

Create Table CteReg_Inventario_Inicial_Secretaria
(
     IdEmpresa varchar(3) Not Null,
     IdEstado varchar(2) Not Null,
     IdFarmacia varchar(4) Not Null,
     IdSubFarmacia varchar(2) Not Null,
     FolioMovtoInv varchar(30) Not Null,
     ClaveSSA varchar(50) Not Null,
     DescripcionClave varchar(7500) Not Null,
     IdProducto varchar(8) Not Null,
     CodigoEAN varchar(30) Not Null,
     ClaveLote varchar(30) Not Null,
     FechaCaducidad varchar(7) Null,
     EsConsignacion bit Not Null,
     Cantidad numeric(14, 4) Not Null,
     Costo numeric(14, 4) Not Null,
     Importe numeric(14, 4) Not Null,
     Existencia int Not Null
)
Go--#SQL


