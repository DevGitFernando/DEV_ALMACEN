-------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_ADM_VALES_Obtener_FolioTimbre' and xType = 'P' )  
   Drop Proc spp_ADM_VALES_Obtener_FolioTimbre  
Go--#SQL 

--		Exec spp_ADM_VALES_Obtener_FolioTimbre '001', '11', '0019', 0, 1

Create Proc spp_ADM_VALES_Obtener_FolioTimbre  
( 
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '11', @IdFarmacia varchar(4) = '0019', 
	@FolioTimbre int = 0, @iOpcion tinyint = 1 
) 
With Encryption 
As 
Begin 
Set NoCount On 

Declare 
	
	@sMensaje varchar(500), @bManejaFE tinyint, @CadenaInteligente varchar(max)

	Set @sMensaje = ''
	Set @bManejaFE = 0
	Set @CadenaInteligente = ''
		
	/*
		Obtener FolioTimbre					-- @iOpcion = 1
		Actualizar FolioTimbre Disponible	-- @iOpcion = 2
	
	*/
	
	Set @bManejaFE = ( 
						Select Case When Valor = 'True' Then 1 Else 0 End As Valor From Net_CFGC_Parametros (Nolock) 
						Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
						and ArbolModulo = 'PFAR' and NombreParametro = 'Vales_Maneja_FE'
					)
	
	If @bManejaFE = 1
		Begin
		
			If @iOpcion = 1
			Begin 
				
				Set @FolioTimbre = IsNull(( 
									Select Top 1 FolioTimbre
									From ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades (nolock)
									Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Disponible = 1
									and DATEPART(yyyy, FechaValidez) = DATEPART(yyyy, GetDate()) 
									and DATEPART(mm, FechaValidez) = DATEPART(mm, GetDate()) 
									Order By NEWID() 
									), 0)
				
				Set @sMensaje = 'El Folio de timbre se obtuvo satisfactoriamente' 
				
				If @FolioTimbre <> 0
					Begin
						Set @CadenaInteligente = ( Select CadenaInteligente From ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades (nolock)
													Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
													and FolioTimbre = @FolioTimbre )
					End
				Else
					Begin
						Set @sMensaje = 'La Unidad no cuenta con Folios de Timbre disponibles para la emision de vales.'
					End
				
			End	
		
			If @iOpcion = 2
				Begin				 
					Update ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades Set Disponible = 0
					Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioTimbre = @FolioTimbre
									
					Set @sMensaje = 'El Folio de timbre se actualizo satisfactoriamente'				
				End			
			
			If @iOpcion = 3
			Begin
				
				If Exists (	
								Select Top 1 FolioTimbre From ADM_VALES_Emision_FoliosDeVales_Detalles_Unidades (nolock)
								Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and Disponible = 1
								and DATEPART(yyyy, FechaValidez) = DATEPART(yyyy, GetDate()) 
								and DATEPART(mm, FechaValidez) = DATEPART(mm, GetDate()) 
								Order By NEWID()
						   ) 
					Begin				
						Set @FolioTimbre = 1						  
					End
				Else
					Begin					
						Set @FolioTimbre = 0
						Set @sMensaje = 'La Unidad no cuenta con Folios de Timbre disponibles para la emision de vales.'					
					End
			End
			
		End
	Else
		Begin
		
			Set @FolioTimbre = 0 
			Set @sMensaje = 'La Unidad no maneja firma electrónica para la emision de vales.'
			
			If @iOpcion = 3
				Begin
					Set @FolioTimbre = 1
					--Set @sMensaje = 'La Unidad no maneja firma electronica para la emision de vales'
				End
		End
		

	Select @FolioTimbre as FolioTimbre, @CadenaInteligente as CadenaInteligente, @sMensaje as Mensaje

End 
Go--#SQL 


