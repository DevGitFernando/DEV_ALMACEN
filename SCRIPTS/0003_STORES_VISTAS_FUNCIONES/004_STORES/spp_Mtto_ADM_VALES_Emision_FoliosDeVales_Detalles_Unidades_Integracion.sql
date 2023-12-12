


---------------------------------------------------------------------------------------------------------------------------------  
If Exists ( Select * From sysobjects (NoLock) Where Name = 'spp_Mtto_ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades_Integracion' and xType = 'P' ) 
   Drop Proc spp_Mtto_ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades_Integracion  
Go--#SQL 

Create Proc spp_Mtto_ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades_Integracion 
( 
	@IdEmpresa varchar(3) = '', @IdEstado varchar(2) = '', @IdFarmacia varchar(4) = '', @FolioTimbre int = 0,
	@Disponible smallint = 1, @CadenaInteligente varchar(1000) = '', @FechaValidez datetime
) 
With Encryption 
As 
Begin 
Set NoCount On 
	
	Declare @iError tinyint
	
	set @iError = 0

	If Exists ( Select * From ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades (Nolock) 
				Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and CadenaInteligente = @CadenaInteligente )
		Begin
			set @iError = 1
		End
	Else
		Begin
		
			Insert Into ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades
			( 
				IdEmpresa, IdEstado, IdFarmacia, FolioTimbre, Disponible, CadenaInteligente, FechaValidez 
			) 
			Select @IdEmpresa, @IdEstado, @IdFarmacia, @FolioTimbre, @Disponible, @CadenaInteligente, @FechaValidez 
		End	
		
	 
	 Select @iError as Error
			  
End 
Go--#SQL 
	