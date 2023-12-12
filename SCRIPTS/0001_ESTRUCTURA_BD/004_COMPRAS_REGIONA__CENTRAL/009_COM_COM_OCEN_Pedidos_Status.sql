If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_OCEN_Pedidos_Status' and xType = 'U' ) 
   Drop Table COM_OCEN_Pedidos_Status 
Go--#SQL 

Create Table COM_OCEN_Pedidos_Status 
( 
	IdStatus varchar(2) Not Null, 
	TipoStatus varchar(6) Not Null Unique Clustered, 	
	Descripcion varchar(50) Not Null, 
	Keyx int identity, 
	Status varchar(1) default 'A', 
	Actualizado tinyint default 0 
) 
Go--#SQL 

Alter Table COM_OCEN_Pedidos_Status Add Constraint PK_COM_OCEN_Pedidos_Status Primary Key ( IdStatus ) 
Go--#SQL 


Set NoCount On 
Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '01', 'A', 'Generado por Farmacia' ) 
Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '02', 'AC', 'Asignado a Comprador' ) 
Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '03', 'PC', 'Procesado para Compra' ) 
Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '04', 'PFR', 'Pedido Farmacia Rechazado' ) 
Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '05', 'PG', 'Pre-Pedido Generado' ) 
Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '06', 'PCP', 'Pre-Pedido Confirmado por Proveedor' ) 
Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '07', 'PRP', 'Pre-Pedido Rechazado por Proveedor' ) 

Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '08', 'OCG', 'Orden de Compra Generada' ) 
Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '09', 'OCE', 'Orden de Compra Embarcada' ) 

Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '10', 'OCR', 'Orden de Compra Recibida 100%' ) 
Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '11', 'OCNR', 'Orden de Compra No Recibida 100%' ) 

--Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '12', 'OCR-1', 'Orden de Compra Recibida con Faltante' ) 
--Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '13', 'OCR-2', 'Orden de Compra Recibida Fuera de Tiempo' ) 
--Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '14', 'OCR-3', 'Orden de Compra Recibida con Precio Diferente contra Factura' ) 
--
--Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '15', 'OCR-4', 'Orden de Compra Recibida con Faltante/Tiempo' ) 
--Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '16', 'OCR-5', 'Orden de Compra Recibida con Faltante/Precios' ) 
--Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '17', 'OCR-6', 'Orden de Compra Recibida con Faltante/Tiempo/Precios' ) 
--
--Insert Into COM_OCEN_Pedidos_Status ( IdStatus, TipoStatus, Descripcion ) Values ( '18', 'OCR-6', 'Orden de Compra Recibida con Tiempo/Precios' ) 
Go--#SQL 
