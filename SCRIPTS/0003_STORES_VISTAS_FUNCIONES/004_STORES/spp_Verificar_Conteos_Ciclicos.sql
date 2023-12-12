

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Verificar_Conteos_Ciclicos' and xType = 'P' )
   Drop proc spp_Verificar_Conteos_Ciclicos
Go--#SQL 


	----  Exec spp_Verificar_Conteos_Ciclicos '001', '11', '0005'

Create Proc spp_Verificar_Conteos_Ciclicos 
(  
	@IdEmpresa  varchar(3)= '001', @IdEstado varchar(2)= '11', @IdFarmacia varchar(4) = '0005' 
) 
With Encryption 
As 
Begin 
	Set dateformat YMD
	Set NoCount On 

	Declare @Año int, @Mes int, @FolioConteo varchar(30), @Resultado int
	
	Set @Año = 0
	Set @Mes = 0
	Set @FolioConteo = ''
	Set @Resultado = 0
	
	If Exists ( Select * From Inv_ConteosCiclicosEnc Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia )
	Begin
		Select top 1 @Año = datepart(yyyy, FechaRegistro), @Mes = datepart(mm, FechaRegistro), @FolioConteo = FolioConteo From Inv_ConteosCiclicosEnc 
		Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia
		Order By FolioConteo Desc
		
		If (@Año = datepart(yyyy, getdate())) and (@Mes = datepart(mm, getdate())) 
		Begin
			Set @Resultado = 1
		End
			
	End		
	
	--------    spp_Verificar_Conteos_Ciclicos
	 
	 Select @Resultado as Resultado, @FolioConteo as Folio

End 
Go--#SQL 