If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_AIEF_CuadroBasico' and xType = 'P' ) 
   Drop Proc spp_AIEF_CuadroBasico 
Go--#SQL 

Create Proc spp_AIEF_CuadroBasico 
(
	@IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0014'   
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD     
	
	
	Select ClaveSSA, DescripcionClave -- , Presentacion 
	From vw_CB_CuadroBasico_Farmacias 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		  and StatusMiembro = 'A' and StatusClave = 'A'
	Order By DescripcionClave 

End 
Go--#SQL 


