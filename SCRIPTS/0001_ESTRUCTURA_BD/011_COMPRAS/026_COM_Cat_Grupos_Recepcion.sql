
---------------------------------------------------------------------------------------------------------------------------------------------
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_Adt_CheckList_Recepcion' and xType = 'U' )
----		Drop Table  COM_Adt_CheckList_Recepcion
----Go--#xxxSQL

---------------------------------------------------------------------------------------------------------------------------------------------
----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_CheckList_Recepcion' and xType = 'U' )
----	Drop Table COM_CheckList_Recepcion
----Go--#xxxSQL


----------------------------------------------------------------------------------------------------------------------------------------

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_Cat_Grupos_Recepcion' and xType = 'U' )
Begin 
	Create Table COM_Cat_Grupos_Recepcion 
	(
		IdGrupo varchar(3) Not Null,	
		DescripcionGrupo varchar(200) Not Null,

		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table COM_Cat_Grupos_Recepcion Add Constraint PK_COM_Cat_Grupos_Recepcion Primary Key ( IdGrupo ) 

End 
Go--#SQL 

	If Not Exists ( Select * From COM_Cat_Grupos_Recepcion Where IdGrupo = '001' )  Insert Into COM_Cat_Grupos_Recepcion (  IdGrupo, DescripcionGrupo, Status, Actualizado )  Values ( '001', 'DOCUMENTOS', 'A', 0 )    Else Update COM_Cat_Grupos_Recepcion Set DescripcionGrupo = 'DOCUMENTOS', Status = 'A', Actualizado = 0 Where IdGrupo = '001' 
	If Not Exists ( Select * From COM_Cat_Grupos_Recepcion Where IdGrupo = '002' )  Insert Into COM_Cat_Grupos_Recepcion (  IdGrupo, DescripcionGrupo, Status, Actualizado )  Values ( '002', 'CONDICIONES DE TRANSPORTE', 'A', 0 )    Else Update COM_Cat_Grupos_Recepcion Set DescripcionGrupo = 'CONDICIONES DE TRANSPORTE', Status = 'A', Actualizado = 0 Where IdGrupo = '002'
	If Not Exists ( Select * From COM_Cat_Grupos_Recepcion Where IdGrupo = '003' )  Insert Into COM_Cat_Grupos_Recepcion (  IdGrupo, DescripcionGrupo, Status, Actualizado )  Values ( '003', 'CONDICIONES DEL PRODUCTO', 'A', 0 )    Else Update COM_Cat_Grupos_Recepcion Set DescripcionGrupo = 'CONDICIONES DEL PRODUCTO', Status = 'A', Actualizado = 0 Where IdGrupo = '003'
Go--#SQL	


---------------------------------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_CheckList_Recepcion' and xType = 'U' )
Begin  
	Create Table COM_CheckList_Recepcion 
	(
		IdGrupo varchar(3) Not Null,
		IdMotivo varchar(3) Not Null,
		DescripcionMotivo varchar(200) Not Null,
		Respuesta_SI bit Not Null Default 0,
		Respuesta_SI_RequiereFirma bit Not Null Default 0,

		Respuesta_NO bit Not Null Default 0,
		Respuesta_NO_RequiereFirma bit Not Null Default 0,

		Respuesta_Rechazo bit Not Null Default 0,
		Respuesta_Rechazo_RequiereFirma bit Not Null Default 0, 

		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table COM_CheckList_Recepcion Add Constraint PK_COM_CheckList_Recepcion Primary Key ( IdGrupo, IdMotivo ) 

	Alter Table COM_CheckList_Recepcion Add Constraint FK_COM_CheckList_Recepcion_COM_Cat_Grupos_Recepcion
	Foreign Key ( IdGrupo ) References COM_Cat_Grupos_Recepcion ( IdGrupo ) 

End 
Go--#SQL 

	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '001' and IdMotivo = '001' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '001', '001', 'O.C. IMPRESA Y COLOCADA', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'O.C. IMPRESA Y COLOCADA', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '001' and IdMotivo = '001'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '001' and IdMotivo = '002' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '001', '002', 'FACTURAS Y COPIAS', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'FACTURAS Y COPIAS', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '001' and IdMotivo = '002'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '001' and IdMotivo = '003' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '001', '003', 'Lotes detallados y caducidad en factura.', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Lotes detallados y caducidad en factura.', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '001' and IdMotivo = '003'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '001' and IdMotivo = '004' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '001', '004', 'Carta canje ( en caso necesario)', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Carta canje ( en caso necesario)', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '001' and IdMotivo = '004'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '001' and IdMotivo = '005' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '001', '005', 'Certificado de Calidad.', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Certificado de Calidad.', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '001' and IdMotivo = '005'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '001' and IdMotivo = '006' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '001', '006', 'Temperatura adecuada (medicamento refrigerado)', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Temperatura adecuada (medicamento refrigerado)', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '001' and IdMotivo = '006'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '002' and IdMotivo = '001' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '002', '001', 'Unidad Limpia.', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Unidad Limpia.', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '002' and IdMotivo = '001'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '002' and IdMotivo = '002' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '002', '002', 'Olores extraños.', 0, 0, 1, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Olores extraños.', Respuesta_SI = 0, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 1, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '002' and IdMotivo = '002'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '002' and IdMotivo = '003' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '002', '003', 'Unidad Cerrada.', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Unidad Cerrada.', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '002' and IdMotivo = '003'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '002' and IdMotivo = '004' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '002', '004', 'Con goteras o evidencia de humedad.', 0, 0, 1, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Con goteras o evidencia de humedad.', Respuesta_SI = 0, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 1, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '002' and IdMotivo = '004'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '002' and IdMotivo = '005' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '002', '005', 'Con manchas de aceite o combustible.', 0, 1, 1, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Con manchas de aceite o combustible.', Respuesta_SI = 0, Respuesta_SI_RequiereFirma = 1, Respuesta_NO = 1, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '002' and IdMotivo = '005'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '003' and IdMotivo = '001' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '003', '001', 'Producto estibado en tarima.', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Producto estibado en tarima.', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '003' and IdMotivo = '001'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '003' and IdMotivo = '002' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '003', '002', 'Producto húmedo', 0, 0, 1, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Producto húmedo', Respuesta_SI = 0, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 1, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '003' and IdMotivo = '002'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '003' and IdMotivo = '003' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '003', '003', 'Producto dañado.', 0, 1, 1, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Producto dañado.', Respuesta_SI = 0, Respuesta_SI_RequiereFirma = 1, Respuesta_NO = 1, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '003' and IdMotivo = '003'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '003' and IdMotivo = '004' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '003', '004', 'Es fácil ubicar los productos.', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Es fácil ubicar los productos.', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '003' and IdMotivo = '004'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '003' and IdMotivo = '005' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '003', '005', 'Con manchas de aceite o combustible.', 0, 1, 1, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Con manchas de aceite o combustible.', Respuesta_SI = 0, Respuesta_SI_RequiereFirma = 1, Respuesta_NO = 1, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '003' and IdMotivo = '005'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '003' and IdMotivo = '006' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '003', '006', 'Temperatura adecuada (medicamento refrigerado)', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Temperatura adecuada (medicamento refrigerado)', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '003' and IdMotivo = '006'
	If Not Exists ( Select * From COM_CheckList_Recepcion Where IdGrupo = '003' and IdMotivo = '007' )  Insert Into COM_CheckList_Recepcion (  IdGrupo, IdMotivo, DescripcionMotivo, Respuesta_SI, Respuesta_SI_RequiereFirma, Respuesta_NO, Respuesta_NO_RequiereFirma, Respuesta_Rechazo, Respuesta_Rechazo_RequiereFirma, Status, Actualizado )  Values ( '003', '007', 'Transporta sólo Insumos para la Salud.', 1, 0, 0, 0, 0, 0, 'A', 0 )    Else Update COM_CheckList_Recepcion Set DescripcionMotivo = 'Transporta sólo Insumos para la Salud.', Respuesta_SI = 1, Respuesta_SI_RequiereFirma = 0, Respuesta_NO = 0, Respuesta_NO_RequiereFirma = 0, Respuesta_Rechazo = 0, Respuesta_Rechazo_RequiereFirma = 0, Status = 'A', Actualizado = 0 Where IdGrupo = '003' and IdMotivo = '007'

Go--#SQL	


---------------------------------------------------------------------------------------------------------------------------------------
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'COM_Adt_CheckList_Recepcion' and xType = 'U' )
Begin 	 
	Create Table COM_Adt_CheckList_Recepcion
	(
		IdEmpresa varchar(3) Not Null,
		IdEstado varchar(2) Not Null,
		IdFarmacia varchar(4) Not Null,
		FolioOrdenCompra varchar(30) Not Null,
		IdGrupo varchar(3) Not Null,
		IdMotivo varchar(3) Not Null,
		Respuesta_SI bit Not Null Default 0,
		Respuesta_NO bit Not Null Default 0,
		Respuesta_Rechazo bit Not Null Default 0, 
		Comentario varchar(200) Not Null Default '',
		EsFirmado bit Not Null Default 0,
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	)

	Alter Table COM_Adt_CheckList_Recepcion Add Constraint PK_COM_Adt_CheckList_Recepcion
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra, IdGrupo, IdMotivo )	

	Alter Table COM_Adt_CheckList_Recepcion Add Constraint FK_COM_Adt_CheckList_Recepcion_COM_CheckList_Recepcion
		Foreign Key ( IdGrupo, IdMotivo ) 
		References COM_CheckList_Recepcion ( IdGrupo, IdMotivo ) 

	Alter Table COM_Adt_CheckList_Recepcion Add Constraint FK_COM_Adt_CheckList_Recepcion_OrdenesDeComprasEnc
		Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra ) 
		References OrdenesDeComprasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioOrdenCompra ) 

End 
Go--#SQL 
	

	
