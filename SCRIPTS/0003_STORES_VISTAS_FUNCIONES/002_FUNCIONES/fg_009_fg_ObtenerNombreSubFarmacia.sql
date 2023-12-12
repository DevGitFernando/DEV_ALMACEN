If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_ObtenerNombreSubFarmacia' and xType = 'FN' ) 
   Drop Function fg_ObtenerNombreSubFarmacia 
Go--#SQL

Create Function dbo.fg_ObtenerNombreSubFarmacia
(
	@IdEstado varchar(2) = '25', @IdFarmacia varchar(4) = '0002', @IdSubFarmacia varchar(2) = '01'
)  
Returns varchar(100)  
With Encryption 
As 
Begin 
Declare @sSubFarmacia varchar(100) 

	Set @sSubFarmacia = '' 
	Select @sSubFarmacia = Descripcion 
	From CatFarmacias_SubFarmacias 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and IdSubFarmacia = @IdSubFarmacia 

	return IsNull(@sSubFarmacia, 'NO ENCONTRADO')   
End  
Go--#SQL
 
