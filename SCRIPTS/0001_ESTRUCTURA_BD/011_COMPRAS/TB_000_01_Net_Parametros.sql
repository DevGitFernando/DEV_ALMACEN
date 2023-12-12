
----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Net_CFG_Parametros_Compras' and xType = 'U' ) 
   Drop Table Net_CFG_Parametros_Compras 
Go--#SQL     

Create Table Net_CFG_Parametros_Compras 
(
	IdEstado varchar(2) Not Null, 
	IdFarmacia varchar(4) Not Null, 
	ArbolModulo varchar(4) Not Null, 
	NombreParametro varchar(100) Not Null, 
	Valor varchar(200) Not Null Default '', 
	Descripcion varchar(500) Not Null, 
	EsDeSistema bit Not Null Default 'False', 
	EsEditable bit Not Null Default 'true',  	
	Status varchar(1) Not Null Default 'A', 
	Actualizado smallint Not Null Default 0  
)
Go--#SQL  

Alter Table Net_CFG_Parametros_Compras Add Constraint PK_Net_CFG_Parametros_Compras Primary Key ( IdEstado, IdFarmacia, ArbolModulo, NombreParametro ) 
Go--#SQL  

Alter Table Net_CFG_Parametros_Compras Add Constraint FK_Net_CFG_Parametros_Compras 
	Foreign Key ( IdEstado, IdFarmacia ) References CatFarmacias ( IdEstado, IdFarmacia ) 
Go--#SQL  


----------------------------------------------------------------------------------------------------------------------------------
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Mtto_Net_CFG_Parametros_Compras' and xType = 'P' )
   Drop Proc spp_Mtto_Net_CFG_Parametros_Compras 
Go--#SQL   

Create Proc spp_Mtto_Net_CFG_Parametros_Compras 
( 
	@IdEstado varchar(2), @IdFarmacia varchar(4), 
	@ArbolModulo varchar(4), @NombreParametro varchar(50), @Valor varchar(200), @Descripcion varchar(500), 
	@EsDeSistema bit = 0, @EsEditable bit = 0, @Actualizar tinyint = 0 
) 
With Encryption 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From Net_CFG_Parametros_Compras (NoLock) 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ArbolModulo = @ArbolModulo and NombreParametro = @NombreParametro ) 
	   Insert Into Net_CFG_Parametros_Compras ( IdEstado, IdFarmacia, ArbolModulo, NombreParametro, Valor, Descripcion, EsDeSistema, Status, EsEditable ) 
	   Select @IdEstado, @IdFarmacia, @ArbolModulo, @NombreParametro, upper(@Valor), @Descripcion, @EsDeSistema, 'A', @EsEditable 
	Else 
	   Begin 
	       If @Actualizar = 1 
	          Begin  
			     Update Net_CFG_Parametros_Compras Set Status = 'A', Valor = upper(@Valor), Actualizado = 0 
			     Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and ArbolModulo = @ArbolModulo and NombreParametro = @NombreParametro
			  End 
	   End 
End 
Go--#SQL  

