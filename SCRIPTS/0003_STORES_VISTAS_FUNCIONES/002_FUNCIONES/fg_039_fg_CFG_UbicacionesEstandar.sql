



If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'fg_Tiene_CFG_UbicacionesEstandar' and xType = 'FN' )
   Drop Function fg_Tiene_CFG_UbicacionesEstandar  
Go--#SQL     
      
Create Function dbo.fg_Tiene_CFG_UbicacionesEstandar( @IdEmpresa varchar(3), @IdEstado varchar(2), @IdFarmacia varchar(4))       
Returns varchar(10)  
With Encryption 
As 
Begin 
Declare 
    @sValor bit    
    
    
    If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'Cat_ALMN_Ubicaciones_Estandar' and xType = 'U' )
    Begin
		If Not Exists ( Select * From Cat_ALMN_Ubicaciones_Estandar  )
			Begin
				Set @sValor = 1 
			End
		Else
			Begin
				Select @sValor = 1 
				From Cat_ALMN_Ubicaciones_Estandar A
				Where Not Exists( Select * From CFG_ALMN_Ubicaciones_Estandar B 
								Where B.IdEmpresa = @IdEmpresa and B.IdEstado = @IdEstado and B.IdFarmacia = @IdFarmacia 
								and B.NombrePosicion = A.NombrePosicion) 
			End
	End
	Else
	Begin
		Set @sValor = 1
	End
    
    Set @sValor = IsNull(@sValor, 0) 

    return @sValor
          
End 
Go--#SQL 

--  select dbo.fg_Tiene_CFG_UbicacionesEstandar( '001', '21', '2182' ) 