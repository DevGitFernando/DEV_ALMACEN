If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CatMotivos_Dev_Transferencia' and xType = 'U' ) 
Begin 
	Create Table CatMotivos_Dev_Transferencia 
	(
		IdMotivo varchar(2) Not Null, 
		Descripcion varchar(100) Not Null, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table CatMotivos_Dev_Transferencia Add Constraint PK_CatMotivos_Dev_Transferencia Primary Key ( IdMotivo )  
End 
Go--#SQL   

If Not Exists ( Select * From CatMotivos_Dev_Transferencia Where IdMotivo = '00' )  Insert Into CatMotivos_Dev_Transferencia (  IdMotivo, Descripcion, Status, Actualizado )  Values ( '00', 'Sin especificar', 'A', 0 )    Else Update CatMotivos_Dev_Transferencia Set Descripcion = 'Sin especificar', Status = 'A', Actualizado = 0 Where IdMotivo = '00'
If Not Exists ( Select * From CatMotivos_Dev_Transferencia Where IdMotivo = '01' )  Insert Into CatMotivos_Dev_Transferencia (  IdMotivo, Descripcion, Status, Actualizado )  Values ( '01', 'La marca de algun producto no es la que maneja la unidad', 'A', 0 )    Else Update CatMotivos_Dev_Transferencia Set Descripcion = 'La marca de algun producto no es la que maneja la unidad', Status = 'A', Actualizado = 0 Where IdMotivo = '01'
If Not Exists ( Select * From CatMotivos_Dev_Transferencia Where IdMotivo = '02' )  Insert Into CatMotivos_Dev_Transferencia (  IdMotivo, Descripcion, Status, Actualizado )  Values ( '02', 'Claves no solicitadas', 'A', 0 )    Else Update CatMotivos_Dev_Transferencia Set Descripcion = 'Claves no solicitadas', Status = 'A', Actualizado = 0 Where IdMotivo = '02'
If Not Exists ( Select * From CatMotivos_Dev_Transferencia Where IdMotivo = '03' )  Insert Into CatMotivos_Dev_Transferencia (  IdMotivo, Descripcion, Status, Actualizado )  Values ( '03', 'Sobreabasto de producto en la unidad', 'A', 0 )    Else Update CatMotivos_Dev_Transferencia Set Descripcion = 'Sobreabasto de producto en la unidad', Status = 'A', Actualizado = 0 Where IdMotivo = '03'
If Not Exists ( Select * From CatMotivos_Dev_Transferencia Where IdMotivo = '04' )  Insert Into CatMotivos_Dev_Transferencia (  IdMotivo, Descripcion, Status, Actualizado )  Values ( '04', 'Se recibio pedido duplicado', 'A', 0 )    Else Update CatMotivos_Dev_Transferencia Set Descripcion = 'Se recibio pedido duplicado', Status = 'A', Actualizado = 0 Where IdMotivo = '04'
If Not Exists ( Select * From CatMotivos_Dev_Transferencia Where IdMotivo = '05' )  Insert Into CatMotivos_Dev_Transferencia (  IdMotivo, Descripcion, Status, Actualizado )  Values ( '05', 'Productos con proxima caducidad', 'A', 0 )    Else Update CatMotivos_Dev_Transferencia Set Descripcion = 'Productos con proxima caducidad', Status = 'A', Actualizado = 0 Where IdMotivo = '05'
If Not Exists ( Select * From CatMotivos_Dev_Transferencia Where IdMotivo = '06' )  Insert Into CatMotivos_Dev_Transferencia (  IdMotivo, Descripcion, Status, Actualizado )  Values ( '06', 'Lotes recibidos fisicamente no corresponden con el listado recibido', 'A', 0 )    Else Update CatMotivos_Dev_Transferencia Set Descripcion = 'Lotes recibidos fisicamente no corresponden con el listado recibido', Status = 'A', Actualizado = 0 Where IdMotivo = '06'
Go--#SQL   

