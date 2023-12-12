---------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'VentasDigitalizacion' and xType = 'U' ) 
	Drop Table VentasDigitalizacion 
Go--#SQL  

Create Table VentasDigitalizacion  
( 
	IdEmpresa varchar(3) Not Null, 
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	FolioVenta varchar(30) Not Null, 

	IdImagen smallint Not Null, 
	TipoDeImagen smallint Not Null Default 0,   -- 1 ==> Ticket, 2 ==> Receta 

	FechaDigitalizacion datetime Not Null Default getdate(),   	

	ImagenComprimida varchar(max) Not Null Default '', 
	ImagenOriginal varchar(max) Not Null Default '', 
	Ancho int Not Null Default 0, 
	Alto int Not Null Default 0, 
	
	Status varchar(1) Not Null Default '', 	
	Actualizado tinyint Not Null Default 0, 
	FechaControl datetime Not Null Default getdate(),   
) 
Go--#SQL  

Alter Table VentasDigitalizacion Add Constraint PK_VentasDigitalizacion 
	Primary Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta, IdImagen ) 
Go--#SQL  

--Alter Table VentasDigitalizacion Add Constraint FK_VentasDigitalizacion_VentasEnc 
--	Foreign Key ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
--	References VentasEnc ( IdEmpresa, IdEstado, IdFarmacia, FolioVenta ) 
--Go--#xSQL  

