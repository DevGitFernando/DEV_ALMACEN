----------------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'MovtosInv_Motivos_Dev' and xType = 'U' )
Begin
	Create Table MovtosInv_Motivos_Dev 
	(
		IdTipoMovto_Inv varchar(6) Not Null,
		IdMotivo varchar(3) Not Null, 
		Descripcion varchar(200) Not Null, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table MovtosInv_Motivos_Dev Add Constraint PK_MovtosInv_Motivos_Dev Primary Key ( IdTipoMovto_Inv, IdMotivo )

	Alter Table MovtosInv_Motivos_Dev Add Constraint FK_MovtosInv_Motivos_Dev_Movtos_Inv_Tipos 
	Foreign Key ( IdTipoMovto_Inv ) References Movtos_Inv_Tipos ( IdTipoMovto_Inv )
End 
Go--#SQL    


If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'CC' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'CC', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'CC' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'CSDP' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'CSDP', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'CSDP' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'DEPC' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'DEPC', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'DEPC' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'DEPD' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'DEPD', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'DEPD' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'DOC' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'DOC', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'DOC' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'DPDC' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'DPDC', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'DPDC' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'ED' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'ED', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'ED' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'EDD' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'EDD', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'EDD' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'EDT' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'EDT', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'EDT' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'IC' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'IC', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'IC' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'SDP' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'SDP', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'SDP' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'SDT' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'SDT', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'SDT' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'TEC' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'TEC', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'TEC' and IdMotivo = '000'  
If Not Exists ( Select * From MovtosInv_Motivos_Dev Where IdTipoMovto_Inv = 'TSC' and IdMotivo = '000' )  Insert Into MovtosInv_Motivos_Dev (  IdTipoMovto_Inv, IdMotivo, Descripcion, Status, Actualizado )  Values ( 'TSC', '000', 'NO ESPECIFICADO', 'A', 0 )    Else Update MovtosInv_Motivos_Dev Set Descripcion = 'NO ESPECIFICADO', Status = 'A', Actualizado = 0 Where IdTipoMovto_Inv = 'TSC' and IdMotivo = '000'  
Go--#SQL   