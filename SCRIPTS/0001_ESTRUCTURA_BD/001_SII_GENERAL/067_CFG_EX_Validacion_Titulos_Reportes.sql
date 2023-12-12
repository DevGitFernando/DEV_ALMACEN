-------------------------------------------------------------------------------------------------------------- 
If Not Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_EX_Validacion_Titulos_Reportes' and xType = 'U' ) 
Begin 
	Create Table CFG_EX_Validacion_Titulos_Reportes  
	(
		IdEstado varchar(2) Not Null, 
		IdTitulo varchar(2) Not Null, 
		TituloEncabezadoReporte varchar(200) Not Null Default '', 
		Status varchar(1) Not Null Default 'A', 
		Actualizado tinyint Not Null Default 0 
	) 

	Alter Table CFG_EX_Validacion_Titulos_Reportes 
		Add Constraint PK_CFG_EX_Validacion_Titulos_Reportes Primary Key ( IdEstado, TituloEncabezadoReporte  ) 

End 
Go--#SQL 

If Not Exists ( Select * From CFG_EX_Validacion_Titulos_Reportes Where IdEstado = '11' and TituloEncabezadoReporte = 'FASSA' )  Insert Into CFG_EX_Validacion_Titulos_Reportes (  IdEstado, IdTitulo, TituloEncabezadoReporte, Status, Actualizado )  Values ( '11', '02', 'FASSA', 'A', 0 )    Else Update CFG_EX_Validacion_Titulos_Reportes Set IdTitulo = '02', Status = 'A', Actualizado = 0 Where IdEstado = '11' and TituloEncabezadoReporte = 'FASSA'
If Not Exists ( Select * From CFG_EX_Validacion_Titulos_Reportes Where IdEstado = '11' and TituloEncabezadoReporte = 'SEGURO POPULAR' )  Insert Into CFG_EX_Validacion_Titulos_Reportes (  IdEstado, IdTitulo, TituloEncabezadoReporte, Status, Actualizado )  Values ( '11', '01', 'SEGURO POPULAR', 'A', 0 )    Else Update CFG_EX_Validacion_Titulos_Reportes Set IdTitulo = '01', Status = 'A', Actualizado = 0 Where IdEstado = '11' and TituloEncabezadoReporte = 'SEGURO POPULAR'
Go--#SQL 


