If Exists ( Select Name From Sysobjects (NoLock) Where Name = 'spp_AIE_Lista_ClavesLicitadas' and xType = 'P' ) 
   Drop Proc spp_AIE_Lista_ClavesLicitadas 
Go--#SQL 

Create Proc spp_AIE_Lista_ClavesLicitadas 
(
	@IdAccesoExterno varchar(4) = '0001', @MostrarTodo int = 0 
) 
With Encryption 
As 
Begin 
Set NoCount On 
Set DateFormat YMD     
	
	
	If @MostrarTodo = 1 
		Begin 
			Select ClaveSSA, DescripcionClave, Presentacion, ContenidoPaquete, EsControlado, EsAntibiotico, GrupoTerapeutico, 
				(case when Status = 'A' Then 'Activa' Else 'Inactiva' end) as Status 
			From vw_AIE_Claves_Licitadas  
			Where IdAccesoExterno = @IdAccesoExterno 
		End 
	Else 	
		Begin 
			Select ClaveSSA, DescripcionClave, Presentacion, (case when Status = 'A' Then 'Activa' Else 'Inactiva' end) as Status  
			From vw_AIE_Claves_Licitadas  
			Where IdAccesoExterno = @IdAccesoExterno 
		End 		
	
--	sp_listacolumnas vw_AIE_Claves_Licitadas 
End 
Go--#SQL 
