
----------------------------------------------------------------------------------------------------------------------------------------------- 
----------------------------------------------------------------------------------------------------------------------------------------------- 
If Exists ( Select * From Sysobjects (NoLock) Where Name = 'spp_Rpt_Administrativos_Update_Niveles_Atencion' and xType = 'P' ) 
   Drop Proc spp_Rpt_Administrativos_Update_Niveles_Atencion
Go--#SQL  
 
 ----	Exec spp_Rpt_Administrativos_Update_Niveles_Atencion '001', '21', '2132'
 
 
Create Proc spp_Rpt_Administrativos_Update_Niveles_Atencion 
(
	@IdEmpresa varchar(3) = '001', @IdEstado varchar(2) = '21', @IdFarmacia varchar(4) = '2113'
) 
With Encryption 
As 
Begin 
Set NoCount On 

/* 
	select * from RptAdmonDispensacion (nolock)

	drop table RptAdmonDispensacion 
	
	update RptAdmonDispensacion Set IdPerfilAtencion = 0, IdSubPerfilAtencion = 0, PerfilDeAtencion = '' 
	update RptAdmonDispensacion_Detallado Set IdPerfilAtencion = 0, IdSubPerfilAtencion = 0, PerfilDeAtencion = '' 

*/

	If Exists( Select * From CFGC_FAR_CB_NivelesAtencion_Miembros (Nolock) Where IdEstado = @IdEstado and IdFarmacia = @IdFarmacia )
		Begin 
			--Update T Set T.IdPerfilAtencion = M.IdPerfilAtencion, T.IdSubPerfilAtencion = M.IdSubPerfilAtencion, T.PerfilDeAtencion = N.PerfilDeAtencion 
			--From RptAdmonDispensacion T (Nolock) 
			--Inner Join vw_CFGC_FAR_CB_NivelesAtencion_ClavesSSA N (Nolock)
			--	On ( N.IdEmpresa = T.IdEmpresa and N.IdEstado = T.IdEstado and N.IdCliente = T.IdCliente and N.IdSubCliente = T.IdSubCliente
			--		and N.IdPrograma = T.IdPrograma and N.IdSubPrograma = T.IdSubPrograma and N.ClaveSSA = T.ClaveSSA )
			--Inner Join CFGC_FAR_CB_NivelesAtencion_Miembros M (NoLock) 
			--	On ( T.IdEstado = M.IdEstado And T.IdFarmacia = M.IdFarmacia And N.IdPerfilAtencion = M.IdPerfilAtencion and N.IdSubPerfilAtencion = M.IdSubPerfilAtencion )
			--Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia  			
			

			Update T Set T.IdPerfilAtencion = M.IdPerfilAtencion, T.IdSubPerfilAtencion = M.IdSubPerfilAtencion, T.PerfilDeAtencion = N.PerfilDeAtencion
			From RptAdmonDispensacion_Detallado T (Nolock)
			Inner Join vw_CFGC_FAR_CB_NivelesAtencion_ClavesSSA N (Nolock)
				On ( N.IdEmpresa = T.IdEmpresa and N.IdEstado = T.IdEstado and N.IdCliente = T.IdCliente and N.IdSubCliente = T.IdSubCliente
					and N.IdPrograma = T.IdPrograma and N.IdSubPrograma = T.IdSubPrograma and N.ClaveSSA = T.ClaveSSA )
			Inner Join CFGC_FAR_CB_NivelesAtencion_Miembros M (NoLock) 
				On ( T.IdEstado = M.IdEstado And T.IdFarmacia = M.IdFarmacia And N.IdPerfilAtencion = M.IdPerfilAtencion and N.IdSubPerfilAtencion = M.IdSubPerfilAtencion )
			Where T.IdEmpresa = @IdEmpresa and T.IdEstado = @IdEstado and T.IdFarmacia = @IdFarmacia  			



			Update T Set T.IdPerfilAtencion = M.IdPerfilAtencion, T.IdSubPerfilAtencion = M.IdSubPerfilAtencion, T.PerfilDeAtencion = M.PerfilDeAtencion
			From RptAdmonDispensacion_Detallado M (Nolock)
			Inner Join RptAdmonDispensacion T (Nolock) 
				On ( T.IdEmpresa = M.IdEmpresa and T.IdEstado = M.IdEstado and T.IdFarmacia = M.IdFarmacia and T.Folio = M.Folio 
					and T.ClaveSSA = M.ClaveSSA ) 


 ----	Exec spp_Rpt_Administrativos_Update_Niveles_Atencion 

		End	
	
	Update T Set T.PerfilDeAtencion = 'SIN ESPECIFICAR'
	From RptAdmonDispensacion T (Nolock) Where T.IdPerfilAtencion = 0
	
	Update T Set T.PerfilDeAtencion = 'SIN ESPECIFICAR'
	From RptAdmonDispensacion_Detallado T (Nolock) Where T.IdPerfilAtencion = 0	
	

	--Update B Set IdCliente = '0001', 
	--	--NombreCliente = '', 
	--	--NombreSubCliente = '', 
	--	Programa = '', SubPrograma = ''
	--From RptAdmonDispensacion B (NoLock) 
	--Where B.PerfilDeAtencion like '%oportunidades%'

	--Update B Set IdCliente = '0001', 
	--	--NombreCliente = '', 
	--	--NombreSubCliente = '', 
	--	Programa = '', SubPrograma = ''
	--From RptAdmonDispensacion_Detallado B (NoLock) 
	--Where B.PerfilDeAtencion like '%oportunidades%'
	
---------------------------------------------------  SALIDA FINAL 	
	Select Distinct IdPerfilAtencion, IdSubPerfilAtencion, PerfilDeAtencion 
	From RptAdmonDispensacion_Detallado T (Nolock)
	Order By IdPerfilAtencion, IdSubPerfilAtencion  
 	
			
End
Go--#SQL 