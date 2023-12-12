---------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select * From Sysobjects (nolock) Where Name = 'Pedidos__Ubicaciones_Excluidas_Surtimiento' and xType = 'U' ) 
Begin 
	Create Table Pedidos__Ubicaciones_Excluidas_Surtimiento 
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdPasillo int Not Null Default -1, 
		IdEstante int Not Null Default -1, 
		IdEntrepaño int Not Null Default -1,   
		Excluida bit Not Null Default 'false' 
	) 

	Alter Table Pedidos__Ubicaciones_Excluidas_Surtimiento Add Constraint PK_Pedidos__Ubicaciones_Excluidas_Surtimiento 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 

	Alter Table Pedidos__Ubicaciones_Excluidas_Surtimiento 
		Add Constraint FK_Pedidos__Ubicaciones_Excluidas_Surtimiento___CatPasillos_Estantes_Entrepaños  
		Foreign key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
		References CatPasillos_Estantes_Entrepaños ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
End 
Go--#SQL    


---------------------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select * From Sysobjects (nolock) Where Name = 'Pedidos__Ubicaciones_Excluidas_Surtimiento__Historico' and xType = 'U' ) 
Begin 
	Create Table Pedidos__Ubicaciones_Excluidas_Surtimiento__Historico 
	( 
		IdEmpresa varchar(3) Not Null, 
		IdEstado varchar(2) Not Null, 
		IdFarmacia varchar(4) Not Null, 
		IdPasillo int Not Null Default -1, 
		IdEstante int Not Null Default -1, 
		IdEntrepaño int Not Null Default -1,   
		Excluida bit Not Null Default 'false', 
		FechaRegistro datetime Not Null Default getdate(), 
		IdPersonal varchar(4) Not Null, 
		Keyx int identity(1,1) 
	) 

	Alter Table Pedidos__Ubicaciones_Excluidas_Surtimiento__Historico Add Constraint PK_Pedidos__Ubicaciones_Excluidas_Surtimiento__Historico 
		Primary Key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño, FechaRegistro ) 

	Alter Table Pedidos__Ubicaciones_Excluidas_Surtimiento__Historico 
		Add Constraint FK_Pedidos__Ubicaciones_Excluidas_Surtimiento___Pedidos__Ubicaciones_Excluidas_Surtimiento   
		Foreign key ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
		References Pedidos__Ubicaciones_Excluidas_Surtimiento ( IdEmpresa, IdEstado, IdFarmacia, IdPasillo, IdEstante, IdEntrepaño ) 
End 
Go--#SQL    

