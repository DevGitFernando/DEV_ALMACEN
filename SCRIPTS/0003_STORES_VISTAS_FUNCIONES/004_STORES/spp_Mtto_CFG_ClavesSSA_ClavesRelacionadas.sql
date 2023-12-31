------------------------------------------------------------------------------------------------------------------------------------ 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Mtto_CFG_ClavesSSA_ClavesRelacionadas' and xType = 'P' ) 
   Drop Proc spp_Mtto_CFG_ClavesSSA_ClavesRelacionadas 
Go--#SQL    

Create Proc spp_Mtto_CFG_ClavesSSA_ClavesRelacionadas 
( 
	@IdEstado varchar(2) = '', @IdCliente varchar(4) = '', @IdSubCliente varchar(4) = '', 
	@IdClaveSSA varchar(4) = '', @IdClaveSSA_Relacionada varchar(4) = '', @Status varchar(1) = '', 
	@Multiplo Int = 1, @AfectaVenta smallint = 1, @AfectaConsigna smallint = 0  
) 
As 
Begin 
Set NoCount On 

	If Not Exists ( Select * From CFG_ClavesSSA_ClavesRelacionadas (NoLock) 
		Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente  
			and @IdClaveSSA = IdClaveSSA and IdClaveSSA_Relacionada = @IdClaveSSA_Relacionada ) 
		Begin 
			Insert Into CFG_ClavesSSA_ClavesRelacionadas ( IdEstado, IdCliente, IdSubCliente, IdClaveSSA, IdClaveSSA_Relacionada, Status, Actualizado, Multiplo ) 
		   select @IdEstado, @IdCliente, @IdSubCliente, @IdClaveSSA, @IdClaveSSA_Relacionada, 'A', 0, @Multiplo 
		End 
	Else 
		Begin 
		   Update CFG_ClavesSSA_ClavesRelacionadas 
				Set Status = @Status, Actualizado = 0, 
					Multiplo = (case when @Multiplo = 0 then Multiplo else @Multiplo end)  
		   Where IdEstado = @IdEstado and IdCliente = @IdCliente and IdSubCliente = @IdSubCliente  
				and @IdClaveSSA = IdClaveSSA and IdClaveSSA_Relacionada = @IdClaveSSA_Relacionada 
		End 

End 
Go--#SQL    


