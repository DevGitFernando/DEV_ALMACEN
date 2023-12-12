

If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_Obtener_Claves_ConteosCiclicos' and xType = 'P' )
   Drop proc spp_Obtener_Claves_ConteosCiclicos
Go--#SQL 


	----  Exec spp_Obtener_Claves_ConteosCiclicos '001', '11', '0005', '00000001'

Create Proc spp_Obtener_Claves_ConteosCiclicos 
(  
	@IdEmpresa  varchar(3)= '001', @IdEstado varchar(2)= '11', @IdFarmacia varchar(4) = '0005', @FolioConteo varchar(30) = '00000001' 
) 
With Encryption 
As 
Begin 
	Set dateformat YMD
	Set NoCount On 

	Declare @Resultado int
	
	Set @Resultado = 0
	
	Select top 0 space(30) as ClaveSSA, space(8000) as DescripcionClave, space(3) as Categoria Into #tmpClaves
	
	Select Categoria, cast((Claves/25) as Int) as Claves Into #tmpCategorias From Inv_ConteosCiclicos_Resumen
	Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia and FolioConteo = @FolioConteo
	
	If Exists ( Select * From Inv_ConteosCiclicos_Claves Where IdEmpresa = @IdEmpresa and IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				and FolioConteo = @FolioConteo and Convert(varchar(10), FechaRegistro, 120) = Convert(varchar(10), GetDate(), 120) ) 
		Begin
		
			Insert Into #tmpClaves
			Select C.ClaveSSA, S.DescripcionSal, C.Categoria
			From Inv_ConteosCiclicos_Claves C
			Inner Join vw_ClavesSSA_Sales S On ( S.ClaveSSA = C.ClaveSSA )
			Where C.IdEmpresa = @IdEmpresa and C.IdEstado = @IdEstado and C.IdFarmacia = @IdFarmacia 
			and C.FolioConteo = @FolioConteo and Convert(varchar(10), FechaRegistro, 120) = Convert(varchar(10), GetDate(), 120)
			
		End
	Else
		Begin
				Declare @Category varchar(3), @Claves int
				
				Set @Category = ''
				Set @Resultado = 1
				
				Declare Categorias
				cursor Local For
					Select Categoria, Claves From #tmpCategorias
					OPEN Categorias
						FETCH NEXT FROM Categorias INTO @Category, @Claves
							WHILE ( @@FETCH_STATUS = 0 ) 
								BEGIN
									
									Insert Into #tmpClaves
									Select top (@Claves) C.ClaveSSA, S.DescripcionSal, C.Categoria
									From Inv_ConteosCiclicosDet C
									Inner Join vw_ClavesSSA_Sales S On ( S.ClaveSSA = C.ClaveSSA )
									Where C.IdEmpresa = @IdEmpresa and C.IdEstado = @IdEstado and C.IdFarmacia = @IdFarmacia 
									and C.FolioConteo = @FolioConteo and C.Categoria = @Category and Not Exists 
										( Select * From Inv_ConteosCiclicos_Claves I Where I.IdEmpresa = C.IdEmpresa and I.IdEstado = C.IdEstado 
											and I.IdFarmacia = C.IdFarmacia and I.FolioConteo = C.FolioConteo and I.ClaveSSA = C.ClaveSSA 
											and I.Categoria = C.Categoria )
									Order By RAND()
									
															
									FETCH NEXT FROM Categorias INTO  @Category, @Claves
								END 
					CLOSE Categorias
				DEALLOCATE Categorias
			
		End
	
	
	
	--------    spp_Obtener_Claves_ConteosCiclicos
	 
	 Select *, @Resultado as Resultado From #tmpClaves 

End 
Go--#SQL 