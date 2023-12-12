----If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Historial_EdoJuris_NoCauses_Surtimiento_Recetas' and xType = 'U' ) 
----	Drop Table Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
----Go--#--SQL  

If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Historial_EdoJuris_NoCauses_Surtimiento_Recetas' and xType = 'U' ) 
Begin 
	Create Table Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
	(
		IdEstado varchar(2) Not Null,
		IdJurisdiccion varchar(3) Not Null, 
		Jurisdiccion varchar(50) Not Null Default '', 	
		IdFarmacia varchar(4) Not Null, 
		Farmacia varchar(100) Not Null Default '1', 
		FolioVenta varchar(8) Not Null Default '', 
		IdTipoInsumo varchar(10) Not Null Default '', 
		DescTipoDeInsumo varchar(100) Not Null Default '', 
		
		FechaRegistro datetime Not Null Default getdate(),
		CantidadSurtida numeric(14, 4) Not Null Default 0, 
		CantidadNoSurtida numeric(14, 4) Not Null Default 0,
		TotalPiezas numeric(14, 4) Not Null Default 0,	
		PorcentajeSurtido numeric(14, 4) Not Null Default 0, 
		PorcentajeNoSurtido numeric(14, 4) Not Null Default 0, 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table Historial_EdoJuris_NoCauses_Surtimiento_Recetas Add Constraint PK_Historial_EdoJuris_NoCauses_Surtimiento_Recetas 
		Primary Key ( IdEstado, IdJurisdiccion, IdFarmacia, FechaRegistro, FolioVenta, IdTipoInsumo ) 

	Alter Table Historial_EdoJuris_NoCauses_Surtimiento_Recetas Add Constraint FK_Historial_EdoJuris_NoCauses_Surtimiento_Recetas_CatJurisdicciones
		Foreign Key ( IdEstado, IdJurisdiccion ) References CatJurisdicciones( IdEstado, IdJurisdiccion ) 

	Alter Table Historial_EdoJuris_NoCauses_Surtimiento_Recetas Add Constraint FK_Historial_EdoJuris_NoCauses_Surtimiento_Recetas_CatFarmacias 
		Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia )

End 
Go--#SQL 

