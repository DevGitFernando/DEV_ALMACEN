If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Historial_EdoJuris_Surtimiento_Recetas' and xType = 'U' ) 
Begin 
	Create Table Historial_EdoJuris_Surtimiento_Recetas 
	( 
		IdEstado varchar(2) Not Null, 
		IdJurisdiccion varchar(3) Not Null, 
		Jurisdiccion varchar(50) Not Null Default '', 	
		IdFarmacia varchar(4) Not Null, 
		Farmacia varchar(100) Not Null Default '1', 
		FechaRegistro datetime Not Null Default getdate(), 
		Recetas int Not Null Default 0, 
		RecetasCompletas numeric(14, 4) Not Null Default 0, 
		Vales int Not Null Default 0, 
		PorcentajeVales numeric(14, 4) Not Null Default 0, 
		NoSurtido int Not Null Default 0, 
		PorcentajeNoSurtido numeric(14, 4) Not Null Default 0, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table Historial_EdoJuris_Surtimiento_Recetas Add Constraint PK_Historial_EdoJuris_Surtimiento_Recetas Primary Key ( IdEstado, IdJurisdiccion, IdFarmacia, FechaRegistro ) 

	Alter Table Historial_EdoJuris_Surtimiento_Recetas Add Constraint FK_Historial_EdoJuris_Surtimiento_Recetas_CatJurisdicciones
		Foreign Key ( IdEstado, IdJurisdiccion ) References CatJurisdicciones( IdEstado, IdJurisdiccion ) 

	Alter Table Historial_EdoJuris_Surtimiento_Recetas Add Constraint FK_Historial_EdoJuris_Surtimiento_Recetas_CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 

End 
Go--#SQL 
