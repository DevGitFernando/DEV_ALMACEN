If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_AIEF_Existencia_ClaveSSA' and xType = 'P' ) 
   Drop Proc spp_AIEF_Existencia_ClaveSSA  
Go--#SQL 

Create Proc spp_AIEF_Existencia_ClaveSSA 
(
	@IdEstado varchar(2) = '20', @IdFarmacia varchar(4) = '0114', @ClaveSSA varchar(30) = '1711', @MostrarGrupo bit = 0, 
	@MAC varchar(20) = '', @Host varchar(100) = ''       
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD     

Declare 
	@iCantidad_Clave_Princical int,  
	@Orden int, 
	@sIdGrupoTerapeuticoClave varchar(10), 
	@GrupoTerapeuticoClave varchar(100) 

--- Determinar el grupo al que pertenece la Clave solicitada 
	Set @Orden = 1 
	Select @sIdGrupoTerapeuticoClave = IdGrupoTerapeutico, @GrupoTerapeuticoClave = GrupoTerapeutico  
	From vw_ClavesSSA_Sales Where ClaveSSA = @ClaveSSA 
	
	--- Select * From vw_ClavesSSA_Sales 


	Select @Orden as Orden, ClaveSSA, DescripcionClave, cast(Existencia as int) as Cantidad, MesesParaCaducar as Meses -- , identity(int, 1,1) as  Keyx   
	Into #tmpClaves 
	From vw_ExistenciaPorCodigoEAN_Lotes 
	Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
		  and ClaveSSA = @ClaveSSA 
		  -- and MesesParaCaducar < 0 and Existencia > 0 

	If @MostrarGrupo = 1 
	Begin 
		Set @Orden = 2 
		Insert Into #tmpClaves 
		Select T.Orden, T.ClaveSSA, T.DescripcionClave, sum(Cantidad) as Cantidad, T.Meses
		From 
		( 
			Select @Orden as Orden, ClaveSSA, DescripcionClave, cast(Existencia as int) as Cantidad, MesesParaCaducar as Meses  
			From vw_ExistenciaPorCodigoEAN_Lotes 
			Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia 
				  and ClaveSSA in 
				  (
					 Select ClaveSSA 
					 From vw_ClavesSSA_Sales 
					 Where IdGrupoTerapeutico = @sIdGrupoTerapeuticoClave 
						   and ClaveSSA <> @ClaveSSA 
				  ) 
		) as T 
		Group By T.Orden, T.ClaveSSA, T.DescripcionClave, T.Meses  
		Having sum(Cantidad) > 0 	
	End 


--- Registrar la Claves consultada 
	Exec spp_AIEF_ADT_Existencia_ClavesLog	@ClaveSSA, @MAC, @Host 


----------------------------------------------------------------------------------- 
--- Salida Final de la Existencia soliciada 	
	Select GrupoTerapeutico, ClaveSSA, DescripcionClave, Cantidad 
	From 
	( 
		Select Orden, @GrupoTerapeuticoClave as GrupoTerapeutico, ClaveSSA, DescripcionClave, sum(Cantidad) as Cantidad
		From #tmpClaves 
		Where Meses > 0 -- and Orden in ( 1, @Orden )  
		Group By Orden, ClaveSSA, DescripcionClave 		
	) as T 	
	Order By Orden 
		

---		spp_AIEF_Existencia_ClaveSSA 

End 
Go--#SQL 
