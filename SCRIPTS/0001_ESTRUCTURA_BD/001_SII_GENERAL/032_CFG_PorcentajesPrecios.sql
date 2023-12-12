If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'CFG_PorcentajesPrecios' and xType = 'U' ) 
   Drop Table CFG_PorcentajesPrecios 
Go--#SQL  

Create Table CFG_PorcentajesPrecios
(
	IdEstado varchar(2) Not Null, 
	Porcentaje numeric(14,4) Not Null Default 0, 	
	FechaRegistro datetime Not Null Default getdate(), 
	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
) 
Go--#SQL  
  
Alter Table CFG_PorcentajesPrecios Add Constraint PK_CFG_PorcentajesPrecios Primary Key ( IdEstado ) 
Go--#SQL  


--- Insertar Valores Iniciaales 
Declare @sIdEstado varchar(2) 

	Set @sIdEstado = '10' 
	If Exists ( Select top 1 IdEstado From FarmaciaProductos (NoLock) Where IdEstado = @sIdEstado ) 
	Begin 
		If Not Exists ( Select * From CFG_PorcentajesPrecios (NoLock) Where IdEstado = @sIdEstado ) 
		Begin 
		   Insert Into  CFG_PorcentajesPrecios ( IdEstado, Porcentaje  ) select @sIdEstado, 0 
		End
	End 
	
	Set @sIdEstado = '12' 	
	If Exists ( Select top 1 IdEstado From FarmaciaProductos (NoLock) Where IdEstado = @sIdEstado ) 
	Begin 
		If Not Exists ( Select * From CFG_PorcentajesPrecios (NoLock) Where IdEstado = @sIdEstado ) 
		Begin 
		   Insert Into  CFG_PorcentajesPrecios ( IdEstado, Porcentaje  ) select @sIdEstado, 0 
		End
	End 
	
		
--- Sinaloa es el Unico Estado por el momento que se maneja este porcentaje 
	Set @sIdEstado = '25' 
	If Exists ( Select top 1 IdEstado From FarmaciaProductos (NoLock) Where IdEstado = @sIdEstado ) 
	Begin 
		If Not Exists ( Select * From CFG_PorcentajesPrecios (NoLock) Where IdEstado = @sIdEstado ) 
		Begin 
		   Insert Into  CFG_PorcentajesPrecios ( IdEstado, Porcentaje  ) select @sIdEstado, 20 
		End
	End  	
Go--#SQL  	

-----------------------------------------------------------------------------------------------------
----------------------------------------------------------------------------------------------------- 
If Exists ( Select So.Name From Sysobjects So (NoLock) Where So.Name = 'CFGC_PorcentajesDescuentoPrecios' and xType = 'U' ) 
   Drop Table CFGC_PorcentajesDescuentoPrecios 
Go--#SQL  

Create Table CFGC_PorcentajesDescuentoPrecios 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	Porcentaje numeric(14,4) Not Null Default 0, 
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0 
)
Go--#SQL  

Alter Table CFGC_PorcentajesDescuentoPrecios Add Constraint PK_CFGC_PorcentajesDescuentoPrecios Primary Key ( IdEstado, IdFarmacia ) 
Go--#SQL  

Alter Table CFGC_PorcentajesDescuentoPrecios Add Constraint FK_CFGC_PorcentajesDescuentoPrecios_CatFarmacias 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia  ) 
Go--#SQL  

-- If Not Exists ( Select * From CFGC_PorcentajesDescuentoPrecios Where IdEstado = '25' and IdFarmacia = '0013' )  Insert Into CFGC_PorcentajesDescuentoPrecios Values ( '25', '0013', 8.2500, 'A', 0 )    Else Update CFGC_PorcentajesDescuentoPrecios Set Porcentaje = 8.2500, Status = 'A', Actualizado = 0 Where IdEstado = '25' and IdFarmacia = '0013'
Go--#SQL  
